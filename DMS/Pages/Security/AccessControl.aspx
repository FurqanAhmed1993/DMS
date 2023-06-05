<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/AdminMaster.master" AutoEventWireup="true" CodeFile="AccessControl.aspx.cs" Inherits="Pages_Security_AccessControl" %>

<%@ Register Src="~/CustomControls/Shared/InProgress.ascx" TagPrefix="uc" TagName="InProgress" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style>
        .TreeView {
            width: 200px;
        }

            .TreeView input[type="checkbox"] {
                width: 28px;
                height: 28px;
                background: #8C0A0A;
                background: linear-gradient(top, #8C0A0A 0%, #dfe5d7 40%, #b3bead 100%);
                box-shadow: inset 0px 1px 1px white, 0px 1px 3px rgba(0,0,0,0.5);
            }

        TreeView a {
            font-size: 20px;
        }

        .TreeView a:active {
            background-color: green;
        }
    </style>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:UpdateProgress ID="UpdateProgress2" runat="server">
        <ProgressTemplate>
            <uc:InProgress runat="server" ID="InProgress" />
        </ProgressTemplate>
    </asp:UpdateProgress>
    <div class="row heading-bg">
        <div class="col-lg-3 col-md-4 col-sm-4 col-xs-12">
            <h5 class="txt-dark">Access Control</h5>
        </div>
    </div>
    <asp:UpdatePanel ID="upData" runat="server">
        <ContentTemplate>
            <input type="hidden" runat="server" id="IsEdit" value="0" />
            <div class="panel panel-default card-view">
                <div class="panel-wrapper collapse in">
                    <div class="panel-body">
                        <div class="row">
                            <div class="col-md-3 col-sm-3">
                                <div class="form-group">
                                    <label class="mb-5">Role</label>
                                    <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator2" ControlToValidate="ddlRole" InitialValue="" Text="*" ValidationGroup="Update" ForeColor="Red"></asp:RequiredFieldValidator>
                                    <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator0" ControlToValidate="ddlRole" InitialValue="0" Text="*" ValidationGroup="Update" ForeColor="Red"></asp:RequiredFieldValidator>
                                    <asp:DropDownList runat="server" ID="ddlRole" CssClass="form-control ddlRole clsDropDown" AutoPostBack="true" OnSelectedIndexChanged="ddlRole_SelectedIndexChanged"></asp:DropDownList>
                                </div>
                            </div>
                            <div class="col-md-9 col-sm-9 text-right">
                                <div class="button-list">
                                    <asp:Button ID="btnUpdate" CssClass="btn btn-primary" runat="server" Visible="false" ValidationGroup="Update" Text="Update" OnClick="btnUpdate_Click" />
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-lg-12">
                                <asp:TreeView ID="TreeView1" runat="server" ShowCheckBoxes="All" CssClass="TreeView MyTreeView">
                                </asp:TreeView>
                            </div>
                        </div>
                    </div>
                </div>
            </div>

        </ContentTemplate>
    </asp:UpdatePanel>


    <script type="text/javascript">
        function pageLoad() {
            $(".MyTreeView").find(":checkbox").change(function () {

                var nextele = $(this).closest("table").next()[0];
                if (nextele && nextele.tagName == "DIV") {
                    $(nextele).find(":checkbox").prop("checked", $(this).prop("checked"));
                }
                count = 0;
                var a = $(this).closest('div').prev('table').find("input:checkbox");
                CheckParentNodes(a);
                var bc = $(this).closest('div').prev('table').next('div').children();
                $(bc).each(function () {

                    var b = $(this).find("input:checkbox");
                    if ($(b).is(':checked'))
                        count += 1;
                });

                if (!$(a).is(':checked')) {
                    $(a).prop('checked', true);
                }

                if ($(a).is(':checked') && count > 0) {
                    $(a).prop('checked', true);
                }
                else if ($(a).is(':checked') && count == 0) {
                    $(a).prop('checked', false);
                }
                else {
                    $(a).prop('checked', false);

                }

            });

        }

        function CheckParentNodes(Parentnode) {

            var nextelevelParentnode = $(Parentnode).closest('div').prev('table').find("input:checkbox");
            if ($(nextelevelParentnode).length > 0) {

                if ($(nextelevelParentnode).is(':checked') == false) {
                    $(nextelevelParentnode).prop('checked', true);

                    $(nextelevelParentnode).find(":checkbox").each(function () {
                        CheckParentNodes($(this));
                    });
                }
            }
            else { return; }

        }


        function AlertBox(title, Message, type) {
            swal(title, Message, type);
        }

    </script>
</asp:Content>

