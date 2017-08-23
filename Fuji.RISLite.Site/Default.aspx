<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="Fuji.RISLite.Site.Default" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link rel="stylesheet" href="styles/kendo.common.min.css" />
    <link rel="stylesheet" href="styles/kendo.default.min.css" />
    <link rel="stylesheet" href="styles/kendo.default.mobile.min.css" />   
    <script src="js/jquery.min.js"></script>
    <script src="js/kendo.all.min.js"></script>
    <script src="js/kendo.timezones.min.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div>
        <div class="page-header">
            <div class="messagealert" id="alert_container"></div>
            <h1>
            Dashboard
            </h1>
        </div><!-- /.page-header -->
        <div class="row">
            <div class="col-xs-12">
                <div class="row">
                    <div class="col-sm-12 infobox-container">
                        Gráficas
                        <div class="infobox infobox-green">
						    <div class="infobox-icon">
							    <i class="ace-icon fa fa-comments"></i>
						    </div>

						    <div class="infobox-data">
							    <span class="infobox-data-number">32</span>
							    <div class="infobox-content">Datos 1</div>
						    </div>

						    <div class="stat stat-success">8%</div>
					    </div>

					    <div class="infobox infobox-blue">
						    <div class="infobox-icon">
							    <i class="ace-icon fa fa-twitter"></i>
						    </div>

						    <div class="infobox-data">
							    <span class="infobox-data-number">11</span>
							    <div class="infobox-content">Datos 2</div>
						    </div>

						    <div class="badge badge-success">
							    +32%
							    <i class="ace-icon fa fa-arrow-up"></i>
						    </div>
					    </div>

					    <div class="infobox infobox-pink">
						    <div class="infobox-icon">
							    <i class="ace-icon fa fa-shopping-cart"></i>
						    </div>

						    <div class="infobox-data">
							    <span class="infobox-data-number">8</span>
							    <div class="infobox-content">Datos 3</div>
						    </div>
						    <div class="stat stat-important">4%</div>
					    </div>

                        <div class="infobox infobox-red">
						    <div class="infobox-icon">
							    <i class="ace-icon fa fa-flask"></i>
						    </div>

						    <div class="infobox-data">
							    <span class="infobox-data-number">7</span>
							    <div class="infobox-content">Datos 4</div>
						    </div>
					    </div>

					    <div class="infobox infobox-orange2">
						    <div class="infobox-chart">
							    <span class="sparkline" data-values="196,128,202,177,154,94,100,170,224"></span>
						    </div>

						    <div class="infobox-data">
							    <span class="infobox-data-number">6,251</span>
							    <div class="infobox-content">Datos 5</div>
						    </div>

						    <div class="badge badge-success">
							    7.2%
							    <i class="ace-icon fa fa-arrow-up"></i>
						    </div>
					    </div>
                    </div>
                </div>
            </div>
        </div>
        <div class="row">
            <div class="col-xs-12">
                <div class="col-sm-5">
                    <div class="widget-box">
                        <div class="widget-header widget-header-flat widget-header-small">
							<h5 class="widget-title">
								<i class="ace-icon fa fa-tags"></i>
								Otras graficas
							</h5>
                        </div>
                        <div class="widget-body">
                            <div class="widget-main">
                                <div class="row">
                                    
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="col-sm-7">
					<div class="widget-box">
                        <div class="widget-header widget-header-flat widget-header-small">
							<h5 class="widget-title">
								<i class="ace-icon fa fa-tags"></i>
								Agenda
							</h5>
                        </div>
                        <div class="widget-body">
                            <div class="widget-main">
                                <div class="row">
                                    <div  id="example2">
                                        <div id="scheduler"></div>
                                        <div style="display: none;" class="box wide">
                                            <label for="language">Choose language:</label>
                                            <input id="language" value="es-MX"/>
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

    <script type="text/javascript">
            $(function () {
                $("#scheduler").kendoScheduler({
                    date: new Date("2013/6/13"),
                    startTime: new Date("2013/6/13 07:00 AM"),
                    height: 600,
                    views: [
                        "day",
                        "week",
                        { type: "month", selected: true },
                        "agenda",
                        "timeline"
                    ],
                    timezone: "Etc/UTC",
                    dataSource: {
                        batch: true,
                        transport: {
                            read: {
                                url: "https://demos.telerik.com/kendo-ui/service/meetings",
                                dataType: "jsonp"
                            },
                            update: {
                                url: "https://demos.telerik.com/kendo-ui/service/meetings/update",
                                dataType: "jsonp"
                            },
                            create: {
                                url: "https://demos.telerik.com/kendo-ui/service/meetings/create",
                                dataType: "jsonp"
                            },
                            destroy: {
                                url: "https://demos.telerik.com/kendo-ui/service/meetings/destroy",
                                dataType: "jsonp"
                            },
                            parameterMap: function (options, operation) {
                                if (operation !== "read" && options.models) {
                                    return { models: kendo.stringify(options.models) };
                                }
                            }
                        },
                        schema: {
                            model: {
                                id: "meetingID",
                                fields: {
                                    meetingID: { from: "MeetingID", type: "number" },
                                    title: { from: "Title", defaultValue: "No title", validation: { required: true } },
                                    start: { type: "date", from: "Start" },
                                    end: { type: "date", from: "End" },
                                    startTimezone: { from: "StartTimezone" },
                                    endTimezone: { from: "EndTimezone" },
                                    description: { from: "Description" },
                                    recurrenceId: { from: "RecurrenceID" },
                                    recurrenceRule: { from: "RecurrenceRule" },
                                    recurrenceException: { from: "RecurrenceException" },
                                    roomId: { from: "RoomID", nullable: true },
                                    attendees: { from: "Attendees", nullable: true },
                                    isAllDay: { type: "boolean", from: "IsAllDay" }
                                }
                            }
                        }
                    },
                    group: {
                        date: true,
                        resources: ["Rooms"]

                    },
                    resources: [
                        {
                            field: "roomId",
                            name: "Rooms",
                            dataSource: [
                                { text: "Meeting Room 101", value: 1, color: "#6eb3fa" },
                                { text: "Meeting Room 201", value: 2, color: "#f58a8a" }
                            ],
                            title: "Room"
                        },
                        {
                            field: "attendees",
                            name: "Attendees",
                            dataSource: [
                                { text: "Alex", value: 1, color: "#f8a398" },
                                { text: "Bob", value: 2, color: "#51a0ed" },
                                { text: "Charlie", value: 3, color: "#56ca85" }
                            ],
                            multiple: true,
                            title: "Attendees"
                        }
                    ]
                });

                var scheduler = $("#scheduler").data("kendoScheduler");

                $("#orientation").kendoDropDownList({
                    value: scheduler.options.group.orientation,
                    change: function () {
                        scheduler.options.group.orientation = this.value();
                        scheduler.view(scheduler.view().name);
                    }
                });
            });

            function changeLanguage() {
                kendo.ui.progress($("#scheduler"), true);
                var baseUrl = 'https://kendo.cdn.telerik.com/2017.2.621/js/messages/kendo.messages.';
                $.getScript(baseUrl + this.value() + ".min.js", function () {
                    kendo.ui.progress($("#scheduler"), false);
                    createScheduler();
                });

                document.getElementById("language").style.visibility = "hidden";
            }

            $(document).ready(function () {
                $("#language").kendoDropDownList({
                    change: changeLanguage,
                    dataTextField: "text",
                    dataValueField: "value",
                    dataSource: [
                        { text: "es-MX" },
                        { text: "bg-BG" },
                        { text: "zh-CN" },
                        { text: "en-US" }
                    ]
                });

                //$("#people :checkbox").change(function (e) {
                //    var checked = $.map($("#people :checked"), function (checkbox) {
                //        return parseInt($(checkbox).val());
                //    });

                //    var scheduler = $("#scheduler").data("kendoScheduler");

                //    scheduler.dataSource.filter({
                //        operator: function (task) {
                //            return $.inArray(task.ownerId, checked) >= 0;
                //        }
                //    });
                //});

                $("#language").data("kendoDropDownList").trigger("change");
            });

            function createScheduler() {
                var element = $("#scheduler");

                if (element.data("kendoScheduler")) {
                    element.data("kendoScheduler").destroy();
                    element.empty();
                }

                element.kendoScheduler({
                    date: new Date("2013/6/13"),
                    startTime: new Date("2013/6/13 07:00 AM"),
                    height: 600,
                    //views: [
                    //    "day",
                    //    { type: "workWeek", selected: true },
                    //    "week",
                    //    "month",
                    //    "agenda"
                    //],
                    views: [
                        //"day",
                        //{ type: "workWeek", selected: true },
                        //"week",
                        //"month",
                        "agenda"
                    ],
                    timezone: "Etc/UTC",
                    dataSource: schedulerDataSource,
                    resources: [
                        {
                            field: "ownerId",
                            title: "Owner",
                            dataSource: [
                                { text: "Alex", value: 1, color: "#f8a398" },
                                { text: "Bob", value: 2, color: "#51a0ed" },
                                { text: "Charlie", value: 3, color: "#56ca85" }
                            ]
                        }
                    ]
                });
            }

            var schedulerDataSource = new kendo.data.SchedulerDataSource({
                batch: true,
                transport: {
                    read: {
                        url: "https://demos.telerik.com/kendo-ui/service/tasks",
                        dataType: "jsonp"
                    },
                    update: {
                        url: "https://demos.telerik.com/kendo-ui/service/tasks/update",
                        dataType: "jsonp"
                    },
                    create: {
                        url: "https://demos.telerik.com/kendo-ui/service/tasks/create",
                        dataType: "jsonp"
                    },
                    destroy: {
                        url: "https://demos.telerik.com/kendo-ui/service/tasks/destroy",
                        dataType: "jsonp"
                    },
                    parameterMap: function (options, operation) {
                        if (operation !== "read" && options.models) {
                            return { models: kendo.stringify(options.models) };
                        }
                    }
                },
                schema: {
                    model: {
                        id: "taskId",
                        fields: {
                            taskId: { from: "TaskID", type: "number" },
                            title: { from: "Title", defaultValue: "No title", validation: { required: true } },
                            start: { type: "date", from: "Start" },
                            end: { type: "date", from: "End" },
                            startTimezone: { from: "StartTimezone" },
                            endTimezone: { from: "EndTimezone" },
                            description: { from: "Description" },
                            recurrenceId: { from: "RecurrenceID" },
                            recurrenceRule: { from: "RecurrenceRule" },
                            recurrenceException: { from: "RecurrenceException" },
                            ownerId: { from: "OwnerID", defaultValue: 1 },
                            isAllDay: { type: "boolean", from: "IsAllDay" }
                        }
                    }
                },
                filter: {
                    logic: "or",
                    filters: [
                        { field: "ownerId", operator: "eq", value: 1 },
                        { field: "ownerId", operator: "eq", value: 2 }
                    ]
                }
            });

            function exportExcel() {
                var url = document.getElementById('hflURL').value;
                var sucOID = document.getElementById("ddlSucurs").value;
                var datIni = $('#reportrange').data('daterangepicker').startDate;
                var datFin = $('#reportrange').data('daterangepicker').endDate;
                var jsonDataExport = JSON.stringify({
                    FechaIncio: '/Date(' + datIni + ')/',
                    FechaFin: '/Date(' + datFin + ')/',
                    sucOID: sucOID
                });
                $.ajax({
                    type: "POST",
                    url: url + "/Services/NapoleonService.svc/json/getDatosTabla",
                    data: jsonDataExport,
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: OnSuccessExport_,
                    error: OnErrorCall_
                });
            }

        </script>
</asp:Content>
