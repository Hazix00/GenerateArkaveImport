using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;

namespace GenerateArkaveImport
{
    class DB
    {
        string conString;

        public DB(string conString)
        {
            this.conString = conString;
        }

        public DataTable SousDossier(string id_dossier)
        {
            using (SqlConnection con = new SqlConnection(conString))
            {
                con.Open();
                string reqD = $"select * from sous_dossier where id_dossier ='{id_dossier}'";
                SqlDataAdapter da = new SqlDataAdapter(reqD, con);
                DataTable DT = new DataTable();
                da.Fill(DT);
                return DT;
            }
        }

        public DataTable Piece(string id_sd)
        {
            using (SqlConnection con = new SqlConnection(conString))
            {
                con.Open();
                string reqD = $"select * from piece where id_SD ='{id_sd}'";
                SqlDataAdapter da = new SqlDataAdapter(reqD, con);
                DataTable DT = new DataTable();
                da.Fill(DT);
                return DT;
            }
        }

    }
}
