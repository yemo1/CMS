using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using FM_ContentsUpload.Classes;
using System.IO;
using System.Drawing;
using System.Text;

namespace FM_ContentsUpload
{
    public partial class vContents : System.Web.UI.Page
    {
        //protected string connectionString
        string connection = BusinessLayer.subsConnection;
        string query = "select distinct ID,category from s_mtnplay_cat where status='true' order by category";
        string dataField;
        protected void Page_Load(object sender, EventArgs e)
        {
            //grvContents.DataSource = BusinessLayer.getCategory(connection, "select contents from s_mtnplay where category='love'");
            //grvContents.DataBind();
            dataField = string.Empty;
            
            if (!Page.IsPostBack)
            {
                ddlPlatform.DataSource = BusinessLayer.GetPlatform();
                ddlPlatform.DataBind();
                imgExport.Visible = false;
            }
        }

        protected void ddlPlatform_SelectedIndexChanged(object sender, EventArgs e)
        {
            ddlServices.Items.Clear();
            ddlServices.Items.Insert(0, "--Select a Service--");
            if (ddlPlatform.SelectedItem.Text == "Funmobile")
            {
                ddlServices.DataSource = BusinessLayer.GetAllServices();
                ddlServices.DataTextField = dataField;
            }
            else if (ddlPlatform.SelectedItem.Text == "MTNPlay")
            {
                ddlServices.DataSource = BusinessLayer.getCategory(connection, query);
                ddlServices.DataTextField = "category";
                ddlServices.DataValueField = "ID";
                //ddlServices.DataBind();
            }
           ddlServices.DataBind();
        }

        protected void ddlServices_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlPlatform.SelectedItem.Text == "MTNPlay" && ddlServices.SelectedIndex > 0 )
            {
                //string fetctContents = "select contents from s_mtnplay where category='" + ddlServices.SelectedItem.Text + "'";
                //grvContents.DataSource = BusinessLayer.getCategory(connection, fetctContents);
                //grvContents.DataBind();
                if (ddlServices.SelectedItem.Text == "Names of Allah")
                {
                    grvContents.Visible = false;
                    grvNames.Visible = true;
                    grvNames.DataSource = BusinessLayer.getNamesOfAllah(connection);
                    grvNames.PageIndex = 0;
                    grvNames.DataBind();
                }
                else
                {
                    grvContents.Visible = true;
                    grvNames.Visible = false;
                    grvContents.DataSource = BusinessLayer.getContentsByCategory(connection, ddlServices);
                    grvContents.PageIndex = 0;
                    grvContents.DataBind();
                }
               
                if (grvContents.PageCount > 0 || grvNames.PageCount > 0)
                {
                    imgExport.Visible = true;
                }
                else
                {
                    imgExport.Visible = false;
                }
            }
           
            //else if (ddlServices.SelectedIndex > 0 && ddlPlatform.SelectedIndex == 2)
            //{

            //}
        }

        protected void grvContents_RowDataBound(object sender, GridViewRowEventArgs e)
        {
           
            if (e.Row.RowIndex < 0)
                return;

            int _myColumnIndex = 0;   // Substitute your value here

            string text = e.Row.Cells[_myColumnIndex].Text;

            if (text.Length > 100)
            {
                e.Row.Cells[_myColumnIndex].Text = text.Substring(0, 50) + ".....";
                e.Row.Cells[_myColumnIndex].ToolTip = text;
            }
        }

        protected void grvContents_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            if (ddlServices.SelectedItem.Text == "Names of Allah")
            {
                //grvContents.Visible = false;
                //grvNames.Visible = true;
                grvNames.DataSource = BusinessLayer.getNamesOfAllah(connection);
                grvNames.PageIndex = e.NewPageIndex;
                grvNames.DataBind();
            }
            else
            {
                //grvContents.Visible = true;
                //grvNames.Visible = false;
                grvContents.DataSource = BusinessLayer.getContentsByCategory(connection, ddlServices);
                grvContents.PageIndex = e.NewPageIndex;
                grvContents.DataBind();
            }
        }

        public override void VerifyRenderingInServerForm(Control control)
        {
            /* Verifies that the control is rendered */
        }

        protected void imgExport_Click(object sender, ImageClickEventArgs e)
        {
            if (ddlServices.SelectedItem.Text == "Names of Allah")
            {
                BusinessLayer.exportData(grvNames, Response,ddlServices);
            }
            else
            {
                 BusinessLayer.exportData(grvContents,Response,ddlServices);
            }
        }
    }
}