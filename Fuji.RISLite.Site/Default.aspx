<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="Fuji.RISLite.Site.Default" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="page-content">
        <div class="page-header">
		    <h1>
			    Dashboard
			    <small>
				    <i class="ace-icon fa fa-angle-double-right"></i>
				    Estudios
			    </small>
		    </h1>
	    </div><!-- /.page-header -->
        <div class="row">
			<div class="col-xs-12">
                <h1><asp:Label ID="lblUser" runat="server" Text=""></asp:Label></h1>
            </div><!-- /.col -->
		</div><!-- /.row -->
	</div><!-- /.page-content -->
    <!-- page specific plugin scripts -->
</asp:Content>
