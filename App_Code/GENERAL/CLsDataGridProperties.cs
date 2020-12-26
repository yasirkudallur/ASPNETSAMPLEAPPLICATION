using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Security.Cryptography;
using System.IO;
using System.Text;
using System.Collections;
using System.Data.SqlClient;
/// <summary>
/// Summary description for CLsDataGridProperties
/// </summary>
public class CLsDataGridProperties : Page
{
    public DataTable DtForPrivilageSettings = new DataTable();
	public CLsDataGridProperties()
	{
        
	}
    //faslu: to Disable LinkButtons on the grid by passing datagrid name ,Delete link button name,Edit link button name,Delete link button cell index, Edit link button cell index, Privilage, flag to check : whether control come from edit klnk button click.
    //If we call this function from Item databound then bool flag is false else true.
    public void DisableLinkButtonOnDataGrid(DataGrid ObjDataGrid, string DeleteBtnName, string EditBtnName, int CelIndexOfDeleteBtn, int CelIndexOfEditBtn, string Privilag)
    {
        string Privilage = DeCryPt(Privilag);
        string[] Privil = Privilage.Split(','); //Storing privilage to string array : privil
        // if no permission to delete
        
            if (Privil[2].ToString() == "0")//If the user has no privilage to delete.
            {
                if (ObjDataGrid.Items.Count != 0)
                {
                        if (((LinkButton)ObjDataGrid.Items[ObjDataGrid.Items.Count - 1].FindControl(DeleteBtnName)) != null)//If Delete link button is there.
                        {
                            ((LinkButton)ObjDataGrid.Items[ObjDataGrid.Items.Count - 1].FindControl(DeleteBtnName)).Text = "";
                            ((LinkButton)ObjDataGrid.Items[ObjDataGrid.Items.Count - 1].FindControl(DeleteBtnName)).Enabled = false;
                            ObjDataGrid.Items[ObjDataGrid.Items.Count - 1].Cells[CelIndexOfDeleteBtn].Text = "DELETE";
                            ObjDataGrid.Items[ObjDataGrid.Items.Count - 1].Cells[CelIndexOfDeleteBtn].Font.Underline = true;
                            ObjDataGrid.Items[ObjDataGrid.Items.Count - 1].Cells[CelIndexOfDeleteBtn].ForeColor = System.Drawing.Color.DarkGray;
                        }
                        else
                            ObjDataGrid.Items[ObjDataGrid.Items.Count - 1].Cells[CelIndexOfDeleteBtn].Enabled = false;
                }
            }
            if (Privil[1].ToString() == "0")//If the user has no privilage to Edit.
            {
                if (ObjDataGrid.Items.Count != 0)
                {
                    if (((LinkButton)ObjDataGrid.Items[ObjDataGrid.Items.Count - 1].FindControl(EditBtnName)) != null)//If link button exists.
                    {
                        string Code;
                        if (((LinkButton)ObjDataGrid.Items[ObjDataGrid.Items.Count - 1].FindControl(EditBtnName)).Text == "")//IF link button text is null.
                        {
                            Code = ObjDataGrid.Items[ObjDataGrid.Items.Count - 1].Cells[CelIndexOfEditBtn].Text;
                        }
                        else
                        {
                            Code = ((LinkButton)ObjDataGrid.Items[ObjDataGrid.Items.Count - 1].FindControl(EditBtnName)).Text;
                        }
                        ((LinkButton)ObjDataGrid.Items[ObjDataGrid.Items.Count - 1].FindControl(EditBtnName)).Text = "";
                        ((LinkButton)ObjDataGrid.Items[ObjDataGrid.Items.Count - 1].FindControl(EditBtnName)).Enabled = false;
                        ObjDataGrid.Items[ObjDataGrid.Items.Count - 1].Cells[CelIndexOfEditBtn].Text = Code;
                        if (Code.ToString().ToUpper().Trim() == "EDIT")//If the linkButton caption is"Edit"
                        {
                            ObjDataGrid.Items[ObjDataGrid.Items.Count - 1].Cells[CelIndexOfEditBtn].Font.Underline = true;
                            ObjDataGrid.Items[ObjDataGrid.Items.Count - 1].Cells[CelIndexOfEditBtn].ForeColor = System.Drawing.Color.DarkGray;
                        }
                    }
                    else
                        ObjDataGrid.Items[ObjDataGrid.Items.Count - 1].Cells[CelIndexOfEditBtn].Enabled = false;
                }
            }
        
    }
    //Faizal: to Disable LinkButtons on the grid by passing datagrid name ,Delete link button name,Edit link button name,Delete link button cell index, Edit link button cell index, Privilage, flag to check : whether control come from edit klnk button click.
    //If we call this function from Item databound then bool flag is false else true.
    public void DisableButtonOnDataGrid(DataGrid ObjDataGrid, string DeleteBtnName, string EditBtnName, int CelIndexOfDeleteBtn, int CelIndexOfEditBtn, string Privilag)
    {
        string Privilage = DeCryPt(Privilag);
        string[] Privil = Privilage.Split(','); //Storing privilage to string array : privil
        // if no permission to delete

        if (Privil[2].ToString() == "0")//If the user has no privilage to delete.
        {
            if (ObjDataGrid.Items.Count != 0)
            {
                if (((Button)ObjDataGrid.Items[ObjDataGrid.Items.Count - 1].FindControl(DeleteBtnName)) != null)//If Delete link button is there.
                {
                    //((Button)ObjDataGrid.Items[ObjDataGrid.Items.Count - 1].FindControl(DeleteBtnName)).Text = "";
                    ((Button)ObjDataGrid.Items[ObjDataGrid.Items.Count - 1].FindControl(DeleteBtnName)).Enabled = false;
                    ((Button)ObjDataGrid.Items[ObjDataGrid.Items.Count - 1].FindControl(DeleteBtnName)).ForeColor = System.Drawing.Color.DarkGray;
                    //ObjDataGrid.Items[ObjDataGrid.Items.Count - 1].Cells[CelIndexOfDeleteBtn].Text = "DELETE";
                    //ObjDataGrid.Items[ObjDataGrid.Items.Count - 1].Cells[CelIndexOfDeleteBtn].Font.Underline = true;
                    //ObjDataGrid.Items[ObjDataGrid.Items.Count - 1].Cells[CelIndexOfDeleteBtn].ForeColor = System.Drawing.Color.DarkGray;
                }
                else
                    ObjDataGrid.Items[ObjDataGrid.Items.Count - 1].Cells[CelIndexOfDeleteBtn].Enabled = false;
            }
        }
        if (Privil[1].ToString() == "0")//If the user has no privilage to Edit.
        {
            if (ObjDataGrid.Items.Count != 0)
            {
                if (((Button)ObjDataGrid.Items[ObjDataGrid.Items.Count - 1].FindControl(EditBtnName)) != null)//If link button exists.
                {
                    //string Code;
                    //if (((Button)ObjDataGrid.Items[ObjDataGrid.Items.Count - 1].FindControl(EditBtnName)).Text == "")//IF link button text is null.
                    //{
                    //    Code = ObjDataGrid.Items[ObjDataGrid.Items.Count - 1].Cells[CelIndexOfEditBtn].Text;
                    //}
                    //else
                    //{
                    //    Code = ((Button)ObjDataGrid.Items[ObjDataGrid.Items.Count - 1].FindControl(EditBtnName)).Text;
                    //}
                    //((Button)ObjDataGrid.Items[ObjDataGrid.Items.Count - 1].FindControl(EditBtnName)).Text = "";
                    ((Button)ObjDataGrid.Items[ObjDataGrid.Items.Count - 1].FindControl(EditBtnName)).Enabled = false;
                    ((Button)ObjDataGrid.Items[ObjDataGrid.Items.Count - 1].FindControl(EditBtnName)).ForeColor = System.Drawing.Color.DarkGray;
                    //ObjDataGrid.Items[ObjDataGrid.Items.Count - 1].Cells[CelIndexOfEditBtn].Text = Code;
                    //if (Code.ToString().ToUpper().Trim() == "EDIT")//If the linkButton caption is"Edit"
                    //{
                    //    ObjDataGrid.Items[ObjDataGrid.Items.Count - 1].Cells[CelIndexOfEditBtn].Font.Underline = true;
                    //    ObjDataGrid.Items[ObjDataGrid.Items.Count - 1].Cells[CelIndexOfEditBtn].ForeColor = System.Drawing.Color.DarkGray;
                    //}
                }
                else
                    ObjDataGrid.Items[ObjDataGrid.Items.Count - 1].Cells[CelIndexOfEditBtn].Enabled = false;
            }
        }

    }

    public void DisableGridEditButton(DataGrid ObjDataGrid, string EditBtnName, int CelIndexOfEditBtn, string Privilag)
    {
        string Privilage = DeCryPt(Privilag);
        string[] Privil = Privilage.Split(',');
        if (Privil[1].ToString() == "0")//If the user has no privilage to Edit.
        {
            if (ObjDataGrid.Items.Count != 0)
            {
                if (((LinkButton)ObjDataGrid.Items[ObjDataGrid.Items.Count - 1].FindControl(EditBtnName)) != null)//If link button exists.
                {
                    string Code;
                    if (((LinkButton)ObjDataGrid.Items[ObjDataGrid.Items.Count - 1].FindControl(EditBtnName)).Text == "")//IF link button text is null.
                    {
                        Code = ObjDataGrid.Items[ObjDataGrid.Items.Count - 1].Cells[CelIndexOfEditBtn].Text;
                    }
                    else
                    {
                        Code = ((LinkButton)ObjDataGrid.Items[ObjDataGrid.Items.Count - 1].FindControl(EditBtnName)).Text;
                    }
                    ((LinkButton)ObjDataGrid.Items[ObjDataGrid.Items.Count - 1].FindControl(EditBtnName)).Text = "";
                    ((LinkButton)ObjDataGrid.Items[ObjDataGrid.Items.Count - 1].FindControl(EditBtnName)).Enabled = false;
                    ObjDataGrid.Items[ObjDataGrid.Items.Count - 1].Cells[CelIndexOfEditBtn].Text = Code;
                    if (Code.ToString().ToUpper() == "EDIT")//If the linkButton caption is"Edit"
                    {
                        ObjDataGrid.Items[ObjDataGrid.Items.Count - 1].Cells[CelIndexOfEditBtn].Font.Underline = true;
                        ObjDataGrid.Items[ObjDataGrid.Items.Count - 1].Cells[CelIndexOfEditBtn].ForeColor = System.Drawing.Color.DarkGray;
                    }
                }
                else
                    ObjDataGrid.Items[ObjDataGrid.Items.Count - 1].Cells[CelIndexOfEditBtn].Enabled = false;
            }
        }
    }


    public void DisableSaveButton(ImageButton BtnSave, string Privilag)
    {
        string Privilage = DeCryPt(Privilag);
        if (Privilage != null)
        {
          string[]  privi = Privilage.ToString().Split(',');
            if (privi[0].ToString() == "0")
            {
                BtnSave.Enabled = false;
            }
        }
    }
    public static void DisableSaveButton(Button BtnSave, string Privilag)
    {
        string Privilage = DeCryPt(Privilag);
        if (Privilage != null)
        {
            string[] privi = Privilage.ToString().Split(',');
            if (privi[0].ToString() == "0")
            {
                BtnSave.Enabled = false;
            }
        }
    }
    public string EncryPt(string plainText)
    {
        try
        {
            //string plainText = Privilage;
            string passPhrase = "Pas5pr@se";        // can be any string
            string saltValue = "s@1tValue";        // can be any string
            string hashAlgorithm = "SHA1";             // can be "MD5"
            int passwordIterations = 2;                  // can be any number
            string initVector = "@1B2c3D4e5F6g7H8"; // must be 16 bytes
            plainText = plainText.Replace(",", "Sacrosys Private Limited");
            // Convert strings into byte arrays.
            // Let us assume that strings only contain ASCII codes.
            // If strings include Unicode characters, use Unicode, UTF7, or UTF8 
            // encoding.
            byte[] initVectorBytes = Encoding.ASCII.GetBytes(initVector);
            byte[] saltValueBytes = Encoding.ASCII.GetBytes(saltValue);

            // Convert our plaintext into a byte array.
            // Let us assume that plaintext contains UTF8-encoded characters.
            byte[] plainTextBytes = Encoding.UTF8.GetBytes(plainText);

            // First, we must create a password, from which the key will be derived.
            // This password will be generated from the specified passphrase and 
            // salt value. The password will be created using the specified hash 
            // algorithm. Password creation can be done in several iterations.
            PasswordDeriveBytes password = new PasswordDeriveBytes(
                                                            passPhrase,
                                                            saltValueBytes,
                                                            hashAlgorithm,
                                                            passwordIterations);

            // Use the password to generate pseudo-random bytes for the encryption
            // key. Specify the size of the key in bytes (instead of bits).
            byte[] keyBytes = password.GetBytes(256 / 8);

            // Create uninitialized Rijndael encryption object.
            RijndaelManaged symmetricKey = new RijndaelManaged();

            // It is reasonable to set encryption mode to Cipher Block Chaining
            // (CBC). Use default options for other symmetric key parameters.
            symmetricKey.Mode = CipherMode.CBC;

            // Generate encryptor from the existing key bytes and initialization 
            // vector. Key size will be defined based on the number of the key 
            // bytes.
            ICryptoTransform encryptor = symmetricKey.CreateEncryptor(
                                                             keyBytes,
                                                             initVectorBytes);

            // Define memory stream which will be used to hold encrypted data.
            MemoryStream memoryStream = new MemoryStream();

            // Define cryptographic stream (always use Write mode for encryption).
            CryptoStream cryptoStream = new CryptoStream(memoryStream,
                                                         encryptor,
                                                         CryptoStreamMode.Write);
            // Start encrypting.
            cryptoStream.Write(plainTextBytes, 0, plainTextBytes.Length);

            // Finish encrypting.
            cryptoStream.FlushFinalBlock();

            // Convert our encrypted data from a memory stream into a byte array.
            byte[] cipherTextBytes = memoryStream.ToArray();

            // Close both streams.
            memoryStream.Close();
            cryptoStream.Close();

            // Convert encrypted data into a base64-encoded string.
            string cipherText = Convert.ToBase64String(cipherTextBytes);
            cipherText = cipherText.Replace('+', '^');
            // Return encrypted string.
            return cipherText;
        }
        catch (Exception e)
        {
            System.Web.HttpContext.Current.Response.Redirect("FrmIndex.aspx");
            return null;
        }

    }
    public static string DeCryPt(string cipherText)
    {
        try
        {
            //string cipherText = ToDecryPt;
            string passPhrase = "Pas5pr@se";        // can be any string
            string saltValue = "s@1tValue";        // can be any string
            string hashAlgorithm = "SHA1";             // can be "MD5"
            int passwordIterations = 2;                  // can be any number
            string initVector = "@1B2c3D4e5F6g7H8"; // must be 16 bytes

            cipherText = cipherText.Replace('^', '+');
            // Convert strings defining encryption key characteristics into byte
            // arrays. Let us assume that strings only contain ASCII codes.
            // If strings include Unicode characters, use Unicode, UTF7, or UTF8
            // encoding.
            byte[] initVectorBytes = Encoding.ASCII.GetBytes(initVector);
            byte[] saltValueBytes = Encoding.ASCII.GetBytes(saltValue);

            // Convert our ciphertext into a byte array.
            byte[] cipherTextBytes = Convert.FromBase64String(cipherText);

            // First, we must create a password, from which the key will be 
            // derived. This password will be generated from the specified 
            // passphrase and salt value. The password will be created using
            // the specified hash algorithm. Password creation can be done in
            // several iterations.
            PasswordDeriveBytes password = new PasswordDeriveBytes(
                                                            passPhrase,
                                                            saltValueBytes,
                                                            hashAlgorithm,
                                                            passwordIterations);

            // Use the password to generate pseudo-random bytes for the encryption
            // key. Specify the size of the key in bytes (instead of bits).
            byte[] keyBytes = password.GetBytes(256 / 8);

            // Create uninitialized Rijndael encryption object.
            RijndaelManaged symmetricKey = new RijndaelManaged();

            // It is reasonable to set encryption mode to Cipher Block Chaining
            // (CBC). Use default options for other symmetric key parameters.
            symmetricKey.Mode = CipherMode.CBC;

            // Generate decryptor from the existing key bytes and initialization 
            // vector. Key size will be defined based on the number of the key 
            // bytes.
            ICryptoTransform decryptor = symmetricKey.CreateDecryptor(
                                                             keyBytes,
                                                             initVectorBytes);

            // Define memory stream which will be used to hold encrypted data.
            MemoryStream memoryStream = new MemoryStream(cipherTextBytes);

            // Define cryptographic stream (always use Read mode for encryption).
            CryptoStream cryptoStream = new CryptoStream(memoryStream,
                                                          decryptor,
                                                          CryptoStreamMode.Read);

            // Since at this point we don't know what the size of decrypted data
            // will be, allocate the buffer long enough to hold ciphertext;
            // plaintext is never longer than ciphertext.
            byte[] plainTextBytes = new byte[cipherTextBytes.Length];

            // Start decrypting.
            int decryptedByteCount = cryptoStream.Read(plainTextBytes,
                                                       0,
                                                       plainTextBytes.Length);

            // Close both streams.
            memoryStream.Close();
            cryptoStream.Close();

            // Convert decrypted data into a string. 
            // Let us assume that the original plaintext string was UTF8-encoded.
            string plainText = Encoding.UTF8.GetString(plainTextBytes,
                                                       0,
                                                       decryptedByteCount);

            plainText = plainText.Replace("Sacrosys Private Limited", ",");

            // Return decrypted string.   
            return plainText;
        }
        catch (Exception e)
        {
            System.Web.HttpContext.Current.Response.Redirect("FrmLogin.aspx");
            return null;
        }


    }
}
