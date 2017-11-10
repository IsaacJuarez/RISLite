<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="frmAgenda.aspx.cs" Inherits="Fuji.RISLite.Site.frmAgenda"  Culture="es-MX" UICulture="Auto"%>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <!-- bootstrap & fontawesome -->
	<link rel="stylesheet" href="assets/css/bootstrap.min.css" />
	<link rel="stylesheet" href="assets/font-awesome/4.5.0/css/font-awesome.min.css" />

	<!-- page specific plugin styles -->
	<link rel="stylesheet" href="assets/css/jquery-ui.custom.min.css" />
	<link rel="stylesheet" href="assets/css/fullcalendar.min.css" />

	<!-- text fonts -->
	<link rel="stylesheet" href="assets/css/fonts.googleapis.com.css" />

	<!-- ace styles -->
	<link rel="stylesheet" href="assets/css/ace.min.css" class="ace-main-stylesheet" id="main-ace-style" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="page-content">
        <div class="page-header">
		    <h1>
			    Agenda
			    <small>
				    <i class="ace-icon fa fa-angle-double-right"></i>
				    Citas
			    </small>
		    </h1>
	    </div><!-- /.page-header -->
        <div class="row">
            <div class="col-xs-3">
                <div class="widget-box">
                    <div class="widget-header widget-header-flat widget-header-small">
						<h5 class="widget-title">
							<i class="ace-icon fa fa-id-card-o"></i>
							Agregar Cita
						</h5>
                    </div>
                    <div class="widget-body">
                        <div class="widget-main">
                            <div class="row">
                                <div class="col-lg-3 col-md-3 col-sm-1 col-xs-1">

                                </div>
                                <div class="col-lg-9 col-md-9 col-sm-11 col-xs-11 form-search">
                                    <asp:AutoCompleteExtender ID="acxBusqueda" runat="server" TargetControlID="txtBusquedaPaciente" MinimumPrefixLength="3" EnableCaching="true" CompletionSetCount="1" 
                                    CompletionInterval="500" ServiceMethod="obtenerPacienteBusqueda" CompletionListCssClass="completionList" CompletionListItemCssClass="listItem"
                                    CompletionListHighlightedItemCssClass="itemHighlighted"></asp:AutoCompleteExtender>
                                    <asp:TextBox ID="txtBusquedaPaciente" runat="server" CssClass="nav-search-input" placeholder="Busquéda Paciente..." ToolTip="Búsqueda de Paciente por Nombre, Apellido, NSS, ID del paciente." Width="100%"></asp:TextBox>
                                </div>
                            </div>
                            <hr />
                            <div class="row">
                                <div class="col-lg-5 col-md-5 col-sm-12 col-xs-12">
                                    <asp:Label runat="server" Text="Nombre"></asp:Label>
                                </div>
                                <div class="col-lg-7 col-md-7 col-sm-12 col-xs-12">
                                    <asp:TextBox runat="server" Text="" ID="txtNombrePaciente" Width="100%"></asp:TextBox>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-lg-5 col-md-5 col-sm-12 col-xs-12">
                                    <asp:Label runat="server" Text="Apellidos"></asp:Label>
                                </div>
                                <div class="col-lg-7 col-md-7 col-sm-12 col-xs-12">
                                    <asp:TextBox runat="server" Text="" ID="txtApellidos" Width="100%"></asp:TextBox>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-lg-5 col-md-5 col-sm-12 col-xs-12">
                                    <asp:Label runat="server" Text="Fecha de Nacimiento"></asp:Label>
                                </div>
                                <div class="col-lg-7 col-md-7 col-sm-12 col-xs-12">
                                    <table style="width:100%">
                                        <tr>
                                            <td style="width:90%">
                                                <asp:TextBox runat="server" ID="Date1" autocomplete="off" CssClass="form-control" Width="100%" Font-Size="Small"/>
                                            </td>
                                            <td style="width:10%">
                                                <asp:ImageButton ID="imgPopup" ImageUrl="~/Images/ic_action_calendar_month.png"  Width="25px" Height="25px" ImageAlign="Bottom" runat="server" />
                                                <asp:CalendarExtender ID="customCalendarExtender" runat="server" TargetControlID="Date1" PopupButtonID="imgPopup"
                                                CssClass="cal" Format="dd/MM/yyyy" />
                                            </td>
                                        </tr>
                                    </table>
                                </div>
                            </div>
                            <hr />
                            <div class="row">
                                Adicionales
                                <div id="divCheks" runat="server" class="btn-group" data-toggle="buttons">
                                </div>
                            </div>
                            <hr />
                            <div class="row">
                                <div class="col-lg-3 col-md-3 col-sm-1 col-xs-1">

                                </div>
                                <div class="col-lg-9 col-md-9 col-sm-11 col-xs-11 form-search">
                                    <asp:AutoCompleteExtender ID="AutoCompleteExtender1" runat="server" TargetControlID="txtEstudio" MinimumPrefixLength="3" EnableCaching="true" CompletionSetCount="1" 
                                    CompletionInterval="500" ServiceMethod="obtenerEstudioBusqueda" CompletionListCssClass="completionList" CompletionListItemCssClass="listItem"
                                    CompletionListHighlightedItemCssClass="itemHighlighted"></asp:AutoCompleteExtender>
                                    <asp:TextBox ID="txtEstudio" runat="server" CssClass="nav-search-input" placeholder="Estudio..." ToolTip="Seleccionar Estudio" Width="100%"></asp:TextBox>
                                </div>
                            </div>
                            <div class="row">
                                Estudio(s):
                                <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
                                    <small>grid....</small>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-lg-3 col-md-3 col-sm-1 col-xs-1">

                                </div>
                                <div class="col-lg-9 col-md-9 col-sm-11 col-xs-11 text-right">
                                    <asp:Button ID="btnCancelPaciente" runat="server" Text="Cancelar" CssClass="btn btn-danger" />
                                    <asp:Button ID="Button1" runat="server" Text="Guardar" CssClass="btn btn-success" />
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
			<div class="col-xs-9">
                <div class="tabbable">
                    <ul class="nav nav-tabs" id="myTab">
                        <li class="active">
							<a data-toggle="tab" href="#home">
								<i class="green ace-icon fa fa-home bigger-120"></i>
								Horarios
							</a>
						</li>

						<li>
							<a data-toggle="tab" href="#messages">
								Agenda
								<span class="badge badge-danger">4</span>
							</a>
						</li>
                    </ul>
                    <div class="tab-content">
                        <div id="home" class="tab-pane fade in active">
							<p>Sugerencias</p>
						</div>

						<div id="messages" class="tab-pane fade">
							<!-- PAGE CONTENT BEGINS -->
				            <div class="row">
					            <div class="col-sm-9">
						            <div class="space"></div>

						            <div id="calendar"></div>
					            </div>

					            <div class="col-sm-3">
						            <div class="widget-box transparent">
							            <div class="widget-header">
								            <h4>Draggable events</h4>
							            </div>

							            <div class="widget-body">
								            <div class="widget-main no-padding">
									            <div id="external-events">
										            <div class="external-event label-grey" data-class="label-grey">
											            <i class="ace-icon fa fa-arrows"></i>
											            Evento 1
										            </div>

										            <div class="external-event label-success" data-class="label-success">
											            <i class="ace-icon fa fa-arrows"></i>
											            Evento 2
										            </div>

										            <div class="external-event label-danger" data-class="label-danger">
											            <i class="ace-icon fa fa-arrows"></i>
											            Evento 3
										            </div>

										            <div class="external-event label-purple" data-class="label-purple">
											            <i class="ace-icon fa fa-arrows"></i>
											            Evento 4
										            </div>

										            <div class="external-event label-yellow" data-class="label-yellow">
											            <i class="ace-icon fa fa-arrows"></i>
											            Evento 5
										            </div>

										            <div class="external-event label-pink" data-class="label-pink">
											            <i class="ace-icon fa fa-arrows"></i>
											            Evento 6
										            </div>

										            <div class="external-event label-info" data-class="label-info">
											            <i class="ace-icon fa fa-arrows"></i>
											            Evento 7
										            </div>

										            <label>
											            <input type="checkbox" class="ace ace-checkbox" id="drop-remove" />
											            <span class="lbl"> Quita despues de arrastrar</span>
										            </label>
									            </div>
								            </div>
							            </div>
						            </div>
					            </div>
				            </div>
				            <!-- PAGE CONTENT ENDS -->
						</div>
                    </div>
                </div>
				
			</div><!-- /.col -->
		</div><!-- /.row -->
	</div><!-- /.page-content -->

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

    <!-- page specific plugin scripts -->
	<script src="assets/js/jquery-ui.custom.min.js"></script>
	<script src="assets/js/jquery.ui.touch-punch.min.js"></script>
	<script src="assets/js/moment.min.js"></script>
	<script src="assets/js/fullcalendar.min.js"></script>
	<script src="assets/js/bootbox.js"></script>
    <!-- inline scripts related to this page -->
	<script type="text/javascript">
		jQuery(function($) {

        /* initialize the external events
	        -----------------------------------------------------------------*/

	        $('#external-events div.external-event').each(function() {

		        // create an Event Object (http://arshaw.com/fullcalendar/docs/event_data/Event_Object/)
		        // it doesn't need to have a start or end
		        var eventObject = {
			        title: $.trim($(this).text()) // use the element's text as the event title
		        };

		        // store the Event Object in the DOM element so we can get to it later
		        $(this).data('eventObject', eventObject);

		        // make the event draggable using jQuery UI
		        $(this).draggable({
			        zIndex: 999,
			        revert: true,      // will cause the event to go back to its
			        revertDuration: 0  //  original position after the drag
		        });
		
	        });




	        /* initialize the calendar
	        -----------------------------------------------------------------*/

	        var date = new Date();
	        var d = date.getDate();
	        var m = date.getMonth();
	        var y = date.getFullYear();


	        var calendar = $('#calendar').fullCalendar({
		        //isRTL: true,
		        //firstDay: 1,// >> change first day of week 
		
		        buttonHtml: {
			        prev: '<i class="ace-icon fa fa-chevron-left"></i>',
			        next: '<i class="ace-icon fa fa-chevron-right"></i>'
		        },
	
		        header: {
			        left: 'prev,next today',
			        center: 'title',
			        right: 'month,agendaWeek,agendaDay'
		        },
		        events: [
		            {
			        title: 'All Day Event',
			        start: new Date(y, m, 1),
			        className: 'label-important'
		            },
		            {
			        title: 'Long Event',
			        start: moment().subtract(5, 'days').format('YYYY-MM-DD'),
			        end: moment().subtract(1, 'days').format('YYYY-MM-DD'),
			        className: 'label-success'
		            },
		            {
			        title: 'Some Event',
			        start: new Date(y, m, d-3, 16, 0),
			        allDay: false,
			        className: 'label-info'
		            }
		        ]
		        ,
		
		        /**eventResize: function(event, delta, revertFunc) {

			        alert(event.title + " end is now " + event.end.format());

			        if (!confirm("is this okay?")) {
				        revertFunc();
			        }

		        },*/
		
		        editable: true,
		        droppable: true, // this allows things to be dropped onto the calendar !!!
		        drop: function(date) { // this function is called when something is dropped
		
			        // retrieve the dropped element's stored Event Object
			        var originalEventObject = $(this).data('eventObject');
			        var $extraEventClass = $(this).attr('data-class');
			
			
			        // we need to copy it, so that multiple events don't have a reference to the same object
			        var copiedEventObject = $.extend({}, originalEventObject);
			
			        // assign it the date that was reported
			        copiedEventObject.start = date;
			        copiedEventObject.allDay = false;
			        if($extraEventClass) copiedEventObject['className'] = [$extraEventClass];
			
			        // render the event on the calendar
			        // the last `true` argument determines if the event "sticks" (http://arshaw.com/fullcalendar/docs/event_rendering/renderEvent/)
			        $('#calendar').fullCalendar('renderEvent', copiedEventObject, true);
			
			        // is the "remove after drop" checkbox checked?
			        if ($('#drop-remove').is(':checked')) {
				        // if so, remove the element from the "Draggable Events" list
				        $(this).remove();
			        }
			
		        }
		        ,
		        selectable: true,
		        selectHelper: true,
		        select: function(start, end, allDay) {
			
			        bootbox.prompt("New Event Title:", function(title) {
				        if (title !== null) {
					        calendar.fullCalendar('renderEvent',
						        {
							        title: title,
							        start: start,
							        end: end,
							        allDay: allDay,
							        className: 'label-info'
						        },
						        true // make the event "stick"
					        );
				        }
			        });
			

			        calendar.fullCalendar('unselect');
		        }
		        ,
		        eventClick: function(calEvent, jsEvent, view) {

			        //display a modal
			        var modal = 
			        '<div class="modal fade">\
			            <div class="modal-dialog">\
			            <div class="modal-content">\
				            <div class="modal-body">\
				            <button type="button" class="close" data-dismiss="modal" style="margin-top:-10px;">&times;</button>\
				            <form class="no-margin">\
					            <label>Change event name &nbsp;</label>\
					            <input class="middle" autocomplete="off" type="text" value="' + calEvent.title + '" />\
					            <button type="submit" class="btn btn-sm btn-success"><i class="ace-icon fa fa-check"></i> Save</button>\
				            </form>\
				            </div>\
				            <div class="modal-footer">\
					        <button type="button" class="btn btn-sm btn-danger" data-action="delete"><i class="ace-icon fa fa-trash-o"></i> Delete Event</button>\
					        <button type="button" class="btn btn-sm" data-dismiss="modal"><i class="ace-icon fa fa-times"></i> Cancel</button>\
				            </div>\
			            </div>\
			            </div>\
			        </div>';
		
		
			        var modal = $(modal).appendTo('body');
			        modal.find('form').on('submit', function(ev){
				        ev.preventDefault();

				        calEvent.title = $(this).find("input[type=text]").val();
				        calendar.fullCalendar('updateEvent', calEvent);
				        modal.modal("hide");
			        });
			        modal.find('button[data-action=delete]').on('click', function() {
				        calendar.fullCalendar('removeEvents' , function(ev){
					        return (ev._id == calEvent._id);
				        })
				        modal.modal("hide");
			        });
			
			        modal.modal('show').on('hidden', function(){
				        modal.remove();
			        });


			        //console.log(calEvent.id);
			        //console.log(jsEvent);
			        //console.log(view);

			        // change the border color just for fun
			        //$(this).css('border-color', 'red');

		        }
		
	        });


        })
	</script>
</asp:Content>
