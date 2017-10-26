using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using FM_ContentsUpload.Classes;
using System.IO;
using System.Data;

namespace FM_ContentsUpload
{
    public partial class DataService : System.Web.UI.Page
    {
        private string sqlService = "Select ID,Category from MTN_DataService_CAT order by Category";
        double maxFileSize = Math.Round(368640 * 1024.0, 1);
        string serviceName = string.Empty;
        private string strExcelConn, extension;
        int iStartCount = 0;
        int iEndCount = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            success.Visible = false;
            if (!Page.IsPostBack)
            {
                ddlService.Items.Insert(0, "--Select a service--");
                ddlService.DataSource = BusinessLayer.getService(sqlService);
                ddlService.DataValueField = "Id";
                ddlService.DataTextField = "Category";
                ddlService.DataBind();
            }
        }

        private void successful()
        {
            lblStatus.Text = Convert.ToString(iEndCount - iStartCount) + " records were successfully uploaded";
            success.Attributes["class"] = "notification-box notification-box-success";
            hpkClose.CssClass = "notification-close notification-close-success";
            ddlService.SelectedIndex = 0;
            success.Visible = true;
        }
        protected void btnUpload_Click(object sender, EventArgs e)
        {
            //string title = txtTitle.Text.Trim();
            //string link = txtLink.Text.Trim();
            //string img = txtImage.Text.Trim();
            //string desc = CKEditor1.Text.Trim() ;
            //int ServiceId = Convert.ToInt32(ddlService.SelectedValue);
            //string query = "INSERT INTO MTN_DataService (CategoryID,Title,Link,Image,Description)VALUES(@serviceid,@title,@link,@image,@desc)";
            //BusinessLayer.insertData(query,ServiceId,title, link, img, desc);
            //lblStatus.Text = "Your contents have been saved to the database";
            //success.Visible = true;
            //clear();
            try
            {
                if (fuUpload.HasFile)
                {
                    string fileName = Server.HtmlEncode(fuUpload.FileName);
                    extension = Path.GetExtension(fileName);
                    string fName = Path.GetFileNameWithoutExtension(fileName);
                    string strUploadFileName = "~/Uploads/" + DateTime.Now.ToString("dd-MM-yyyy hh.mm.ss tt") + extension;

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
                        if ((extension == ".xls") || (extension == ".xlsx"))
                        {
                            // Save the Excel spreadsheet on server.
                            fuUpload.SaveAs(Server.MapPath(strUploadFileName));
                            DataTable excelData;
                            excelData = BusinessLayer.RetrieveData(strExcelConn, "Select [Title],[Link],[Image],[Description] from [DataService$]");
                            iStartCount = BusinessLayer.GetRowCounts();
                            BusinessLayer.insertDataFeeds(excelData, BusinessLayer.dataConnection, ddlService);
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
                    }
                    else
                    {
                        lblStatus.Text = "File size cannot exceed 20MB.";
                        success.Attributes["class"] = "notification-box notification-box-error";
                        hpkClose.CssClass = "notification-close notification-close-error";
                        success.Visible = true;
                    }

                }
            }
            catch (Exception ex)
            {
                lblStatus.Text = ex.ToString();
                success.Attributes["class"] = "notification-box notification-box-error";
                hpkClose.CssClass = "notification-close notification-close-error";
                success.Visible = true;
            }
            
        }

        //private void clear()
        //{
        //    txtTitle.Text = string.Empty;
        //    txtLink.Text = string.Empty;
        //    txtImage.Text = string.Empty;
        //    ddlService.SelectedIndex = 0;
        //    CKEditor1.Text = string.Empty;
        //}
    }
}