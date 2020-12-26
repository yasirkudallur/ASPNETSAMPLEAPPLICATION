using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.IO;

/// <summary>
/// Summary description for MyWebRequest
/// </summary>
public class MyWebRequest
{
    private WebRequest request;
    private Stream dataStream;

    private string status;

    public String Status
    {
        get
        {
            return status;
        }
        set
        {
            status = value;
        }
    }

    public MyWebRequest(string url)
    {
        // Create a request using a URL that can receive a post.

        request = WebRequest.Create(url);
    }

    public MyWebRequest(string url, string method)
        : this(url)
    {

        if (method.Equals("GET") || method.Equals("POST"))
        {
            // Set the Method property of the request to POST.
            request.Method = method;
        }
        else
        {
            throw new Exception("Invalid Method Type");
        }
    }

    public MyWebRequest(string url, string method, string data)
        : this(url, method)
    {

        // Create POST data and convert it to a byte array.
        string postData = data;
        byte[] byteArray = Encoding.UTF8.GetBytes(postData);

        // Set the ContentType property of the WebRequest.
        //request.ContentType = "application/x-www-form-urlencoded";
        request.ContentType = "application/json";

        // Set the ContentLength property of the WebRequest.
        request.ContentLength = byteArray.Length;

        // Get the request stream.
        dataStream = request.GetRequestStream();

        // Write the data to the request stream.
        dataStream.Write(byteArray, 0, byteArray.Length);

        // Close the Stream object.
        dataStream.Close();

    }

    public MyWebRequest(string url, string method, String data, String Authorization)
: this(url, method)
    {

        //System.Net.ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls;

        // Set the ContentType property of the WebRequest.
        //request.ContentType = "application/x-www-form-urlencoded";
        //request.ContentType = "application/json";
        //request.Headers["Authorization"] = "Basic " + Authorization;

        // Get the request stream.
        //dataStream = request.GetRequestStream();

        // Close the Stream object.
        //dataStream.Close();

    }
    /*
    public string GetResponse()
    {
        // Get the original response.
        WebResponse response = request.GetResponse();

        this.Status = ((HttpWebResponse)response).StatusDescription;

        // Get the stream containing all content returned by the requested server.
        dataStream = response.GetResponseStream();

        // Open the stream using a StreamReader for easy access.
        StreamReader reader = new StreamReader(dataStream);

        // Read the content fully up to the end.
        string responseFromServer = reader.ReadToEnd();

        // Clean up the streams.
        reader.Close();
        dataStream.Close();
        response.Close();

        return responseFromServer;
    }
    */
    public string GetResponse()
    {
        string responseFromServer = null;
        try
        {
            // Get the original response.
            WebResponse response = request.GetResponse();

            this.Status = ((HttpWebResponse)response).StatusDescription;

            // Get the stream containing all content returned by the requested server.
            dataStream = response.GetResponseStream();

            // Open the stream using a StreamReader for easy access.
            StreamReader reader = new StreamReader(dataStream);

            // Read the content fully up to the end.
            responseFromServer = reader.ReadToEnd();

            // Clean up the streams.
            reader.Close();
            dataStream.Close();
            response.Close();


        }
        catch (WebException e)
        {
            using (WebResponse response = e.Response)
            {
                HttpWebResponse httpResponse = (HttpWebResponse)response;
                Console.WriteLine("Error code: {0}", httpResponse.StatusCode);
                using (Stream data = response.GetResponseStream())
                using (var reader = new StreamReader(data))
                {
                    responseFromServer = reader.ReadToEnd();
                    Console.WriteLine(responseFromServer);
                }
            }
        }
        return responseFromServer;

    }
}