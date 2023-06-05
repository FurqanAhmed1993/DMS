<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/AdminMaster.master" AutoEventWireup="true" CodeFile="Menu.aspx.cs" Inherits="Pages_Security_Menu" %>

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
        <div class="col-lg-3 col-md-4 col-sm-4 col-xs-12">
            <h5 class="txt-dark">Menu</h5>
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
                                <div class="form-group">
                                    <label class="mb-5">Parent Menu</label>
                                    <asp:DropDownList runat="server" ID="ddlParentMenuSearch" CssClass="form-control ddlParentMenuSearch clsDropDown"></asp:DropDownList>
                                </div>
                            </div>
                            <div class="col-md-3 col-sm-3">
                                <div class="form-group">
                                    <label class="mb-5">Parent Menu</label>
                                    <asp:TextBox ID="txtParentMenu" runat="server" CssClass="form-control" AutoCompleteType="Disabled"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-md-6 col-sm-6 text-right">
                                <div class="button-list" style="margin-top: 15px;">
                                    <asp:Button ID="btnSearch" CssClass="btn btn-primary" runat="server" Text="Search" OnClick="btnSearch_Click" />
                                    <asp:Button ID="btnCancel" CssClass="btn btn-default" runat="server" Text="Cancel" OnClick="btnCancel_Click" />
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>

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
                                            <th class="">Parent Menu </th>
                                            <th class="AllignCenter">Sort Order </th>
                                            <th class="AllignCenter">Menu</th>
                                            <th class="AllignCenter">Is Displayed In Menu</th>
                                            <th class="AllignCenter">Features</th>
                                            <%-- <th class="AllignCenter">Active</th>--%>
                                            <th class="AllignCenter">Created By </th>
                                            <th class="AllignCenter">Modified By</th>
                                            <th class="AllignCenter" style="width: 200px;">Action</th>
                                        </tr>
                                    </thead>
                                    <tbody>
                                        <asp:Repeater ID="rpt" runat="server" OnItemDataBound="rpt_ItemDataBound">
                                            <ItemTemplate>
                                                <tr>
                                                    <input type="hidden" runat="server" id="hfMenuId" class="hfMenuId" value='<%# Eval("MenuId") %>' />
                                                    <input type="hidden" runat="server" id="hfFeatureIds" class="hfFeatureIds" value='<%# Eval("FeatureIds") %>' />
                                                    <td>
                                                        <%#(((RepeaterItem)Container).ItemIndex+1).ToString()%>
                                                    </td>
                                                    <td>
                                                        <%# Eval("Parent") %>
                                                    </td>
                                                    <td class="AllignCenter">
                                                        <%# Eval("SortOrder") %>
                                                    </td>
                                                    <td class="AllignCenter">
                                                        <%# Eval("Menu_Name") %>
                                                    </td>
                                                    <td class="AllignCenter">
                                                        <%# Eval("Is_Displayed_In_Menu") %>
                                                    </td>
                                                    <td class="AllignCenter">
                                                        <%# Eval("Features") %>
                                                    </td>
                                                    <%--   <td class="AllignCenter">
                                                        <%# Convert.ToBoolean(Eval("IsActive")) == true ? "Yes" : "No" %>
                                                    </td>--%>
                                                    <td class="AllignCenter">
                                                        <%# Convert.ToString( Eval("CreatedByName")) == "" ? ""  : (Eval("CreatedByName") + " <br /> <strong>On</strong> <br />")  %>

                                                        <%# Convert.ToString(Eval("CreatedDate")) == "" ? "" :  Convert.ToDateTime(Eval("CreatedDate")).ToString(DMS_Utilities.GenericConstants.DateTimeFormat1_) %>
                                                    </td>
                                                    <td class="AllignCenter">
                                                        <%# Convert.ToString( Eval("ModifiedByName")) == "" ? ""  : (Eval("ModifiedByName") + " <br /> <strong>On</strong> <br />")  %>
                                                        <%# Convert.ToString(Eval("ModifiedDate")) == "" ? "" :  Convert.ToDateTime(Eval("ModifiedDate")).ToString(DMS_Utilities.GenericConstants.DateTimeFormat1_) %>
                                                    </td>
                                                    <td class="AllignCenter">
                                                        <asp:ImageButton ID="lbEdit" ImageUrl="~/Assets/Images/edit-icon.png" runat="server" Text="Edit" ToolTip="Edit" OnClick="lbEdit_Click" />
                                                        &nbsp
                                                        <asp:ImageButton ID="lbDelete" ImageUrl="~/Assets/Images/delete-icon.png" runat="server" Text="Remove" ToolTip="Remove" OnClick="lbDelete_Click" />
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
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>

    <%-- Modal Start--%>
    <input type="button" data-toggle="modal" data-target="#AddEditModal" class="openmodal" style="display: none;" />
    <div class="modal fade in inmodal " id="AddEditModal" tabindex="-1" role="dialog" aria-hidden="true" data-backdrop="static">
        <div class="modal-dialog">
            <div class="modal-content animated">
                <div class="modal-header" style="padding-bottom: 9px; padding-top: 9px; text-align: center">
                    <h5 class="modal-title">Add / Update Menu</h5>
                </div>
                <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                    <ContentTemplate>
                        <div class="modal-body">
                            <input type="hidden" id="hfId" runat="server" class="hfId" value="0" />
                            <input type="hidden" id="hfFeaturesId" runat="server" class="hfFeaturesId" value="" />
                            <div class="row">
                                <div class="col-md-6 col-sm-6 col-lg-6">
                                    <div class="form-group">
                                        <label class="mb-5">
                                            Parent Menu 
                                        </label>
                                        <asp:DropDownList runat="server" ID="ddlParentMenu" CssClass="form-control ddlParentMenu clsDropDown"></asp:DropDownList>
                                    </div>
                                </div>
                                <div class="col-md-6 col-sm-6 col-lg-6">
                                    <div class="form-group">
                                        <label class="mb-5">
                                            Parent Menu Icon
                                        </label>
                                        <asp:TextBox ID="txtParentMenuIcon" runat="server" MaxLength="30" CssClass="form-control txtParentMenuIcon clsTextBox" AutoCompleteType="Disabled"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-6 col-sm-6 col-lg-6">
                                    <div class="form-group">
                                        <label class="mb-5">Menu </label>
                                        <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator3" ControlToValidate="txtMenuName" Text="*" ValidationGroup="Add" ForeColor="Red"></asp:RequiredFieldValidator>
                                        <asp:TextBox ID="txtMenuName" runat="server" MaxLength="200" CssClass="form-control txtMenuName clsTextBox" AutoCompleteType="Disabled"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-12 col-sm-12 col-lg-12">
                                    <div class="form-group">
                                        <label class="mb-5">URL</label>
                                        <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator1" ControlToValidate="txtUrl" Text="*" ValidationGroup="Add" ForeColor="Red"></asp:RequiredFieldValidator>
                                        <asp:TextBox ID="txtUrl" runat="server" CssClass="form-control txtUrl clsTextBox" AutoCompleteType="Disabled"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-6 col-sm-6 col-lg-6">
                                    <div class="form-group">
                                        <label class="mb-5">Sort Order</label>
                                        <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator4" ControlToValidate="txtSortOrder" Text="*" ValidationGroup="Add" ForeColor="Red"></asp:RequiredFieldValidator>
                                        <asp:TextBox ID="txtSortOrder" runat="server" TextMode="Number" MaxLength="5" CssClass="form-control txtSortOrder clsTextBox" AutoCompleteType="Disabled"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-md-6 col-sm-6 col-lg-6">
                                    <div class="form-group">
                                        <label class="mb-5">Display in Menu </label>
                                        <asp:CheckBox runat="server" ID="chk_IsDisplay" CssClass="form-control " />
                                    </div>

                                </div>

                            </div>

                            <div class="row">
                                <div class="col-md-6 col-sm-6 col-lg-6">
                                    <div class="form-group">
                                        <label class="mb-5">Features</label>
                                        <br />
                                        <asp:CheckBoxList runat="server" ID="chk_Feature" CssClass="chk_Feature" />
                                    </div>
                                </div>
                                <div class="col-md-6 col-sm-6 col-lg-6" style="visibility: hidden">
                                    <div class="form-group">
                                        <label class="mb-5">Active</label>
                                        <asp:CheckBox runat="server" ID="chk_IsActive" CssClass="form-control " />
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="modal-footer">
                            <asp:Button Text="Save" CssClass="btn btn-primary" ID="btnAdd" ValidationGroup="Add" runat="server" OnClick="btnAdd_Click" />
                            <asp:Button Text="Cancel" ID="btnCancelEdit" runat="server" CssClass="btnCancelEdit btn btn-default" data-dismiss="modal" />
                        </div>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
        </div>
    </div>
    <%-- Modal End--%>


    <script type="text/javascript">
        function pageLoad() {
            //ParentMenuIcon(0);
            //$(".ddlParentMenu").change(function () {
            //    ParentMenuIcon($(this).val());
            //});

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

        //function ParentMenuIcon(value) {
        //    $(".txtParentMenuIcon").removeAttr("disabled");
        //    if (value != null || value != "" || value != "0") {
        //        $(".txtParentMenuIcon").val("");
        //        $(".txtParentMenuIcon").attr("disabled", "disabled");
        //    }
        //}

        function ClosePopup() {
            $('.hfId').val("0");
            $('.clsTextBox').val("");
            $('.clsDropDown').val("0");
            $('.btnCancelEdit').click();
        }

        function OpenPopup() {
            $('.openmodal').click();
        }


    </script>
</asp:Content>

