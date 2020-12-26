<%@ Page Language="C#" AutoEventWireup="true" EnableEventValidation ="false" CodeFile="frmViewVisitors.aspx.cs" Inherits="frmViewVisitors" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>



<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">

    <link href="../css/SiteContent.css" rel="stylesheet" />
    <title></title>
    <style>
    .breakword
    {
    word-wrap:break-word;
    word-break:break-all;
    }
    </style>
</head>
<body  style="margin-top: 0px;">
    <form id="form1" runat="server">

           <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
        <br />
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>
                   <table cellpadding="0" cellspacing="0" width="99%" align="center" 
                style="border: 1px solid #D0C4B4" bgcolor="white">
  <tr style="background-color: #F4F0EA;" align="center">
        <td style="font-size: 25px" colspan="2">
            Reports 
        </td>
    </tr>
                       <tr>
                           <td colspan="2">
                            <table cellpadding="5" cellspacing="0" border="0" align="center" id="Table1" runat="server">
                <tr>
                    <td>
                            &nbsp;</td>
                    <td>
                        <asp:Label ID="lblIdn" runat="server" Text="Emirates Id Number : "></asp:Label>
                    </td>
                    <td>
                                              <asp:TextBox ID="txtIdn" runat="server" CssClass="txtBox"  
                                        Width="300px"></asp:TextBox>
                        <asp:FilteredTextBoxExtender ID="fteIdn" TargetControlID="txtIdn" runat="server" FilterType="Numbers"></asp:FilteredTextBoxExtender>
                    </td>
                </tr>
                                <tr>
                                    <td>&nbsp;</td>
                                    <td>
                                        <asp:Label ID="lblIdn0" runat="server" Text="Date : "></asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtDate" runat="server" CssClass="txtBox" Width="150px"></asp:TextBox>
<asp:CalendarExtender ID="CalendarExtender1" TargetControlID="txtDate" runat="server" Format="dd/MM/yyyy"></asp:CalendarExtender>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        &nbsp;</td>
                                    <td colspan="2">
                                        <table align="center" border="0" cellpadding="2" cellspacing="0">
                                            <tr>
                                                <td>
                                                    <asp:Button ID="btnSearch" runat="server" CssClass="submit" onclick="btnSearch_Click" Text="Search" />
                                                </td>
                                                <td>
                                                    <asp:Button ID="btnExport" runat="server" CssClass="submit" onclick="btnExport_Click" Text="Export" />
                                                </td>
                                                <td>
                                                    <img src="../Images/iconExcel.png" width="30px" height="30px" />
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                <tr>
                <td align="center">
                                                     &nbsp;</td>
                    <td align="center" colspan="2">
                        <asp:Label ID="lblMessage" runat="server"></asp:Label>
                    </td>
                </tr>
                </table></td>
                       </tr>
                       <tr>
                           <td colspan="2">
                                <table cellpadding="2" cellspacing="0" border="0" width="99%" align="center">
                                    <tr>
                                        <td>
                                        <asp:DataGrid ID="dgMyTaskDetails" runat="server" AutoGenerateColumns="False" 
                                CellPadding="4" CssClass="mGrid" 
                                onitemcommand="dgMyTaskDetails_ItemCommand1" 
                                onitemcreated="dgMyTaskDetails_ItemCreated" 
                                onitemdatabound="dgMyTaskDetails_ItemDataBound" 
                                onselectedindexchanged="dgMyTaskDetails_SelectedIndexChanged" Width="100%" AllowPaging="True" 
                                            OnPageIndexChanged="dgMyTaskDetails_PageIndexChanged" PageSize="50" GridLines="None">
                                <PagerStyle CssClass="row1" NextPageText="Next" PrevPageText="Previous &nbsp;&nbsp;" Font-Bold="True" Font-Size="15px" HorizontalAlign="Center" />
                                <AlternatingItemStyle CssClass="row2" />
                                <ItemStyle CssClass="row1" />
                                <HeaderStyle CssClass="dgHeader" />
                                <Columns>

                                    <asp:BoundColumn DataField="VISITOR_ID" HeaderText="VISITOR_ID" Visible="False"></asp:BoundColumn>

                                    <asp:BoundColumn DataField="SNO" HeaderText="S.No">
                                        <HeaderStyle HorizontalAlign="Center" Width="4%" />
                                        <ItemStyle HorizontalAlign="Center" Width="4%" />
                                    </asp:BoundColumn>

                                    
                                    <asp:BoundColumn DataField="VISITED_DATE" HeaderText="Date of swab">
                                        <HeaderStyle Width="7%" />
                                        <ItemStyle Width="7%" CssClass="breakword" />
    
                                    </asp:BoundColumn>
                                   


                                    <asp:BoundColumn DataField="IDN" HeaderText="Emirates ID">
                                        <HeaderStyle  Width="10%" />
                                        <ItemStyle  Width="10%" />
                                    </asp:BoundColumn>



                                    <asp:BoundColumn DataField="CARD_EXPIRY_DATE" HeaderText="Id Expiry">
                                            <HeaderStyle  Width="9%" />
                                        <ItemStyle  Width="9%" />
                                    </asp:BoundColumn>



                                    <asp:BoundColumn DataField="NAME_EN" HeaderText="Name">
                                        <HeaderStyle  Width="12%" />
                                        <ItemStyle  Width="12%" />
                                    </asp:BoundColumn>



                                    <asp:BoundColumn DataField="MOB_NUMBER" HeaderText="Mobile">
                                                             <HeaderStyle  Width="7%" />
                                        <ItemStyle  Width="7%" />
                                    </asp:BoundColumn>



                                    <asp:BoundColumn DataField="CATEGORY" HeaderText="Location">
                                                           <HeaderStyle  Width="11%" />
                                        <ItemStyle  Width="11%" />
                                    </asp:BoundColumn>



                                    <asp:BoundColumn HeaderText="Nationality" DataField="NATIONALITY">
                                        <HeaderStyle  Width="10%" />
                                        <ItemStyle  Width="10%" />
                                    </asp:BoundColumn>

                                    <asp:BoundColumn DataField="GENDER" HeaderText="Gender">
                                        <HeaderStyle  Width="8%" />
                                        <ItemStyle  Width="8%" />
                                    </asp:BoundColumn>



                                    <asp:BoundColumn DataField="DOB" HeaderText="Dob">
                                                      <HeaderStyle  Width="8%" />
                                        <ItemStyle  Width="8%" />
                                    </asp:BoundColumn>



                                    <asp:BoundColumn DataField="CATEGORY_ID" HeaderText="Age">
                                        <HeaderStyle  Width="9%" />
                                        <ItemStyle  Width="9%" />
                                    </asp:BoundColumn>



                                    <asp:TemplateColumn HeaderText="Details">
                                        <HeaderStyle HorizontalAlign="Center" Width="5%" />
                                        <ItemStyle HorizontalAlign="Center" Width="5%" />
                                        <ItemTemplate>
                                            <asp:ImageButton ID="imgBtnInfo" runat="server" ImageUrl="~/Images/info.png" CommandName="INFO" />
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
                </ContentTemplate>
        <Triggers>
        <asp:PostBackTrigger ControlID="btnExport" /> 
        </Triggers>
    </asp:UpdatePanel>
    <br />
    <br />
    <br />

    </form>
    </body>
</html>
