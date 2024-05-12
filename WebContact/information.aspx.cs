using MySql.Data.MySqlClient; //se incorpora
using System;
using System.Collections.Generic;
using System.Configuration;//se incorpora
using System.Data;//se incorpora
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;


namespace WebContact
{
    public partial class information : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string dbConnnectionString = ConfigurationManager.ConnectionStrings["DataBaseConnectionString"].ConnectionString;
            var queryString = "select reqn_name Nombre,reqc_cell_phone Movil,reqc_id Documento,reqn_post Cargo,reqc_office_phone Movil_Empresarial from req_phonebook";
            var dbConnection = new MySqlConnection(dbConnnectionString);
            var dataAdapter = new MySqlDataAdapter(queryString, dbConnection);
            var CommanBuilder = new MySqlCommandBuilder(dataAdapter);
            var ds = new DataSet();
            dataAdapter.Fill(ds);
            GridView.DataSource = ds.Tables[0];
            GridView.DataBind();
        }
    }
}