using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;
using MySql.Data.MySqlClient;

namespace WebContact
{
    public class data
    {
        readonly string CONN_PARAMS = "server=localhost;user=root;password=root;database=data;sslmode=none;";

        public string name { get; set; }
        public string phone { get; set; }
        public byte[] image { get; set; }
        public string base64String { get; set; }
        public string id { get; set; }
        public string exist { get; set; }
        public string post { get; set; }
        public string office_phone { get; set; }

        public HttpPostedFileBase file { get; set; }

        public static List<data> list = new List<data>();

        public void search(string key)
        {
            MySqlConnection conn = new MySqlConnection(CONN_PARAMS);
            conn.Open();
            string sql = "select * from req_phonebook where reqn_name like ?";
            MySqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = sql;
            cmd.Parameters.AddWithValue("reqn_name", key + "%");
            MySqlDataReader reader = cmd.ExecuteReader();
            list.Clear();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    data data = new data();

                    data.name = reader["reqn_name"].ToString();
                    data.phone = reader["reqc_cell_phone"].ToString();
                    data.id = reader["reqc_id"].ToString();

                    if (!Convert.IsDBNull(reader["reqn_picture"]))
                    {
                        data.image = (byte[])reader["reqn_picture"];
                    }
                    else
                    {
                        data.image = null;
                    }

                    list.Add(data);
                }

            }
            reader.Dispose();
            cmd.Dispose();
            conn.Close();
        }

        public void getSelected(string id)
        {
            MySqlConnection conn = new MySqlConnection(CONN_PARAMS);
            conn.Open();
            string sql = "SELECT * FROM req_phonebook where reqc_id ='" + id+ "' order by reqn_name asc";
            MySqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = sql;
            MySqlDataReader reader = cmd.ExecuteReader();

            if (reader.Read())
            {
                this.name = reader["reqn_name"].ToString();
                this.phone = reader["reqc_cell_phone"].ToString();
                if (!Convert.IsDBNull(reader["reqn_picture"]))
                {
                    byte[] imagenBytes = (byte[])reader["reqn_picture"];
                    this.base64String = Convert.ToBase64String(imagenBytes);
                }
                else
                {
                    this.base64String = null;
                }
                this.id = reader["reqc_id"].ToString();
                this.post = reader["reqn_post"].ToString();
                this.office_phone = reader["reqc_office_phone"].ToString();
            }
            reader.Dispose();
            cmd.Dispose();
            conn.Close();
        }

        public void setUpdate(string id, string name, string phone, string post, string office_phone)
        {
            MySqlConnection conn = null;
            MySqlCommand cmd = null;
            try
            {
                conn = new MySqlConnection(CONN_PARAMS);
                conn.Open();
                cmd = conn.CreateCommand();
                string sql = "update req_phonebook set reqn_name='" + name + "',reqc_cell_phone=" + phone + ",reqn_post='" + post + "',reqc_office_phone=" + ((office_phone != "") ? office_phone : "null") + " where reqc_id ='" + id + "'";
                cmd.CommandText = sql;
                cmd.ExecuteNonQuery();
            }
            catch
            {
                throw;
            }
            finally
            {
                cmd.Dispose();
                conn.Close();

            }

        }

        public void setImageUpdate(string id, byte[] imagenBytes)
        {
            MySqlConnection conn = null;
            MySqlCommand cmd = null;
            try
            {


                conn = new MySqlConnection(CONN_PARAMS);
                conn.Open();

                cmd = conn.CreateCommand();

                string sql = "update req_phonebook set reqn_picture=@imagen where reqc_id ='" + id + "'";
                cmd.CommandText = sql;
                cmd.Parameters.AddWithValue("@imagen", imagenBytes);
                cmd.ExecuteNonQuery();
            }
            catch
            {
                throw;
            }
            finally
            {
                cmd.Dispose();
                conn.Close();

            }
        }


        public void getInsert(string id, string name, string phone, string post, string office_phone)
        {
            MySqlConnection conn = null;
            MySqlCommand cmd = null;
            try
            {
                conn = new MySqlConnection(CONN_PARAMS);
                conn.Open();
                cmd = conn.CreateCommand();
                string sql = "INSERT INTO req_phonebook (reqc_id, reqn_name, reqc_cell_phone, reqn_post, reqc_office_phone) VALUES('" + id + "', '" + name + "', " + phone + ", '" + post + "', " + ((office_phone != "") ? office_phone : "null") + ")";
                cmd.CommandText = sql;
                cmd.ExecuteNonQuery();

            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                cmd.Dispose();
                conn.Close();
            }
        }


        public void getDelete(string id)
        {

            MySqlConnection conn = new MySqlConnection(CONN_PARAMS);
            conn.Open();
            MySqlCommand cmd = conn.CreateCommand();
            string sql = "DELETE FROM req_phonebook WHERE reqc_id='" + id + "'";
            cmd.CommandText = sql;
            cmd.ExecuteNonQuery();
            //MessageBox.Show("Contacto Eliminado");
            cmd.Dispose();
            conn.Close();
        }

        public void existeContacto(string id)
        {
            MySqlConnection conn = new MySqlConnection(CONN_PARAMS);
            conn.Open();
            string sql = "SELECT reqc_id FROM req_phonebook where reqc_id ='" + id + "' order by reqn_name asc";
            MySqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = sql;
            MySqlDataReader reader = cmd.ExecuteReader();

            if (reader.Read())
            {
                this.exist = reader["reqc_id"].ToString();

            }
            else {
                this.exist = null;
            }
            reader.Dispose();
            cmd.Dispose();
            conn.Close();
        }
    }
}