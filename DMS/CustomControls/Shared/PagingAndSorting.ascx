<%@ Control Language="C#" AutoEventWireup="true" CodeFile="PagingAndSorting.ascx.cs" Inherits="ERP.Website.controls.PagingAndSorting" %>

<style>
    .DropDown {
        border: thin #CCCCCC solid;
        font-family: Calibri;
        color: #000;
        width: 200px;
        font-size: 8pt;
    }
</style>
    <table cellpadding="0" cellspacing="0" runat="server" id="tblPaging" style="width: 100%;">
        <tr>
            <td nowrap="nowrap" style="vertical-align: top">&nbsp;&nbsp;<asp:Label ID="lblPageSize" runat="server" Text="Page Size:" Font-Bold="true"></asp:Label>&nbsp;
            <asp:DropDownList CssClass="Dropdown" ID="ddlPageSize" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlPageSize_SelectedIndexChanged"></asp:DropDownList>&nbsp;&nbsp;&nbsp;&nbsp;
            </td>
            <td style="vertical-align: top">
                <table cellpadding="0" cellspacing="0" style="margin: 0 auto;">
                    <tr>
                        <td style="vertical-align: top">
                            <asp:ImageButton ID="imgPrevious" runat="server" OnClick="imgPrevious_Click" ImageUrl="~/Assets/img/previous_btn.jpg" ToolTip="Previous" Width="17px" Height="19px" />&nbsp;
                        </td>
                        <td style="vertical-align: top">
                            <asp:DropDownList CssClass="Dropdown" ID="ddlPage" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlPage_SelectedIndexChanged"></asp:DropDownList>&nbsp;
                        </td>
                        <td style="vertical-align: top">
                            <asp:ImageButton ID="imgNext" runat="server" OnClick="imgNext_Click" ImageUrl="~/Assets/img/next_btn.jpg" ToolTip="Next" Width="17px" Height="19px" />
                        </td>
                    </tr>
                </table>
            </td>
            <td nowrap="nowrap" style="vertical-align: middle; text-align:right;">&nbsp;&nbsp;&nbsp;&nbsp;<asp:Label ID="lblRecordCountText" runat="server" Text="Total Records : "></asp:Label>
                <asp:Label ID="lblRecordCount" CssClass="clsRecordCount" runat="server" Font-Bold="true"></asp:Label>&nbsp;&nbsp;
            </td>
        </tr>
    </table>
