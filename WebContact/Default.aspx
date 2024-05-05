<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="WebContact._Default" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <link href="Content/estilos.css" rel="stylesheet" />
    

    <main>
        <center>
         <div class="card" style="width: 20rem;">
    
                <div class="card-body" id="cardForm">
                    <asp:ScriptManager runat="server"></asp:ScriptManager>
                    <br />
                    
                   <asp:DropDownList ID="dropdownNombres" runat="server" CssClass="form-control" SelectedIndexChanged="dropdownNombres_SelectedIndexChanged" AutoPostBack="True" OnSelectedIndexChanged="dropdownNombres_SelectedIndexChanged1"></asp:DropDownList>
                    
                    <br />
                    <asp:ImageButton runat="server" ViewStateMode="Enabled" ID="pictureCreate" Width="30px" ImageAlign="Right" Height="30px" ImageUrl="~/Buttons/agregar-usuario.png" OnClick="pictureCreate_Click"></asp:ImageButton>
                    

                    <script>
                        function mostrarSelector() {
                            document.getElementById('fileInput').click();
                        }

                        function cargarImagen(event) {
                            var file = event.target.files[0];
                            var reader = new FileReader();
                            reader.onload = function (e) {
                                document.getElementById('imagen').src = e.target.result;
                            };
                            reader.readAsDataURL(file);
                        }
                    </script>

                    <input type="file" id="fileInput" style="display: none;" onchange="cargarImagen(event)" accept="image/jpeg, image/png, image/jpg"/>

                    <asp:Image ID="imgPhoto" runat="server" Width="100px" Height="100px" ImageAlign="Middle"  BorderStyle="Solid" CssClass="imagen-redondeada" ImageUrl="~/Buttons/usuario.png" onclick="mostrarSelector()" cursor="pointer"/>
                    <br />
                    <asp:ImageButton runat="server" ViewStateMode="Enabled" ID="pictureDelete" Width="30px" ImageAlign="Right" Height="30px" ImageUrl="~/Buttons/eliminar.png"></asp:ImageButton>
                    <asp:ImageButton runat="server" ID="pictureEdit"  Width="30px" Height="30px" ImageAlign="Right" ImageUrl="~/Buttons/editar.png" ViewStateMode="Enabled"></asp:ImageButton>
                    <br />
                    <asp:Image runat="server"></asp:Image>
                    <br />
                    <fieldset class="groupBox">
                        <legend>Nombre</legend>

                        <asp:TextBox ID="TextBoxName" runat="server"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="rfvInput" runat="server" ControlToValidate="TextBoxName" ErrorMessage="Este campo es obligatorio" Text="*" />
                    </fieldset>
                     <fieldset class="groupBox">

                        <legend>Móvil</legend>

                        <asp:TextBox ID="txtboxPhone" runat="server"></asp:TextBox>

                    </fieldset>

                    <fieldset class="groupBox">

                        <legend>N° Identificación</legend>

                        <asp:TextBox ID="txtboxId" runat="server"></asp:TextBox>

                    </fieldset>

                    <fieldset class="groupBox">

                        <legend>Móvil Empresarial</legend>

                        <asp:TextBox ID="txtboxOfficePhone" runat="server"></asp:TextBox>

                    </fieldset>
                    <fieldset class="groupBox">

                        <legend>Cargo</legend>

                        <asp:TextBox ID="txtboxPost" runat="server"></asp:TextBox>

                    </fieldset>
                    <br />
                    <asp:Button runat="server" Text="Guardar" Visible="False" ID="btnGuardar" OnClientClick="mostrarMensaje();" OnClick="btnGuardar_Click" BackColor="#0099FF" BorderStyle="None"></asp:Button>
                    
                </div>

                <div class="card-body"></div></div>
                    </center>

    
    </main>

</asp:Content>
