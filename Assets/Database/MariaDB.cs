//using MySql.Data.MySqlClient;
using System;
using System.Data;
using System.Linq;
using System.Text;
using MySqlConnector;
using UnityEditor;
using UnityEngine;

namespace MariaDBLib
{
    public class MariaDB : MonoBehaviour
    {
        public bool IsSaving { get; private set; }

        void Start()
        {
            IsSaving = false;
        }


        private string connectionString = "server=localhost;userid=root;password=abc;database=rotdk";
        public string Read(string query)
        {
            StringBuilder sb = new StringBuilder();

            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                conn.Open();
                using (MySqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandTimeout = 300;
                    cmd.CommandText = query;

                    

                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            sb.AppendLine($"{reader["name"]}\t{reader["date"]}\t{reader["position"]}");
                        }
                    }
                }
                conn.Close();
            }

            return sb.ToString();
        }

        public int Insert(string name, string position, DateTime date)
        {
            IsSaving = true;
            int rowsAffected = 0;

            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                conn.Open();
                using (MySqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandTimeout = 300;

                    cmd.CommandText = "INSERT INTO save_data(save_id, name, date, position) VALUES(?save_id, ?name, ?date, ?position)";
                    //cmd.CommandText = "INSERT INTO save_data(name, date, position) VALUES(?name, ?date, ?position)";
                    cmd.Parameters.Add("?save_id", MySqlDbType.Guid).Value = Guid.NewGuid();
                    cmd.Parameters.Add("?name", MySqlDbType.VarChar).Value = name;
                    cmd.Parameters.Add("?date", MySqlDbType.Timestamp).Value = DateTime.Now;
                    cmd.Parameters.Add("?position", MySqlDbType.VarChar).Value = position;

                    rowsAffected = cmd.ExecuteNonQuery();
                }
            }
            IsSaving = false;
            return rowsAffected;
        }

        
    }
}
