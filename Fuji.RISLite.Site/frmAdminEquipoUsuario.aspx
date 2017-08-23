<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="frmAdminEquipoUsuario.aspx.cs" Inherits="Fuji.RISLite.Site.frmAdminEquipoUsuario" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="assets/js/bootbox.js"></script>
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
                            <div class="row">
                                <div class="col-lg-12 text-right">
                                    <!-- Small modal -->
                                      <button type="button" class="btn btn-primary" data-toggle="modal" data-target=".bs-example-modal-sm">Agregar</button>
                                    <!-- /modals -->
                                </div>
                            </div>
                            <div class="row">
                                <div runat="server" id="tableTecnico">

                                </div>
                            </div>
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
                            <div class="row">
                                <div class="col-lg-12 text-right">
                                    <!-- Small modal -->
                                      <button type="button" class="btn btn-primary" data-toggle="modal" data-target=".bs-example-modal-sm">Agregar</button>

                                      <div class="modal fade bs-example-modal-sm" tabindex="-1" role="dialog" aria-hidden="true">
                                        <div class="modal-dialog modal-sm">
                                          <div class="modal-content">

                                            <div class="modal-header">
                                              <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">×</span>
                                              </button>
                                              <h4 class="modal-title" id="myModalLabel2">Agregar Modalidad</h4>
                                            </div>
                                            <div class="modal-body">
                                                <div class="form-horizontal" role="form">
                                                <div class="form-group">
	                                                <label class="col-sm-3 control-label no-padding-right" for="form-field-1"> Nombre del Equipo </label>

	                                                <div class="col-sm-9">
		                                                <input type="text" id="form-field-1" placeholder="Username" class="col-xs-10 col-sm-5" />
	                                                </div>
                                                </div>

                                                <div class="form-group">
	                                                <label class="col-sm-3 control-label no-padding-right" for="form-field-1-1"> Código del Equipo </label>

	                                                <div class="col-sm-9">
		                                                <input type="text" id="form-field-1-1" placeholder="Text Field" class="form-control" />
	                                                </div>
                                                </div>

                                                <div class="form-group">
                                                    <label for="form-field-select-1">Default</label>
                                                    <select class="form-control" id="form-field-select-1">
                                                        <option value="0">Seleccionar Modalidad</option>
		                                                <option value="1">RAYOS X</option>
		                                                <option value="2">TOMOGRAFIA</option>
		                                                <option value="3">HEMODIALISIS</option>
		                                                <option value="4">MASTOGRAFIA</option>
		                                                <option value="5">RESONANCIA MAGNETICA</option>
		                                                <option value="7">FLUROSCOPIA</option>
		                                                <option value="8">ULTRASONIDO</option>
		                                                <option value="9">ANGIOGRAFIA</option>
                                                    </select>
                                                </div>
                                                </div>
                                            </div>
                                            <div class="modal-footer">
                                              <button type="button" class="btn btn-default" data-dismiss="modal">Cerrar</button>
                                              <button type="button" class="btn btn-primary">Agregar</button>
                                            </div>

                                          </div>
                                        </div>
                                      </div>
                                      <!-- /modals -->
                                </div>
                            </div>
                            <div class="row">
                                <div runat="server" id="tableEquipos">

                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>

    <!-- page specific plugin scripts -->
		<script src="assets/js/jquery.dataTables.min.js"></script>
		<script src="assets/js/jquery.dataTables.bootstrap.min.js"></script>
		<script src="assets/js/dataTables.buttons.min.js"></script>
		<script src="assets/js/buttons.flash.min.js"></script>
		<script src="assets/js/buttons.html5.min.js"></script>
		<script src="assets/js/buttons.print.min.js"></script>
		<script src="assets/js/buttons.colVis.min.js"></script>
		<script src="assets/js/dataTables.select.min.js"></script>

    <!-- inline scripts related to this page -->
		<script type="text/javascript">
			jQuery(function($) {
				//initiate dataTables plugin
				var myTable = 
				$('#dynamic-table')
				//.wrap("<div class='dataTables_borderWrap' />")   //if you are applying horizontal scrolling (sScrollX)
				.DataTable( {
					bAutoWidth: false,
					"aoColumns": [
					  { "bSortable": false },
					  null, null,null, null, null,
					  { "bSortable": false }
					],
					"aaSorting": [],
					
					
					//"bProcessing": true,
			        //"bServerSide": true,
			        //"sAjaxSource": "http://127.0.0.1/table.php"	,
			
					//,
					//"sScrollY": "200px",
					//"bPaginate": false,
			
					//"sScrollX": "100%",
					//"sScrollXInner": "120%",
					//"bScrollCollapse": true,
					//Note: if you are applying horizontal scrolling (sScrollX) on a ".table-bordered"
					//you may want to wrap the table inside a "div.dataTables_borderWrap" element
			
					//"iDisplayLength": 50
			
			
					select: {
						style: 'multi'
					}
			    } );
			
				
				
				$.fn.dataTable.Buttons.defaults.dom.container.className = 'dt-buttons btn-overlap btn-group btn-overlap';
				
				new $.fn.dataTable.Buttons( myTable, {
					buttons: [
					  {
						"extend": "colvis",
						"text": "<i class='fa fa-search bigger-110 blue'></i> <span class='hidden'>Show/hide columns</span>",
						"className": "btn btn-white btn-primary btn-bold",
						columns: ':not(:first):not(:last)'
					  },
					  {
						"extend": "copy",
						"text": "<i class='fa fa-copy bigger-110 pink'></i> <span class='hidden'>Copy to clipboard</span>",
						"className": "btn btn-white btn-primary btn-bold"
					  },
					  {
						"extend": "csv",
						"text": "<i class='fa fa-database bigger-110 orange'></i> <span class='hidden'>Export to CSV</span>",
						"className": "btn btn-white btn-primary btn-bold"
					  },
					  {
						"extend": "excel",
						"text": "<i class='fa fa-file-excel-o bigger-110 green'></i> <span class='hidden'>Export to Excel</span>",
						"className": "btn btn-white btn-primary btn-bold"
					  },
					  {
						"extend": "pdf",
						"text": "<i class='fa fa-file-pdf-o bigger-110 red'></i> <span class='hidden'>Export to PDF</span>",
						"className": "btn btn-white btn-primary btn-bold"
					  },
					  {
						"extend": "print",
						"text": "<i class='fa fa-print bigger-110 grey'></i> <span class='hidden'>Print</span>",
						"className": "btn btn-white btn-primary btn-bold",
						autoPrint: false,
						message: 'This print was produced using the Print button for DataTables'
					  }		  
					]
				} );
				myTable.buttons().container().appendTo( $('.tableTools-container') );
			
			
			
			})
		</script>
</asp:Content>
