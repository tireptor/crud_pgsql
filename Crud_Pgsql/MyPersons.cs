using System;
using System.Collections.Generic;

//On inclue la librairie
using Npgsql;
using NpgsqlTypes;
//Fin
using System.Linq;
using System.Data;
using System.Text;

namespace TutoDeveloppez
{
    //Réalisé par José Landry BOGBE le 09/07/2014 a 10 heures 39 minutes
    public class MyPersons
    {
        string Conx = "Server=localhost;Port=5432;Database=db_crud;User Id=postgres;Password=a;";
        NpgsqlCommand MyCmd = null;
        NpgsqlConnection MyCnx = null;

        public void InsertPersons(string nom, string prenom, string tel, string adresse)
        {
            MyCnx = new NpgsqlConnection(Conx);
            string insert = "INSERT INTO \"Personne\"(id,nom,prenom,telephone,adresse) values(DEFAULT,:nom,:prenom,:tel,:adresse)";
            //La valeur DEFAULT parce que la propriété id est auto incrémenté
            MyCnx.Open();
            MyCmd = new NpgsqlCommand(insert, MyCnx);

            //Définition  et ajout des paramètres 

            MyCmd.Parameters.Add(new NpgsqlParameter("nom", NpgsqlDbType.Varchar)).Value = nom;
            MyCmd.Parameters.Add(new NpgsqlParameter("prenom", NpgsqlDbType.Varchar)).Value = prenom;
            MyCmd.Parameters.Add(new NpgsqlParameter("tel", NpgsqlDbType.Varchar)).Value = tel;
            MyCmd.Parameters.Add(new NpgsqlParameter("adresse", NpgsqlDbType.Varchar)).Value = adresse;

            MyCmd.ExecuteNonQuery(); //Exécution
            MyCnx.Close();
        }

        public void UpdatePersons(int id, string nom, string prenom, string tel, string adresse)
        {
            MyCnx = new NpgsqlConnection(Conx); //Instanciation
            string update = "UPDATE  \"Personne\"  SET nom =:pnom ,prenom=:pprenom,telephone=:ptel,adresse=:padresse WHERE(id=:pid);";
            MyCnx.Open();

            MyCmd = new NpgsqlCommand(update, MyCnx);

            //Définition  et ajout des paramètres 
            MyCmd.Parameters.Add(new NpgsqlParameter("pid", NpgsqlDbType.Varchar)).Value = id;
            MyCmd.Parameters.Add(new NpgsqlParameter("pnom", NpgsqlDbType.Varchar)).Value = nom;
            MyCmd.Parameters.Add(new NpgsqlParameter("pprenom", NpgsqlDbType.Varchar)).Value = prenom;
            MyCmd.Parameters.Add(new NpgsqlParameter("ptel", NpgsqlDbType.Varchar)).Value = tel;
            MyCmd.Parameters.Add(new NpgsqlParameter("padresse", NpgsqlDbType.Varchar)).Value = adresse;

            MyCmd.ExecuteNonQuery();//Exécution
            MyCnx.Close();
        }

        public DataTable SelectAllPerson()
        {
            DataTable MyData = new DataTable();
            NpgsqlDataAdapter da;
            MyCnx = new NpgsqlConnection(Conx);
            MyCnx.Open();
            string select = "SELECT * FROM \"Personne\"";
            MyCmd = new NpgsqlCommand(select, MyCnx);
            da = new NpgsqlDataAdapter(MyCmd);
            da.Fill(MyData);
            MyCnx.Close();
            return MyData;
        }

        public void DeletePersonneById(int idpersonne)
        {
            MyCnx = new NpgsqlConnection(Conx);
            string delete = "DELETE FROM \"Personne\" WHERE(id=:pid)";

            MyCnx.Open();
            MyCmd = new NpgsqlCommand(delete, MyCnx);
            MyCmd.Parameters.Add(new NpgsqlParameter("pid", NpgsqlDbType.Integer)).Value = idpersonne;
            MyCmd.ExecuteNonQuery();
            MyCnx.Close();
        }
    }
}//Fin
