<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="ControlInventario.WebForm1" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1, maximum-scale=1, user-scalable=no" />
    <link rel="stylesheet" href="Estilos/estiloLogin.css" />
    <link
        href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.3/dist/css/bootstrap.min.css"
        rel="stylesheet"
        integrity="sha384-QWTKZyjpPEjISv5WaRU9OFeRpok6YctnYmDr5pNlyT2bRjXh0JMhjY6hW+ALEwIH"
        crossorigin="anonymous" />
    <title>Ingresar</title>
</head>
<body>
    <form id="form1" runat="server" class="cuerpo">
        <div class="card" width="1000px">
            <div class="accordion" id="accordionExample">
                <div class="accordion-item">
                    <h1 class="accordion-header text-center">
                        <button class="accordion-button" type="button" data-bs-toggle="collapse" data-bs-target="#collapseOne" aria-expanded="true" aria-controls="collapseOne">
                            <strong>Iniciar Sesión</strong>
                        </button>
                    </h1>
                    <div id="collapseOne" class="accordion-collapse collapse show" data-bs-parent="#accordionExample">
                        <div class="accordion-body">
                            <section class="card-body">
                                <div class="row">
                                    <div class="input col-md-5 col-12">
                                        <asp:Label ID="lblUsuario" runat="server" Text="Usuario:"></asp:Label>
                                    </div>
                                    <div class="input col-md-7 col-12">
                                        <asp:TextBox CssClass="textBox" ID="Usuario" runat="server"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="input col-md-5 col-sm-12">
                                        <asp:Label ID="lblPass" runat="server" Text="Contraseña:"></asp:Label>
                                    </div>
                                    <div class="input col-md-7 col-sm-12">
                                        <asp:TextBox CssClass="textBox" ID="Password" runat="server" TextMode="Password"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="row justify-content-center">
                                    <div class="boton col-md-8 col-sm-12 text-center">
                                        <asp:Button CssClass="botonIngresar" ID="btnIngresar" runat="server" Text="Ingresar" OnClick="btnIngresar_Click" />
                                    </div>
                                </div>

                            </section>
                        </div>
                    </div>
                </div>
                <div class="accordion-item dos">
                    <h2 class="accordion-header text-center">
                        <button class="accordion-button collapsed" type="button" data-bs-toggle="collapse" data-bs-target="#collapseTwo" aria-expanded="false" aria-controls="collapseTwo">
                            <strong>Crear cuenta</strong>
                        </button>
                    </h2>
                    <div id="collapseTwo" class="accordion-collapse collapse" data-bs-parent="#accordionExample">
                        <div class="accordion-body">
                            <section class="card-body">
                                <div class="row">
                                    <div class="input col-12">
                                        <asp:Label ID="Label1" runat="server" Text="Nombre:"></asp:Label>
                                    </div>
                                    <div class="input col-12">
                                        <asp:TextBox CssClass="textBox" ID="CrearNombre" runat="server"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="input col-sm-12">
                                        <asp:Label ID="Label2" runat="server" Text="Apellido:"></asp:Label>
                                    </div>
                                    <div class="input col-sm-12">
                                        <asp:TextBox CssClass="textBox" ID="CrearApellido" runat="server"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="input col-sm-12">
                                        <asp:Label ID="Label5" runat="server" Text="Nombre de Usuario:"></asp:Label>
                                    </div>
                                    <div class="input col-sm-12">
                                        <asp:TextBox CssClass="textBox" ID="NomUser" runat="server"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="input col-12">
                                        <asp:Label ID="Label3" runat="server" Text="Contraseña:"></asp:Label>
                                    </div>
                                    <div class="input col-12">
                                        <asp:TextBox CssClass="textBox" ID="CrearPass" runat="server" TextMode="Password"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="input col-sm-12">
                                        <asp:Label ID="Label4" runat="server" Text="Confirmar contraseña:"></asp:Label>
                                    </div>
                                    <div class="input col-sm-12">
                                        <asp:TextBox CssClass="textBox" ID="PassConfirm" runat="server" TextMode="Password"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="row justify-content-center">
                                    <div class="boton col-md-8 col-sm-12 text-center">
                                        <asp:Button CssClass="botonIngresar" ID="btnCrear" runat="server" Text="Crear Cuenta" OnClick="btnCrear_Click" />
                                    </div>
                                </div>
                            </section>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </form>
    <script
        src="https://cdn.jsdelivr.net/npm/bootstrap@5.3.3/dist/js/bootstrap.bundle.min.js"
        integrity="sha384-YvpcrYf0tY3lHB60NNkmXc5s9fDVZLESaAA55NDzOxhy9GkcIdslK1eN7N6jIeHz"
        crossorigin="anonymous">
    </script>
</body>
</html>
