using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;
using FM_ContentsUpload.Classes;
using System.Data.SqlClient;

namespace FM_ContentsUpload
{
    public partial class Campaign : System.Web.UI.Page
    {
        protected string myConnection = WebConfigurationManager.ConnectionStrings["Campaign"].ConnectionString;
        protected static string sqlServices = "SELECT Id, ServiceName FROM TargetService ORDER BY ServiceName";
        protected static string sqlShortcodes = "Select ID,Name FROM shortcodes ORDER BY name";
        protected static string sqlSegment = "Select SegmentId,Name FROM Segment ORDER BY SegmentId";
        protected static string sqlStates = "Select Id,Name FROM State ORDER BY ID"; //protected int messageId;


        //protected static string sqlServices = "SELECT Id, ServiceName FROM Service ORDER BY ServiceName";
        //protected static string sqlSegment = "Select Id,Name FROM Segment ORDER BY Id";
        //protected static string sqlShortcodes = "Select ID,Name FROM shortcode ORDER BY name";
        //protected static string sqlStates = "Select Id,Name FROM Locations ORDER BY ID"; 
      
       
        protected string campaignQuery;
        protected string shortcode;
        protected int appid;
        protected int serviceId;
        protected string schedule;
        protected DateTime time;
        protected DateTime date;
        protected DateTime timeTo;
        protected int targetsize;
        protected int stateid;
        protected string message;
        protected int segmentid;
        protected int IsTarget;

        protected void Page_Load(object sender, EventArgs e)
        {
            success.Visible = false;
            
            if (!IsPostBack)
            {
                ddlShortcode.Items.Clear();
                ddlState.Items.Clear();
                ddlService.Items.Clear();
                ddlBind.Items.Clear();
                ddlSegment.Items.Clear();

                ddlService.Items.Insert(0, "--Select a service--");
                ddlService.Items.Insert(1, new ListItem("All Users", "0"));
                ddlService.DataSource=BusinessLayer.getCategory(myConnection, sqlServices);
                ddlService.DataTextField = "ServiceName";
                ddlService.DataValueField = "Id";
                ddlService.DataBind();

                ddlShortcode.Items.Insert(0, "--Select shortcode--");
                ddlShortcode.DataSource=BusinessLayer.getCategory(myConnection, sqlShortcodes);
                ddlShortcode.DataTextField = "Name";
                ddlShortcode.DataValueField = "Id";
                ddlShortcode.DataBind();

                ddlState.Items.Insert(0, "--Select a state--");
                ddlState.Items.Insert(1, new ListItem("No State", "0"));
                ddlState.DataSource=BusinessLayer.getCategory(myConnection, sqlStates);
                ddlState.DataTextField = "Name";
                ddlState.DataValueField = "Id";
                ddlState.DataBind();

                ddlSegment.Items.Insert(0, "--Select a segment--");
                ddlSegment.Items.Insert(1, new ListItem("No Segment", "0"));
                ddlSegment.DataSource=BusinessLayer.getCategory(myConnection, sqlSegment);
                ddlSegment.DataTextField = "Name";
                ddlSegment.DataValueField = "SegmentId";
                ddlSegment.DataBind();

                ddlBind.Items.Insert(0, "--Select Bind--");
                ddlBind.Items.Insert(1, new ListItem("0", "0"));
                ddlBind.Items.Insert(2, new ListItem("1", "1"));
                ddlBind.DataBind();
            }
            validDate.ValueToCompare = DateTime.Now.ToShortDateString();
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            appid = Convert.ToInt32(ddlBind.SelectedValue);
            segmentid = Convert.ToInt32(ddlSegment.SelectedValue);
            //serviceId = Convert.ToInt32(ddlService.SelectedValue);
            //if (serviceId == 0)
            //{
            //    IsTarget = 0;
            //    stateid = 0;
            //}
            //else
            //{
            //    IsTarget = 1;
            //    stateid = Convert.ToInt32(ddlState.SelectedValue);
            //}
            targetsize = Convert.ToInt32(txtSize.Text.Trim());
            date = DateTime.Parse(txtDate.Text.Trim());
            schedule = txtDate.Text.Trim() + " " + txtTime.Text.Trim();
            time = DateTime.Parse(schedule);
            timeTo = time.AddHours(3);
            message = txtMessage.Text.Trim();
            shortcode = ddlShortcode.SelectedItem.Text;

            campaignQuery = "INSERT INTO SCHEDULECAMPAIGN(TopSelect,SegmentId,StateId,ServiceId,DateToGoOut,TimeFrom,Shortcode,Message,Appid,Istarget,TimeTo)VALUES(@size,@segmentid,@stateid,@serviceid,@date,@time,@shortcode,@message,@appid,@istarget,@timeto)";

            BusinessLayer.InsertCampaign(myConnection, campaignQuery, shortcode, appid, serviceId, stateid, targetsize, segmentid, date, time, message,IsTarget,timeTo);
            lblStatus.Text = "Your campaign message has been submitted.";
            success.Attributes["class"] = "notification-box notification-box-success";
            hpkClose.CssClass = "notification-close notification-close-success";
            success.Visible = true;
            Reset();
        }


        public void Reset()
        {
            txtMessage.Text = string.Empty;
            ddlShortcode.SelectedIndex = 0;
            ddlService.SelectedIndex = 0;
            ddlSegment.SelectedIndex = 0;
            ddlBind.SelectedIndex = 0;
            ddlState.SelectedIndex = 0;
        }

        protected void ddlService_SelectedIndexChanged(object sender, EventArgs e)
        {
            serviceId = Convert.ToInt32(ddlService.SelectedValue);
            if (serviceId == 0)
            {
                ddlState.Enabled = true;
                IsTarget = 0;
                stateid = Convert.ToInt32(ddlService.SelectedValue);
                
            }
            else
            {
                ddlState.Enabled = false;
                IsTarget = 1;
                stateid = 0;
            }
        }
    }
}