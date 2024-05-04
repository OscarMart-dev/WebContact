using System;
using System.Collections.Generic;
using System.Configuration;
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
                InicializarDropDownNombres();


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



        protected void btnGenerar_Click(object sender, EventArgs e)
        {
            string id = "1024503182";
            data get = new data();
            get.getSelected(id);
            txtboxId.Text = get.id;
            TextBoxName.Text = get.name;
            txtboxPhone.Text = get.phone;
            txtboxOfficePhone.Text = get.office_phone;
            txtboxPost.Text = get.post;
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

        
        

        protected void dropdownNombres_SelectedIndexChanged1(object sender, EventArgs e)
        {
            string nombreSeleccionado = dropdownNombres.SelectedValue;

            // Llamar a un método para obtener la información del registro seleccionado desde la base de datos
            data get = new data();
            get.getSelected(nombreSeleccionado);

            // Mostrar la información del registro en el formulario
            if (nombreSeleccionado != null)
            {
                // Asignar los valores del registro a los controles del formulario
                TextBoxName.Text = get.name;
                txtboxId.Text = get.id;
                txtboxPhone.Text = get.phone;
                txtboxOfficePhone.Text = get.office_phone;
                txtboxPost.Text = get.post;
                // Otros campos del formulario según los datos que quieras mostrar
            }
        }
    }
}
