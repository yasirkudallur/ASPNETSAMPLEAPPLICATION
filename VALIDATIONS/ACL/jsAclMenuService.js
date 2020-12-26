function validateEn()
{
    alert("hai");
    if (document.getElementById("DdlModuleId").value == 0) {
        alert("Please select module name !!!");
        document.getElementById("DdlModuleId").focus();
        return false;
    }
    if (document.getElementById("TxtMenuName").value == 0) {
        alert("Please enter menu name in english!!!");
        document.getElementById("TxtMenuName").focus();
        return false;
    }
    if (document.getElementById("TxtMenuNameAr").value == 0) {
        alert("Please enter menu name in arabic!!!");
        document.getElementById("TxtMenuNameAr").focus();
        return false;
    }

    if (document.getElementById("ddlURL").value == 0) {
        alert("Select Form URL.... ");
        document.getElementById("ddlURL").focus();
        return false;
    }
    return true;
}

    function ValidateDelete()
  {
         if (confirm("Are you sure you want to delete the contact?")==true)
            return true;
        else
            return false;
  }
  function Clear()
  {
      document.getElementById("TxtMenuName").value = "";
      document.getElementById("TxtMenuNameAr").value = "";
      
    document.getElementById("DdlModuleId").value=0;
    document.getElementById("DdlModuleId").focus();
    document.getElementById("ddlURL").selectedIndex=0;
    return true;
  }
  function ClearMessage()
  {
      document.getElementById("lblMessage").innerHTML = "";
      document.getElementById('tblMessage').style.display = 'none';
  }
