using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MySql.Data.MySqlClient;


namespace WebContact
{
    public partial class _Default : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LlenarDropDownList();

            }
        }

        private void InicializarDropDownNombres()
        {
            // Lógica para inicializar el DropDownList y cargar los nombres desde la base de datos
            List<string> contactos = GetCompletionList("");
            contactos.Insert(0, " ");
            dropdownNombres.DataSource = contactos;
            dropdownNombres.DataBind();

        }



        public static List<string> GetCompletionList(string prefixText)
        {
            List<string> suggestions = new List<string>();
            string constr = ConfigurationManager.ConnectionStrings["DataBaseConnectionString"].ConnectionString;

            using (MySqlConnection conn = new MySqlConnection(constr))
            {
                conn.Open();
                string sql = "select * from req_phonebook where reqn_name like ?";
                MySqlCommand cmd = conn.CreateCommand();
                cmd.CommandText = sql;
                {
                    cmd.Parameters.AddWithValue("reqn_name", prefixText + "%");
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            suggestions.Add(reader["reqn_name"].ToString());
                        }
                    }
                    conn.Close();
                }
            }
            return suggestions;
        }


        protected void pictureEdit_Click(object sender, ImageClickEventArgs e)
        {

            btnGuardar.Visible = true;
            txtboxId.ReadOnly = true;
            TextBoxName.ReadOnly = false;
            txtboxPhone.ReadOnly = false;
            txtboxOfficePhone.ReadOnly = false;
            txtboxPost.ReadOnly = false;

        }


        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            string id = txtboxId.Text;
            string name = TextBoxName.Text;
            string phone = txtboxPhone.Text;
            string post = txtboxPost.Text;
            string phone_emp = txtboxOfficePhone.Text;

            data get = new data();
            get.setUpdate(id, name, phone, post, phone_emp);

        }

        protected void pictureCreate_Click(object sender, ImageClickEventArgs e)
        {
            dropdownNombres.Visible = false;
            btnGuardar.Visible = true;
            pictureEdit.Visible = false;
            pictureDelete.Visible = false;
        }

       

        private byte[] ImageToByteArray(System.Drawing.Image image)
        {
            try
            {
                if (image == null) return null;
                using (MemoryStream ms = new MemoryStream())
                {
                    image.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
                    return ms.ToArray();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al convertir la imagen a arreglo de bytes: {ex.Message}");
                return null;
            }
        }

        public byte[] image { get; set; }

        protected void dropdownNombres_SelectedIndexChanged1(object sender, EventArgs e)
        {
            string nombreSeleccionado = dropdownNombres.SelectedValue;

            // Llamar a un método para obtener la información del registro en base de datos
            data get = new data();
            get.getSelected(nombreSeleccionado);

            // Se compara si viene vacio no haga nada de lo contrario que muestre la info
            if (nombreSeleccionado != null)
            {
                // Asignar los valores del registro a los controles del formulario se debe poner base 64 para mostrarla
                TextBoxName.Text = get.name;
                txtboxId.Text = get.id;
                txtboxPhone.Text = get.phone;
                txtboxOfficePhone.Text = get.office_phone;
                txtboxPost.Text = get.post;
                imagen.Src = "data:image/jpeg;base64," + get.base64String;
            }
        }

        private void LlenarDropDownList()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["DataBaseConnectionString"].ConnectionString;
            MySqlConnection conn = new MySqlConnection(connectionString);

            string query = "select reqc_id,reqn_name from req_phonebook";
            using (MySqlCommand cmd = new MySqlCommand(query, conn))
            {
                conn.Open();
                MySqlDataReader reader = cmd.ExecuteReader();
                dropdownNombres.DataSource = reader;
                dropdownNombres.DataTextField = "reqn_name"; // El campo que se mostrará en el dropdown
                dropdownNombres.DataValueField = "reqc_id"; // El valor que corresponde a cada texto
                dropdownNombres.DataBind();
            }
            dropdownNombres.Items.Insert(0, new ListItem(" ", "0"));
        }

        
    }
}
