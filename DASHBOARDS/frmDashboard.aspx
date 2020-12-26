<%@ Page Language="C#" AutoEventWireup="true" CodeFile="frmDashboard.aspx.cs" Inherits="frmDashboard" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
        <link href="../vendors/easypiechart/jquery.easy-pie-chart.css" rel="stylesheet" media="screen"/>
    <script src="../vendors/jquery-1.9.1.min.js"></script>
    <script src="../vendors/easypiechart/jquery.easy-pie-chart.js"></script>
        <script src="../assets/scripts.js"></script>
        <script>
            $(function () {
                // Easy pie charts
                $('.chart').easyPieChart({ animate: 1000 });
            });
        </script>

        <script src="../vendors/modernizr-2.6.2-respond-1.1.0.min.js"></script>
    <link href="../css/SiteContent.css" rel="stylesheet" />

    <style>
        /*img {
  border-radius: 8px;
  -webkit-filter: drop-shadow(5px 5px 5px #222);
  filter: drop-shadow(5px 5px 5px #222);
}*/
        .pFirst {
        border: 3px solid #8D5D00;
        border-left:1px;
        border-top:0px;
        margin: 3px;
        padding : 10px;
        width:100%;
        }
    </style>

</head>
<body>
    <form id="form1" runat="server">
   <table cellpadding="0" cellspacing="0" border="0" width="99%"  align="center">
       <tr>
           <td>
               <p class="pFirst" ><asp:Label ID="lblPersonalInfo" runat="server" Text="Statistics" Font-Bold="True" Font-Size="15px" meta:resourcekey="lblPersonalInfoRes"></asp:Label></p>
               
           </td>
       </tr>
       <tr>
           <td>
               <%--<table cellpadding="2" cellspacing="0" border="0" align="center">
                   <tr>
                       <td>

                           <asp:Label ID="lblFromDate" runat="server" CssClass="lblBox" Text="من تاريخ : "></asp:Label>

                       </td>
                       <td>
                       <asp:TextBox ID="txtFromDate" runat="server" CssClass="txtBox"></asp:TextBox> 
                       </td>
                                              <td>

                                                  <asp:Label ID="lblToDate" runat="server" CssClass="lblBox" Text="إلى تاريخ : "></asp:Label>

                       </td>
                       <td>
                       <asp:TextBox ID="txtToDate" runat="server" CssClass="txtBox"></asp:TextBox>
                       </td>
                                              <td>

                                                  <asp:Label ID="lblMonth" runat="server" CssClass="lblBox" Text="الشهر : "></asp:Label>

                       </td>
                                              <td>

        
        <asp:DropDownList ID="drpMonths" runat="server" CssClass="drpList" Width="200px">
            <asp:ListItem></asp:ListItem>
            <asp:ListItem Value="1">Jan</asp:ListItem>
            <asp:ListItem Value="2">Feb</asp:ListItem>
        </asp:DropDownList>

                       </td>
                                              <td>

                                                  <asp:Label ID="lblYears" runat="server" CssClass="lblBox" Text="السنة : "></asp:Label>

                       </td>
                                              <td>

        <asp:DropDownList ID="drpYear" runat="server" CssClass="drpList" Width="200px">
            <asp:ListItem></asp:ListItem>
            <asp:ListItem>2018</asp:ListItem>
            <asp:ListItem>2019</asp:ListItem>
        </asp:DropDownList>

                       </td>
                       <td>
<asp:Button ID="btnSearch" runat="server" OnClick="btnSearch_Click" Text="بحث" CssClass="submit" BackColor="#E9E9E9" ForeColor="Black" />
                       </td>
                   </tr>
               </table>--%>
           </td>
       </tr>
    <tr style="background-color: #F4F0EA;" align="center">
        <td style="font-size: 25px">
Dashboard            <%--<table cellpadding="2" cellspacing="0" border="0" width="100%">--%>
                <%--<tr>
                    <td height="50px" >--%>
                    <%--<table cellpadding="2" cellspacing="0" border="0" align="center">
                        <tr>
                            <td>
                                <asp:Label ID="lblTotalVisitors" runat="server" Text="عدد الطلبات : " 
                                    Font-Bold="True"></asp:Label>
                            </td>
                            <td>
                            <asp:Label ID="lblTotalVisitorsValue" runat="server" Font-Bold="True"></asp:Label>
                            </td>
                        </tr>
                    </table>--%>
                  <%--  </td>
 
                </tr>--%>
            <%--</table>--%>
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
<table cellpadding="0" cellspacing="0" width="99%" align="center" 
                    bgcolor="White" style="border: 1px solid #D0C4B4">
        <tr>
        <td valign="middle" colspan="2" bgcolor="#F6F4F0" height="30" 
                
                style="color: #493318; font-weight: bold; padding-right: 20px; padding-left: 20px;">
                                <table cellpadding="2" cellspacing="0" border="0">
                <tr>
                <td>
                    <asp:Image ID="Image2" runat="server" Height="30px" 
                        ImageUrl="~/IMAGES/iconReassess.png" Width="30px" 
                        meta:resourcekey="Image1Resource2" />
                </td>
                <td>
                                                            <asp:Label ID="Label1" runat="server" Text="Male" 
                                                                meta:resourcekey="Label7Resource2"></asp:Label>

                </td>
                </tr>
                </table>

            </td>
        </tr>
        <tr>
        <td>
        <div class="chart"   data-option="sucessOptions"   data-percent= <%Response.Write(lblFitCount.Text);%>   >
        <asp:Label ID="lblPreRegCount" runat="server" Text="" 
        meta:resourcekey="lblNonFeasibleResource1"></asp:Label>%</div>
        </td>
        <td valign="middle">
        <table cellpadding="2" cellspacing="0" border="0">
        <tr>
        <td align="center" valign="middle">
                                                                    <asp:Label ID="Label4" runat="server" Text="Total" 
                                                                        meta:resourcekey="Label8Resource2"></asp:Label>
            </td>
        </tr>
        <tr>
        <td align="center" valign="middle" 
                style="border-bottom-style: solid; border-bottom-width: 1px; border-bottom-color: #d7c5ab">
<asp:Label ID="lblFitCount" runat="server" 
                meta:resourcekey="lblOnProgressCountResource2" 
></asp:Label>
            </td>
        </tr>
        </table>

        </td>
        </tr>
        </table>

        </td>
        <td width="33%" valign="top">
                <table cellpadding="0" cellspacing="0" width="99%" align="center" 
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
                                                            <asp:Label ID="Label7" runat="server" Text="Female" 
                                                                meta:resourcekey="Label7Resource2"></asp:Label>

                </td>
                </tr>
                </table>

            </td>
        </tr>
        <tr>
        <td>
        <div class="chart"   data-option="pendingOptions"   data-percent= <%Response.Write(lblCheckInCount.Text);%>   >
        <asp:Label ID="lblUnfitCount" runat="server" Text="" 
        meta:resourcekey="lblNonFeasibleResource1"></asp:Label>%</div>
        </td>
        <td valign="middle">
        <table cellpadding="2" cellspacing="0" border="0">
        <tr>
        <td align="center" valign="middle">
                                                                    <asp:Label ID="Label8" runat="server" Text="Total" 
                                                                        meta:resourcekey="Label8Resource2"></asp:Label>
            </td>
        </tr>
        <tr>
        <td align="center" valign="middle" 
                style="border-bottom-style: solid; border-bottom-width: 1px; border-bottom-color: #d7c5ab">
<asp:Label ID="lblCheckInCount" runat="server" 
                meta:resourcekey="lblOnProgressCountResource2" 
></asp:Label>
            </td>
        </tr>

  
        </table>

        </td>
        </tr>
        </table>

        </td>
        <td width="33%" valign="top">
                <table cellpadding="0" cellspacing="0" width="99%" align="center" bgcolor="White" 
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
                                                           <asp:Label ID="Label15" runat="server" Text="Total" 
                                                               meta:resourcekey="Label15Resource2"></asp:Label>
           
                </td>
                </tr>
                </table>
               </td>
        </tr>
        <tr>
        <td>
                                                                     <div class="chart"   data-option="warningOptions"   data-percent= <%Response.Write(lblCheckOutCount.Text);%>   >
                                        <asp:Label ID="lblPendingCount" runat="server" Text="" 
                                             ></asp:Label>%</div>
        </td>
        <td valign="middle">
        <table cellpadding="2" cellspacing="0" border="0">
        <tr>
        <td align="center" valign="middle">
                                                                   <asp:Label ID="Label16" runat="server" Text="Total" 
                                                                       meta:resourcekey="Label16Resource2"></asp:Label>
            </td>
        </tr>
        <tr>
        <td align="center" valign="middle" 
                style="border-bottom-style: solid; border-bottom-width: 1px; border-bottom-color: #d7c5ab">
<asp:Label ID="lblCheckOutCount" runat="server" 
                meta:resourcekey="lblPendingOverdueCountResource2"></asp:Label>
        </td>
        </tr>

        </table>

        </td>
        </tr>
        </table>

        </td>
        </tr>
    </table>
    </td>
        </tr>
       <tr>
           <td>&nbsp;</td>
       </tr>
       <tr>
           <td align="center">
               <img src="../Images/COR_BANNER.jpg" height="250px" width="65%" />
           </td>
       </tr>
       </table>
        
        
        

    </form>
</body>
</html>
