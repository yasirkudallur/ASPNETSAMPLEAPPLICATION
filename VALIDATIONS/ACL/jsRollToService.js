// JScript File

    function validateSaveBtn()
    {
        var cmb1 = document.getElementById("cmbRole");
        if(cmb1.options[cmb1.selectedIndex].value==0)
        {
            alert('No Role Selected');
	        document.getElementById("cmbRole").focus();            
            return false;            
        }
        self.location.reload();
    }
    
    function validateCopyBtn()
    {
        var cmb1 = document.getElementById("cmbCopyRole");
        if(cmb1.options[cmb1.selectedIndex].value==0)
        {
            alert('No Role Selected');
	        document.getElementById("cmbCopyRole").focus();            
            return false;            
        }
    }
    


    