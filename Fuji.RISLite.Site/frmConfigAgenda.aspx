<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="frmConfigAgenda.aspx.cs" Inherits="Fuji.RISLite.Site.frmConfigAgenda" %>
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
    <div>
        <div class="page-header">
            <div class="messagealert" id="alert_container"></div>
            <h1>
                Configuración de la Agenda
            </h1>
        </div><!-- /.page-header -->
        <div class="row">
            <div class="col-lg-6  col-sm-12">
                <div class="widget-box">
                    <div class="widget-header widget-header-flat widget-header-small">
						<h5 class="widget-title">
							<i class="ace-icon fa fa-id-card-o"></i>
							Configuraciones
						</h5>
                    </div>
                    <div class="widget-body">
                        <div class="widget-main">
                            <div class="row">
                                <div class="col-lg-12 col-md-12">
                                    <div class="row">
                                        <div class="col-lg-3 col-sm-4 col-xs-12">
                                            Horario de Actividad
                                        </div>
                                        <div class="col-lg-2 col-sm-4 col-xs-12">
                                            <asp:Label runat="server" Text="Hora de Inicio"></asp:Label>
                                        </div>
                                        <div class="col-lg-2 col-sm-4 col-xs-12">
                                            <div class="input-group bootstrap-timepicker timepicker">
                                                <input id="timepicker1" type="text" class="form-control input-small" style="width:100%" readonly="readonly">
                                                <span class="input-group-addon"><i class="glyphicon glyphicon-time"></i></span>
                                            </div>
                                        </div>
                                        <div class="col-lg-1 col-sm-4 col-xs-12">
                                        </div>
                                        <div class="col-lg-2 col-sm-4 col-xs-12">
                                            <asp:Label runat="server" Text="Hora Final "></asp:Label>
                                        </div>
                                        <div class="col-lg-2 col-sm-4 col-xs-12">
                                            <div class="input-group bootstrap-timepicker timepicker">
                                                <input id="timepicker2" type="text" class="form-control input-small" style="width:100%"  readonly="readonly">
                                                <span class="input-group-addon"><i class="glyphicon glyphicon-time"></i></span>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <hr />
                            <div class="row">
                                <div class="col-lg-12">
                                    <div class="col-lg-3 col-sm-3 col-xs-12">
                                        Días laborales
                                    </div>
                                    <div class="col-lg-9 col-sm-9 col-xs-12">
                                        <div id="divCheks" runat="server" class="btn-group" data-toggle="buttons">
                                            <label class='btn btn-success '>
                                                <input type='checkbox' autocomplete='off' > L 
                                                <span class='glyphicon glyphicon-ok'></span> 
                                            </label> 
                                            <label class='btn btn-primary '> 
                                                <input type='checkbox' autocomplete='off' > M 
                                                <span class='glyphicon glyphicon-ok'></span> 
                                            </label> 
                                            <label class='btn btn-info '> 
                                                 <input type='checkbox' autocomplete='off' > Mi 
                                                  <span class='glyphicon glyphicon-ok'></span> 
                                            </label> 
                                            <label class='btn btn-default '> 
                                                <input type='checkbox' autocomplete='off' > J 
                                                <span class='glyphicon glyphicon-ok'></span> 
                                            </label> 
                                            <label class='btn btn-warning '> 
                                                <input type='checkbox' autocomplete='off' > V 
                                                <span class='glyphicon glyphicon-ok'></span> 
                                            </label> 
                                            <label class='btn btn-danger '> 
                                                <input type='checkbox' autocomplete='off' > S 
                                                <span class='glyphicon glyphicon-ok'></span> 
                                            </label> 
                                            <label class='btn btn-success '> 
                                                <input type='checkbox' autocomplete='off' > D 
                                                <span class='glyphicon glyphicon-ok'></span> 
                                            </label>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <hr />
                             <div class="row">
                                <div class="col-lg-12">
                                    <div class="col-lg-3 col-sm-3 col-xs-12">
                                        Días feriados
                                    </div>
                                    <div class="col-lg-9 col-sm-9 col-xs-12">
                                        <input type="text" name="tags" id="form-field-tags" value="" placeholder="Agregar fecha ..." style="width:100%" />
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-lg-12">
                                    <div class="col-lg-9 col-sm-9 col-xs-12">
                                    </div>
                                    <div class="col-lg-3 col-sm-3 col-xs-12  input-group input-group-sm">
                                        <input type="text" id="datepicker" data-date-format="dd-mm-yyyy" style="width:100%"/>
                                        <span class="input-group-addon">
											<i class="ace-icon fa fa-calendar"></i>
										</span>
                                    </div>
                                </div>
                            </div>
                            <hr />
                            <div class="row">
                                <div class="col-lg-12">
                                    <div class="col-lg-4 col-sm-4 col-xs-12">
                                        Horas Muertas por día.
                                    </div>
                                    <div class="col-lg-8 col-sm-8 col-xs-12 text-right">
                                        <label>
											<input name="switch-field-1" class="ace ace-switch" type="checkbox" />
											<span class="lbl" data-lbl="SI&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;NO">    REPETIR</span>
										</label>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-lg-12">
                                    <div class="col-lg-3 col-sm-3 col-xs-12">
                                        
                                    </div>
                                    <div class="col-lg-9 col-sm-9 col-xs-12">
                                        <input type="text" name="tags" id="form-field-tags2" value="" placeholder="Agregar horario ..." style="width:100%" />
                                    </div>
                                </div>
                            </div>
                            <div  class="row">
                                <div class="col-lg-12">
                                    <div class="col-lg-3 col-sm-4 col-xs-12" >

                                    </div>
                                    <div class="col-lg-1 col-sm-4 col-xs-12">
                                       <asp:Label runat="server" Text="Inicio"></asp:Label>
                                    </div>
                                    <div class="col-lg-2 col-sm-4 col-xs-12">
                                        <div class="input-group bootstrap-timepicker timepicker">
                                            <input id="timepicker3" type="text" class="form-control input-small" style="width:100%" readonly="readonly">
                                            <span class="input-group-addon"><i class="glyphicon glyphicon-time"></i></span>
                                        </div>
                                    </div>
                                    <div class="col-lg-1 col-sm-4 col-xs-12">
                                    </div>
                                    <div class="col-lg-1 col-sm-4 col-xs-12">
                                        <asp:Label runat="server" Text="Final"></asp:Label>
                                    </div>
                                    <div class="col-lg-2 col-sm-4 col-xs-12">
                                        <div class="input-group bootstrap-timepicker timepicker">
                                            <input id="timepicker4" type="text" class="form-control input-small" style="width:100%" readonly="readonly">
                                            <span class="input-group-addon"><i class="glyphicon glyphicon-time"></i></span>
                                        </div>
                                    </div>
                                    <div class="col-lg-2 col-sm-4 col-xs-12">
                                        <button id="Agregar-btn" type="button" class="btn btn-info" data-loading-text="Agregando...">Agregar</button>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-lg-12">
                                    <button id="loading-btn" type="button" class="btn btn-success" data-loading-text="Guardando...">Guardar</button>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
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
