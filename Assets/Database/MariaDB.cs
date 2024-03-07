//using MySql.Data.MySqlClient;
using MySqlConnector;
using System;
using System.Data;
using System.Text;
using UnityEngine;

namespace MariaDBLib
{
    public class MariaDB : MonoBehaviour
    {
        public enum SelectType
        {
            MostRecent,
            All
        }

        public bool IsSaving { get; private set; }

        void Start()
        {
            IsSaving = false;
        }


        private string connectionString = "server=localhost;userid=root;password=abc;database=rotdk";
        public string Read(SelectType type)
        {
            StringBuilder sb = new StringBuilder();
            string query = TypeToString(type);

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
                            sb.AppendLine($"{reader["name"]};{reader["date"]};{reader["position"]};{reader["scene_name"]}");
                        }
                    }
                }
                conn.Close();
            }

            return sb.ToString();
        }

        public int Insert(string name, string position, DateTime date, string scene_name)
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

                    cmd.CommandText = "INSERT INTO save_data(save_id, name, date, position, scene_name) VALUES(?save_id, ?name, ?date, ?position, ?scene_name)";
                    //cmd.CommandText = "INSERT INTO save_data(name, date, position) VALUES(?name, ?date, ?position)";
                    cmd.Parameters.Add("?save_id", MySqlDbType.Guid).Value = Guid.NewGuid();
                    cmd.Parameters.Add("?name", MySqlDbType.VarChar).Value = name;
                    cmd.Parameters.Add("?date", MySqlDbType.Timestamp).Value = DateTime.Now;
                    cmd.Parameters.Add("?position", MySqlDbType.VarChar).Value = position;
                    cmd.Parameters.Add("?scene_name", MySqlDbType.VarChar).Value = scene_name;

                    rowsAffected = cmd.ExecuteNonQuery();
                }
            }
            IsSaving = false;
            return rowsAffected;
        }

        private string TypeToString(SelectType type) => type switch
        {
            SelectType.All => "SELECT * FROM save_data",
            SelectType.MostRecent => "SELECT * FROM save_data ORDER BY date DESC LIMIT 1",
            _ => string.Empty
        };
    }
}
