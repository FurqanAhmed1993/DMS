<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/AdminMaster.master" AutoEventWireup="true" CodeFile="IssuanceRegister.aspx.cs" Inherits="Pages_Domicile_IssuanceRegister" %>

<%@ Register Src="~/CustomControls/Shared/InProgress.ascx" TagPrefix="uc" TagName="InProgress" %>
<%@ Register Src="~/CustomControls/Shared/PagingAndSorting.ascx" TagPrefix="uc" TagName="PagingAndSorting" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <div class="row heading-bg">
        <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
            <h5 class="txt-dark">Issuance Register
            </h5>
        </div>
    </div>

    <asp:UpdateProgress ID="UpdateProgress2" runat="server" AssociatedUpdatePanelID="upData">
        <ProgressTemplate>
            <uc:InProgress runat="server" ID="InProgress" />
        </ProgressTemplate>
    </asp:UpdateProgress>

    <asp:UpdatePanel ID="upData" runat="server">
        <ContentTemplate>

               <input type="hidden" runat="server" id="IsAdd" value="0" />
            <input type="hidden" runat="server" id="IsView" value="0" />
            <input type="hidden" runat="server" id="IsEdit" value="0" />
            <input type="hidden" runat="server" id="IsDelete" value="0" />

            <div class="alert alert-danger" role="alert" id="divAlert" runat="server" visible="false">
                <asp:Label runat="server" ID="lblErrorDiv"></asp:Label>
            </div>

              <div class="alert alert-success" role="alert" id="divSuccess" runat="server" visible="false">
                <asp:Label runat="server" ID="lblSuccessDiv"></asp:Label>
            </div>

            <div class="panel panel-default card-view">
                <div class="panel-wrapper collapse in">
                    <div class="panel-body">
                        <div class="row">

                            <div class="col-md-3 col-sm-3">
                                <div class="form-group">
                                    <label class="mb-5">
                                        ARN Number</label><span class="MandatoryValue">* </span>
                                    <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator9" ControlToValidate="txtARNNumber" Text="*" ValidationGroup="Search" ForeColor="Red"></asp:RequiredFieldValidator>
                                    <asp:TextBox ID="txtARNNumber" runat="server" CssClass="form-control" autocomplete="off"></asp:TextBox>
                                </div>
                            </div>

                            <div class="col-md-9 col-sm-9">
                                <div class="button-list" style="margin-top: 15px;">
                                    <asp:Button ID="btnSearch" CssClass="btn btn-primary" runat="server" Text="Search" OnClick="btnSearch_Click" ValidationGroup="Search" />
                                    <asp:Button ID="btnCancel" CssClass="btn btn-default" runat="server" Text="Cancel" OnClick="btnCancel_Click" />
                                </div>
                            </div>


                        </div>
                    </div>
                </div>

                <h4>Issue Domicile</h4>

                <div class="panel panel-default border-panel card-view">
                    <div class="panel-wrapper collapse in">
                        <div class="panel-body">

                            <div class="row">

                                <div class="col-md-3 col-sm-3">
                                    <div class="form-group">
                                        <label class="mb-5">Issuance Date</label>
                                        <span class="MandatoryValue">* </span>
                                        <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator8" ControlToValidate="txtIssuanceDate" Text="*" ValidationGroup="Add" ForeColor="Red"></asp:RequiredFieldValidator>
                                        <asp:TextBox ID="txtIssuanceDate" runat="server" CssClass="form-control txtFromDate  date" AutoCompleteType="Disabled" Enabled="false"></asp:TextBox>
                                    </div>
                                </div>

                                <div class="col-md-3 col-sm-3">
                                    <div class="form-group">
                                        <label class="mb-5">
                                            Module Name</label><span class="MandatoryValue">* </span>
                                        <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator7" ControlToValidate="txtModule" Text="*" ValidationGroup="Add" ForeColor="Red"></asp:RequiredFieldValidator>
                                        <asp:TextBox ID="txtModule" runat="server" CssClass="form-control" autocomplete="off" Enabled="false" Text="Domicile"></asp:TextBox>
                                    </div>
                                </div>

                                <div class="col-md-3 col-sm-3">
                                    <div class="form-group">
                                        <label class="mb-5">
                                            Sub Module Name</label><span class="MandatoryValue">* </span>
                                        <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator6" ControlToValidate="txtSubModule" Text="*" ValidationGroup="Add" ForeColor="Red"></asp:RequiredFieldValidator>
                                        <asp:TextBox ID="txtSubModule" runat="server" CssClass="form-control" autocomplete="off" Enabled="false"></asp:TextBox>
                                    </div>
                                </div>

                                <div class="col-md-3 col-sm-3">
                                    <div class="form-group">
                                        <label class="mb-5">
                                            Applicant Name</label><span class="MandatoryValue">* </span>
                                        <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator4" ControlToValidate="txtApplicantName" Text="*" ValidationGroup="Add" ForeColor="Red"></asp:RequiredFieldValidator>
                                        <asp:TextBox ID="txtApplicantName" runat="server" CssClass="form-control" autocomplete="off" Enabled="false"></asp:TextBox>
                                    </div>
                                </div>

                            </div>

                            <div class="row">

                                <div class="col-md-3 col-sm-3">
                                    <div class="form-group">
                                        <label class="mb-5">
                                            Father Name</label><span class="MandatoryValue">* </span>
                                        <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator1" ControlToValidate="txtFatherName" Text="*" ValidationGroup="Add" ForeColor="Red"></asp:RequiredFieldValidator>
                                        <asp:TextBox ID="txtFatherName" runat="server" CssClass="form-control" autocomplete="off" Enabled="false"></asp:TextBox>
                                    </div>
                                </div>

                                <div class="col-md-3 col-sm-3">
                                    <div class="form-group">
                                        <label class="mb-5">
                                            Name of Receiver</label><span class="MandatoryValue">* </span>
                                        <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator5" ControlToValidate="txtReceiverName" Text="*" ValidationGroup="Add" ForeColor="Red"></asp:RequiredFieldValidator>
                                        <asp:TextBox ID="txtReceiverName" runat="server" CssClass="form-control" autocomplete="off"></asp:TextBox>
                                    </div>
                                </div>


                                <div class="col-md-3 col-sm-3">
                                    <div class="form-group">
                                        <label class="mb-5">
                                            Address of Receiver</label><span class="MandatoryValue">* </span>
                                        <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator2" ControlToValidate="txtReceiverAddress" Text="*" ValidationGroup="Add" ForeColor="Red"></asp:RequiredFieldValidator>
                                        <asp:TextBox ID="txtReceiverAddress" runat="server" CssClass="form-control" autocomplete="off" Rows="5" Columns="50"></asp:TextBox>
                                    </div>
                                </div>


                                <div class="col-md-3 col-sm-3">
                                    <div class="form-group">
                                        <label class="mb-5">
                                            NIC of Receiver</label><span class="MandatoryValue">* </span>
                                        <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator3" ControlToValidate="txtReceiverCnic" Text="*" ValidationGroup="Add" ForeColor="Red"></asp:RequiredFieldValidator>
                                        <asp:RegularExpressionValidator ID="RegularExpressionValidatorStreet" runat="server"
                                            ErrorMessage="Wrong NIC Format!" ValidationExpression="^[0-9+]{5}-[0-9+]{7}-[0-9]{1}$"
                                            ControlToValidate="txtReceiverCnic" ForeColor="Red" ValidationGroup="Add"></asp:RegularExpressionValidator><br />
                                        <asp:TextBox ID="txtReceiverCnic" runat="server" CssClass="form-control cnic" autocomplete="off" PlaceHolder="XXXXX-XXXXXXX-X"></asp:TextBox>
                                    </div>
                                </div>

                            </div>

                            <div class="row" align="center">
                                <div class="col-md-12 col-sm-12">
                                    <div class="button-list" style="margin-top: 15px;">
                                        <asp:Button ID="btnSubmit" CssClass="btn btn-primary" runat="server" Text="Submit" OnClick="btnSubmit_Click" ValidationGroup="Add" />

                                    </div>
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
        function AlertBox(title, Message, type) {
            swal(title, Message, type);
        }


    </script>

</asp:Content>

