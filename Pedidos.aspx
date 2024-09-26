<%@ Page Title="" Language="C#" MasterPageFile="~/AdminClientes.Master" AutoEventWireup="true" CodeBehind="Pedidos.aspx.cs" Inherits="ControlInventario.Pedidos" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <h1 class="text-center">Administración de Pedidos</h1>
    <section class="card p-4 mb-4">
        <div class="row">
            <div class="col-md-6 col-sm-12">
                <asp:Label ID="Label2" runat="server" CssClass="form-label" Text="Codigo del pedido:"></asp:Label>
                <asp:TextBox ID="TbCodigo" CssClass="form-control" runat="server"></asp:TextBox>
            </div>
            <div class="col-md-6 col-sm-12">
                <asp:Label ID="Label3" runat="server" CssClass="form-label" Text="Fecha:"></asp:Label>
                <asp:TextBox ID="TbFecha" CssClass="form-control" TextMode="Date" runat="server"></asp:TextBox>
            </div>
        </div>
        <div class="row">
            <div class="col-md-3 col-sm-12">
                <asp:Label ID="Label4" runat="server" CssClass="form-label" Text="Codigo del cliente:"></asp:Label>
                <asp:DropDownList CssClass="btn btn-light dropdown-toggle" ID="DdCliente" runat="server" AutoPostBack="True" OnSelectedIndexChanged="DdCliente_SelectedIndexChanged" Width="100%"></asp:DropDownList>
            </div>
            <div class="col-md-3 col-sm-12">
                <asp:Label ID="Label5" runat="server" CssClass="form-label" Text="Nombre del cliente:"></asp:Label>
                <asp:TextBox ID="TbNomCliente" CssClass="form-control" runat="server"></asp:TextBox>
            </div>
            <div class="col-md-3 col-sm-12">
                <asp:Label ID="Label6" runat="server" CssClass="form-label" Text="Apellido del cliente:"></asp:Label>
                <asp:TextBox ID="TbApCliente" CssClass="form-control" runat="server"></asp:TextBox>
            </div>
            <div class="col-md-3 col-sm-12">
                <asp:Label ID="Label13" runat="server" CssClass="form-label" Text="Correo del cliente:"></asp:Label>
                <asp:TextBox ID="TbCorreo" CssClass="form-control" runat="server"></asp:TextBox>
            </div>
        </div>
        <div class="row">
            <div class="col-md-4 col-sm-12">
                <asp:Label ID="Label7" runat="server" CssClass="form-label" Text="Codigo del producto:"></asp:Label>
                <asp:DropDownList CssClass="btn btn-light dropdown-toggle" ID="DdProducto" runat="server" AutoPostBack="True" OnSelectedIndexChanged="DdProducto_SelectedIndexChanged" Width="100%"></asp:DropDownList>
            </div>
            <div class="col-md-4 col-sm-12">
                <asp:Label ID="Label8" runat="server" CssClass="form-label" Text="Producto:"></asp:Label>
                <asp:TextBox ID="TbProd" CssClass="form-control" runat="server"></asp:TextBox>
            </div>
            <div class="col-md-4 col-sm-12">
                <asp:Label ID="Label1" runat="server" CssClass="form-label" Text="Propiedades del producto:"></asp:Label>
                <asp:TextBox ID="TbPropProd" CssClass="form-control" runat="server"></asp:TextBox>
            </div>
        </div>
        <div class="row">
            <div class="col-md-12 col-sm-12">
                <asp:Label ID="Label9" runat="server" CssClass="form-label" Text="Detalles del producto:"></asp:Label>
                <asp:TextBox ID="TbDetProd" CssClass="form-control" runat="server"></asp:TextBox>
            </div>
        </div>
        <div class="row">
            <div class="col-md-4 col-sm-12">
                <asp:Label ID="Label12" runat="server" CssClass="form-label" Text="Cantidad:"></asp:Label>
                <asp:DropDownList CssClass="btn btn-light dropdown-toggle" ID="DdCantidad" runat="server" AutoPostBack="True" OnSelectedIndexChanged="DdCantidad_SelectedIndexChanged" Width="100%"></asp:DropDownList>
            </div>
            <div class="col-md-4 col-sm-12">
                <asp:Label ID="Label10" runat="server" CssClass="form-label" Text="Total:"></asp:Label><br />
                <asp:TextBox ID="TbTotal" CssClass="form-control" runat="server"></asp:TextBox>

            </div>
            <div class="col-md-4 col-sm-12">
                <asp:Label ID="Label11" runat="server" CssClass="form-label" Text="Estado:"></asp:Label><br />
                <asp:DropDownList CssClass="btn btn-light dropdown-toggle" ID="DdEstado" runat="server" AutoPostBack="True" OnSelectedIndexChanged="DdEstado_SelectedIndexChanged" Width="100%"></asp:DropDownList>
            </div>
        </div>
    </section>

    <section class="card mb-4 p-2">
        <div class="row justify-content-center">
            <div class="col-md-3 col-sm-12 text-center p-2">
                <asp:Button CssClass="btn btn-success" ID="Guardar" runat="server" Text="Guardar" Width="150px" OnClick="Guardar_Click" />
            </div>
            <div class="col-md-3 col-sm-12 text-center p-2">
                <asp:Button CssClass="btn btn-primary" ID="Editar" runat="server" Text="Editar" Width="150px" OnClick="Editar_Click" />
            </div>
            <div class="col-md-3 col-sm-12 text-center p-2">
                <asp:Button CssClass="btn btn-danger" ID="Eliminar" runat="server" Text="Eliminar" Width="150px" OnClick="Eliminar_Click" />
            </div>
        </div>
    </section>

    <section class="card mb-4 p-2">
        <div class="row justify-content-evenly">
            <div class="col-md-4 col-sm-6 p-2">
                <asp:TextBox CssClass="form-control" ID="TextBoxBuscar" runat="server" Width="100%" placeholder="Ingrese un codigo..."></asp:TextBox>
            </div>
            <div class="col-md-2 col-sm-12 p-2 text-center">
                <asp:Button CssClass="btn btn-dark" ID="Buscar" runat="server" Text="Buscar" Width="100px" OnClick="Buscar_Click"></asp:Button>
            </div>
        </div>
    </section>

    <section class="card mb-4 p-3 tabla">
        <asp:GridView ID="GridView1" CssClass="table table-striped-columns" runat="server" CellPadding="4" ForeColor="#333333" GridLines="None">
            <AlternatingRowStyle BackColor="White" />
            <EditRowStyle BackColor="#7C6F57" />
            <FooterStyle BackColor="#1C5E55" ForeColor="White" Font-Bold="True" />
            <HeaderStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White" />
            <PagerStyle ForeColor="White" HorizontalAlign="Center" BackColor="#666666" />
            <RowStyle BackColor="#E3EAEB" />
            <SelectedRowStyle BackColor="#C5BBAF" Font-Bold="True" ForeColor="#333333" />
            <SortedAscendingCellStyle BackColor="#F8FAFA" />
            <SortedAscendingHeaderStyle BackColor="#246B61" />
            <SortedDescendingCellStyle BackColor="#D4DFE1" />
            <SortedDescendingHeaderStyle BackColor="#15524A" />

        </asp:GridView>
    </section>
</asp:Content>
