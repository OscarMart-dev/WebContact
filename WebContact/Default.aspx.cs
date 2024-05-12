using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Xml.Linq;
using MySql.Data.MySqlClient;
using static System.Net.Mime.MediaTypeNames;


namespace WebContact
{
    public partial class _Default : Page
    {
        private string temp="";
        private string opcion = "";

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

            opcion = "E";
            Response.Cookies["Opcion"].Value = opcion;
            if (string.IsNullOrEmpty(txtboxId.Text))
            {
                //pictureEdit.OnClientClick = "notificar('info')";
                ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('No hay ningún registro para editar');", true);
            }
            else
            {
                temp = txtboxId.Text;
                dropdownNombres.Visible = false;
                pictureEdit.Visible = false;
                pictureDelete.Visible = false;
                agregar.Visible = false;
                TextBoxName.ReadOnly = false;
                txtboxPhone.ReadOnly = false;
                txtboxOfficePhone.ReadOnly = false;
                txtboxPost.ReadOnly = false;
                
                ///hay que pensar como hacer que cuando se de clic se muestre el input file 
                btnGuardar.Visible = true;
                btnCancelar.Visible = true;
                Response.Cookies["Opcion"].Value = opcion;
                Response.Cookies["temp"].Value = opcion;
            }

        }

        public HttpPostedFileBase file { get; set; }

        public Regex regexSoloLetras = new Regex("^[a-zA-Z]+$");

        
        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            opcion = Request.Cookies["Opcion"]?.Value ?? "";
            string id = txtboxId.Text;
            string name = TextBoxName.Text;
            string phone = txtboxPhone.Text;
            string post = txtboxPost.Text;
            string phone_emp = txtboxOfficePhone.Text;
            HttpPostedFile archivoImagen = Request.Files["imgFile"];
            data insert = new data();
            HttpPostedFileBase archivoBase = new HttpPostedFileWrapper(archivoImagen);
            //TimeSpan interval = new TimeSpan(0, 0, 5);

            if (opcion.Equals("A"))
            {

                insert.existeContacto(id);

                if (string.IsNullOrEmpty(TextBoxName.Text))
                {
                    //btnGuardar.OnClientClick = "notificar('alert')";
                    ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('El campo nombre no puede estar vacio');", true);
                }
                else if (string.IsNullOrEmpty(txtboxPhone.Text))
                {
                    //btnGuardar.OnClientClick = "notificar('obligMov')";
                    ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('El campo movil no puede estar vacio');", true);
                }
                else if (string.IsNullOrEmpty(txtboxId.Text))
                {
                    //btnGuardar.OnClientClick = "notificar('obligId')";
                    ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('El campo número de identificación no puede estar vacio');", true);
                }
                else if (id != insert.exist)
                {

                    insert.getInsert(id, name, phone, post, phone_emp);
                    insert.setImageUpdate(id, ConvertirImagenABytes(archivoBase));
                    insert.getSelected(id);
                    LlenarDropDownList();
                    btnCancelar.Visible = false;
                    btnGuardar.Visible = false;
                    dropdownNombres.Visible = true;
                    dropdownNombres.SelectedValue = null;
                    pictureEdit.Visible = true;
                    pictureDelete.Visible = true;
                    agregar.Visible = true;
                    TextBoxName.ReadOnly = true;
                    txtboxPhone.ReadOnly = true;
                    txtboxId.ReadOnly = true;
                    txtboxOfficePhone.ReadOnly = true;
                    txtboxPost.ReadOnly = true;
                    
                    if (insert.base64String != null)
                    {
                        imagen.Src = "data:image/jpeg;base64," + insert.base64String;
                    }
                    else
                    {
                        imagen.Src = "Buttons/atencion.png";///hay que cambiar esta imagen
                    }
                    // btnGuardar.OnClientClick = "confirmation()";
                    //btnGuardar.OnClientClick = "notificar('success')";
                    ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('El Contacto ha sido agregado');", true);
                }
            }
            else
            {
                if (string.IsNullOrEmpty(TextBoxName.Text))
                {
                    //btnGuardar.OnClientClick = "notificar('obligNom')";
                    ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('El campo nombre no puede estar vacio');", true);

                }
                else if (string.IsNullOrEmpty(txtboxPhone.Text))
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('El campo movil no puede estar vacio');", true);
                }
                else
                {
                    insert.setUpdate(id, name, phone, post, phone_emp);
                    
                    if (archivoImagen != null && archivoImagen.ContentLength > 0)
                    {
                        insert.setImageUpdate(id, ConvertirImagenABytes(archivoBase));
                        imagen.Src = "data:image/jpeg;base64," + insert.base64String;
                    }
                    else
                    {
                        imagen.Src = "Buttons/atencion.png";
                    }

                    ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('El Contacto Fue Actualizado');", true);

                    btnCancelar.Visible = false;
                    btnGuardar.Visible = false;
                    dropdownNombres.Visible = true;
                    dropdownNombres.SelectedValue = null;
                    pictureEdit.Visible = true;
                    pictureDelete.Visible = true;
                    agregar.Visible = true;
                    TextBoxName.ReadOnly = true;
                    txtboxPhone.ReadOnly = true;
                    txtboxId.ReadOnly = true;
                    txtboxOfficePhone.ReadOnly = true;
                    txtboxPost.ReadOnly = true;
                    insert.getSelected(id);
                    LlenarDropDownList();
                    if (insert.base64String != null)
                    {
                        imagen.Src = "data:image/jpeg;base64," + insert.base64String;
                    }
                    else
                    {
                        imagen.Src = "Buttons/atencion.png";///hay que cambiar esta imagen
                    }

                }

                //ocultar botones guardar y mostrar el resto


            }

            //Thread.Sleep(interval);
        }


        private byte[] ImageToByteArray(HttpPostedFileBase file)
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
                temp = get.id;
                txtboxPhone.Text = get.phone;
                txtboxOfficePhone.Text = get.office_phone;
                txtboxPost.Text = get.post;
                temp = Request.Cookies["temp"]?.Value ?? "";
                if (get.base64String != null)
                {
                    imagen.Src = "data:image/jpeg;base64," + get.base64String;
                }
                else
                {
                    imagen.Src = "Buttons/atencion.png";///hay que cambiar esta imagen
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
            opcion = "A";
            dropdownNombres.Visible = false;
            pictureEdit.Visible = false;
            pictureDelete.Visible = false;
            agregar.Visible = false;
            TextBoxName.ReadOnly = false;
            txtboxPhone.ReadOnly = false;
            txtboxId.ReadOnly = false;
            txtboxOfficePhone.ReadOnly = false;
            txtboxPost.ReadOnly = false;
            imagen.Src = "Buttons/atencion.png";///imagen por defecto
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
            Response.Cookies["Opcion"].Value = opcion;
            Console.WriteLine("El último valor de la variable es: " + opcion);
        }


        protected void btnCancelar_Click(object sender, EventArgs e)
        {

            opcion = Request.Cookies["Opcion"]?.Value ?? "";
            temp= Request.Cookies["temp"]?.Value ?? "";
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
            //Console.WriteLine("El último valor de la variable es: " + opcion);
            if (opcion == "A")
            {

                if (string.IsNullOrEmpty(temp))
                {

                    imagen.Src = "Buttons/atencion.png";///imagen por defecto

                }
                else
                {

                    data retorna = new data();
                    retorna.getSelected(temp);
                    if (retorna.base64String != null)
                    {
                        imagen.Src = "data:image/jpeg;base64," + retorna.base64String;
                    }
                    else
                    {
                        imagen.Src = "Buttons/atencion.png";///hay que cambiar esta imagen
                    }
                }

            }
            else
            {//se debe tener en cuenta que al cancelar si no se retorna los valores anteriores va a quedar en blanco , se revierte los cambios//
                string id = txtboxId.Text;
                data retorna = new data();
                retorna.getSelected(id);
                TextBoxName.Text = retorna.name;
                txtboxId.Text = retorna.id;
                txtboxPhone.Text = retorna.phone;
                txtboxPost.Text = retorna.post;
                txtboxOfficePhone.Text = retorna.office_phone;
                if (retorna.base64String != null)
                {
                    imagen.Src = "data:image/jpeg;base64," + retorna.base64String;
                }
                else
                {
                    imagen.Src = "Buttons/atencion.png";///hay que cambiar esta imagen
                }

            }


        }



        protected void pictureDelete_Click(object sender, ImageClickEventArgs e)
        {
            string id = txtboxId.Text;
            data delete = new data();
            if (string.IsNullOrEmpty(id))
            {
                //pictureDelete.OnClientClick = "return notificar('delexist');";
                ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('No hay ningún registro para eliminar.');", true);
            }
            else
            {
                
                delete.getDelete(id);
                //pictureDelete.OnClientClick = "notificar('delete')";
                ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('El registro se ha eliminado exitosamente.');", true);
                dropdownNombres.SelectedValue = null;
                LlenarDropDownList();
                TextBoxName.Text = null;
                txtboxPhone.Text = null;
                txtboxId.Text = null;
                txtboxOfficePhone.Text = null;
                txtboxPost.Text = null;
                imagen.Src = "Buttons/atencion.png";

            }
        }

        

        
    }
}
