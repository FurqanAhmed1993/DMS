<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/AdminMaster.master" AutoEventWireup="true" CodeFile="ViewApplicationSaved.aspx.cs" Inherits="Pages_Domicile_ViewApplicationSaved" MaintainScrollPositionOnPostback="true" %>

<%@ Register Src="~/CustomControls/Shared/InProgress.ascx" TagPrefix="uc" TagName="InProgress" %>
<%@ Register Src="~/CustomControls/Shared/PagingAndSorting.ascx" TagPrefix="uc" TagName="PagingAndSorting" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">


    <asp:HiddenField ID="hdnApplicationId" runat="server" />
    <asp:HiddenField ID="hdnApplicationType" runat="server" />
    <asp:HiddenField ID="hdnRequestType" runat="server" />

    <asp:UpdateProgress ID="UpdateProgress2" runat="server">
        <ProgressTemplate>
            <uc:InProgress runat="server" ID="InProgress" />
        </ProgressTemplate>
    </asp:UpdateProgress>

    <div class="row heading-bg">
        <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
            <h5 class="txt-dark">Search Application For Domicile
            </h5>
            <asp:Label ID="lblReqType" runat="server"></asp:Label>
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

            <input type="hidden" runat="server" id="IsAdd" value="0" />
            <input type="hidden" runat="server" id="IsView" value="0" />
            <input type="hidden" runat="server" id="IsEdit" value="0" />
            <input type="hidden" runat="server" id="IsDelete" value="0" />
            <asp:HiddenField runat="server" ID="hdnStatusId" Value="0" />

            <div class="panel panel-default card-view">
                <div class="panel-wrapper collapse in">
                    <div class="panel-body">
                        <div class="row">

                            <div class="col-md-3 col-sm-3">
                                <div class="form-group">
                                    <label class="mb-5">
                                        ARN Number</label>
                                    <asp:TextBox ID="txtARNNumber" runat="server" CssClass="form-control" autocomplete="off"></asp:TextBox>
                                </div>
                            </div>

                            <div class="col-md-3 col-sm-3">
                                <div class="form-group">
                                    <label class="mb-5">
                                        Domicile Number</label>
                                    <asp:TextBox ID="txtDomicileNumber" runat="server" CssClass="form-control" autocomplete="off"></asp:TextBox>
                                </div>
                            </div>

                            <div class="col-md-3 col-sm-3">
                                <div class="form-group">
                                    <label class="mb-5">
                                        NIC Number</label>
                                    <asp:TextBox ID="txtNicNumber" runat="server" CssClass="form-control cnic" autocomplete="off"></asp:TextBox>
                                </div>
                            </div>

                            <div class="col-md-3 col-sm-3">
                                <div class="form-group">
                                    <label class="mb-3">Taluka</label>
                                    <asp:DropDownList runat="server" ID="ddlTaluka" CssClass="form-control"></asp:DropDownList>
                                </div>
                            </div>

                        </div>


                        <div class="row">

                            <div class="col-md-3 col-sm-3">
                                <div class="form-group">
                                    <label class="mb-5">From Issuance Date</label>
                                    <asp:TextBox ID="txtFromIssuanceDate" runat="server" CssClass="form-control txtFromDate  date" autocomplete="off"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-md-3 col-sm-3">
                                <div class="form-group">
                                    <label class="mb-5">To Issuance Date</label>
                                    <asp:TextBox ID="txtToIssuanceDate" runat="server" CssClass="form-control txtToDate  date" autocomplete="off"></asp:TextBox>
                                </div>
                            </div>


                            <div class="col-md-3 col-sm-3">
                                <div class="form-group">
                                    <label class="mb-5">From Submission Date</label>
                                    <asp:TextBox ID="txtFromDate" runat="server" CssClass="form-control txtFromDate  date" autocomplete="off"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-md-3 col-sm-3">
                                <div class="form-group">
                                    <label class="mb-5">To Submission Date</label>
                                    <asp:TextBox ID="txtToDate" runat="server" CssClass="form-control txtToDate  date" autocomplete="off"></asp:TextBox>
                                </div>
                            </div>
                        </div>

                        <div class="row">

                            <div class="col-md-3 col-sm-3">
                                <div class="form-group">
                                    <label class="mb-5">From Cancellation Date</label>
                                    <asp:TextBox ID="txtFromCancellationDate" runat="server" CssClass="form-control txtFromDate  date" autocomplete="off"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-md-3 col-sm-3">
                                <div class="form-group">
                                    <label class="mb-5">To Cancellation Date</label>
                                    <asp:TextBox ID="txtToCancellationDate" runat="server" CssClass="form-control txtToDate  date" autocomplete="off"></asp:TextBox>
                                </div>
                            </div>

                            <div class="col-md-3 col-sm-3">
                                <div class="form-group">
                                    <label class="mb-3">Status</label>
                                    <asp:DropDownList runat="server" ID="ddlStatus" CssClass="form-control">
                                        <asp:ListItem Value="0">--Select Title--</asp:ListItem>
                                        <asp:ListItem Value="1">Initial Draft</asp:ListItem>
                                        <asp:ListItem Value="2">Submitted to DDO</asp:ListItem>
                                        <asp:ListItem Value="3">Rejected</asp:ListItem>
                                        <asp:ListItem Value="4">Objected</asp:ListItem>
                                        <asp:ListItem Value="5">Approved</asp:ListItem>
                                        <asp:ListItem Value="6">Disabled</asp:ListItem>
                                        <asp:ListItem Value="7">Cancelled</asp:ListItem>
                                        <asp:ListItem Value="8">Issued</asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                            </div>

                            <div class="col-md-3 col-sm-3">
                                <div class="button-list" style="margin-top: 15px;">
                                    <asp:Button ID="btnSearch" CssClass="btn btn-primary" runat="server" Text="Search" OnClick="btnSearch_Click" />
                                    <asp:Button ID="btnCancel" CssClass="btn btn-default" runat="server" Text="Cancel" OnClick="btnCancel_Click" />
                                </div>
                            </div>

                        </div>

                    </div>
                </div>
            </div>

            <h4><asp:Label runat="server" ID="lblStatus"></asp:Label></h4>

            <div class="panel panel-default border-panel card-view">
                <div class="panel-wrapper collapse in">
                    <div class="panel-body">
                        <div class="text-right mb-15">
                            <asp:Button ID="Btn_Add" runat="server" Visible="false" Text="Add" CssClass="btn btn-primary Btn_Add" OnClick="Btn_Add_Click" />
                        </div>
                        <div class="table-wrap">
                            <div class="table-responsive">
                                <table class="table table-striped display pb-30">
                                    <thead>
                                        <tr>
                                            <th>S.No</th>
                                            <th>ARN Number</th>
                                            <th>CNIC</th>
                                            <th class="AllignCenter">Applicant's Name </th>
                                            <th class="AllignCenter">Domicile No.</th>
                                            <th class="AllignCenter">Application Status</th>
                                            <th class="AllignCenter">Submission Date</th>
                                            <th class="AllignCenter">Issuance Date</th>
                                            <th class="AllignCenter">Action</th>
                                        </tr>
                                    </thead>
                                    <tbody>
                                        <asp:Repeater ID="rpt" runat="server" OnItemDataBound="rpt_ItemDataBound">
                                            <ItemTemplate>
                                                <tr>

                                                    <asp:HiddenField runat="server" ID="hdnAppId" Value='<%# Eval("ApplicationId") %>' />
                                                    <asp:HiddenField ID="hdnApplicationTypeId" runat="server" Value='<%# Eval("ApplicationTypeId") %>' />
                                                    <asp:HiddenField ID="hdnRequestTypeId" runat="server" Value='<%# Eval("RequestTypeId") %>' />
                                                    <asp:HiddenField ID="hdnApplicationStatusId" runat="server" Value='<%# Eval("ApplicationStatusId") %>' />
                                                    <asp:HiddenField ID="hdnCNICFront" runat="server" Value='<%# Eval("CNIC_Front") %>' />
                                                    <asp:HiddenField ID="hdnCNICBack" runat="server" Value='<%# Eval("CNIC_Back") %>' />
                                                    <asp:HiddenField ID="hdnAsstCom" runat="server" Value='<%# Eval("ASSISTANT_COMMISSIONERS_REPORT_Path") %>' />
                                                    <asp:HiddenField ID="hdnMukhRep" runat="server" Value='<%# Eval("MUKHTIARKAR_REPORT_Path") %>' />
                                                    <asp:HiddenField ID="hdnPC" runat="server" Value='<%# Eval("PRIMARY_CERTIFICATE_Path") %>' />
                                                    <asp:HiddenField ID="hdnMC" runat="server" Value='<%# Eval("MATRIC_CERTIFICATE_Path") %>' />
                                                    <asp:HiddenField ID="hdnRC" runat="server" Value='<%# Eval("RESIDENCE_CERTIFICATE_Path") %>' />
                                                    <asp:HiddenField ID="hdnVC" runat="server" Value='<%# Eval("VOTE_CERTIFICATE_Path") %>' />
                                                    <asp:HiddenField ID="hdnGD" runat="server" Value='<%# Eval("GUARDIANS_DOMICILE_Path") %>' />
                                                    <asp:HiddenField ID="hdnBankCh" runat="server" Value='<%# Eval("BANK_CHALLANS_Path") %>' />
                                                    <asp:HiddenField ID="hdnOD1" runat="server" Value='<%# Eval("OTHER_DOCUMENT1_Path") %>' />
                                                    <asp:HiddenField ID="hdnOD2" runat="server" Value='<%# Eval("OTHER_DOCUMENT2_Path") %>' />

                                                    <asp:HiddenField ID="hdnCNICFront_Revise" runat="server" Value='<%# Eval("CNICFront_Revise") %>' />
                                                    <asp:HiddenField ID="hdnCNICBack_Revise" runat="server" Value='<%# Eval("CNICBack_Revise") %>' />
                                                    <asp:HiddenField ID="hdnApproval_Authority_Revise" runat="server" Value='<%# Eval("Approval_Authority_Revise") %>' />
                                                    <asp:HiddenField ID="hdnBank_Challan_Revise" runat="server" Value='<%# Eval("Bank_Challan_Revise") %>' />
                                                    <asp:HiddenField ID="hdnCorrection_Doc1_Revise" runat="server" Value='<%# Eval("Correction_Doc1_Revise") %>' />
                                                    <asp:HiddenField ID="hdnCorrection_Doc2_Revise" runat="server" Value='<%# Eval("Correction_Doc2_Revise") %>' />
                                                    <asp:HiddenField ID="hdnOthersDoc_Revise" runat="server" Value='<%# Eval("OthersDoc_Revise") %>' />


                                                    <asp:HiddenField ID="hdnCNICFront_Dup" runat="server" Value='<%# Eval("CNICFront_Duplicate") %>' />
                                                    <asp:HiddenField ID="hdnCNICBack_Dup" runat="server" Value='<%# Eval("CNICBack_Duplicate") %>' />
                                                    <asp:HiddenField ID="hdnFir_Dup" runat="server" Value='<%# Eval("FirCopy_Duplicate") %>' />
                                                    <asp:HiddenField ID="hdnApproval_Authority_Dup" runat="server" Value='<%# Eval("Approval_Authority_Duplicate") %>' />
                                                    <asp:HiddenField ID="hdnBank_Challan_Dup" runat="server" Value='<%# Eval("Bank_Challan_Duplicate") %>' />
                                                    <asp:HiddenField ID="hdnApplication_Duplicate" runat="server" Value='<%# Eval("Application_Duplicate") %>' />


                                                    <asp:HiddenField ID="hdnCNICFront_Cancel" runat="server" Value='<%# Eval("CNICFront_Cancel") %>' />
                                                    <asp:HiddenField ID="hdnCNICBack_Cancel" runat="server" Value='<%# Eval("CNICBack_Cancel") %>' />
                                                    <asp:HiddenField ID="hdnResidence_Cancel" runat="server" Value='<%# Eval("Residence_Cancel") %>' />
                                                    <asp:HiddenField ID="hdnVote_Cancel" runat="server" Value='<%# Eval("Vote_Cancel") %>' />
                                                    <asp:HiddenField ID="hdnApplication_Cancel" runat="server" Value='<%# Eval("Application_Cancel") %>' />
                                                    <asp:HiddenField ID="hdnAffidevit_Cancel" runat="server" Value='<%# Eval("Affidevit_Cancel") %>' />
                                                    <asp:HiddenField ID="hdnApproval_Cancel" runat="server" Value='<%# Eval("Approval_Cancel") %>' />
                                                    <asp:HiddenField ID="hdnOther_Cancel" runat="server" Value='<%# Eval("OthersDoc_Cancel") %>' />
                                                    <asp:HiddenField ID="hdnOthe2_Cancel" runat="server" Value='<%# Eval("OthersDoc1_Cancel") %>' />


                                                    <td>
                                                        <%#(((RepeaterItem)Container).ItemIndex+1).ToString()%>
                                                    </td>
                                                    <td>
                                                        <%# Eval("Application_RefNo") %>
                                                    </td>
                                                    <td class="AllignCenter">

                                                        <%# Eval("Applicant_Cnic") %>
                                                      
                                                    </td>
                                                    <td class="AllignCenter">
                                                        <%# Eval("Applicant_Name") %>
                                                    </td>
                                                    <td class="AllignCenter">
                                                        <%# Eval("Domicile_No") %>
                                                    </td>
                                                    <td class="AllignCenter">
                                                        <%# Eval("Status") %>
                                                        <asp:HiddenField ID="hdnStatusReperter" runat="server" Value='<%# Eval("Status") %>' />
                                                    </td>
                                                    <td class="AllignCenter">
                                                        <%# Eval("CreatedDate" , "{0:dd/MM/yyyy}") %>
                                                    </td>
                                                    <td></td>
                                                    <td class="AllignCenter">
                                                        <asp:ImageButton ID="lbEdit" ImageUrl="~/Assets/Images/edit-icon.png" runat="server" Text="Edit" ToolTip="Edit" OnClick="lbEdit_Click" />
                                                        &nbsp
                                                             <asp:ImageButton ID="lnkViewForm" ImageUrl="~/Assets/Images/doc.png" runat="server" Text="View Form" ToolTip="View Form" OnClick="lbFormView_Click" />
                                                        &nbsp
                                                             <asp:ImageButton ID="lnkView" ImageUrl="~/Assets/Images/view.png" runat="server" Text="View" ToolTip="View Details" OnClick="lnkView_Click" />
                                                        &nbsp;
                                                         <asp:ImageButton ID="lbDelete" ImageUrl="~/Assets/Images/delete-icon.png" runat="server" Text="Remove" ToolTip="Remove" OnClick="lbDelete_Click" />
                                                        &nbsp;
                                                         <asp:LinkButton ID="lbEnable"  runat="server"  Text="Enable" OnClick="lbEnable_Click" />
                                                        <%-- <asp:ImageButton ID="lbDelete" ImageUrl="~/Assets/Images/delete-icon.png" runat="server" Text="Remove" ToolTip="Remove" OnClick="lbDelete_Click" />--%>
                                                    </td>
                                                </tr>
                                            </ItemTemplate>
                                        </asp:Repeater>
                                    </tbody>
                                </table>

                                <uc:PagingAndSorting runat="server" ID="PagingAndSorting" />
                            </div>
                        </div>
                    </div>
                </div>
            </div>

        </ContentTemplate>
    </asp:UpdatePanel>


     <%-- Modal Start--%>
    <input type="button" data-toggle="modal" data-target="#DeleteModel" class="OpenDeleteModal" style="display: none;" />
    <div class="modal fade in inmodal " id="DeleteModel" tabindex="-1" role="dialog" aria-hidden="true" data-backdrop="static">
        <div class="modal-dialog">
            <div class="modal-content animated">
                <div class="modal-header" style="padding-bottom: 9px; padding-top: 9px; text-align: center">
                    <h5 class="modal-title">Delete Application</h5>
                </div>
                <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                    <ContentTemplate>
                        <div class="modal-body">
                            <input type="hidden" id="hfId" runat="server" class="hfId" value="0" />
                            <div class="row">
                                <div class="col-md-12 col-sm-12 col-lg-12">
                                    <div class="form-group">
                                        <label class="mb-5">Comments</label>
                                        <span class="MandatoryValue">* </span>

                                        <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator3" ControlToValidate="txtDeleteComments" Text="*" ValidationGroup="Add" ForeColor="Red"></asp:RequiredFieldValidator>
                                        <asp:TextBox ID="txtDeleteComments" runat="server" CssClass="form-control txtValue clsTextBox" AutoCompleteType="Disabled" Width="50%"></asp:TextBox>
                                    </div>
                                </div>
                            </div>

                        </div>
                        <div class="modal-footer">
                            <asp:Button Text="Save" CssClass="btn btn-primary" ID="btnAdd" ValidationGroup="Add" runat="server" OnClick="btnAdd_Click" />
                            <asp:Button Text="Cancel" ID="btnCancelDelete" runat="server" CssClass="btnCancelDelete btn btn-default" data-dismiss="modal" />
                        </div>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
        </div>
    </div>
    <%-- Modal End--%>



    <script type="text/javascript">
        function pageLoad() {

            $(".cnic").mask("99999-9999999-9");

            $('.date').datepicker({
                forceParse: false,
                calendarWeeks: true,
                autoclose: true,
                format: 'dd-MM-yyyy',
            });
            $('.date').keydown(function () {
                return false;
            });

        }


        function ClosePopup() {
            $('.hfId').val("0");
            $('.btnCancelEdit').click();
        }

        function OpenDeletePopup() {
            $('.OpenDeleteModal').click();
        }
        function CloseDeletePopup() {
            $('.btnCancelDelete').click();
        }
        function AlertBox(title, Message, type) {
            swal(title, Message, type);
        }
    </script>

</asp:Content>

