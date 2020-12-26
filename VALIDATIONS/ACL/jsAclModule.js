// JScript File
document.getElementById("TxtName").focus();

function Validate()
    {
    if (document.getElementById("TxtName").value == 0)
        {
            alert("Name Field Cant Be Empty...");
            document.getElementById("TxtName").focus();
            return false;
        }
     
    }
    function ClearFields()
    {
        document.getElementById("TxtName").value = "";
        document.getElementById("TxtNameAr").value = "";
        document.getElementById("TxtName").focus();
        return false;
    }
    function ValidateDelete()
  {
         if (confirm("Are you sure you want to delete the contact?")==true)
            return true;
        else
            return false;
  }
  function ClearMessage()
  {
      document.getElementById("lblMessage").innerHTML = "";
      document.getElementById('tblMessage').style.display = 'none';
  }
  
  var g_blinkTime = 1;
  var g_blinkCounter = 0;
  function blinkElement(elementId)
  { 
    if ( (g_blinkCounter % 2) == 0 ) 
        { 
             if ( document.getElementById ) 
                 {  
                     document.getElementById(elementId).style.visibility = 'visible'; 
                 }
        } 
      else 
         {  
        if ( document.getElementById )
                {  
                     document.getElementById(elementId).style.visibility = 'hidden'; 
                }  
        } 
                             if ( g_blinkCounter < 10 ) 
                                { 
                                    g_blinkCounter++
                                }  
                            else 
                                { 
                                     document.getElementById(elementId).style.visibility = 'visible'; 
                                     g_blinkCounter=0;
                                     
                                } 
        window.setTimeout('blinkElement(\"' + elementId + '\")', g_blinkTime);
      }