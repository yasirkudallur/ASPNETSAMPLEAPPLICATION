<%@ Page Language="C#" AutoEventWireup="true" CodeFile="frmDashboard.aspx.cs" Inherits="frmDashboard" culture="auto" meta:resourcekey="PageResource2" uiculture="auto" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<%@ Register Assembly="System.Web.DataVisualization, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
    Namespace="System.Web.UI.DataVisualization.Charting" TagPrefix="asp" %>



<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">


<html id="htmlExpDashboard" runat="server"  dir='<%$ Resources:ValidationResources, TextDirection %>' xmlns="http://www.w3.org/1999/xhtml" >
<head  runat="server">
    <meta http-equiv="X-UA-Compatible" content="IE=edge"/>
    <meta http-equiv="X-UA-Compatible" content="IE=7"/>
    <meta http-equiv="X-UA-Compatible" content="IE=8"/>
    <meta http-equiv="X-UA-Compatible" content="IE=9"/>
    <meta http-equiv="X-UA-Compatible" content="IE=10"/>
    <meta http-equiv="X-UA-Compatible" content="IE=11"/>

    <script src="../Dashbaords/FusionCharts/FusionCharts.js" type="text/javascript"></script>
    <link href="css/SiteContent.css" rel="stylesheet" type="text/css" />



    <title></title>
    <style>
        .breakword
 {
 word-wrap:break-word;
 word-break:break-all;
 }
        .tblTodayTasks
    {
        border: 1px solid #645A30;
        border-collapse: separate;
        /*border-left: 0;*/
        border-radius: 4px;
        border-spacing: 0px;
        }
    </style>
</head>
    <%--style="background-image: url('Images/bg.png'); background-repeat: repeat"--%>
<body >
    <form id="form1" runat="server">
    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server" 
        CombineScripts="True"></asp:ToolkitScriptManager>
        <table cellpadding="0" cellspacing="0" border="0" width="99%"  align="center">
    <tr style="background-color: #F4F0EA; height: 60px;">
        <td>
            <table cellpadding="2" cellspacing="0" border="0" width="100%">
                <tr>
                    <td width="50%">
                    <table cellpadding="2" cellspacing="0" border="0">
                        <tr>
                            <td>
                                <asp:Label ID="Label1" runat="server" Text="مجموع زوار اليوم:" 
                                    Font-Bold="True"></asp:Label>
                            </td>
                            <td>
                            <asp:Label ID="lblDayVisitorsValue" runat="server" Font-Bold="True"></asp:Label>
                            </td>
                        </tr>
                    </table>
                    </td>
                    <td width="50%" align="left">

                    </td>
                </tr>
            </table>
        </td>
    </tr>
    <tr >
    <td>
    <table cellpadding="0" cellspacing="0" border="0" width="100%">
    <tr>
    <td>
    </td>
        <td>
    </td>
        <td>
            &nbsp;</td>
    </tr>
        <tr>
        <td width="33%" valign="top">
        <table cellpadding="0" cellspacing="0" width="375px" align="center" bgcolor="White" 
                style="border: 1px solid #D0C4B4">
        <tr>
        <td valign="middle" colspan="2" bgcolor="#F6F4F0" height="30" 
                
                style="color: #493318; font-weight: bold; padding-right: 20px; padding-left: 20px;">
                <table cellpadding="2" cellspacing="0" border="0">
                <tr>
                <td>
                    <asp:Image ID="imgPndSug" runat="server" Height="30px" 
                        ImageUrl="~/IMAGES/iconReassess.png" Width="30px" 
                        meta:resourcekey="imgPndSugResource2" />
                </td>
                <td>
                    <asp:Label ID="Label3" runat="server" Text=" زوارالمكتب الرئيسي" 
                        meta:resourcekey="Label3Resource2"></asp:Label>
                </td>
                </tr>
                </table>
            </td>
        </tr>
        <tr>
        <td>
                <asp:Literal ID="ltrlPngSug" runat="server" 
                    meta:resourcekey="FCLiteral1Resource1"></asp:Literal>
        </td>
        <td valign="middle">
        <table cellpadding="2" cellspacing="0" border="0">
        <tr>
        <td align="center" valign="middle">
                            <asp:Label ID="Label4" runat="server" Text="المجموع" 
                                meta:resourcekey="Label4Resource2"></asp:Label>
            </td>
        </tr>
        <tr>
        <td align="center" valign="middle" 
                style="border-bottom-style: solid; border-bottom-width: 1px; border-bottom-color: #d7c5ab">
<asp:Label ID="lblTotalPending" runat="server" CssClass="lblBox" 
                meta:resourcekey="lblTotalPendingResource2"></asp:Label>
        </td>
        </tr>
        <tr>
        <td align="center" valign="middle">
                                    <asp:Label ID="Label5" runat="server" Text="عدد زوارالمكتب الرئيسي" 
                                        meta:resourcekey="Label5Resource2"></asp:Label>

        </td>
        </tr>
        <tr>
        <td align="center" valign="middle" 
                style="border-bottom-style: solid; border-bottom-width: 1px; border-bottom-color: #d7c5ab">

                        <asp:Label ID="lblPendingCount" runat="server" Text="" CssClass="lblBox"></asp:Label>
        </td>
        </tr>
        </tr>
        </table>

        </td>
        </tr>
        </table>

        </td>
        <td width="33%" valign="top">
                <table cellpadding="0" cellspacing="0" width="375px" align="center" 
                    bgcolor="White" style="border: 1px solid #D0C4B4">
        <tr>
        <td valign="middle" colspan="2" bgcolor="#F6F4F0" height="30" 
                
                style="color: #493318; font-weight: bold; padding-right: 20px; padding-left: 20px;">
                                <table cellpadding="2" cellspacing="0" border="0">
                <tr>
                <td>
                    <asp:Image ID="Image1" runat="server" Height="30px" 
                        ImageUrl="~/IMAGES/iconReassess.png" Width="30px" 
                        meta:resourcekey="Image1Resource2" />
                </td>
                <td>
                                                            <asp:Label ID="Label7" runat="server" Text="الزوار الذين خرجومن المكتب الرئيسي" 
                                                                meta:resourcekey="Label7Resource2"></asp:Label>

                </td>
                </tr>
                </table>

            </td>
        </tr>
        <tr>
        <td>
        <asp:Literal ID="ltrlFsblSug" runat="server" 
                meta:resourcekey="FCLiteral1Resource1"></asp:Literal>
        </td>
        <td valign="middle">
        <table cellpadding="2" cellspacing="0" border="0">
        <tr>
        <td align="center" valign="middle">
                                                                    <asp:Label ID="Label8" runat="server" Text="المجموع" 
                                                                        meta:resourcekey="Label8Resource2"></asp:Label>
            </td>
        </tr>
        <tr>
        <td align="center" valign="middle" 
                style="border-bottom-style: solid; border-bottom-width: 1px; border-bottom-color: #d7c5ab">
<asp:Label ID="lblTotalOutgoing" runat="server" CssClass="lblBox" 
                meta:resourcekey="lblTotalOutgoingResource2"></asp:Label>
            </td>
        </tr>
        <tr>
        <td align="center" valign="middle">
               <asp:Label ID="Label2" runat="server" Text="عددالزوار الذين خرجومن المكتب الرئيسي" 
                                        meta:resourcekey="Label5Resource2"></asp:Label>

        </td>
        </tr>
        <tr>
        <td align="center" valign="middle" 
                style="border-bottom-style: solid; border-bottom-width: 1px; border-bottom-color: #d7c5ab">
            <asp:Label ID="lblOnProgressCount" runat="server" 
                meta:resourcekey="lblOnProgressCountResource2" 
></asp:Label>
            <%--<asp:Label ID="lblOnProgressCount" runat="server" Text="" CssClass="lblBox"></asp:Label>--%>
        </td>
        </tr>
        </table>

        </td>
        </tr>
        </table>

        </td>
        <td width="33%" valign="top">
                <table cellpadding="0" cellspacing="0" width="375px" align="center" bgcolor="White" 
                style="border: 1px solid #D0C4B4">
        <tr>
        <td valign="middle" colspan="2" bgcolor="#F6F4F0" height="30" 
                
                style="color: #493318; font-weight: bold; padding-right: 20px; padding-left: 20px;">
                                                                <table cellpadding="2" cellspacing="0" border="0">
                <tr>
                <td>
                    <asp:Image ID="Image3" runat="server" Height="30px" 
                        ImageUrl="~/IMAGES/iconReassess.png" Width="30px" 
                        meta:resourcekey="Image3Resource2" />
                </td>
                <td>
                                                           <asp:Label ID="Label15" runat="server" Text="زوار الشهر" 
                                                               meta:resourcekey="Label15Resource2"></asp:Label>
           
                </td>
                </tr>
                </table>
               </td>
        </tr>
        <tr>
        <td>
                <asp:Literal ID="ltrlImplSug" runat="server" 
                    meta:resourcekey="FCLiteral1Resource1"></asp:Literal>
        </td>
        <td valign="middle">
        <table cellpadding="2" cellspacing="0" border="0">
        <tr>
        <td align="center" valign="middle">
                                                                   <asp:Label ID="Label16" runat="server" Text="المجموع" 
                                                                       meta:resourcekey="Label16Resource2"></asp:Label>
            </td>
        </tr>
        <tr>
        <td align="center" valign="middle" 
                style="border-bottom-style: solid; border-bottom-width: 1px; border-bottom-color: #d7c5ab">
<asp:Label ID="lblTotalPendingDelayed" runat="server" CssClass="lblBox" 
                meta:resourcekey="lblTotalPendingDelayedResource2"></asp:Label>
        </td>
        </tr>
        <tr>
        <td align="center" valign="middle">
                                                                           <asp:Label ID="Label9" runat="server" Text="عددالزوار الشهر" 
                                        meta:resourcekey="Label5Resource2"></asp:Label>

        </td>
        </tr>
        <tr>
        <td align="center" valign="middle" 
                style="border-bottom-style: solid; border-bottom-width: 1px; border-bottom-color: #d7c5ab">
            <%--            <asp:LinkButton ID="lblPendingOverdueCount" runat="server" 
                onclick="lblPendingCount_Click"></asp:LinkButton>--%>
                <asp:Label ID="lblPendingOverdueCount" runat="server" 
                meta:resourcekey="lblPendingOverdueCountResource2"></asp:Label>
            <%--            <asp:Label ID="lblPendingCount" runat="server" Text="" CssClass="lblBox"></asp:Label>--%>
        </td>
        </tr>
        </table>

        </td>
        </tr>
        </table>

        </td>
        </tr>
    </table>
        <table cellpadding="0" cellspacing="0" border="0" width="100%">
    <tr>
    <td>
    </td>
        <td>
    </td>
        <td>
            &nbsp;</td>
    </tr>
    <tr>
    <td colspan="3">
    <table cellpadding="0" cellspacing="0"  align="center" 
                            bgcolor="White" style="border: 1px solid #D0C4B4" width="99%">
        <tr>
        <td valign="middle" bgcolor="#F6F4F0" height="30" 
                
                
                style="color: #493318; font-weight: bold; padding-right: 20px; padding-left: 20px;">
                                                                                <table cellpadding="2" cellspacing="0" border="0" >
                <tr>
                <td>
                    <asp:Image ID="Image4" runat="server" Height="30px" 
                        ImageUrl="~/IMAGES/iconNotifications.png" Width="30px" 
                        meta:resourcekey="Image4Resource2" />
                </td>
                <td>
                 <asp:Label ID="Label19" runat="server" Text="تحليل الادارة" 
                        meta:resourcekey="Label19Resource2"></asp:Label>

                </td>
                </tr>
                </table>
           </td>
        </tr>
        <tr>
        <td>
<asp:GridView ID="gvPmAnalysis" runat="server" AutoGenerateColumns="False" 
                    CellPadding="4" CssClass="mGrid" Font-Size="12px" width="100%" PageSize="5" 
                             
                                                   
                meta:resourcekey="gvPmAnalysisResource1" onrowdatabound="gvPmAnalysis_RowDataBound" 
>
                <PagerStyle CssClass="row1" />
                <AlternatingRowStyle CssClass="row2" />
                <RowStyle CssClass="row1" />
                    <Columns>

                        <asp:BoundField HeaderText="م" >
                        <HeaderStyle Width="91px" HorizontalAlign="Center" />
                        <ItemStyle Width="91px" HorizontalAlign="Center" />
                        </asp:BoundField>

                        <asp:BoundField DataField="DEPARTMENT_NAME_AR" HeaderText="اسم الإدارة" 
                            meta:resourcekey="BoundFieldResource6">
                        <HeaderStyle Width="900px" />
                        <ItemStyle Width="900px" />
                        </asp:BoundField>

                        <asp:BoundField DataField="CNT" HeaderText="عدد الزوار" 
                            meta:resourcekey="BoundFieldResource8">
                        <HeaderStyle Width="184px" HorizontalAlign="Center" />
                        <ItemStyle Width="184px" HorizontalAlign="Center" />
                        </asp:BoundField>
                        

                    </Columns>
                <HeaderStyle  Font-Size="10px" Height="25px" />
                </asp:GridView>
        </td>
        </tr>
        </table></td>
    </tr>
        <tr>
        <td width="400px" valign="top">
                                &nbsp;</td>

        <td width="800px" colspan="2" valign="top">

                        

        </td>
        </tr>
    </table>
    </form>
    </body>
</html>
