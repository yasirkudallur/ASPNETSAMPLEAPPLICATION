<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="VISITORS_MANAGEMENT_Default" %>

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


</head>
<body>
    <form id="form1" runat="server">
        <table cellpadding="2" cellspacing="0" border="0" width="100%">
            <tr>
                <td align="center">
                                                             <div class="chart"   data-option="recieved"    data-percent= <%Response.Write(lblPending.Text);%>   >
                                        <asp:Label ID="lblPending" runat="server" Text="40" 
                                                 meta:resourcekey="lblPendingResource1"></asp:Label></div>
                </td>
                <td align="center">
                                    <div class="chart"   data-option="recieved"   data-percent= <%Response.Write(lblTrasnferred.Text);%>   >
                                        <asp:Label ID="lblTrasnferred" runat="server" Text="40" 
                                            meta:resourcekey="Label1Resource1"></asp:Label></div>
                </td>
                <td align="center">
                                                                     <div class="chart"   data-option="warningOptions"   data-percent= <%Response.Write(lblFeasible.Text);%>   >
                                        <asp:Label ID="lblFeasible" runat="server" Text="40" 
                                             meta:resourcekey="lblFeasibleResource1"></asp:Label></div>
                </td>
                <td align="center">
                                                                            <div class="chart"   data-option="pendingOptions"   data-percent= <%Response.Write(lblNonFeasible.Text);%>   >
                                        <asp:Label ID="lblNonFeasible" runat="server" Text="40" 
                                                meta:resourcekey="lblNonFeasibleResource1"></asp:Label></div>
                </td>
                <td align="center">
                                     <div class="chart"   data-option="sucessOptions"   data-percent= <%Response.Write(lblImplemented.Text);%>   >
                                        <asp:Label ID="lblImplemented" runat="server" Text="40" 
                                            meta:resourcekey="Label1Resource1"></asp:Label>%</div>
                </td>
            </tr>
        </table>

           
    </form>
</body>
</html>
