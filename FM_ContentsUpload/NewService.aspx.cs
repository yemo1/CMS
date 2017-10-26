using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using FM_ContentsUpload.Classes;
using System.Web.Configuration;

namespace FM_ContentsUpload
{
    public partial class NewService : System.Web.UI.Page
    {
        protected string subsConnection = WebConfigurationManager.ConnectionStrings["subs"].ConnectionString;
        protected void Page_Load(object sender, EventArgs e)
        {
            success.Visible = false;
        }

        private void successful()
        {
            lblStatus.Text = "Service created successfully";
            success.Attributes["class"] = "notification-box notification-box-success";
            hpkClose.CssClass = "notification-close notification-close-success";
            success.Visible = true;
        }

        protected void btnCreate_Click(object sender, EventArgs e)
        {
            try
            {
                string name = txtService.Text.Trim();
                BusinessLayer.AddMtnPlayService(subsConnection, name);
                successful();
                txtService.Text = string.Empty;
            }
            catch (Exception ex)
            {
                lblStatus.Text = ex.Message;
                success.Attributes["class"] = "notification-box notification-box-error";
                hpkClose.CssClass = "notification-close notification-close-error";
                success.Visible = true;
            }
        }
    }
}