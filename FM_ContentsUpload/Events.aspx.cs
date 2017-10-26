using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;

namespace FM_ContentsUpload
{
    public partial class Events : System.Web.UI.Page
    {
        protected string subsConnection = WebConfigurationManager.ConnectionStrings["subs"].ConnectionString;
        protected string query = "INSERT INTO w_predictor(TeamA,TeamB,EventCode,KickoffTime)VALUES(@teamA,@teamB,@code,@time)";
        protected void Page_Load(object sender, EventArgs e)
        {
            success.Visible = false;
            validDate.ValueToCompare = DateTime.Now.ToShortDateString();
        }
        private void successful()
        {
            lblStatus.Text = "Event created successfully";
            success.Attributes["class"] = "notification-box notification-box-success";
            hpkClose.CssClass = "notification-close notification-close-success";
            success.Visible = true;
        }
        protected void btnCreate_Click(object sender, EventArgs e)
        {
            string teamA  =txtTeamA.Text.Trim();
            string teamB = txtTeamB.Text.Trim();
            string code = txtCode.Text.Trim();
            string time = txtDate.Text.Trim();
            DateTime dt; //= Convert.ToDateTime(txtDate.Text);
            try
            {
                using (SqlConnection conn = new SqlConnection(subsConnection))
                {
                    if(DateTime.TryParse(time,out dt))
                    {
                        conn.Open();
                        SqlCommand cmd = new SqlCommand(query, conn);
                        cmd.Parameters.AddWithValue("@teamA", teamA);
                        cmd.Parameters.AddWithValue("@teamB", teamB);
                        cmd.Parameters.AddWithValue("@code", code);
                        cmd.Parameters.AddWithValue("@time", dt);
                        cmd.ExecuteNonQuery();
                        successful();
                    }
                    else
                    {
                        lblStatus.Text = "Invalid kickoff time";
                        success.Attributes["class"] = "notification-box notification-box-error";
                        hpkClose.CssClass = "notification-close notification-close-error";
                        success.Visible = true;
                    }
                }
            }
            catch(Exception ex)
            {
                lblStatus.Text = ex.Message;
                success.Attributes["class"] = "notification-box notification-box-error";
                hpkClose.CssClass = "notification-close notification-close-error";
                success.Visible = true;
            } 
        }
    }
}