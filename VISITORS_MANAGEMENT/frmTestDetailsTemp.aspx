<%@ Page Language="C#" AutoEventWireup="true" CodeFile="frmTestDetailsTemp.aspx.cs" Inherits="frmTestDetailsTemp" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="../css/SiteContent.css" rel="stylesheet" />
    <script src="../js/base64.js" type="text/javascript"></script>
    <script src="../js/errors.js" type="text/javascript"></script>
    <script src="../js/zfcomponent.js" type="text/javascript"></script>
    <%--    <script src="../js/jquery-ui-1.9.0.js" type="text/javascript"></script>--%>
    <script src="http://code.jquery.com/jquery-1.7.1.min.js"></script>



    <script language="javascript" type="text/javascript">


            function hideMessage() {
                document.getElementById('tblMessage').style.display = 'none';
                return true;
            }
            function validateControls() {
                if (document.getElementById("txtFullNameEn").value.length == 0) {
                    alert("Please enter fullname in english.");
                    document.getElementById("txtFullNameEn").focus();
                    return false;
                }
                if (document.getElementById("cmbDepartment").options[document.getElementById("cmbDepartment").selectedIndex].value == -1 && document.getElementById("drpEvents").options[document.getElementById("drpEvents").selectedIndex].value == -1) {
                    alert("Please select department or events.");
                    document.getElementById("cmbDepartment").focus();
                    return false;
                }

                if (document.getElementById("txtVisitorCardNumber").value.length == 0) {
                    alert("Please enter visitor card number...");
                    document.getElementById("txtVisitorCardNumber").focus();
                    return false;
                }
                return true;

 
            }
            function doReadPublicData() {
                var Ret = Initialize();
                if (Ret == false) {
                    document.getElementById("hfEidCardExist").value = "0";
                    return false;
                }
                if (Ret == true) {
                    Ret = ReadPublicData(true, true, true, true, true, true);
                    $("#ef_idn_cn").val(GetEF_IDN_CN());
                    $("#ef_non_mod_data").val(GetEF_NonModifiableData());
                    $("#ef_mod_data").val(GetEF_ModifiableData());
                    $("#ef_sign_image").val(GetEF_HolderSignatureImage());
                    $("#ef_photo").val(GetEF_Photography());
                    $("#ef_root_cert").val(GetEF_RootCertificate());
                    $("#ef_home_address").val(GetEF_HomeAddressData());
                    $("#ef_work_address").val(GetEF_WorkAddressData());
                    return true;
                }
                else {
                    document.getElementById("hfEidCardExist").value = "0";
                    return false;
                }
            }
            function doSignData() {
                var Ret = Initialize();
                if (Ret == false)
                    return;

                var pin = $("#txtPin").val();
                var data = $("#txtSignData").val();

                if (pin == null || pin == "" || pin.length != 4) {
                    alert("Empty or invalid PIN size!");
                    return;
                }

                if (data == null || data == "") {
                    alert("Data field is empty!");
                    return;
                }

                data = window.btoa(data);

                Ret = SignData(pin, data);
                if (Ret == "")
                    return;

                $("#certificate").val(ExportSignCertificate());
                $("#signature").val(Ret);
                $("#btnVerifySignature").removeAttr("disabled");

                $("#btnVerifyPubData").removeAttr("disabled");
                $("#msg p:last").html("Data Signed successfully");
                $("#msg").show("fade", {}, 500);
                ChangeUrl("frm", "frm");
            }
            function ChangeUrl(title, url) {
                if (typeof (history.pushState) != "undefined") {
                    var obj = { Title: title, Url: url };
                    history.pushState(obj, obj.Title, obj.Url);
                } else {
                    alert("Browser does not support HTML5.");
                }
            }
            function doSignChallenge() {
                var Ret = Initialize();
                if (Ret == false)
                    return;

                var pin = $("#txtPin").val();
                if (pin == null || pin == "" || pin.length != 4) {
                    alert("Empty or invalid PIN size!");
                    return;
                }


                var challenge = "";
                $.ajax({
                    url: "GenerateChallenge.aspx",
                    type: "POST",
                    async: false,
                    dataType: 'text',
                    context: document.body,
                    success: function (data) {
                        challenge = data;
                    }
                });

                Ret = SignChallenge(pin, challenge);
                if (Ret == "")
                    return;

                $("#certificate").val(ExportAuthCertificate());
                $("#signature").val(Ret);
                $("#btnVerifySignature").removeAttr("disabled");

                $("#msg p:last").html("Server Challenge Signed successfully");
                $("#msg").show("fade", {}, 500);
            }
            function verifySignature() {
                $("#form1").attr("action", "VerifySignature");
                $('#form1').submit();
            }
            function onSignChange(rd) {
                if (rd.value == "Auth")
                    $("#TR_Data").hide();
                else
                    $("#TR_Data").show();
            }
            function doSign() {
                if ($("#rdSign").is(':checked'))
                    doSignData();
                else
                    doSignChallenge();
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
<body dir="rtl" >
    <form id="form1" runat="server">
  <script src="../JQUERY_AUTO_COMPLETE/jquery-1.10.0.min.js" type="text/javascript"></script>
        <link href="../JQUERY_AUTO_COMPLETE/jquery-ui.css" rel="stylesheet" />
     
        <script src="../JQUERY_AUTO_COMPLETE/jquery-ui.min.js" type="text/javascript"></script>
 <script type="text/javascript">
        $(function () {
            $("[id$=txtCompanyNameSearch]").autocomplete({
                source: function (request, response) {
                    $.ajax({
                        url: '<%=ResolveUrl("frmReadVisitorInfo.aspx/GetCustomers") %>',
                        data: "{ 'prefix': '" + request.term + "'}",
                        dataType: "json",
                        type: "POST",
                        contentType: "application/json; charset=utf-8",
                        success: function (data) {
                            response($.map(data.d, function (item) {
                                return {
                                    label: item.split('-')[0],
                                    val: item.split('-')[1]
                                }
                            }))
                        },
                        error: function (response) {
                            alert(response.responseText);
                        },
                        failure: function (response) {
                            alert(response.responseText);
                        }
                    });
                },
                select: function (e, i) {
                    $("[id$=hfCustomerId]").val(i.item.val);
                },
                minLength: 1
            });
        });   
    </script>

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

    <%--<asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="UpdatePanel1"
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
                                          <asp:Label ID="Label2" runat="server" Text="بيانات المتعامل " 
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
            <td  colspan="7" align="left">


                


                </td>

    </tr>
             <tr>
                 <td>
                     <asp:Label ID="lblMaritalStatus1" runat="server" Text="رقم الهوية : "></asp:Label>
                 </td>
                 <td>
                     <asp:TextBox ID="idn" runat="server" CssClass="txtBox" Width="300px"></asp:TextBox></td>
                 <td>
                     &nbsp;</td>
                 <td>
                     <asp:Label ID="lblNameAr1" runat="server" Text="الاسم بالعربية  : "></asp:Label>
                 </td>
                 <td>
                     <asp:TextBox ID="cardHolderNameAr" runat="server" CssClass="txtBox" Width="300px"></asp:TextBox>
                 </td>
                 <td rowspan="8">
            
                 </td>
                 <td rowspan="8">
                     <table cellpadding="2" cellspacing="0" border="0">
                         <tr>
                             <td>
الصورة الشخصية
                             </td>
                         </tr>
                         <tr>
                             <td>
                                 <img alt="Photo" id="PhotoBase64" runat="server" src="~/Images/avtar.png"></img>
                             </td>
                         </tr>
                         <tr>
                             <td>
                                 &nbsp;</td>
                         </tr>
                     </table>
                     </td>
             </tr>
             <tr>
                 <td class="auto-style1">
                     <asp:Label ID="lblNameEn" runat="server" Text="الاسم بالانجلزية : "></asp:Label>
                 </td>
                 <td class="auto-style1">
                     <asp:TextBox ID="cardHolderNameEn" runat="server" CssClass="txtBox" Width="300px"></asp:TextBox>

                 </td>
                 <td class="auto-style1">
                     </td>
                 <td class="auto-style1">
                     <asp:Label ID="lblSex1" runat="server" Text="الجنس :"></asp:Label>
                 </td>
                 <td class="auto-style1">
                     <asp:RadioButtonList ID="radGender" runat="server" RepeatDirection="Horizontal">
                         <asp:ListItem Value="1">ذكر</asp:ListItem>
                         <asp:ListItem Value="2">أنثى</asp:ListItem>
                     </asp:RadioButtonList>
                 </td>
             </tr>
             <tr>
                 <td>
                     &nbsp;</td>
                 <td>
                     &nbsp;</td>
                 <td>
                     &nbsp;</td>
                 <td>
                     &nbsp;</td>
                 <td>
        
                     <asp:DropDownList ID="drpNationality" runat="server" CssClass="drpList" Width="304px">
                     </asp:DropDownList>
        
                 </td>
             </tr>
                                      <tr>
                                          <td>
                                              <asp:Label ID="lblNameEn0" runat="server" Text="تاريخ الميلاد : "></asp:Label>
                                          </td>
                                          <td>
                                              <asp:TextBox ID="txtDob" runat="server" CssClass="txtBox" Width="300px"></asp:TextBox>
                                          </td>
                                          <td>&nbsp;</td>
                                          <td>
                                              <asp:Label ID="lblNationality2" runat="server" Text="الجنسية :"></asp:Label>
                                          </td>
                                          <td><asp:TextBox ID="txtNationality" runat="server" CssClass="txtBox" Width="300px"></asp:TextBox></td>
                                      </tr>
                                      <tr>
                                          <td>
                                              <asp:Label ID="lblNameAr3" runat="server" Text="رقم جواز السفر : "></asp:Label>
                                          </td>
                                          <td>
                                              <asp:TextBox ID="txtPassportNumber" runat="server" CssClass="txtBox" Width="300px"></asp:TextBox>
                                          </td>
                                          <td>&nbsp;</td>
                                          <td>
                                              <asp:Label ID="lblStreetAr6" runat="server" Text="رقم الهاتف : "></asp:Label>
                                          </td>
                                          <td>
                                              <asp:TextBox ID="visitor_mobile" runat="server" CssClass="txtBox" Width="300px"></asp:TextBox>
                                          </td>
                                      </tr>
                                      <tr>
                                          <td>
                                              <asp:Label ID="lblMaritalStatus22" runat="server" Text="عنوان البريد الإلكتروني : "></asp:Label>
                                          </td>
                                          <td>
                                              <asp:TextBox ID="txtEmail" runat="server" CssClass="txtBox" Width="300px"></asp:TextBox>
                                          </td>
                                          <td>&nbsp;</td>
                                          <td>
                                              <asp:Label ID="lblMaritalStatus25" runat="server" Text="الفئة : "></asp:Label>
                                          </td>
                                          <td>
                                              <asp:DropDownList ID="drpCategory" runat="server" CssClass="drpList" Width="304px">
                                              </asp:DropDownList>
                                          </td>
                                      </tr>
             <tr>
                 <td>
        
                     <asp:Label ID="lblStreetAr9" runat="server" Text="Job Info :"></asp:Label>
                 </td>
                 <td>
                        <asp:TextBox ID="txtJobInfo" runat="server" CssClass="txtBox" Width="300px"></asp:TextBox>
              
                 </td>
                 <td>
                     &nbsp;</td>
                 <td>
        
                     <asp:Label ID="lblMaritalStatus26" runat="server" Text="عنوان : "></asp:Label>
                 </td>
                 <td>
                     <asp:TextBox ID="txtAddress" runat="server" CssClass="txtBox" Width="300px"></asp:TextBox>
                 </td>
             </tr>
                                      <tr>
                                          <td>
                                              <asp:Label ID="Label3" runat="server" Text="الكتاب الصادر من الجهة  : "></asp:Label>
                                          </td>
                                          <td>
                                              <asp:FileUpload ID="fuLetter" runat="server" />
                                          </td>
                                          <td>&nbsp;</td>
                                          <td>&nbsp;</td>
                                          <td>&nbsp;</td>
                                      </tr>
             <tr>
                 <td>
                     &nbsp;</td>
                 <td>
        
                                        &nbsp;</td>
                 <td>
                     &nbsp;</td>
                 <td>
                     &nbsp;</td>
                 <td>
                     &nbsp;</td>
             </tr>
             <tr>
                 <td colspan="7">
                 <table cellpadding="2" cellspacing="0" border="0" width="100%">
                     <tr>
                         <td>
                             <asp:Label ID="Label19" runat="server" CssClass="lblBox" Font-Bold="True" Font-Size="20px" Text="اختبار طبي - 1"></asp:Label>
                         </td>
                         <td>
                             <asp:Label ID="Label20" runat="server" CssClass="lblBox" Font-Bold="True" Font-Size="20px" Text="اختبار طبي - 2"></asp:Label>
                         </td>
                         <td>
                             <asp:Label ID="Label21" runat="server" CssClass="lblBox" Font-Bold="True" Font-Size="20px" Text="اختبار طبي - 3"></asp:Label>
                         </td>
                         <td>
                             <asp:Label ID="Label22" runat="server" CssClass="lblBox" Font-Bold="True" Font-Size="20px" Text="اختبار طبي - 4"></asp:Label>
                         </td>
                     </tr>
                     <tr>
                         <td>
                             <asp:CheckBox ID="chkTestOneEye" runat="server" AutoPostBack="True" Font-Size="20px" OnCheckedChanged="chkTestOneEye_CheckedChanged" Text="فحص العين" />
                         </td>
                         <td>
                             <asp:CheckBox ID="chkTestTwoBlood" runat="server" AutoPostBack="True" Font-Size="20px" OnCheckedChanged="chkTestTwoBlood_CheckedChanged" Text="فحص الدم" />
                         </td>
                         <td>
                             <asp:CheckBox ID="chkTestThreeXray" runat="server" AutoPostBack="True" Font-Size="20px" OnCheckedChanged="chkTestThreeXray_CheckedChanged" Text="الاشعة" />
                         </td>
                         <td>
                             <asp:CheckBox ID="chkTestFourEye" runat="server" AutoPostBack="True" Font-Size="20px" OnCheckedChanged="chkTestFourEye_CheckedChanged" Text="اخصائي العيون" />
                         </td>
                     </tr>
                     <tr>
                         <td>
                             <asp:CheckBox ID="chkTestOneHeight" runat="server" AutoPostBack="True" Font-Size="20px" OnCheckedChanged="chkTestOneHeight_CheckedChanged" Text="الطول" />
                         </td>
                         <td>
                             <asp:CheckBox ID="chkTestTwoUrine" runat="server" AutoPostBack="True" Font-Size="20px" OnCheckedChanged="chkTestTwoUrine_CheckedChanged" Text="فحص البول" />
                         </td>
                         <td></td>
                         <td>
                             <asp:CheckBox ID="chkTestFourBone" runat="server" AutoPostBack="True" Font-Size="20px" OnCheckedChanged="chkTestFourBone_CheckedChanged" Text="اخصائي الجلدية" />
                         </td>
                     </tr>
                     <tr>
                         <td>
                             <asp:CheckBox ID="chkTestOneWeight" runat="server" AutoPostBack="True" Font-Size="20px" OnCheckedChanged="chkTestOneWeight_CheckedChanged" Text="الوزن" />
                         </td>
                         <td>&nbsp;</td>
                         <td>&nbsp;</td>
                         <td>
                             <asp:CheckBox ID="chkTestFourSkin" runat="server" AutoPostBack="True" Font-Size="20px" OnCheckedChanged="chkTestFourSkin_CheckedChanged" Text="اخصائية العظام" />
                         </td>
                     </tr>
                     <tr>
                         <td>&nbsp;</td>
                         <td>&nbsp;</td>
                         <td>&nbsp;</td>
                         <td>
                             <asp:CheckBox ID="chkTestFourPsych" runat="server" AutoPostBack="True" Font-Size="20px" OnCheckedChanged="chkTestFourPsych_CheckedChanged" Text="اخصائي النفسية" />
                         </td>
                     </tr>
                     <tr>
                         <td>&nbsp;</td>
                         <td>&nbsp;</td>
                         <td>&nbsp;</td>
                         <td>
                             <asp:CheckBox ID="chkTestFourSurgery" runat="server" AutoPostBack="True" Font-Size="20px" OnCheckedChanged="chkTestFourSurgery_CheckedChanged" Text="اخصائي الجراحة" />
                         </td>
                     </tr>
                     <tr>
                         <td>&nbsp;</td>
                         <td>&nbsp;</td>
                         <td>&nbsp;</td>
                         <td>
                             <asp:CheckBox ID="chkTestFourGp" runat="server" AutoPostBack="True" Font-Size="20px" OnCheckedChanged="chkTestFourGp_CheckedChanged" Text="الممارس العام" />
                         </td>
                     </tr>
                     <tr>
                         <td colspan="4">                     <table cellpadding="2" cellspacing="0" border="0">
                                             <tr>
                                                 <td class="auto-style1"><asp:Label ID="Label18" runat="server" Text="النتائج : " CssClass="lblBox" Font-Bold="False" Font-Size="20px"></asp:Label></td>
                                                 <td class="auto-style1">
                                                     <asp:RadioButtonList ID="radFitnessSelection" runat="server" AutoPostBack="True" Font-Size="20px" OnSelectedIndexChanged="radFitnessSelection_SelectedIndexChanged" RepeatDirection="Horizontal">
                                                         <asp:ListItem Value="1">لائق </asp:ListItem>
                                                         <asp:ListItem Value="0">غير لائق</asp:ListItem>
                                                     </asp:RadioButtonList>
                                                 </td>
                                             </tr>
                                         </table></td>
                     </tr>
                 </table>
                 </td>
             </tr>
                                      </table>

                      </td>
                  </tr>

        </table>
            <br />
                <asp:HiddenField ID="hfEidCardExist" runat="server" />
            </ContentTemplate>
                </asp:UpdatePanel>
    </form>
</body>
</html>
