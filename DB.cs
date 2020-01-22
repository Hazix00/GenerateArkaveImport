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

        //public DataTable SousDossier(string id_dossier)
        //{
        //    using (SqlConnection con = new SqlConnection(conString))
        //    {
        //        con.Open();
        //        string reqD = $"select * from TB_Vues where id_dossier ='{id_dossier}'";
        //        SqlDataAdapter da = new SqlDataAdapter(reqD, con);
        //        DataTable DT = new DataTable();
        //        da.Fill(DT);
        //        return DT;
        //    }
        //}

        //public DataTable PieceSD0(string id_dossier)
        //{
        //    using (SqlConnection con = new SqlConnection(conString))
        //    {
        //        con.Open();
        //        string reqD = $"select * from TB_Vues where id_dossier ='{id_dossier}' and (num_sous_dos is null or num_sous_dos = '') ";
        //        SqlDataAdapter da = new SqlDataAdapter(reqD, con);
        //        DataTable DT = new DataTable();
        //        da.Fill(DT);
        //        return DT;
        //    }
        //}

        public DataTable Piece(string id_dossier)
        {
            using (SqlConnection con = new SqlConnection(conString))
            {
                con.Open();
                string reqD = $"select * from TB_Vues where id_dossier ='{id_dossier}'";
                SqlDataAdapter da = new SqlDataAdapter(reqD, con);
                DataTable DT = new DataTable();
                da.Fill(DT);
                return DT;
            }
        }


    }
}
