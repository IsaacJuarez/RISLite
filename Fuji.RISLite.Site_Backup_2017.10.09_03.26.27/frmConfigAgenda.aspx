<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="frmConfigAgenda.aspx.cs" Inherits="Fuji.RISLite.Site.frmConfigAgenda" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>

 <%@ Register TagPrefix="uc" TagName="uc1" Src="~/WebUserControl2.ascx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <!-- page specific plugin styles -->
		<link rel="stylesheet" href="assets/css/jquery-ui.custom.min.css" />
		<link rel="stylesheet" href="assets/css/chosen.min.css" />
		<link rel="stylesheet" href="assets/css/bootstrap-datepicker3.min.css" />
		<link rel="stylesheet" href="assets/css/bootstrap-timepicker.min.css" />
		<link rel="stylesheet" href="assets/css/daterangepicker.min.css" />
		<link rel="stylesheet" href="assets/css/bootstrap-datetimepicker.min.css" />
		<link rel="stylesheet" href="assets/css/bootstrap-colorpicker.min.css" />
    <script src="assets/js/jquery-ui.custom.min.js"></script>
		<script src="assets/js/jquery.ui.touch-punch.min.js"></script>
		<script src="assets/js/chosen.jquery.min.js"></script>
		<script src="assets/js/spinbox.min.js"></script>
		<script src="assets/js/bootstrap-datepicker.min.js"></script>
		<script src="assets/js/bootstrap-timepicker.min.js"></script>
		<script src="assets/js/moment.min.js"></script>
		<script src="assets/js/daterangepicker.min.js"></script>
		<script src="assets/js/bootstrap-datetimepicker.min.js"></script>
		<script src="assets/js/bootstrap-colorpicker.min.js"></script>
		<script src="assets/js/jquery.knob.min.js"></script>
		<script src="assets/js/autosize.min.js"></script>
		<script src="assets/js/jquery.inputlimiter.min.js"></script>
		<script src="assets/js/jquery.maskedinput.min.js"></script>
		<script src="assets/js/bootstrap-tag.min.js"></script>
</asp:Content>



<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

      <telerik:RadAjaxManager ID="RadAjaxManager1" runat="server">
     <AjaxSettings>


           <telerik:AjaxSetting AjaxControlID="GV_Agenda">
                <UpdatedControls>                   
                     <telerik:AjaxUpdatedControl ControlID="GV_Agenda" />                       
                </UpdatedControls>                  
            </telerik:AjaxSetting>

           <telerik:AjaxSetting AjaxControlID="btnAgregar">
                <UpdatedControls>                   
                     <telerik:AjaxUpdatedControl ControlID="GV_Agenda" />          
                     <telerik:AjaxUpdatedControl ControlID="GV_Agenda" />           
                     <telerik:AjaxUpdatedControl ControlID="GV_Agenda" />           
                </UpdatedControls>                  
            </telerik:AjaxSetting>

            <telerik:AjaxSetting AjaxControlID="RB_agregar_DiaFeriado">
                <UpdatedControls>                   
                     <telerik:AjaxUpdatedControl ControlID="RDZ_DiaFeriado" />   
                     <telerik:AjaxUpdatedControl ControlID="HD_DiaFeriado" />                       
                </UpdatedControls>                  
            </telerik:AjaxSetting>

         <telerik:AjaxSetting AjaxControlID="RB_Agregar_HM">
                <UpdatedControls>                   
                     <telerik:AjaxUpdatedControl ControlID="RDZ_HM" />   
                     <telerik:AjaxUpdatedControl ControlID="HD_HM" />        
                     <telerik:AjaxUpdatedControl ControlID="RDL_HM" />  
                    <telerik:AjaxUpdatedControl ControlID="HF_validacion_HM" />                        
                </UpdatedControls>                  
            </telerik:AjaxSetting>

         <telerik:AjaxSetting AjaxControlID="RB_Lunes">
                <UpdatedControls>                   
                     <telerik:AjaxUpdatedControl ControlID="RB_Lunes" />                                 
                </UpdatedControls>                  
            </telerik:AjaxSetting>
                
          <telerik:AjaxSetting AjaxControlID="RBMartes">
                <UpdatedControls>                   
                     <telerik:AjaxUpdatedControl ControlID="RBMartes" />                                 
                </UpdatedControls>                  
            </telerik:AjaxSetting>

          <telerik:AjaxSetting AjaxControlID="RBMiercoles">
                <UpdatedControls>                   
                     <telerik:AjaxUpdatedControl ControlID="RBMiercoles" />                                 
                </UpdatedControls>                  
            </telerik:AjaxSetting>

          <telerik:AjaxSetting AjaxControlID="RBJueves">
                <UpdatedControls>                   
                     <telerik:AjaxUpdatedControl ControlID="RBJueves" />                                 
                </UpdatedControls>                  
            </telerik:AjaxSetting>

          <telerik:AjaxSetting AjaxControlID="RBViernes">
                <UpdatedControls>                   
                     <telerik:AjaxUpdatedControl ControlID="RBViernes" />                                 
                </UpdatedControls>                  
            </telerik:AjaxSetting>

          <telerik:AjaxSetting AjaxControlID="RBSabado">
                <UpdatedControls>                   
                     <telerik:AjaxUpdatedControl ControlID="RBSabado" />                                 
                </UpdatedControls>                  
            </telerik:AjaxSetting>

          <telerik:AjaxSetting AjaxControlID="RB_Domingo">
                <UpdatedControls>                   
                     <telerik:AjaxUpdatedControl ControlID="RB_Domingo" />                                 
                </UpdatedControls>                  
            </telerik:AjaxSetting>
         
            <telerik:AjaxSetting AjaxControlID="RTP_HM_Inicio">
                <UpdatedControls>                   
                     <telerik:AjaxUpdatedControl ControlID="RTP_HM_Inicio" />  
                     <telerik:AjaxUpdatedControl ControlID="RTP_HM_Fin" />                      
                </UpdatedControls>                  
            </telerik:AjaxSetting>

          <telerik:AjaxSetting AjaxControlID="RTP_Inicio">
                <UpdatedControls>                   
                     <telerik:AjaxUpdatedControl ControlID="RTP_Inicio" />  
                     <telerik:AjaxUpdatedControl ControlID="RTP_Fin" />                      
                </UpdatedControls>                  
            </telerik:AjaxSetting>

          <telerik:AjaxSetting AjaxControlID="RBTHorarioGeneralTotal">
                <UpdatedControls>                   
                     <telerik:AjaxUpdatedControl ControlID="RTP_Inicio" />  
                     <telerik:AjaxUpdatedControl ControlID="RTP_Fin" />                      
                </UpdatedControls>                  
            </telerik:AjaxSetting>

         <telerik:AjaxSetting AjaxControlID="RDZ_DiaFeriado">
                <UpdatedControls>                   
                     <telerik:AjaxUpdatedControl ControlID="RDZ_DiaFeriado" />                          
                </UpdatedControls>                  
            </telerik:AjaxSetting>

          <telerik:AjaxSetting AjaxControlID="RDZ_HM">
                <UpdatedControls>                   
                     <telerik:AjaxUpdatedControl ControlID="RDZ_HM" />                          
                </UpdatedControls>                  
            </telerik:AjaxSetting>
         
            <telerik:AjaxSetting AjaxControlID="RB_Agergar_Modalidad">
                <UpdatedControls>                   
                     <telerik:AjaxUpdatedControl ControlID="RB_Agergar_Modalidad" />   
                        <telerik:AjaxUpdatedControl ControlID="RB_Agergar_Modalidad" />        
                </UpdatedControls>                  
            </telerik:AjaxSetting>  

           <telerik:AjaxSetting AjaxControlID="RB_Agergar_Modalidad">
                <UpdatedControls>                   
                     <telerik:AjaxUpdatedControl ControlID="RB_Agergar_Modalidad" />                          
                </UpdatedControls>                  
            </telerik:AjaxSetting>  
         
           <telerik:AjaxSetting AjaxControlID="GV_Agenda">
                <UpdatedControls>                   
                     <telerik:AjaxUpdatedControl ControlID="GV_Agenda" />                          
                </UpdatedControls>                  
            </telerik:AjaxSetting>  
         
                       <telerik:AjaxSetting AjaxControlID="ucRadColorPicker1">
                <UpdatedControls>                   
                     <telerik:AjaxUpdatedControl ControlID="ucRadColorPicker1" />               
                     <telerik:AjaxUpdatedControl ControlID="GV_Agenda" />                          
                </UpdatedControls>                  
            </telerik:AjaxSetting>  

                <telerik:AjaxSetting AjaxControlID="btnVisualizar">
                <UpdatedControls>                                               
                     <telerik:AjaxUpdatedControl ControlID="GV_Agenda" />                          
                </UpdatedControls>                  
            </telerik:AjaxSetting>  
               

           <telerik:AjaxSetting AjaxControlID="btnUpdateVarPacinete">
                <UpdatedControls>                                               
                     <telerik:AjaxUpdatedControl ControlID="GV_Agenda" />                          
                </UpdatedControls>                  
            </telerik:AjaxSetting>

           <telerik:AjaxSetting AjaxControlID="bntCancelEditPaciente">
                <UpdatedControls>                                               
                     <telerik:AjaxUpdatedControl ControlID="GV_Agenda" />                          
                </UpdatedControls>                  
            </telerik:AjaxSetting>  
              
            <telerik:AjaxSetting AjaxControlID="RB_intervalo">
                <UpdatedControls>                                               
                     <telerik:AjaxUpdatedControl ControlID="TB_intervalo" />                          
                </UpdatedControls>                  
            </telerik:AjaxSetting>  
              
         

          </AjaxSettings> 
         </telerik:RadAjaxManager>

    
        <telerik:RadScriptBlock runat="server" ID="RadCodeBlock1">
        <script type="text/javascript">
            //<![CDATA[
            var RTP_HM_Inicio;
            var RTP_HM_Fin;

            function cerrar_ventana(sender, eventArgs) {
                var evento = eventArgs.Command.get_name();
                var titulo = sender._title;              
                $find("<%= RadAjaxPanel2.ClientID%>").ajaxRequestWithTarget("<%= RadAjaxPanel2.UniqueID %>", evento + "|" + titulo);
            }

            function eliminar_HM(sender, eventArgs) {
                var evento = eventArgs.Command.get_name();
                var HM_eliminada = sender._title;             
                $find("<%= RadAjaxPanel3.ClientID%>").ajaxRequestWithTarget("<%= RadAjaxPanel3.UniqueID %>", evento + "|" + HM_eliminada);
            }

         

            function validar_fecha_duplicada(sender, args) {
                var fecha_insertada = RDP_DiaFeriado._text;
                args.IsValid = true;

                var Lista_dias_feriados = document.getElementById("HD_DiaFeriado").value.split("|");

                for (index = 0; index < Lista_dias_feriados.length; ++index) {
                   
                    if (Lista_dias_feriados[index] == fecha_insertada) {
                        alert("La fecha agregada ya se encuentra en la lista");
                        args.IsValid = false;
                    }                   
                }
            }

            function ONL_RDP_Diaferiado(sender, args) {
                RDP_DiaFeriado = sender;
            }

            


            function validar(sender, args) {
                var Date1 = new Date(RTP_HM_Inicio.get_selectedDate());
                var Date2 = new Date(RTP_HM_Fin.get_selectedDate());


                var DATE1_Horas = Date1.getHours();
                var DATE1_min = Date1.getMinutes();               

                var Fecha1_calculada = (DATE1_Horas * 60) + Number(DATE1_min);


                var DATE2_Horas = Date2.getHours();
                var DATE2_min = Date2.getMinutes();            

                var Fecha2_calculada = (DATE2_Horas * 60) + Number(DATE2_min);                           

                args.IsValid = true;
                if ((Date2 - Date1) < 0) {
                    document.getElementById("HF_validacion_HM").value = "true";
                    alert("La (Hora Muerta FIN) no puede ser mayor que (Hora Muerta INICIO)");                  
                    args.IsValid = false;
                }

                else {

                    var lista_horas = document.getElementById("HD_HM").value.split("|");

                    for (index = 0; index < lista_horas.length; ++index) {


                        if (lista_horas[index] != "") {
                            var lista_horas_independeintes = lista_horas[index].split("-");
                            var desde = lista_horas_independeintes[0];
                            var hasta = lista_horas_independeintes[1];

                            var desde_limpia = desde.split(':');

                            var desde_hrs = desde_limpia[0];
                            var desde_min = desde_limpia[1];

                            var desde_calculado = (desde_hrs * 60) + Number(desde_min);

                            var hasta_limpia = hasta.split(':');

                            var hasta_hrs = hasta_limpia[0];
                            var hasta_min = hasta_limpia[1];                          

                            var hasta_calculado = (hasta_hrs * 60) + Number(hasta_min);


                            if (Fecha1_calculada > desde_calculado && Fecha1_calculada < hasta_calculado) {
                                document.getElementById("HF_validacion_HM").value = "true";
                                alert("El periodo de tiempo insertado ya esta en el sistema");                                 
                                args.IsValid = false;
                            }

                            if (Fecha2_calculada > desde_calculado && Fecha2_calculada < hasta_calculado) {
                                document.getElementById("HF_validacion_HM").value = "true";
                                alert("El periodo de tiempo insertado ya esta en el sistema");
                                args.IsValid = false;
                                
                            }

                           //else {
                           //     document.getElementById("HF_validacion_HM").value = "false";
                           // }                            
                        }
                    }                   
                }               
            }

            function onLoadRadTimePicker1(sender, args) {
                RTP_HM_Inicio = sender;
            }

            function onLoadRadTimePicker2(sender, args) {
                RTP_HM_Fin = sender;
            }

            function solonumeros(e) {
                var unicode = e.charCode ? e.charCode : e.keyCode
                if (unicode != 8 && unicode != 44) {
                    if (unicode < 48 || unicode > 58) //if not a number
                    { return false } //disable key press    
                }
            }  
            //]]>
        </script>
    </telerik:RadScriptBlock>

        <div class="page-header">
            <div class="messagealert" id="alert_container"></div>
            <h1>
                Herramientas Agenda
            </h1>
        </div><!-- /.page-header -->


         <div class="row">
            <div class="col-lg-12 col-md-12 col-sm-12">
                <div class="tabbable tabs-left" id="Tabs">
				    <ul class="nav nav-tabs" id="myTab3">					   					  
                        <li class="active">
                            <a data-toggle="tab" href="#ConfbasicaAgenda">
                                <i class="purple ace-icon fa fa-cogs"></i>
							    Configuración
                            </a>
                        </li>
                          <li>
                            <a data-toggle="tab" href="#Confadminagenda">
                                <i class="blue ace-icon fa fa-calendar"></i>
							    Administración
                            </a>
                        </li>                      
				    </ul>

                       <div class="tab-content">

                               <div id="ConfbasicaAgenda" class="tab-pane in active">
						    <div class="widget-box">
								<div class="widget-header">
									<h4 class="widget-title">Configuración de agenda</h4>

									<div class="widget-toolbar">									
									</div>
								</div>

								<div class="widget-body">
									<div class="widget-main">
                                         <div class="widget-body">
                                                        <div class="widget-main">
                                                            <div class="row">
                                                                <div class="col-lg-12 col-md-12">
                                                                    <div class="row">
                                                                        <div class="col-sm-3 center-block">
                                                                            Horario de Actividad
                                                                        </div>
                                                                        <div class="col-sm-1 center-block">
                                                                            <asp:Label runat="server" Text="Hora de Inicio"></asp:Label>
                                                                        </div>
                                                                        <div class="col-sm-1 center-block">
                                                                            <telerik:RadTimePicker ID="RTP_Inicio" runat="server" OnSelectedDateChanged="RTP_Inicio_SelectedDateChanged" AutoPostBack="true"
                                                                                TimeView-HeaderText="Desde" onkeypress="return solonumeros(event);" DateInput-MaxLength="5"></telerik:RadTimePicker>


                                                                          <%--  <div class="input-group bootstrap-timepicker timepicker">
                                                                                <input id="timepicker1" type="text" class="form-control input-small" style="width:100%" readonly="readonly">
                                                                                <span class="input-group-addon"><i class="glyphicon glyphicon-time"></i></span>
                                                                            </div>--%>
                                                                        </div>
                                                                        <div class="col-sm-2 center-block">
                                                                        </div>
                                                                        <div class="col-sm-1 center-block">
                                                                            <asp:Label runat="server" Text="Hora Final "></asp:Label>
                                                                        </div>
                                                                        <div class="col-sm-1 center-block">
                                                                                 <telerik:RadTimePicker ID="RTP_Fin" runat="server" TimeView-HeaderText="Hasta" 
                                                                                     onkeypress="return solonumeros(event);" DateInput-MaxLength="5"></telerik:RadTimePicker>
                                                                        </div>

                                                                          <div class="col-sm-1 center-block">
                                                                        </div>

                                                                         <div class="col-sm-1 center-block">
                                                                         <%--   <asp:Button runat="server" ID="BTHorarioTotal" CssClass="btn btn-success" Text="Establecer" OnClick="BTHorarioTotal_Click"/>--%>

                                                                              <telerik:RadButton ID="RBTHorarioGeneralTotal" runat="server" Text="Establecer" CssClass="btn btn-success"
                                                                               OnClick="BTHorarioTotal_Click"></telerik:RadButton>
                                                                        </div> 

                                                                         <div class="col-sm-1 center-block">
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                            <hr />
                                                            <div class="row">
                                                            
                                                                    <div class="col-sm-3">
                                                                        Días laborales
                                                                    </div>


                                                                         <div class="col-sm-1 center">
                                                                                <telerik:RadButton RenderMode="Lightweight" ID="RB_Lunes" runat="server" ToggleType="CheckBox" ButtonType="StandardButton"
                                                                                    OnCheckedChanged="RB_dia_CheckedChanged" CssClass="btn btn-primary">
                                                                                    <ToggleStates>
                                                                                        <telerik:RadButtonToggleState Text="Lunes" PrimaryIconCssClass="p-i-checkbox-checked"></telerik:RadButtonToggleState>
                                                                                        <telerik:RadButtonToggleState Text="Lunes" PrimaryIconCssClass="p-i-checkbox" CssClass="btn btn-empty"></telerik:RadButtonToggleState>
                                                                                    </ToggleStates>
                                                                                </telerik:RadButton>
                                                                           </div>

                                                                         <div class="col-sm-1 center">
                                                                                 <telerik:RadButton RenderMode="Lightweight" ID="RBMartes" runat="server" ToggleType="CheckBox" ButtonType="StandardButton" OnCheckedChanged="RB_dia_CheckedChanged"
                                                                                    CssClass="btn btn-info2">
                                                                                    <ToggleStates>
                                                                                        <telerik:RadButtonToggleState Text="Martes" PrimaryIconCssClass="p-i-checkbox-checked"></telerik:RadButtonToggleState>
                                                                                        <telerik:RadButtonToggleState Text="Martes" PrimaryIconCssClass="p-i-checkbox" CssClass="btn btn-empty"></telerik:RadButtonToggleState>
                                                                                    </ToggleStates>
                                                                                </telerik:RadButton>
                                                                           </div>

                                                                         <div class="col-sm-2 center">
                                                                                 <telerik:RadButton RenderMode="Lightweight" ID="RBMiercoles" runat="server" ToggleType="CheckBox" ButtonType="StandardButton" OnCheckedChanged="RB_dia_CheckedChanged"
                                                                                    CssClass="btn btn-info">
                                                                                    <ToggleStates>
                                                                                        <telerik:RadButtonToggleState Text="Miercoles" PrimaryIconCssClass="p-i-checkbox-checked"></telerik:RadButtonToggleState>
                                                                                        <telerik:RadButtonToggleState Text="Miercoles" PrimaryIconCssClass="p-i-checkbox" CssClass="btn btn-empty"></telerik:RadButtonToggleState>
                                                                                    </ToggleStates>
                                                                                </telerik:RadButton>
                                                                           </div>

                                                                         <div class="col-sm-1 center">
                                                                             
                                                                             <telerik:RadButton RenderMode="Lightweight" ID="RBJueves" runat="server" ToggleType="CheckBox" ButtonType="StandardButton" OnCheckedChanged="RB_dia_CheckedChanged"
                                                                                    CssClass="btn btn-purple">
                                                                                    <ToggleStates>
                                                                                        <telerik:RadButtonToggleState Text="Jueves" PrimaryIconCssClass="p-i-checkbox-checked"></telerik:RadButtonToggleState>
                                                                                        <telerik:RadButtonToggleState Text="Jueves" PrimaryIconCssClass="p-i-checkbox" CssClass="btn btn-empty"></telerik:RadButtonToggleState>
                                                                                    </ToggleStates>
                                                                                </telerik:RadButton>
                                                                           </div>

                                                                         <div class="col-sm-1 center">
                                                                              <telerik:RadButton RenderMode="Lightweight" ID="RBViernes" runat="server" ToggleType="CheckBox" ButtonType="StandardButton" OnCheckedChanged="RB_dia_CheckedChanged"
                                                                                     CssClass="btn btn-warning">
                                                                                    <ToggleStates>
                                                                                        <telerik:RadButtonToggleState Text="Viernes" PrimaryIconCssClass="p-i-checkbox-checked"></telerik:RadButtonToggleState>
                                                                                        <telerik:RadButtonToggleState Text="Viernes" PrimaryIconCssClass="p-i-checkbox" CssClass="btn btn-empty"></telerik:RadButtonToggleState>
                                                                                    </ToggleStates>
                                                                                </telerik:RadButton>
                                                                           </div>

                                                                         <div class="col-sm-1 center">
                                                                              <telerik:RadButton RenderMode="Lightweight" ID="RBSabado" runat="server" ToggleType="CheckBox" ButtonType="StandardButton" OnCheckedChanged="RB_dia_CheckedChanged"
                                                                                     CssClass="btn btn-danger">
                                                                                    <ToggleStates>
                                                                                        <telerik:RadButtonToggleState Text="Sábado" PrimaryIconCssClass="p-i-checkbox-checked"></telerik:RadButtonToggleState>
                                                                                        <telerik:RadButtonToggleState Text="Sábado" PrimaryIconCssClass="p-i-checkbox"  CssClass="btn btn-empty"></telerik:RadButtonToggleState>
                                                                                    </ToggleStates>
                                                                                </telerik:RadButton>
                                                                           </div>
                                                                    <div class="col-sm-1 center">                                                                                                                                                                                                                                                                                                                                                                   
                                                                             <telerik:RadButton RenderMode="Lightweight" ID="RB_Domingo" runat="server" ToggleType="CheckBox" ButtonType="StandardButton" OnCheckedChanged="RB_dia_CheckedChanged"
                                                                                     CssClass="btn btn-pink">
                                                                                    <ToggleStates>
                                                                                        <telerik:RadButtonToggleState Text="Domingo" PrimaryIconCssClass="p-i-checkbox-checked"></telerik:RadButtonToggleState>
                                                                                        <telerik:RadButtonToggleState Text="Domingo" PrimaryIconCssClass="p-i-checkbox" CssClass="btn btn-empty"></telerik:RadButtonToggleState>
                                                                                    </ToggleStates>
                                                                                </telerik:RadButton>
                                                                    </div>

                                                                    
                                                                </div>
                                                      
                                                            <hr />
                                                             <div class="row">
                                                                <div class="col-lg-12">
                                                                    <div class="col-lg-3 col-sm-3 col-xs-12">
                                                                        Días feriados
                                                                    </div>

                                                                      <div class="col-lg-8 col-sm-9 col-xs-12">

                                                                           
                                                                             <telerik:RadAjaxPanel ID="RadAjaxPanel2" runat="server" OnAjaxRequest="RadAjaxPanel2_AjaxRequest">
                                                                                        <telerik:RadDockLayout ID="RDL_DiaFeriado" runat="server" >
                                                                                            <telerik:RadDockZone runat="server" ID="RDZ_DiaFeriado" Orientation="Horizontal"
                                                                                                Height="115px">
                                                                                            </telerik:RadDockZone>
                                                                                        </telerik:RadDockLayout>
                                                                               </telerik:RadAjaxPanel>
                                                                           
                                                                     
                                                                           </div>                                                           
                                                                </div>
                                                            </div>
                                                            <br/>
                                                            <div class="row">
                                                                <div class="col-lg-12">
                                                                    <div class="col-sm-7">
                                                                    </div>
                                                                    <div class="col-sm-3">

                                                                        <telerik:RadDatePicker ID="RDP_DiaFeriado" runat="server" TimeView-HeaderText="Día feriado">
                                                                            <DateInput ID="DateInput3" runat="server">
                                                                                    <ClientEvents OnLoad="ONL_RDP_Diaferiado"></ClientEvents>
                                                                                </DateInput>
                                                                        </telerik:RadDatePicker>
                                                                                                                                       
                                                                    </div>
                                                                     <div class="col-sm-1">
                                                                           <telerik:RadButton ID="RB_agregar_DiaFeriado" runat="server" Text="Agregar" CssClass="btn btn-success" 
                                                                               OnClick="RadButton1_Click" OnClientClicking="validar_fecha_duplicada"></telerik:RadButton>
                                                                         </div>

                                                                     <div class="col-sm-1">
                                                                         </div>
                                                                </div>
                                                            </div>
                                                            <hr />
                                                            <div class="row">
                                                                <div class="col-lg-12">
                                                                    <div class="col-lg-3 col-sm-3 col-xs-12">
                                                                        Horas Muertas por día.
                                                                    </div>
                                                                    <div class="col-lg-8 col-sm-9 col-xs-12">
                                                                        <telerik:RadAjaxPanel ID="RadAjaxPanel3" runat="server" OnAjaxRequest="RadAjaxPanel3_AjaxRequest">
                                                                            <telerik:RadDockLayout ID="RDL_HM" runat="server">
                                                                                <telerik:RadDockZone runat="server" ID="RDZ_HM" Orientation="Horizontal"
                                                                                    Height="75px">
                                                                                </telerik:RadDockZone>
                                                                            </telerik:RadDockLayout>
                                                                        </telerik:RadAjaxPanel>

                                                                    </div>
                                                                </div>
                                                            </div>

                                                              
                                                                 <hr />
                                                        <telerik:RadAjaxPanel ID="RadAjaxPanel1" runat="server">
                                                            <div  class="row">
                                                        
                                                                    <div class="col-sm-3" >
                                                                    </div>
                                                                    <div class="col-sm-1">
                                                                       <asp:Label runat="server" Text="Inicio"></asp:Label>
                                                                    </div>
                                                                    <div class="col-sm-2">
                                                                          <telerik:RadTimePicker ID="RTP_HM_Inicio" runat="server" TimeView-HeaderText="Desde" onkeypress="return solonumeros(event);"
                                                                              OnSelectedDateChanged="RTP_HM_Inicio_SelectedDateChanged" AutoPostBack="true" DateInput-MaxLength="5">
                                                                               <DateInput ID="DateInput1" runat="server">
                                                                                            <ClientEvents OnLoad="onLoadRadTimePicker1"></ClientEvents>
                                                                                        </DateInput>
                                                                          </telerik:RadTimePicker>     
                                                                        
                                                                    </div>
                                                                    <div class="col-sm-1">
                                                                    </div>
                                                                    <div class="col-sm-1">
                                                                        <asp:Label runat="server" Text="Final"></asp:Label>
                                                                    </div>
                                                                    <div class="col-sm-2">
                                                                        <telerik:RadTimePicker ID="RTP_HM_Fin" runat="server" TimeView-HeaderText="Hasta" onkeypress="return solonumeros(event);" 
                                                                            DateInput-MaxLength="5">
                                                                            <DateInput ID="DateInput2" runat="server">
                                                                                    <ClientEvents OnLoad="onLoadRadTimePicker2"></ClientEvents>
                                                                                </DateInput>
                                                                        </telerik:RadTimePicker>
                                                                 

                                                                    </div>
                                                                    <div class="col-sm-2">
                                                                             <telerik:RadButton ID="RB_Agregar_HM" runat="server" Text="Agregar" CssClass="btn btn-success" 
                                                                                 OnClick="RB_Agregar_HM_Click" OnClientClicking="validar"></telerik:RadButton>                                                                     
                                                                    </div>
                                                                
                                                           
                                                                 <%--   <asp:CustomValidator ID="CustomValidator1" EnableClientScript="true" runat="server"
                                                                        ControlToValidate="RTP_HM_Fin" ClientValidationFunction="validate">
                                                                    </asp:CustomValidator>        --%>                                                            
                                                               

                                                            </div>   
                                                            
                                                          </telerik:RadAjaxPanel>                                                         
                                                                 
                                                               <hr />
                                                              <div class="row">
                                                                <div class="col-lg-12 col-md-12">
                                                                    <div class="row">
                                                                        <div class="col-sm-3 center-block">
                                                                            Intervalo
                                                                        </div>
                                                                        <div class="col-sm-1 center-block">
                                                                            <asp:Label runat="server" Text="Minutos"></asp:Label>
                                                                        </div>
                                                                        <div class="col-sm-1 center-block">                                                                         
                                                                        </div>
                                                                        <div class="col-sm-4 center-block">
                                                                            <asp:TextBox ID="TB_intervalo" runat="server" Width="100%" MaxLength="5" 
                                                                                onkeypress="return solonumeros(event);"></asp:TextBox>
                                                                         
                                                                        </div>                                                                      

                                                                          <div class="col-sm-1 center-block">
                                                                        </div>

                                                                         <div class="col-sm-1 center-block">                                                                        
                                                                              <telerik:RadButton ID="RB_intervalo" runat="server" Text="Establecer" CssClass="btn btn-success"
                                                                               OnClick="RB_intervalo_Click"></telerik:RadButton>
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
                           
                               <div id="Confadminagenda" class="tab-pane">
						    <div class="widget-box">
								<div class="widget-header">
									<h4 class="widget-title">Administraión de agenda</h4>

									<div class="widget-toolbar">
									</div>
								</div>

								<div class="widget-body">
									<div class="widget-main">    
                               
                                            <div class="row form-group">
                                            <div class="col-lg-3 col-sm-12">
                                                <asp:TextBox runat="server" ID="txt_Modalidad" Text="" CssClass="form-control" placeholder="Nombre de modalidad"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="rfvModaldidad" runat="server" Text="* Capturar modalidad." ForeColor="Red"  ControlToValidate="txt_Modalidad" ValidationGroup="Agregar_Modalidad"></asp:RequiredFieldValidator>
                                            </div>
                                            <div class="col-lg-5 col-sm-12">
                                                 <asp:TextBox runat="server" ID="txtDescripcion" Text="" CssClass="form-control" placeholder="Descripción de modalidad"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="rfvDescripcion" runat="server" Text="* Capturar descripción." ForeColor="Red"  ControlToValidate="txtDescripcion" ValidationGroup="Agregar_Modalidad"></asp:RequiredFieldValidator>
                                            </div>
                                            <div class="col-lg-1 col-sm-12 center-block">
                                                <telerik:RadColorPicker runat="server" ShowIcon="true" ID="RCP_add_modadlidad" Preset="Default" Width="100%" />                                             
                                            </div>                                            
                                                    <div class="col-lg-3 col-sm-12 center-block">

                                                           <telerik:RadButton ID="RB_Agergar_Modalidad" runat="server" Text="Agregar" CssClass="btn btn-success" 
                                                                                 OnClick="btnAgregar_Click1" OnClientClicking="validar" ValidationGroup="vgAddUser">
                                                           </telerik:RadButton>            
                                                     <%--   <asp:Button runat="server" ID="btnAgregar" CssClass="btn-block btn-success " Text="Agregar" OnClick="btnAgregar_Click1"  ValidationGroup="vgAddUser"/>--%>
                                                    </div>                                              
                                            </div>
                                   
                                         <asp:GridView ID="GV_Agenda" runat="server" AllowPaging="true" CssClass="table table-striped table-bordered"
                                                                    PageSize="10" AutoGenerateColumns="false" OnRowDataBound="GV_Agenda_RowDataBound" Font-Size="10px"
                                                                    OnPageIndexChanging="GV_Agenda_PageIndexChanging" DataKeyNames="intModalidadID"  OnRowEditing="GV_Agenda_RowEditing"
                                                                    OnRowCommand="GV_Agenda_RowCommand" UpdateMode="Conditional" OnRowUpdating="GV_Agenda_RowUpdating"
                                                                    EmptyDataText="No hay resultado bajo el criterio de búsqueda." OnRowCancelingEdit="GV_Agenda_RowCancelingEdit1">
                                                                 <EditRowStyle BackColor="#DEDEDE" />
                                                                    <Columns>

                                                                        <asp:TemplateField HeaderText="IDModalidad" >
                                                                            <ItemTemplate>
                                                                                <asp:Label id="lblIDModalidad" runat="server"><%# Eval("intModalidadID")%></asp:Label>
                                                                            </ItemTemplate>
                                                                            <EditItemTemplate>
                                                                                <asp:TextBox ID="TxtIDModalidad" runat="server" Text='<%# Bind("intModalidadID")%>' CssClass="form-control" ></asp:TextBox>
                                                                            </EditItemTemplate>
                                                                            </asp:TemplateField>

                                                                        <asp:TemplateField HeaderText="Modaldidad">
                                                                            <ItemTemplate>
                                                                                <asp:Label id="lblcodigo" runat="server"><%# Eval("vchCodigo")%></asp:Label>
                                                                            </ItemTemplate>
                                                                            <EditItemTemplate>
                                                                                <asp:TextBox ID="Txtcodigo" runat="server" Text='<%# Bind("vchCodigo")%>' CssClass="form-control" ></asp:TextBox>
                                                                            </EditItemTemplate>
                                                                            </asp:TemplateField>

                                                                           <asp:TemplateField HeaderText="Descripción">
                                                                            <ItemTemplate>
                                                                                <asp:Label id="lblmodalidad" runat="server"><%# Eval("vchModalidad")%></asp:Label>
                                                                            </ItemTemplate>
                                                                            <EditItemTemplate>
                                                                                <asp:TextBox ID="Txtmodalidad" runat="server" Text='<%# Bind("vchModalidad")%>' CssClass="form-control" ></asp:TextBox>
                                                                            </EditItemTemplate>
                                                                            </asp:TemplateField>

                                                                         <asp:TemplateField HeaderText="Color">
                                                                            <ItemTemplate>
                                                                                <div style='position:center; width: 16px; height: 16px; background-color: <%# Eval("vchColor") %>'></div> 
                                                                            </ItemTemplate>
                                                                            <EditItemTemplate>
                                                                             <%--   <telerik:RadColorPicker runat="server" ShowIcon="true" ID="RadColorPicker1" Preset="Default" Visible="false" />    --%>                                                                             
                                                                                <uc:uc1 runat="server" id="ucRadColorPicker1" DBSelectedColor ='<%# Bind("vchColor") %>' />                                                                         
                                                                            </EditItemTemplate>
                                                                            </asp:TemplateField>
                                                                         
                                                                                                                     
                                                                        <asp:TemplateField HeaderText="Editar" ItemStyle-HorizontalAlign="Center">

                                                                            <ItemTemplate>                                                                                   
                                                                               <%-- <asp:LinkButton ID="btnVisualizar" CausesValidation="false" CommandName="Edit" CommandArgument='<%# Bind("intModalidadID") %>' runat="server">--%>
                                                                                  <asp:LinkButton ID="btnVisualizar" CausesValidation="false" CommandName="Edit" runat="server">
                                                                                    <asp:Image ID="ImageVisializa" runat="server" ImageUrl="~/images/ic_action_edit.png" Height="25px" Width="25px" ToolTip="Editar"/>
                                                                                </asp:LinkButton>
                                                                            </ItemTemplate>
                                                                            <EditItemTemplate>                                                                               
                                                                                               <asp:LinkButton ID="btnUpdateVarPacinete" runat="server" CommandName="Update"  Text="Actualizar">
                                                                                                   <i class="fa fa-floppy-o" aria-hidden="true"  title="Actualizar" style="font-size:25px;"></i>
                                                                                               </asp:LinkButton>
                                                                                               <asp:LinkButton ID="bntCancelEditPaciente" runat="server" CommandName="Cancel"  Text="Cancelar" >
                                                                                                   <i class="fa fa-ban" aria-hidden="true" title="Cancelar" style="font-size:25px;"></i>
                                                                                               </asp:LinkButton>
                                                                                           </EditItemTemplate>                                                                           
                                                                        </asp:TemplateField>

                                                                        <asp:TemplateField HeaderText="Estatus" ItemStyle-HorizontalAlign="Center">
                                                                            <ItemTemplate>
                                                                                <asp:ImageButton ID="imbEstatus" runat="server" BackColor="Transparent"  Height="25px" Width="25px"  
                                                                                    CommandArgument='<%#Eval("intModalidadID") %>' CommandName="Estatus" ToolTip="Cambia el estatus de la modalidad" />
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

                                           <%-- <asp:UpdatePanel runat="server">                                                	                                                                                              
                                                    <ContentTemplate>
                                                        <asp:Panel runat="server">
                                                           
                                                        </asp:Panel>
                                                    </ContentTemplate>
                                                </asp:UpdatePanel>--%>
                                          
                                    </div>
                                </div>
                            </div>
					    </div>

                       
                           </div>

                 </div>
            </div>
               <asp:HiddenField ID="HD_DiaFeriado" runat="server" ClientIDMode="Static" />
               <asp:HiddenField ID="HD_HM" runat="server" ClientIDMode="Static" />
              <asp:HiddenField ID="HF_validacion_HM" runat="server" ClientIDMode="Static"/>
        </div>

     
    <style type="text/css"> 
        .completionList {
            border:solid 1px Gray;
            margin:0px;
            padding:3px;
            height: 120px;
            overflow:auto;
            background-color: #FFFFFF;     
        } 
        .listItem {
            color: #191919;
        } 
        .itemHighlighted {
            background-color: #ADD6FF;       
        }
        .ajax__calendar_today
        {
            color:Red;    
        }

        .ajax__calendar_active  
        {
            color: #004080;
            font-weight: bold;
            background-color: #000;
        }

        .cal .ajax__calendar_header
        {
            background-color: Silver;
        }

        .cal .ajax__calendar_container
        {
            background-color: #CEECF5;
        }
        .btn span.glyphicon {    			
	        opacity: 0;				
        }
        .btn.active span.glyphicon {				
	        opacity: 1;				
        }

      
        /*.rdTitlebar,.rdTitle  
    {  
        background:green !important;  
    }*/     

        /*html .myDock.RadDock_Bootstrap div.rdTitleBar em {
             color: #428bca;
        }*/

    </style>
    <script type="text/javascript">

        jQuery(function ($) {

            $("#datepicker").datepicker({
                showOtherMonths: true,
                selectOtherMonths: false,

            });

            var tag_input = $('#form-field-tags');
            try {
                tag_input.tag(
                    {
                        placeholder: tag_input.attr('placeholder'),
                        //enable typeahead by specifying the source array
                        source: ace.vars['US_STATES'],//defined in ace.js >> ace.enable_search_ahead
						/**
						//or fetch data from database, fetch those that match "query"
						source: function(query, process) {
						  $.ajax({url: 'remote_source.php?q='+encodeURIComponent(query)})
						  .done(function(result_items){
							process(result_items);
						  });
						}
						*/
                    }
                )

                //programmatically add/remove a tag
                var $tag_obj = $('#form-field-tags').data('tag');
                $tag_obj.add('01-01-2017');
                $tag_obj.add('01-05-2017');
                $tag_obj.add('05-06-2017');
                var index = $tag_obj.inValues('some tag');
                $tag_obj.remove(index);
            }
            catch (e) {
                //display a textarea for old IE, because it doesn't support this plugin or another one I tried!
                tag_input.after('<textarea id="' + tag_input.attr('id') + '" name="' + tag_input.attr('name') + '" rows="3">' + tag_input.val() + '</textarea>').remove();
                //autosize($('#form-field-tags'));
            }

            //Tag2
            var tag_input2 = $('#form-field-tags2');
            try {
                tag_input2.tag(
                    {
                        placeholder: tag_input2.attr('placeholder'),
                        //enable typeahead by specifying the source array
                        source: ace.vars['US_STATES'],//defined in ace.js >> ace.enable_search_ahead
						/**
						//or fetch data from database, fetch those that match "query"
						source: function(query, process) {
						  $.ajax({url: 'remote_source.php?q='+encodeURIComponent(query)})
						  .done(function(result_items){
							process(result_items);
						  });
						}
						*/
                    }
                )

                //programmatically add/remove a tag
                var $tag_obj = $('#form-field-tags2').data('tag');
                $tag_obj.add('2:30 PM-3:00 PM');
                $tag_obj.add('7:30 PM-8:00 PM');
                var index = $tag_obj.inValues('some tag');
                $tag_obj.remove(index);
            }
            catch (e) {
                //display a textarea for old IE, because it doesn't support this plugin or another one I tried!
                tag_input2.after('<textarea id="' + tag_input2.attr('id') + '" name="' + tag_input2.attr('name') + '" rows="3">' + tag_input2.val() + '</textarea>').remove();
                //autosize($('#form-field-tags'));
            }

            $('#loading-btn').on(ace.click_event, function () {
                var btn = $(this);
                btn.button('loading')
                setTimeout(function () {
                    btn.button('reset')
                }, 2000)
            });

            $('#Agregar-btn').on(ace.click_event, function () {
                var btn = $(this);
                btn.button('loading')
                setTimeout(function () {
                    btn.button('reset')
                }, 2000)
            });
        });
        $('#timepicker1').timepicker();
        $('#timepicker2').timepicker();
        $('#timepicker3').timepicker();
        $('#timepicker4').timepicker();
    </script>
</asp:Content>
