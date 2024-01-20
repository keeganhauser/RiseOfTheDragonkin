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
        string readQ = "SELECT * FROM save_data";
        void Start()
        {
            Insert("test_unity_write");
            Debug.Log(Read(readQ));
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
                            sb.AppendLine($"{reader["name"]} | {reader["date"]}");
                        }
                    }
                }
                conn.Close();
            }

            return sb.ToString();
        }

        public int Insert(string name, string position = "")
        {
            int rowsAffected = 0;
            GameObject player = GameObject.FindGameObjectsWithTag("Player").FirstOrDefault();
            Vector3 pos = player.GetComponent<Transform>().position;

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
                    cmd.Parameters.Add("?position", MySqlDbType.VarChar).Value = pos.ToString("F6");

                    rowsAffected = cmd.ExecuteNonQuery();
                }
            }
            return rowsAffected;
        }
    }
}
