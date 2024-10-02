<%@ Page Title="Editar Salário" Language="C#" MasterPageFile="~/UI/Pages/MasterPage.Master" AutoEventWireup="true" CodeBehind="EditSalary.aspx.cs" Inherits="CRUD.UI.Pages.EditSalary" Async="true" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>Editar Salário</title>
    <link href="https://stackpath.bootstrapcdn.com/bootstrap/4.5.2/css/bootstrap.min.css" rel="stylesheet" />
    <script src="../../Application/Scripts/CoverterCurrency.js"  type="text/javascript"></script>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container mt-5">
        <h2 class="mb-4">Recalcular Salário</h2>

        <asp:HiddenField ID="HiddenFieldPersonId" runat="server" Value="" />
        <div class="form-group">
            <label for="TextBoxNome">Nome</label>
            <asp:TextBox ID="TextBoxNome" runat="server" CssClass="form-control" placeholder="Nome" ReadOnly="true"></asp:TextBox>
        </div>

        <div class="form-group">
            <label for="TextBoxCargo">Cargo</label>
            <asp:TextBox ID="TextBoxCargo" runat="server" CssClass="form-control" placeholder="Cargo" ReadOnly="true"></asp:TextBox>
        </div>

        <div class="form-group">
            <label for="TextBoxSalario">Salário Atual</label>
            <asp:TextBox ID="TextBoxSalario" runat="server" CssClass="form-control" placeholder="Salário atual" ReadOnly="true"></asp:TextBox>
        </div>

        <div class="form-group">
            <label for="TextBoxNovoSalario">Novo Salário</label>
            <asp:TextBox ID="TextBoxNovoSalario" runat="server" CssClass="form-control" placeholder="Insira o novo salário" 
                         onkeyup="formatCurrency(this);" ></asp:TextBox> 
        </div>

        <asp:Button ID="btnUpdate" runat="server" Text="Atualizar Salário" CssClass="btn btn-success" OnClick="btnUpdate_Click" />
        <asp:Button ID="btnReturn" runat="server" Text="Voltar" CssClass="btn btn-danger" OnClick="btnReturn_Click" />
    </div>
</asp:Content>
