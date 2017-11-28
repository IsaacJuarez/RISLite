<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="frmRepEstadisticas.aspx.cs" Inherits="Fuji.RISLite.Site.frmRepEstadisticas" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <telerik:RadAjaxManager ID="RadAjaxManager1" runat="server">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="RCB_Sitio">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="RCB_Modalidad" />
                    <telerik:AjaxUpdatedControl ControlID="RCB_Sitio" />
                </UpdatedControls>
            </telerik:AjaxSetting>

            <telerik:AjaxSetting AjaxControlID="RCB_Estatus">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="RCB_Estatus" />
                </UpdatedControls>
            </telerik:AjaxSetting>

            <telerik:AjaxSetting AjaxControlID="RB_Buscar">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="RHC_grafica_modalidad" />
                    <telerik:AjaxUpdatedControl ControlID="RG_modalidad" />
                    <telerik:AjaxUpdatedControl ControlID="RHC_grafica_modalidad" />
                    <%-- <telerik:AjaxUpdatedControl ControlID="IB_export_modalidad" />--%>
                </UpdatedControls>
            </telerik:AjaxSetting>

            <telerik:AjaxSetting AjaxControlID="RCB_Sitio_Personal">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="RCB_Personal" />
                    <telerik:AjaxUpdatedControl ControlID="RCB_Sitio_Personal" />
                </UpdatedControls>
            </telerik:AjaxSetting>


            <telerik:AjaxSetting AjaxControlID="RCB_Estatus_Personal">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="RCB_Estatus_Personal" />
                </UpdatedControls>
            </telerik:AjaxSetting>

            <telerik:AjaxSetting AjaxControlID="RB_Buscar_Personal">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="RCH_Personal" />
                    <telerik:AjaxUpdatedControl ControlID="RG_Usuarios" />
                    <%-- <telerik:AjaxUpdatedControl ControlID="IB_Export_usuarios" />--%>
                </UpdatedControls>
            </telerik:AjaxSetting>

            <%--  <telerik:AjaxSetting AjaxControlID="RB_export">
                <UpdatedControls>                    
                    <telerik:AjaxUpdatedControl ControlID=" RB_export" />                    
                </UpdatedControls>
            </telerik:AjaxSetting>


            <telerik:AjaxSetting AjaxControlID="IB_Export_usuarios">
                <UpdatedControls>                    
                    <telerik:AjaxUpdatedControl ControlID="IB_Export_usuarios" />                    
                </UpdatedControls>
            </telerik:AjaxSetting>--%>
        </AjaxSettings>
    </telerik:RadAjaxManager>


    <div class="page-content">
        <div class="page-header">
            <h1>Reportes y Estadísticas
			   
                <small>
                    <i class="ace-icon fa fa-angle-double-right"></i>

                </small>
            </h1>
        </div>
        <!-- /.page-header -->
        <div class="row">
            <div class="col-lg-12 col-md-12 col-sm-12">
                <div class="tabbable tabs-left" id="Tabs">
                    <ul class="nav nav-tabs" id="myTab3">
                        <li class="active">
                            <a data-toggle="tab" href="#estadisticaModalidad">
                                <i class="purple ace-icon fa fa-bar-chart"></i>
                                Modalidad
                            </a>
                        </li>
                        <li>
                            <a data-toggle="tab" href="#estadisticaPersonal">
                                <i class="blue ace-icon fa fa-area-chart"></i>
                                Personal
                            </a>
                        </li>
                    </ul>

                    <div class="tab-content">



                        <div id="estadisticaModalidad" class="tab-pane in active">
                            <div class="widget-box">
                                <div class="widget-header">
                                    <h4 class="widget-title">Modalidad</h4>
                                    <div class="widget-toolbar">
                                    </div>
                                </div>

                                <div class="widget-body">
                                    <div class="widget-main">
                                        <div class="row">
                                            <div class="col-lg-12 col-md-12">
                                                <div class="row">
                                                    <div class="col-sm-1 center-block">
                                                        Sitio:
                                                    </div>
                                                    <div class="col-sm-4 center">
                                                        <telerik:RadComboBox ID="RCB_Sitio" runat="server" Width="100%"
                                                            OnSelectedIndexChanged="RCB_Sitio_SelectedIndexChanged" AutoPostBack="true"
                                                            DataTextField="vchNombreSitio" DataValueField="intSitioID" RenderMode="Lightweight" ForeColor="DarkGreen">
                                                        </telerik:RadComboBox>
                                                    </div>

                                                    <div class="col-sm-1 center-block">
                                                    </div>

                                                    <div class="col-sm-1 center-block">
                                                        Modalidad:
                                                    </div>

                                                    <div class="col-sm-4 right">
                                                        <telerik:RadComboBox ID="RCB_Modalidad" runat="server"
                                                            AutoPostBack="true" CheckBoxes="true" Width="100%"
                                                            DataTextField="vchModalidad" DataValueField="intModalidadID" RenderMode="Lightweight" ForeColor="DarkGreen">
                                                            <Localization AllItemsCheckedString="Todas seleccionadas" ItemsCheckedString="Seleccionadas" />
                                                        </telerik:RadComboBox>
                                                    </div>
                                                    <div class="col-sm-1 center-block">
                                                    </div>
                                                </div>

                                                <br />

                                                <div class="row">
                                                    <div class="col-sm-1 center-block">
                                                        Desde:
                                                    </div>
                                                    <div class="col-sm-4 center">
                                                        <telerik:RadDatePicker ID="RDP_Desde" runat="server" Width="100%"></telerik:RadDatePicker>
                                                    </div>

                                                    <div class="col-sm-1 center-block">
                                                    </div>

                                                    <div class="col-sm-1 center-block">
                                                        Hasta:
                                                    </div>

                                                    <div class="col-sm-4 right">
                                                        <telerik:RadDatePicker ID="RDP_Hasta" runat="server" Width="100%"></telerik:RadDatePicker>
                                                    </div>
                                                    <div class="col-sm-1 center-block">
                                                    </div>
                                                </div>

                                                <br />

                                                <div class="row">
                                                    <div class="col-sm-1 center-block">
                                                        Estatus:
                                                    </div>
                                                    <div class="col-sm-4 center-block">
                                                        <telerik:RadComboBox ID="RCB_Estatus" runat="server"
                                                            AutoPostBack="true" CheckBoxes="true" Width="100%"
                                                            DataTextField="vchEstatus" DataValueField="intEstatusEstudio" RenderMode="Lightweight" ForeColor="DarkGreen">
                                                            <Localization AllItemsCheckedString="Todos seleccionados" ItemsCheckedString="Seleccionados" />
                                                        </telerik:RadComboBox>
                                                    </div>
                                                    <div class="col-sm-1 center-block">
                                                    </div>
                                                    <div class="col-sm-1 center-block">
                                                    </div>
                                                    <div class="col-sm-4 center-block">
                                                        <telerik:RadButton ID="RB_Buscar" runat="server" Text="Buscar" Width="100%" OnClick="RB_Buscar_Click"></telerik:RadButton>
                                                    </div>
                                                    <div class="col-sm-1 center-block">
                                                    </div>
                                                </div>

                                                <br />
                                                <div class="row">
                                                    <div class="col-sm-1 center-block">
                                                    </div>
                                                    <div class="col-sm-10 right">

                                                        <telerik:RadHtmlChart runat="server" ID="RHC_grafica_modalidad" Width="100%" Visible="false">
                                                            <PlotArea>
                                                                <Series>
                                                                    <telerik:ColumnSeries Name="tabla_Modalidad_Grafica" DataFieldY="Total">
                                                                        <LabelsAppearance Visible="false" />
                                                                    </telerik:ColumnSeries>
                                                                </Series>
                                                                <XAxis DataLabelsField="Modalidad">
                                                                </XAxis>
                                                                <YAxis Step="1">
                                                                    <TitleAppearance Text="Estudios"></TitleAppearance>
                                                                </YAxis>
                                                            </PlotArea>
                                                            <Legend>
                                                                <Appearance Visible="false" />
                                                            </Legend>
                                                            <ChartTitle Text="Estadistica Modalidades">
                                                            </ChartTitle>
                                                        </telerik:RadHtmlChart>
                                                    </div>
                                                    <div class="col-sm-1 center-block">
                                                    </div>
                                                </div>

                                                <br />

                                                <div class="row">
                                                    <div class="col-sm-1 center-block">
                                                    </div>
                                                    <div class="col-sm-10 right">
                                                        <telerik:RadGrid ID="RG_modalidad" runat="server" Visible="false">
                                                            <ExportSettings HideStructureColumns="true">
                                                            </ExportSettings>
                                                        </telerik:RadGrid>
                                                    </div>
                                                    <div class="col-sm-1 center-block">
                                                    </div>
                                                </div>

                                                <br />

                                                <div class="row">
                                                    <div class="col-sm-1 center-block">
                                                    </div>

                                                    <div class="col-sm-7 center-block">
                                                    </div>
                                                    <div class="col-sm-3 right">
                                                        <%-- <telerik:RadButton ID="RB_export_modalidad" runat="server" Text="Exportar Datos" Width="100%" OnClick="RB_export_modalidad_Click" Visible="false">
                                                        </telerik:RadButton>--%>

                                                        <asp:ImageButton ID="IB_export_modalidad" runat="server" ImageUrl="Images/Excel_XLSX.png" Width="50px" Height="50px"
                                                            OnClick="RB_export_modalidad_Click" AlternateText="ExcelML" />
                                                    </div>
                                                    <div class="col-sm-1 center-block">
                                                    </div>
                                                </div>


                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>



                        <div id="estadisticaPersonal" class="tab-pane">
                            <div class="widget-box">
                                <div class="widget-header">
                                    <h4 class="widget-title">Personal</h4>
                                    <div class="widget-toolbar">
                                    </div>
                                </div>
                                <div class="widget-body">
                                    <div class="widget-main">
                                        <div class="row">
                                            <div class="col-lg-12 col-md-12">
                                                <div class="row">
                                                    <div class="col-sm-1 center-block">
                                                        Sitio:
                                                    </div>
                                                    <div class="col-sm-4 center">
                                                        <telerik:RadComboBox ID="RCB_Sitio_Personal" runat="server" Width="100%"
                                                            OnSelectedIndexChanged="RCB_Sitio_Personal_SelectedIndexChanged" AutoPostBack="true"
                                                            DataTextField="vchNombreSitio" DataValueField="intSitioID" RenderMode="Lightweight" ForeColor="DarkGreen">
                                                        </telerik:RadComboBox>
                                                    </div>

                                                    <div class="col-sm-1 center-block">
                                                    </div>

                                                    <div class="col-sm-1 center-block">
                                                        Personal:
                                                    </div>

                                                    <div class="col-sm-4 right">
                                                        <telerik:RadComboBox ID="RCB_Personal" runat="server"
                                                            AutoPostBack="true" CheckBoxes="true" Width="100%"
                                                            DataTextField="vchUsuario" DataValueField="intUsuarioID" RenderMode="Lightweight" ForeColor="DarkGreen">
                                                            <Localization AllItemsCheckedString="Todas seleccionadas" ItemsCheckedString="Seleccionadas" />
                                                        </telerik:RadComboBox>
                                                    </div>
                                                    <div class="col-sm-1 center-block">
                                                    </div>
                                                </div>

                                                <br />

                                                <div class="row">
                                                    <div class="col-sm-1 center-block">
                                                        Desde:
                                                    </div>
                                                    <div class="col-sm-4 center">
                                                        <telerik:RadDatePicker ID="RDP_desde_personal" runat="server" Width="100%"></telerik:RadDatePicker>
                                                    </div>

                                                    <div class="col-sm-1 center-block">
                                                    </div>

                                                    <div class="col-sm-1 center-block">
                                                        Hasta:
                                                    </div>

                                                    <div class="col-sm-4 right">
                                                        <telerik:RadDatePicker ID="RDP_hasta_personal" runat="server" Width="100%"></telerik:RadDatePicker>
                                                    </div>
                                                    <div class="col-sm-1 center-block">
                                                    </div>
                                                </div>

                                                <br />

                                                <div class="row">
                                                    <div class="col-sm-1 center-block">
                                                        Estatus:
                                                    </div>
                                                    <div class="col-sm-4 center-block">
                                                        <telerik:RadComboBox ID="RCB_Estatus_Personal" runat="server"
                                                            AutoPostBack="true" CheckBoxes="true" Width="100%"
                                                            DataTextField="vchEstatus" DataValueField="intEstatusEstudio" RenderMode="Lightweight" ForeColor="DarkGreen">
                                                            <Localization AllItemsCheckedString="Todos seleccionados" ItemsCheckedString="Seleccionados" />
                                                        </telerik:RadComboBox>
                                                    </div>
                                                    <div class="col-sm-1 center-block">
                                                    </div>
                                                    <div class="col-sm-1 center-block">
                                                    </div>
                                                    <div class="col-sm-4 center-block">
                                                        <telerik:RadButton ID="RB_Buscar_Personal" runat="server" Text="Buscar" Width="100%" OnClick="RB_Buscar_Personal_Click"></telerik:RadButton>
                                                    </div>
                                                    <div class="col-sm-1 center-block">
                                                    </div>
                                                </div>

                                                <br />

                                                <div class="row">
                                                    <div class="col-sm-1 center-block">
                                                    </div>
                                                    <div class="col-sm-10 right">

                                                        <telerik:RadHtmlChart runat="server" ID="RCH_Personal" Width="100%" Visible="false">
                                                            <PlotArea>
                                                                <Series>
                                                                    <telerik:ColumnSeries Name="tabla_Modalidad_Grafica" DataFieldY="Total">
                                                                        <LabelsAppearance Visible="false" />
                                                                    </telerik:ColumnSeries>
                                                                </Series>
                                                                <XAxis DataLabelsField="Usuario">
                                                                </XAxis>
                                                                <YAxis Step="1">
                                                                    <TitleAppearance Text="Estudios"></TitleAppearance>
                                                                </YAxis>
                                                            </PlotArea>
                                                            <Legend>
                                                                <Appearance Visible="false" />
                                                            </Legend>
                                                            <ChartTitle Text="Estadistica Personal">
                                                            </ChartTitle>
                                                        </telerik:RadHtmlChart>
                                                    </div>
                                                    <div class="col-sm-1 center-block">
                                                    </div>
                                                </div>



                                                <br />

                                                <div class="row">
                                                    <div class="col-sm-1 center-block">
                                                    </div>
                                                    <div class="col-sm-10 right">
                                                        <telerik:RadGrid ID="RG_Usuarios" runat="server" OnItemCommand="RG_Usuarios_ItemCommand1" Visible="false">
                                                            <MasterTableView CommandItemDisplay="Top">
                                                                <CommandItemSettings ShowExportToExcelButton="true" ShowAddNewRecordButton="false" ShowRefreshButton="false" />
                                                            </MasterTableView>

                                                        </telerik:RadGrid>
                                                    </div>
                                                    <div class="col-sm-1 center-block">
                                                    </div>
                                                </div>

                                                <br />


                                                <div class="row">
                                                    <div class="col-sm-1 center-block">
                                                    </div>

                                                    <div class="col-sm-7 center-block">
                                                    </div>
                                                    <div class="col-sm-3 right">
                                                        <%-- <telerik:RadButton ID="RB_export" runat="server" Text="Exportar Datos" Width="100%" OnClick="RB_export_Click" Visible="false" AutoPostBack="true">
                                                        </telerik:RadButton>--%>
                                                        <asp:ImageButton ID="IB_Export_usuarios" runat="server" ImageUrl="Images/Excel_XLSX.png" Width="50px" Height="50px"
                                                            OnClick="RB_export_Click" AlternateText="ExcelML" />
                                                    </div>
                                                    <div class="col-sm-1 center-block">
                                                    </div>
                                                </div>

                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
