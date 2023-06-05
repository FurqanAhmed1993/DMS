<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/AdminMaster.master" AutoEventWireup="true" CodeFile="DomicileFormText.aspx.cs" Inherits="Pages_DomicileFormText" ValidateRequest="true" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit.HTMLEditor" TagPrefix="cc1" %>
<%@ Register Src="~/CustomControls/Shared/InProgress.ascx" TagPrefix="uc" TagName="InProgress" %>
<%@ Register Src="~/CustomControls/Shared/PagingAndSorting.ascx" TagPrefix="uc" TagName="PagingAndSorting" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:UpdateProgress ID="UpdateProgress2" runat="server">
        <ProgressTemplate>
            <uc:InProgress runat="server" ID="InProgress" />
        </ProgressTemplate>
    </asp:UpdateProgress>
    <div class="row heading-bg">
        <div class="col-lg-6 col-md-6 col-sm-6 col-xs-12">
            <h5 class="txt-dark">
                <asp:Label ID="lblHeading" runat="server" />
            </h5>
        </div>
    </div>
    <asp:UpdatePanel ID="upData" runat="server">
        <ContentTemplate>
            <input type="hidden" runat="server" id="IsAdd" value="0" />
            <input type="hidden" runat="server" id="IsView" value="0" />
            <input type="hidden" runat="server" id="IsEdit" value="0" />
            <input type="hidden" runat="server" id="IsDelete" value="0" />

            <div class="panel panel-default card-view">
                <div class="panel-wrapper collapse in">
                    <div class="panel-body">


                        <div class="row">
                            <div class="col-md-3 col-sm-3">
                                <asp:DropDownList runat="server" ID="ddlHeaderArea" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlHeader_SelectedChanged">

                                    <asp:ListItem Value="">--Select Middle Area Values--</asp:ListItem>
                                    <asp:ListItem Value="@Name">Applicant Name</asp:ListItem>
                                    <asp:ListItem Value="@FName">Father Name</asp:ListItem>
                                    <asp:ListItem Value="@Cast">Cast</asp:ListItem>
                                    <asp:ListItem Value="@PlaceOfBirth">Place of Birth</asp:ListItem>
                                    <asp:ListItem Value="@ACnic">Applicant CNIC</asp:ListItem>
                                    <asp:ListItem Value="@PlaceIssueDomicile">Place of Issue of Domicile</asp:ListItem>
                                    <asp:ListItem Value="@Single/Married/Widow">Single/Married/Widow</asp:ListItem>
                                    <asp:ListItem Value="@Wife_HusbandName">Name of Wife/Husband</asp:ListItem>
                                    <asp:ListItem Value="@DoB">Date of Birth</asp:ListItem>
                                    <asp:ListItem Value="@ResidentOf">Resident of</asp:ListItem>
                                    <asp:ListItem Value="@Taluka">Taluka</asp:ListItem>
                                    <asp:ListItem Value="@EducationDetails">Education Details</asp:ListItem>
                                    <asp:ListItem Value="@Sr.No">Sr.No</asp:ListItem>
                                    <asp:ListItem Value="@Deh">Deh</asp:ListItem>
                                    <asp:ListItem Value="@FatherCNIC">Father CNIC</asp:ListItem>
                                    <asp:ListItem Value="@TempAddress">Temporary Address</asp:ListItem>
                                    <asp:ListItem Value="@PermanentAddress">Permanent Address</asp:ListItem>
                                    <asp:ListItem Value="@GuardianDomicileDate">Guardian Domicile Certificate Date</asp:ListItem>
                                    <asp:ListItem Value="@ParticularsProperty">Particulars Property Name</asp:ListItem>
                                    <asp:ListItem Value="@ParticularsLocation">Particulars Property Location</asp:ListItem>
                                    <asp:ListItem Value="@DomicileApprovalDate">Domicile Approval Date</asp:ListItem>
                                    <asp:ListItem Value="@ForeignAddress">Foreign Address</asp:ListItem>
                                    <asp:ListItem Value="@DateArrival">Date of Arrival at Domicile Place</asp:ListItem>
                                    <asp:ListItem Value="@TradeOccupation">Trade Occupation</asp:ListItem>
                                    <asp:ListItem Value="@MarkIdentification">Mark of Identification</asp:ListItem>
                                    <asp:ListItem Value="@DomicileApprovalDate">Domicile Approval Date</asp:ListItem>
                                    <asp:ListItem Value="@Educatedat">Educated At</asp:ListItem>
                                    <asp:ListItem Value="@DeputyCommissioner">Deputy Commissioner Name</asp:ListItem>
                                </asp:DropDownList>
                            </div>
                            <div class="col-md-3 col-sm-3">Token Value 
                                <asp:TextBox ID="txtheaderToken" runat="server"></asp:TextBox></div>
                        </div>

                        <div class="row">
                            <div class="col-md-3 col-sm-3">
                                <label class="mb-5">Header Area</label><span class="MandatoryValue">* </span>
                                <div class="form-group">


                                    <cc1:Editor ID="txtHeaderArea" runat="server" CssClass="RichBoxValidate" Height="100" Width="1200"></cc1:Editor>

                                </div>
                            </div>
                        </div>
                        <br />

                        <div class="row">
                            <div class="col-md-3 col-sm-3">
                                <asp:DropDownList runat="server" ID="ddlMiddleArea" CssClass="form-control" OnSelectedIndexChanged="ddlMiddle_SelectedChanged" AutoPostBack="true">
                                    <asp:ListItem Value="">--Select Middle Area Values--</asp:ListItem>
                                    <asp:ListItem Value="@Name">Applicant Name</asp:ListItem>
                                    <asp:ListItem Value="@FName">Father Name</asp:ListItem>
                                    <asp:ListItem Value="@Cast">Cast</asp:ListItem>
                                    <asp:ListItem Value="@PlaceOfBirth">Place of Birth</asp:ListItem>
                                    <asp:ListItem Value="@ACnic">Applicant CNIC</asp:ListItem>
                                    <asp:ListItem Value="@PlaceIssueDomicile">Place of Issue of Domicile</asp:ListItem>
                                    <asp:ListItem Value="@Single/Married/Widow">Single/Married/Widow</asp:ListItem>
                                    <asp:ListItem Value="@Wife_HusbandName">Name of Wife/Husband</asp:ListItem>
                                    <asp:ListItem Value="@DoB">Date of Birth</asp:ListItem>
                                    <asp:ListItem Value="@ResidentOf">Resident of</asp:ListItem>
                                    <asp:ListItem Value="@Taluka">Taluka</asp:ListItem>
                                    <asp:ListItem Value="@EducationDetails">Education Details</asp:ListItem>
                                    <asp:ListItem Value="@Sr.No">Sr.No</asp:ListItem>
                                    <asp:ListItem Value="@Deh">Deh</asp:ListItem>
                                    <asp:ListItem Value="@FatherCNIC">Father CNIC</asp:ListItem>
                                    <asp:ListItem Value="@TempAddress">Temporary Address</asp:ListItem>
                                    <asp:ListItem Value="@PermanentAddress">Permanent Address</asp:ListItem>
                                    <asp:ListItem Value="@GuardianDomicileDate">Guardian Domicile Certificate Date</asp:ListItem>
                                    <asp:ListItem Value="@ParticularsProperty">Particulars Property Name</asp:ListItem>
                                    <asp:ListItem Value="@ParticularsLocation">Particulars Property Location</asp:ListItem>
                                    <asp:ListItem Value="@DomicileApprovalDate">Domicile Approval Date</asp:ListItem>
                                    <asp:ListItem Value="@ForeignAddress">Foreign Address</asp:ListItem>
                                    <asp:ListItem Value="@DateArrival">Date of Arrival at Domicile Place</asp:ListItem>
                                    <asp:ListItem Value="@TradeOccupation">Trade Occupation</asp:ListItem>
                                    <asp:ListItem Value="@MarkIdentification">Mark of Identification</asp:ListItem>
                                    <asp:ListItem Value="@DomicileApprovalDate">Domicile Approval Date</asp:ListItem>
                                    <asp:ListItem Value="@Educatedat">Educated At</asp:ListItem>
                                    <asp:ListItem Value="@DeputyCommissioner">Deputy Commissioner Name</asp:ListItem>
                                </asp:DropDownList>
                            </div>
                            <div class="col-md-3 col-sm-3">
                                Token Value 
                                <asp:TextBox ID="txtMiddleToken" runat="server"></asp:TextBox>
                            </div>
                        </div>
                        <br />
                        <br />

                        <div class="row">
                            <div class="col-md-3 col-sm-3">
                                <label class="mb-5">Middle Area</label><span class="MandatoryValue">* </span>
                                <div class="form-group">

                                    <cc1:Editor ID="txtMiddleArea" runat="server" CssClass="RichBoxValidate" Height="800" Width="1200"></cc1:Editor>

                                </div>
                            </div>
                        </div>

                        <div class="row">
                            <div class="col-md-3 col-sm-3">
                                <asp:DropDownList runat="server" ID="ddlLowerArea" CssClass="form-control" OnSelectedIndexChanged="ddlLower_SelectedChanged" AutoPostBack="true">
                                    <asp:ListItem Value="">--Select Lower Area Values--</asp:ListItem>
                                    <asp:ListItem Value="@Name">Applicant Name</asp:ListItem>
                                    <asp:ListItem Value="@FName">Father Name</asp:ListItem>
                                    <asp:ListItem Value="@Cast">Cast</asp:ListItem>
                                    <asp:ListItem Value="@ACnic">Applicant CNIC</asp:ListItem>
                                    <asp:ListItem Value="@PlaceIssueDomicile">Place of Issue of Domicile</asp:ListItem>
                                    <asp:ListItem Value="@Single/Married/Widow">Single/Married/Widow</asp:ListItem>
                                    <asp:ListItem Value="@Wife_HusbandName">Name of Wife/Husband</asp:ListItem>
                                    <asp:ListItem Value="@DoB">Date of Birth</asp:ListItem>
                                    <asp:ListItem Value="@ResidentOf">Resident of</asp:ListItem>
                                    <asp:ListItem Value="@Taluka">Taluka</asp:ListItem>
                                    <asp:ListItem Value="@EducationDetails">Education Details</asp:ListItem>
                                    <asp:ListItem Value="@Sr.No">Sr.No</asp:ListItem>
                                    <asp:ListItem Value="@Deh">Deh</asp:ListItem>
                                    <asp:ListItem Value="@FatherCNIC">Father CNIC</asp:ListItem>
                                    <asp:ListItem Value="@TempAddress">Temporary Address</asp:ListItem>
                                    <asp:ListItem Value="@PermanentAddress">Permanent Address</asp:ListItem>
                                    <asp:ListItem Value="@GuardianDomicileDate">Guardian Domicile Certificate Date</asp:ListItem>
                                    <asp:ListItem Value="@ParticularsProperty">Particulars Property Name</asp:ListItem>
                                    <asp:ListItem Value="@ParticularsLocation">Particulars Property Location</asp:ListItem>
                                    <asp:ListItem Value="@DeputyCommissioner">Deputy Commissioner Name</asp:ListItem>
                                </asp:DropDownList>
                            </div>
                            <div class="col-md-3 col-sm-3">Token Value
                                <asp:TextBox ID="txtLowerToken" runat="server"></asp:TextBox></div>
                        </div>
                        <br />
                        <br />


                        <div class="row">
                            <div class="col-md-3 col-sm-3">
                                <label class="mb-5">Lower Area</label><span class="MandatoryValue">* </span>
                                <div class="form-group">
                                    <cc1:Editor ID="txtLowerArea" runat="server" CssClass="RichBoxValidate" Height="800" Width="1200"></cc1:Editor>

                                </div>
                            </div>
                        </div>

                        <div class="row text-right">
                            <div class="text-right mb-15">

                                <asp:Button ID="btnUpdate" CssClass="btn btn-primary" runat="server" Text="Update" OnClick="btnUpdate_Click" />
                                <asp:Button ID="btnCancel" CssClass="btn btn-default" runat="server" Text="Cancel" OnClick="btnCancel_Click" />

                            </div>
                        </div>
                    </div>
                </div>
            </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>


    <script type="text/javascript">
        function pageLoad() {

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


            var oInput = document.getElementById("<%=txtHeaderArea.ClientID%>");
            oInput.focus();
            oInput.value += "";


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


        function insertAtCaret(text) {
            debugger;
            var txtarea = document.getElementById("<%=txtHeaderArea.ClientID%>");
            if (!txtarea) {
                return;
            }

            var scrollPos = txtarea.scrollTop;
            var strPos = 0;
            var br = ((txtarea.selectionStart || txtarea.selectionStart == '0') ?
                "ff" : (document.selection ? "ie" : false));
            if (br == "ie") {
                txtarea.focus();
                var range = document.selection.createRange();
                range.moveStart('character', -txtarea.value.length);
                strPos = range.text.length;
            } else if (br == "ff") {
                strPos = txtarea.selectionStart;
            }

            var front = (txtarea.value).substring(0, strPos);
            var back = (txtarea.value).substring(strPos, txtarea.value.length);
            txtarea.value = front + text + back;
            strPos = strPos + text.length;
            if (br == "ie") {
                txtarea.focus();
                var ieRange = document.selection.createRange();
                ieRange.moveStart('character', -txtarea.value.length);
                ieRange.moveStart('character', strPos);
                ieRange.moveEnd('character', 0);
                ieRange.select();
            } else if (br == "ff") {
                txtarea.selectionStart = strPos;
                txtarea.selectionEnd = strPos;
                txtarea.focus();
            }

            txtarea.scrollTop = scrollPos;
        }



    </script>
</asp:Content>

