<%@ Page Title="Index" Language="C#" MasterPageFile="~/UI/Pages/MasterPage.Master" AutoEventWireup="true" CodeBehind="Index.aspx.cs" Inherits="CRUD.UI.Pages.Index" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>Gerencia</title>
    <link href="https://stackpath.bootstrapcdn.com/bootstrap/4.5.2/css/bootstrap.min.css" rel="stylesheet" />
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container mt-5">
        <h2 class="mb-4">Gerência</h2>

        <!-- Opção para buscar pessoas -->
        <div class="form-group">
            <label for="searchInput">Buscar por Nome</label>
            <asp:TextBox ID="searchInput" runat="server" CssClass="form-control" placeholder="Digite o nome para buscar"></asp:TextBox>
        </div>


        <asp:Button ID="btnSearch" runat="server" Text="Buscar" CssClass="btn btn-primary" OnClick="btnSearch_Click" />
        <asp:Button ID="btnadd" runat="server" Text="Novo Usuario" CssClass="btn btn-success  mt-0 float-right" OnClick="btnadd_Click" />
        

        <!-- Tabela para listar pessoas -->
        <table class="table table-striped mt-4">
            <thead>
                <tr>
                    <th>ID</th>
                    <th>Nome</th>
                    <th>Salario</th>
                    <th>Opções</th>
                </tr>
            </thead>
            <tbody>
                <asp:Repeater ID="peopleRepeater" runat="server">
                    <ItemTemplate>
                        <tr>
                            <td><%# Eval("ID") %></td>
                            <td><%# Eval("Name") %></td>
                            <td>R$<%# Eval("Salary") %></td>
                            
                            <td>
                                <asp:Button ID="btneditar" runat="server" Text="Editar" CssClass="btn btn-warning" OnClick="btnSearch_Click" />
                                <asp:Button ID="btnexcluir" runat="server" Text="Excluir" CssClass="btn btn-danger" OnClick="btnSearch_Click" />
                            </td>
                        </tr>
                    </ItemTemplate>
                </asp:Repeater>
            </tbody>
        </table>
    </div>
</asp:Content>

