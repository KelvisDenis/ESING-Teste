<%@ Page Title="Error" Language="C#" MasterPageFile="~/UI/Pages/MasterPage.Master" AutoEventWireup="true" CodeBehind="ErrorPage.aspx.cs" Inherits="CRUD.UI.Pages.ErrorPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>Error Page</title>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="error-container">
        <h1>Ops! Algo deu errado.</h1>
        <p>Lamentamos, mas ocorreu um erro inesperado.</p>
        <p>Tente novamente mais tarde ou entre em contato com o suporte se o problema persistir.</p>

        <asp:Panel ID="ErrorDetailsPanel" runat="server" Visible="false">
            <h3>Error Details:</h3>
            <p><strong>Message:</strong> <asp:Literal ID="ErrorMessage" runat="server" /></p>
            <p><strong>Stack Trace:</strong></p>
            <pre><asp:Literal ID="ErrorStackTrace" runat="server" /></pre>
        </asp:Panel>

        <asp:Button ID="btnPrevious" runat="server" Text="Pagaina inicial" CssClass="btn btn-primary" OnClick="btHome_Click" />

    </div>
</asp:Content>

