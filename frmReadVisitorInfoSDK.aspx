<%@ Page Language="C#" AutoEventWireup="true" CodeFile="frmReadVisitorInfoSDK.aspx.cs" Inherits="frmReadVisitorInfoSDK" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>



<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">



<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="../css/SiteContent.css" rel="stylesheet" />
        <script src="../JS_SDK/eidatoolkit.js"></script>
    <script src="../JS_SDK/toolkit_sample.js"></script>

    
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
                    <table cellpadding="0" cellspacing="0" align="center" bgcolor="White"  style="border: 1px solid #D0C4B4; margin-top: 10px;" width="99%">
                  <tr>
        <td valign="middle" class="trHeading"
                
                
                style="font-weight: bold; padding-right: 20px; padding-left: 20px;" 
                height="30px"> 
                    
                            <table cellpadding="2" cellspacing="0" border="0" width="100%">
                    <tr>
                        <td>
                                          <asp:Label ID="Label1" runat="server" Text="إدارة الزوار " 
                            meta:resourcekey="lblHeadingResource1" CssClass="lblBox"></asp:Label>
                        </td>
                        <td align="left">

                                                    <table cellpadding="2" cellspacing="0" border="0" style="visibility: visible">
                            <tr>
                            <td valign="middle">
                                                       <button onclick="Initialize()" id="Button1" >
                                Initialize</button>
         
                                    
                            </td>
                            <td>
                            <textarea rows="1" cols="50" id="prgssText" disabled></textarea>
                            </td>
                                                        <td>
                                   <button class="buttonInitial" id="publicDataBtn"   onclick="DisplayPublicData()" >
                                Read Emirates ID Info</button>    
                            </td>
                                <td>
                                                                    <button class="buttonInitial" id="disconnectBtn" disabled onclick="disconnectWS()">
                                Disconnect
                            </button>
                                </td>
                            </tr>
                        </table>
                        </td>
                    </tr>
                </table>

                </td>
 
        </tr>
                        <tr>
                            <td>
                                
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
                  <table cellpadding="0" cellspacing="0" align="center" bgcolor="White"  style="border: 0px solid #D0C4B4; margin-top: 10px;" width="100%">
                  
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
            <td  colspan="8" align="center">


                بيانات الزيارة</td>

    </tr>
             <tr>
                 <td>
        <asp:Label ID="lblNameAr" runat="server" Text="الاسم بالعربية  : "></asp:Label>
                 </td>
                 <td>
            <asp:TextBox ID="cardHolderNameAr" runat="server" CssClass="txtBox" 
                Width="300px"></asp:TextBox>
                 </td>
                 <td>
                     &nbsp;</td>
                 <td>
        <asp:Label ID="lblNameEn" runat="server" Text="الاسم بالانجلزية : "></asp:Label>
                 </td>
                 <td>
            <asp:TextBox ID="cardHolderNameEn" runat="server" CssClass="txtBox" 
                Width="300px"></asp:TextBox>
                 </td>
                 <td rowspan="4">
            
                 الصورة الشخصية</td>
                 <td rowspan="4">
                     <img alt="Photo" id="PhotoBase64" runat="server" src="~/Images/avtar.png"></img></td>
                 <td>
                     &nbsp;</td>
             </tr>
             <tr>
                 <td>
        <asp:Label ID="lblNameAr0" runat="server" Text="اسم الشركة :  "></asp:Label>
                 </td>
                 <td>
                 <asp:TextBox ID="txtCompanyNameSearch" runat="server" CssClass="txtBox" Width="300px"></asp:TextBox>

                 </td>
                 <td>
                     &nbsp;</td>
                 <td>
                <asp:Label ID="lblMaritalStatus1" runat="server" Text="رقم الهوية : "></asp:Label>
                 </td>
                 <td>
        <asp:TextBox ID="idn" runat="server" CssClass="txtBox" Width="300px"></asp:TextBox>
                 </td>
                 <td>
                     &nbsp;</td>
             </tr>
             <tr>
                 <td>
        
        <asp:Label ID="lblStreetAr6" runat="server" Text="رقم الهاتف : "></asp:Label>
                 </td>
                 <td>
                        <asp:TextBox ID="visitor_mobile" runat="server" CssClass="txtBox" 
                Width="300px"></asp:TextBox>
              
                 </td>
                 <td>
                     &nbsp;</td>
                 <td>
        
                <asp:Label ID="lblMaritalStatus15" runat="server" Text="رقم بطاقة الزائر"></asp:Label>
                 </td>
                 <td>
                <asp:TextBox ID="VisitorCardNumber" runat="server" CssClass="txtBox" Text ="0"
                Width="300px"></asp:TextBox>
                 </td>
                 <td>
                     &nbsp;</td>
             </tr>
             <tr>
                 <td>
        
        <asp:Label ID="lblNationality0" runat="server" Text="الجنسية :"></asp:Label>
                 </td>
                 <td>
                <asp:DropDownList ID="drpNationality" runat="server" CssClass="drpList" 
                    Width="304px">
                </asp:DropDownList>
              
                 </td>
                 <td>
                     &nbsp;</td>
                 <td>
        <asp:Label ID="lblSex1" runat="server" Text="الجنس :"></asp:Label>
                 </td>
                 <td>
                     <asp:RadioButtonList ID="radGender" runat="server" RepeatDirection="Horizontal">
                         <asp:ListItem Value="1">ذكر</asp:ListItem>
                         <asp:ListItem Value="2">أنثى</asp:ListItem>
                     </asp:RadioButtonList>
                 </td>
                 <td>
                     &nbsp;</td>
             </tr>
             <tr>
                 <td>
                     <asp:Label ID="lblMaritalStatus21" runat="server" Text="عنوان البريد الإلكتروني : "></asp:Label>
                 </td>
                 <td>
                     <asp:TextBox ID="txtEmail" runat="server" CssClass="txtBox" Width="300px"></asp:TextBox>
                 </td>
                 <td>
                     &nbsp;</td>
                 <td>
        <asp:Label ID="lblSex0" runat="server" Text="تاريخ الميلاد :"></asp:Label>
                 </td>
                 <td>
            <asp:TextBox ID="txtDob" runat="server" CssClass="txtBox" Width="300px"></asp:TextBox>
                 </td>
                 <td rowspan="3">
            التوقيع</td>
                 <td rowspan="3">
                     <img id="imgSignature" runat="server" alt="Photo" src="Images/signature.jpg" /></td>
                 <td>
                     &nbsp;</td>
             </tr>
             <tr>
                 <td colspan="5" align="center">
                بيانات الشخص المزار
</td>
                 <td></td>
             </tr>
             <tr>
                 <td>
        
                <asp:Label ID="lblCampaignName10" runat="server" CssClass="lblBox" meta:resourcekey="lblMobileResource1" Text="القطاع : "></asp:Label>
                 </td>
                 <td>
            <asp:DropDownList ID="drpSector" runat="server" AutoPostBack="True" CssClass="drpList" OnSelectedIndexChanged="drpSector_SelectedIndexChanged" Width="304px">
                <asp:ListItem></asp:ListItem>
                <asp:ListItem Value="1">System Admin</asp:ListItem>
                <asp:ListItem Value="2">Branch Admin</asp:ListItem>
                <asp:ListItem Value="2">Branch User</asp:ListItem>
            </asp:DropDownList>
                 </td>
                 <td>
                     &nbsp;</td>
                 <td>
        
                <asp:Label ID="lblMaritalStatus14" runat="server" Text="الإدارة : "></asp:Label>
        
                 </td>
                 <td>
                <asp:DropDownList ID="drpDepartment" runat="server" CssClass="drpList" 
                    Width="304px" AutoPostBack="True" OnSelectedIndexChanged="drpDepartment_SelectedIndexChanged">
                </asp:DropDownList>
                 </td>
                 <td>
                     &nbsp;</td>
             </tr>
             <tr>
                 <td>
        
                <asp:Label ID="lblCampaignName11" runat="server" CssClass="lblBox" meta:resourcekey="lblMobileResource1" Text="القسم : "></asp:Label>
                 </td>
                 <td>
            <asp:DropDownList ID="drpSection" runat="server" AutoPostBack="True" CssClass="drpList" OnSelectedIndexChanged="drpSection_SelectedIndexChanged" Width="304px" style="height: 29px">
                <asp:ListItem></asp:ListItem>
                <asp:ListItem Value="1">System Admin</asp:ListItem>
                <asp:ListItem Value="2">Branch Admin</asp:ListItem>
                <asp:ListItem Value="2">Branch User</asp:ListItem>
            </asp:DropDownList>
                 </td>
                 <td>
                     &nbsp;</td>
                 <td>
        
                <asp:Label ID="lblMaritalStatus17" runat="server" Text="الفرع : "></asp:Label>
        
                 </td>
                 <td>
                <asp:DropDownList ID="drpUnits" runat="server" CssClass="drpList" 
                    Width="304px" AutoPostBack="True" OnSelectedIndexChanged="drpUnits_SelectedIndexChanged">
                </asp:DropDownList>
                 </td>
                 <td>
                     &nbsp;</td>
                 <td>
                     &nbsp;</td>
                 <td>
                     &nbsp;</td>
             </tr>
             <tr>
                 <td>
        
                <asp:Label ID="lblCampaignName12" runat="server" CssClass="lblBox" meta:resourcekey="lblMobileResource1" Text="الشخص المزار : "></asp:Label>
                 </td>
                 <td>
            <asp:DropDownList ID="drpUsers" runat="server" CssClass="drpList" Width="304px" AutoPostBack="True" OnSelectedIndexChanged="drpUsers_SelectedIndexChanged">
                <asp:ListItem></asp:ListItem>
                <asp:ListItem Value="1">System Admin</asp:ListItem>
                <asp:ListItem Value="2">Branch Admin</asp:ListItem>
                <asp:ListItem Value="2">Branch User</asp:ListItem>
            </asp:DropDownList>
                 </td>
                 <td>
                     &nbsp;</td>
                 <td>
        
                <asp:Label ID="lblMaritalStatus19" runat="server" Text="مكتن الزيارة : "></asp:Label>
        
                 </td>
                 <td>
            <asp:DropDownList ID="drpVisitingPlace" runat="server" CssClass="drpList" Width="304px" OnSelectedIndexChanged="drpVisitingPlace_SelectedIndexChanged">
                <asp:ListItem></asp:ListItem>
                <asp:ListItem Value="1">System Admin</asp:ListItem>
                <asp:ListItem Value="2">Branch Admin</asp:ListItem>
                <asp:ListItem Value="2">Branch User</asp:ListItem>
            </asp:DropDownList>
                 </td>
                 <td>
                     &nbsp;</td>
                 <td>
                     &nbsp;</td>
                 <td>
                     &nbsp;</td>
             </tr>
             <tr>
                 <td>
        
                <asp:Label ID="lblMaritalStatus18" runat="server" Text="   وقت وتاريخ الدخول  : "></asp:Label>
        
                 </td>
                 <td>
            <asp:TextBox ID="txtCheckInTime" runat="server" CssClass="txtBox" Width="300px" ></asp:TextBox>
                     <asp:CalendarExtender ID="ceCheckInTime" runat="server" TargetControlID="txtCheckInTime" Format="dd/MM/yyyy" ></asp:CalendarExtender>
                 </td>
                 <td>
                     &nbsp;</td>
                 <td>
        
                <asp:Label ID="lblCampaignName13" runat="server" CssClass="lblBox" meta:resourcekey="lblMobileResource1" Text="   وقت وتاريخ الخروج : "></asp:Label>
        
                 </td>
                 <td>
            <asp:TextBox ID="txtCheckOutTime" runat="server" CssClass="txtBox" Width="300px" 
                ></asp:TextBox>
                 </td>
                 <td>
                     &nbsp;</td>
                 <td>
                     &nbsp;</td>
                 <td>
                     &nbsp;</td>
             </tr>
             <tr>
                 <td>
        
                <asp:Label ID="Label18" runat="server" Text="  رقم الهاتف الجوال  : "></asp:Label>
                 </td>
                 <td>
            <asp:TextBox ID="txtMeetingPersomMob" runat="server" CssClass="txtBox" Width="300px" 
                ></asp:TextBox>
                 </td>
                 <td>
                     &nbsp;</td>
                 <td>
                    <asp:Label ID="Label19" runat="server" Text=" رقم الهاتف الارضى : "></asp:Label>
                 </td>
                 <td>
            <asp:TextBox ID="txtBaseMobNo" runat="server" CssClass="txtBox" Width="300px" 
                ></asp:TextBox>
                 </td>
                 <td>
                     &nbsp;</td>
                 <td>
                     &nbsp;</td>
                 <td>
                     &nbsp;</td>
             </tr>
             <tr>
                 <td>
        
                <asp:Label ID="lblMaritalStatus16" runat="server" Text="سبب الزيارة : " 
                    ToolTip="Purpose of the visit"></asp:Label>
        
                 </td>
                 <td colspan="4">
            <asp:TextBox ID="txtReason" runat="server" CssClass="txtBox" 
                Width="99%"  Height="50px" 
                TextMode="MultiLine"></asp:TextBox>
                 </td>
                 <td>
                     &nbsp;</td>
                 <td>
                     &nbsp;</td>
                 <td>
                     &nbsp;</td>
             </tr>
             <tr>
                 <td colspan="5">
                        <table cellpadding="2" cellspacing="0" border="0">
                    <tr>
                        <td><asp:Button ID="btnSave" runat="server" CssClass="md-btn-primary" Text="حفظ البيانات" OnClick="btnSave_Click" />
                        </td>
                        <td>
                        <asp:Button ID="btnRegister" runat="server" CssClass="md-btn-danger" Text="تسجيل دخول" OnClick="btnRegister_Click" />
                        </td>
                        <td>
                            <asp:ImageButton ID="ImageButton3" runat="server" OnClientClick="doReadPublicData();" 
                  PostBackUrl="~/VISITORS_MANAGEMENT/frmReadVisitorInfo.aspx" onclick="imgBtnReadPublicData_Click" 
                  BackColor="Transparent" BorderColor="Transparent"  Height="30px" 
                                                      ImageUrl="~/Images/reader.png" Width="30px" 
                                                      ToolTip="Read data from Emirates Id Card..."></asp:ImageButton>
                        </td>
  
                    </tr>
                </table></td>
                 <td>
                     &nbsp;</td>
                 <td>
                     &nbsp;</td>
                 <td>
                     &nbsp;</td>
             </tr>
             <tr>
                 <td>
                     &nbsp;</td>
                 <td>
        
                                        <br />
                <asp:DropDownList ID="drpCompanyName" runat="server" CssClass="drpList" Visible ="false" 
                    Width="304px">
                </asp:DropDownList></td>
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
                 <td>
                     &nbsp;</td>
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
                            </td>
                        </tr>
                                </table>
    



</body>
</html>
