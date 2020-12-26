<%@ Page Language="C#" AutoEventWireup="true" CodeFile="frmDiagnosisDetailsTemp.aspx.cs" Inherits="frmDiagnosisDetailsTemp" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
                <script language="javascript" type="text/javascript">
            window.onload = hidePrint;
            function hidePrint()
            {
                document.getElementById('divPrint').style.visibility = 'hidden';
                return true;
            }
            </script>
    <title></title>
    <link href="../css/SiteContent.css" rel="stylesheet" />
    <style>
    hr{
  border: 1px solid #003366;
}
     .message 
    {
    border: 1px solid #ADA;
    background: #EFE;
    }

        .auto-style1 {
            height: 19px;
        }

        </style>
</head>
<body >
     <script language="javascript" type="text/javascript">
            function PrintPanel() {
                var panel = document.getElementById("divPrint");
                var printWindow = window.open('', '', 'height=400,width=800');
                printWindow.document.write('<html><head><title></title>');
                printWindow.document.write('</head><body >');
                printWindow.document.write(divPrint.innerHTML);
                printWindow.document.write('</body></html>');
                printWindow.document.close();
                setTimeout(function () {
                    printWindow.print();
                }, 500);
                return false;
            }
        </script>
    <form id="form1" runat="server" enctype="multipart/form-data">
       <table cellpadding="0" cellspacing="0" align="center" bgcolor="White"  style="border: 1px solid #D0C4B4; margin-top: 10px;" width="99%">
                  <tr>
        <td valign="middle" class="trHeading"
                
                
                style="font-weight: bold; padding-right: 20px; padding-left: 20px;" 
                height="30px"> 
                    
                            <table cellpadding="2" cellspacing="0" border="0" width="100%">
                    <tr>
                        <td>
                                          <asp:Label ID="Label2" runat="server" Text="Request Details" 
                            meta:resourcekey="lblHeadingResource1" CssClass="lblBox"></asp:Label>
                        </td>

                    </tr>
                </table>

                </td>
 
        </tr>
                  <tr>
                      <td valign="top">
                                                  <table cellpadding="2" cellspacing="0" border="0" width="99%" align="center" id="tblMessage" runat="server" class="message" style="margin-top: 10px; margin-bottom: 10px">
        <tr>
        <td>
            <table  align="center" cellpadding="4" cellspacing="0" border="0" class ="middleTable">
             <tr>
                  <td>
                      <asp:Label ID="lblMessage" runat="server" CssClass="lblBox" Font-Bold="True" ForeColor="Black" meta:resourcekey="lblMessageResource1"></asp:Label>
                  </td>
               </tr>

            </table>
           </td>
           </tr>
           </table>


                                  <table cellpadding="6" cellspacing="0" border="0" align="center" id="tblMainControls" runat="server">
             <tr>
            <td  colspan="8" align="left">


                


                </td>

    </tr>
             <tr>
                 <td>
                     <asp:Label ID="lblNameEn4" runat="server" Text="Date of swab taken :"></asp:Label>
                 </td>
                 <td>
                     <asp:TextBox ID="txtDateSampleTaken" runat="server" CssClass="txtBox" Width="300px"></asp:TextBox>
                 </td>
<td style="color: RED">&nbsp;</td>
                 <td>
                                              <asp:Label ID="lblNameEn2" runat="server" Text="Name : "></asp:Label>
                                          </td>
                 <td>
                                              <asp:TextBox ID="cardHolderNameEn" runat="server" CssClass="txtBox" Width="300px"></asp:TextBox>
                                          </td>
                 
<td style="color: RED">&nbsp;</td>
                 
                 
             </tr>
             <tr>
                 <td>
                     <asp:Label ID="lblMaritalStatus1" runat="server" Text="Emirates Id Number : "></asp:Label>
                 </td>
                 <td>
                     <asp:TextBox ID="idn" runat="server" CssClass="txtBox" Width="300px"></asp:TextBox>
                 </td>
<td style="color: RED">&nbsp;</td>
                 <td>
                     <asp:Label ID="lblMaritalStatus2" runat="server" Text="Emirates Id Expiry :"></asp:Label>
                 </td>
                 <td>
                     <asp:TextBox ID="txtExpiryDate" runat="server" CssClass="txtBox" Width="300px" autocomplete="off"></asp:TextBox></td>
                 
<td style="color: RED">&nbsp;</td>
                 
                 
             </tr>
                                      <tr>
                                          <td>
                                              <asp:Label ID="lblNationality3" runat="server" Text="Nationality : "></asp:Label>
                                          </td>
                                          <td>
                                              <asp:TextBox ID="txtNationality" runat="server" CssClass="txtBox" Width="300px"></asp:TextBox>
                                          </td>
                                          <td style="color: RED">&nbsp;</td>
                                          <td>
                                              <asp:Label ID="lblNameEn7" runat="server" Text="Date of Birth : "></asp:Label></td>
                                          <td>
                                              <asp:TextBox ID="txtDob" runat="server" CssClass="txtBox" Width="300px"  autocomplete="off"></asp:TextBox></td>
                                          <td style="color: RED">&nbsp;</td>
                                      </tr>
             <tr>
                 <td >
                     <asp:Label ID="lblSex2" runat="server" Text="Gender : "></asp:Label>
                 </td>
                 <td >
                     

                     <asp:RadioButtonList ID="radGender" runat="server" RepeatDirection="Horizontal">
                         <asp:ListItem Value="1">Male</asp:ListItem>
                         <asp:ListItem Value="2">Female</asp:ListItem>
                     </asp:RadioButtonList>
                     

                 </td>
                 <td style="color: RED">
                     &nbsp;</td>
                 <td >
                     <asp:Label ID="lblNameEn8" runat="server" Text="Age : "></asp:Label>
                 </td>
                 <td >
                     <asp:TextBox ID="txtAge" runat="server" CssClass="txtBox" Width="300px"></asp:TextBox>
                 </td>
                 <td style="color: RED">
                     &nbsp;</td>
             </tr>
             <tr>
                 <td >
                     <asp:Label ID="lblSex3" runat="server" Text="Mobile Number : "></asp:Label></td>
                 <td >
                     

                     <asp:TextBox ID="txtTelNumber" runat="server" CssClass="txtBox" Width="300px"></asp:TextBox></td>
                 <td style="color: RED">
                     &nbsp;</td>
                 <td >
                     <asp:Label ID="lblSex4" runat="server" Text="Clinic Name : "></asp:Label></td>
                 <td >
                     

                     <asp:TextBox ID="txtClinicNae" runat="server" CssClass="txtBox" Width="300px"></asp:TextBox></td>
                 <td style="color: RED">
                     &nbsp;</td>
             </tr>
             <tr>
                 <td colspan="8">
                        <table cellpadding="2" cellspacing="0" border="0" align="center">
                    <tr>
              
                        <td>
                        <asp:Button ID="btnRegister" runat="server" CssClass="submit" Text="Print"  OnClientClick = "return PrintPanel();"  />
                        </td>
        
  
                    </tr>
                </table></td>
             </tr>
             <tr>
                 <td>
                     &nbsp;</td>
                 <td>
        
                                        <br />
                 </td>
                 <td>
                     &nbsp;</td>
                 <td>
                     &nbsp;</td>
                 <td>
                     &nbsp;</td>
                 <td>&nbsp;</td>
                 <td>
                     &nbsp;</td>
                 <td>
                     &nbsp;</td>
             </tr>
                                      </table>
                      </td>
                  </tr>

        </table>
        <div id="divPrint" runat="server">
                <table cellpadding="0" cellspacing="0" border="0" dir="ltr" >
                    <tr>
                        <td class="auto-style1">Date</td>
                        <td class="auto-style1">
                            <asp:Label ID="lblSampleTaken" runat="server" Text="Label"></asp:Label></td>
                    </tr>
                  <tr>
                        <td>ID Number</td>
                        <td><asp:Label ID="lblIdn" runat="server" Text="Label"></asp:Label></td>
                    </tr>
                    <tr>
                        <td>Name</td>
                        <td><asp:Label ID="lblNameEn" runat="server" Text="Label"></asp:Label></td>
                        </tr>
                    <tr>
                        <td>Nationality</td>
                        <td><asp:Label ID="lblNationality" runat="server" Text="Label"></asp:Label></td>
                    </tr>
                    <tr>
                        <td>Gender</td>
                        <td>
                            <asp:Label ID="lblSex" runat="server" Text="Label"></asp:Label>
                            </td>
                    </tr>
                    <tr>
                        <td>Age</td>
                        <td><asp:Label ID="lblAge" runat="server" Text="Label"></asp:Label></td>
                    </tr>
                         </table>
                    </div>
    </form>
</body>
</html>
