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
            folder.ShowDialog();
            string path = folder.SelectedPath;
            
            //string connectString = $"Server = {server}; Database = CADASTRE_CONTROLE_TAZA; User ID = {Login}; Password = {password}";

            using (SqlConnection con = new SqlConnection(conString))
            {
                try
                {

                    con.Open();

                    string reqD = "select * from dossier ";

                    SqlDataAdapter da = new SqlDataAdapter(reqD, con);
                    DataTable DT = new DataTable();
                    da.Fill(DT);

                    foreach(DataRow row in DT.Rows)
                    {
                        


                    }

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }

            }
        }
    }
}
