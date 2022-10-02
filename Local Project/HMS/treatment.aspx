<%@ Page Title="" Language="C#" MasterPageFile="~/HMS.Master" AutoEventWireup="true" CodeFile="treatment.aspx.cs" Inherits="HMS.treatment" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
   
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="row clearfix">
        <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
            <div class="card">
                <div class="header">
                    <h2>Treatment
                            </h2>
                    <ul class="header-dropdown m-r--5">
                        <asp:UpdatePanel ID="up1" runat="server">
                            <ContentTemplate>
                                <asp:Button ID="btnNextPatient" runat="server" Text="Next Patient" CssClass="btn btn-default waves-effect pull-right" OnClick="btnNextPatient_Click" />
                                <asp:Button ID="btnSkipNextPatient" runat="server" Text="Skip & Next Patient" CssClass="btn btn-default waves-effect pull-right" OnClientClick="return confirm('Are you sure you want to skip current patient?');" OnClick="btnSkipNextPatient_Click" />
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </ul>
                </div>
                <div class="body">
                    <!-- Nav tabs -->
                    <ul class="nav nav-tabs" role="tablist">
                        <li role="presentation" class="active">
                            <a href="#home_with_icon_title" data-toggle="tab">
                                <i class="material-icons">add_to_photos</i> Treatment
                                    </a>
                        </li>
                        <li role="presentation">
                            <a href="treatmentHistory.aspx">
                                <i class="material-icons">insert_drive_file</i>Patient History
                                    </a>
                        </li>
                    </ul>

                    <!-- Tab panes -->
                    <div class="tab-content">
                        <div role="tabpanel" class="tab-pane fade in active" id="home_with_icon_title">
                            <div class="row clearfix">
                                <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
                                    <div class="card">
                                        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                            <ContentTemplate>
                                                <div class="panel-body">
                                                    <div class="col-md-6">
                                                        <span><b>Card Number:</b>&nbsp</span>
                                                        <asp:Label ID="txtCardNumber" runat="server"></asp:Label>
                                                    </div>
                                                    <div class="col-md-6">
                                                        <span><b>Patient Name:</b>&nbsp</span>
                                                        <asp:Label ID="lblPatientName" runat="server"></asp:Label>
                                                    </div>
                                                    <div class="col-md-6">
                                                        <span><b>Age:</b>&nbsp</span>
                                                        <asp:Label ID="lblAge" runat="server"></asp:Label>
                                                    </div>
                                                    <div class="col-md-6">
                                                        <span><b>Priamry Contact #:</b>&nbsp</span>
                                                        <asp:Label ID="lblContactNumber1" runat="server"></asp:Label>
                                                    </div>
                                                    <div class="col-md-6">
                                                        <span><b>Secondary Contact #:</b>&nbsp</span>
                                                        <asp:Label ID="lblContactNumber2" runat="server"></asp:Label>
                                                    </div>
                                                    <div class="col-md-12">
                                                        <hr />
                                                    </div>
                                                    <div class="col-md-6">
                                                        <span><b>Token Number:</b>&nbsp</span>
                                                        <asp:Label ID="lblTokenNumber" runat="server"></asp:Label>
                                                    </div>
                                                    <div class="col-md-6">
                                                        <span><b>Doctor:</b>&nbsp</span>
                                                        <asp:Label ID="lblDoctor" runat="server"></asp:Label>
                                                    </div>
                                                    <div class="col-md-6">
                                                        <span><b>Appointment Date:</b>&nbsp</span>
                                                        <asp:Label ID="lblAppointmentDate" runat="server"></asp:Label>
                                                    </div>
                                                </div>
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                        <div class="body">
                                            <div class="row clearfix">
                                                <div class="col-md-12">
                                                    <div class="form-group">
                                                        <ol class="breadcrumb breadcrumb-bg-grey">
                                                            <li class="active"><i class="material-icons">library_books</i> Treatment Details</li>
                                                        </ol>
                                                    </div>
                                                </div>
                                                <div class="col-md-6">
                                                    <div class="form-group form-float">
                                                        <div class="form-line">
                                                            <asp:TextBox ID="txtFever" runat="server" CssClass="form-control" required></asp:TextBox>
                                                            <label class="form-label">Fever</label>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="col-md-6">
                                                    <div class="form-group form-float">
                                                        <div class="form-line">
                                                            <asp:TextBox ID="txtBp" runat="server" CssClass="form-control" required></asp:TextBox>
                                                            <label class="form-label">B.P</label>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="col-md-6">
                                                    <div class="form-group form-float">
                                                        <div class="form-line">
                                                            <asp:TextBox ID="txtSugar" runat="server" CssClass="form-control" required></asp:TextBox>
                                                            <label class="form-label">Sugar</label>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="col-md-12">
                                                    <div class="form-group form-float">
                                                        <div class="form-line">
                                                            <asp:TextBox ID="txtOtherDaisies" runat="server" CssClass="form-control" Height="30px" TextMode="MultiLine"></asp:TextBox>
                                                            <label class="form-label">Daisies</label>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="col-md-12">
                                                    <div class="form-group form-float">
                                                        <div class="form-line">
                                                            <asp:TextBox ID="txtDiagnostic" runat="server" CssClass="form-control" Height="30px" TextMode="MultiLine"></asp:TextBox>
                                                            <label class="form-label">Other Diagnostic</label>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="col-md-12">
                                                    <div class="form-group form-float">
                                                        <ol class="breadcrumb breadcrumb-bg-grey">
                                                            <li class="active"><i class="material-icons">library_books</i> Internal Medicine</li>
                                                        </ol>
                                                    </div>
                                                </div>
                                                <div class="col-md-4">
                                                    <div class="form-group form-float">
                                                        <div class="form-line">
                                                            <asp:DropDownList ID="ddlMedicalDrugName" runat="server" CssClass="form-control show-tick" data-live-search="true"></asp:DropDownList>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="col-md-2">
                                                    <div class="form-group form-float">
                                                        <div class="form-line">
                                                            <asp:TextBox ID="txtMedicalDrugQty" runat="server" CssClass="form-control"></asp:TextBox>
                                                            <label class="form-label">Qty</label>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="col-md-2">
                                                    <div class="form-group form-float">
                                                        <div class="form-line">
                                                            <asp:DropDownList ID="ddlMedicalMedUsage" runat="server" CssClass="form-control show-tick" data-live-search="true"></asp:DropDownList>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="col-md-2">
                                                    <div class="form-group form-float">
                                                        <div class="form-line">
                                                            <asp:DropDownList ID="ddlMedicalDrugDays" runat="server" CssClass="form-control show-tick" data-live-search="true">
                                                                <asp:ListItem Value="0" Text="Days"></asp:ListItem>
                                                                <asp:ListItem Value="1" Text="1"></asp:ListItem>
                                                                <asp:ListItem Value="2" Text="2"></asp:ListItem>
                                                                <asp:ListItem Value="3" Text="3"></asp:ListItem>
                                                                <asp:ListItem Value="4" Text="4"></asp:ListItem>
                                                                <asp:ListItem Value="5" Text="5"></asp:ListItem>
                                                                <asp:ListItem Value="6" Text="6"></asp:ListItem>
                                                                <asp:ListItem Value="7" Text="7"></asp:ListItem>
                                                                <asp:ListItem Value="8" Text="8"></asp:ListItem>
                                                                <asp:ListItem Value="9" Text="9"></asp:ListItem>
                                                                <asp:ListItem Value="10" Text="10"></asp:ListItem>
                                                            </asp:DropDownList>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="col-md-2">
                                                    <div class="form-group form-float">
                                                        <asp:Button ID="btnAddMedicalDrug" runat="server" Text="Add" CssClass="btn btn-primary waves-effect" OnClick="insertInternalMedicine" UseSubmitBehavior="false" />
                                                    </div>
                                                </div>
                                                <div class="col-md-12">
                                                    <div class="form-group form-float">
                                                        <asp:Label ID="internalMedicineError" runat="server" Visible="false" style="color:red;"></asp:Label>
                                                        <asp:GridView ID="grvMedicalDrug" runat="server" CssClass="table table-bordered" AutoGenerateColumns="false" OnRowDeleting="OnRowDeletingInternlMedicine" OnRowDataBound="OnRowDataBoundInternlMedicine">
                                                            <Columns>
                                                                <asp:BoundField DataField="MedicalDrugName" HeaderText="Medicine Name" ItemStyle-Width="150" />
                                                                <asp:BoundField DataField="MedicalDrugQty" HeaderText="Qty" ItemStyle-Width="50" />
                                                                <asp:BoundField DataField="MedicalMedUsage" HeaderText="Usage Timings" ItemStyle-Width="100" />
                                                                <asp:BoundField DataField="MedicalMedDays" HeaderText="Days" ItemStyle-Width="50" />
                                                                <asp:CommandField ShowDeleteButton="True" ButtonType="Button" ItemStyle-Width="20" />
                                                            </Columns>
                                                        </asp:GridView>
                                                    </div>
                                                </div>
                                                <div class="col-md-12">
                                                    <div class="form-group">
                                                        <ol class="breadcrumb breadcrumb-bg-grey">
                                                            <li class="active"><i class="material-icons">library_books</i> Pharmacy Prescription</li>
                                                        </ol>
                                                    </div>
                                                </div>
                                                <div class="col-md-4">
                                                    <div class="form-group form-float">
                                                        <div class="form-line">
                                                            <asp:DropDownList ID="ddlPrescriptionDrugName" runat="server" CssClass="form-control show-tick" data-live-search="true"></asp:DropDownList>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="col-md-2">
                                                    <div class="form-group form-float">
                                                        <div class="form-line">
                                                            <asp:TextBox ID="txtPrescriptionDrugQty" runat="server" CssClass="form-control"></asp:TextBox>
                                                            <label class="form-label">Qty</label>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="col-md-2">
                                                    <div class="form-group form-float">
                                                        <div class="form-line">
                                                            <asp:DropDownList ID="ddlPrescriptionMedUsage" runat="server" CssClass="form-control show-tick" data-live-search="true"></asp:DropDownList>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="col-md-2">
                                                    <div class="form-group form-float">
                                                        <div class="form-line">
                                                            <asp:DropDownList ID="ddlPresMedcDays" runat="server" CssClass="form-control show-tick" data-live-search="true">
                                                                <asp:ListItem Value="0" Text="Days"></asp:ListItem>
                                                                <asp:ListItem Value="1" Text="1"></asp:ListItem>
                                                                <asp:ListItem Value="2" Text="2"></asp:ListItem>
                                                                <asp:ListItem Value="3" Text="3"></asp:ListItem>
                                                                <asp:ListItem Value="4" Text="4"></asp:ListItem>
                                                                <asp:ListItem Value="5" Text="5"></asp:ListItem>
                                                                <asp:ListItem Value="6" Text="6"></asp:ListItem>
                                                                <asp:ListItem Value="7" Text="7"></asp:ListItem>
                                                                <asp:ListItem Value="8" Text="8"></asp:ListItem>
                                                                <asp:ListItem Value="9" Text="9"></asp:ListItem>
                                                                <asp:ListItem Value="10" Text="10"></asp:ListItem>
                                                            </asp:DropDownList>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="col-md-2">
                                                    <div class="form-group form-float">
                                                        <asp:Button ID="btnAddPrescriptionDrug" runat="server" Text="Add" CssClass="btn btn-primary waves-effect" OnClick="insertPharmacyMedicine" UseSubmitBehavior="false" />
                                                    </div>
                                                </div>
                                                <div class="col-md-12">
                                                    <div class="form-group">
                                                        <asp:Label ID="pharmacyMedicineError" runat="server" Visible="false" style="color:red;"></asp:Label>
                                                        <asp:GridView ID="grvPrescriptionDrug" runat="server" CssClass="table table-bordered" AutoGenerateColumns="false" OnRowDeleting="OnRowDeletingPharmacyMedicine" OnRowDataBound="OnRowDataBoundPharmacyMedicine">
                                                            <Columns>
                                                                <asp:BoundField DataField="PrescriptionDrugName" HeaderText="Medicine Name" ItemStyle-Width="150" />
                                                                <asp:BoundField DataField="PrescriptionQty" HeaderText="Drug Qty" ItemStyle-Width="50" />
                                                                <asp:BoundField DataField="PrescriptionMedUsage" HeaderText="Usage Timings" ItemStyle-Width="100" />
                                                                <asp:BoundField DataField="PrescriptionMedDays" HeaderText="Days" ItemStyle-Width="50" />
                                                                <asp:CommandField ShowDeleteButton="True" ButtonType="Button" ItemStyle-Width="20" />
                                                            </Columns>
                                                        </asp:GridView>
                                                    </div>
                                                </div>
                                                <div class="col-md-12">
                                                    <div class="form-group form-float">
                                                        <asp:Button ID="btnSubmit" runat="server" Text="Submit" CssClass="btn btn-primary waves-effect btnSize" OnClick="btnSubmit_Click" />
                                                        <asp:Button ID="btnCancel" runat="server" Text="Clear" CssClass="btn btn-danger waves-effect btnSize" OnClick="btnCancel_Click" />
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
