<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="frmArribo.aspx.cs" Inherits="Fuji.RISLite.Site.frmArribo" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <link rel="icon" href="Images/faviconF.ico" type="image/ico" />
    <title>Axon Ris</title>
    <link rel="stylesheet" href="assets/css/style.css" />
    <link rel="stylesheet" href="assets/css/bootstrap.min.css" />
    <!-- ace styles -->
    <link rel="stylesheet" href="assets/css/ace.min.css" class="ace-main-stylesheet" id="main_ace_style" />
    <script src="assets/js/jquery-1.11.3.min.js"></script>
    <!-- bootstrap & fontawesome -->
    <link rel="stylesheet" href="assets/css/bootstrap.min.css" />
    <link rel="stylesheet" href="assets/font-awesome/4.5.0/css/font-awesome.min.css" />
    <style type="text/css">
        .imagen {
            -webkit-background-size: cover;
            -moz-background-size: cover;
            -o-background-size: cover;
            background-size: cover;
            height: 80%;
            width: 80%;
            text-align: center;
        }

        .imagenFuji {
            -webkit-background-size: cover;
            -moz-background-size: cover;
            -o-background-size: cover;
            background-size: cover;
            height: 7%;
            width: 48%;
            text-align: center;
        }

        .btn-info {
            color: #fff;
            background-color: #5bc0de;
            border-color: #46b8da;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <asp:ScriptManager runat="server" ></asp:ScriptManager>
        <div class="row">
            <div class="col-lg-2 col-md-1 col-sm-1 col-xs-1">
            </div>
            <div class="col-lg-8 col-md-10 col-sm-10 col-xs-10">
                <div class="page-content">
                    <div class="page-header">
                        <div class="messagealert" id="alert_container"></div>
                        <h1>Paciente en Cita
			            <small>
                            <i class="ace-icon fa fa-angle-double-right"></i>
                            Arribos
                        </small>
                        </h1>
                    </div>
                    <!-- /.page-header -->
                    <div class="row">
                        <asp:UpdatePanel runat="server">
                            <ContentTemplate>
                                <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
                                    <div class="row">
                                        <div class="col-lg-10 col-md-10 col-sm-12 col-xs-12">
                                            <h1><small><asp:Label runat="server" Text="Paciente  " ForeColor="DarkGreen" ></asp:Label></small><asp:Label runat="server" ID="lblNamePacient" Text="Nombre del paciente" ForeColor="DarkGreen" Font-Bold="true"></asp:Label></h1>
                                        </div>
                                        <div class="col-lg-2 col-md-2 col-sm-12 col-xs-12 text-right">
                                            <asp:LinkButton ID="btnEditPaciente" runat="server" OnClick="btnEditPaciente_Click" Text="Detalle" ToolTip="Detalle del Paciente">
                                                <i class="fa fa-pencil-square-o" aria-hidden="true"  title="Detalle del Paciente" style="font-size:25px;"></i>
                                            </asp:LinkButton>
                                        </div>
                                    </div>
                                    <hr />
                                    <div class="row">
                                        <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
                                            <h1><small><asp:Label runat="server" Text="Fecha y Hora:  " ForeColor="DarkGreen" ></asp:Label></small><asp:Label runat="server" ID="Label1" Text="dd/MM/yyyy HH:mm" ForeColor="DarkGreen" Font-Bold="true"></asp:Label></h1>
                                        </div>
                                    </div>
                                    <hr />
                                    <div class="row">
                                        <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
                                            <asp:Label runat="server" ID="Label2" Text="Estudios." ForeColor="DarkGreen" Font-Bold="true"></asp:Label>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
                                            <asp:GridView ID="grvEstudios" runat="server" AllowPaging="true" CssClass="table table-striped table-bordered"
                                                PageSize="10" AutoGenerateColumns="false" OnRowDataBound="grvEstudios_RowDataBound" Font-Size="10px"
                                                OnPageIndexChanging="grvEstudios_PageIndexChanging" DataKeyNames="intRELModPres, intEstudioID, intPrestacionID"
                                                OnRowCommand="grvEstudios_RowCommand"
                                                EmptyDataText="No hay estudios para la cita.">
                                                <Columns>
                                                    <asp:BoundField DataField="vchPrestacion" HeaderText="Estudio" ReadOnly="true" ItemStyle-CssClass="hidden-md" HeaderStyle-CssClass="hidden-md" />
                                                    <asp:TemplateField HeaderText="Fecha de Inicio" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblFechaInicio" runat="server" Text='<%# String.Format("{0:dd/MM/yyyy}", Eval("fechaInicio")) %>' ForeColor="DarkGreen" />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Hora de Inicio" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblHoraInicio" runat="server" Text='<%# String.Format("{0:hh:mm}", Eval("fechaInicio")) %>' ForeColor="DarkGreen" />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:BoundField DataField="vchPrestacion" HeaderText="Estatus" ReadOnly="true" ItemStyle-CssClass="hidden-md" HeaderStyle-CssClass="hidden-md" />
                                                    <asp:TemplateField HeaderText="Elegir Horario" ItemStyle-HorizontalAlign="Center">
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="btnVisualizar" CausesValidation="false" CommandName="ElegirHorario" CommandArgument='<%#Eval("intRELModPres") %>' runat="server">
                                                                <i class="fa fa-calendar-check-o" aria-hidden="true" title="Buscar horario" style="font-size:25px;"></i>
                                                            </asp:LinkButton>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                </Columns>
                                                <PagerTemplate>
                                                    <asp:Label ID="lblTemplate" runat="server" Text="Muestra Filas: " CssClass="Label" />
                                                    <asp:DropDownList ID="ddlBandeja" runat="server" AutoPostBack="true" CausesValidation="false"
                                                        Enabled="true" OnSelectedIndexChanged="ddlBandeja_SelectedIndexChanged">
                                                        <asp:ListItem Value="10" />
                                                        <asp:ListItem Value="15" />
                                                        <asp:ListItem Value="20" />
                                                    </asp:DropDownList>
                                                    &nbsp;Página
                                                        <asp:TextBox ID="txtBandeja" runat="server" AutoPostBack="true" OnTextChanged="txtBandeja_TextChanged"
                                                            Width="40" MaxLength="10" />
                                                    de
                                                        <asp:Label ID="lblBandejaTotal" runat="server" />
                                                    &nbsp;
                                                        <asp:Button ID="btnBandeja_I" runat="server" CommandName="Page" CausesValidation="false"
                                                            ToolTip="Página Anterior" CommandArgument="Prev" CssClass="previous" />
                                                    <asp:Button ID="btnBandeja_II" runat="server" CommandName="Page" CausesValidation="false"
                                                        ToolTip="Página Siguiente" CommandArgument="Next" CssClass="next" />
                                                </PagerTemplate>
                                                <HeaderStyle CssClass="headerstyle" />
                                                <FooterStyle CssClass="text-center" />
                                                <PagerStyle CssClass="text-center" />
                                            </asp:GridView>
                                        </div>
                                    </div>
                                </div>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>
                    <!-- /.row -->
                </div>
            </div>
            <div class="col-lg-2 col-md-1 col-sm-1 col-xs-1">
            </div>
        </div>
        <!-- /.page-content -->
    </form>
</body>
</html>
