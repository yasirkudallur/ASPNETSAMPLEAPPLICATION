<%@ Page Language="C#" AutoEventWireup="true" CodeFile="frmDepartmentMaster.aspx.cs" Inherits="frmDepartmentMaster" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>

    <link href="../Styles/SiteContent.css" rel="stylesheet" />
    <script language="javascript" type="text/javascript">
    function validateControls()
    {
        if (document.getElementById("txtDepartment").value.length == 0)
        {
            alert("يرجى تحديد الادارة");
            document.getElementById("txtDepartment").focus();
            return false;
        }

        return true;
    }
    </script>

</head>
<body dir="rtl">
    <form id="form1" runat="server">
        <br />
    <table cellpadding="0" cellspacing="0" align="center" bgcolor="White" width="98%" style="border: 1px solid #D0C4B4;">
        <tr>
        <td valign="middle" bgcolor="#F6F4F0" 
                
                
                style="color: #493318; font-weight: bold; padding-right: 20px; padding-left: 20px;" 
                height="30px"> 
                  <asp:Label ID="Label4" runat="server" Text="الاداراة" 
                            meta:resourcekey="lblHeadingResource1" CssClass="lblBox"></asp:Label></td>
 
        </tr>
        <tr>
            <td>
                <table cellpadding="2" cellspacing="0" border="0" align="center">
            <tr>
                <td>
                    &nbsp;</td>
                <td>
                    &nbsp;</td>
                <td style="color: red">
                    &nbsp;</td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="Label1" runat="server" Text="الإدارة  :  " 
                        CssClass="lblBox"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtDepartment" runat="server" CssClass="txtBox" 
                        Width="400px"></asp:TextBox>
                </td>
                <td style="color: red">
                    *</td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="Label3" runat="server" Text="فعال ? " 
                        CssClass="lblBox"></asp:Label>
                </td>
                <td>
                    <asp:RadioButtonList ID="radActive" runat="server" RepeatDirection="Horizontal">
                        <asp:ListItem Value="1">نعم</asp:ListItem>
                        <asp:ListItem Value="0">لا</asp:ListItem>
                    </asp:RadioButtonList>
                </td>
                <td>
                    &nbsp;</td>
            </tr>
            <tr>
                <td>
                    &nbsp;</td>
                <td>
                    <asp:Button ID="btnSubmit" runat="server" onclick="btnSubmit_Click" 
                        Text="حفظ" CssClass="submit" />
                </td>
                <td>
                    &nbsp;</td>
            </tr>
            <tr>
                <td>
                    &nbsp;</td>
                <td>
                    <asp:Label ID="lblMessage" runat="server" CssClass="lblBox"></asp:Label>
                </td>
                <td>
                    &nbsp;</td>
            </tr>
        </table>
            </td>
        </tr>
        <tr>
            <td>
                <table cellpadding="2" cellspacing="0" border="0" width="99%" align="center">
                    <tr>
                        <td>
                                            <asp:DataGrid ID="dgDepartments" runat="server" AutoGenerateColumns="False" 
                                CellPadding="4" CssClass="mGrid" Font-Size="12px" 
                                onitemcommand="dgDepartments_ItemCommand" 
                                onitemcreated="dgDepartments_ItemCreated" 
                                onitemdatabound="dgDepartments_ItemDataBound" Width="100%" AllowPaging="True" PageSize="30" OnPageIndexChanged="dgDepartments_PageIndexChanged">
                                <PagerStyle CssClass="row1" />
                                <AlternatingItemStyle CssClass="row2" />
                                <ItemStyle CssClass="row1" />
                                <HeaderStyle CssClass="dgHeader" />
                                  <Columns>
                                <asp:BoundColumn DataField="SNO" HeaderText="م">
                                    <HeaderStyle HorizontalAlign="Center" Width="10%" />
                                    <ItemStyle HorizontalAlign="Center" Width="10%" />
                                </asp:BoundColumn>
                                <asp:BoundColumn DataField="DEPARTMENT_NAME_AR" HeaderText="الإدارة ">
                                    <HeaderStyle  Width="50%" />
                                    <ItemStyle  Width="50%" />
                                </asp:BoundColumn>
                                <asp:BoundColumn DataField="DEPARTMENT_ID" HeaderText="DEPARTMENT_ID" Visible="False">
                                </asp:BoundColumn>
                                <asp:BoundColumn HeaderText="فعال؟">
                                    <HeaderStyle HorizontalAlign="Center" Width="20%" />
                                    <ItemStyle HorizontalAlign="Center" Width="20%" />
                                </asp:BoundColumn>
                                <asp:TemplateColumn HeaderText="تحرير">
                                    <HeaderStyle HorizontalAlign="Center" Width="20%" />
                                    <ItemStyle HorizontalAlign="Center" Width="20%" />
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lnkBtnEdit" runat="server" CommandName="EDIT">تحرير</asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="حذف" Visible="False">
                                                                    <HeaderStyle HorizontalAlign="Center" Width="150px" />
                                    <ItemStyle HorizontalAlign="Center" Width="150px" />
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lnkBtnDelete" runat="server" CommandName="DELETE">حذف</asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:BoundColumn DataField="ACTIVE" HeaderText="Active" Visible ="false">
                                </asp:BoundColumn>
                            </Columns>
                                                            <PagerStyle HorizontalAlign="Center" Mode="NumericPages" />
                            </asp:DataGrid>
                        </td>
                    </tr>
                </table>

            </td>
        </tr>
        <tr>
            <td>

                &nbsp;</td>
        </tr>

        </table>
        


    </form>
</body>
</html>
