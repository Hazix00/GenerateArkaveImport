using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Telerik.WinControls;
using Telerik.WinControls.UI;

namespace GenerateArkaveImport
{
    public partial class RadForm1 : Telerik.WinControls.UI.RadForm
    {
        public RadForm1()
        {
            InitializeComponent();
        }

        private void btnconx_Click(object sender, EventArgs e)
        {
            string server = TBserverName.Text;
            string Login = TBLogin.Text;
            string password = TBpassword.Text;

            string connectString = $"Server = {server}; Database = CADASTRE_CONTROLE_TAZA; User ID = {Login}; Password = {password}";
            using (SqlConnection con = new SqlConnection(connectString))
            {
                try
                {
                    con.Open();
                    MessageBox.Show("Connection OK");
                    string req = "select DISTINCT id_tranche from TB_UNITE";
                    SqlDataAdapter da = new SqlDataAdapter(req, con);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    if (dt.Rows.Count == 0)  
                    {
                        MessageBox.Show("aucun tranche");

                        return;
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
