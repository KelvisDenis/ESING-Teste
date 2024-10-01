<%@ Page Title="Index" Language="C#" MasterPageFile="~/UI/Pages/MasterPage.Master" AutoEventWireup="true" CodeBehind="Index.aspx.cs" Inherits="CRUD.UI.Pages.Index" Async="true" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>Gerência</title>
    <link href="https://stackpath.bootstrapcdn.com/bootstrap/4.5.2/css/bootstrap.min.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container mt-5">
        <h2 class="mb-4">Gerência</h2>

        <div class="form-group">
            <label for="searchInput">Buscar por Nome</label>
            <asp:TextBox ID="searchInput" runat="server" CssClass="form-control" placeholder="Digite o nome"></asp:TextBox>
        </div>

        <asp:Button ID="btnSearch" runat="server" Text="Buscar" CssClass="btn btn-primary" OnClick="btnSearch_Click" />
        <asp:Button ID="btnadd" runat="server" Text="Novo Usuário" CssClass="btn btn-success mt-0 float-right" OnClick="btnadd_Click" />

        <table class="table table-striped mt-4">
            <thead>
                <tr>
                    <th>ID</th>
                    <th>Nome</th>
                    <th>Salário</th>
                    <th>Recalcular Salario</th>
                    <th>Opções</th>
                </tr>
            </thead>
            <tbody>
                <asp:Repeater ID="peopleRepeater" runat="server">
                      <ItemTemplate>
                            <tr>
                                <td><%# Eval("ID") != null ? Eval("ID") : "" %></td>
                                <td><%# Eval("Name") != null ? Eval("Name") : "" %></td>
                                <td><%# Eval("Salary") != null ? "R$" + Eval("Salary") : "" %></td>
                                <td>
                                    <asp:HyperLink 
                                        ID="LinkRecalcular" 
                                        runat="server" 
                                        Text="Recalcular Salario" 
                                        CssClass="btn btn-secondary" 
                                        NavigateUrl='<%# Eval("ID") != null ? "EditSalary.aspx?id=" + Eval("ID") : "" %>' 
                                        Visible='<%# Eval("ID") != null %>' />
                                </td>

                                <td>
                                    <asp:HyperLink 
                                        ID="lnkEditar" 
                                        runat="server" 
                                        Text="Editar" 
                                        CssClass="btn btn-warning" 
                                        NavigateUrl='<%# Eval("ID") != null ? "Edit.aspx?id=" + Eval("ID") : "" %>' 
                                        Visible='<%# Eval("ID") != null %>'>
                                    </asp:HyperLink>

                                    <asp:Button 
                                        ID="btnExcluir" 
                                        runat="server" 
                                        Text="Excluir" 
                                        CssClass="btn btn-danger" 
                                        CommandArgument='<%# Eval("ID") %>' 
                                        OnClick="btnExcluir_Click" 
                                        Visible='<%# Eval("ID") != null %>' />
                                </td>
                            </tr>
                        </ItemTemplate>
                </asp:Repeater>
                <tr runat="server" id="noDataRow" visible="false">
                    <td colspan="4" class="text-danger text-center">Não encontrado</td>
                </tr>
            </tbody>
        </table>

        <div class="pagination-container">
            <asp:Button ID="btnPrevious" runat="server" Text="Anterior" CssClass="btn btn-primary" OnClick="btnPrevious_Click" />
            <asp:Label ID="lblPageNumber" runat="server" Text="Página 1" CssClass="mx-2" />
            <asp:Button ID="btnNext" runat="server" Text="Próxima" CssClass="btn btn-primary" OnClick="btnNext_Click" />
        </div>
    </div>
</asp:Content>
