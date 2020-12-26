<%@ Page Language="C#" AutoEventWireup="true" CodeFile="frmLogin.aspx.cs" Inherits="frmLogin" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
   <head runat="server">
<meta http-equiv="X-UA-Compatible" content="IE=11"/>
<meta http-equiv="X-UA-Compatible" content="IE=10"/>
<meta http-equiv="X-UA-Compatible" content="IE=9"/>
<meta http-equiv="X-UA-Compatible" content="IE=8"/>

<meta http-equiv="X-UA-Compatible" content="IE=edge"/>
<meta http-equiv="X-UA-Compatible" content="IE=EmulateIE8" />
<meta http-equiv="X-UA-Compatible" content="IE=EmulateIE9" />
<meta http-equiv="X-UA-Compatible" content="IE=EmulateIE10" />
<meta http-equiv="X-UA-Compatible" content="IE=EmulateIE11" />
       <link href="css/SiteContent.css" rel="stylesheet" />
                    
       <script language="javascript" type="text/javascript">
           function validateControls() {
               if (document.getElementById("txtUserName").value.length == 0) {
                   alert("Please enter email address!!!");
                   document.getElementById("txtUserName").focus();
                   return false;
               }
               if (document.getElementById("txtPassword").value.length == 0) {
                   alert("Please enter password!!!");
                   document.getElementById("txtPassword").focus();
                   return false;
               }
               return true;
           }
       </script> 
        <style>
    hr{
  border: 1px solid #B7A500;
}

    </style>
   </head>
<body>
    <form id="form1" runat="server">
    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
    </asp:ToolkitScriptManager>
        <table cellpadding="0" cellspacing="0" border="0" width="100%" bgcolor="White">
            <tr >
                         <td align="left" width="30%">
                                        <table cellpadding="0" cellspacing="0" border="0">
                          <tr>

                              <td width="10px">&nbsp;&nbsp;&nbsp;&nbsp;</td>
                              <td><img src="Images/icon_moi.png" /></td>
                               
                          </tr>
                      </table>
                


                         </td>
      
                          <td align="center" width="40%" style="font-size: 25px; font-weight: bold; color: #B7A500;">
                              نظام تسجيل المسحات
<br />
Swab Registration System                    <%--<img src="Images/systemname.png" />--%>

                </td>
     
                  <td align="right" width="30%">
           
                                                   <table cellpadding="0" cellspacing="0" border="0">
                          <tr>
                              <td><img src="Images/govlogo.jpg" /></td>
                              <td width="10px">&nbsp;&nbsp;</td>
  
                          </tr>
                      </table>
                </td>
            </tr>
<%--            <tr >
                         <td align="right" width="35%">&nbsp;</td>
                <td align="center" width="30%" style="font-size: 35px; font-weight: bold">
                    &nbsp;</td>
     
                  <td align="left" width="35%">
                      &nbsp;</td>
            </tr>--%>
<%--            <tr>
                <td colspan="3" style="border-top-style: solid; border-top-width: 40px; border-top-color: #B29300">

                </td>
            </tr>--%>
                        <tr>
                <td colspan="3" >
                    <hr />
                </td>
            </tr>
            <tr>
                <td colspan="3">
                    <img src="Images/Banner.jpg" width="100%" />
                </td>
            </tr>
            <tr>
                <td colspan="3" align="center" >
                    <asp:Label ID="Label3" runat="server" Text="Login to the System" Font-Bold="False" Font-Size="30px" ForeColor="#B7A500"></asp:Label>
                    <hr />

                </td>
            </tr>
            <tr>
                <td colspan="3">
                    <table cellpadding="2" cellspacing="0" border="0" width="100%">
                        <tr>
                            <td align="right">
                                <img src="Images/uae_logo.png" style="visibility: hidden" />
                            </td>
                            <td>
                                <table cellpadding="5"cellspacing="0" border="0" align="center">
                                                <tr>
                            <td>&nbsp;</td>
                            <td>
                                <asp:Label ID="Label1" runat="server" Text="Username : "></asp:Label>
                                                    </td>
                            <td>
                                <asp:TextBox ID="txtUserName" runat="server" CssClass="txtBox" Width="350px"></asp:TextBox>
                                                    </td>
                            <td style="color: red">
                                *</td>
                        </tr>
                                                <tr>
                            <td>&nbsp;</td>
                            <td>
                                <asp:Label ID="Label2" runat="server" Text="Password : "></asp:Label>
                                                    </td>
                            <td>
     


<asp:TextBox ID="txtPassword" runat="server" CssClass="txtBox" TextMode="Password" Width="350px"></asp:TextBox>

                                                    </td>
                            <td style="color: red">
                                *</td>
                        </tr>
                                                <tr>
                            <td>&nbsp;</td>
                            <td>&nbsp;</td>
                            <td>
                                <asp:Button ID="btnLogin" runat="server" CssClass="md-btn-primary" Text="Login" OnClick="btnLogin_Click1" BackColor="#E9E9E9" Width="200px" ForeColor="Black" />
                                                    </td>
                            <td></td>
                        </tr>
                                                <tr>
                            <td>&nbsp;</td>
                            <td>&nbsp;</td>
                            <td>



                  <asp:Label ID="lblMessage" runat="server" Text=""></asp:Label>
        

                                                    </td>
                            <td>&nbsp;</td>
                        </tr>
                                                </table>
                            </td>
                            <td align="left" >
                                <table cellpadding="0" cellspacing="0" border="0">
                                    <tr>

                                        <td>
                                            <img src="Images/uae_logo.png" style="visibility: hidden" />
                                        </td>
                                                                                <td width="10px"></td>
                                    </tr>
                                </table>
                                
                            </td>
                        </tr>
                    </table>
                    
                </td>
            </tr>
            <tr>
                <td colspan="3">&nbsp;</td>
            </tr>
            <tr>
               <td colspan="3" style="border-top-style: solid; border-top-width:10px; border-top-color: #B29300">

                </td>
            </tr>
            <td valign="middle" colspan="3" height="30px" align="center" style="font-size: 20px">
                Copyright © 2020</td>
        </table>

                  </form>



</body>
</html>
