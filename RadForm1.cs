using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Telerik.WinControls;
using Telerik.WinControls.UI;
using System.Data.SqlClient;
using System.IO;
using System.Threading.Tasks;

namespace GenerateArkaveImport
{
    public partial class RadForm1 : Telerik.WinControls.UI.RadForm
    {
        string dbTr;
        string dbname;
        string livrableID = "0";
        string conString;
        public RadForm1()
        {
            InitializeComponent();
        }

        private void btnConnexion_Click(object sender, EventArgs e)
        {
            string server = tbServeur.Text;
            string Login = tbLogin.Text;
            string password = tbPassword.Text;

            conString = $"Server = {server}; Database = master ; User ID = {Login}; Password = {password}";
            using (SqlConnection con = new SqlConnection(conString))
            {
                try
                {
                    con.Open();
                    string REQ = "SELECT name FROM sys.databases where name like'%_ANCFCC'";
                    SqlDataAdapter DA = new SqlDataAdapter(REQ, con);
                    DataTable DT = new DataTable();
                    DA.Fill(DT);
                    if (DT.Rows.Count == 0)
                    {
                        MessageBox.Show("aucune BaseDonnée");
                        return;
                    }
                    else
                    {
                        ComboBaseDonnee.DataSource = DT;
                        ComboBaseDonnee.DisplayMember = "name";
                        ComboBaseDonnee.ValueMember = "name";
                        ComboBaseDonnee.SelectedIndexChanged += ComboBaseDonnee_SelectedIndexChanged;
                    }
                    MessageBox.Show("Connexion OK!!!");

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }

        }

        private async void bntGenerer_Click(object sender, EventArgs e)
        {
            if (dbTr == null || dbname == null)
            {
                MessageBox.Show("Test");
                return;
            }
            FolderBrowserDialog folder = new FolderBrowserDialog();
            if (folder.ShowDialog() == DialogResult.OK)
            {
                string path = folder.SelectedPath;
                using (SqlConnection con = new SqlConnection(conString))
                {
                    try
                    {
                        con.Open();
                        string id_tranche = dbTr;
                        string reqD = $"select top 10 * from TB_Dossier where id_unite in(select id_unite from tb_unite where id_tranche = {id_tranche})and id_livrable is null";
                        SqlDataAdapter da = new SqlDataAdapter(reqD, con);
                        DataTable DT = new DataTable();
                        da.Fill(DT);
                        var nb_dossies = DT.Rows.Count;
                        radProgressBar1.Maximum = nb_dossies;

                        // The Progress<T> constructor captures our UI context,
                        //  so the lambda will be run on the UI thread.

                        var progress = new Progress<int>(percent =>
                        {
                            radProgressBar1.Value1 = percent;
                            radProgressBar1.Text = $"{percent * 100 / nb_dossies} %";
                            radLabel1.Text = $"{percent}/{nb_dossies} Dossiers";
                        });

                        // DoProcessing is run on the thread pool.
                        await Task.Run(() => GenererImport(path, DT, progress));

                        MessageBox.Show("Géneration Complete!!!");

                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }
            }
        }

        private void GenererImport(string path, DataTable dT, IProgress<int> progress)
        {
            
            DB dB = new DB(conString);
            int i = 0; //progress
            // Dossiers
            livrableID = dB.CreerLigneLivrable( dbTr, dbname);
            if (livrableID == "0")
            {

                MessageBox.Show("Livrable n'est pas créer");
            }
            else
            {
                foreach (DataRow doss in dT.Rows)
                {
                    string nomDoss = "";
                    string NatureOrigine = doss["nature_origine"].ToString().Trim();
                    string id_dossier = doss["id_dossier"].ToString();
                    if (NatureOrigine == "R")
                    {
                        string NumeroOrigine = doss["numero_origine"].ToString().Trim();
                        string indiceOrigine = doss["indice_origine"].ToString().Trim();
                        nomDoss = $"R{NumeroOrigine}-{indiceOrigine}";
                    }
                    else if (NatureOrigine == "T")
                    {
                        string NumeroOrigine = doss["numero_titre"].ToString().Trim();
                        string indiceOrigine = doss["indice_Titre"].ToString().Trim();
                        nomDoss = $"T{NumeroOrigine}-{indiceOrigine}";
                    }
                    string dossPath = Path.Combine(path, nomDoss);
                    if (!Directory.Exists(dossPath))
                    {
                        Directory.CreateDirectory(dossPath);
                    }
                    // Pieces
                    DataTable pieces = dB.Piece(id_dossier);
                    foreach (DataRow piece in pieces.Rows)
                    {
                        string num_page = piece["Num_page"].ToString().Trim();
                        string nomPiece = piece["nom_piece"].ToString().Trim();
                        string chemin = piece["url"].ToString().Trim();
                        string num_sd = piece["num_sous_dos"].ToString().Trim();
                        if (num_sd == "") num_sd = "0";
                        int numSousDossier = Convert.ToInt32(num_sd);
                        String Formalite = piece["formalite"].ToString().Trim();

                        String SousDossierName = numSousDossier != 0 ? $"sd{numSousDossier} {Formalite}" : "sd0";

                        string sdPath = Path.Combine(dossPath, SousDossierName);
                        if (!Directory.Exists(sdPath))
                        {
                            Directory.CreateDirectory(sdPath);
                        }

                        string piecePath = Path.Combine(sdPath, nomPiece);
                        if (!Directory.Exists(piecePath))
                        {
                            Directory.CreateDirectory(piecePath);
                        }
                        //test Images
                        chemin = @"C:\Users\HP\Desktop\p1.tif";
                        string imgNewPath = Path.Combine(piecePath, $"p{num_page}{new FileInfo(chemin).Extension}");
                        if (!File.Exists(imgNewPath) && File.Exists(chemin))
                        {
                            File.Copy(chemin, imgNewPath);
                        }
                    }

                    if (livrableID != "0")
                    {
                        dB.UpdateDossierLivrable(id_dossier, livrableID);
                    }
                    
                    progress.Report(++i);
                }
            }


        }

        private void btnAnnuler_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void ComboBaseDonnee_SelectedIndexChanged(object sender, EventArgs e)
        {
            dbname = ComboBaseDonnee.Text;
            conString = conString.Replace("master", dbname);

            using (var con = new SqlConnection(conString))
            {
                con.Open();
                string req = " select DISTINCT id_tranche from TB_UNITE  ";

                SqlDataAdapter da = new SqlDataAdapter(req, con);
                DataTable dt = new DataTable();
                da.Fill(dt);
                if (dt.Rows.Count == 0)
                {
                    MessageBox.Show("aucun tranche");

                    return;
                }
                else
                {
                    comboxTranche.DataSource = dt;
                    comboxTranche.DisplayMember = "id_tranche";
                    comboxTranche.ValueMember = "id_tranche";
                    comboxTranche.SelectedIndexChanged += ComboxTranche_SelectedIndexChanged;
                }
            }
        }

        private void ComboxTranche_SelectedIndexChanged(object sender, EventArgs e)
        {
            dbTr = comboxTranche.Text;
        }
    }
}
