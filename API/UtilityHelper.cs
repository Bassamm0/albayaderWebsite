using System;
using System.IO;
using System.Web;


using System.Data.Common;

using System.Net.Mail;

using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Linq;
using System.Net;
using System.Collections;
using Microsoft.Web.Helpers;



namespace API
{
    public static class UtilityHelper
    {


        public static string ChangeURL(string sUrl)
        {
            string sReturn = string.Empty;

            if (sUrl.IndexOf("?") > -1)
            {
                sReturn = sUrl.Substring(0, sUrl.IndexOf("?") - 1);
            }



            return sReturn;
        }



        public static string changeFilename(string fileName)
        {
           
            string filename = Path.GetFileNameWithoutExtension(fileName);
            if (filename != "")
            {
                string strExtn = Path.GetExtension(fileName);
                filename = AppendDateTime(filename, strExtn);
                
            }
            return filename;
        }



        //public static string GetFileUpload(FileUpload txtUploadCtrl)
        //{
        //    Page srv = new Page();
        //    string filename = Path.GetFileNameWithoutExtension(txtUploadCtrl.FileName);
        //    if (filename != "")
        //    {
        //        string strExtn = Path.GetExtension(txtUploadCtrl.FileName);
        //        filename = AppendDateTime(filename, strExtn);
        //        try
        //        {
        //            txtUploadCtrl.SaveAs(srv.Server.MapPath(@"~\Uploads\") + filename);
        //        }
        //        catch (Exception ex)
        //        {
        //            throw ex;
        //        }
        //    }
        //    return filename;
        //}


       

       


        public static string AppendDateTime(string filename, string fileExt)
        {
            filename = filename + "_" + DateTime.Now.ToString("yyyyMMddhhmmssfffffff") + fileExt;
            return filename;
        }

        public static string RestoreOriginalFileName(string sFileName)
        {
            if (sFileName == string.Empty || sFileName == null)
                return "";

            string sOrgName = string.Empty;
            if (sFileName.IndexOf("_") > -1)
            {
                sOrgName = sFileName.Substring(0, sFileName.LastIndexOf('_'));
                sOrgName += sFileName.Substring(sFileName.LastIndexOf('.'));

            }

            return sOrgName;
        }

       

       

        //public static bool SendEmail(string from, string to, string subject, string body)
        //{
        //    bool IsMsgSent = false;
        //    string smtpServer = WebConfigurationManager.AppSettings["smtpclient"];
        //    string pwd = WebConfigurationManager.AppSettings["pwd"];
        //    SmtpClient client = new SmtpClient(smtpServer);
        //    client.UseDefaultCredentials = false;
        //    client.Credentials = new System.Net.NetworkCredential(from, pwd);
        //    MailMessage message = new MailMessage(from, to, subject, body);
        //    message.IsBodyHtml = true;
        //    try
        //    {
        //        client.Send(message);
        //    }
        //    catch
        //    {

        //    }

        //    IsMsgSent = true;
        //    return IsMsgSent;
        //}


       

     



        public static string Between(string src, string findfrom, string findto)
        {
            int start = src.IndexOf(findfrom);
            int to = src.IndexOf(findto, start + findfrom.Length);
            if (start < 0 || to < 0) return "";
            string s = src.Substring(
                           start + findfrom.Length,
                           to - start - findfrom.Length);
            return s;
        }


        public static string StripNumbers(string input)
        {
            Regex regEx = new Regex("[0-9]+");
            StringBuilder sb = new StringBuilder();
            foreach (char a in input)
            {
                if (!regEx.IsMatch(a.ToString()))
                {
                    sb.Append(a);
                }
            }

            return sb.ToString();
        }


        private static string StripEndTags(string item)
        {

            // try to find a tag at the end of the line using EndsWith
            if (item.Trim().EndsWith(">"))
            {

                // now search for the opening tag...
                int lastLocation = item.LastIndexOf("</");

                // remove the identified section, if it is a valid region
                if (lastLocation >= 0)
                    item = item.Substring(0, lastLocation);
            }

            return item;
        }


        // time lapse functions

        public static string CreatFolder(string Path, string Name)

        {

            string pathString = Path + Name;

            if (Directory.Exists(pathString))
            {
                DirectoryInfo di = new DirectoryInfo(pathString);
                Empty(di);
            }

            Directory.CreateDirectory(pathString);
            return pathString;
        }

        public static void Empty(this System.IO.DirectoryInfo directory)
        {
            foreach (System.IO.FileInfo file in directory.GetFiles()) file.Delete();
            foreach (System.IO.DirectoryInfo subDirectory in directory.GetDirectories()) subDirectory.Delete(true);
        }
     

        public static bool deleteFile(string physicalWebRootPath, string fileName)
        {
           
            string rootFolder = Path.Combine(physicalWebRootPath, "Uploads");
            try
            {
                // Check if file exists with its full path    
                if (File.Exists(Path.Combine(rootFolder, fileName)))
                {
                    // If file found, delete it    
                    File.Delete(Path.Combine(rootFolder, fileName));
                    return true;
                    Console.WriteLine("File deleted.");
                }
                else {
                    return true;
                }
            }
            catch (IOException ioExp)
            {
                Console.WriteLine(ioExp.Message);
                return false;
            }

        }


        //public static void openLogTextFile(string message)
        //{
        //    Page srv = new Page();
        //    string Apppath = srv.Server.MapPath(@"~\logs\");
        //    string fileName = "log";

        //    fileName = "log.txt";

        //    using (StreamWriter w = File.AppendText(Apppath + fileName))
        //    {
        //        string logMessage = "Date:" + DateTime.Now.ToString() + "\r\n Error Message: " + message;
        //        Log(logMessage, w);
        //    }
        //}


        // time lapse logo
        public static void Log(string logMessage, TextWriter w)
        {


            w.Write("\r\nLog Entry : ");
            w.WriteLine("{0} {1}", DateTime.Now.ToLongTimeString(),
                DateTime.Now.ToLongDateString());
            w.WriteLine("  :{0}", logMessage);
            w.WriteLine("-------------------------------");
        }


       

        public static string SaveStreamTpServerFTP(string source, string folderName, string fileName)
        {
            String ftpurl = "FTPUrl" + "/" + folderName; // e.g. ftp://serverip/foldername/foldername
            String ftpusername = ""; // e.g. username
            String ftppassword = ""; // e.g. password
            try
            {

                using (WebClient client = new WebClient())
                {
                    client.Headers["User-Agent"] =
                    "Mozilla/4.0 (Compatible; Windows NT 5.1; MSIE 6.0) " +
                    "(compatible; MSIE 6.0; Windows NT 5.1; " +
                    ".NET CLR 1.1.4322; .NET CLR 2.0.50727)";

                    // Download data.
                    byte[] arr = client.DownloadData(source);


                    FtpWebRequest request = (FtpWebRequest)WebRequest.Create(ftpurl + "/" + fileName);
                    request.Method = WebRequestMethods.Ftp.UploadFile;
                    // This example assumes the FTP site uses anonymous logon.
                    request.Credentials = new NetworkCredential(ftpusername, ftppassword);
                    // Copy the contents of the file to the request stream.
                    MemoryStream storeStream = new MemoryStream();
                    // byte[] buffer = new byte[storeStream.Length];
                    storeStream.Read(arr, 0, arr.Length);
                    //byte[] fileContents = Encoding.UTF8.GetBytes(sourceStream.ReadToEnd());
                    storeStream.Close();
                    using (Stream reqStream = request.GetRequestStream())
                    {
                        reqStream.Write(arr, 0, arr.Length);
                    }
                    //Gets the FtpWebResponse of the uploading operation
                    FtpWebResponse response = (FtpWebResponse)request.GetResponse();
                    Console.WriteLine(response.StatusDescription); //Display response


                    response.Close();

                }
            }
            catch (Exception e)
            {

            }

            return "true";
        }

        public static bool CreateDirIfNotExist(String ftpurl, string DirName)
        {
            try
            {
                FtpWebRequest request = (FtpWebRequest)WebRequest.Create(ftpurl + "/" + DirName);
                request.Method = WebRequestMethods.Ftp.MakeDirectory;
                request.Credentials = new NetworkCredential("userName", "password");
                using (var resp = (FtpWebResponse)request.GetResponse())
                {
                    Console.WriteLine(resp.StatusCode);
                }
            }
            catch (WebException ex)
            {
                if (ex.Response != null)
                {

                }
            }
            return true;
        }

        public static bool directoryExists(String host, string dirName)
        {


            FtpWebResponse response = null;
            Stream ftpStream = null;
            try
            {
                string[] Files = GetFileList(host);
                ArrayList arrDirectories = new ArrayList();
                if (Files != null)
                {
                    foreach (string dir in Files)
                    {
                        arrDirectories.Add(dir);
                    }
                }
                if (!arrDirectories.Contains(dirName))
                {
                    //// dirName = name of the directory to create.
                    //reqFTP = (FtpWebRequest)FtpWebRequest.Create(new Uri(host + "/" + dirName));
                    //reqFTP.Method = WebRequestMethods.Ftp.MakeDirectory;
                    //reqFTP.UseBinary = true;
                    //reqFTP.KeepAlive = false;
                    //reqFTP.Proxy = null;
                    //reqFTP.Credentials = new NetworkCredential(userName, password);
                    //response = (FtpWebResponse)reqFTP.GetResponse();
                    //ftpStream = response.GetResponseStream();
                    return false;

                }
            }
            catch (Exception ex)
            {
                if (ftpStream != null)
                {
                    ftpStream.Close();
                }
                if (response != null)
                {
                    response.Close();
                }
                return false;

            }
            return true;
        }


        public static string[] GetFileList(String host)
        {

            string[] downloadFiles;
            StringBuilder result = new StringBuilder();
            WebResponse response = null;
            StreamReader reader = null;
            try
            {
                FtpWebRequest reqFTP;
                reqFTP = (FtpWebRequest)FtpWebRequest.Create(new Uri(host + "/"));
                reqFTP.UseBinary = true;
                reqFTP.Credentials = new NetworkCredential("userName", "password");
                reqFTP.Method = WebRequestMethods.Ftp.ListDirectory;
                reqFTP.Proxy = null;
                reqFTP.KeepAlive = false;
                reqFTP.UsePassive = false;
                response = reqFTP.GetResponse();
                reader = new StreamReader(response.GetResponseStream());
                string line = reader.ReadLine();
                while (line != null)
                {
                    result.Append(line);
                    result.Append("\n");
                    line = reader.ReadLine();
                }
                // to remove the trailing '\n'
                result.Remove(result.ToString().LastIndexOf('\n'), 1);
                return result.ToString().Split('\n');
            }
            catch (Exception ex)
            {
                if (reader != null)
                {
                    reader.Close();
                }
                if (response != null)
                {
                    response.Close();
                }
                downloadFiles = null;
                return downloadFiles;
            }
        }
        //public static string StreamAndView(string source)
        //{

        //    string base64String = "";
        //    string ReturnMessage = "true";
        //    try
        //    {

        //        using (WebClient client = new WebClient())
        //        {
        //            client.Headers["User-Agent"] =
        //            "Mozilla/4.0 (Compatible; Windows NT 5.1; MSIE 6.0) " +
        //            "(compatible; MSIE 6.0; Windows NT 5.1; " +
        //            ".NET CLR 1.1.4322; .NET CLR 2.0.50727)";
        //            // Download data.
        //            byte[] arr = client.DownloadData(source);
        //            MemoryStream tiffStream = new MemoryStream(arr);
        //            MemoryStream outStream = new MemoryStream();
        //            Bitmap newImage = new Bitmap(Bitmap.FromStream(tiffStream));
        //            try
        //            {
        //                // Get an ImageCodecInfo object that represents the JPEG codec.
        //                newImage.Save(outStream, ImageFormat.Jpeg);
        //                Byte[] bytes = new Byte[outStream.Length];
        //                outStream.Position = 0;
        //                outStream.Read(bytes, 0, (int)bytes.Length);
        //                base64String = Convert.ToBase64String(arr, 0, arr.Length);
        //                newImage.Dispose();
        //            }
        //            catch (Exception e)
        //            {
        //                ReturnMessage = e.Message;
        //            }
        //            // return "data:image/Jpeg;base64," + base64String;
        //        }
        //    }
        //    catch (Exception e)
        //    {

        //    }
        //    return "data:image/Jpeg;base64," + base64String;
        //}


        //public static bool deleteFileFTP(string fileName)
        //{
        //    try
        //    {
        //        FtpWebRequest requestFileDelete = (FtpWebRequest)WebRequest.Create(FTPUrl + "/" + fileName);
        //        requestFileDelete.Credentials = new NetworkCredential(userName, password);
        //        requestFileDelete.Method = WebRequestMethods.Ftp.DeleteFile;

        //        FtpWebResponse responseFileDelete = (FtpWebResponse)requestFileDelete.GetResponse();

        //    }
        //    catch (Exception e)
        //    {
        //        return false;
        //    }
        //    return true;
        //}

        //public static bool downloadFileFTP(string fileName)
        //{
        //    string serverUri = FTPUrl + "/" + fileName;
        //    // Get the object used to communicate with the server.
        //    WebClient request = new WebClient();

        //    // This example assumes the FTP site uses anonymous logon.
        //    request.Credentials = new NetworkCredential(userName, password);
        //    try
        //    {
        //        byte[] newFileData = request.DownloadData(serverUri.ToString());
        //        string fileString = System.Text.Encoding.UTF8.GetString(newFileData);
        //        Console.WriteLine(fileString);
        //    }
        //    catch (WebException e)
        //    {
        //        Console.WriteLine(e.ToString());
        //    }
        //    return true;
        //}

        public static string convertToTime(string time)
        {
            string DayTime = time.Substring(time.Length - 2);
            string HH = time.Substring(0, time.IndexOf(":"));
            string MM = time.Substring(time.IndexOf(":") + 1).Remove(2);

            if (DayTime.ToLower() == "pm")
            {
                if (Convert.ToInt32(HH) < 12)
                {
                    HH = (Convert.ToInt32(HH) + 12).ToString();
                }
                else if (Convert.ToInt32(HH) == 12)
                {
                    HH = "00";
                }

            }
            if (HH.Length == 1)
            {
                HH = "0" + HH;
            }
            string returnStr = HH + ":" + MM;
            return returnStr;
        }
        public static string convertBackTime(string time)
        {
            string returnStr = "";
            time = time.Remove(time.Length - 3);
            string HH = time.Substring(0, time.IndexOf(":"));
            string MM = time.Substring(time.IndexOf(":") + 1);

            if (Convert.ToInt32(HH) > 12)
            {
                HH = (Convert.ToInt32(HH) - 12).ToString();
                if (MM.Length == 1)
                    MM = "0" + MM;
                returnStr = HH + ":" + MM + " PM";
            }
            else
            {
                if (HH.Length == 1)
                    HH = "0" + HH;
                if (MM.Length == 1)
                    MM = "0" + MM;
                returnStr = HH + ":" + MM + " AM";

            }


            return returnStr;
        }


      
    }
}
