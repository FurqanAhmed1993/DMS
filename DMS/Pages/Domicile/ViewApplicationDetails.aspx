<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/AdminMaster.master" AutoEventWireup="true" CodeFile="ViewApplicationDetails.aspx.cs" Inherits="Pages_Domicile_ViewApplicationDetails" MaintainScrollPositionOnPostback="true" %>

<%@ Register Src="~/CustomControls/Shared/InProgress.ascx" TagPrefix="uc" TagName="InProgress" %>
<%@ Register Src="~/CustomControls/Shared/PagingAndSorting.ascx" TagPrefix="uc" TagName="PagingAndSorting" %>
<%@ Register Namespace="AjaxControlToolkit" Assembly="AjaxControlToolkit" TagPrefix="ajax" %>

<%@ Register Assembly="System.Web.DataVisualization, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
    Namespace="System.Web.UI.DataVisualization.Charting" TagPrefix="asp" %>


<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=15.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91"
    Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>



<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">

    <style>
        div.left {
            box-sizing: border-box; /* <------- required */
            width: 50%; /* <------------------- required */
            float: left; /* <------------------ required */
        }

        div.right {
            overflow: hidden; /* <------------- required */
        }
    </style>



</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <asp:HiddenField ID="hdnApplicationID" runat="server" />
    <asp:HiddenField ID="hdnRequestTypeId" runat="server" />
    <asp:HiddenField ID="hdnStatusId" runat="server" />
    <asp:HiddenField ID="hdnImageURL" runat="server" />
    <input type="hidden" runat="server" id="IsAdd" value="0" />
    <input type="hidden" runat="server" id="IsView" value="0" />
    <input type="hidden" runat="server" id="IsEdit" value="0" />
    <input type="hidden" runat="server" id="IsDelete" value="0" />

    <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="upData">
        <ProgressTemplate>
            <uc:InProgress runat="server" ID="InProgress" />
        </ProgressTemplate>
    </asp:UpdateProgress>


    <div class="row heading-bg">
        <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
            <h5 class="txt-dark">View Application Details:
            </h5>
        </div>
    </div>


    <asp:UpdatePanel ID="upData" runat="server">
        <ContentTemplate>

            <div class="alert alert-danger" role="alert" id="divAlert" runat="server" visible="false">
                <asp:Label runat="server" ID="lblErrorDiv"></asp:Label>
            </div>
            <div class="alert alert-success" role="alert" runat="server" id="divSuccess" visible="false">
                <asp:Label runat="server" ID="lblSuccessDiv"></asp:Label>
            </div>

            <div class="row">
                <div class="col-md-12 col-sm-12">

                    <div class="panel panel-default card-view" style="margin-bottom: 50px;">
                        <div class="panel-wrapper collapse in">
                            <div class="panel-body">
                                <div class="row">
                                    <div class="col-md-12 col-sm-12" id="divObjectionComments" runat="server" visible="false">
                                        <div class="form-group">
                                            <h5 class="mb-5"><strong>Approver Objection Comments:
                                                <asp:Label CssClass="text-danger" ID="lblObjectionComments" runat="server"></asp:Label></strong> </h5>

                                        </div>
                                    </div>
                                    <div class="col-md-12 col-sm-12" id="divRejectionComments" runat="server" visible="false">
                                        <div class="form-group">
                                            <h5 class="mb-5"><strong>Rejection Comments:
                                                <asp:Label CssClass="text-danger" ID="lblRejectionComments" runat="server"></asp:Label></strong> </h5>

                                        </div>
                                    </div>
                                </div>

                                <div class="row">
                                    <div class="col-lg-12">
                                        <div id="divReceiptPrint" runat="server" visible="false">
                                            <asp:Button ID="btnReceipt" CssClass="btn btn-primary" runat="server" Text="Print Confirmation Receipt" OnClick="btnReceipt_Click" />

                                            <asp:Button ID="btnApplciationFormPDF" CssClass="btn btn-primary" runat="server" Text="Print Application Form PDF" OnClick="btnApplicationForm_Click" />

                                            <asp:Button ID="btnApplciationFormExcel" CssClass="btn btn-primary" runat="server" Text="Print Application Form Excel" OnClick="btnApplicationFormExcel_Click" />
                                        </div>
                                    </div>
                                </div>


                                <div class="row">
                                    <div class="col-lg-3">
                                        <div id="divEducation" runat="server" visible="false" style="margin-top: 10px;">
                                            <label class="mb-5">Education</label>
                                            <span class="MandatoryValue">* </span>
                                            <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator2" ControlToValidate="txtEducation" Text="*" ValidationGroup="IssueForm" ForeColor="Red"></asp:RequiredFieldValidator>
                                            <asp:TextBox ID="txtEducation" CssClass="form-control" runat="server" placeholder="Education"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>

                                <div class="row">
                                    <div class="col-lg-12">
                                        <div id="divIssueDomicile" runat="server" visible="false" style="margin-top: 10px;">
                                            <asp:Button ID="btnIssueDomicile" CssClass="btn btn-primary" runat="server" Text="Issue Domicile" OnClick="btnIssueDomicile_Click" Visible="false" />
                                            <asp:Button ID="btnIssueFormC" CssClass="btn btn-primary" runat="server" Text="Issue Form C" OnClick="btnIssueFormC_Click" ValidationGroup="IssueForm" Visible="false" />
                                            <asp:Button ID="btnIssueFormD" CssClass="btn btn-primary" runat="server" Text="Issue Form D" OnClick="btnIssueFormD_Click" ValidationGroup="IssueForm" Visible="false" />
                                        </div>
                                    </div>
                                </div>

                                <div class="row">
                                    <div class="col-lg-12">
                                        <div id="divDomicilePrint" runat="server" visible="false" style="margin-top: 10px;">
                                            <asp:Button ID="btnDomicile" CssClass="btn btn-primary" runat="server" Text="Print Domicile" OnClick="btnPrintDomicile_Click" Visible="false" />
                                            <asp:Button ID="btnDomicileExcel" CssClass="btn btn-primary" runat="server" Text="Print Domicile Excel Format" OnClick="btnPrintDomicileExcel_Click" Visible="false" />
                                            <asp:Button ID="btnFormC" CssClass="btn btn-primary" runat="server" Text="Print FormC" OnClick="btnPrintFormCTest_Click" Visible="false" />
                                            <asp:Button ID="btnFormCExcel" CssClass="btn btn-primary" runat="server" Text="Print Form C Excel Format" OnClick="btnPrintFormCExcel_Click" Visible="false" />
                                            <asp:Button ID="btnFormD" CssClass="btn btn-primary" runat="server" Text="Print FormD" OnClick="btnPrintFormD_Click" Visible="false" />
                                            <asp:Button ID="btnFormDExcel" CssClass="btn btn-primary" runat="server" Text="Print Form D Excel Format" OnClick="btnPrintFormDExcel_Click" Visible="false" />
                                        </div>
                                    </div>
                                </div>

                            </div>
                        </div>
                    </div>

                    <div class="row">
                        <div class="col-lg-9">
                            <div class="row">
                                <div class="col-lg-12">
                                    <div class="panel panel-default card-view">
                                        <div class="panel-wrapper collapse in">
                                            <div class="panel-body" style="text-align: center;">

                                                <asp:Image ID="Imgphoto" runat="server" Height="160" Width="160" />
                                                <div class="form-group">
                                                    <label class="mb-0">
                                                        <strong>ARN Number:</strong>
                                                    </label>
                                                    <br />
                                                    <asp:Label ID="lblARNNumber" runat="server"></asp:Label>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>

                            <div class="row">
                                <div class="col-lg-12">
                                    <div class="panel panel-default card-view">
                                        <div class="panel-wrapper collapse in">
                                            <div class="panel-header">
                                                <div class="row">
                                                    <div class="col-md-12 col-sm-12">
                                                        <h5 class="txt-dark">Personal Information</h5>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="panel-body view-application">
                                                <div class="row">
                                                    <div class="col-md-6 col-sm-12">
                                                        <div class="form-group">
                                                            <label class="mb-0">
                                                                Title: 
                                                            </label>
                                                            <br />
                                                            <asp:Label CssClass="title" ID="lblTitle" runat="server"></asp:Label>
                                                        </div>
                                                        <hr />
                                                    </div>

                                                    <div class="col-md-6 col-sm-12">
                                                        <div class="form-group">
                                                            <label class="mb-0">
                                                                Name: 
                                                            </label>
                                                            <br />
                                                            <asp:Label CssClass="title" ID="lblName" runat="server"></asp:Label>
                                                        </div>
                                                        <hr />
                                                    </div>

                                                    <div class="col-md-6 col-sm-12">
                                                        <div class="form-group">
                                                            <label class="mb-0">
                                                                Father Name: 
                                                            </label>
                                                            <br />
                                                            <asp:Label CssClass="title" ID="lblFatherName" runat="server"></asp:Label>
                                                        </div>
                                                        <hr />
                                                    </div>

                                                    <div class="col-md-6 col-sm-12">
                                                        <div class="form-group">
                                                            <label class="mb-0">
                                                                Cast/Surname: 
                                                            </label>
                                                            <br />
                                                            <asp:Label CssClass="title" ID="lblCast" runat="server"></asp:Label>
                                                        </div>
                                                        <hr />
                                                    </div>

                                                    <div class="col-md-6 col-sm-12">
                                                        <div class="form-group">
                                                            <label class="mb-0">
                                                                Applicant's NIC: 
                                                            </label>
                                                            <br />
                                                            <asp:Label CssClass="title" ID="lblCNIC" runat="server"></asp:Label>
                                                        </div>
                                                        <hr />
                                                    </div>

                                                    <div class="col-md-6 col-sm-12">
                                                        <div class="form-group">
                                                            <label class="mb-0">
                                                                Date of Birth: 
                                                            </label>
                                                            <br />
                                                            <asp:Label CssClass="title" ID="lblDoB" runat="server"></asp:Label>
                                                        </div>
                                                        <hr />
                                                    </div>

                                                    <div class="col-md-6 col-sm-12">
                                                        <div class="form-group">
                                                            <label class="mb-0">
                                                                Place of Birth: 
                                                            </label>
                                                            <br />
                                                            <asp:Label CssClass="title" ID="lblPlaceofBirth" runat="server"></asp:Label>
                                                        </div>
                                                        <hr />
                                                    </div>


                                                    <div class="col-md-6 col-sm-12">
                                                        <div class="form-group">
                                                            <label class="mb-0">
                                                                Resident of: 
                                                            </label>
                                                            <br />
                                                            <asp:Label CssClass="title" ID="lblResidentof" runat="server"></asp:Label>
                                                        </div>
                                                        <hr />
                                                    </div>

                                                    <div class="col-md-6 col-sm-12">
                                                        <div class="form-group">
                                                            <label class="mb-0">
                                                                Marks of Identification:   
                                                            </label>
                                                            <br />
                                                            <asp:Label CssClass="title" ID="lblMarkIdentification" runat="server"></asp:Label>
                                                        </div>
                                                        <hr />
                                                    </div>

                                                    <div class="col-md-6 col-sm-12">
                                                        <div class="form-group">
                                                            <label class="mb-0">
                                                                Trade Occupation:  
                                                            </label>
                                                            <br />
                                                            <asp:Label CssClass="title" ID="lblTrade" runat="server"></asp:Label>
                                                        </div>
                                                        <hr />
                                                    </div>

                                                    <div class="col-md-6 col-sm-12">
                                                        <div class="form-group">
                                                            <label class="mb-0">
                                                                Name of Wife / Husband:  
                                                            </label>
                                                            <br />
                                                            <asp:Label CssClass="title" ID="lblHusbandName" runat="server"></asp:Label>
                                                        </div>
                                                        <hr />
                                                    </div>
                                                </div>

                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>

                            <div class="row" id="divChild" runat="server" visible="false">
                                <div class="col-md-12 col-sm-12">
                                    <div class="panel panel-default border-panel card-view">
                                        <div class="panel-wrapper collapse in">
                                            <div class="panel-header">
                                                <h5 class="txt-dark">Child Details</h5>
                                            </div>
                                            <div class="panel-body">
                                                <div class="table-wrap">
                                                    <div class="table-responsive">
                                                        <table class="table table-striped table-bordered display pb-30">
                                                            <thead>
                                                                <tr>
                                                                    <th>Child Name</th>
                                                                    <th>Date of Birth</th>
                                                                </tr>
                                                            </thead>
                                                            <tbody>
                                                                <asp:Repeater ID="rptChild" runat="server">
                                                                    <ItemTemplate>
                                                                        <tr>
                                                                            <td>
                                                                                <%# Eval("Child_Name") %>
                                                                            </td>

                                                                            <td>
                                                                                <%# Eval("Child_DoB", "{0: dd/MM/yyyy}") %>
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
                                </div>
                            </div>

                            <div class="row">
                                <div class="col-lg-12">
                                    <div class="panel panel-default card-view">
                                        <div class="panel-wrapper collapse in">
                                            <div class="row">
                                                <div class="col-md-12 col-sm-12">
                                                    <div class="panel-header ">
                                                        <h5 class="txt-dark">Other Information</h5>
                                                    </div>
                                                    <div class="panel-body view-application">
                                                        <div class="row">
                                                            <div class="col-md-6 col-sm-12">
                                                                <div class="form-group">
                                                                    <label class="mb-0">
                                                                        Taluka:  
                                                                    </label>
                                                                    <br />
                                                                    <asp:Label CssClass="title" ID="lblTaluka" runat="server"></asp:Label>
                                                                </div>
                                                                <hr />
                                                            </div>

                                                            <div class="col-md-6 col-sm-12">
                                                                <div class="form-group">
                                                                    <label class="mb-0">
                                                                        District: 
                                                                    </label>
                                                                    <br />
                                                                    <asp:Label CssClass="title" ID="lblDistrict" runat="server"></asp:Label>
                                                                </div>
                                                                <hr />
                                                            </div>

                                                            <div class="col-md-6 col-sm-12">
                                                                <div class="form-group">
                                                                    <label class="mb-0">
                                                                        Temporary Address:  
                                                                    </label>
                                                                    <br />
                                                                    <asp:Label CssClass="title" ID="lblTempAddress" runat="server"></asp:Label>
                                                                </div>
                                                                <hr />
                                                            </div>

                                                            <div class="col-md-6 col-sm-12">
                                                                <div class="form-group">
                                                                    <label class="mb-0">
                                                                        Permanent Address:  
                                                                    </label>
                                                                    <br />
                                                                    <asp:Label CssClass="title" ID="lblPermanentAddress" runat="server"></asp:Label>
                                                                </div>
                                                                <hr />
                                                            </div>


                                                            <div class="col-md-6 col-sm-12">
                                                                <div class="form-group">
                                                                    <label class="mb-0">
                                                                        S.No of Electoral Area: 
                                                                    </label>
                                                                    <br />
                                                                    <asp:Label CssClass="title" ID="lblElectoralArea" runat="server"></asp:Label>
                                                                </div>
                                                                <hr />
                                                            </div>

                                                            <div class="col-md-6 col-sm-12">
                                                                <div class="form-group">
                                                                    <label class="mb-0">
                                                                        Name of Electoral Area Taluka:  
                                                                    </label>
                                                                    <br />
                                                                    <asp:Label CssClass="title" ID="lblElectoralTaluka" runat="server"></asp:Label>
                                                                </div>
                                                                <hr />
                                                            </div>

                                                            <div class="col-md-6 col-sm-12">
                                                                <div class="form-group">
                                                                    <label class="mb-0">
                                                                        Name of Electoral Area Deh:  
                                                                    </label>
                                                                    <br />
                                                                    <asp:Label CssClass="title" ID="lblElectoralAreaDeh" runat="server"></asp:Label>
                                                                </div>
                                                                <hr />
                                                            </div>

                                                            <div class="col-md-6 col-sm-12">
                                                                <div class="form-group">
                                                                    <label class="mb-0">
                                                                        Guardian's N.I.C No:  
                                                                    </label>
                                                                    <br />
                                                                    <asp:Label CssClass="title" ID="lblGuardianCNIC" runat="server"></asp:Label>
                                                                </div>
                                                                <hr />
                                                            </div>

                                                            <div class="col-md-6 col-sm-12">
                                                                <div class="form-group">
                                                                    <label class="mb-0">
                                                                        Guardian's Relationship:  
                                                                    </label>
                                                                    <br />
                                                                    <asp:Label CssClass="title" ID="lblGuardianRelationShip" runat="server"></asp:Label>
                                                                </div>
                                                                <hr />
                                                            </div>

                                                            <div class="col-md-6 col-sm-12">
                                                                <div class="form-group">
                                                                    <label class="mb-0">
                                                                        Applicant Phone #:  
                                                                    </label>
                                                                    <br />
                                                                    <asp:Label CssClass="title" ID="lblApplicantPhone" runat="server"></asp:Label>
                                                                </div>
                                                                <hr />
                                                            </div>


                                                            <div class="col-md-6 col-sm-12">
                                                                <div class="form-group">
                                                                    <label class="mb-0">
                                                                        Guardian Domicile Certificate Date: 
                                                                    </label>
                                                                    <br />
                                                                    <asp:Label CssClass="title" ID="lblGuardianDomicileDate" runat="server"></asp:Label>
                                                                </div>
                                                                <hr />
                                                            </div>

                                                            <div class="col-md-6 col-sm-12">
                                                                <div class="form-group">
                                                                    <label class="mb-0">
                                                                        Guardian's Relationship 2:  
                                                                    </label>
                                                                    <br />
                                                                    <asp:Label CssClass="title" ID="lblGuardianRelationShip2" runat="server"></asp:Label>
                                                                </div>
                                                                <hr />
                                                            </div>

                                                            <div class="col-md-6 col-sm-12">
                                                                <div class="form-group">
                                                                    <label class="mb-0">
                                                                        Applicant's Age:  
                                                                    </label>
                                                                    <br />
                                                                    <asp:Label CssClass="title" ID="lblAge" runat="server"></asp:Label>
                                                                </div>
                                                                <hr />
                                                            </div>


                                                            <div class="col-md-6 col-sm-12">
                                                                <div class="form-group">
                                                                    <label class="mb-0">
                                                                        Date of Arrival to Place of DomicIle: 
                                                                    </label>
                                                                    <br />
                                                                    <asp:Label CssClass="title" ID="lblDateArrival" runat="server"></asp:Label>
                                                                </div>
                                                                <hr />
                                                            </div>

                                                            <div class="col-md-6 col-sm-12">
                                                                <div class="form-group">
                                                                    <label class="mb-0">
                                                                        Is By Birth: 
                                                                    </label>
                                                                    <br />
                                                                    <asp:Label CssClass="title" ID="lblByBirth" runat="server"></asp:Label>
                                                                </div>
                                                                <hr />
                                                            </div>

                                                            <div class="col-md-6 col-sm-12">
                                                                <div class="form-group">
                                                                    <label class="mb-0">
                                                                        Address in Foreign Country: 
                                                                    </label>
                                                                    <br />
                                                                    <asp:Label CssClass="title" ID="lblAddressForeign" runat="server"></asp:Label>
                                                                </div>
                                                                <hr />
                                                            </div>

                                                            <div class="col-md-6 col-sm-12">
                                                                <div class="form-group">
                                                                    <label class="mb-0">
                                                                        The Mukhtiarkar(Rev) of Taluka:  
                                                                    </label>
                                                                    <br />
                                                                    <asp:Label CssClass="title" ID="lblMukhtiarkar" runat="server"></asp:Label>
                                                                </div>
                                                                <hr />
                                                            </div>

                                                            <div class="col-md-6 col-sm-12">
                                                                <div class="form-group">
                                                                    <label class="mb-0">
                                                                        The Deputy District Officer (Revenue) of Taluka:  
                                                                    </label>
                                                                    <br />
                                                                    <asp:Label CssClass="title" ID="lblDeputyDistrict" runat="server"></asp:Label>
                                                                </div>
                                                                <hr />
                                                            </div>

                                                            <div class="col-md-6 col-sm-12">
                                                                <div class="form-group">
                                                                    <label class="mb-0">
                                                                        Marital Status:  
                                                                    </label>
                                                                    <br />
                                                                    <asp:Label CssClass="title" ID="lblMaritalStatus" runat="server"></asp:Label>
                                                                </div>
                                                                <hr />
                                                            </div>


                                                            <div class="col-md-6 col-sm-12">
                                                                <div class="form-group">
                                                                    <label class="mb-0">
                                                                        Delivery Date: 
                                                                    </label>
                                                                    <br />
                                                                    <asp:Label CssClass="title" ID="lblDateDelivery" runat="server"></asp:Label>
                                                                </div>
                                                                <hr />
                                                            </div>

                                                            <div class="col-md-6 col-sm-12">
                                                                <div class="form-group">
                                                                    <label class="mb-0">
                                                                        District of Education:  
                                                                    </label>
                                                                    <br />
                                                                    <asp:Label CssClass="title" ID="lblDistrictEducation" runat="server"></asp:Label>
                                                                </div>
                                                                <hr />
                                                            </div>

                                                            <div class="col-md-6 col-sm-12">
                                                                <div class="form-group">
                                                                    <label class="mb-0">
                                                                        Date of Submission: 
                                                                    </label>
                                                                    <br />
                                                                    <asp:Label CssClass="title" ID="lblDateSubmission" runat="server"></asp:Label>
                                                                </div>
                                                                <hr />
                                                            </div>

                                                            <div class="col-md-6 col-sm-12">
                                                                <div class="form-group">
                                                                    <label class="mb-0">
                                                                        Date of issue:  
                                                                    </label>
                                                                    <br />
                                                                    <asp:Label CssClass="title" ID="lblDateIssue" runat="server"></asp:Label>
                                                                </div>
                                                                <hr />
                                                            </div>

                                                        </div>


                                                    </div>

                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>

                            <div class="row">
                                <div class="col-lg-12">
                                    <div class="panel panel-default card-view">
                                        <div class="panel-wrapper collapse in">
                                            <div class="row">
                                                <div class="col-md-12 col-sm-12">
                                                    <div class="panel-header ">
                                                        <h5 class="txt-dark">Educational Qualifications</h5>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="panel-body view-application">
                                                <div class="row">
                                                    <div class="col-md-12 col-sm-12">
                                                        <h6 class="education">Primary Education:</h6>
                                                    </div>
                                                    <div class="col-md-6 col-sm-12">
                                                        <div class="form-group">
                                                            <label class="mb-0">
                                                                Primary School: 
                                                            </label>
                                                            <br />
                                                            <asp:Label CssClass="title" ID="lblPrimarySchool" runat="server"></asp:Label>
                                                        </div>
                                                        <hr />
                                                    </div>

                                                    <div class="col-md-6 col-sm-12">
                                                        <div class="form-group">
                                                            <label class="mb-0">
                                                                Town Or Village:  
                                                            </label>
                                                            <br />
                                                            <asp:Label CssClass="title" ID="lblTownVillage" runat="server"></asp:Label>
                                                        </div>
                                                        <hr />
                                                    </div>

                                                    <div class="col-md-6 col-sm-12">
                                                        <div class="form-group">
                                                            <label class="mb-0">
                                                                From Date:  
                                                            </label>
                                                            <br />
                                                            <asp:Label CssClass="title" ID="lblFromDatePrimary" runat="server"></asp:Label>
                                                        </div>
                                                        <hr />
                                                    </div>

                                                    <div class="col-md-6 col-sm-12">
                                                        <div class="form-group">
                                                            <label class="mb-0">
                                                                To Date:  
                                                            </label>
                                                            <br />
                                                            <asp:Label CssClass="title" ID="lblToDatePrimary" runat="server"></asp:Label>
                                                        </div>
                                                        <hr />
                                                    </div>
                                                </div>


                                                <div class="row">
                                                    <div class="col-md-12 col-sm-12">
                                                        <h6 class="education">Middle Education:</h6>
                                                    </div>
                                                    <div class="col-md-6 col-sm-12">
                                                        <div class="form-group">
                                                            <label class="mb-0">
                                                                Middle School:  
                                                            </label>
                                                            <br />
                                                            <asp:Label CssClass="title" ID="lblMiddleSchool" runat="server"></asp:Label>
                                                        </div>
                                                        <hr />
                                                    </div>

                                                    <div class="col-md-6 col-sm-12">
                                                        <div class="form-group">
                                                            <label class="mb-0">
                                                                Town Or Village: 
                                                            </label>
                                                            <br />
                                                            <asp:Label CssClass="title" ID="lblMiddleTownVillage" runat="server"></asp:Label>
                                                        </div>
                                                        <hr />
                                                    </div>

                                                    <div class="col-md-6 col-sm-12">
                                                        <div class="form-group">
                                                            <label class="mb-0">
                                                                From Date:  
                                                            </label>
                                                            <br />
                                                            <asp:Label CssClass="title" ID="lblFromDateMiddle" runat="server"></asp:Label>
                                                        </div>
                                                        <hr />
                                                    </div>

                                                    <div class="col-md-6 col-sm-12">
                                                        <div class="form-group">
                                                            <label class="mb-0">
                                                                To Date:  
                                                            </label>
                                                            <br />
                                                            <asp:Label CssClass="title" ID="lblToDateMiddle" runat="server"></asp:Label>
                                                        </div>
                                                        <hr />
                                                    </div>
                                                </div>

                                                <div class="row">
                                                    <div class="col-md-12 col-sm-12">
                                                        <h6 class="education">High Education:</h6>
                                                    </div>
                                                    <div class="col-md-6 col-sm-12">
                                                        <div class="form-group">
                                                            <label class="mb-0">
                                                                High School:  
                                                            </label>
                                                            <br />
                                                            <asp:Label CssClass="title" ID="lblHighSchool" runat="server"></asp:Label>
                                                        </div>
                                                        <hr />
                                                    </div>

                                                    <div class="col-md-6 col-sm-12">
                                                        <div class="form-group">
                                                            <label class="mb-0">
                                                                Town Or Village:  
                                                            </label>
                                                            <br />
                                                            <asp:Label CssClass="title" ID="lblHighTownVillage" runat="server"></asp:Label>
                                                        </div>
                                                        <hr />
                                                    </div>


                                                    <div class="col-md-6 col-sm-12">
                                                        <div class="form-group">
                                                            <label class="mb-0">
                                                                From Date:  
                                                            </label>
                                                            <br />
                                                            <asp:Label CssClass="title" ID="lblFromDateHigh" runat="server"></asp:Label>
                                                        </div>
                                                        <hr />
                                                    </div>


                                                    <div class="col-md-6 col-sm-12">
                                                        <div class="form-group">
                                                            <label class="mb-0">
                                                                To Date:  
                                                            </label>
                                                            <br />
                                                            <asp:Label CssClass="title" ID="lblToDateHigh" runat="server"></asp:Label>
                                                            <hr />
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>

                            <div class="row">
                                <div class="col-lg-12">
                                    <div class="panel panel-default card-view">
                                        <div class="panel-wrapper collapse in">
                                            <div class="row">
                                                <div class="col-md-12 col-sm-12">
                                                    <div class="panel-header ">
                                                        <h5 class="txt-dark">Immovable Properties</h5>
                                                    </div>
                                                    <div class="panel-body view-application">
                                                        <div class="row">
                                                            <div class="col-md-6 col-sm-12">
                                                                <div class="form-group">
                                                                    <label class="mb-0">
                                                                        Particulars of Property: 
                                                                    </label>
                                                                    <br />
                                                                    <asp:Label CssClass="title" ID="lblParticualrsPropoerty" runat="server"></asp:Label>
                                                                </div>
                                                                <hr />
                                                            </div>

                                                            <div class="col-md-6 col-sm-12">
                                                                <div class="form-group">
                                                                    <label class="mb-0">
                                                                        Location: 
                                                                    </label>
                                                                    <br />
                                                                    <asp:Label CssClass="title" ID="lblLocation" runat="server"></asp:Label>
                                                                </div>
                                                                <hr />
                                                            </div>
                                                        </div>
                                                    </div>

                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>


                            <div class="row">
                                <div class="col-lg-12">
                                    <div class="panel panel-default border-panel card-view" id="divDomicileHistory" runat="server" visible="false">
                                        <div class="panel-wrapper collapse in">
                                            <div class="panel-header">
                                                <h5 class="txt-dark">Domicile History</h5>
                                            </div>
                                            <div class="panel-body">
                                                <div class="table-wrap">
                                                    <div class="table-responsive">
                                                        <table class="table table-striped table-bordered display pb-30">
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
                                                                                <%# Eval("ApprovedDate", "{0: dd-MM-yyyy}") %>
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
                                </div>
                            </div>



                            <div class="row">
                                <div class="col-lg-12">
                                    <div class="panel panel-default border-panel card-view" id="DivReceiver" runat="server" visible="false">
                                        <div class="panel-wrapper collapse in">
                                            <div class="panel-header">
                                                <h5 class="txt-dark">Receiver Details</h5>
                                            </div>
                                            <div class="panel-body">
                                                <div class="table-wrap">
                                                    <div class="table-responsive">
                                                        <table class="table table-striped table-bordered display pb-30">
                                                            <thead>
                                                                <tr>

                                                                    <th>Receiver Name </th>
                                                                    <th>Receiver CNIC </th>
                                                                    <th>Receiver Address</th>
                                                                    <th>Receiving Date</th>
                                                                </tr>
                                                            </thead>
                                                            <tbody>
                                                                <asp:Repeater ID="rptReceiver" runat="server">
                                                                    <ItemTemplate>
                                                                        <tr>
                                                                            <td>
                                                                                <%# Eval("Receiver_Name") %>
                                                                            </td>
                                                                            <td>
                                                                                <%# Eval("Receiver_CNIC") %>
                                                                            </td>
                                                                            <td>
                                                                                <%# Eval("Receiver_Address") %>
                                                                            </td>
                                                                            <td>
                                                                                <%# Eval("Issuance_Date",  "{0: dd-MM-yyyy}") %>
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
                                </div>
                            </div>



                        </div>

                        <div class="col-lg-3">
                            <div class="panel panel-default card-view">
                                <div class="panel-wrapper collapse in">
                                    <div class="panel-body">
                                        <div id="divNewDocs" runat="server" visible="false">
                                            <div class="row">

                                                <div class="col-md-12 col-sm-12 col-lg-12">
                                                    <div class="form-group">
                                                        <label class="mb-0">CNIC Front</label><span class="MandatoryValue">* </span>
                                                        <asp:RequiredFieldValidator runat="server" ID="rfvCNICFront" ControlToValidate="FileUploadCNICFront" Text="*" ValidationGroup="AddNewDocs" ForeColor="Red"></asp:RequiredFieldValidator><br />
                                                        <span class="MandatoryValue" style="font-size: 12px">(Only jpeg , jpg , png images allowed)</span>
                                                        <asp:FileUpload ID="FileUploadCNICFront" runat="server" onchange="UploadDoc()" />

                                                        <asp:ImageButton ID="lnkCNICFront" runat="server" Style="height: 200px; width: 200px;" Visible="false" OnClick="btnImageView_Click" />
                                                        <br />
                                                        <asp:Label ID="lblMessageCNICFront" runat="server" ForeColor="red"
                                                            Visible="false" />
                                                    </div>
                                                </div>
                                            </div>

                                            <div class="row">
                                                <div class="col-md-12 col-sm-12 col-lg-12">
                                                    <div class="form-group">
                                                        <label class="mb-0">CNIC Back</label><span class="MandatoryValue">* </span>
                                                        <asp:RequiredFieldValidator runat="server" ID="rfvCNICBack" ControlToValidate="FileUploadCNICBack" Text="*" ValidationGroup="AddNewDocs" ForeColor="Red"></asp:RequiredFieldValidator>
                                                        <br />
                                                        <span class="MandatoryValue" style="font-size: 12px">(Only jpeg , jpg , png images allowed)</span>
                                                        <asp:FileUpload ID="FileUploadCNICBack" runat="server" onchange="UploadDoc()" />

                                                        <asp:ImageButton ID="lblCNICBack" runat="server" Style="height: 200px; width: 200px;" Visible="false" OnClick="btnImageView_Click" />
                                                        <br />
                                                        <asp:Label ID="lblMessageCNICBack" runat="server" ForeColor="red"
                                                            Visible="false" />
                                                    </div>
                                                </div>
                                            </div>

                                            <div class="row">
                                                <div class="col-md-12 col-sm-12 col-lg-12">
                                                    <div class="form-group">
                                                        <label class="mb-0">ASSISTANT COMMISSIONER’S REPORT</label><span class="MandatoryValue">* </span>
                                                        <asp:RequiredFieldValidator runat="server" ID="rfvAsstComm" ControlToValidate="FileUpload_AsstComm" Text="*" ValidationGroup="AddNewDocs" ForeColor="Red"></asp:RequiredFieldValidator><span class="MandatoryValue" style="font-size: 12px">(Only jpeg , jpg , png images allowed)</span>
                                                        <asp:FileUpload ID="FileUpload_AsstComm" runat="server" onchange="UploadDoc()" />

                                                        <asp:ImageButton ID="lblCommReport" runat="server" Style="height: 200px; width: 200px;" Visible="false" OnClick="btnImageView_Click" />
                                                        <br />
                                                        <asp:Label ID="lblMsgCommReport" runat="server" ForeColor="red"
                                                            Visible="false" />
                                                    </div>
                                                </div>
                                            </div>


                                            <div class="row">
                                                <div class="col-md-12 col-sm-12 col-lg-12">
                                                    <div class="form-group">
                                                        <label class="mb-0">MUKHTIARKAR’S REPORT</label><span class="MandatoryValue">* </span>
                                                        <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator3" ControlToValidate="FileUpload4" Text="*" ValidationGroup="AddNewDocs" ForeColor="Red"></asp:RequiredFieldValidator><br />
                                                        <span class="MandatoryValue" style="font-size: 12px">(Only jpeg , jpg , png images allowed)</span>
                                                        <asp:FileUpload ID="FileUpload4" runat="server" onchange="UploadDoc()" />

                                                        <asp:ImageButton ID="lblRefMUKHTIARKAR" runat="server" Style="height: 200px; width: 200px;" Visible="false" OnClick="btnImageView_Click" />
                                                        <br />
                                                        <asp:Label ID="Label2" runat="server" ForeColor="red"
                                                            Visible="false" />
                                                    </div>
                                                </div>
                                            </div>

                                            <div class="row">
                                                <div class="col-md-12 col-sm-12 col-lg-12">
                                                    <div class="form-group">
                                                        <label class="mb-0">PRIMARY CERTIFICATE</label><br />
                                                        <span class="MandatoryValue" style="font-size: 12px">(Only jpeg , jpg , png images allowed)</span>
                                                        <asp:FileUpload ID="FileUpload5" runat="server" onchange="UploadDoc()" />

                                                        <asp:ImageButton ID="lblRefPrimaryCertificate" runat="server" Style="height: 200px; width: 200px;" Visible="false" OnClick="btnImageView_Click" />
                                                        <br />
                                                        <asp:Label ID="Label3" runat="server" ForeColor="red"
                                                            Visible="false" />
                                                    </div>
                                                </div>
                                            </div>

                                            <div class="row">
                                                <div class="col-md-12 col-sm-12 col-lg-12">
                                                    <div class="form-group">
                                                        <label class="mb-0">MATRIC CERTIFICATE</label><br />
                                                        <span class="MandatoryValue" style="font-size: 12px">(Only jpeg , jpg , png images allowed)</span>
                                                        <asp:FileUpload ID="FileUpload6" runat="server" onchange="UploadDoc()" />

                                                        <asp:ImageButton ID="lblRefMatricCertificate" runat="server" Style="height: 200px; width: 200px;" Visible="false" OnClick="btnImageView_Click" />
                                                        <br />
                                                        <asp:Label ID="Label4" runat="server" ForeColor="red"
                                                            Visible="false" />
                                                    </div>
                                                </div>
                                            </div>


                                            <div class="row">
                                                <div class="col-md-12 col-sm-12 col-lg-12">
                                                    <div class="form-group">
                                                        <label class="mb-0">RESIDENCE CERTIFICATE</label><span class="MandatoryValue">*</span>
                                                        <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator4" ControlToValidate="FileUpload7" Text="*" ValidationGroup="AddNewDocs" ForeColor="Red"></asp:RequiredFieldValidator>

                                                        <asp:FileUpload ID="FileUpload7" runat="server" onchange="UploadDoc()" />
                                                        <span class="MandatoryValue" style="font-size: 12px">(Only jpeg , jpg , png images allowed)</span>
                                                        <asp:ImageButton ID="lblRefRESIDENCECERTIFICATE" runat="server" Style="height: 200px; width: 200px;" Visible="false" OnClick="btnImageView_Click" />
                                                        <br />
                                                        <asp:Label ID="Label5" runat="server" ForeColor="red"
                                                            Visible="false" />
                                                    </div>
                                                </div>
                                            </div>

                                            <div class="row">
                                                <div class="col-md-12 col-sm-12 col-lg-12">
                                                    <div class="form-group">
                                                        <label class="mb-0">VOTE CERTIFICATE</label><span class="MandatoryValue">* </span>
                                                        <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator5" ControlToValidate="FileUpload8" Text="*" ValidationGroup="AddNewDocs" ForeColor="Red"></asp:RequiredFieldValidator><br />
                                                        <span class="MandatoryValue" style="font-size: 12px">(Only jpeg , jpg , png images allowed)</span>
                                                        <asp:FileUpload ID="FileUpload8" runat="server" onchange="UploadDoc()" />
                                                        <asp:ImageButton ID="lblVote" runat="server" Style="height: 200px; width: 200px;" Visible="false" OnClick="btnImageView_Click" />
                                                        <br />
                                                        <asp:Label ID="Label6" runat="server" ForeColor="red"
                                                            Visible="false" />
                                                    </div>
                                                </div>
                                            </div>

                                            <div class="row">
                                                <div class="col-md-12 col-sm-12 col-lg-12">
                                                    <div class="form-group">
                                                        <label class="mb-0">GUARDIAN’S DOMICILE</label><br />
                                                        <span class="MandatoryValue" style="font-size: 12px">(Only jpeg , jpg , png images allowed)</span>

                                                        <asp:FileUpload ID="FileUpload9" runat="server" onchange="UploadDoc()" />
                                                        <asp:ImageButton ID="lblGUARDIANDomicile" runat="server" Style="height: 200px; width: 200px;" Visible="false" OnClick="btnImageView_Click" />
                                                        <br />
                                                        <asp:Label ID="Label7" runat="server" ForeColor="red"
                                                            Visible="false" />

                                                    </div>
                                                </div>
                                            </div>

                                            <div class="row">
                                                <div class="col-md-12 col-sm-12 col-lg-12">
                                                    <div class="form-group">
                                                        <label class="mb-0">BANK CHALLANS</label><span class="MandatoryValue">* </span>
                                                        <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator6" ControlToValidate="FileUpload10" Text="*" ValidationGroup="AddNewDocs" ForeColor="Red"></asp:RequiredFieldValidator><br />
                                                        <span class="MandatoryValue" style="font-size: 12px">(Only jpeg , jpg , png images allowed)</span>
                                                        <asp:FileUpload ID="FileUpload10" runat="server" onchange="UploadDoc()" />
                                                        <asp:ImageButton ID="lblBankChallan" runat="server" Style="height: 200px; width: 200px;" Visible="false" OnClick="btnImageView_Click" />
                                                        <br />
                                                        <asp:Label ID="Label8" runat="server" ForeColor="red"
                                                            Visible="false" />
                                                    </div>
                                                </div>
                                            </div>

                                            <div class="row">
                                                <div class="col-md-12 col-sm-12 col-lg-12">
                                                    <div class="form-group">
                                                        <label class="mb-0">Other Document 1</label><br />
                                                        <span class="MandatoryValue" style="font-size: 12px">(Only jpeg , jpg , png images allowed)</span>

                                                        <asp:FileUpload ID="FileUpload11" runat="server" onchange="UploadDoc()" />

                                                        <asp:ImageButton ID="lblOtherDoc1" runat="server" Style="height: 200px; width: 200px;" Visible="false" OnClick="btnImageView_Click" />
                                                        <br />
                                                        <asp:Label ID="Label9" runat="server" ForeColor="red"
                                                            Visible="false" />

                                                    </div>
                                                </div>
                                            </div>


                                            <div class="row">
                                                <div class="col-md-12 col-sm-12 col-lg-12">
                                                    <div class="form-group">
                                                        <label class="mb-0">Other Document 2</label><br />
                                                        <span class="MandatoryValue" style="font-size: 12px">(Only jpeg , jpg , png images allowed)</span>

                                                        <asp:FileUpload ID="FileUpload12" runat="server" onchange="UploadDoc()" />
                                                        <asp:ImageButton ID="lblOtherDoc2" runat="server" Style="height: 200px; width: 200px;" Visible="false" OnClick="btnImageView_Click" />
                                                        <br />
                                                        <asp:Label ID="Label10" runat="server" ForeColor="red"
                                                            Visible="false" />
                                                    </div>
                                                </div>
                                            </div>



                                            <div class="row">
                                                <div class="col-md-12 col-sm-12 col-lg-12">
                                                    <div class="form-group">
                                                        <label class="mb-0">Other Document 3</label><br />
                                                        <span class="MandatoryValue" style="font-size: 12px">(Only jpeg , jpg , png images allowed)</span>

                                                        <asp:FileUpload ID="FileOtherDoc3" runat="server" onchange="UploadDoc()" />
                                                        <asp:ImageButton ID="ImgOtherDoc3" runat="server" Style="height: 200px; width: 200px;" Visible="false" OnClick="btnImageView_Click" />
                                                        <br />
                                                        <asp:Label ID="lblOtherDoc3" runat="server" ForeColor="red"
                                                            Visible="false" />
                                                    </div>
                                                </div>
                                            </div>



                                            <div class="row">
                                                <div class="col-md-12 col-sm-12 col-lg-12">
                                                    <div class="form-group">
                                                        <label class="mb-0">Other Document 4</label><br />
                                                        <span class="MandatoryValue" style="font-size: 12px">(Only jpeg , jpg , png images allowed)</span>

                                                        <asp:FileUpload ID="FileOtherDoc4" runat="server" onchange="UploadDoc()" />
                                                        <asp:ImageButton ID="ImgOtherDoc4" runat="server" Style="height: 200px; width: 200px;" Visible="false" OnClick="btnImageView_Click" />
                                                        <br />
                                                        <asp:Label ID="lblOtherDoc4" runat="server" ForeColor="red"
                                                            Visible="false" />
                                                    </div>
                                                </div>
                                            </div>


                                            <div class="row">
                                                <div class="col-md-12 col-sm-12 col-lg-12">
                                                    <div class="form-group">
                                                        <label class="mb-0">Other Document 5</label><br />
                                                        <span class="MandatoryValue" style="font-size: 12px">(Only jpeg , jpg , png images allowed)</span>

                                                        <asp:FileUpload ID="FileOtherDoc5" runat="server" onchange="UploadDoc()" />
                                                        <asp:ImageButton ID="ImgOtherDoc5" runat="server" Style="height: 200px; width: 200px;" Visible="false" OnClick="btnImageView_Click" />
                                                        <br />
                                                        <asp:Label ID="lblOtherDoc5" runat="server" ForeColor="red"
                                                            Visible="false" />
                                                    </div>
                                                </div>
                                            </div>

                                            <asp:Button Text="Approve" CssClass="btn btn-primary" ID="btnAdd" ValidationGroup="AddNewDocs" runat="server" OnClick="btnAdd_Click" Visible="false" />

                                        </div>

                                        <div id="divDuplicateDocs" runat="server" visible="false">

                                            <div class="row">
                                                <div class="col-md-12 col-sm-12 col-lg-12">
                                                    <div class="form-group">
                                                        <label class="mb-0">CNIC Front</label><span class="MandatoryValue">* </span>
                                                        <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator11" ControlToValidate="FileUploadCNICFront_Dup" Text="*" ValidationGroup="AddDUP" ForeColor="Red"></asp:RequiredFieldValidator><br />
                                                        <span class="MandatoryValue" style="font-size: 12px">(Only jpeg , jpg , png images allowed)</span>
                                                        <asp:FileUpload ID="FileUploadCNICFront_Dup" runat="server" onchange="UploadDoc()" />

                                                        <asp:ImageButton ID="ImgCNICFront" runat="server" Style="height: 200px; width: 200px;" Visible="false" OnClick="btnImageView_Click" />
                                                        <br />
                                                        <asp:Label ID="lblMsgCNICFront" runat="server" ForeColor="red"
                                                            Visible="false" />
                                                        <asp:Button ID="Button4" Text="Upload" runat="server" OnClick="Upload" Style="display: none" />
                                                    </div>
                                                </div>
                                            </div>

                                            <div class="row">
                                                <div class="col-md-12 col-sm-12 col-lg-12">
                                                    <div class="form-group">
                                                        <label class="mb-0">CNIC Back</label><span class="MandatoryValue">* </span>
                                                        <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator14" ControlToValidate="FileUploadCNICBack_Dup" Text="*" ValidationGroup="AddDUP" ForeColor="Red"></asp:RequiredFieldValidator><br />
                                                        <span class="MandatoryValue" style="font-size: 12px">(Only jpeg , jpg , png images allowed)</span>
                                                        <asp:FileUpload ID="FileUploadCNICBack_Dup" runat="server" onchange="UploadDoc()" />
                                                        <asp:ImageButton ID="ImgCNICBack" runat="server" Style="height: 200px; width: 200px;" Visible="false" OnClick="btnImageView_Click" />
                                                        <br />
                                                        <asp:Label ID="lblMsgCNICBack" runat="server" ForeColor="red"
                                                            Visible="false" />
                                                    </div>
                                                </div>
                                            </div>

                                            <div class="row">
                                                <div class="col-md-12 col-sm-12 col-lg-12">
                                                    <div class="form-group">
                                                        <label class="mb-0">NC/ FIR COPY</label><span class="MandatoryValue">* </span>
                                                        <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator15" ControlToValidate="FileUploadFIR_Dup" Text="*" ValidationGroup="AddDUP" ForeColor="Red"></asp:RequiredFieldValidator><br />
                                                        <span class="MandatoryValue" style="font-size: 12px">(Only jpeg , jpg , png images allowed)</span>
                                                        <asp:FileUpload ID="FileUploadFIR_Dup" runat="server" onchange="UploadDoc()" />
                                                        <asp:ImageButton ID="ImgFir" runat="server" Style="height: 200px; width: 200px;" Visible="false" OnClick="btnImageView_Click" />
                                                        <br />
                                                        <asp:Label ID="lblMsgFir" runat="server" ForeColor="red"
                                                            Visible="false" />

                                                    </div>
                                                </div>
                                            </div>


                                            <div class="row">
                                                <div class="col-md-12 col-sm-12 col-lg-12">
                                                    <div class="form-group">
                                                        <label class="mb-5">APPROVAL OF AUTHORITY</label><span class="MandatoryValue">* </span>
                                                        <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator16" ControlToValidate="FileUploadAuthority_Dup" Text="*" ValidationGroup="AddDUP" ForeColor="Red"></asp:RequiredFieldValidator><br />
                                                        <span class="MandatoryValue" style="font-size: 12px">(Only jpeg , jpg , png images allowed)</span>
                                                        <asp:FileUpload ID="FileUploadAuthority_Dup" runat="server" onchange="UploadDoc()" />
                                                        <asp:ImageButton ID="ImgApproval" runat="server" Style="height: 200px; width: 200px;" Visible="false" OnClick="btnImageView_Click" />
                                                        <br />
                                                        <asp:Label ID="lblApproval" runat="server" ForeColor="red"
                                                            Visible="false" />
                                                    </div>
                                                </div>
                                            </div>

                                            <div class="row">
                                                <div class="col-md-12 col-sm-12 col-lg-12">
                                                    <div class="form-group">
                                                        <label class="mb-5">BANK CHALLAN</label><span class="MandatoryValue">* </span>
                                                        <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator17" ControlToValidate="FileUploadChallan_Dup" Text="*" ValidationGroup="AddDUP" ForeColor="Red"></asp:RequiredFieldValidator><br />
                                                        <span class="MandatoryValue" style="font-size: 12px">(Only jpeg , jpg , png images allowed)</span>
                                                        <asp:FileUpload ID="FileUploadChallan_Dup" runat="server" onchange="UploadDoc()" />
                                                        <asp:ImageButton ID="ImgBankChallan" runat="server" Style="height: 200px; width: 200px;" Visible="false" OnClick="btnImageView_Click" />

                                                        <br />
                                                        <asp:Label ID="lblBanlChallan" runat="server" ForeColor="red"
                                                            Visible="false" />
                                                    </div>
                                                </div>
                                            </div>

                                            <div class="row">
                                                <div class="col-md-12 col-sm-12 col-lg-12">
                                                    <div class="form-group">
                                                        <label class="mb-5">APPLICATION</label><br />
                                                        <span class="MandatoryValue" style="font-size: 12px">(Only jpeg , jpg , png images allowed)</span>
                                                        <asp:FileUpload ID="FileUploadApp_Dup" runat="server" onchange="UploadDoc()" />
                                                        <asp:ImageButton ID="ImgApp" runat="server" Style="height: 200px; width: 200px;" Visible="false" OnClick="btnImageView_Click" />
                                                        <br />
                                                        <asp:Label ID="lblApp" runat="server" ForeColor="red"
                                                            Visible="false" />
                                                    </div>
                                                </div>
                                            </div>


                                            <div class="row">
                                                <div class="col-md-12 col-sm-12 col-lg-12">
                                                    <div class="form-group">
                                                        <label class="mb-5">Other Document 1</label><br />
                                                        <span class="MandatoryValue" style="font-size: 12px">(Only jpeg , jpg , png images allowed)</span>

                                                        <asp:FileUpload ID="FileDupOther1" runat="server" onchange="UploadDoc()" />
                                                        <asp:ImageButton ID="ImgDupOther1" runat="server" Style="height: 200px; width: 200px;" Visible="false" OnClick="btnImageView_Click" />
                                                        <br />
                                                        <asp:Label ID="lblDupOther1" runat="server" ForeColor="red"
                                                            Visible="false" />
                                                    </div>
                                                </div>
                                            </div>



                                            <div class="row">
                                                <div class="col-md-12 col-sm-12 col-lg-12">
                                                    <div class="form-group">
                                                        <label class="mb-5">Other Document 2</label><br />
                                                        <span class="MandatoryValue" style="font-size: 12px">(Only jpeg , jpg , png images allowed)</span>

                                                        <asp:FileUpload ID="FileDupOther2" runat="server" onchange="UploadDoc()" />
                                                        <asp:ImageButton ID="ImgDupOther2" runat="server" Style="height: 200px; width: 200px;" Visible="false" OnClick="btnImageView_Click" />
                                                        <br />
                                                        <asp:Label ID="lblDupOther2" runat="server" ForeColor="red"
                                                            Visible="false" />
                                                    </div>
                                                </div>
                                            </div>



                                            <div class="row">
                                                <div class="col-md-12 col-sm-12 col-lg-12">
                                                    <div class="form-group">
                                                        <label class="mb-5">Other Document 3</label><br />
                                                        <span class="MandatoryValue" style="font-size: 12px">(Only jpeg , jpg , png images allowed)</span>

                                                        <asp:FileUpload ID="FileDupOther3" runat="server" onchange="UploadDoc()" />
                                                        <asp:ImageButton ID="ImgDupOther3" runat="server" Style="height: 200px; width: 200px;" Visible="false" OnClick="btnImageView_Click" />
                                                        <br />
                                                        <asp:Label ID="lblDupOther3" runat="server" ForeColor="red"
                                                            Visible="false" />
                                                    </div>
                                                </div>
                                            </div>

                                            <asp:Button Text="Approve" CssClass="btn btn-primary" ID="btnApproveDuplicate" ValidationGroup="AddDUP" runat="server" Visible="false" OnClick="btnAdd_Click" />
                                        </div>

                                        <div id="divRevisionDocs" runat="server" visible="false">
                                            <div class="row">
                                                <div class="col-md-12 col-sm-12 col-lg-12">
                                                    <div class="form-group">
                                                        <label class="mb-0">CNIC Front</label><span class="MandatoryValue">* </span>
                                                        <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator7" ControlToValidate="FileUpload13" Text="*" ValidationGroup="AddRev" ForeColor="Red"></asp:RequiredFieldValidator><br />
                                                        <span class="MandatoryValue" style="font-size: 12px">(Only jpeg , jpg , png images allowed)</span>
                                                        <asp:FileUpload ID="FileUpload13" runat="server" onchange="UploadDoc()" />
                                                        <asp:ImageButton ID="Label11" runat="server" Style="height: 200px; width: 200px;" Visible="false" OnClick="btnImageView_Click" />
                                                        <br />
                                                        <asp:Label ID="Label12" runat="server" ForeColor="red"
                                                            Visible="false" />
                                                        <asp:Button ID="Button1" Text="Upload" runat="server" OnClick="Upload" Style="display: none" />
                                                    </div>
                                                </div>
                                            </div>

                                            <div class="row">
                                                <div class="col-md-12 col-sm-12 col-lg-12">
                                                    <div class="form-group">
                                                        <label class="mb-0">CNIC Back</label><span class="MandatoryValue">* </span>
                                                        <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator8" ControlToValidate="FileUpload14" Text="*" ValidationGroup="AddRev" ForeColor="Red"></asp:RequiredFieldValidator><br />
                                                        <span class="MandatoryValue" style="font-size: 12px">(Only jpeg , jpg , png images allowed)</span>
                                                        <asp:FileUpload ID="FileUpload14" runat="server" onchange="UploadDoc()" />

                                                        <asp:ImageButton ID="Label13" runat="server" Style="height: 200px; width: 200px;" Visible="false" OnClick="btnImageView_Click" />
                                                        <br />
                                                        <asp:Label ID="Label14" runat="server" ForeColor="red"
                                                            Visible="false" />
                                                    </div>
                                                </div>
                                            </div>

                                            <div class="row">
                                                <div class="col-md-12 col-sm-12 col-lg-12">
                                                    <div class="form-group">
                                                        <label class="mb-0">APPROVAL OF AUTHORITY</label><span class="MandatoryValue">* </span>
                                                        <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator9" ControlToValidate="FileUpload15" Text="*" ValidationGroup="AddRev" ForeColor="Red"></asp:RequiredFieldValidator><br />
                                                        <span class="MandatoryValue" style="font-size: 12px">(Only jpeg , jpg , png images allowed)</span>
                                                        <asp:FileUpload ID="FileUpload15" runat="server" onchange="UploadDoc()" />

                                                        <asp:ImageButton ID="Label15" runat="server" Style="height: 200px; width: 200px;" Visible="false" OnClick="btnImageView_Click" />
                                                        <br />
                                                        <asp:Label ID="Label16" runat="server" ForeColor="red"
                                                            Visible="false" />

                                                    </div>
                                                </div>
                                            </div>


                                            <div class="row">
                                                <div class="col-md-12 col-sm-12 col-lg-12">
                                                    <div class="form-group">
                                                        <label class="mb-5">BANK CHALLAN</label><span class="MandatoryValue">* </span>
                                                        <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator10" ControlToValidate="FileUpload16" Text="*" ValidationGroup="AddRev" ForeColor="Red"></asp:RequiredFieldValidator><br />
                                                        <span class="MandatoryValue" style="font-size: 12px">(Only jpeg , jpg , png images allowed)</span>
                                                        <asp:FileUpload ID="FileUpload16" runat="server" onchange="UploadDoc()" />

                                                        <asp:ImageButton ID="Label17" runat="server" Style="height: 200px; width: 200px;" Visible="false" OnClick="btnImageView_Click" />
                                                        <br />
                                                        <asp:Label ID="Label18" runat="server" ForeColor="red"
                                                            Visible="false" />
                                                    </div>
                                                </div>
                                            </div>

                                            <div class="row">
                                                <div class="col-md-12 col-sm-12 col-lg-12">
                                                    <div class="form-group">
                                                        <label class="mb-5">CORRECTION OF DOCUMENT 1</label><span class="MandatoryValue">* </span>
                                                        <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator12" ControlToValidate="FileUpload17" Text="*" ValidationGroup="AddRev" ForeColor="Red"></asp:RequiredFieldValidator><br />
                                                        <span class="MandatoryValue" style="font-size: 12px">(Only jpeg , jpg , png images allowed)</span>
                                                        <asp:FileUpload ID="FileUpload17" runat="server" onchange="UploadDoc()" />

                                                        <asp:ImageButton ID="Label19" runat="server" Style="height: 200px; width: 200px;" Visible="false" OnClick="btnImageView_Click" />
                                                        <br />
                                                        <asp:Label ID="Label20" runat="server" ForeColor="red"
                                                            Visible="false" />
                                                    </div>
                                                </div>
                                            </div>

                                            <div class="row">
                                                <div class="col-md-12 col-sm-12 col-lg-12">
                                                    <div class="form-group">
                                                        <label class="mb-5">CORRECTION OF DOCUMENT 2</label><span class="MandatoryValue">* </span>
                                                        <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator13" ControlToValidate="FileUpload18" Text="*" ValidationGroup="AddRev" ForeColor="Red"></asp:RequiredFieldValidator><br />
                                                        <span class="MandatoryValue" style="font-size: 12px">(Only jpeg , jpg , png images allowed)</span>
                                                        <asp:FileUpload ID="FileUpload18" runat="server" onchange="UploadDoc()" />

                                                        <asp:ImageButton ID="Label21" runat="server" Style="height: 200px; width: 200px;" Visible="false" OnClick="btnImageView_Click" />
                                                        <br />
                                                        <asp:Label ID="Label22" runat="server" ForeColor="red"
                                                            Visible="false" />
                                                    </div>
                                                </div>
                                            </div>


                                            <div class="row">
                                                <div class="col-md-12 col-sm-12 col-lg-12">
                                                    <div class="form-group">
                                                        <label class="mb-5">Other Document 1</label><br />
                                                        <span class="MandatoryValue" style="font-size: 12px">(Only jpeg , jpg , png images allowed)</span>
                                                        <asp:FileUpload ID="FileUpload19" runat="server" onchange="UploadDoc()" />

                                                        <asp:ImageButton ID="Label23" runat="server" Style="height: 200px; width: 200px;" Visible="false" OnClick="btnImageView_Click" />
                                                        <br />
                                                        <asp:Label ID="Label24" runat="server" ForeColor="red"
                                                            Visible="false" />
                                                    </div>
                                                </div>
                                            </div>


                                            <div class="row">
                                                <div class="col-md-12 col-sm-12 col-lg-12">
                                                    <div class="form-group">
                                                        <label class="mb-5">Other Document 2</label><br />
                                                        <span class="MandatoryValue" style="font-size: 12px">(Only jpeg , jpg , png images allowed)</span>

                                                        <asp:FileUpload ID="FileOtherRev2" runat="server" onchange="UploadDoc()" />
                                                        <asp:ImageButton ID="ImgOtherRev2" runat="server" Style="height: 200px; width: 200px;" Visible="false" OnClick="btnImageView_Click" />
                                                        <br />
                                                        <asp:Label ID="lblOtherRev2" runat="server" ForeColor="red"
                                                            Visible="false" />
                                                    </div>
                                                </div>
                                            </div>


                                            <div class="row">
                                                <div class="col-md-12 col-sm-12 col-lg-12">
                                                    <div class="form-group">
                                                        <label class="mb-5">Other Document 3</label><br />
                                                        <span class="MandatoryValue" style="font-size: 12px">(Only jpeg , jpg , png images allowed)</span>

                                                        <asp:FileUpload ID="FileOtherRev3" runat="server" onchange="UploadDoc()" />
                                                        <asp:ImageButton ID="ImgOtherRev3" runat="server" Style="height: 200px; width: 200px;" Visible="false" OnClick="btnImageView_Click" />
                                                        <br />
                                                        <asp:Label ID="lblOtherRev3" runat="server" ForeColor="red"
                                                            Visible="false" />
                                                    </div>
                                                </div>
                                            </div>


                                            <div class="row">
                                                <div class="col-md-12 col-sm-12 col-lg-12">
                                                    <div class="form-group">
                                                        <label class="mb-5">Other Document 4</label><br />
                                                        <span class="MandatoryValue" style="font-size: 12px">(Only jpeg , jpg , png images allowed)</span>

                                                        <asp:FileUpload ID="FileOtherRev4" runat="server" onchange="UploadDoc()" />
                                                        <asp:ImageButton ID="ImgOtherRev4" runat="server" Style="height: 200px; width: 200px;" Visible="false" OnClick="btnImageView_Click" />
                                                        <br />
                                                        <asp:Label ID="lblOtherRev4" runat="server" ForeColor="red"
                                                            Visible="false" />
                                                    </div>
                                                </div>
                                            </div>


                                            <asp:Button Text="Approve" CssClass="btn btn-primary" ID="btnApproveRev" ValidationGroup="AddRev" runat="server" Visible="false" OnClick="btnAdd_Click" />
                                        </div>


                                        <div id="divCancelDocs" runat="server" visible="false">

                                            <div class="row">
                                                <div class="col-md-12 col-sm-12 col-lg-12">
                                                    <div class="form-group">
                                                        <label class="mb-0">CNIC Front</label><span class="MandatoryValue">* </span>
                                                        <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator19" ControlToValidate="FileCNICFront_Cancel" Text="*" ValidationGroup="AddCancelDocs" ForeColor="Red"></asp:RequiredFieldValidator><br />
                                                        <span class="MandatoryValue" style="font-size: 12px">(Only jpeg , jpg , png images allowed)</span>
                                                        <asp:FileUpload ID="FileCNICFront_Cancel" runat="server" onchange="UploadDoc()" />
                                                        <asp:ImageButton ID="Label37" runat="server" Style="height: 200px; width: 200px;" Visible="false" OnClick="btnImageView_Click" />

                                                        <br />
                                                        <asp:Label ID="Label38" runat="server" ForeColor="red"
                                                            Visible="false" />

                                                    </div>
                                                </div>
                                            </div>

                                            <div class="row">
                                                <div class="col-md-12 col-sm-12 col-lg-12">
                                                    <div class="form-group">
                                                        <label class="mb-0">CNIC Back</label><span class="MandatoryValue">* </span>
                                                        <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator20" ControlToValidate="FileCNICBack_Cancel" Text="*" ValidationGroup="AddCancelDocs" ForeColor="Red"></asp:RequiredFieldValidator><br />
                                                        <span class="MandatoryValue" style="font-size: 12px">(Only jpeg , jpg , png images allowed)</span>
                                                        <asp:FileUpload ID="FileCNICBack_Cancel" runat="server" onchange="UploadDoc()" />
                                                        <asp:ImageButton ID="Label39" runat="server" Style="height: 200px; width: 200px;" Visible="false" OnClick="btnImageView_Click" />

                                                        <br />
                                                        <asp:Label ID="Label40" runat="server" ForeColor="red"
                                                            Visible="false" />
                                                    </div>
                                                </div>
                                            </div>

                                            <div class="row">
                                                <div class="col-md-12 col-sm-12 col-lg-12">
                                                    <div class="form-group">
                                                        <label class="mb-5">Residence</label><br />
                                                        <span class="MandatoryValue" style="font-size: 12px">(Only jpeg , jpg , png images allowed)</span>
                                                        <asp:FileUpload ID="FileResidence_Cancel" runat="server" onchange="UploadDoc()" />
                                                        <asp:ImageButton ID="Label41" runat="server" Style="height: 200px; width: 200px;" Visible="false" OnClick="btnImageView_Click" />

                                                        <br />
                                                        <asp:Label ID="Label42" runat="server" ForeColor="red"
                                                            Visible="false" />

                                                    </div>
                                                </div>
                                            </div>


                                            <div class="row">
                                                <div class="col-md-12 col-sm-12 col-lg-12">
                                                    <div class="form-group">
                                                        <label class="mb-5">Vote</label><br />
                                                        <span class="MandatoryValue" style="font-size: 12px">(Only jpeg , jpg , png images allowed)</span>
                                                        <asp:FileUpload ID="FileVote_Cancel" runat="server" onchange="UploadDoc()" />
                                                        <asp:ImageButton ID="Label43" runat="server" Style="height: 200px; width: 200px;" Visible="false" OnClick="btnImageView_Click" />

                                                        <br />
                                                        <asp:Label ID="Label44" runat="server" ForeColor="red"
                                                            Visible="false" />
                                                    </div>
                                                </div>
                                            </div>

                                            <div class="row">
                                                <div class="col-md-12 col-sm-12 col-lg-12">
                                                    <div class="form-group">
                                                        <label class="mb-5">Application</label><br />
                                                        <span class="MandatoryValue" style="font-size: 12px">(Only jpeg , jpg , png images allowed)</span>
                                                        <asp:FileUpload ID="FileApplication_Cancel" runat="server" onchange="UploadDoc()" />

                                                        <asp:ImageButton ID="Label45" runat="server" Style="height: 200px; width: 200px;" Visible="false" OnClick="btnImageView_Click" />
                                                        <br />
                                                        <asp:Label ID="Label46" runat="server" ForeColor="red"
                                                            Visible="false" />
                                                    </div>
                                                </div>
                                            </div>

                                            <div class="row">
                                                <div class="col-md-12 col-sm-12 col-lg-12">
                                                    <div class="form-group">
                                                        <label class="mb-5">Affidevit</label><br />
                                                        <span class="MandatoryValue" style="font-size: 12px">(Only jpeg , jpg , png images allowed)</span>
                                                        <asp:FileUpload ID="FileAffidevit_Cancel" runat="server" onchange="UploadDoc()" />

                                                        <asp:ImageButton ID="Label47" runat="server" Style="height: 200px; width: 200px;" Visible="false" OnClick="btnImageView_Click" />
                                                        <br />
                                                        <asp:Label ID="Label48" runat="server" ForeColor="red"
                                                            Visible="false" />
                                                    </div>
                                                </div>
                                            </div>



                                            <div class="row">
                                                <div class="col-md-12 col-sm-12 col-lg-12">
                                                    <div class="form-group">
                                                        <label class="mb-5">Approval</label><span class="MandatoryValue">* </span>
                                                        <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator25" ControlToValidate="FileApproval_Cancel" Text="*" ValidationGroup="AddCancelDocs" ForeColor="Red"></asp:RequiredFieldValidator><span class="MandatoryValue" style="font-size: 12px">(Only jpeg , jpg , png images allowed)</span><br />
                                                        <asp:FileUpload ID="FileApproval_Cancel" runat="server" onchange="UploadDoc()" />

                                                        <asp:ImageButton ID="Label49" runat="server" Style="height: 200px; width: 200px;" Visible="false" OnClick="btnImageView_Click" />
                                                        <br />
                                                        <asp:Label ID="Label50" runat="server" ForeColor="red"
                                                            Visible="false" />
                                                    </div>
                                                </div>
                                            </div>

                                            <div class="row">
                                                <div class="col-md-12 col-sm-12 col-lg-12">
                                                    <div class="form-group">
                                                        <label class="mb-5">Other Doc 1</label><br />
                                                        <span class="MandatoryValue" style="font-size: 12px">(Only jpeg , jpg , png images allowed)</span>
                                                        <asp:FileUpload ID="FileOtherDoc1_Cancel" runat="server" onchange="UploadDoc()" />

                                                        <asp:ImageButton ID="Label51" runat="server" Style="height: 200px; width: 200px;" Visible="false" OnClick="btnImageView_Click" />
                                                        <br />
                                                        <asp:Label ID="Label52" runat="server" ForeColor="red"
                                                            Visible="false" />
                                                    </div>
                                                </div>
                                            </div>


                                            <div class="row">
                                                <div class="col-md-12 col-sm-12 col-lg-12">
                                                    <div class="form-group">
                                                        <label class="mb-5">OtherDoc 2</label><br />
                                                        <span class="MandatoryValue" style="font-size: 12px">(Only jpeg , jpg , png images allowed)</span>
                                                        <asp:FileUpload ID="FileOtherDoc2_Cancel" runat="server" onchange="UploadDoc()" />

                                                        <asp:ImageButton ID="Label53" runat="server" Style="height: 200px; width: 200px;" Visible="false" OnClick="btnImageView_Click" />
                                                        <br />
                                                        <asp:Label ID="Label54" runat="server" ForeColor="red"
                                                            Visible="false" />
                                                    </div>
                                                </div>
                                            </div>



                                            <div class="row">
                                                <div class="col-md-12 col-sm-12 col-lg-12">
                                                    <div class="form-group">
                                                        <label class="mb-5">OtherDoc 3</label><br />
                                                        <span class="MandatoryValue" style="font-size: 12px">(Only jpeg , jpg , png images allowed)</span>
                                                        <asp:FileUpload ID="FileCancelOther3" runat="server" onchange="UploadDoc()" />

                                                        <asp:ImageButton ID="ImgCancelOther3" runat="server" Style="height: 200px; width: 200px;" Visible="false" OnClick="btnImageView_Click" />
                                                        <br />
                                                        <asp:Label ID="lblCancelOther3" runat="server" ForeColor="red"
                                                            Visible="false" />
                                                    </div>
                                                </div>
                                            </div>



                                            <div class="row">
                                                <div class="col-md-12 col-sm-12 col-lg-12">
                                                    <div class="form-group">
                                                        <label class="mb-5">OtherDoc 4</label><br />
                                                        <span class="MandatoryValue" style="font-size: 12px">(Only jpeg , jpg , png images allowed)</span>
                                                        <asp:FileUpload ID="FileCancelOther4" runat="server" onchange="UploadDoc()" />

                                                        <asp:ImageButton ID="ImgCancelOther4" runat="server" Style="height: 200px; width: 200px;" Visible="false" OnClick="btnImageView_Click" />
                                                        <br />
                                                        <asp:Label ID="lblCancelOther4" runat="server" ForeColor="red"
                                                            Visible="false" />
                                                    </div>
                                                </div>
                                            </div>


                                            <div class="row">
                                                <div class="col-md-12 col-sm-12 col-lg-12">
                                                    <div class="form-group">
                                                        <label class="mb-5">OtherDoc 5</label><br />
                                                        <span class="MandatoryValue" style="font-size: 12px">(Only jpeg , jpg , png images allowed)</span>
                                                        <asp:FileUpload ID="FileCancelOther5" runat="server" onchange="UploadDoc()" />

                                                        <asp:ImageButton ID="ImgCancelOther5" runat="server" Style="height: 200px; width: 200px;" Visible="false" OnClick="btnImageView_Click" />
                                                        <br />
                                                        <asp:Label ID="lblCancelOther5" runat="server" ForeColor="red"
                                                            Visible="false" />
                                                    </div>
                                                </div>
                                            </div>


                                            <asp:Button Text="Approve" CssClass="btn btn-primary" ID="btnApproveCancel" ValidationGroup="AddCancelDocs" runat="server" Visible="false" OnClick="btnAdd_Click" />

                                        </div>



                                        <div id="divSuperAdmin_AdditionalDocs" runat="server" visible="false">

                                            <div class="row">
                                                <div class="col-md-12 col-sm-12 col-lg-12">
                                                    <div class="form-group">
                                                        <label class="mb-0">Admin Docs</label>
                                                        <br />
                                                        <span class="MandatoryValue" style="font-size: 12px">(Only jpeg , jpg , png images allowed)</span>
                                                        <asp:FileUpload ID="FileUpload_AdminDoc" runat="server" onchange="UploadDoc()" />
                                                        <asp:ImageButton ID="ImageButton_AdminDoc" runat="server" Style="height: 200px; width: 200px;" Visible="false" OnClick="btnImageView_Click" />
                                                        <br />
                                                        <asp:Label ID="lbl_AdminDoc" runat="server" ForeColor="red"
                                                            Visible="false" />
                                                    </div>
                                                </div>
                                            </div>

                                            <div class="row">
                                                <div class="col-md-12 col-sm-12 col-lg-12">
                                                    <div class="form-group">
                                                        <label class="mb-0">Authority's Remarks</label>
                                                        <br />
                                                        <span class="MandatoryValue" style="font-size: 12px">(Only jpeg , jpg , png images allowed)</span>
                                                        <asp:FileUpload ID="FileUpload_AuthRemarks" runat="server" onchange="UploadDoc()" />
                                                        <asp:ImageButton ID="ImageButton_AuthRemarks" runat="server" Style="height: 200px; width: 200px;" Visible="false" OnClick="btnImageView_Click" />

                                                        <br />
                                                        <asp:Label ID="lbl_AuthRemarks" runat="server" ForeColor="red"
                                                            Visible="false" />
                                                    </div>
                                                </div>
                                            </div>


                                            <div class="row">
                                                <div class="col-md-12 col-sm-12 col-lg-12">
                                                    <div class="form-group">
                                                        <label class="mb-0">Inquiry Report</label>
                                                        <br />
                                                        <span class="MandatoryValue" style="font-size: 12px">(Only jpeg , jpg , png images allowed)</span>
                                                        <asp:FileUpload ID="FileUpload_InqReport" runat="server" onchange="UploadDoc()" />
                                                        <asp:ImageButton ID="ImageButton_InqReport" runat="server" Style="height: 200px; width: 200px;" Visible="false" OnClick="btnImageView_Click" />

                                                        <br />
                                                        <asp:Label ID="lbl_InqReport" runat="server" ForeColor="red"
                                                            Visible="false" />
                                                    </div>
                                                </div>
                                            </div>

                                              <asp:Button Text="Upload" CssClass="btn btn-primary" ID="btnSuperAdminDocs"  runat="server" Visible="false" OnClick="btnAdd_Click" /><br />
                                        </div>


                                        <asp:Button ID="btnUpload" Text="Upload" runat="server" OnClick="Upload" Style="display: none" />

                                        <div id="divApproverSubmit" runat="server" visible="false">
                                            <div class="row" style="margin-bottom: 5px;">
                                                <div class="col-md-12 col-sm-12 col-lg-12">
                                                    <div>
                                                        <label class="mb-5">Select Status</label>
                                                        <span class="MandatoryValue">* </span>
                                                        <asp:RequiredFieldValidator runat="server" ErrorMessage="*" ControlToValidate="ddlStatus" InitialValue="0" ValidationGroup="AddStatus" ForeColor="Red" />
                                                        <asp:DropDownList runat="server" ID="ddlStatus" CssClass="form-control" OnSelectedIndexChanged="ddlStatus_OnSelectedIndexChanged" AutoPostBack="true">
                                                            <asp:ListItem Value="0">--Select Status--</asp:ListItem>
                                                            <asp:ListItem Value="3">Reject</asp:ListItem>
                                                            <asp:ListItem Value="4">Object</asp:ListItem>
                                                            <asp:ListItem Value="5">Approved</asp:ListItem>
                                                        </asp:DropDownList>

                                                    </div>
                                                </div>
                                            </div>

                                            <div class="row">
                                                <div class="col-md-12 col-sm-12 col-lg-12">
                                                    <div runat="server" id="divObjection_Rejection" visible="false">
                                                        <label class="mb-5">Comments</label>
                                                        <span class="MandatoryValue">* </span>
                                                        <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator1" ControlToValidate="txtObjection" Text="*" ValidationGroup="AddStatus" ForeColor="Red"></asp:RequiredFieldValidator>
                                                        <asp:TextBox ID="txtObjection" runat="server" CssClass="form-control" autocomplete="off"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>



                                            <asp:Button Text="Submit" CssClass="btn btn-primary" Style="margin-top: 5px;" ID="btnSubmit" runat="server" OnClick="btnSubmit_Click" ValidationGroup="AddStatus" />
                                        </div>



                                    </div>
                                </div>
                            </div>
                        </div>

                    </div>
                </div>



                <%--Image Modal Start--%>

                <input type="button" data-toggle="modal" data-target="#ImageModal" class="OpenImageModel" style="display: none;" />
                <div class="modal fade in inmodal " id="ImageModal" tabindex="-1" role="dialog" aria-hidden="true" data-backdrop="static">
                    <div class="modal-dialog">
                        <div class="modal-content animated">
                            <div class="modal-header view-image" style="padding-bottom: 9px; padding-top: 9px; text-align: center">

                                <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                                    <span aria-hidden="true">&times;</span>
                                </button>
                            </div>
                            <asp:Image ID="ImgDoc" runat="server" Height="650" Width="600" />

                            <div class="row">
                                <div class="col-md-3 col-sm-3 col-lg-3">
                                    <br />

                                </div>
                            </div>

                        </div>


                    </div>
                </div>
                <%--Image Modal End--%>



                <%--Forms/Receipts Modal Start--%>

                <input type="button" data-toggle="modal" data-target="#SuccessModel" class="OpenSuccessModal" style="display: none;" />
                <div class="modal fade in inmodal " id="SuccessModel" tabindex="-1" role="dialog" aria-hidden="true" data-backdrop="static">
                    <div class="modal-dialog">
                        <div class="modal-content animated">
                            <div class="modal-header" style="padding-bottom: 9px; padding-top: 9px; text-align: center">
                                <h5 class="modal-title">
                                    <asp:Label runat="server" ID="lblSuccess"></asp:Label>.</h5>
                            </div>


                            <div class="modal-body" style="align-items: center">
                                <div class="row" id="divReceipt" runat="server" visible="false">
                                    <div class="col-md-3 col-sm-3 col-lg-3">
                                        <br />

                                    </div>
                                </div>
                                <div class="row" id="divApplciationFormPDF" runat="server" visible="false">
                                    <div class="col-md-3 col-sm-3 col-lg-3">
                                        <br />

                                    </div>
                                </div>
                                <div class="row" id="divApplciationFormExcel" runat="server" visible="false">
                                    <div class="col-md-3 col-sm-3 col-lg-3">
                                        <br />


                                    </div>
                                </div>

                                <div class="row">
                                    <div class="col-md-3 col-sm-3 col-lg-3">
                                        <br />

                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>



                <%--Forms/Receipts Modal End--%>
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="btnUpload" />
        </Triggers>
    </asp:UpdatePanel>










    <script type="text/javascript">

        function OpenImageModel(hyperlink) {
            $('.OpenImageModel').click();
        }

        function OpenSuccessModal() {
            $('.OpenSuccessModal').click();
        }

        function UploadDoc() {
            document.getElementById("<%=btnUpload.ClientID %>").click();
        }

        function AlertBox(title, Message, type) {
            swal(title, Message, type);
        }

    </script>

</asp:Content>

