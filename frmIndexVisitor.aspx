<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="frmIndexVisitor.aspx.cs" Inherits="frmIndexVisitor" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">
    <script language="javascript" type="text/javascript">

    window.history.forward(1);

    function calcHeight() {
        try {

            var the_height = document.getElementById('IFRAME1').contentWindow.document.body.scrollHeight;
            if (the_height > 400) {
                document.getElementById('IFRAME1').height = the_height;
            }
            else {
                document.getElementById('IFRAME1').height = 650;
            }
        }
        catch (e) {
            // alert("Exception");
        }







    }
    function IFRAME1_onclick() {

    }

</script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
        <IFRAME  style="WIDTH: 100%;"  id="IFRAME1" 
    language="javascript"  onload="calcHeight();"
name="dispFrame"  frameBorder="0" scrolling="No"  
    onclick="return IFRAME1_onclick()"  src="frmBlankPage.aspx"></IFRAME>
</asp:Content>

