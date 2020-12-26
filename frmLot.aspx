<%@ Page Language="C#" AutoEventWireup="true" CodeFile="frmLot.aspx.cs" Inherits="frmLot" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="Styles/SiteContent.css" rel="stylesheet" type="text/css" />
<%--    <script language="javascript" type="text/javascript">
        window.onload = maximizeWindow;
        function maximizeWindow() {
            if (top.moveTo)
                top.moveTo(0, 0);
            if (top.resizeTo)
                top.resizeTo(screen.availWidth - 25, screen.availHeight - 25);
            if (top.outerWidth)
                top.outerWidth = screen.availWidth - 25;
            if (top.outerHeight)
                top.outerHeight = screen.availHeight - 25;
        }
    </script>--%>
    </head>
<body dir="rtl" >
    <form id="form1" runat="server">

<asp:ScriptManager ID="ScriptManager1" runat="server">
</asp:ScriptManager>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>
<table cellpadding="0" cellspacing ="0" border ="0" width="100%">
    <tr>
        <td>
            <img src="Images/Logo-1.png" width="150px" height="141px" />
        </td>
        <td align="center">
            <asp:Image ID="Image1" runat="server" ImageUrl="~/Images/progress-new.gif" />
<%--            <asp:Image ID="imgStableCube" runat="server" ImageUrl="~/Images/progress-new - Copy.gif" />--%>
            <asp:ImageButton ID="imgStableCube" runat="server" ImageUrl="~/Images/progress-new - Copy.gif" OnClick="imgStableCube_Click"/>
        </td>
        <td align="left">
            <img src="Images/Logo-1.png" width="150px" height="141px" />
        </td>
    </tr>
    <tr>
        <td colspan="3">
              <table cellpadding="2" cellspacing="0" align="center">
                <tr>
                    <td>
                     <asp:Label ID="lblMessage" runat="server" Font-Bold="True" Font-Size="20px" />
                    </td>
                    <td>
            <asp:Label ID="txtFullNameAr" runat="server" Font-Bold="True" Font-Size="20px"></asp:Label>
                    </td>
                </tr>
            </table>


            <br />

            <table cellpadding="2" cellspacing="0" align="center">
                <tr>
                    <td>
                                                <asp:Image ID="imgGift1" runat="server" ImageUrl="~/Images/baloon.gif" Height="300px" Width="300px" />
                    </td>
                    <td>&nbsp;</td>
                    <td>
                                   <img alt="Photo" id="PhotoBase64" runat="server" width="300" height="300" src="~/Images/profile.jpg"></img>
                    </td>
                                        <td>&nbsp;</td>
                    <td>
                        <asp:Image ID="imgGift" runat="server" ImageUrl="~/Images/baloon.gif" Height="300px" Width="300px" />
                    </td>

                </tr>
            </table>
 
            <br />
            
            <br />
            <asp:Label ID="lblTime" runat="server" Visible="False" />
            <br />

            <br />
            <asp:Timer ID="Timer1" runat="server" OnTick="GetTime" Interval="500" Enabled="False" />
            <br />
        </td>
    </tr>
    <tr>
        <td>
            <img src="Images/Logo-1.png" width="150px" height="141px" />
        </td>
        <td align="center">
            <asp:Button ID="Button1" runat="server"  OnClick="Button1_Click" BorderColor="Green" BackColor="Green" Height="100px" Width="183px" Visible="False" />
        </td>
        <td align="left">
            <img src="Images/Logo-1.png" width="150px" height="141px" />
        </td>
    </tr>

</table>
     </ContentTemplate>
</asp:UpdatePanel>       




</form>


</body>
</html>
