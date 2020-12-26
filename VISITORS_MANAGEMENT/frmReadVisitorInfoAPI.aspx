<%@ Page Language="C#" AutoEventWireup="true" CodeFile="frmReadVisitorInfoAPI.aspx.cs" Inherits="frmReadVisitorInfoAPI" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
  
    <link href="../css/SiteContent.css" rel="stylesheet" />
    <script src="../js/base64.js" type="text/javascript"></script>
    <script src="../js/errors.js" type="text/javascript"></script>
    <script src="../js/zfcomponent.js" type="text/javascript"></script>
    <script src="http://code.jquery.com/jquery-1.7.1.min.js"></script>
            <script language="javascript" type="text/javascript">
            window.onload = hidePrint;
            function hidePrint()
            {
                document.getElementById('divPrint').style.visibility = 'hidden';
                return true;
            }
            function restartService() {
                alert("تم إعادة تشغيل الخدمة بنجاح");
                return true;
            }
            function errorService() {
                alert("Couldnt restart the service");
                return true;
            }
            function validateControls()
            {
                if (document.getElementById("txtDateSampleTaken").value.length == 0) {
                    document.getElementById("txtDateSampleTaken").focus();
                    alert("Please enter date of sample taken.");
                    return false;
                }
                if (document.getElementById("cardHolderNameEn").value.length == 0) {
                    document.getElementById("cardHolderNameEn").focus();
                    alert("Please enter name.");
                    return false;
                }
                if (document.getElementById("txtNationality").value.length == 0) {
                    document.getElementById("txtNationality").focus();
                    alert("Please enter nationality.");
                    return false;
                }
                if (document.getElementById("txtDob").value.length == 0) {
                    document.getElementById("txtDob").focus();
                    alert("Please enter date of birth.");
                    return false;
                }
                var varAction = "";
                var rbClear = document.getElementById("radGender");
                if (rbClear != null) {
                    var radioClear = rbClear.getElementsByTagName("input");
                    var isCheckedClear = false;
                    for (var i = 0; i < radioClear.length; i++) {
                        if (radioClear[i].checked) {
                            varAction = i;
                            isCheckedClear = true;
                            break;
                        }
                    }
                }
                else {
                    varAction = 0;//Idea is clear.
                }

                if (isCheckedClear == false) {
                    alert("Please choose the field 'Gender'");
                    document.getElementById("radGender").focus();
                    return false;
                }
                
                if (document.getElementById("txtTelNumber").value.length == 0) {
                    document.getElementById("txtTelNumber").focus();
                    alert("Please enter mobile number.");
                    return false;
                }
                
                
                if (document.getElementById("txtLocation").value.length == 0) {
                    document.getElementById("txtLocation").focus();
                    alert("Please enter location.");
                    return false;
                }
                //if (document.getElementById("drpClicnics").options[document.getElementById("drpClicnics").selectedIndex].value == -1) {
                //    alert("يرجى إختيار اسم الموقع");
                //    document.getElementById("drpClicnics").focus();
                //    return false;
                //}
                return true;
            }
            </script>
       


    <style>
    .message 
    {
    border: 1px solid #ADA;
    background: #EFE;
    }

    .modal
    {
        position: fixed;
        z-index: 999;
        height: 100%;
        width: 100%;
        top: 0;
        background-color: Black;
        filter: alpha(opacity=60);
        opacity: 0.6;
        -moz-opacity: 0.8;
    }
    .center
    {
        z-index: 1000;
        margin: 300px auto;
        padding: 10px;
        width: 130px;
        background-color: White;
        border-radius: 10px;
        filter: alpha(opacity=100);
        opacity: 1;
        -moz-opacity: 1;
    }
    .center img
    {
        height: 128px;
        width: 128px;
    }
     .menu
        {
               z-index:1000;
            }
          .modalBackground
            {
                background-color: Black;
                filter: alpha(opacity=60);
                opacity: 0.6;
            }
            .modalPopup
            {
                background-color: #FFFFFF;
                width: 300px;
                border: 3px solid #0DA9D0;
                padding: 0;
            }
            .modalPopup .header
            {
                background-color: #2FBDF1;
                height: 30px;
                color: White;
                line-height: 30px;
                text-align: center;
                font-weight: bold;
            }
            .modalPopup .body
            {
                min-height: 50px;
                line-height: 30px;
                text-align: center;
                font-weight: bold;
                margin-bottom: 5px;
            }






        </style>
    </head>
<body>
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
    <form id="form1" runat="server">
  <script src="../JQUERY_AUTO_COMPLETE/jquery-1.10.0.min.js" type="text/javascript"></script>
        <link href="../JQUERY_AUTO_COMPLETE/jquery-ui.css" rel="stylesheet" />
     
        <script src="../JQUERY_AUTO_COMPLETE/jquery-ui.min.js" type="text/javascript"></script>


        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>

    <input type="hidden" id="ef_idn_cn" name="ef_idn_cn" value="" />
    <input type="hidden" id="ef_non_mod_data" name="ef_non_mod_data" value="" />
    <input type="hidden" id="ef_mod_data" name="ef_mod_data" value="" />
    <input type="hidden" id="ef_sign_image" name="ef_sign_image" value="" />
    <input type="hidden" id="ef_photo" name="ef_photo" value="" />
    <input type="hidden" id="ef_root_cert" name="ef_root_cert" value="" />
    <input type="hidden" id="ef_home_address" name="ef_home_address" value="" />
    <input type="hidden" id="ef_work_address" name="ef_work_address" value="" />
    
    <input type="hidden" id="certificate" name="certificate" value="" />
    <input type="hidden" id="signature" name="signature" value="" />

<%--    <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="UpdatePanel1"
        ClientIDMode="Predictable" ViewStateMode="Inherit">
        <ProgressTemplate>
    <div class="modal"  >
        <div class="center">
            <img alt="" src="loader.gif" />
        </div>
    </div>
        </ProgressTemplate>
    </asp:UpdateProgress>--%>

            <asp:HiddenField ID="hfCustomerId" runat="server" />
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
                  <table cellpadding="0" cellspacing="0" align="center" bgcolor="White"  style="border: 1px solid #D0C4B4; margin-top: 10px;" width="99%">
                  <tr>
        <td valign="middle" class="trHeading"
                
                
                style="font-weight: bold; padding-right: 20px; padding-left: 20px;" 
                height="30px"> 
                    
                            <table cellpadding="2" cellspacing="0" border="0" width="100%">
                    <tr>
                        <td>
                                          <asp:Label ID="Label2" runat="server" Text="Add New Customer" 
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


                


                <asp:ImageButton ID="imgBtnService" runat="server" Height="32px" ImageUrl="~/Images/iconService.png" OnClick="imgBtnService_Click" Width="32px" />


                


                </td>

    </tr>
             <tr>
                 <td>
                     <asp:Label ID="lblNameEn4" runat="server" Text="Date of taken Swab : "></asp:Label>
                 </td>
                 <td>
                     <asp:TextBox ID="txtDateSampleTaken" runat="server" CssClass="txtBox" Width="300px"  autocomplete="off"></asp:TextBox>
                     <asp:CalendarExtender ID="CalendarExtender1" TargetControlID="txtDateSampleTaken" runat="server" Format="dd/MM/yyyy"></asp:CalendarExtender>
                 </td>
<td style="color: RED">*</td>
                 <td>
                     <asp:Label ID="lblNameEn2" runat="server" Text="Name : "></asp:Label>
                 </td>
                 <td>
                     <asp:TextBox ID="cardHolderNameEn" runat="server" CssClass="txtBox" Width="300px"  autocomplete="off"></asp:TextBox>
                 </td>
                 
<td style="color: RED">*</td>
                 
                 
             </tr>
                                      <tr>
                                          <td>
                                              <asp:Label ID="lblMaritalStatus1" runat="server" Text="Emirates Id Number : "></asp:Label>
                                          </td>
                                          <td>
                                              <asp:TextBox ID="idn" runat="server" CssClass="txtBox" Width="300px"  autocomplete="off"></asp:TextBox>
                                              <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" FilterType="Numbers" TargetControlID="idn">
                                              </asp:FilteredTextBoxExtender>
                                          </td>
                                          <td style="color: RED">&nbsp;</td>
                                          <td>
                                              <asp:Label ID="lblMaritalStatus2" runat="server" Text="Emirates Id Expiry :"></asp:Label>
                                          </td>
                                          <td>
                                              <asp:TextBox ID="txtExpiryDate" runat="server" CssClass="txtBox" Width="300px" autocomplete="off"></asp:TextBox>
                                             <asp:CalendarExtender ID="CalendarExtender2" runat="server" Format="dd/MM/yyyy" TargetControlID="txtExpiryDate">
                                              </asp:CalendarExtender>
                                          </td>
                                          <td style="color: RED">&nbsp;</td>
                                      </tr>
                                      <tr>
                                          <td>
                                              <asp:Label ID="lblNationality3" runat="server" Text="Nationality : "></asp:Label>
                                          </td>
                                          <td>
                                              <asp:TextBox ID="txtNationality" runat="server" CssClass="txtBox" Width="300px"  autocomplete="off"></asp:TextBox>
                                          </td>
                                          <td style="color: RED">*</td>
                                          <td>
                                              <asp:Label ID="lblNameEn7" runat="server" Text="Date of Birth : "></asp:Label>
                                          </td>
                                          <td>
                                              <asp:TextBox ID="txtDob" runat="server" CssClass="txtBox" Width="300px"  autocomplete="off"></asp:TextBox>
                                              <asp:CalendarExtender ID="ceDob" runat="server" Format="dd/MM/yyyy" TargetControlID="txtDob">
                                              </asp:CalendarExtender>
                                          </td>
                                          <td style="color: RED">*</td>
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
                     *</td>
                 <td >
                     <asp:Label ID="lblSex3" runat="server" Text="Mobile Number : "></asp:Label>
                 </td>
                 <td >
                     <asp:TextBox ID="txtTelNumber" runat="server" CssClass="txtBox" Width="300px"  autocomplete="off"></asp:TextBox>
                 </td>
                 <td style="color: RED">
                     *</td>
             </tr>
                                      <tr>
                                          <td>
                                              <asp:Label ID="lblSex4" runat="server" Text="Location : "></asp:Label>
                                          </td>
                                          <td>
                                             <%-- <asp:CalendarExtender ID="txtDateSampleTaken0_CalendarExtender" runat="server" Format="dd/MM/yyyy" TargetControlID="txtDateSampleTaken0">
                                              </asp:CalendarExtender>--%>
                                              <asp:TextBox ID="txtLocation" runat="server" CssClass="txtBox" Width="300px" ></asp:TextBox>

                                          </td>
                                          <td style="color: RED">*</td>
                                          <td>
                                              &nbsp;</td>
                                          <td>
                                              &nbsp;</td>
                                          <td style="color: RED">&nbsp;</td>
                                      </tr>
             <tr>
                 <td colspan="8">
                        <table cellpadding="2" cellspacing="0" border="0" align="center">
                    <tr>
                        <td>
                            <asp:Button ID="btnReadEmiratesIdCard" runat="server" CssClass="submit" Text="Read Emirates Id Card ..." OnClick="btnReadEmiratesIdCard_Click"  />
                        </td>
                        
                        <td>
                            <asp:Button ID="Button1" runat="server" CssClass="submit" Text="Save" OnClick="btnRegister_Click" />
                            <asp:Button ID="btnSave" runat="server" CssClass="submit" Text="Save" OnClick="btnSave_Click" Visible="False" />
                        </td>
                        <td>
                        <asp:Button ID="btnRegister" runat="server" CssClass="submit" Text="Print"  OnClientClick = "return PrintPanel();" Visible="False"  />
                        </td>
                        <td>
                        
                        </td>
                        <td>
                            <asp:ImageButton ID="ImageButton3" runat="server"  onclick="imgBtnReadPublicData_Click" 
                  BackColor="Transparent" BorderColor="Transparent"  Height="30px" 
                                                      ImageUrl="~/Images/reader.png" Width="30px" 
                                                      ToolTip="Read data from Emirates Id Card..." Visible="False"></asp:ImageButton>
                        </td>
  
                    </tr>
                </table></td>
             </tr>
             <tr>
                 <td>
                     &nbsp;</td>
                 <td>

                     <asp:DropDownList ID="drpClicnics" runat="server" CssClass="drpList" Width="310px" Visible="False">
                         <asp:ListItem></asp:ListItem>
                         <asp:ListItem Value="1">مركز شرطة المناطق الصناعية</asp:ListItem>
                         <asp:ListItem Value="1">مركز شرطة الغرب</asp:ListItem>
                     </asp:DropDownList>

                 </td>
                 <td>
                     &nbsp;</td>
                 <td>
                     &nbsp;</td>
                 <td>
                     

                 </td>
                 <td>&nbsp;</td>
                 <td>
                     <asp:Label ID="lblNameEn8" runat="server" Text="Age : " Visible="false"></asp:Label></td>
                 <td>
                     <asp:TextBox ID="txtAge" runat="server" CssClass="txtBox" Width="150px" Visible="false"></asp:TextBox>
                     <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" runat="server" FilterType="Numbers" TargetControlID="txtAge">
                     </asp:FilteredTextBoxExtender>
                 </td>
             </tr>
                                      </table>
                      </td>
                  </tr>
                      <tr>
                          <td align="center">
                              <img src="../Images/COR_BANNER.jpg" height="250px" width="65%" />
                          </td>
                      </tr>

        </table>
            <br />
                <asp:HiddenField ID="hfEidCardExist" runat="server" />
                <div id="divPrint" runat="server">
                <table cellpadding="0" cellspacing="0" border="0" dir="ltr" >
                    <tr >
                        <td>Date</td>
                        <td>
                            <asp:Label ID="lblSampleTaken" runat="server" Text=""></asp:Label></td>
                    </tr>
                  <tr>
                        <td>ID Number</td>
                        <td><asp:Label ID="lblIdNumber" runat="server" Text=""></asp:Label></td>
                    </tr>
                    <tr>
                        <td>Name</td>
                        <td><asp:Label ID="lblNameEn" runat="server" Text=""></asp:Label></td>
                    </tr>
                    <tr  >
                        <td >Nationality</td>
                        <td >
                            <asp:Label ID="lblNationality" runat="server" Text=""></asp:Label>
                        </td>
                    </tr>
                    <tr >
                        <td >Gender</td>
                        <td >
                            <asp:Label ID="lblGender" runat="server" Text=""></asp:Label>
                            </td>
                    </tr>
                     <tr >
                        <td >Age</td>
                        <td >
                            <asp:Label ID="lblAge" runat="server" Text=""></asp:Label>
                        </td>
                    </tr>
 
                </table>
                    </div>

            </ContentTemplate>
                        <Triggers>
                            <asp:PostBackTrigger ControlID="Button1" />
                            <asp:PostBackTrigger ControlID="imgBtnService" />
                            
                        </Triggers>
                </asp:UpdatePanel>
    </form>

</body>
</html>
