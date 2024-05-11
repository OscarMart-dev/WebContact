<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="WebContact._Default" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <link href="Content/estilos.css" rel="stylesheet" />
    <script src="Notification/index.var.js"></script>
    <link href="Notification/style.css" rel="stylesheet" />
    <script src="Notification/notifica.js"></script>
    <script src="https://unpkg.com/sweetalert/dist/sweetalert.min.js"></script>
   
    <body class="body">
        <main>

            <center>
                <form runat="server" method="post" enctype="multipart/form-data">
                    <div class="card" style="width: 20rem;" background-color: rgba(255, 255, 255, 0.5); >
                        <div class="card-body" id="cardForm">
                            <asp:ScriptManager runat="server"></asp:ScriptManager>
                            <asp:DropDownList ID="dropdownNombres" runat="server" CssClass="form-control" SelectedIndexChanged="dropdownNombres_SelectedIndexChanged" AutoPostBack="True" OnSelectedIndexChanged="dropdownNombres_SelectedIndexChanged1"></asp:DropDownList>
                            <div class="upload">
                                <img class="imagen" id="imagen" runat="server">
                                <br />
                                <input type="file" name="imgFile" id="imgFile" accept="image/*" class="inputfile" onchange="cargarImagen(event)" disabled="true" >
                                <label for="imgFile">
                                    <i class="fa-solid fa-image"></i>
                                    selecciona una imagen
                                </label>
                            </div>
                            <br />
                            <asp:ImageButton runat="server" ViewStateMode="Enabled" ID="pictureDelete" Width="30px" ImageAlign="Right" Height="30px" ImageUrl="~/Buttons/delete.png" class="ImageButton" OnClientClick="return confirmarEliminacion();" OnClick="pictureDelete_Click"></asp:ImageButton>
                            <asp:ImageButton runat="server" ID="pictureEdit" Width="30px" Height="30px" ImageAlign="Right" ImageUrl="~/Buttons/edit.png" ViewStateMode="Enabled" class="ImageButton" OnClick="pictureEdit_Click" OnClientClick="deshabilitarInputFile();"></asp:ImageButton>
                            <br />
                            <asp:Image runat="server"></asp:Image>
                            <br />
                            <fieldset class="groupBox">
                                <legend>Nombre</legend>

                                <asp:TextBox ID="TextBoxName" runat="server" pattern="[a-z & A-Z]*" CssClass="TextBox" ClientIDMode="Static" ReadOnly="True"></asp:TextBox>
                            </fieldset>
                            <fieldset class="groupBox">

                                <legend>Móvil</legend>

                                <asp:TextBox ID="txtboxPhone" runat="server" pattern="[0-9]*" CssClass="TextBox" ReadOnly="True"></asp:TextBox>

                            </fieldset>

                            <fieldset class="groupBox">

                                <legend>N° Identificación</legend>

                                <asp:TextBox ID="txtboxId" runat="server" pattern="[0-9]*" CssClass="TextBox" ReadOnly="True"></asp:TextBox>

                            </fieldset>

                            <fieldset class="groupBox">

                                <legend>Móvil Empresarial</legend>

                                <asp:TextBox ID="txtboxOfficePhone" runat="server" pattern="[0-9]*" CssClass="TextBox" ReadOnly="True"></asp:TextBox>

                            </fieldset>
                            <fieldset class="groupBox">

                                <legend>Cargo</legend>

                                <asp:TextBox ID="txtboxPost" runat="server" pattern="[a-z & A-Z]*" CssClass="TextBox" ReadOnly="True"></asp:TextBox>

                            </fieldset>

                            <asp:ImageButton runat="server" ViewStateMode="Enabled" ID="agregar" Width="50px" ImageAlign="Right" Height="50px" ImageUrl="~/Buttons/agregar.png" class="ImageButton" float="right;" OnClick="agregar_Click"></asp:ImageButton>
                            <asp:Button runat="server" Text="Guardar" Visible="False" ID="btnGuardar" class="guardar" OnClick="btnGuardar_Click"></asp:Button>
                            <div></div>
                            <asp:Button runat="server" Text="Cancelar" Visible="False" ID="btnCancelar" class="cancelar" OnClick="btnCancelar_Click"></asp:Button>

                        </div>

                    </div>
                </form>
            </center>
        </main>
    </body>
    

    <script type="text/javascript">    
        function cargarImagen(event) {
            let reader = new FileReader();

            reader.onload = function () {
                let imgPhoto = document.querySelector('.imagen');
                imgPhoto.src = reader.result;
            }
            reader.readAsDataURL(event.target.files[0]);
        }

        function confirmation() {
            swal({
                title: "Contacto agregado",
                text: "Pulse ok para continuar",
                icon: "success",
                button: "ok",
            })
        }

       
        function confirmarEliminacion() {
            return confirm('¿Estás seguro de realizar esta acción?');
        }

        function existeEliminacion() {
            return confirm('No se selecciono un contacto a eliminar');
        }

        function mostrarInputFile() {
            // Obtiene el elemento inputFile
            var inputFile = document.getElementById("file");

            // Cambia el estilo display a "inline-grid"
            inputFile.style.display = "inline-grid";
        }

        function deshabilitarInputFile() {
            document.getElementById("imgFile").disabled = false;
        }
    
    </script>

</asp:Content>
