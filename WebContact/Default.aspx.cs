using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Xml.Linq;
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
                    cmd.Parameters.AddWithValue("reqn_name", "%" + prefixText + "%");
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

       // string opcion = null;

        public HttpPostedFileBase file { get; set; }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            string id = txtboxId.Text;
            string name = TextBoxName.Text;
            string phone = txtboxPhone.Text;
            string post = txtboxPost.Text;
            string phone_emp = txtboxOfficePhone.Text;
            HttpPostedFile archivoImagen = Request.Files["imgFile"];


            // if (opcion=="A") {
            data insert = new data();
            insert.existeContacto(id);

            if (id != insert.exist)
            {
                HttpPostedFileBase archivoBase = new HttpPostedFileWrapper(archivoImagen);
                insert.getInsert(id, name, phone, post, phone_emp);
                insert.setImageUpdate(id, ConvertirImagenABytes(archivoBase));
                insert.getSelected(id);
                LlenarDropDownList();
                btnCancelar.Visible = false;
                btnGuardar.Visible = false;
                dropdownNombres.Visible = true;
                pictureEdit.Visible = true;
                pictureDelete.Visible = true;
                agregar.Visible = true;
                TextBoxName.ReadOnly = true;
                txtboxPhone.ReadOnly = true;
                txtboxId.ReadOnly = true;
                txtboxOfficePhone.ReadOnly = true;
                txtboxPost.ReadOnly = true;
            }
            else {
                btnGuardar.OnClientClick = "";
            }

            //}
            
               // data get = new data();
                //get.setUpdate(id, name, phone, post, phone_emp);
            
        }


        private byte[] ImageToByteArray (HttpPostedFileBase file)
        {

                try
                {
                if (file == null) return null;
                    
                byte[] archivoBytes;
                    using (var binaryReader = new BinaryReader(file.InputStream))
                    {
                        archivoBytes = binaryReader.ReadBytes(file.ContentLength);
                    return archivoBytes;
                  
                }
                   
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error al convertir la imagen a arreglo de bytes: {ex.Message}");
                    return null;
                }
            
        }


        public byte[] ConvertirImagenABytes(HttpPostedFileBase file)
        {
            byte[] imagenBytes = null;

            if (file != null && file.ContentLength > 0)
            {
                try
                {
                    imagenBytes = ConvertirImagenAPNG(file);

                    Console.WriteLine("Imagen convertida a bytes correctamente.");
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error al convertir la imagen a bytes: " + ex.Message);
                }
            }
            else
            {
                Console.WriteLine("No se ha seleccionado ningún archivo.");
            }

            return imagenBytes;
        }
        public byte[] ConvertirImagenAPNG(HttpPostedFileBase archivo)
        {
            using (var imagenOriginal = System.Drawing.Image.FromStream(archivo.InputStream))
            {
                using (var memoryStream = new MemoryStream())
                {
                    // Guardar la imagen en formato PNG en el MemoryStream
                    imagenOriginal.Save(memoryStream, ImageFormat.Png);
                    // Retornar los bytes de la imagen
                    return memoryStream.ToArray();
                }
            }
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
                if (get.base64String != null)
                {
                    imagen.Src = "data:image/jpeg;base64," + get.base64String;
                }
                else {
                    imagen.Src = "Buttons/edit.png";///hay que cambiar esta imagen
                }
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

        protected void agregar_Click(object sender, ImageClickEventArgs e)
        {
            dropdownNombres.Visible = false;
            pictureEdit.Visible = false;
            pictureDelete.Visible=false;
            agregar.Visible = false;
            TextBoxName.ReadOnly = false;
            txtboxPhone.ReadOnly = false;
            txtboxId.ReadOnly = false;  
            txtboxOfficePhone.ReadOnly = false; 
            txtboxPost.ReadOnly = false;
            ///hay que pensar como hacer que cuando se de clic se muestre el input file 
            btnGuardar.Visible = true;
            btnCancelar.Visible = true;
            ///se dejan los valores vacios en dado caso  que vengan llenos
            TextBoxName.Text = null;
            txtboxPhone.Text = null;
            txtboxId.Text = null;
            txtboxOfficePhone.Text = null;
            txtboxPost.Text = null;
            //imagen.Src = "Buttons/atencion.png";
            //opcion = "A";//por ultimo se define la opcion A para agregar para reutilar el botón
        }

        protected void pictureDelete_Click(object sender, ImageClickEventArgs e)
        {

        }
    }
}
