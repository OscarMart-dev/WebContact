﻿<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="WebContact._Default" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <link href="Content/estilos.css" rel="stylesheet" />
    

    <main>
        <center>
         <div class="card" style="width: 20rem;">
             <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/6.5.2/css/all.min.css" />
    
                <div class="card-body" id="cardForm">
                    <asp:ScriptManager runat="server"></asp:ScriptManager>
                    <br />
                    
                   <asp:DropDownList ID="dropdownNombres" runat="server" CssClass="form-control" SelectedIndexChanged="dropdownNombres_SelectedIndexChanged" AutoPostBack="True" OnSelectedIndexChanged="dropdownNombres_SelectedIndexChanged1"></asp:DropDownList>
                    
                        <div class="upload">
                             <center><img class="imagen" id="imagen" runat="server">
                            <br />
                                 <br />
                    <input type="file" name="file" id="file" accept="image/*" class="inputfile" onchange="cargarImagen(event)">
                    <label for="file">
                        <i class ="fa-solid fa-image"></i>
                        selecciona una imagen
                    </label>
                    </center>
                            </div>

                        <script>    
                            function cargarImagen(event) {
                                let reader = new FileReader();

                                reader.onload = function() {
                                    let imgPhoto = document.querySelector('.imagen');
                                    imgPhoto.src = reader.result;
                                }
                                reader.readAsDataURL(event.target.files[0]);
                            }
                        </script>
                        
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
