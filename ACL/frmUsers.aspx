<%@ Page Language="C#" AutoEventWireup="true" CodeFile="frmUsers.aspx.cs" Inherits="frmUsers" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
	<title></title>
    <link href="../css/SiteContent.css" rel="stylesheet" />
	
	<script language="javascript" type="text/javascript">
	function validateControls()
	{
		if (document.getElementById("txtName").value.length == 0)
		{
			alert("يرجى تحديد الاسم");
			document.getElementById("txtName").focus();
			return false;
		}
		if (document.getElementById("txtEmail").value.length == 0) {
			alert("يرجى تحديد بريد الإلكتروني");
			document.getElementById("txtEmail").focus();
			return false;
		}
		if (document.getElementById("cmbRole").options[document.getElementById("cmbRole").selectedIndex].value == 0) {
			alert("يرجى إختيار الدور");
			document.getElementById("cmbRole").focus();
			return false;
		}
		if (document.getElementById("cmbRole").options[document.getElementById("cmbRole").selectedIndex].value == 2 || document.getElementById("cmbRole").options[document.getElementById("cmbRole").selectedIndex].value == 3) {
		    if (document.getElementById("drpBranches").options[document.getElementById("drpBranches").selectedIndex].value == 0) {
		        alert("يرجى إختيار الفرع");
		        document.getElementById("drpBranches").focus();
		        return false;
		    }
		}
		if (document.getElementById("hfPassword").value == "") {//INSERT OPERATION
			if (document.getElementById("txtPassword").value.length == 0) {
				alert("Please enter your password.");
				document.getElementById("txtPassword").focus();
				return false;
			}
			if (document.getElementById("txtPassword").value.length < 8) {
				alert("Password length should be atleast minimum 8 characters.");
				document.getElementById("txtPassword").focus();
				return false;
			}
			if (document.getElementById("txtConfirmPassword").value.length == 0) {
				alert("Please confirm your password.");
				document.getElementById("txtConfirmPassword").focus();
				return false;
			}
			if (document.getElementById("txtConfirmPassword").value.length < 8) {
				alert("Password length should be atleast minimum 8 characters.");
				document.getElementById("txtConfirmPassword").focus();
				return false;
			}
			if (document.getElementById("txtPassword").value != document.getElementById("txtConfirmPassword").value) {
				alert("Password mismatch!!! Please try again.");
				document.getElementById("txtConfirmPassword").focus();
				return false;
			}

		}
		else {//UPDATE OPERATION
			if (document.getElementById("txtPassword").value.length != 0) {
				if (document.getElementById("txtPassword").value.length < 8) {
					alert("Password length should be atleast minimum 8 characters.");
					document.getElementById("txtPassword").focus();
					return false;
				}

				if (document.getElementById("txtConfirmPassword").value.length == 0) {
					alert("يرجى تحديد تأكيد كلمة المرور");
					document.getElementById("txtConfirmPassword").focus();
					return false;
				}
				if (document.getElementById("txtConfirmPassword").value.length == 0) {
					alert("Please confirm your password.");
					document.getElementById("txtConfirmPassword").focus();
					return false;
				}
				if (document.getElementById("txtConfirmPassword").value.length < 8) {
					alert("Password length should be atleast minimum 8 characters.");
					document.getElementById("txtConfirmPassword").focus();
					return false;
				}
				if (document.getElementById("txtPassword").value != document.getElementById("txtConfirmPassword").value) {
					alert("Password mismatch!!! Please try again.");
					document.getElementById("txtConfirmPassword").focus();
					return false;
				}
			}
		}

		
		
		return true;
	}
	
	function hideMessage() {
	    document.getElementById('tblMessage').style.display = 'none';
	    return true;
	}
    </script>

     <style>
    .message 
    {
    border: 1px solid #ADA;
    background: #EFE;
    }

    /*.modal
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
            }*/



    </style>

</head>
<body dir="rtl">
	<form id="form1" runat="server">
         
             <asp:ScriptManager ID="ScriptManager2" runat="server">
    </asp:ScriptManager>
    <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="UpdatePanel1"
        ClientIDMode="Predictable" ViewStateMode="Inherit">
        <ProgressTemplate>
    <div class="modal"  >
        <div class="center">
            <img alt="" src="loader.gif" />
        </div>
    </div>
        </ProgressTemplate>
    </asp:UpdateProgress>

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
                                          <asp:Label ID="Label2" runat="server" Text="ادارة المستخدمين" 
                            meta:resourcekey="lblHeadingResource1" CssClass="lblBox"></asp:Label>
                        </td>
                        <td align="left">


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
            <td  colspan="4" align="center">


            </td>

    </tr>
             <tr>
                 <td>
                     <asp:Label ID="Label1" runat="server" CssClass="lblBox" Text="الاسم : "></asp:Label>
                 </td>
                 <td>
                     <asp:TextBox ID="txtName" runat="server" CssClass="txtBox" Width="350px"></asp:TextBox>
                 </td>
                 <td>
                     <asp:Label ID="lblCampaignName10" runat="server" CssClass="lblBox" meta:resourcekey="lblEmailResource1" Text="إسم  المستخدم : "></asp:Label>
                 </td>
                 <td>
                     <asp:TextBox ID="txtUserName" runat="server" CssClass="txtBox" Width="350px"></asp:TextBox>
                 </td>
             </tr>
             <tr>
                 <td>
                     <asp:Label ID="lblCampaignName6" runat="server" CssClass="lblBox" Height="22px" meta:resourcekey="lblMobileResource1" Text="البريد الإلكتروني : "></asp:Label>
                 </td>
                 <td>
                     <asp:TextBox ID="txtMobileNumber" runat="server" CssClass="txtBox" Width="350px"></asp:TextBox>
                 </td>
                 <td>
                     <asp:Label ID="lblDepartment5" runat="server" CssClass="lblBox" meta:resourcekey="lblDepartmentResource1" Text="نوع المستخدم : "></asp:Label>
                 </td>
                 <td>
                     <asp:DropDownList ID="drpRoles" runat="server" CssClass="drpList">
                         <asp:ListItem meta:resourcekey="lblListItemResourceGender" Value="2">--الدور --</asp:ListItem>
                         <asp:ListItem meta:resourcekey="lblListItemResourceMale" Value="1">Admin</asp:ListItem>
                         <asp:ListItem meta:resourcekey="lblListItemResourceFemale" Value="2">Data Entry Operator</asp:ListItem>
                     </asp:DropDownList>
                 </td>
             </tr>
             <tr>
                 <td >
                     <asp:Label ID="lblDepartment3" runat="server" CssClass="lblBox" meta:resourcekey="lblDepartmentResource1" Text="كلمة المرور : "></asp:Label>
                 </td>
                 <td >
                     <asp:TextBox ID="txtPassword" runat="server" CssClass="txtBox" TextMode="Password" Width="350px"></asp:TextBox>
                 </td>
                 <td >
                     <asp:Label ID="lblDepartment4" runat="server" CssClass="lblBox" meta:resourcekey="lblDepartmentResource1" Text="تأكيد كلمة المرور : "></asp:Label>
                 </td>
                 <td >
                     <asp:TextBox ID="txtConfirmPassword" runat="server" CssClass="txtBox" TextMode="Password" Width="350px"></asp:TextBox>
                 </td>
             </tr>
             <tr>
                 <td>
                     <asp:Label ID="Label4" runat="server" CssClass="lblBox" Text="فعال  "></asp:Label>
                 </td>
                 <td>
                     <asp:RadioButtonList ID="radActive" runat="server" RepeatDirection="Horizontal">
                         <asp:ListItem Value="1">نعم</asp:ListItem>
                         <asp:ListItem Value="0">لا</asp:ListItem>
                     </asp:RadioButtonList>
                 </td>
                 <td>
                     &nbsp;</td>
                 <td>
                     &nbsp;</td>
             </tr>
             <tr>
                 <td>
                     &nbsp;</td>
                 <td>
                     &nbsp;</td>
                 <td>
                     &nbsp;</td>
                 <td>
                     <asp:HiddenField ID="hfPassword" runat="server" />
                 </td>
             </tr>
    <tr>
            <td colspan="4" align="left">
          					<table cellpadding="2" cellspacing="0" border="0">
						<tr>
							<td>
													<asp:Button ID="btnSubmit" runat="server" onclick="btnSubmit_Click" 
						Text="حفظ" CssClass="submit" />
							</td>

													  <td>
																					<asp:Button ID="btnCLear" runat="server" 
						Text="مسح" CssClass="submit" OnClick="btnCLear_Click"  />
							</td>
														<td>
																					<asp:Button ID="btnViewUsers" runat="server" 
						Text="عرض المستخدمين" CssClass="submit" OnClick="btnViewUsers_Click" />
							</td>
						</tr>
					</table>
</td>

    </tr>

        </table>








</td>
            </tr>
                      </table>

                      </ContentTemplate>

                      </asp:UpdatePanel>
      

	</form>
</body>
</html>
