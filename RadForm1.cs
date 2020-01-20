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

namespace GenerateArkaveImport
{
    public partial class RadForm1 : Telerik.WinControls.UI.RadForm
    {
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

            conString = $"Server = {server}; Database = CADASTRE_CONTROLE_TAZA; User ID = {Login}; Password = {password}";
            using (SqlConnection con = new SqlConnection(conString))
            {
                try
                {
                    con.Open();
                    MessageBox.Show("Connection Avec Succe");
                    string req = "select DISTINCT id_tranche from TB_UNITE";
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
                        comboxTranche.DataSource = dt.DefaultView;
                        comboxTranche.DisplayMember = "id_tranche";
                        comboxTranche.ValueMember = "id_tranche";
                    }
                    if (comboxTranche.Items.Count > 0)
                    {
                        bntGenerer.Enabled = true;
                    }

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }

        }

        private void comboxTranche_SelectedIndexChanged(object sender, EventArgs e)
        {
            /*DataRowView view = comboxTranche.SelectedItem as DataRowView;      
            int id_tranche = Convert.ToInt32(view["id_tranche"]);*/
        }

        private void bntGenerer_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog folder = new FolderBrowserDialog();
            if (folder.ShowDialog() == DialogResult.OK)
            {
                string path = folder.SelectedPath;

                //string connectString = $"Server = {server}; Database = CADASTRE_CONTROLE_TAZA; User ID = {Login}; Password = {password}";

                using (SqlConnection con = new SqlConnection(conString))
                {
                    try
                    {
                        con.Open();
                        string id_tranche = comboxTranche.SelectedValue.ToString();
                        string reqD = $"select top 10 * from dossier where id_unite in(select id_unite from tb_unite where id_tranche = {id_tranche})";
                        SqlDataAdapter da = new SqlDataAdapter(reqD, con);
                        DataTable DT = new DataTable();
                        da.Fill(DT);
                        // Dossiers
                        foreach (DataRow doss in DT.Rows)
                        {
                            string nomDoss = "";
                            string NatureOrigine = doss["nature_origine"].ToString();
                            string id_dossier = doss["id_dossier"].ToString();
                            if (NatureOrigine == "R")
                            {
                                string NumeroOrigine = doss["Numero_origine"].ToString();
                                string indiceOrigine = doss["indice_origine"].ToString();
                                nomDoss = $"R{NumeroOrigine}-{indiceOrigine}";
                            }
                            else if (NatureOrigine == "T")
                            {
                                string NumeroOrigine = doss["Numero_Titre"].ToString();
                                string indiceOrigine = doss["indice_Titre"].ToString();
                                nomDoss = $"R{NumeroOrigine}-{indiceOrigine}";
                            }
                            string dossPath = Path.Combine(path, nomDoss);
                            if (!Directory.Exists(dossPath))
                            {
                                Directory.CreateDirectory(dossPath);
                            }
                            DB dB = new DB(conString);
                            // Pieces sans hors sous dossiers
                            DataTable piecesSD0 = dB.PieceSD0(id_dossier);
                            foreach (DataRow piece in piecesSD0.Rows)
                            {
                                string num_pg = piece["Numero_page"].ToString();
                                string nomPiece = piece["Nature_Acte"].ToString();
                                string chemin = piece["CHEMIN_PHYSIQUE"].ToString();

                                string sdPath = Path.Combine(dossPath, "sd0");
                                if (!Directory.Exists(sdPath))
                                {
                                    Directory.CreateDirectory(sdPath);
                                }

                                string piecePath = Path.Combine(sdPath, nomPiece);
                                if (!Directory.Exists(piecePath))
                                {
                                    Directory.CreateDirectory(piecePath);
                                }

                                string imgNewPath = Path.Combine(piecePath, $"p{num_pg}{new FileInfo(chemin).Extension}");
                                if (!File.Exists(imgNewPath))
                                {
                                    File.Copy(chemin, imgNewPath);
                                }
                            }
                            // Sous Dossiers
                            DataTable souDoss = dB.SousDossier(id_dossier);
                            foreach (DataRow sd in souDoss.Rows)
                            {
                                string id_sd = sd["ID_SD"].ToString();
                                string numSD = sd["NUMERO_SD"].ToString();
                                string formaliteSD = sd["FORMALITE"].ToString();
                                string sdName = $"sd{numSD} {formaliteSD}";
                                string sdPath = Path.Combine(dossPath, sdName);
                                if (Directory.Exists(sdPath))
                                {
                                    Directory.CreateDirectory(sdPath); 
                                }


                                // Pieces dans sous dossier
                                DataTable pieces = dB.Piece(id_sd);
                                foreach (DataRow piece in pieces.Rows)
                                {
                                    string num_pg = piece["Numero_page"].ToString();
                                    string nomPiece = piece["Nature_Acte"].ToString();
                                    string chemin = piece["CHEMIN_PHYSIQUE"].ToString();
                                    string piecePath = Path.Combine(sdPath, nomPiece);
                                    if (!Directory.Exists(piecePath))
                                    {
                                        Directory.CreateDirectory(piecePath);
                                    }

                                    string imgNewPath = Path.Combine(piecePath, $"p{num_pg}{new FileInfo(chemin).Extension}");
                                    if (!File.Exists(imgNewPath))
                                    {
                                        File.Copy(chemin, imgNewPath);
                                    }
                                }
                            }

                        }
                        MessageBox.Show("Géneration Complete!!!");

                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }

                }
            }
        }

        private void btnAnnuler_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
