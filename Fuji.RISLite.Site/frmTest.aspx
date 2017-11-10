<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="frmTest.aspx.cs" Inherits="Fuji.RISLite.Site.frmTest" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:FileUpload ID="FileUpload1" runat="server" />
    <hr />
    <asp:Button ID="btnUpload" Text="Upload" runat="server" OnClick="btnUpload_Click" />
    <br />
    <asp:Label ID="lblMessage" ForeColor="Green" runat="server" />
</asp:Content>
