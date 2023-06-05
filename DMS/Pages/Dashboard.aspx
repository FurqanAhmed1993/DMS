<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Dashboard.aspx.cs" Inherits="Pages_Dashboard" MasterPageFile="~/MasterPage/AdminMaster.master" %>

<%@ Register Src="~/CustomControls/Shared/InProgress.ascx" TagPrefix="uc" TagName="InProgress" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">

    <style>
        .sm-data-box .data-wrap-left, .sm-data-box .data-wrap-right {
            padding-top: 5px;
            padding-bottom: 5px;
            min-height: 90px;
        }

        .sm-data-box {
            color: #ffffff !important;
            border-radius: 4px;
            padding-right: 5px;
            /* Permalink - use to edit and share this gradient: https://colorzilla.com/gradient-editor/#d99d00+0,ffbc0d+100 */
                filter: progid:DXImageTransform.Microsoft.gradient( startColorstr='#d99d00', endColorstr='#ffbc0d',GradientType=1 ); /* IE6-9 fallback on horizontal gradient */
       
                background: #6699ff;
                }


            .sm-data-box .counter {
                font-size: 40px;
                font-weight: 600;
            }

        .sm-data-img {
            background-color: #fff;
            border-radius: 50px;
            width: 62px;
            height: 60px;
            padding: 19px;
            margin-top: 10px;
        }

        .sm-data-box .progress {
            margin-bottom: 10px;
        }

            .sm-data-box .progress .progress-bar {
                font-size: 6px;
                background: #cbd245;
            }



        /* width */
        ::-webkit-scrollbar {
            width: 7px;
        }

        /* Track */
        ::-webkit-scrollbar-track {
            background: #f1f1f1;
        }

        /* Handle */
        ::-webkit-scrollbar-thumb {
            background: #c5c4c4;
        }

            /* Handle on hover */
            ::-webkit-scrollbar-thumb:hover {
                background: #555;
            }
    </style>


</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="pt-20">
        <asp:UpdateProgress ID="UpdateProgress2" runat="server">
            <ProgressTemplate>
                <uc:InProgress runat="server" ID="InProgress" />
            </ProgressTemplate>
        </asp:UpdateProgress>

        <asp:UpdatePanel ID="upData" runat="server">
            <ContentTemplate>
                <asp:Timer ID="Timer1" runat="server" OnTick="Timer1_Tick" Interval="60000" Enabled="false"></asp:Timer>

                <div runat="server" id="Div_Main">

                    <div class="row">
                        <div id="divError1" runat="server" visible="false" class="card-view panel-danger1">
                            <asp:Label ID="lblError1" runat="server"></asp:Label>
                        </div>
                    </div>
                  

                    <div class="row">
                        <asp:LinkButton ID="lbNew" runat="server" OnClick="lbNew_Click">
                            <div class="col-lg-4 col-md-4 col-sm-4 col-xs-6">
                                <div class="panel panel-default card-view pa-0 shadow-none">
                                    <div class="panel-wrapper collapse in">
                                        <div class="panel-body pa-0">
                                            <div class="sm-data-box">
                                                <div class="container-fluid">
                                                    <div class="row">
                                                        <div class="col-xs-12 text-center pl-0 pr-0 data-wrap-left">
                                                            <span class="block counter"><span class="counter-anim">
                                                                <asp:Label runat="server" ID="lbl_InitialDraft" CssClass="number" Text="0"></asp:Label>
                                                            </span></span>
                                                            <span class="capitalize-font block">Initial Draft</span>
                                                        </div>
                                                       
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </asp:LinkButton>
                        <asp:LinkButton ID="lblApproverSubmitted" runat="server" OnClick="lbApproverSubmitted_Click">
                            <div class="col-lg-4 col-md-4 col-sm-4 col-xs-6">
                                <div class="panel panel-default card-view pa-0 shadow-none">
                                    <div class="panel-wrapper collapse in">
                                        <div class="panel-body pa-0">
                                            <div class="sm-data-box">
                                                <div class="container-fluid">
                                                    <div class="row">
                                                        <div class="col-xs-12 text-center pl-0 pr-0 data-wrap-left">
                                                            <span class="block counter"><span class="counter-anim">
                                                                <asp:Label runat="server" ID="lbl_ApproverPending" CssClass="number" Text="0"></asp:Label>
                                                            </span></span>
                                                            <span class="capitalize-font block">Pending at Approver</span>
                                                        </div>
                                                        
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </asp:LinkButton>
                        <asp:LinkButton ID="lblApproved" runat="server" OnClick="lbApprove_Click">
                            <div class="col-lg-4 col-md-4 col-sm-4 col-xs-6">
                                <div class="panel panel-default card-view pa-0 shadow-none">
                                    <div class="panel-wrapper collapse in">
                                        <div class="panel-body pa-0">
                                            <div class="sm-data-box">
                                                <div class="container-fluid">
                                                    <div class="row">
                                                        <div class="col-xs-12 text-center pl-0 pr-0 data-wrap-left">
                                                            <span class="block counter"><span class="counter-anim">
                                                                <asp:Label runat="server" ID="lbl_Approved" CssClass="number" Text="0"></asp:Label>
                                                            </span></span>
                                                            <span class="capitalize-font block">Approved</span>
                                                        </div>
                                                        
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </asp:LinkButton>
                        <asp:LinkButton ID="lbObjected" runat="server" OnClick="lbObjected_Click">
                            <div class="col-lg-4 col-md-4 col-sm-4 col-xs-6">
                                <div class="panel panel-default card-view pa-0 shadow-none">
                                    <div class="panel-wrapper collapse in">
                                        <div class="panel-body pa-0">
                                            <div class="sm-data-box">
                                                <div class="container-fluid">
                                                    <div class="row">
                                                        <div class="col-xs-12 text-center pl-0 pr-0 data-wrap-left">
                                                            <span class="block counter"><span class="counter-anim">
                                                                <asp:Label runat="server" ID="lblObjected" CssClass="number" Text="0"></asp:Label>
                                                            </span></span>
                                                            <span class="capitalize-font block">Objected</span>
                                                        </div>
                                                       
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </asp:LinkButton>

                            <asp:LinkButton ID="lbRejected" runat="server" OnClick="lbRejected_Click">
                            <div class="col-lg-4 col-md-4 col-sm-4 col-xs-6">
                                <div class="panel panel-default card-view pa-0 shadow-none">
                                    <div class="panel-wrapper collapse in">
                                        <div class="panel-body pa-0">
                                            <div class="sm-data-box">
                                                <div class="container-fluid">
                                                    <div class="row">
                                                        <div class="col-xs-12 text-center pl-0 pr-0 data-wrap-left">
                                                            <span class="block counter"><span class="counter-anim">
                                                                <asp:Label runat="server" ID="lblRejected" CssClass="number" Text="0"></asp:Label>
                                                            </span></span>
                                                            <span class="capitalize-font block">Rejected</span>
                                                        </div>
                                                     
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </asp:LinkButton>


                        
                            <asp:LinkButton ID="LinkButton1" runat="server" OnClick="lbIssued_Click">
                            <div class="col-lg-4 col-md-4 col-sm-4 col-xs-6">
                                <div class="panel panel-default card-view pa-0 shadow-none">
                                    <div class="panel-wrapper collapse in">
                                        <div class="panel-body pa-0">
                                            <div class="sm-data-box">
                                                <div class="container-fluid">
                                                    <div class="row">
                                                        <div class="col-xs-12 text-center pl-0 pr-0 data-wrap-left">
                                                            <span class="block counter"><span class="counter-anim">
                                                                <asp:Label runat="server" ID="lblIssued" CssClass="number" Text="0"></asp:Label>
                                                            </span></span>
                                                            <span class="capitalize-font block">Issued</span>
                                                        </div>
                                                     
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </asp:LinkButton>

                    </div>

                </div>
            </ContentTemplate>
        </asp:UpdatePanel>

    </div>

    <script type="text/javascript">

        function CurrencyFormate() {
            $('.money').each(function () {
                var value = $(this).html();
                value = value.replace(",", "");
                value = parseFloat(value);
                $(this).html(accounting.formatMoney(value, ''))
            });

            $('.number').each(function () {
                var value = $(this).html();
                value = value.replace(",", "");
                value = parseFloat(value);
                $(this).html(accounting.formatNumber(value))
            });
        }

        function pageLoad() {
            $('.date').datepicker({
                forceParse: false,
                calendarWeeks: true,
                autoclose: true,
                format: 'dd-MM-yyyy',
            });
            $('.date').keydown(function () {
                return false;
            });

            $('.cblCheckAll input').change(function () {

                var currChk = $(this);
                if ($(this).val() == "-1") {
                    $(this).closest('table').find('input:checkbox').prop('checked', $(currChk).is(':checked'));
                }
                else {
                    if (!this.checked) {
                        $("#ContentPlaceHolder1_chkboxorderfrom_2").prop("checked", false);
                    }
                }
            });

            CurrencyFormate();

            $('.datetime').datepicker({
                forceParse: false,
                calendarWeeks: true,
                autoclose: true,
                format: 'dd-M-yyyy',
            });
            $('.datetime').keydown(function () {
                return false;
            });
        }



    </script>

</asp:Content>
