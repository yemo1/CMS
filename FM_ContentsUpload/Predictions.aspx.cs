using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Text.RegularExpressions;

namespace FM_ContentsUpload
{
    public partial class Predictions : System.Web.UI.Page
    {
        protected string subsConnection = WebConfigurationManager.ConnectionStrings["subs"].ConnectionString;
        protected string query = "SELECT TeamA,TeamB FROM w_Predictor";
        //protected string pquery = "SELECT TeamA,TeamB,MSISDN,Message,recTime FROM w_predictor_data pd JOIN w_predictor p ON pd.eventID=p.eventID WHERE TeamA =@teamA AND TeamB=@teamB ";
        protected string A = string.Empty;
        protected string B = string.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                ddlCode.Items.Insert(0, "--Select a match--");
                using (SqlConnection conn = new SqlConnection(subsConnection))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand(query, conn);
                    SqlDataReader dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        A = dr["TeamA"].ToString();
                        B = dr["TeamB"].ToString();
                        ddlCode.Items.Add(A + " VS " + B);
                    }
                    dr.Close();
                }
            }
        }

        protected void btnFind_Click(object sender, EventArgs e)
        {
            string match = ddlCode.SelectedItem.Text;
            string[] team = Regex.Split(match, " VS ");
            string teamA = team[0];
            string teamB = team[1];
            string pquery = "SELECT TeamA,TeamB,MSISDN,Message,recTime FROM w_predictor_data pd JOIN w_predictor p ON pd.eventID=p.eventID WHERE TeamA =@teamA AND TeamB=@teamB";
            using(SqlConnection conn = new SqlConnection(subsConnection))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(pquery, conn);
                cmd.Parameters.AddWithValue("@teamA", teamA);
                cmd.Parameters.AddWithValue("@teamB", teamB);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                grvPredict.DataSource = dt;
                grvPredict.DataBind();
            }
        }
    }
}