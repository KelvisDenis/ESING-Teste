<%@ Page Title="Create Item" Language="C#" MasterPageFile="~/UI/Pages/MasterPage.Master" AutoEventWireup="true" CodeBehind="Create.aspx.cs" Inherits="CRUD.UI.Pages.Create" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>Create Item</title>
    <!-- Adicionando Bootstrap para melhor visual -->
    <link href="https://stackpath.bootstrapcdn.com/bootstrap/4.5.2/css/bootstrap.min.css" rel="stylesheet" />
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container mt-5">
        <h2 class="mb-4">Criar Nova Pessoa</h2>

        <!-- Formulário com classes Bootstrap -->
        <div class="form-group">
            <label for="TextBox1">Name</label>
            <asp:TextBox ID="TextBox1" runat="server" CssClass="form-control" placeholder="Enter name"></asp:TextBox>
        </div>

        <div class="form-group">
            <label for="TextBox2">Cidade</label>
            <asp:TextBox ID="TextBox2" runat="server" CssClass="form-control" placeholder="Enter cidade"></asp:TextBox>
        </div>

        <div class="form-group">
            <label for="TextBox3">Email</label>
            <asp:TextBox ID="TextBox3" runat="server" CssClass="form-control" placeholder="Enter email"></asp:TextBox>
        </div>

        <div class="form-group">
            <label for="TextBox4">Endereço</label>
            <asp:TextBox ID="TextBox4" runat="server" CssClass="form-control" placeholder="Enter endereço"></asp:TextBox>
        </div>

        <div class="form-group">
            <label for="TextBox5">CEP</label>
            <asp:TextBox ID="TextBox5" runat="server" CssClass="form-control" placeholder="Enter CEP"></asp:TextBox>
        </div>

        <div class="form-group">
            <label for="TextBox6">Usuario</label>
            <asp:TextBox ID="TextBox6" runat="server" CssClass="form-control" placeholder="Enter usuario"></asp:TextBox>
        </div>

        <div class="form-group">
            <label for="TextBox7">Telefone</label>
            <asp:TextBox ID="TextBox7" runat="server" CssClass="form-control" placeholder="Enter telefone"></asp:TextBox>
        </div>

        <div class="form-group">
            <label for="TextBox8">Data de Nascimento</label>
            <asp:TextBox ID="TextBox8" runat="server" CssClass="form-control" placeholder="Enter data de nascimento" TextMode="Date"></asp:TextBox>
        </div>

        <div class="form-group">
            <label for="DropDownListCargo">Cargo</label>
            <asp:DropDownList ID="DropDownListCargo" runat="server" CssClass="form-control">
                <asp:ListItem Value="1">Estagiario</asp:ListItem>
                <asp:ListItem Value="2">Gerente</asp:ListItem>
                <asp:ListItem Value="3">Analista</asp:ListItem>
                <asp:ListItem Value="4">Técnico</asp:ListItem>
                <asp:ListItem Value="5">Coordenador</asp:ListItem>
            </asp:DropDownList>
        </div>

        <!-- Botão Criar -->
        <asp:Button ID="btnCreate" runat="server" Text="Create" CssClass="btn btn-primary mt-3" OnClick="btnCreate_Click" />
        <asp:Button ID="btnreturn" runat="server" Text="Voltar" CssClass="btn btn-secondary mt-3" OnClick="btnReturn_Click" /> 

    </div>
</asp:Content>
