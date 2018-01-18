<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="Fuji.RISLite.Site.Default" Culture="es-ES" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <link rel="stylesheet" href="assets/css/bootstrap.min.css" />
    <link rel="stylesheet" href="assets/font-awesome/4.5.0/css/font-awesome.min.css" />

    <!-- page specific plugin styles -->

	<!-- text fonts -->
	<link rel="stylesheet" href="assets/css/fonts.googleapis.com.css" />

	<!-- ace styles -->
	<link rel="stylesheet" href="assets/css/ace.min.css" class="ace-main-stylesheet" id="main_ace_style" />

    <link rel="stylesheet" href="assets/css/Site.css" />

	<!--[if lte IE 9]>
		<link rel="stylesheet" href="assets/css/ace-part2.min.css" class="ace-main-stylesheet" />
	<![endif]-->
	<link rel="stylesheet" href="assets/css/ace-skins.min.css" />
	<link rel="stylesheet" href="assets/css/ace-rtl.min.css" />

	<!--[if lte IE 9]>
		<link rel="stylesheet" href="assets/css/ace-ie.min.css" />
	<![endif]-->

	<!-- inline styles related to this page -->

    <script src="assets/jquery/dist/jquery.min.js"></script>
	<!-- ace settings handler -->
	<script src="assets/js/ace-extra.min.js"></script>
    <script src="assets/js/bootbox.js"></script>


    <link rel="stylesheet" href="styles/kendo.common.min.css" />
    <link rel="stylesheet" href="styles/kendo.default.min.css" />
    <link rel="stylesheet" href="styles/kendo.default.mobile.min.css" />
    <script src="js/jquery.min.js"></script>
    <script src="js/kendo.all.min.js"></script>
    <script src="js/kendo.timezones.min.js"></script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="page-content">
        <div class="page-header">
            <h1>Dashboard
			    <small>
                    <i class="ace-icon fa fa-angle-double-right"></i>
                    Estudios
                </small>
            </h1>
        </div>
        <!-- /.page-header -->
        <div class="row">
            <div class="col-xs-12">
                <h1>
                    <asp:Label ID="lblUser" runat="server" Text=""></asp:Label></h1>

              <%--   <telerik:RadScriptManager runat="server" ID="RadScrip tManager1" EnableScriptGlobalization="true"/>--%>
                <telerik:RadSkinManager ID="RadSkinManager1" runat="server" ShowChooser="false" />
                <telerik:RadAjaxManager ID="RadAjaxManager1" runat="server">
                    <AjaxSettings>
                        <telerik:AjaxSetting AjaxControlID="RS_Agenda">
                            <UpdatedControls>
                                <telerik:AjaxUpdatedControl ControlID="RS_Agenda" LoadingPanelID="RadAjaxLoadingPanel1"></telerik:AjaxUpdatedControl>
                            </UpdatedControls>
                        </telerik:AjaxSetting>                       
                    </AjaxSettings>
                </telerik:RadAjaxManager>

                <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server">
                </telerik:RadAjaxLoadingPanel>

                <telerik:RadAjaxPanel runat="server" ID="RadAjaxPanel1" LoadingPanelID="RadAjaxLoadingPanel1">
                                   
               <%--                 <telerik:RadScheduler ID="RadScheduler1" runat="server" DataKeyField="TaskID" Height="100%"
                      
                        AllowDelete="false"
                        AllowInsert="false"
                        AllowEdit="false"

                        OnNavigationCommand="RS_Agenda_NavigationCommand2"
                        OnResourceHeaderCreated="RS_Agenda_ResourceHeaderCreated1"
                        OnAppointmentDataBound="RS_Agenda_AppointmentDataBound1"                       

                        RenderMode="Lightweight"
                        GroupBy="vchCodigo"
                        GroupingDirection="Horizontal"
                                                
                        SelectedDate="2017-08-24"
                        AppointmentStyleMode="Default"
                        FirstDayOfWeek="Friday"
                        LastDayOfWeek="Sunday"
                        DataSubjectField="Description"
                        DataStartField="Start"
                        DataEndField="End"
                        SelectedView="AgendaView"
                        OverflowBehavior="Auto"
                        EnableDescriptionField="true"
                        ShowFooter="false"
                        
                        Culture="es-ES" Localization-HeaderTimeline="Linea de tiempo"                         
                        Localization-HeaderWeek="Semana" Localization-HeaderYear="Año" 
                        Localization-HeaderToday="Hoy" Localization-HeaderMonth="Mes" 
                        Localization-HeaderDay="Día" Localization-AllDay="Todo el día" 
                        Localization-HeaderAgendaAppointment="Cita" 
                        Localization-HeaderAgendaDate="Fecha" 
                        Localization-HeaderAgendaResource="Modalidad" 
                        Localization-HeaderAgendaTime="Horario">

                        <AdvancedForm Modal="true"></AdvancedForm>                        
                        <Reminders Enabled="false"></Reminders>
                        <TimeSlotContextMenuSettings EnableDefault="true"></TimeSlotContextMenuSettings>
                        <AppointmentContextMenuSettings EnableDefault="true"></AppointmentContextMenuSettings>

                        <WeekView UserSelectable="false" />
                        <MonthView UserSelectable="false" />
                        <TimelineView UserSelectable="false" />
                        <AgendaView UserSelectable="true" NumberOfDays="1"  />

                        <ResourceHeaderTemplate>
                            <div id="DIVTitulosEncabezados_agenda" class="row">
                                <asp:Panel ID="Panel_Agenda" runat="server">
                                    <div class="col-sm-3">
                                        <asp:Panel ID="ResourceImageWrapper_Agenda" runat="server">
                                            <asp:Image ID="Imagen_Modalidad_Agenda" runat="server" Height="50px" hAlternateText='<%# Eval("Text") %>'></asp:Image>
                                        </asp:Panel>
                                    </div>
                                    <div class="col-sm-9 align-center">
                                        <asp:Label ID="LEncabezado" runat="server" Text='<%# Eval("Text") %>'></asp:Label>
                                    </div>
                                </asp:Panel>
                            </div>
                        </ResourceHeaderTemplate>

                        <ResourceTypes>
                            <telerik:ResourceType KeyField="intModalidadID" Name="vchCodigo" TextField="vchCodigo" ForeignKeyField="intModalidadID"
                                DataSourceID="SqlDataSource1"></telerik:ResourceType>
                        </ResourceTypes>
                        <ResourceStyles>
                        </ResourceStyles>
                        <TimeSlotContextMenuSettings EnableDefault="true"></TimeSlotContextMenuSettings>
                        <AppointmentContextMenuSettings EnableDefault="true"></AppointmentContextMenuSettings>

                    </telerik:RadScheduler>--%>
            
                    <telerik:RadScheduler ID="RS_Agenda" runat="server" DataKeyField="TaskID" Height="100%"
                      
                        AllowDelete="false"
                        AllowInsert="false"
                        AllowEdit="false"

                        OnNavigationCommand="RS_Agenda_NavigationCommand2"
                        OnResourceHeaderCreated="RS_Agenda_ResourceHeaderCreated1"
                        OnAppointmentDataBound="RS_Agenda_AppointmentDataBound1"                       

                        RenderMode="Lightweight"
                        GroupBy="vchCodigo"
                        GroupingDirection="Horizontal"
                                                                     
                        AppointmentStyleMode="Default"
                        FirstDayOfWeek="Monday"
                        LastDayOfWeek="Sunday"
                        DataSubjectField="Description"
                        DataStartField="Start"
                        DataEndField="End"
                        SelectedView="AgendaView"
                        OverflowBehavior="Auto"
                        EnableDescriptionField="true"
                        ShowFooter="false"
                        
                        Culture="es-ES" Localization-HeaderTimeline="Linea de tiempo"                         
                        Localization-HeaderWeek="Semana" Localization-HeaderYear="Año" 
                        Localization-HeaderToday="Hoy" Localization-HeaderMonth="Mes" 
                        Localization-HeaderDay="Día" Localization-AllDay="Todo el día" 
                        Localization-HeaderAgendaAppointment="Cita" 
                        Localization-HeaderAgendaDate="Fecha" 
                        Localization-HeaderAgendaResource="Modalidad" 
                        Localization-HeaderAgendaTime="Horario">

                        <AdvancedForm Modal="true"></AdvancedForm>                        
                        <Reminders Enabled="false"></Reminders>
                        <TimeSlotContextMenuSettings EnableDefault="true"></TimeSlotContextMenuSettings>
                        <AppointmentContextMenuSettings EnableDefault="true"></AppointmentContextMenuSettings>

                        <WeekView UserSelectable="false" />
                        <MonthView UserSelectable="false" />
                        <TimelineView UserSelectable="false" />
                        <AgendaView UserSelectable="true" NumberOfDays="1"  />

                        <ResourceHeaderTemplate>
                            <div id="DIVTitulosEncabezados_agenda" class="row">
                                <asp:Panel ID="Panel_Agenda" runat="server">
                                    <div class="col-sm-3">
                                        <asp:Panel ID="ResourceImageWrapper_Agenda" runat="server">
                                            <asp:Image ID="Imagen_Modalidad_Agenda" runat="server" Height="50px" hAlternateText='<%# Eval("Text") %>'></asp:Image>
                                        </asp:Panel>
                                    </div>
                                    <div class="col-sm-9 align-center">
                                        <asp:Label ID="LEncabezado" runat="server" Text='<%# Eval("Text") %>'></asp:Label>
                                    </div>
                                </asp:Panel>
                            </div>
                        </ResourceHeaderTemplate>

                        <ResourceTypes>
                            <telerik:ResourceType KeyField="intModalidadID" Name="vchCodigo" TextField="vchCodigo" ForeignKeyField="intModalidadID"
                                DataSourceID="SqlDataSource1"></telerik:ResourceType>
                        </ResourceTypes>
                        <ResourceStyles>
                        </ResourceStyles>
                        <TimeSlotContextMenuSettings EnableDefault="true"></TimeSlotContextMenuSettings>
                        <AppointmentContextMenuSettings EnableDefault="true"></AppointmentContextMenuSettings>

                    </telerik:RadScheduler>
         
                </telerik:RadAjaxPanel>


                <asp:SqlDataSource ID="SqlDataSource1" runat="server"
                    ConnectionString="<%$ ConnectionStrings:BD2 %>"
                    SelectCommand="SELECT * FROM [tbl_CAT_Modalidad] WHERE bitActivo = 'True' AND intSitioID = @idsitioss_">

                   <SelectParameters>
                        <asp:Parameter Name="idsitioss_" Type="String" DefaultValue="1" />
                   </SelectParameters>

                </asp:SqlDataSource>
            </div>
        </div>
    </div>


    <!-- page specific plugin scripts -->
    <script src="assets/js/jquery-ui.custom.min.js"></script>
    <script src="assets/js/jquery.ui.touch-punch.min.js"></script>
    <script src="assets/js/moment.min.js"></script>
    <script src="assets/js/fullcalendar.min.js"></script>
    <script src="assets/js/bootbox.js"></script>
    <!-- inline scripts related to this page -->


    <script type="text/javascript">


</script>
</asp:Content>
