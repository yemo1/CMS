using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using System.Data.OleDb;
using System.Web.Configuration;
using System.Web.UI.WebControls;
using System.IO;
using System.Text;



namespace FM_ContentsUpload.Classes
{
    public class BusinessLayer
    {
        public static string connectionString = "Data Source=.;Initial Catalog=Valentine;Integrated Security=True";
        public static string subConnection = WebConfigurationManager.ConnectionStrings["sub"].ConnectionString;
        public static string subsConnection = WebConfigurationManager.ConnectionStrings["subs"].ConnectionString;
        public static string smsConnection = WebConfigurationManager.ConnectionStrings["sms"].ConnectionString;
        public static string dataConnection = WebConfigurationManager.ConnectionStrings["DataConnection"].ConnectionString;
        // public static string uploadConnection = WebConfigurationManager.ConnectionStrings["Upload"].ConnectionString;
        public static string folder, description, code, fileName;

        public static List<string> GetDBNames(string connection)
        {
            List<string> lstDBName = new List<string>();

            using (SqlConnection sqlConn = new SqlConnection(connection))
            {
                sqlConn.Open();
                DataTable tblDatabases = sqlConn.GetSchema("Databases");
                foreach (DataRow row in tblDatabases.Rows)
                {
                    int id = Int32.Parse(row["dbid"].ToString());
                    if (id > 6)
                    {
                        lstDBName.Add(row["database_name"].ToString());
                    }
                }
            }
            return lstDBName;
        }

        public static List<string> GetAllTables(string connection)
        {
            List<string> result = new List<string>();
            using (SqlConnection conn = new SqlConnection(connection))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand("SELECT name FROM sys.Tables", conn))
                {
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            result.Add(reader["name"].ToString());
                        }
                    }
                }
            }
            return result;
        }

        public static string GetTableName(string service)
        {
            string result = string.Empty;
            using (SqlConnection conn = new SqlConnection(subsConnection))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand("SELECT TableName FROM BatchUpload where ServiceName=@servicename", conn))
                {
                    cmd.Parameters.AddWithValue("@servicename", service);
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            result = reader["TableName"].ToString();
                        }
                    }
                }
            }
            return result;
        }

        public static string GetDatabaseName(string service)
        {
            string result = string.Empty;
            using (SqlConnection conn = new SqlConnection(subsConnection))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand("SELECT DatabaseName FROM BatchUpload where ServiceName=@servicename", conn))
                {
                    cmd.Parameters.AddWithValue("@servicename", service);
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            result = reader["DatabaseName"].ToString();
                        }
                    }
                }
            }
            return result;
        }

        public static List<string> GetAllServices()
        {
            List<string> services = new List<string>();
            using (SqlConnection conn = new SqlConnection(subsConnection))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand("SELECT ServiceName FROM BatchUpload", conn))
                {
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            services.Add(reader["ServiceName"].ToString());
                        }
                    }
                }
            }
            //lstServices.Add("Bible");
            //lstServices.Add("Prayer");
            //lstServices.Add("Ayat");
            //lstServices.Add("Dua");
            //lstServices.Add("Azan");
            //lstServices.Add("Hadith");
            //lstServices.Add("Love");
            //lstServices.Add("Lines");
            //lstServices.Add("Jokes");
            //lstServices.Add("Quotes");
            //lstServices.Add("Word");
            //lstServices.Add("Weight");
            //lstServices.Add("Productivity");
            //lstServices.Add("Relationship");
            //lstServices.Add("Etiqutte");
            //lstServices.Add("General");
            //lstServices.Add("Health");
            //lstServices.Add("Success");
            //lstServices.Add("Stress");
            //lstServices.Add("Fashion");
            //lstServices.Add("Today");
            //lstServices.Add("FX");
            //lstServices.Add("Golf");
            //lstServices.Add("Sahur");
            //lstServices.Add("Fasting");
            //lstServices.Add("Christmas");
            //lstServices.Add("Valentine");
            //lstServices.Add("Easter");
            //lstServices.Add("Easter sermon");
            //lstServices.Add("Seven words");
            //lstServices.Add("Easter cross");
            //lstServices.Add("Ramadan");
            //lstServices.Add("Ramadan Greetings");
            //lstServices.Add("Eid Greetings");
            //lstServices.Add("Christmas bible quotes");
            //lstServices.Add("Christmas Recipes");
            //lstServices.Add("Christmas Teachings");
            //lstServices.Add("Conflict Management");
            //lstServices.Add("Study Tips");
            //lstServices.Add("Wellness");
            //lstServices.Add("Sport");
            //lstServices.Add("Nutrition");
            //lstServices.Add("Handyman");
            //lstServices.Add("CV Tips");
            //lstServices.Add("Women Empowerment");
            //lstServices.Add("IQ");
            //lstServices.Add("Dating Tips");
            //lstServices.Add("World cup moments");
            //lstServices.Add("Beauty Tips");
            //lstServices.Add("Yoruba Proverbs");
            //lstServices.Add("Common English Mistakes");
            //lstServices.Add("Arabic word of the day");
            //lstServices.Sort();
            //lstServices.Insert(0, "--Select a service--");
            services.Sort();
            return services;
        }

        public static List<string> GetTypes()
        {
            List<string> lstType = new List<string>();
            lstType.Add("Fulltracks");
            lstType.Add("Sms");
            lstType.Add("Truetones");
            lstType.Add("Videos");
            lstType.Add("Wallpapers");
            return lstType;
        }

        public static List<string> GetPlatform()
        {
            List<string> platforms = new List<string>();
            platforms.Insert(0, "--Select a platform--");
            platforms.Add("Funmobile");
            platforms.Add("MTNPlay");
            return platforms;
        }

        public static DataTable insertContents(DataTable excelData, string connectionString, string tableName)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string msg = string.Empty;
                //SqlTransaction trans = conn.BeginTransaction();
                using (SqlBulkCopy bCopy = new SqlBulkCopy(conn))
                {
                    bCopy.DestinationTableName = tableName;
                    foreach (DataColumn col in excelData.Columns)
                    {
                        bCopy.ColumnMappings.Add(col.ColumnName, col.ColumnName);
                    }
                    try
                    {
                        bCopy.WriteToServer(excelData);
                        //trans.Commit();
                    }
                    catch (Exception ex)
                    {
                        //trans.Rollback();
                        //MessageBox.Show("There was an error: " + ex.Message,"Error",MessageBoxButtons.OK,MessageBoxIcon.Error);
                        msg = "<script>";
                        msg += "alert('" + "The following error has occured: " + ex.Message.ToString() + "');";
                        msg += "</script>";
                        System.Web.HttpContext.Current.Response.Write(msg);
                    }
                }
            }
            return excelData;
        }

        public static DataTable insertMTNPlay(DataTable excelData, string connectionString, DropDownList ddl)
        {
            int categoryID = Convert.ToInt32(ddl.SelectedValue);
            string msg = string.Empty;
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                //SqlTransaction trans = conn.BeginTransaction();
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.CommandText = "INSERT INTO S_MTNPlay(Contents,CategoryID)VALUES(@contents,@categoryid)";
                    cmd.Connection = conn;
                    //cmd.Transaction = trans;
                    try
                    {
                        foreach (DataRow row in excelData.Rows)
                        {
                            cmd.Parameters.Clear();
                            cmd.Parameters.AddWithValue("@contents", row["Contents"]);
                            cmd.Parameters.AddWithValue("@categoryid", categoryID);
                            cmd.ExecuteNonQuery();
                        }
                        //trans.Commit();
                    }
                    catch (Exception ex)
                    {
                        //trans.Rollback();

                        msg = "<script>";
                        msg += "alert('" + "The following error has occured: " + ex.Message.ToString() + "');";
                        msg += "</script>";
                        System.Web.HttpContext.Current.Response.Write(msg);
                    }
                }
            }
            return excelData;
        }

        public static void AddMtnPlayService(string connectionString, string name)
        {
            string msg = string.Empty;
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                //SqlTransaction trans = conn.BeginTransaction();
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.CommandText = "INSERT INTO S_MTNPlay_Cat(Category)VALUES(@category)";
                    cmd.Connection = conn;
                    //cmd.Transaction = trans;
                    try
                    {
                        cmd.Parameters.Clear();
                        cmd.Parameters.AddWithValue("@category", name);
                        cmd.ExecuteNonQuery();
                    }
                    catch (Exception ex)
                    {
                        //trans.Rollback();

                        msg = "<script>";
                        msg += "alert('" + "The following error has occured: " + ex.Message.ToString() + "');";
                        msg += "</script>";
                        System.Web.HttpContext.Current.Response.Write(msg);
                    }
                }
            }
        }

        public static void InsertShortzContent(string message, string connectionString, DropDownList ddl)
        {
            int categoryID = Convert.ToInt32(ddl.SelectedValue);
            string msg = string.Empty;
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                //SqlTransaction trans = conn.BeginTransaction();
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.CommandText = "INSERT INTO S_MTNPlay(Contents,CategoryID)VALUES(@contents,@categoryid)";
                    cmd.Connection = conn;
                    //cmd.Transaction = trans;
                    try
                    {
                        cmd.Parameters.Clear();
                        cmd.Parameters.AddWithValue("@contents", message);
                        cmd.Parameters.AddWithValue("@categoryid", categoryID);
                        cmd.ExecuteNonQuery();
                    }
                    catch (Exception ex)
                    {
                        //trans.Rollback();

                        msg = "<script>";
                        msg += "alert('" + "The following error has occured: " + ex.Message.ToString() + "');";
                        msg += "</script>";
                        System.Web.HttpContext.Current.Response.Write(msg);
                    }
                }
            }
        }

        public static DataTable insertDataFeeds(DataTable excelData, string connectionString, DropDownList ddl)
        {
            int categoryID = Convert.ToInt32(ddl.SelectedValue);
            string msg = string.Empty;
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                //SqlTransaction trans = conn.BeginTransaction();
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.CommandText = "INSERT INTO MTN_DataService(Title,Link,Image,Description,CategoryID)VALUES(@title,@link,@image,@description,@categoryid)";
                    cmd.Connection = conn;
                    //cmd.Transaction = trans;
                    try
                    {
                        foreach (DataRow row in excelData.Rows)
                        {
                            cmd.Parameters.Clear();
                            cmd.Parameters.AddWithValue("@title", row["Title"]);
                            cmd.Parameters.AddWithValue("@link", row["Link"]);
                            cmd.Parameters.AddWithValue("@image", row["Image"]);
                            cmd.Parameters.AddWithValue("@description", row["Description"]);
                            cmd.Parameters.AddWithValue("@categoryid", categoryID);
                            cmd.ExecuteNonQuery();
                        }
                        //trans.Commit();
                    }
                    catch (Exception ex)
                    {
                        //trans.Rollback();

                        msg = "<script>";
                        msg += "alert('" + "The following error has occured: " + ex.Message.ToString() + "');";
                        msg += "</script>";
                        System.Web.HttpContext.Current.Response.Write(msg);
                    }
                }
            }
            return excelData;
        }

        public static string insertData(string SQL, int serviceid, string title, string link, string image, string desc)
        {

            using (SqlConnection sqlConnection = new SqlConnection(dataConnection))
            {
                sqlConnection.Open();
                using (SqlCommand cmd = new SqlCommand(SQL, sqlConnection))
                {
                    cmd.Parameters.AddWithValue("@serviceid", serviceid);
                    cmd.Parameters.AddWithValue("@title", title);
                    cmd.Parameters.AddWithValue("@link", link);
                    cmd.Parameters.AddWithValue("@image", image);
                    cmd.Parameters.AddWithValue("desc", desc);
                    cmd.ExecuteNonQuery();
                }
            }
            return SQL;
        }

        public static DataTable getService(string query)
        {
            DataTable dataTable = new DataTable();
            using (SqlConnection sqlConnection = new SqlConnection(dataConnection))
            {
                sqlConnection.Open();
                using (SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(query, sqlConnection))
                {
                    sqlDataAdapter.Fill(dataTable);
                }
            }
            return dataTable;
        }

        public static int GetRowCounts()
        {
            int iRowCount = 0;

            using (SqlConnection conn = new SqlConnection(WebConfigurationManager.ConnectionStrings["subs"].ToString()))
            {
                SqlCommand cmd = new SqlCommand("select count(*) from MTN_DataService", conn);
                conn.Open();

                // Execute the SqlCommand and get the row counts.
                iRowCount = (int)cmd.ExecuteScalar();
            }

            return iRowCount;
        }

        public static DataTable RetrieveData(string strConn, string query)
        {
            DataTable excelData = new DataTable();
            using (OleDbConnection conn = new OleDbConnection(strConn))
            {
                OleDbDataAdapter da = new OleDbDataAdapter(query, conn);
                da.Fill(excelData);
            }
            return excelData;
        }

        public static DataTable getCategory(string strConn, string query)
        {
            DataTable mtnServices = new DataTable();
            using (SqlConnection conn = new SqlConnection(strConn))
            {
                conn.Open();
                using (SqlDataAdapter da = new SqlDataAdapter(query, conn))
                {
                    da.Fill(mtnServices);
                }
            }
            return mtnServices;
        }

        public static void getFiles(string filePath, string category, string query, string destinationPath)
        {
            string[] files = Directory.GetFiles(filePath);
            foreach (var file in files)
            {
                string[] arrfileName = file.Split(new char[] { '\\' });
                Array.Reverse(arrfileName);
                folder = arrfileName[0];
                string desc = Path.GetFileNameWithoutExtension(folder);
                fileName = Path.GetFileName(folder);
                code = desc;
                description = code + " " + category;
                using (SqlConnection conn = new SqlConnection(smsConnection))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@code", code);
                    cmd.Parameters.AddWithValue("@description", description);
                    cmd.Parameters.AddWithValue("@folder", destinationPath + fileName);
                    //cmd.Parameters.AddWithValue("@zip", zipName);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public static string remDuplicate(string query)
        {
            using (SqlConnection conn = new SqlConnection(smsConnection))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.ExecuteNonQuery();
                }
            }
            return query;
        }

        public static string sqlQuery(string tableName)
        {
            string sql = "INSERT INTO " + tableName + "(code,description,folder)VALUES(@code,@description,@folder)";
            return sql;
        }

        public static string duplicateQuery(string tableName)
        {
            string sql = @" WITH tempTable as
                        (
                        SELECT ROW_NUMBER() Over(PARTITION BY Code ORDER BY Code) As RowNumber,* FROM " + tableName +
                        @") 
                         DELETE FROM tempTable where RowNumber >1";
            return sql;
        }

        public static DataTable getContentsByCategory(string con, DropDownList ddl)
        {
            int categoryID = Convert.ToInt32(ddl.SelectedValue);
            string qry = "Select ID,CategoryID,Contents,[Status],[published] FROM S_MTNPlay WHERE CategoryID = @Categoryid ORDER BY [published] DESC";
            using (SqlConnection conn = new SqlConnection(con))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(qry, conn);
                cmd.Parameters.AddWithValue("@Categoryid", categoryID);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                return dt;
            }
        }

        public static DataTable getNamesOfAllah(string con)
        {
            string qry = "Select ID,Name,Arabic,Meaning,[published] from r_names";
            using (SqlConnection conn = new SqlConnection(con))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(qry, conn);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                return dt;
            }
        }

        public static string EscapeCsvField(string sFieldValueToEscape)
        {
            // since we delimit values with a comma, we need to escape commas that are
            // actually in the value to escape.  We do this by putting a (") quote at
            // the front and end of the string.
            if (sFieldValueToEscape.Contains(","))
            {
                // if the string we are escaping already has a (") quote in it, we have to 
                // 'escape the escape character' by putting double quotes in it's place.
                if (sFieldValueToEscape.Contains("\""))
                {
                    return "\"" + sFieldValueToEscape.Replace("\"", "\"\"") + "\"";
                }
                else
                {
                    // there are no quotes in this string so just escape it by wrapping it in
                    // quotes.
                    return "\"" + sFieldValueToEscape + "\"";
                }
            }
            else
            {
                // There are no commas in this string so just return it as is
                return sFieldValueToEscape;
            }
        }

        public static void exportData(GridView grv, HttpResponse rs, DropDownList ddl)
        {
            rs.ClearContent();
            rs.AddHeader("content-disposition", string.Format("attachment; filename={0}", ddl.SelectedItem.Text + ".csv"));
            rs.ContentType = "application/text";
            grv.AllowPaging = false;
            if (ddl.SelectedItem.Text == "Names of Allah")
            {
                grv.Visible = false;
                grv.Visible = true;
                grv.AllowPaging = false;
                grv.DataSource = getNamesOfAllah(subsConnection);
                grv.PageIndex = 0;
                //grv.DataBind();
            }
            else
            {
                grv.Visible = true;
                grv.Visible = false;
                grv.AllowPaging = false;
                grv.DataSource = getContentsByCategory(subsConnection, ddl);
                grv.PageIndex = 0;
                //grv.DataBind();
            }
            grv.DataBind();
            StringBuilder strbldr = new StringBuilder();
            for (int i = 0; i < grv.Columns.Count; i++)
            {
                //separting header columns text with comma operator
                strbldr.Append(grv.Columns[i].HeaderText + ',');
            }
            //appending new line for gridview header row
            strbldr.Append("\n");
            for (int j = 0; j < grv.Rows.Count; j++)
            {
                for (int k = 0; k < grv.Columns.Count; k++)
                {
                    //separating gridview columns with comma
                    if (grv.Rows[j].Cells[k].Text.Contains("....."))
                    {
                        strbldr.Append(BusinessLayer.EscapeCsvField(grv.Rows[j].Cells[k].ToolTip) + ',');
                    }
                    else
                    {
                        strbldr.Append(BusinessLayer.EscapeCsvField(grv.Rows[j].Cells[k].Text) + ',');
                    }
                }
                //appending new line for gridview rows
                strbldr.Append("\n");
            }
            rs.Write(strbldr.ToString());
            rs.End();
        }

        public static string InsertCampaign(string strConn, string query, string shortcode, int appid, int serviceid, int stateid, int size, int segmentid, DateTime date, DateTime time, string message, int isTarget, DateTime timeTo)
        {

            using (SqlConnection conn = new SqlConnection(strConn))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@shortcode", shortcode);
                cmd.Parameters.AddWithValue("@appid", appid);
                cmd.Parameters.AddWithValue("@serviceid", serviceid);
                cmd.Parameters.AddWithValue("@stateid", stateid);
                cmd.Parameters.AddWithValue("@size", size);
                cmd.Parameters.AddWithValue("@segmentid", segmentid);
                cmd.Parameters.AddWithValue("@date", date);
                cmd.Parameters.AddWithValue("@time", time);
                cmd.Parameters.AddWithValue("@message", message);
                cmd.Parameters.AddWithValue("@istarget", isTarget);
                cmd.Parameters.AddWithValue("@timeto", timeTo);
                cmd.ExecuteNonQuery();
            }
            return query;
        }
    }
}