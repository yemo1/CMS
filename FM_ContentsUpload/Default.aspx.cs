using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Globalization;
using System.Data;
using System.Data.SqlClient;
using FM_ContentsUpload.Classes;
using System.Configuration;
using System.Web.Configuration;
using System.IO;
//using System.Windows.Forms;
using Ionic.Zip;
using System.Text.RegularExpressions;
using System.Security.AccessControl;

namespace FM_ContentsUpload
{
    public partial class _Default : System.Web.UI.Page
    {
        protected string valConnection = "Data Source=.;Initial Catalog=Valentine;Integrated Security=True;";
        protected string subConnection = WebConfigurationManager.ConnectionStrings["sub"].ConnectionString;
        protected string subsConnection = WebConfigurationManager.ConnectionStrings["subs"].ConnectionString;
        protected string smsConnection = WebConfigurationManager.ConnectionStrings["sms"].ConnectionString;
        protected string connectionstring;
        protected string myConnection;
        double maxFileSize = Math.Round(368640 * 1024.0, 1);
        string serviceName = string.Empty;
        string sqlServices = "Select ID,Category from S_MTNPlay_Cat where status='true' order by category";
        private string destinationPath, folderPath, sql, duplicate, tableName, service, category, strExcelConn,extension,dbname,tbname;

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            BusinessLayer.InsertShortzContent(txtMessage.Text.Trim(), subsConnection, ddlServices);
        }

        private string webroot = @"c:\inetpub\wwwroot\mobile\";
        int iStartCount = 0;
        int iEndCount = 0;
        //private static string filePath = @"~/Uploads";

        protected void Page_Load(object sender, EventArgs e)
        {

            //Response.ClearHeaders();
            //Response.Cache.SetCacheability(HttpCacheability.NoCache);
            //Response.Cache.SetExpires(DateTime.UtcNow.AddHours(-1));
            //Response.Cache.SetNoStore();
            //Response.AppendHeader("Pragma", "no-cache");
            success.Visible = false;
            
           
            //Response.AddHeader("Cache-Control", "no-cache, no-store, max-age=0, must-revalidate");
            if (!Page.IsPostBack)
            {
                //success.Visible = false;
                type.Visible = false;
                Shortz.Visible = false;
                Bulk.Visible = false;
                ddlPlatform.DataSource = BusinessLayer.GetPlatform();
                ddlPlatform.DataBind();
            }
        }

        protected void ddlPlatform_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlPlatform.SelectedIndex == 1)
            {
                ddlType.Items.Clear();
                ddlType.Items.Insert(0, "--Select content type--");
                ddlType.DataSource = BusinessLayer.GetTypes() ;
                ddlType.DataBind();
                type.Visible = true;
            }
            else
            {
                ddlServices.Items.Clear();
                ddlServices.Items.Insert(0, "--Select a service--");
                type.Visible = false;
                ddlServices.DataSource = BusinessLayer.getCategory(subsConnection, sqlServices);
                ddlServices.DataTextField = "Category";
                ddlServices.DataValueField = "ID";
                ddlServices.DataBind();
            }
        }

        protected void ddlType_SelectedIndexChanged(object sender, EventArgs e)
        {
            ddlServices.Items.Clear();
            ddlServices.Items.Insert(0, "--Select a service--");
            if (ddlType.SelectedItem.Text == "Sms")
            {
                ddlServices.DataSource = BusinessLayer.GetAllServices();
                ddlServices.DataTextField = serviceName;
                ddlServices.DataValueField = string.Empty;
                ddlServices.DataBind();
            }
            else
            {
                var services = from s in BusinessLayer.GetAllServices()
                               where s == "Christmas" || s == "Easter" || s == "Valentine" || s == "Ramadan"
                               select s;
                ddlServices.DataSource = services.ToList();
                ddlServices.DataTextField = serviceName;
                ddlServices.DataValueField = string.Empty;
                ddlServices.DataBind();

            }
        }

        protected void ddlServices_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(ddlServices.SelectedItem.Text == "Shortz")
            {
                Shortz.Visible = true;
                Bulk.Visible = false;
            }
            else
            {
                Shortz.Visible = false;
                Bulk.Visible = true;
            }
            //if (ddlPlatform.SelectedItem.Text == "Funmobile")
            //{
            //    ddlServices.DataSource = BusinessLayer.GetAllServices();
            //    ddlServices.DataTextField = serviceName;
            //    ddlServices.DataValueField = string.Empty;
            //    ddlServices.DataBind();
            //}
            //else if (ddlPlatform.SelectedItem.Text == "MTNPlay")
            //{
            //    ddlServices.DataSource = BusinessLayer.getCategory(subsConnection, sqlServices);
            //    ddlServices.DataTextField = "Category";
            //    ddlServices.DataValueField = "ID";
            //    ddlServices.DataBind();
            //}
        }

        private void successful()
        {
            lblStatus.Text = Convert.ToString(iEndCount - iStartCount) + " records were successfully uploaded";
            success.Attributes["class"] = "notification-box notification-box-success";
            hpkClose.CssClass = "notification-close notification-close-success";
            success.Visible = true;
        }

        protected void btnUpload_Click(object sender, EventArgs e)
        {
            //try
            //{
                if (fuUpload.HasFile)
                {
                    string fileName = Server.HtmlEncode(fuUpload.FileName);
                    extension = Path.GetExtension(fileName);
                    string fName = Path.GetFileNameWithoutExtension(fileName);
                    string strUploadFileName = "~/Uploads/" + DateTime.Now.ToString("dd-MM-yyyy hh.mm.ss tt") + extension;
                    string tempZipUpload = "~/Uploads/" + DateTime.Now.ToString("dd-MM-yyyy hh.mm.ss tt");
                    //try
                    //{
                    int size = fuUpload.PostedFile.ContentLength;
                    if (extension == ".xlsx")
                    {
                        strExcelConn = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + Server.MapPath(strUploadFileName) +
                            ";Extended Properties='Excel 12.0;HDR=YES;IMEX=1;';";
                    }
                    else
                    {
                        strExcelConn = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + Server.MapPath(strUploadFileName) + ";Extended Properties='Excel 8.0;HDR=YES;IMEX=1;';";
                    }
                    if (size < maxFileSize)
                    {
                        if ((ddlPlatform.SelectedItem.Text == "Funmobile" && ddlType.SelectedItem.Text == "Sms") || (ddlPlatform.SelectedItem.Text == "MTNPlay"))
                        {
                            if ((extension == ".xls") || (extension == ".xlsx"))
                            {
                                // Save the Excel spreadsheet on server.
                                fuUpload.SaveAs(Server.MapPath(strUploadFileName));

                                // Generate the connection string for Excel file.
                                //string strExcelConn = "";

                                //HDR="YES" to prevent column headers from being inserted into the DB.
                                service = ddlServices.SelectedItem.Text;
                                dbname = BusinessLayer.GetDatabaseName(service);
                                tbname = BusinessLayer.GetTableName(service);
                                DataTable excelData; //= BusinessLayer.RetrieveData(strExcelConn, "select * from [Stress$]");
                                //DataTable excelMData; //= BusinessLayer.RetrieveData(strExcelConn, "Select Contents from [S_MTNPlay$] ");
                                //int iStartCount = GetRowCounts();
                                if (ddlPlatform.SelectedItem.Text == "Funmobile")
                                {
                                    //if (ddlType.SelectedItem.Text == "Sms")
                                    //{
                                    excelData = BusinessLayer.RetrieveData(strExcelConn, "select * from [" + service + "$] ");

                                    ////foreach(string s in BusinessLayer.GetAllServices())
                                    switch (dbname)
                                    {
                                        case "Subscription":
                                            connectionstring = BusinessLayer.subConnection;
                                            break;
                                        case "Subscriptions":
                                            connectionstring = BusinessLayer.subsConnection;
                                            break;
                                        case "MMXSPSMS":
                                            connectionstring = BusinessLayer.smsConnection;
                                            break;
                                    }

                                    iStartCount = BusinessLayer.GetRowCounts();
                                    BusinessLayer.insertContents(excelData, connectionstring, tbname);
                                    iEndCount = BusinessLayer.GetRowCounts();
                                    if (iEndCount > iStartCount)
                                    {
                                        successful();
                                    }
                                    else
                                    {
                                        lblStatus.Text = "No records were uploaded, confirm that you are targeting the right database";
                                        success.Attributes["class"] = "notification-box notification-box-warning";
                                        hpkClose.CssClass = "notification-close notification-close-warning";
                                        success.Visible = true;
                                    }
                                    File.Delete(Server.MapPath(strUploadFileName));

                                }
                                else if (ddlPlatform.SelectedItem.Text == "MTNPlay")
                                {
                                    if (ddlServices.SelectedItem.Text != "Names of Allah")
                                    {
                                        excelData = BusinessLayer.RetrieveData(strExcelConn, "Select Contents from [MTNPLAY$]");
                                        iStartCount = BusinessLayer.GetRowCounts();
                                        BusinessLayer.insertMTNPlay(excelData, subsConnection, ddlServices);
                                        iEndCount = BusinessLayer.GetRowCounts();
                                        successful();
                                    }
                                    else
                                    {
                                        excelData = BusinessLayer.RetrieveData(strExcelConn, "Select * from [Names$]");
                                        iStartCount = BusinessLayer.GetRowCounts();
                                        BusinessLayer.insertContents(excelData, subsConnection, "r_names");
                                        iEndCount = BusinessLayer.GetRowCounts();
                                        successful();
                                    }

                                    File.Delete(Server.MapPath(strUploadFileName));
                                }
                            }
                            else
                            {
                                lblStatus.Text = "You can only upload files of type xls  and xlsx";
                                success.Attributes["class"] = "notification-box notification-box-error";
                                hpkClose.CssClass = "notification-close notification-close-error";
                                success.Visible = true;
                            }
                        }
                        else
                        {
                            if (extension == ".zip")
                            {
                                HttpContext.Current.Server.ScriptTimeout = 3600;
                                fuUpload.SaveAs(Server.MapPath(strUploadFileName));
                                //DirectorySecurity securityRules = new DirectorySecurity();
                                //securityRules.AddAccessRule(new FileSystemAccessRule(@"IIS_IUSRS", FileSystemRights.Delete, AccessControlType.Allow));
                                //Directory.CreateDirectory(Server.MapPath(tempZipUpload));
                                using (ZipFile zip = ZipFile.Read(System.Web.HttpContext.Current.Server.MapPath(strUploadFileName)))
                                {
                                    foreach (ZipEntry z in zip)
                                    {
                                        //zipName = Path.GetFileNameWithoutExtension(z.FileName);
                                        string extensionz = Path.GetExtension(z.FileName);
                                        if (ddlType.SelectedItem.Text == "Fulltracks")
                                        {
                                            service = ddlServices.SelectedItem.Text;
                                            if (Regex.IsMatch(extensionz, ".mp3", RegexOptions.IgnoreCase))
                                            {
                                                category = "Fulltrack";
                                                switch (service)
                                                {
                                                    case "Valentine":
                                                        destinationPath = webroot + WebConfigurationManager.AppSettings["valFulltrack"];
                                                        z.Extract(destinationPath, ExtractExistingFileAction.OverwriteSilently);
                                                        folderPath = WebConfigurationManager.AppSettings["valFulltrack"];
                                                        tableName = "val_fulltrack";
                                                        sql = BusinessLayer.sqlQuery(tableName);
                                                        duplicate = BusinessLayer.duplicateQuery(tableName);
                                                        break;
                                                    case "Christmas":
                                                        destinationPath = webroot + WebConfigurationManager.AppSettings["xmasFulltrack"];
                                                        z.Extract(destinationPath, ExtractExistingFileAction.OverwriteSilently);
                                                        folderPath = WebConfigurationManager.AppSettings["xmasFulltrack"];
                                                        tableName = "xmas_fulltrack";
                                                        sql = BusinessLayer.sqlQuery(tableName);
                                                        duplicate = BusinessLayer.duplicateQuery(tableName);
                                                        break;
                                                    case "Easter":
                                                        destinationPath = webroot + WebConfigurationManager.AppSettings["easterFulltrack"];
                                                        z.Extract(destinationPath, ExtractExistingFileAction.OverwriteSilently);
                                                        folderPath = WebConfigurationManager.AppSettings["easterFulltrack"];
                                                        tableName = "easter_fulltrack";
                                                        sql = BusinessLayer.sqlQuery(tableName);
                                                        duplicate = BusinessLayer.duplicateQuery(tableName);
                                                        break;
                                                }
                                            }
                                            else
                                            {
                                                lblStatus.Text = "One of the files contain illegal format";
                                                success.Attributes["class"] = "notification-box notification-box-error";
                                                hpkClose.CssClass = "notification-close notification-close-error";
                                                success.Visible = true;
                                            }
                                        }
                                        else if (ddlType.SelectedItem.Text == "Truetones")
                                        {
                                            service = ddlServices.SelectedItem.Text;
                                            if (Regex.IsMatch(extensionz, ".mp3", RegexOptions.IgnoreCase))
                                            {
                                                category = "Ringtone";
                                                switch (service)
                                                {
                                                    case "Valentine":
                                                        destinationPath = webroot + WebConfigurationManager.AppSettings["valRingtone"];
                                                        z.Extract(destinationPath, ExtractExistingFileAction.OverwriteSilently);
                                                        folderPath = WebConfigurationManager.AppSettings["valRingtone"];
                                                        tableName = "val_ringtone";
                                                        sql = BusinessLayer.sqlQuery(tableName);
                                                        duplicate = BusinessLayer.duplicateQuery(tableName);
                                                        break;
                                                    case "Christmas":
                                                        destinationPath = webroot + WebConfigurationManager.AppSettings["xmasrRingtone"];
                                                        z.Extract(destinationPath, ExtractExistingFileAction.OverwriteSilently);
                                                        folderPath = WebConfigurationManager.AppSettings["xmasRingtone"];
                                                        tableName = "xmas_ringtone";
                                                        sql = BusinessLayer.sqlQuery(tableName);
                                                        duplicate = BusinessLayer.duplicateQuery(tableName);
                                                        break;
                                                    case "Easter":
                                                        destinationPath = webroot + WebConfigurationManager.AppSettings["easterRingtone"];
                                                        z.Extract(destinationPath, ExtractExistingFileAction.OverwriteSilently);
                                                        folderPath = WebConfigurationManager.AppSettings["easterRingtone"];
                                                        tableName = "easter_ringtone";
                                                        sql = BusinessLayer.sqlQuery(tableName);
                                                        duplicate = BusinessLayer.duplicateQuery(tableName);
                                                        break;
                                                }
                                            }
                                            else
                                            {
                                                lblStatus.Text = "One of the files contain illegal format";
                                                success.Attributes["class"] = "notification-box notification-box-error";
                                                hpkClose.CssClass = "notification-close notification-close-error";
                                                success.Visible = true;
                                            }
                                        }
                                        else if (ddlType.SelectedItem.Text == "Wallpapers")
                                        {
                                            service = ddlServices.SelectedItem.Text;
                                            if ((Regex.IsMatch(extensionz, ".gif", RegexOptions.IgnoreCase) || (Regex.IsMatch(extensionz, ".jpg", RegexOptions.IgnoreCase))))
                                            {
                                                category = "WallPaper";
                                                switch (service)
                                                {
                                                    case "Valentine":
                                                        destinationPath = webroot + WebConfigurationManager.AppSettings["valWallpaper"];
                                                        z.Extract(destinationPath, ExtractExistingFileAction.OverwriteSilently);
                                                        folderPath = WebConfigurationManager.AppSettings["valWallpaper"];
                                                        tableName = "val_wallpaper";
                                                        sql = BusinessLayer.sqlQuery(tableName);
                                                        duplicate = BusinessLayer.duplicateQuery(tableName);
                                                        break;
                                                    case "Christmas":
                                                        destinationPath = webroot + WebConfigurationManager.AppSettings["xmasWallpaper"];
                                                        z.Extract(destinationPath, ExtractExistingFileAction.OverwriteSilently);
                                                        folderPath = WebConfigurationManager.AppSettings["xmasWallpaper"];
                                                        tableName = "Xmas_wallpaper";
                                                        sql = BusinessLayer.sqlQuery(tableName);
                                                        duplicate = BusinessLayer.duplicateQuery(tableName);
                                                        break;
                                                    case "Easter":
                                                        destinationPath = webroot + WebConfigurationManager.AppSettings["easterWallpaper"];
                                                        z.Extract(destinationPath, ExtractExistingFileAction.OverwriteSilently);
                                                        folderPath = WebConfigurationManager.AppSettings["easterWallpaper"];
                                                        tableName = "easter_wallpaper";
                                                        sql = BusinessLayer.sqlQuery(tableName);
                                                        duplicate = BusinessLayer.duplicateQuery(tableName);
                                                        break;
                                                    case "Ramadan":
                                                        destinationPath = webroot + WebConfigurationManager.AppSettings["RamadanPapers"];
                                                        z.Extract(destinationPath, ExtractExistingFileAction.OverwriteSilently);
                                                        folderPath = WebConfigurationManager.AppSettings["RamadanPapers"];
                                                        tableName = "ramadan_wallpaper";
                                                        sql = BusinessLayer.sqlQuery(tableName);
                                                        duplicate = BusinessLayer.duplicateQuery(tableName);
                                                        break;
                                                }
                                            }
                                            else
                                            {
                                                lblStatus.Text = "One of the files contain illegal format";
                                                success.Attributes["class"] = "notification-box notification-box-error";
                                                hpkClose.CssClass = "notification-close notification-close-error";
                                                success.Visible = true;
                                            }
                                        }
                                        else if (ddlType.SelectedItem.Text == "Videos")
                                        {
                                            if ((extensionz == ".mp4") || (extensionz == ".3gp"))
                                            {
                                                category = "Video";
                                                switch (service)
                                                {
                                                    case "Valentine":
                                                        destinationPath = webroot + WebConfigurationManager.AppSettings["valVideo"];
                                                        z.Extract(destinationPath, ExtractExistingFileAction.OverwriteSilently);
                                                        folderPath = WebConfigurationManager.AppSettings["valVideo"];
                                                        tableName = "val_video";
                                                        sql = BusinessLayer.sqlQuery(tableName);
                                                        duplicate = BusinessLayer.duplicateQuery(tableName);
                                                        break;
                                                    case "Christmas":
                                                        destinationPath = webroot + WebConfigurationManager.AppSettings["xmasVideo"];
                                                        z.Extract(destinationPath, ExtractExistingFileAction.OverwriteSilently);
                                                        folderPath = WebConfigurationManager.AppSettings["xmasVideo"];
                                                        tableName = "Xmas_video";
                                                        sql = BusinessLayer.sqlQuery(tableName);
                                                        duplicate = BusinessLayer.duplicateQuery(tableName);
                                                        break;
                                                    case "Easter":
                                                        destinationPath = webroot + WebConfigurationManager.AppSettings["easterVideo"];
                                                        z.Extract(destinationPath, ExtractExistingFileAction.OverwriteSilently);
                                                        folderPath = WebConfigurationManager.AppSettings["easterVideo"];
                                                        tableName = "easter_video";
                                                        sql = BusinessLayer.sqlQuery(tableName);
                                                        duplicate = BusinessLayer.duplicateQuery(tableName);
                                                        break;
                                                }
                                            }
                                            else
                                            {
                                                lblStatus.Text = "One of the files contain illegal format";
                                                success.Attributes["class"] = "notification-box notification-box-error";
                                                hpkClose.CssClass = "notification-close notification-close-error";
                                                success.Visible = true;
                                            }
                                        }
                                    }
                                    BusinessLayer.getFiles(destinationPath, category, sql, folderPath);
                                    BusinessLayer.remDuplicate(duplicate);
                                    lblStatus.Text = "Content upload successful.";
                                    success.Attributes["class"] = "notification-box notification-box-success";
                                    hpkClose.CssClass = "notification-close notification-close-success";
                                    success.Visible = true;
                                }
                                File.Delete(Server.MapPath(strUploadFileName));
                                //File.Delete(Server.MapPath(tempZipUpload));
                            }
                            else
                            {
                                lblStatus.Text = "You can only upload files of type zip";
                                success.Attributes["class"] = "notification-box notification-box-error";
                                hpkClose.CssClass = "notification-close notification-close-error";
                                success.Visible = true;
                            }
                        }
                    }
                    else
                    {
                        lblStatus.Text = "File size cannot exceed 20MB.";
                        success.Attributes["class"] = "notification-box notification-box-error";
                        hpkClose.CssClass = "notification-close notification-close-error";
                        success.Visible = true;
                    }
                    //}
                    //catch (Exception ex)
                    //{
                    //    lblStatus.Text = ex.Message.ToString();
                    //    success.Attributes["class"] = "notification-box notification-box-error";
                    //    hpkClose.CssClass = "notification-close notification-close-error";
                    //    success.Visible = true;
                    //    if (File.Exists(strUploadFileName))
                    //    {
                    //        File.Delete(Server.MapPath(strUploadFileName));
                    //    }  
                    //}
                }
                else
                {
                    lblStatus.Text = "Select a file to upload.";
                    success.Attributes["class"] = "notification-box notification-box-error";
                    hpkClose.CssClass = "notification-close notification-close-error";
                    success.Visible = true;
                }
            //}
            //catch (Exception ex)
            //{
            //    lblStatus.Text = ex.Message;
            //    success.Attributes["class"] = "notification-box notification-box-error";
            //    hpkClose.CssClass = "notification-close notification-close-error";
            //    success.Visible = true;
            //}
        }
    }
}
