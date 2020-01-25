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
         public void UpdateDossierLivrable(string id_dossier,string id_livrable)
        {
            using (SqlConnection con = new SqlConnection(conString))
            {
                con.Open();
                string reqUP = $"update tb_dossier set id_livrable= {id_livrable} where id_dossier={id_dossier}";
                SqlCommand cmdUp = new SqlCommand(reqUP, con);
                cmdUp.ExecuteNonQuery();

            }

        }


        public DataTable Piece(string id_dossier)
        {
            using (SqlConnection con = new SqlConnection(conString))
            {
                con.Open();
                string reqD = $"select rtrim(nom_piece) nom_piece,rtrim(Num_page) Num_page,rtrim(url) url,rtrim(num_sous_dos) num_sous_dos,rtrim(formalite) formalite from TB_Vues where id_dossier ={id_dossier}";
                SqlDataAdapter da = new SqlDataAdapter(reqD, con);
                DataTable DT = new DataTable();
                da.Fill(DT);
                return DT;
            }
        }

        public string CreerLigneLivrable ( string id_tranche, string NomBase)
        {
            using (SqlConnection con = new SqlConnection(conString))
            {
                con.Open();
                string reqD = $" select tranche From TB_Tranche where id_tranche = { id_tranche }";
                SqlDataAdapter da = new SqlDataAdapter(reqD, con);
                DataTable DT = new DataTable();
                da.Fill(DT);

                string nomlivrable = NomBase + "_" + DT.Rows[0]["tranche"].ToString();
                string LivInsertion = $"INSERT into TB_Livrable ( date_livrable, user_livrable, nom_livrable , etat) output INSERTED.id_livrable " +
                                      $" VALUES ( GETDATE(), 'BatshGeneration','{nomlivrable}',0)";

                SqlCommand cmd = new SqlCommand(LivInsertion, con);
                return cmd.ExecuteScalar().ToString();
            }

        }


    }
}
