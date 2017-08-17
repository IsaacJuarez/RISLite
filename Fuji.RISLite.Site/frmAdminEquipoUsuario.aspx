<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="frmAdminEquipoUsuario.aspx.cs" Inherits="Fuji.RISLite.Site.frmAdminEquipoUsuario" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="page-content">
        <div class="page-header">
		    <h1>
			    Administración
			    <small>
				    <i class="ace-icon fa fa-angle-double-right"></i>
				    Técnicos y Equipos
			    </small>
		    </h1>
	    </div><!-- /.page-header -->
        <div class="row">
            <div class="col-lg-6 col-md-6 col-sm-12">
                <div class="widget-box">
                    <div class="widget-header widget-header-flat widget-header-small">
						<h5 class="widget-title">
							<i class="ace-icon fa fa-user-md"></i>
							Técnicos
						</h5>
                    </div>
                    <div class="widget-body">
                        <div class="widget-main">
                        </div>
                    </div>
                </div>
            </div>
            <div class="col-lg-6 col-md-6 col-sm-12">
                <div class="widget-box">
                    <div class="widget-header widget-header-flat widget-header-small">
						<h5 class="widget-title">
							<i class="ace-icon fa fa-medkit"></i>
							Equipos
						</h5>
                    </div>
                    <div class="widget-body">
                        <div class="widget-main">
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
