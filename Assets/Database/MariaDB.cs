//using MySql.Data.MySqlClient;
using MySqlConnector;
using System;
using System.Data;
using System.Text;
using UnityEngine;

namespace MariaDBLib
{
    public class MariaDB
    {
        public enum SelectType
        {
            MostRecent,
            All
        }

        public bool IsSaving { get; private set; }

        public MariaDB()
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
                            sb.AppendLine($"{reader["name"]};{reader["date"]};{reader["position"]};{reader["scenename"]}");
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

                    cmd.CommandText = "INSERT INTO savedata(saveid, name, date, position, scenename) VALUES(?saveid, ?name, ?date, ?position, ?scenename)";
                    //cmd.CommandText = "INSERT INTO save_data(name, date, position) VALUES(?name, ?date, ?position)";
                    cmd.Parameters.Add("?saveid", MySqlDbType.Guid).Value = Guid.NewGuid();
                    cmd.Parameters.Add("?name", MySqlDbType.VarChar).Value = name;
                    cmd.Parameters.Add("?date", MySqlDbType.Timestamp).Value = DateTime.Now;
                    cmd.Parameters.Add("?position", MySqlDbType.VarChar).Value = position;
                    cmd.Parameters.Add("?scenename", MySqlDbType.VarChar).Value = scene_name;

                    rowsAffected = cmd.ExecuteNonQuery();
                }
            }
            IsSaving = false;
            return rowsAffected;
        }

        private string TypeToString(SelectType type) => type switch
        {
            SelectType.All => "SELECT * FROM savedata",
            SelectType.MostRecent => "SELECT * FROM savedata ORDER BY date DESC LIMIT 1",
            _ => string.Empty
        };
    }
}
