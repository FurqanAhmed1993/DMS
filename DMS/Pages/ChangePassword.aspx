<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/AdminMaster.master" AutoEventWireup="true" CodeFile="ChangePassword.aspx.cs" Inherits="Pages_ChangePassword" %>

<%@ Register Src="~/CustomControls/Shared/InProgress.ascx" TagName="InProgress" TagPrefix="uc2" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <div>
        <asp:UpdateProgress ID="UpdateProgress2" runat="server">
            <ProgressTemplate>
                <uc2:InProgress ID="InProgress2" runat="server" />
            </ProgressTemplate>
        </asp:UpdateProgress>

        <asp:UpdatePanel ID="upData" runat="server">
            <ContentTemplate>
                <!-- Row -->
                <div class="row">
                    <div class="col-md-6 col-md-offset-3">
                        <div class="panel panel-default card-view">
                            <div class="panel-wrapper collapse in">
                                <div class="panel-body pa-15">

                                    <div class="row mb-30">
                                        <div class="col-lg-12">
                                            <div class="form-group">
                                                <p style="color: red">
                                                    Note :   Minimum size of password is 8 characters and it must include at least one non-alphabetic character.
                                                </p>
                                            </div>
                                        </div>
                                    </div>

                                    <div class="row">
                                      <div class="col-md-12 col-sm-6 col-lg-6">
                                            <div class="form-group">
                                                <label class="mb-5">Old Password</label>
                                                <asp:RequiredFieldValidator ID="CurrentPasswordRequired" runat="server" ControlToValidate="txtCurrentPassword" ForeColor="Red"
                                                    CssClass="failureNotification" ErrorMessage="Old Password is required." ToolTip="Old Password is required."
                                                    ValidationGroup="ChangeUserPasswordValidationGroup">*</asp:RequiredFieldValidator>
                                                <asp:TextBox ID="txtCurrentPassword" runat="server" CssClass="form-control" TextMode="Password"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>

                                    <div class="row">
                                         <div class="col-md-12 col-sm-6 col-lg-6">
                                            <div class="form-group">
                                                <label class="mb-5">New Password</label>
                                                <asp:RequiredFieldValidator ID="NewPasswordRequired" runat="server" ControlToValidate="txtNewPassword" ForeColor="Red"
                                                    CssClass="failureNotification" ErrorMessage="New Password is required." ToolTip="New Password is required."
                                                    ValidationGroup="ChangeUserPasswordValidationGroup">*</asp:RequiredFieldValidator>
                                                <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ControlToValidate="txtNewPassword" ForeColor="Red"
                                                    ErrorMessage="New Password does not meet complexity requirements" Display="Dynamic" CssClass="failureNotification"
                                                    ValidationExpression="^(?=.*[^a-zA-Z])(.{8,16})" Text="*"
                                                    ValidationGroup="ChangeUserPasswordValidationGroup"></asp:RegularExpressionValidator>
                                                <asp:TextBox ID="txtNewPassword" runat="server" CssClass="form-control" TextMode="Password" MaxLength="16"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>

                                    <div class="row">
                                     <div class="col-md-12 col-sm-6 col-lg-6">
                                            <div class="form-group">
                                                <label class="mb-5">Confirm New Password</label>
                                                <asp:RequiredFieldValidator ID="ConfirmNewPasswordRequired" runat="server" ControlToValidate="ConfirmNewPassword" ForeColor="Red"
                                                    CssClass="failureNotification" Display="Dynamic" ErrorMessage="Confirm New Password is required."
                                                    ToolTip="Confirm New Password is required." ValidationGroup="ChangeUserPasswordValidationGroup">*</asp:RequiredFieldValidator>
                                                <asp:CompareValidator ID="NewPasswordCompare" runat="server" ControlToCompare="txtNewPassword" ForeColor="Red"
                                                    ControlToValidate="ConfirmNewPassword" CssClass="failureNotification" Display="Dynamic"
                                                    ErrorMessage="The Confirm New Password must match the New Password entry." ValidationGroup="ChangeUserPasswordValidationGroup">*</asp:CompareValidator>
                                                <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="ConfirmNewPassword" ForeColor="Red"
                                                    ErrorMessage="Confirm Password does not meet complexity requirements" Display="Dynamic" CssClass="failureNotification"
                                                    ValidationExpression="^(?=.*[^a-zA-Z])(.{8,16})" Text="*"
                                                    ValidationGroup="ChangeUserPasswordValidationGroup"></asp:RegularExpressionValidator>
                                                <asp:TextBox ID="ConfirmNewPassword" runat="server" CssClass="form-control" TextMode="Password" MaxLength="16"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>

                                    <div class="row">
                                     <div class="col-lg-12">
                                            <asp:ValidationSummary ID="validationSummary" runat="server" ForeColor="Red" EnableClientScript="true"
                                                Enabled="true" ValidationGroup="ChangeUserPasswordValidationGroup" DisplayMode="BulletList"
                                                ShowSummary="true" HeaderText="Required Fields" CssClass='validationSummary' />
                                            <div class="pull-right">
                                                <br />
                                                <br />


                                                <asp:Button ID="btnChangePassword" runat="server" CommandName="ChangePassword" Text="Change Password"
                                                    ValidationGroup="ChangeUserPasswordValidationGroup" CssClass="save_btn btn btn-primary " OnClick="btnChangePassword_Click" />
                                            </div>
                                        </div>
                                    </div>


                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <!-- /Row -->
            </ContentTemplate>
        </asp:UpdatePanel>

    </div>

    <script>
        function AlertBox(title, Message, type) {
            swal(title, Message, type);
        }
    </script>
</asp:Content>

