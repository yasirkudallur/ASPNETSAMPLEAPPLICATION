using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.IO; 

/// <summary>
/// Summary description for clsResourceReader
/// </summary>
public class clsPropertyReader
{
    FileStream f;
    clsBllfrmproperties objClsBllfrmproperties = new clsBllfrmproperties();
    public clsPropertyReader()
	{
		//
		// TODO: Add constructor logic here
		//
	}
    public void PropertySetter(string strFrm, string strpropertyname, string value)
    {
        f = new FileStream("c:\\"+strFrm +".txt",FileMode.Append,FileAccess.Write);
        StreamWriter sw = new StreamWriter(f);
        sw.WriteLine(strpropertyname+"="+value);
        
        sw.Flush();
        sw.Close();
        f.Close();

       
    }
    //..............................
    public void LabelProperties(Control root,string frm)
    {
       
        try
        {
            string[] strr = propertyReader(frm);
            foreach (Control ctrl in root.Controls)
            {
                LabelProperties(ctrl, frm);
                if (ctrl is Label)
                {
                    for (int i = 1; i < strr.Length; i++)

                        if (((Label)ctrl).ID == strr[i])
                        {
                            ((Label)ctrl).Text = strr[i + 1];
                        }
                }
            }
        }
        catch
        {
            //File.Create(System.Web.HttpContext.Current.Server.MapPath("properties")); 
            
 
        }
    } 
    //...............................
    public string[] propertyReader(string strFrm)
    {
        string[] strproper ={ "0" };
        string str = null;
        string path=System.Web.HttpContext.Current.Server.MapPath("PROPERTIES");
        f = new FileStream(path+" \\" + strFrm + ".txt", FileMode.Open, FileAccess.Read);
        StreamReader stReader = new StreamReader(f);
        while(!stReader.EndOfStream)
        {
           str=str+"="+stReader.ReadLine();
        }
        strproper = str.Split('=');

        stReader.Close();
        f.Close();
        return strproper;
       // stReader.Flush();
        
    }
    
}
