<%@ Page Title="" Language="C#" MasterPageFile="~/HMS.Master" AutoEventWireup="true" CodeFile="dashboard.aspx.cs" Inherits="HMS.dashboard" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        #ContentPlaceHolder1_patientCountType__ParentDiv {
            border-style: none!important;
            border-width: 0px!important;
            padding: 22px!important;
        }

        #ContentPlaceHolder1_BarChart1__ParentDiv {
            border-style: none!important;
            border-width: 0px!important;
        }

        #ContentPlaceHolder1_AreaChart1__ParentDiv {
            border-style: none!important;
            border-width: 0px!important;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div style="padding: 20px;">
        <div class="block-header">
            <h2>DASHBOARD</h2>
        </div>
        <div class="row clearfix">
            <asp:UpdatePanel ID="upCounter" runat="server">
                <ContentTemplate>
                    <div class="col-lg-3 col-md-3 col-sm-6 col-xs-12">
                        <div class="info-box bg-orange hover-expand-effect">
                            <div class="icon">
                                <i class="material-icons">person_add</i>
                            </div>
                            <div class="content">
                                <div class="text">
                                    Today's Reg Patients
                           
                                    <asp:HiddenField ID="datato" runat="server" />
                                    <div class="number count-to">
                                        <asp:Label ID="lblNewPatients" runat="server"></asp:Label>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="col-lg-3 col-md-3 col-sm-6 col-xs-12">
                        <div class="info-box bg-pink hover-expand-effect">
                            <div class="icon">
                                <i class="material-icons">playlist_add_check</i>
                            </div>
                            <div class="content">
                                <div class="text">Our Doctors</div>
                                <div class="number count-to">
                                    <asp:Label ID="lblOurDoctors" runat="server"></asp:Label>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="col-lg-3 col-md-3 col-sm-6 col-xs-12">
                        <div class="info-box bg-cyan hover-expand-effect">
                            <div class="icon">
                                <i class="material-icons">help</i>
                            </div>
                            <div class="content">
                                <div class="text">Today's Appointment</div>
                                <div class="number count-to">
                                    <asp:Label ID="lblTodaysAppointment" runat="server"></asp:Label>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="col-lg-3 col-md-3 col-sm-6 col-xs-12">
                        <div class="info-box bg-light-green hover-expand-effect">
                            <div class="icon">
                                <i class="material-icons">forum</i>
                            </div>
                            <div class="content">
                                <div class="text">Pending Appointments</div>
                                <div class="number count-to">
                                    <asp:Label ID="lblPendingAppointment" runat="server"></asp:Label>
                                </div>
                            </div>
                        </div>
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
        <div class="row clearfix">
            <div class="col-xs-12 col-sm-12 col-md-8 col-lg-8">
                <div class="card" style="height: 300px; overflow: auto;">
                    <div class="header">
                        <h2>Doctors Today's Schedule</h2>
                    </div>
                    <div class="body">
                        <div class="table-responsive">
                            <asp:UpdatePanel ID="up1" runat="server">
                                <ContentTemplate>
                                    <table class="table table-hover dashboard-task-infos">
                                        <thead>
                                            <tr>
                                                <th>#
                                                </th>
                                                <th>Doctor
                                                </th>
                                                <th>Timings
                                                </th>
                                                <th>Fee
                                                </th>
                                                <th>Specialty
                                                </th>
                                                <th>Status
                                                </th>
                                            </tr>
                                        </thead>
                                        <tbody>
                                            <asp:Repeater ID="rptDoctors" runat="server">
                                                <ItemTemplate>
                                                    <tr>
                                                        <td>
                                                            <asp:Label ID="lblDoctorIdx" runat="server" Text='<%# Eval("idx")%>' Visible="false"></asp:Label>
                                                            <center>
                                                    <%# Eval("sn")%>
                                                </center>
                                                        </td>
                                                        <td>
                                                            <%# Eval("doctorName")%>
                                                        </td>
                                                        <td>
                                                            <%# Eval("doctorTime")%>
                                                        </td>
                                                        <td>
                                                            <%# Eval("fee")%>
                                                        </td>
                                                        <td>
                                                            <%# Eval("specialty")%>
                                                        </td>
                                                        <td>
                                                            <span class="label bg-green"><%# Eval("DoctorStatus")%></span>
                                                        </td>
                                                    </tr>
                                                </ItemTemplate>
                                            </asp:Repeater>
                                        </tbody>
                                    </table>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </div>
                    </div>
                </div>
            </div>
            <div class="col-xs-12 col-sm-12 col-md-4 col-lg-4">
                <div class="card" style="height: 300px;">
                    <div class="body" style="padding: 0px;">
                        <asp:PieChart ID="patientCountType" runat="server" ChartHeight="200"
                            ChartWidth="250">
                            <PieChartValues>
                            </PieChartValues>
                        </asp:PieChart>
                    </div>
                </div>
            </div>
        </div>
        <%--<div class="row clearfix">
            <div class="col-xs-12 col-sm-12 col-md-12 col-lg-12">
                <div class="card">
                    <asp:BarChart ID="BarChart1" runat="server" ChartHeight="400" ChartWidth="900" ChartType="Column"
                        ChartTitle="Dieses Treatment" CategoriesAxis="21/01/2020,22/01/2020,23/01/2020,24/01/2020,25/01/2020,26/01/2020,27/01/2020"
                        ChartTitleColor="#0E426C" CategoryAxisLineColor="#D08AD9" ValueAxisLineColor="#D08AD9" BaseLineColor="#A156AB" ValueAxisLines="8" Theme="">
                        <Series>
                            <%--                            <asp:BarChartSeries Name="OPD" BarColor="#6C1E83" Data="110, 189, 255, 95, 107, 140, 150" />--%>
                        <%--</Series>--%>
                    <%--</asp:BarChart>--%>
                <%--</div>--%>
            <%--</div>--%>
        <%--</div>--%>
        <div class="row clearfix">
            <div class="col-xs-12 col-sm-12 col-md-12 col-lg-12">
                <div class="card">
                    <asp:AreaChart ID="AreaChart1" runat="server"
                        ChartHeight="400" ChartWidth="900" ChartType="Basic"
                        ChartTitle="Doctors Today's Performance"
                        ChartTitleColor="#0E426C" CategoryAxisLineColor="#D08AD9"
                        ValueAxisLineColor="#D08AD9" BaseLineColor="#A156AB">
                        <Series>
                            <%--<asp:AreaChartSeries Name="United States"
                                AreaColor="green" Data="110, 189, 255, 95, 107, 140" />
                            <asp:AreaChartSeries Name="Europe"
                                AreaColor="orange" Data="49, 77, 95, 68, 70, 79" />--%>
                        </Series>
                    </asp:AreaChart>
                </div>
            </div>
        </div>
    </div>


</asp:Content>
