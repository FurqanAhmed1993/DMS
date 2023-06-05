<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/AdminMaster.master" AutoEventWireup="true" CodeFile="DomicileApplication_Major.aspx.cs" Inherits="DomicileApplication_Major" MaintainScrollPositionOnPostback="true" %>

<%@ Register Src="~/CustomControls/Shared/InProgress.ascx" TagPrefix="uc" TagName="InProgress" %>
<%@ Register Src="~/CustomControls/Shared/PagingAndSorting.ascx" TagPrefix="uc" TagName="PagingAndSorting" %>
<%@ Register Namespace="AjaxControlToolkit" Assembly="AjaxControlToolkit" TagPrefix="ajax" %>

<%@ Register Assembly="System.Web.DataVisualization, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
    Namespace="System.Web.UI.DataVisualization.Charting" TagPrefix="asp" %>


<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=15.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91"
    Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style>
        .mycheckBig input {
            width: 25px;
            height: 25px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">


    <div class="alert alert-danger" role="alert" runat="server" id="divAlertStep2" visible="false">
        <asp:Label runat="server" ID="lblAlertDivStep2"></asp:Label>
    </div>


    <div class="alert alert-danger" role="alert" runat="server" id="divDuplicateDocInfo" visible="false">
        <asp:Label runat="server" ID="lblDuplicateDocInfo"></asp:Label>
    </div>

    <div class="alert alert-danger" role="alert" runat="server" id="divAlertStep1" visible="false">
        <asp:Label runat="server" ID="lblAlertDivStep1"></asp:Label>
    </div>


    <asp:UpdateProgress ID="UpdateProgress2" runat="server">
        <ProgressTemplate>
            <uc:InProgress runat="server" ID="InProgress" />
        </ProgressTemplate>
    </asp:UpdateProgress>
    <div class="row heading-bg">
        <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
            <h5 class="txt-dark">
                <asp:Label ID="lblAppType" runat="server">
                </asp:Label></h5>
        </div>
    </div>

    <asp:UpdatePanel ID="upData" runat="server">

        <ContentTemplate>

            <input type="hidden" runat="server" id="IsAdd" value="0" />
            <input type="hidden" runat="server" id="IsView" value="0" />
            <input type="hidden" runat="server" id="IsEdit" value="0" />
            <input type="hidden" runat="server" id="IsDelete" value="0" />
            <asp:HiddenField runat="server" ID="hdnCNIC" Value="1" />
            <asp:HiddenField runat="server" ID="hdnApplicationId" />
            <asp:HiddenField ID="hdnApplicationType" runat="server" />
            <asp:HiddenField ID="hdnRequestType" runat="server" />
            <asp:HiddenField ID="hdnImageURL" runat="server" />
            <asp:HiddenField ID="hdnApplicationStatusId" runat="server" />

            <div class="panel panel-default card-view">
                <div class="panel-wrapper collapse in">
                    <div class="panel-body">
                        <div class="row">

                            <div class="col-md-4 col-sm-4">
                                <div class="form-group">
                                    <label class="mb-5">Title</label>
                                    <span class="MandatoryValue">* </span>
                                    <asp:RequiredFieldValidator runat="server" ErrorMessage="*" ControlToValidate="ddlTitle" InitialValue="0" ValidationGroup="Add" ForeColor="Red" />
                                    <asp:DropDownList runat="server" ID="ddlTitle" CssClass="form-control">
                                        <asp:ListItem Value="0">--Select Title--</asp:ListItem>
                                        <asp:ListItem Value="Mr">Mr.</asp:ListItem>
                                        <asp:ListItem Value="Mrs">Mrs.</asp:ListItem>
                                        <asp:ListItem Value="Ms">Ms.</asp:ListItem>
                                        <asp:ListItem Value="Miss">Miss.</asp:ListItem>
                                        <asp:ListItem Value="Dr">Dr.</asp:ListItem>

                                    </asp:DropDownList>
                                </div>
                            </div>

                            <div class="col-md-4 col-sm-4">
                                <div class="form-group">
                                    <label class="mb-5">Applicant's Name</label>
                                    <span class="MandatoryValue">* </span>
                                    <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator3" ControlToValidate="txtApplicantName" Text="*" ValidationGroup="Add" ForeColor="Red"></asp:RequiredFieldValidator>
                                    <asp:TextBox ID="txtApplicantName" runat="server" CssClass="form-control" autocomplete="off"></asp:TextBox>
                                </div>
                            </div>

                            <div class="col-md-4 col-sm-4">
                                <div class="form-group">
                                    <label class="mb-5">Father's Name</label>
                                    <span class="MandatoryValue">* </span>
                                    <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator1" ControlToValidate="txtFatherName" Text="*" ValidationGroup="Add" ForeColor="Red"></asp:RequiredFieldValidator>
                                    <asp:TextBox ID="txtFatherName" runat="server" CssClass="form-control" autocomplete="off"></asp:TextBox>
                                </div>
                            </div>
                        </div>

                        <div class="row">

                            <div class="col-md-4 col-sm-4">
                                <div class="form-group">
                                    <label class="mb-5">Applicant's NIC</label>
                                    <span class="MandatoryValue" id="spnCNIC" runat="server">* </span>
                                    <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator5" ControlToValidate="txtCnic" Text="*" ValidationGroup="Add" ForeColor="Red"></asp:RequiredFieldValidator>
                                    <asp:RegularExpressionValidator ID="RegularExpressionValidatorStreet" runat="server"
                                        ErrorMessage="Wrong NIC Format!" ValidationExpression="^[0-9+]{5}-[0-9+]{7}-[0-9]{1}$"
                                        ControlToValidate="txtCnic" ForeColor="Red" ValidationGroup="Add"></asp:RegularExpressionValidator><br />
                                    <asp:TextBox ID="txtCnic" runat="server" CssClass="form-control cnic" autocomplete="off" PlaceHolder="XXXXX-XXXXXXX-X"></asp:TextBox>

                                </div>
                            </div>

                            <div class="col-md-4 col-sm-4">
                                <div class="form-group">
                                    <label class="mb-5">Date of Birth</label>
                                    <span class="MandatoryValue">* </span>
                                    <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator2" ControlToValidate="txtDOB" Text="*" ValidationGroup="Add" ForeColor="Red"></asp:RequiredFieldValidator>
                                    <asp:RegularExpressionValidator ID="RegularExpressionValidatorDoB" runat="server"
                                        ErrorMessage="Use this Format YYYY-MM-DD" ValidationExpression="((([0-9][0-9][0-9][1-9])|([1-9][0-9][0-9][0-9])|([0-9][1-9][0-9][0-9])|([0-9][0-9][1-9][0-9]))-((0[13578])|(1[02]))-((0[1-9])|([12][0-9])|(3[01])))|((([0-9][0-9][0-9][1-9])|([1-9][0-9][0-9][0-9])|([0-9][1-9][0-9][0-9])|([0-9][0-9][1-9][0-9]))-((0[469])|11)-((0[1-9])|([12][0-9])|(30)))|(((000[48])|([0-9]0-9)|([0-9][1-9][02468][048])|([1-9][0-9][02468][048]))-02-((0[1-9])|([12][0-9])))|((([0-9][0-9][0-9][1-9])|([1-9][0-9][0-9][0-9])|([0-9][1-9][0-9][0-9])|([0-9][0-9][1-9][0-9]))-02-((0[1-9])|([1][0-9])|([2][0-8])))"
                                        ControlToValidate="txtDOB" ForeColor="Red" ValidationGroup="Add"></asp:RegularExpressionValidator><br />
                                    <asp:TextBox ID="txtDOB" runat="server" CssClass="form-control txtFromDate  date" autocomplete="off" OnTextChanged="CalculateDate_TextChanged" AutoPostBack="true" placeholder="YYYY-MM-DD" CausesValidation="true"></asp:TextBox>
                                </div>
                            </div>

                            <div class="col-md-4 col-sm-4">
                                <div class="form-group">
                                    <label class="mb-5">Place of Birth</label>
                                    <span class="MandatoryValue">* </span>
                                    <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator4" ControlToValidate="txtPlaceofBirth" Text="*" ValidationGroup="Add" ForeColor="Red"></asp:RequiredFieldValidator>
                                    <asp:TextBox ID="txtPlaceofBirth" runat="server" CssClass="form-control" autocomplete="off"></asp:TextBox>
                                </div>
                            </div>
                        </div>

                        <div class="row">
                            <div class="col-md-4 col-sm-4">
                                <div class="form-group">
                                    <label class="mb-5">Resident of</label>
                                    <span class="MandatoryValue">* </span>
                                    <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator6" ControlToValidate="txtPlaceofBirth" Text="*" ValidationGroup="Add" ForeColor="Red"></asp:RequiredFieldValidator>
                                    <asp:TextBox ID="txtResident" runat="server" CssClass="form-control" autocomplete="off"></asp:TextBox>
                                </div>
                            </div>


                            <div class="col-md-4 col-sm-4">
                                <div class="form-group">
                                    <label class="mb-5">Cast/Surname</label>
                                    <span class="MandatoryValue">* </span>
                                    <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator7" ControlToValidate="txtSurname" Text="*" ValidationGroup="Add" ForeColor="Red"></asp:RequiredFieldValidator>
                                    <asp:TextBox ID="txtSurname" runat="server" CssClass="form-control" autocomplete="off"></asp:TextBox>
                                </div>
                            </div>

                            <div class="col-md-4 col-sm-4" id="divDelivery" runat="server" visible="false">
                                <div class="form-group">
                                    <label class="mb-5">Delivery Date</label>
                                    <asp:TextBox ID="txtDeliveryDate" runat="server" CssClass="form-control txtFromDate  date" autocomplete="off" OnTextChanged="CalculateDate_TextChanged" AutoPostBack="true"></asp:TextBox>
                                </div>
                            </div>

                        </div>

                    </div>
                </div>
            </div>

            <div class="row heading-bg">
                <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
                    <h5 class="txt-dark">Educational Qualifications
                    </h5>
                </div>
            </div>
            <div class="panel panel-default card-view">
                <div class="panel-wrapper collapse in">
                    <div class="panel-body">

                        <div class="row">

                            <div class="col-md-4 col-sm-4">
                                <fieldset style="padding: 10px; border: 2px solid black;">
                                    <legend style="width: auto; font-weight: bold">Primary Education</legend>
                                    <div class="row">
                                        <div class="col-md-12 col-sm-12">
                                            <%--    <h4 style="text-align: center">Primary Education</h4>--%>
                                            <div class="form-group">
                                                <label class="mb-5">Primary School</label>

                                                <asp:DropDownList runat="server" ID="ddlPrimarySchool" CssClass="form-control">
                                                    <asp:ListItem Value="">--Select Primary School--</asp:ListItem>
                                                    <asp:ListItem Value="Primary School">Primary School</asp:ListItem>
                                                    <asp:ListItem Value="N.A">N.A</asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-md-12 col-sm-12">
                                            <div class="form-group">
                                                <label class="mb-5">Town Or Village</label>
                                                <asp:TextBox ID="txtTownVillagePrimary" runat="server" CssClass="form-control" autocomplete="off"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>

                                    <div class="row">
                                        <div class="col-md-12 col-sm-12">
                                            <div class="form-group">
                                                <label class="mb-5">From Date</label>
                                                <asp:TextBox ID="txtFromDatePrimary" runat="server" CssClass="form-control monthPicker" autocomplete="off"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>

                                    <div class="row">
                                        <div class="col-md-12 col-sm-12">
                                            <div class="form-group">
                                                <label class="mb-5">To Date</label>
                                                <asp:TextBox ID="txtToDatePrimary" runat="server" CssClass="form-control monthPicker" autocomplete="off"></asp:TextBox>
                                                <asp:CompareValidator ID="CompareValidator1" ValidationGroup="Add" ForeColor="Red" runat="server"
                                                    ControlToValidate="txtToDatePrimary" ControlToCompare="txtFromDatePrimary" Operator="GreaterThan" Type="Integer"
                                                    ErrorMessage="To date must be greater than From date."></asp:CompareValidator>
                                            </div>
                                        </div>
                                    </div>

                                </fieldset>
                            </div>


                            <div class="col-md-4 col-sm-4">
                                <fieldset style="padding: 10px; border: 2px solid black;">
                                    <legend style="width: auto; font-weight: bold">Middle Education</legend>

                                    <div class="row">
                                        <div class="col-md-12 col-sm-12">
                                            <%--<h4 style="text-align: center">Middle Education</h4>--%>
                                            <div class="form-group">
                                                <label class="mb-5">Middle School</label>

                                                <asp:DropDownList runat="server" ID="ddlMiddleSchool" CssClass="form-control">
                                                    <asp:ListItem Value="">--Select Middle School--</asp:ListItem>
                                                    <asp:ListItem Value="Middle School">Middle School</asp:ListItem>
                                                    <asp:ListItem Value="N.A">N.A</asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-md-12 col-sm-12">
                                            <div class="form-group">
                                                <label class="mb-5">Town Or Village</label>
                                                <asp:TextBox ID="txtTownVillageMiddle" runat="server" CssClass="form-control" autocomplete="off"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>

                                    <div class="row">
                                        <div class="col-md-12 col-sm-12">
                                            <div class="form-group">
                                                <label class="mb-5">From Date</label>
                                                <asp:TextBox ID="txtFromDateMiddle" runat="server" CssClass="form-control monthPicker" autocomplete="off"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-md-12 col-sm-12">
                                            <div class="form-group">
                                                <label class="mb-5">To Date</label>
                                                <asp:TextBox ID="txtToDateMiddle" runat="server" CssClass="form-control monthPicker" autocomplete="off"></asp:TextBox>
                                                <asp:CompareValidator ID="CompareValidator2" ValidationGroup="Add" ForeColor="Red" runat="server"
                                                    ControlToValidate="txtToDateMiddle" ControlToCompare="txtFromDateMiddle" Operator="GreaterThan" Type="Integer"
                                                    ErrorMessage="To date must be greater than From date."></asp:CompareValidator>
                                            </div>
                                        </div>
                                    </div>

                                    <%--          <div class="row">
                                    <div class="col-md-12 col-sm-12" style="text-align: center;">
                                        <div class="form-group">
                                            <asp:Button ID="btnSaveMiddle" CssClass="btn btn-primary" runat="server" Text="Save" />
                                        </div>
                                    </div>
                                </div>--%>
                                </fieldset>
                            </div>

                            <div class="col-md-4 col-sm-4">

                                <fieldset style="padding: 10px; border: 2px solid black;">
                                    <legend style="width: auto; font-weight: bold">High Education</legend>
                                    <div class="row">
                                        <div class="col-md-12 col-sm-12">
                                            <%--<h4 style="text-align: center">High Education</h4>--%>
                                            <div class="form-group">
                                                <label class="mb-5">High School</label>

                                                <asp:DropDownList runat="server" ID="ddlHighSchool" CssClass="form-control" AutoPostBack="true">
                                                    <asp:ListItem Value="">--Select High School--</asp:ListItem>
                                                    <asp:ListItem Value="High School">High School</asp:ListItem>
                                                    <asp:ListItem Value="N.A">N.A</asp:ListItem>

                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                    </div>

                                    <div class="row">
                                        <div class="col-md-12 col-sm-12">
                                            <div class="form-group">
                                                <label class="mb-5">Town Or Village</label>
                                                <asp:TextBox ID="txtTownVillageHigh" runat="server" CssClass="form-control" autocomplete="off"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>


                                    <div class="row">
                                        <div class="col-md-12 col-sm-12">
                                            <div class="form-group">
                                                <label class="mb-5">From Date</label>
                                                <asp:TextBox ID="txtFromDateHigh" runat="server" CssClass="form-control monthPicker" autocomplete="off"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-md-12 col-sm-12">
                                            <div class="form-group">
                                                <label class="mb-5">To Date</label>
                                                <asp:TextBox ID="txtToDateHigh" runat="server" CssClass="form-control monthPicker" autocomplete="off"></asp:TextBox>
                                                <asp:CompareValidator ID="CompareValidator3" ValidationGroup="Add" ForeColor="Red" runat="server"
                                                    ControlToValidate="txtToDateHigh" ControlToCompare="txtFromDateHigh" Operator="GreaterThan" Type="Integer"
                                                    ErrorMessage="To date must be greater than From date."></asp:CompareValidator>
                                            </div>
                                        </div>
                                    </div>

                                    <%--                                <div class="row">
                                    <div class="col-md-12 col-sm-12" style="text-align: center;">
                                        <div class="form-group">
                                            <asp:Button ID="btnSaveHigh" CssClass="btn btn-primary" runat="server" Text="Save" />
                                        </div>
                                    </div>
                                </div>--%>
                                </fieldset>
                            </div>
                        </div>
                        <br />

                        <div class="row">
                            <div class="col-md-4 col-sm-4">
                                <div class="form-group">
                                    <label class="mb-5">District of Education</label>
                                    <asp:TextBox ID="txtDistrictEducation" runat="server" CssClass="form-control" autocomplete="off"></asp:TextBox>
                                </div>
                            </div>

                            <div class="col-md-4 col-sm-4">
                                <div class="form-group">
                                    <label class="mb-5">Date of Submission</label>
                                    <span class="MandatoryValue">* </span>
                                    <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator9" ControlToValidate="txtDateSubmission" Text="*" ValidationGroup="Add" ForeColor="Red"></asp:RequiredFieldValidator>
                                    <asp:TextBox ID="txtDateSubmission" runat="server" CssClass="form-control txtFromDate  date" autocomplete="off" Enabled="false"></asp:TextBox>
                                </div>
                            </div>

                            <div class="col-md-4 col-sm-4">
                                <div class="form-group">
                                    <label class="mb-5">Date of issue</label>
                                    <span class="MandatoryValue">* </span>
                                    <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator8" ControlToValidate="txtIssueDate" Text="*" ValidationGroup="Add" ForeColor="Red"></asp:RequiredFieldValidator>
                                    <asp:TextBox ID="txtIssueDate" runat="server" CssClass="form-control txtFromDate  date" autocomplete="off" Enabled="false"></asp:TextBox>
                                </div>
                            </div>

                        </div>

                    </div>
                </div>
            </div>

            <div class="row heading-bg">
                <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
                    <h5 class="txt-dark">Immovable Properties
                    </h5>
                </div>
            </div>

            <div class="panel panel-default card-view">
                <div class="panel-wrapper collapse in">
                    <div class="panel-body">

                        <div class="row">
                            <div class="col-md-4 col-sm-4">
                                <div class="form-group">
                                    <label class="mb-5">Particulars of Property</label>
                                    <asp:TextBox ID="txtParticularProperty" runat="server" CssClass="form-control" autocomplete="off"></asp:TextBox>
                                </div>
                            </div>

                            <div class="col-md-4 col-sm-4">
                                <div class="form-group">
                                    <label class="mb-5">Location</label>
                                    <asp:TextBox ID="txtLocation" runat="server" CssClass="form-control" autocomplete="off"></asp:TextBox>
                                </div>
                            </div>

                        </div>


                    </div>
                </div>
            </div>



            <div class="row heading-bg">
                <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
                    <h5 class="txt-dark">Other Information
                    </h5>
                </div>
            </div>

            <div class="panel panel-default card-view">
                <div class="panel-wrapper collapse in">
                    <div class="panel-body">

                        <div class="row">
                            <div class="col-md-4 col-sm-4">
                                <div class="form-group">
                                    <label class="mb-5">Taluka</label><span class="MandatoryValue">* </span>
                                    <asp:RequiredFieldValidator runat="server" ErrorMessage="*" ControlToValidate="ddlTaluka" InitialValue="0" ValidationGroup="Add" ForeColor="Red" />
                                    <asp:DropDownList runat="server" ID="ddlTaluka" CssClass="form-control" OnSelectedIndexChanged="ddlTaluka_Changed" AutoPostBack="true"></asp:DropDownList>
                                </div>
                            </div>

                            <div class="col-md-4 col-sm-4">
                                <div class="form-group">
                                    <label class="mb-5">District</label>
                                    <span class="MandatoryValue">* </span>
                                    <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator10" ControlToValidate="txtDistrict" Text="*" ValidationGroup="Add" ForeColor="Red"></asp:RequiredFieldValidator>
                                    <asp:TextBox ID="txtDistrict" runat="server" CssClass="form-control" autocomplete="off" Enabled="false" Text="KHAIRPUR"></asp:TextBox>
                                </div>
                            </div>
                        </div>

                        <div class="row">

                            <div class="col-md-4 col-sm-4">
                                <div class="form-group">
                                    <label class="mb-5">S.No of Electoral Area</label>
                                    <%--<span class="MandatoryValue">* </span>--%>
                                    <%--<asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator11" ControlToValidate="txtSnoElectoralArea" Text="*" ValidationGroup="Add" ForeColor="Red"></asp:RequiredFieldValidator>--%>
                                    <asp:TextBox ID="txtSnoElectoralArea" runat="server" CssClass="form-control" autocomplete="off"></asp:TextBox>
                                </div>
                            </div>

                            <div class="col-md-4 col-sm-4">
                                <div class="form-group">
                                    <label class="mb-5">Name of Electoral Area Taluka</label>
                                    <%-- <span class="MandatoryValue">* </span>
                                    <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator12" ControlToValidate="txtAreaTaluka" Text="*" ValidationGroup="Add" ForeColor="Red"></asp:RequiredFieldValidator>--%>
                                    <asp:TextBox ID="txtAreaTaluka" runat="server" CssClass="form-control" autocomplete="off" Enabled="false"></asp:TextBox>
                                </div>
                            </div>

                            <div class="col-md-4 col-sm-4">
                                <div class="form-group">
                                    <label class="mb-5">Name of Electoral Area Deh</label><%--<span class="MandatoryValue">* </span>--%>
                                    <%--<asp:RequiredFieldValidator runat="server" Text="*" ControlToValidate="ddlDeh" InitialValue="0" ValidationGroup="Add" ForeColor="Red" />--%>
                                    <asp:DropDownList runat="server" ID="ddlDeh" CssClass="form-control"></asp:DropDownList>
                                </div>
                            </div>

                        </div>



                        <div class="row">

                            <div class="col-md-4 col-sm-4">
                                <div class="form-group">
                                    <label class="mb-5">Guardian's N.I.C No. </label>

                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server"
                                        ErrorMessage="Wrong NIC Format!" ValidationExpression="^[0-9+]{5}-[0-9+]{7}-[0-9]{1}$"
                                        ControlToValidate="txtGuardiansCnic" ForeColor="Red" ValidationGroup="Add"></asp:RegularExpressionValidator><br />
                                    <asp:TextBox ID="txtGuardiansCnic" runat="server" CssClass="form-control cnic" autocomplete="off" PlaceHolder="XXXXX-XXXXXXX-X"></asp:TextBox>

                                </div>
                            </div>

                            <div class="col-md-4 col-sm-4">
                                <div class="form-group">
                                    <label class="mb-5">Guardian's Relationship</label>
                                    <asp:DropDownList runat="server" ID="ddlGuardianRelationShip" CssClass="form-control"></asp:DropDownList>
                                </div>
                            </div>

                            <div class="col-md-4 col-sm-4">
                                <div class="form-group">
                                    <label class="mb-5">Applicant's Phone No.</label>
                                    <asp:TextBox ID="txtPhoneNo" runat="server" CssClass="form-control integers" autocomplete="off" MaxLength="11"></asp:TextBox>
                                </div>
                            </div>

                        </div>

                        <div class="row">

                            <div class="col-md-12 col-sm-12">
                                <div class="form-group">
                                    <label class="mb-5">Temporary Address</label>
                                    <asp:TextBox ID="txtTempAddress" runat="server" CssClass="form-control" autocomplete="off"></asp:TextBox>
                                </div>
                            </div>

                            <div class="col-md-12 col-sm-12">
                                <div class="form-group">
                                    <label class="mb-5">Permanent Address</label>
                                    <span class="MandatoryValue">* </span>
                                    <asp:RequiredFieldValidator runat="server" ErrorMessage="*" ControlToValidate="txtPermanentAddress" ValidationGroup="Add" ForeColor="Red" />
                                    <asp:TextBox ID="txtPermanentAddress" runat="server" CssClass="form-control" autocomplete="off"></asp:TextBox>
                                </div>
                            </div>
                        </div>

                        <div class="row">

                            <div class="col-md-4 col-sm-4">
                                <div class="form-group">
                                    <label class="mb-5">Guardian Domicile Certificate Date</label>
                                    <asp:TextBox ID="txtGuardianDomicileDate" runat="server" CssClass="form-control txtFromDate  date" autocomplete="off" placeholder="YYYY-MM-DD"></asp:TextBox>
                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server"
                                        ErrorMessage="Use this Format YYYY-MM-DD" ValidationExpression="((([0-9][0-9][0-9][1-9])|([1-9][0-9][0-9][0-9])|([0-9][1-9][0-9][0-9])|([0-9][0-9][1-9][0-9]))-((0[13578])|(1[02]))-((0[1-9])|([12][0-9])|(3[01])))|((([0-9][0-9][0-9][1-9])|([1-9][0-9][0-9][0-9])|([0-9][1-9][0-9][0-9])|([0-9][0-9][1-9][0-9]))-((0[469])|11)-((0[1-9])|([12][0-9])|(30)))|(((000[48])|([0-9]0-9)|([0-9][1-9][02468][048])|([1-9][0-9][02468][048]))-02-((0[1-9])|([12][0-9])))|((([0-9][0-9][0-9][1-9])|([1-9][0-9][0-9][0-9])|([0-9][1-9][0-9][0-9])|([0-9][0-9][1-9][0-9]))-02-((0[1-9])|([1][0-9])|([2][0-8])))"
                                        ControlToValidate="txtGuardianDomicileDate" ForeColor="Red" ValidationGroup="Add"></asp:RegularExpressionValidator>
                                </div>
                            </div>

                            <div class="col-md-4 col-sm-4">
                                <div class="form-group">
                                    <label class="mb-5">Guardian's Relationship 2</label>
                                    <asp:DropDownList runat="server" ID="ddlGuardian2" CssClass="form-control"></asp:DropDownList>
                                </div>
                            </div>

                            <div class="col-md-4 col-sm-4">
                                <div class="form-group">
                                    <label class="mb-5">Applicant's Age</label>
                                    <span class="MandatoryValue">* </span>
                                    <asp:RequiredFieldValidator runat="server" ErrorMessage="*" ControlToValidate="txtApplicantAge" ValidationGroup="Add" ForeColor="Red" />
                                    <asp:TextBox ID="txtApplicantAge" runat="server" CssClass="form-control" autocomplete="off" Enabled="false"></asp:TextBox>
                                </div>
                            </div>

                        </div>



                        <div class="row">

                            <div class="col-md-3 col-sm-3">
                                <div class="form-group">
                                    <label class="mb-5">Trade Occupation</label><span class="MandatoryValue">* </span>
                                    <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator13" ControlToValidate="txtTradeOccupation" Text="*" ValidationGroup="Add" ForeColor="Red"></asp:RequiredFieldValidator>
                                    <asp:TextBox ID="txtTradeOccupation" runat="server" CssClass="form-control" autocomplete="off"></asp:TextBox>
                                </div>
                            </div>

                            <div class="col-md-3 col-sm-3">
                                <div class="form-group">
                                    <label class="mb-5">Marks of Identification</label><span class="MandatoryValue">* </span>
                                    <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator15" ControlToValidate="txtMarkIdentification" Text="*" ValidationGroup="Add" ForeColor="Red"></asp:RequiredFieldValidator>
                                    <asp:TextBox ID="txtMarkIdentification" runat="server" CssClass="form-control" autocomplete="off"></asp:TextBox>
                                </div>
                            </div>

                            <div class="col-md-3 col-sm-3">
                                <div class="form-group">
                                    <label class="mb-5">Date of Arrival to Place of DomicIle</label>
                                    <%--<span class="MandatoryValue">* </span>--%>
                                    <%-- <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator14" ControlToValidate="txtDateofArrival" Text="*" ValidationGroup="Add" ForeColor="Red"></asp:RequiredFieldValidator>--%>
                                    <asp:TextBox ID="txtDateofArrival" runat="server" CssClass="form-control txtFromDate  date" autocomplete="off" placeholder="YYYY-MM-DD"></asp:TextBox>
                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator3" runat="server"
                                        ErrorMessage="Use this Format YYYY-MM-DD" ValidationExpression="((([0-9][0-9][0-9][1-9])|([1-9][0-9][0-9][0-9])|([0-9][1-9][0-9][0-9])|([0-9][0-9][1-9][0-9]))-((0[13578])|(1[02]))-((0[1-9])|([12][0-9])|(3[01])))|((([0-9][0-9][0-9][1-9])|([1-9][0-9][0-9][0-9])|([0-9][1-9][0-9][0-9])|([0-9][0-9][1-9][0-9]))-((0[469])|11)-((0[1-9])|([12][0-9])|(30)))|(((000[48])|([0-9]0-9)|([0-9][1-9][02468][048])|([1-9][0-9][02468][048]))-02-((0[1-9])|([12][0-9])))|((([0-9][0-9][0-9][1-9])|([1-9][0-9][0-9][0-9])|([0-9][1-9][0-9][0-9])|([0-9][0-9][1-9][0-9]))-02-((0[1-9])|([1][0-9])|([2][0-8])))"
                                        ControlToValidate="txtDateofArrival" ForeColor="Red" ValidationGroup="Add"></asp:RegularExpressionValidator>
                                </div>
                            </div>

                            <div class="col-md-3 col-sm-3">
                                <div class="form-group">
                                    <label class="mb-5">By Birth</label>
                                    <br />
                                    <asp:CheckBox ID="ChkByBirth" runat="server" CssClass="mycheckBig" />
                                </div>
                            </div>

                        </div>

                        <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                            <ContentTemplate>


                                <div class="row" id="divApplicantPhoto">
                                    <div class="col-md-4 col-sm-4">
                                        <div class="form-group">
                                            <label class="mb-5">Applicant's Photograph</label>
                                            <%--<span class="MandatoryValue">* </span>
                                            <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator18" ControlToValidate="FileUpload1" Text="*" ValidationGroup="Add" ForeColor="Red"></asp:RequiredFieldValidator>--%>
                                            <span class="MandatoryValue" style="font-size: 12px">(Only jpeg , jpg , png images allowed)</span>
                                            <asp:FileUpload ID="FileUpload1" runat="server" />
                                            <br />
                                            <asp:Label ID="lblMessage" runat="server" ForeColor="red"
                                                Visible="false" />
                                            <asp:Button ID="btnUpload" Text="Upload" runat="server" OnClick="Upload" Style="display: none" />

                                            <div runat="server" id="divPic" visible="false">
                                                <asp:Image ID="Image1" runat="server" Height="150" Width="150" />
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </ContentTemplate>
                            <Triggers>
                                <asp:PostBackTrigger ControlID="btnUpload" />
                            </Triggers>
                        </asp:UpdatePanel>

                        <div class="row">

                            <div class="col-md-4 col-sm-4">
                                <div class="form-group">
                                    <label class="mb-5">Address in Foreign Country</label>
                                    <asp:TextBox ID="txtForeignAddress" runat="server" CssClass="form-control" autocomplete="off"></asp:TextBox>
                                </div>
                            </div>

                            <div class="col-md-4 col-sm-4">
                                <div class="form-group">
                                    <label class="mb-5">The Mukhtiarkar(Rev) of Taluka</label><span class="MandatoryValue">* </span>
                                    <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator16" ControlToValidate="txtMukhtiarkar" Text="*" ValidationGroup="Add" ForeColor="Red"></asp:RequiredFieldValidator>
                                    <asp:TextBox ID="txtMukhtiarkar" runat="server" CssClass="form-control" autocomplete="off" Enabled="false"></asp:TextBox>
                                </div>
                            </div>

                            <div class="col-md-4 col-sm-4">
                                <div class="form-group">
                                    <label class="mb-5">The Deputy District Officer (Revenue) of Taluka</label><span class="MandatoryValue">* </span>
                                    <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator17" ControlToValidate="txtDistrictOfficerTaluka" Text="*" ValidationGroup="Add" ForeColor="Red"></asp:RequiredFieldValidator>
                                    <asp:TextBox ID="txtDistrictOfficerTaluka" runat="server" CssClass="form-control" autocomplete="off" Enabled="false"></asp:TextBox>
                                </div>
                            </div>


                        </div>

                        <div class="row">

                            <div class="col-md-12 col-sm-12">
                                <div class="form-group">
                                    <label class="mb-5">Marital Status</label><span class="MandatoryValue">* </span>
                                    <asp:RequiredFieldValidator runat="server" ID="RFV123" ValidationGroup="Add" ControlToValidate="rbdMaritialStatus" ForeColor="Red" Text="*" />
                                    <asp:RadioButtonList ID="rbdMaritialStatus" runat="server" CssClass="radioButtonList" RepeatDirection="Horizontal" CellPadding="3" CellSpacing="2" OnSelectedIndexChanged="rbdMaritial_CheckedChanged" AutoPostBack="true">
                                        <asp:ListItem Value="1">Single</asp:ListItem>
                                        <asp:ListItem Value="2">Married</asp:ListItem>
                                        <asp:ListItem Value="3">Widow</asp:ListItem>
                                        <asp:ListItem Value="4">Divorced</asp:ListItem>
                                    </asp:RadioButtonList>
                                </div>
                            </div>

                        </div>

                        <div id="divChild" runat="server" visible="false">

                            <div class="row">
                                <div class="col-md-4 col-sm-4">
                                    <div class="form-group">
                                        <label class="mb-5">Name of Wife / Husband</label>
                                        <asp:TextBox ID="txtWifeHusband" runat="server" CssClass="form-control" autocomplete="off"></asp:TextBox>
                                    </div>
                                </div>
                            </div>

                            <div class="row">

                                <div class="col-md-4 col-sm-4">
                                    <div class="form-group">
                                        <label class="mb-5">Name of Child</label>
                                        <asp:TextBox ID="txtChildName" runat="server" CssClass="form-control" autocomplete="off"></asp:TextBox>
                                    </div>
                                </div>

                                <div class="col-md-4 col-sm-4">
                                    <div class="form-group">
                                        <label class="mb-5">Child's DOB</label>
                                        <span class="MandatoryValue" style="font-size: 12px">(Use Date Format: YYYY-MM-DD)</span>
                                        <asp:TextBox ID="txtChildDob" runat="server" CssClass="form-control txtFromDate  date" autocomplete="off" placeholder="YYYY-MM-DD"></asp:TextBox>
                                        <asp:Label ID="lblChildError" Style="color: red" runat="server"></asp:Label>
                                    </div>
                                </div>

                                <div class="button_panel" id="btn">
                                    <asp:Button ID="btnAddMoreOption" runat="server" CssClass="btn btn-primary btnSearch" Text="+ Add More"
                                        CausesValidation="false" OnClick="btnAddMoreOption_Click" />
                                </div>

                            </div>


                            <table style="width: 100%;">

                                <asp:Repeater ID="rptChild" runat="server" OnItemCommand="rptchild_ItemCommand">
                                    <ItemTemplate>
                                        <tr>
                                            <td>
                                                <asp:HiddenField ID="hdnRow" runat="server" Value='<%# Eval("RowNo") %>' />
                                                <div class="row">
                                                    <div class="col-md-4 col-sm-4">
                                                        <div class="form-group">
                                                            <label class="mb-5">Name of Child</label>
                                                            <asp:TextBox ID="txtChild_New" runat="server" MaxLength="50"
                                                                CssClass="form-control" autocomplete="off" Text='<%# Eval("txtChild_New") %>'></asp:TextBox>
                                                        </div>
                                                    </div>
                                                    <div class="col-md-4 col-sm-4">
                                                        <div class="form-group">
                                                            <label class="mb-5">Child's DOB</label>
                                                            <span class="MandatoryValue" style="font-size: 12px">(Use Date Format: YYYY-MM-DD)</span>
                                                            <asp:TextBox ID="txtChildDob_New" runat="server" CssClass="form-control txtFromDate  date" autocomplete="off" Text='<%# Eval("txtChildDob_New") %>' placeholder="YYYY-MM-DD"></asp:TextBox>
                                                            <asp:Label ID="lblChildErrorGrid" Style="color: red" runat="server"></asp:Label>
                                                        </div>
                                                    </div>
                                                    <div class="col-md-2 col-sm-2">
                                                        <div class="form-group">
                                                            <asp:ImageButton ID="imgBtnDelete" runat="server"
                                                                ImageUrl="~/Assets/Images/delete-icon.png" CommandName="Delete" CommandArgument='<%# Container.ItemIndex %>' />
                                                        </div>
                                                    </div>

                                                </div>
                                            </td>


                                        </tr>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                    </FooterTemplate>
                                </asp:Repeater>
                            </table>

                        </div>


             

                    <div class="panel panel-default border-panel card-view" id="divDomicileHistory" runat="server" visible="false">
                        <div class="panel-wrapper collapse in">
                            <div class="panel-body">
                                <div class="table-wrap">
                                    <div class="table-responsive">
                                        <h4>Domicile History</h4>
                                        <table class="table table-striped display pb-30">
                                            <thead>
                                                <tr>

                                                    <th>Application RefNo </th>
                                                    <th>Domicile Number </th>
                                                    <th>Request Type</th>
                                                    <th>Duplicate Documents Issued</th>
                                                    <th>Status</th>
                                                    <th>Issue Date</th>
                                                </tr>
                                            </thead>
                                            <tbody>
                                                <asp:Repeater ID="rptDomicileHistory" runat="server">
                                                    <ItemTemplate>
                                                        <tr>
                                                            <td>
                                                                <%# Eval("ApplicationRefNo") %>
                                                            </td>
                                                            <td>
                                                                <%# Eval("DomicileNo") %>
                                                            </td>
                                                            <td>
                                                                <%# Eval("RequestType") %>
                                                            </td>
                                                            <td>
                                                                <%# Eval("Duplicate_Document") %>
                                                            </td>
                                                            <td>
                                                                <%# Eval("StatusName") %>
                                                            </td>
                                                            <td>
                                                                <%# Eval("ApprovedDate") %>
                                                            </td>
                                                        </tr>
                                                    </ItemTemplate>
                                                </asp:Repeater>
                                            </tbody>
                                        </table>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>



                    <%-- Modal Start--%>
                    <input type="button" data-toggle="modal" data-target="#AddEditModal" class="openmodal" style="display: none;" />
                    <div class="modal fade in inmodal " id="AddEditModal" tabindex="-1" role="dialog" aria-hidden="true" data-backdrop="static">
                        <div class="modal-dialog">
                            <div class="modal-content animated">
                                <div class="modal-header" style="padding-bottom: 9px; padding-top: 9px; text-align: center">
                                    <h5 class="modal-title">NIC already exists in the system. Press Ok if you want to Continue Anyway.</h5>
                                </div>
                                <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                                    <ContentTemplate>
                                        <div class="modal-body">
                                            <input type="hidden" id="hfId" runat="server" class="hfId" value="0" />
                                            <div class="row">
                                                <div class="col-md-12 col-sm-12 col-lg-12">
                                                    <div class="form-group">

                                                        <table>
                                                            <tr>
                                                                <td>
                                                                    <label class="mb-5">ARN Number:</label>
                                                                </td>
                                                                <td>
                                                                    <label class="mb-5">
                                                                        <asp:Label ID="lblARNNumber" runat="server"></asp:Label></label>
                                                                </td>
                                                            </tr>

                                                            <tr>
                                                                <td style="padding-right: 15px;">
                                                                    <label class="mb-5">Domicile Number:</label>
                                                                </td>
                                                                <td>
                                                                    <label class="mb-5">
                                                                        <asp:Label ID="lblDomicileNumber" runat="server"></asp:Label></label>
                                                                </td>
                                                            </tr>

                                                            <tr>
                                                                <td>
                                                                    <label class="mb-5">Name:</label>
                                                                </td>
                                                                <td>
                                                                    <label class="mb-5">
                                                                        <asp:Label ID="lblName" runat="server"></asp:Label></label>
                                                                </td>
                                                            </tr>

                                                            <tr>
                                                                <td>
                                                                    <label class="mb-5">Father Name:</label>
                                                                </td>
                                                                <td>
                                                                    <label class="mb-5">
                                                                        <asp:Label ID="lblFatherName" runat="server"></asp:Label>
                                                                </td>
                                                            </tr>

                                                        </table>
                                                    </div>
                                                </div>
                                            </div>

                                        </div>
                                        <div class="modal-footer">
                                            <asp:Button Text="Ok" CssClass="btn btn-primary" ID="btnAdd" ValidationGroup="Add" runat="server" OnClick="btnSubmit_Click" />
                                            <asp:Button Text="Cancel" ID="btnCancelEdit" runat="server" CssClass="btnCancelEdit btn btn-default" data-dismiss="modal" />

                                        </div>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </div>
                        </div>
                    </div>
                    <%-- Modal End--%>


                    <%--Receipt Modal Start--%>
                    <input type="button" data-toggle="modal" data-target="#SuccessModel" class="OpenSuccessModal" style="display: none;" />
                    <div class="modal fade in inmodal " id="SuccessModel" tabindex="-1" role="dialog" aria-hidden="true" data-backdrop="static">
                        <div class="modal-dialog">
                            <div class="modal-content animated">
                                <div class="modal-header" style="padding-bottom: 9px; padding-top: 9px; text-align: center">
                                    <h5 class="modal-title">
                                        <asp:Label runat="server" ID="lblSuccess"></asp:Label>.</h5>
                                </div>

                                <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                    <ContentTemplate>
                                        <div class="modal-body" style="align-items: center">
                                            <div class="row" id="divReceipt" runat="server" visible="false">
                                                <div class="col-md-3 col-sm-3 col-lg-3">
                                                    <br />
                                                    <asp:Button ID="btnReceipt" CssClass="btn btn-primary" runat="server" Text="Print Confirmation Receipt" OnClick="btnReceipt_Click" />
                                                </div>
                                            </div>
                                            <div class="row" id="divApplciationFormPDF" runat="server" visible="false">
                                                <div class="col-md-3 col-sm-3 col-lg-3">
                                                    <br />
                                                    <asp:Button ID="btnApplciationFormPDF" CssClass="btn btn-primary" runat="server" Text="Print Application Form PDF" OnClick="btnApplicationForm_Click" />
                                                </div>
                                            </div>
                                            <div class="row" id="divApplciationFormExcel" runat="server" visible="false">
                                                <div class="col-md-3 col-sm-3 col-lg-3">
                                                    <br />
                                                    <asp:Button ID="btnApplciationFormExcel" CssClass="btn btn-primary" runat="server" Text="Print Application Form Excel" OnClick="btnApplicationFormExcel_Click" />

                                                </div>
                                            </div>


                                            <div class="row">
                                                <div class="col-md-3 col-sm-3 col-lg-3">
                                                    <br />
                                                    <asp:Button ID="btnCancel" CssClass="btn btn-primary" runat="server" Text="Cancel" OnClick="btnCancel_Click" />

                                                </div>
                                            </div>

                                        </div>

                                    </ContentTemplate>
                                </asp:UpdatePanel>

                            </div>
                        </div>
                    </div>

                    <%--Rceipt Modal End--%>

                    <br />
                    <div class="row">
                        <div class="col-md-12 col-sm-12" style="text-align: center;">
                            <div class="form-group">
                                <asp:Button ID="btnSubmit" CssClass="btn btn-primary" runat="server" Text="Submit" ValidationGroup="Add" OnClick="btnSubmit_Click" />
                                <asp:Button ID="btnBack" CssClass="btn btn-primary" runat="server" Text="Back" OnClick="btnBack_Click" />


                            </div>
                        </div>
                    </div>

                </div>
            </div>
            </div>




        </ContentTemplate>
    </asp:UpdatePanel>

    <style>
        .vl {
            border-left: 6px solid green;
            height: 500px;
        }
    </style>

    <style type="text/css">
        .radioButtonList {
            list-style: none;
            margin: 0;
            padding: 0;
            width: 50%
        }

            .radioButtonList.horizontal li {
                display: inline;
            }

            .radioButtonList label {
                display: inline;
            }


        .borderdiv {
            border: 1px solid black;
        }
    </style>

    <script type="text/javascript">
        function pageLoad() {

            $(".cnic").mask("99999-9999999-9");

            $(".date").mask("9999-99-99");

            $('.date').datepicker({
                forceParse: false,
                calendarWeeks: true,
                autoclose: true,
                format: 'yyyy-mm-dd',
            });


            $('.monthPicker').datepicker({
                format: "yyyy",
                viewMode: "years",
                minViewMode: "years",
                "autoclose": true,
                enableOnReadonly: false,
            });


            $(".integers").on("keypress", function (evt) {
                var charCode = (evt.which) ? evt.which : evt.keyCode;
                if (charCode > 31 && (charCode < 48 || charCode > 57))
                    return false;
                return true;
            });


            $(".decimals").on("keypress", function (evt) {
                var $txtBox = $(this);
                var charCode = (evt.which) ? evt.which : evt.keyCode
                if (charCode > 31 && (charCode < 48 || charCode > 57) && charCode != 46)
                    return false;
                else {
                    var len = $txtBox.val().length;
                    var index = $txtBox.val().indexOf('.');
                    if (index > 0 && charCode == 46) {
                        return false;
                    }
                    if (index > 0) {
                        var charAfterdot = (len + 1) - index;
                        if (charAfterdot > 3) {
                            return false;
                        }
                    }
                }
                return $txtBox; //for chaining
            });
        }



        function AlertBox(title, Message, type) {
            swal(title, Message, type);
        }

        function ClosePopup() {
            $('.hfId').val("0");
            $('.clsTextBox').val("");
            $('.btnCancelEdit').click();
        }

        function OpenPopup() {
            $('.openmodal').click();
        }


        function OpenModalDeleteModal() {
            $('.OpenDeleteModal').click();
        }
        function CloseDeleteModal() {
            $('.hfmodalDeleteId').val("0");
            $('.btnCancelDelete').click();
        }



        function functionx(evt) {
            if (evt.charCode > 31 && (evt.charCode < 48 || evt.charCode > 57)) {
                return false;
            }
        }


        function UploadFile(fileUpload) {
            if (fileUpload.value != '') {
                document.getElementById("<%=btnUpload.ClientID %>").click();
            }
        }

        function Validate_DuplicateCNIC() {
            var hdnCNIC = document.getElementById("ContentPlaceHolder1_hdnCNIC").value;

            if (hdnCNIC == 1) {
                confirm("CNIC");
            }

        }

        function OpenPopup() {
            $('.openmodal').click();
        }


        function OpenSuccessModal() {
            $('.OpenSuccessModal').click();
        }


    </script>

    <style>
        #customers {
            font-family: Arial, Helvetica, sans-serif;
            border-collapse: collapse;
            width: 50%;
            margin-left: 15px;
        }

            #customers td, #customers th {
                border: 1px solid #ddd;
                padding: 8px;
                text-align: center;
            }

            #customers tr:nth-child(even) {
                background-color: #f2f2f2;
            }

            #customers tr:hover {
                background-color: #ddd;
            }

            #customers th {
                padding-top: 12px;
                padding-bottom: 12px;
                text-align: left;
                background-color: #04AA6D;
                color: white;
            }


        #customers1 {
            font-family: Arial, Helvetica, sans-serif;
            border-collapse: collapse;
            width: 50%;
            margin-left: 15px;
        }

            #customers1 td, #customers1 th {
                border: 1px solid #ddd;
                padding: 8px;
            }

            #customers1 tr:nth-child(even) {
                background-color: #f2f2f2;
            }

            #customers1 tr:hover {
                background-color: #ddd;
            }

            #customers1 th {
                padding-top: 12px;
                padding-bottom: 12px;
                text-align: left;
                background-color: #04AA6D;
                color: white;
            }


        #customers2 {
            font-family: Arial, Helvetica, sans-serif;
            border-collapse: collapse;
            width: 50%;
            margin-left: 15px;
        }

            #customers2 td, #customers2 th {
                border: 1px solid #ddd;
                padding: 8px;
            }

            #customers2 tr:nth-child(even) {
                background-color: #f2f2f2;
            }

            #customers2 tr:hover {
                background-color: #ddd;
            }

            #customers2 th {
                padding-top: 12px;
                padding-bottom: 12px;
                text-align: left;
                background-color: #04AA6D;
                color: white;
            }


        #customers3 {
            font-family: Arial, Helvetica, sans-serif;
            border-collapse: collapse;
            width: 50%;
            margin-left: 15px;
        }

            #customers3 td, #customers3 th {
                border: 1px solid #ddd;
                padding: 8px;
            }

            #customers3 tr:nth-child(even) {
                background-color: #f2f2f2;
            }

            #customers3 tr:hover {
                background-color: #ddd;
            }

            #customers3 th {
                padding-top: 12px;
                padding-bottom: 12px;
                text-align: left;
                background-color: #04AA6D;
                color: white;
            }
    </style>
</asp:Content>

