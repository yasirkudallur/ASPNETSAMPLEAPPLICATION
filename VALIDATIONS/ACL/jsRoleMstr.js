// JScript File
function validateEn() {
    if (document.getElementById("txtRollName").value == 0) {
        alert("Enter role name in english!!!");
        document.getElementById("txtRollName").focus();
        return false;
    }

    if (document.getElementById("txtRollNameAr").value == 0) {
        alert("Enter role name in arabic!!!");
        document.getElementById("txtRollNameAr").focus();
        return false;
    }

    if (document.getElementById("txtRollCode").value == 0) {
        alert("Enter role code!!!");
        document.getElementById("txtRollCode").focus();
        return false;
    }
    return true;
}
function validateAr() {
    if (document.getElementById("txtRollName").value == 0) {
        alert("Enter role name in english!!!");
        document.getElementById("txtRollName").focus();
        return false;
    }

    if (document.getElementById("txtRollNameAr").value == 0) {
        alert("Enter role name in arabic!!!");
        document.getElementById("txtRollNameAr").focus();
        return false;
    }

    if (document.getElementById("txtRollCode").value == 0) {
        alert("Enter role code!!!");
        document.getElementById("txtRollCode").focus();
        return false;
    }
    return true;
}
function validateDelete() {
    if (confirm("Are you sure you want to delete the contact?") == true)
        return true;
    else
        return false;
}
function Clear() {
    document.getElementById("txtRollName").value = "";
    document.getElementById("txtRollNameAr").value = "";
    document.getElementById("txtRollCode").value = "";
    return true;
}
function ClearMessage() {
    document.getElementById("lblMessage").innerHTML = "";
    document.getElementById('tblMessage').style.display = 'none';
}
