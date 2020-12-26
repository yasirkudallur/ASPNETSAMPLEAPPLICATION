<%@ Page Language="C#" AutoEventWireup="true" CodeFile="frmDiagnosisDetails.aspx.cs" Inherits="VISITORS_MANAGEMENT_frmDiagnosisDetails" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
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
            height: 46px;
        }

    </style>
</head>
<body dir="rtl">
    <form id="form1" runat="server" enctype="multipart/form-data">
                 <table cellpadding="0" cellspacing="0" align="center" bgcolor="White"  style="border: 1px solid #D0C4B4; margin-top: 10px;" width="99%">
                  <tr>
        <td valign="middle" class="trHeading"
                
                
                style="font-weight: bold; padding-right: 20px; padding-left: 20px;" 
                height="30px"> 
                    
                            <table cellpadding="2" cellspacing="0" border="0" width="100%">
                    <tr>
                        <td>
                                          <asp:Label ID="Label10" runat="server" Text="إدارة الفحوصات" 
                            meta:resourcekey="lblHeadingResource1" CssClass="lblBox"></asp:Label>
                        </td>

                    </tr>
                </table>

                </td>
 
        </tr>
                     <tr>
                         <td>
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
                             <table cellpadding="2" cellspacing="0" border="0" width="100%" id="tblMainControls" runat="server">
                                 <tr>
                                     <td width="25%"><asp:Label ID="Label14" runat="server" Text="اختبار طبي - 1" CssClass="lblBox" Font-Bold="True" Font-Size="20px"></asp:Label></td>
                                     <td width="25%"><asp:Label ID="Label15" runat="server" Text="اختبار طبي - 2" CssClass="lblBox" Font-Bold="True" Font-Size="20px"></asp:Label></td>
                                     <td width="25%"><asp:Label ID="Label16" runat="server" Text="اختبار طبي - 3" CssClass="lblBox" Font-Bold="True" Font-Size="20px"></asp:Label></td>
                                     <td width="25%"><asp:Label ID="Label17" runat="server" Text="اختبار طبي - 4" CssClass="lblBox" Font-Bold="True" Font-Size="20px"></asp:Label></td>
                                 </tr>
                                 <tr>
                                     <td width="25%" valign="top">
                                         <table cellpadding="2" cellspacing="0">
                                             <tr>
                                                 <td>
                <asp:CheckBox ID="chkTestOneEye" runat="server" Font-Size="20px"  Text="فحص العين" AutoPostBack="True" OnCheckedChanged="chkTestOneEye_CheckedChanged" />
                                                 </td>
                                             </tr>
                                             <tr>
                                                 <td>
                <asp:CheckBox ID="chkTestOneHeight" runat="server" Font-Size="20px" Text="الطول" AutoPostBack="True" OnCheckedChanged="chkTestOneHeight_CheckedChanged" />
                                                 </td>
                                             </tr>
                                             <tr>
                                                 <td>
                <asp:CheckBox ID="chkTestOneWeight" runat="server" Font-Size="20px" Text="الوزن" AutoPostBack="True" OnCheckedChanged="chkTestOneWeight_CheckedChanged" />
                                                 </td>
                                             </tr>
                                             <tr>
                                                 <td>
                                                     <asp:FileUpload ID="fuTestOne" runat="server" CssClass="txtBox" />
                                                 </td>
                                             </tr>
                                             <tr>
                                                 <td>
                                                     <asp:Button ID="btnAddFileTestOne" runat="server" CssClass="submit" Text="ارفاق ملف" OnClick="btnAddFileTestOne_Click" />
                                                 </td>
                                             </tr>
                                         </table>
                                     </td>
                                     <td width="25%"  valign="top">
                                          <table cellpadding="2" cellspacing="0">
                                             <tr>
                                                 <td>
                <asp:CheckBox ID="chkTestTwoBlood" runat="server" Font-Size="20px" Text="فحص الدم" AutoPostBack="True" OnCheckedChanged="chkTestTwoBlood_CheckedChanged" />
                                                 </td>
                                             </tr>
                                             <tr>
                                                 <td>
                <asp:CheckBox ID="chkTestTwoUrine" runat="server" Font-Size="20px" Text="فحص البول" AutoPostBack="True" OnCheckedChanged="chkTestTwoUrine_CheckedChanged" />
                                                 </td>
                                             </tr>
                                              <tr>
                                                  <td>
                                                     <asp:FileUpload ID="fuTestTwo" runat="server" CssClass="txtBox" />
                                                  </td>
                                              </tr>
                                              <tr>
                                                  <td>

                                                     <asp:Button ID="btnAddFileTestTwo" runat="server" CssClass="submit" Text="ارفاق ملف" OnClick="btnAddFileTestTwo_Click" />

                                                  </td>
                                              </tr>
                                             </table>
                                     </td>
                                     <td width="25%" valign="top">
                                         <table cellpadding="2" cellspacing="0">
                                             <tr>
                                                 <td>
                <asp:CheckBox ID="chkTestThreeXray" runat="server" Font-Size="20px" Text="الاشعة" AutoPostBack="True" OnCheckedChanged="chkTestThreeXray_CheckedChanged" />
                                                 </td>
                                             </tr>
                                             <tr>
                                                 <td>

                                                     <asp:FileUpload ID="fuTestThree" runat="server" CssClass="txtBox" />

                                                 </td>
                                             </tr>
                                             <tr>
                                                 <td>

                                                     <asp:Button ID="btnAddFileTestThree" runat="server" CssClass="submit" Text="ارفاق ملف" OnClick="btnAddFileTestThree_Click" />

                                                 </td>
                                             </tr>
                                             </table></td>
                                     <td width="25%"  valign="top">
                                         <table cellpadding="2" cellspacing="0">
                                             <tr>
                                                 <td>
                <asp:CheckBox ID="chkTestFourEye" runat="server" Font-Size="20px" Text="اخصائي العيون" AutoPostBack="True" OnCheckedChanged="chkTestFourEye_CheckedChanged" />
                                                 </td>
                                             </tr>
                                             <tr>
                                                 <td>
                <asp:CheckBox ID="chkTestFourBone" runat="server" Font-Size="20px" Text="اخصائي الجلدية" AutoPostBack="True" OnCheckedChanged="chkTestFourBone_CheckedChanged" />
                                                 </td>
                                             </tr>
                                             <tr>
                                                 <td>
                <asp:CheckBox ID="chkTestFourSkin" runat="server" Font-Size="20px" Text="اخصائية العظام" AutoPostBack="True" OnCheckedChanged="chkTestFourSkin_CheckedChanged" />
                                                 </td>
                                             </tr>
                                             <tr>
                                                 <td>
                <asp:CheckBox ID="chkTestFourPsych" runat="server" Font-Size="20px" Text="اخصائي النفسية" AutoPostBack="True" OnCheckedChanged="chkTestFourPsych_CheckedChanged" />
                                                 </td>
                                             </tr>
                                             <tr>
                                                 <td>
                <asp:CheckBox ID="chkTestFourSurgery" runat="server" Font-Size="20px" Text="اخصائي الجراحة" AutoPostBack="True" OnCheckedChanged="chkTestFourSurgery_CheckedChanged" />
                                                 </td>
                                             </tr>
                                             <tr>
                                                 <td>
                <asp:CheckBox ID="chkTestFourGp" runat="server" Font-Size="20px" Text="الممارس العام" AutoPostBack="True" OnCheckedChanged="chkTestFourGp_CheckedChanged" />
                                                 </td>
                                             </tr>
                                             <tr>
                                                 <td>
                                                     <asp:FileUpload ID="fuTestFour" runat="server" CssClass="txtBox" />
                                                 </td>
                                             </tr>
                                             <tr>
                                                 <td>

                                                     <asp:Button ID="btnAddFileTestFour" runat="server" CssClass="submit" Text="ارفاق ملف" OnClick="btnAddFileTestFour_Click" />

                                                 </td>
                                             </tr>
                                         </table></td>
                                 </tr>
                                 <tr>
                                     <td valign="top" colspan="4">
                                     <hr />
                                     </td>
                                 </tr>
                                 <tr>
                                     <td colspan="4">
                                         <table cellpadding="2" cellspacing="0" border="0">
                                             <tr>
                                                 <td class="auto-style1"><asp:Label ID="Label18" runat="server" Text="النتائج : " CssClass="lblBox" Font-Bold="False" Font-Size="20px"></asp:Label></td>
                                                 <td class="auto-style1">
                                                     <asp:RadioButtonList ID="radFitnessSelection" runat="server" AutoPostBack="True" Font-Size="20px" OnSelectedIndexChanged="radFitnessSelection_SelectedIndexChanged" RepeatDirection="Horizontal">
                                                         <asp:ListItem Value="1">لائق </asp:ListItem>
                                                         <asp:ListItem Value="0">غير لائق</asp:ListItem>
                                                     </asp:RadioButtonList>
                                                 </td>
                                             </tr>
                                             <tr>
                                                 <td><asp:Label ID="lblAttachFitnessCertificate" runat="server" Text="Attach Fitness Certificate : " CssClass="lblBox" Font-Bold="False" Font-Size="20px"></asp:Label></td>
                                                 <td>
                                                     <asp:FileUpload ID="fuFitCertificate" runat="server" CssClass="txtBox" />
                                                 </td>
                                             </tr>
                                             <tr>
                                                 <td>&nbsp;</td>
                                                 <td>
                                                     <asp:Button ID="btnSubmit" runat="server" CssClass="submit" OnClick="btnSubmit_Click" Text="Submit" />
                                                 </td>
                                             </tr>
                                         </table>
                                     </td>
                                 </tr>
                                 <tr>
                                     <td width="25%">&nbsp;</td>
                                     <td width="25%">&nbsp;</td>
                                     <td width="25%">&nbsp;</td>
                                     <td width="25%">&nbsp;</td>
                                 </tr>
                             </table>
                         </td>
                     </tr>
        </table>
    </form>
</body>
</html>
