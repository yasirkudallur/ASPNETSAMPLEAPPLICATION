<%@ Page Language="C#" AutoEventWireup="true" CodeFile="frmViewUsers.aspx.cs" Inherits="frmViewUsers" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">

    <link href="../css/SiteContent.css" rel="stylesheet" type="text/css" />
    <title></title>
    <style>
        .breakword
 {
 word-wrap:break-word;
 word-break:break-all;
 }
    </style>
</head>
<body dir="rtl" >
    <form id="form1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
        <br />
        <%--    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>--%>
                   <table cellpadding="0" cellspacing="0" width="99%" align="center" 
                style="border: 1px solid #D0C4B4" bgcolor="white">
        <tr>
        <td valign="middle" colspan="2" class="trHeading" height="30" 
                
                style="font-weight: bold; padding-right: 20px; padding-left: 20px;">
                  عرض المستخدمون</td>
 
        </tr>
                       <tr>
                           <td colspan="2">
                            <table cellpadding="5" cellspacing="0" border="0" align="center" id="tblMainControls" runat="server">
                <tr>
                    <td>
<asp:Label ID="lblProject" runat="server" Text="الاسم : " 
                            CssClass="lblBox"></asp:Label>
                    </td>
                    <td>
                                              <asp:TextBox ID="txtName" runat="server" CssClass="txtBox"  
                                        Width="200px"></asp:TextBox>   
                    </td>
                    <td>
                      <asp:Label ID="lblDepartment" runat="server" CssClass="lblBox" 
                            Text="رقم الهوية : "></asp:Label>
                    </td>
                    <td>
                                              <asp:TextBox ID="txtIdn" runat="server" CssClass="txtBox"  
                                        Width="200px"></asp:TextBox>   
                    
                    </td>
                    <td>
                        <asp:Label ID="lblStatus" runat="server" CssClass="lblBox" Text="رقم التواصل	 : "></asp:Label>
                    </td>
                    <td>
                                              <asp:TextBox ID="txtContactNumber" runat="server" CssClass="txtBox"  
                                        Width="200px"></asp:TextBox>   
                    </td>
                    <td><asp:Label ID="Label1" runat="server" CssClass="lblBox" Text="الدور : "></asp:Label></td>
                    <td><asp:DropDownList ID="drpRoles" runat="server" Width="250px" 
                            CssClass="drpList" AutoPostBack="True" 
                        >
                        </asp:DropDownList></td>
                </tr>
                                <tr>
                                    <td>
                                        &nbsp;</td>
                                    <td>
                                    </td>
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
                                    <td>&nbsp;</td>
                                </tr>
                <tr>
                <td colspan="8" align="center">
                                                     <asp:Label ID="lblMessage" runat="server" Font-Names="Verdana" Font-Size="12px"></asp:Label>
                    </td>
                </tr>
                <tr>
                <td colspan="8">
            <table cellpadding="8" cellspacing="0" border="0" align="center">
                <tr>
                    <td align="center">
                        <asp:Button ID="btnSearch" runat="server" 
                            onclick="btnSearch_Click" Text="بحث" CssClass="submit" 
                            />&nbsp;
                        <asp:Button ID="btnExport" runat="server"  Text="تصدير" 
                            onclick="btnExport_Click" CssClass="submit" />
                    </td>
                    <td width="250px"></td>
                    <td>
                        <asp:Button ID="btnAddUser" runat="server"  Text="إضافة مستخدم" 
                             CssClass="submit" OnClick="btnAddUser_Click" />
                    </td>
                </tr>
            </table>
                    </td>
                </tr>
                </table></td>
                       </tr>
                       <tr>
                           <td colspan="2">
                                <table cellpadding="2" cellspacing="0" border="0" width="99%" align="center">
                                    <tr>
                                        <td>
                                        <asp:DataGrid ID="dgUsers" runat="server" CellPadding="4"  CssClass="mGrid"
                                Width="100%" 
                                
                                
                                AutoGenerateColumns="False" 
                                
                                                                            onitemcommand="dgUsers_ItemCommand1" 
                                onitemcreated="dgUsers_ItemCreated" 
                                onitemdatabound="dgUsers_ItemDataBound" 
                                onselectedindexchanged="dgUsers_SelectedIndexChanged" 
                                             OnPageIndexChanged="dgUsers_PageIndexChanged"
                                             NextPageText="Next" PrevPageText="Prev" 
                                             AllowPaging="True"  PageSize="30" GridLines="None"

                                >
                                                  <PagerStyle CssClass="row1" HorizontalAlign="Center"  />
                                <AlternatingItemStyle CssClass="row2"  />
                                <ItemStyle CssClass="row1"  />
                                <HeaderStyle CssClass="dgHeader"/>
                                <Columns>

                                    <asp:BoundColumn DataField="USER_ID" HeaderText="USER_ID" Visible="False">
                                    </asp:BoundColumn>

                                    <asp:BoundColumn DataField="SNO" HeaderText="م">
                                        <HeaderStyle HorizontalAlign="Center" Width="8%" />
                                        <ItemStyle HorizontalAlign="Center" Width="8%" />
                                    </asp:BoundColumn>

                                    <asp:BoundColumn DataField="NAME" HeaderText="إسم">
                                    <HeaderStyle  Width="10%" />
                                    <ItemStyle Width="10%" />
                                    </asp:BoundColumn>
                                   
                                    
                                    <asp:BoundColumn DataField="EMAIL_ADDRESS" HeaderText="البريد الإلكتروني">
                                                                                                              <HeaderStyle  Width="9%" />
                                    <ItemStyle Width="9%" />
                                    </asp:BoundColumn>

                                    <asp:BoundColumn DataField="IDN" HeaderText="رقم الهوية">
                                                                      <HeaderStyle  Width="9%" />
                                    <ItemStyle Width="9%" />
                                    </asp:BoundColumn>

                                    <asp:BoundColumn DataField="MOB_NO" HeaderText="الهاتف  ">
                                    <HeaderStyle  Width="7%" />
                                    <ItemStyle Width="7%" />
                                    </asp:BoundColumn>

                                    <asp:BoundColumn DataField="LL_NO" HeaderText="الهاتف">
                                    <HeaderStyle  Width="7%" />
                                    <ItemStyle Width="7%" />
                                    </asp:BoundColumn>






                                                                        <asp:TemplateColumn HeaderText="حذف">
                                                                        <HeaderStyle HorizontalAlign="Center" Width="4%" />
                                    <ItemStyle HorizontalAlign="Center"  Width="4%" />
                                        <ItemTemplate>

                                            <asp:ImageButton ID="imgBtnDelete" runat="server" CommandName="DELETE" 
                                                ImageUrl="~/Images/iconDelete2.gif" />
                                        </ItemTemplate>
                                    </asp:TemplateColumn>



                                    <asp:TemplateColumn HeaderText="تحرير">
                                                                        <HeaderStyle HorizontalAlign="Center" Width="4%" />
                                    <ItemStyle HorizontalAlign="Center"  Width="4%" />
                                        <ItemTemplate>
                                            <asp:ImageButton ID="imgBtnEdit" runat="server" CommandName="EDIT" 
                                                ImageUrl="~/Images/iconEdit.png" />
                                        </ItemTemplate>
                                    </asp:TemplateColumn>



                                    <asp:TemplateColumn HeaderText="تغيير كلمة المرور">
                                    <HeaderStyle HorizontalAlign="Center" Width="4%" />
                                    <ItemStyle HorizontalAlign="Center"  Width="4%" />
                                        <ItemTemplate>
                                            <asp:ImageButton ID="imgBtnChangePassword" runat="server" CommandName="CP" 
                                                ImageUrl="~/Images/password.png" />
                                        </ItemTemplate>
                                    </asp:TemplateColumn>


                                </Columns>
                            </asp:DataGrid>
                                        </td>
                                    </tr>
                                </table>
                                <br />
                               <br />
                            </td>
                       </tr>
        <tr>
        <td>
                
        </td>
        <td valign="top">
            
        </td>
        </tr>

        </table>
        <%--        </ContentTemplate>
        <Triggers>
        <asp:PostBackTrigger ControlID="btnExport" /> 
        </Triggers>
    </asp:UpdatePanel>--%>
    <br />
    <br />
    <br />
            </form>
    <p style="direction: ltr">
        &nbsp;</p>
    </body>
</html>
