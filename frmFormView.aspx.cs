using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Data.SqlClient;
using System.Web.UI.WebControls;
using System.ComponentModel.Design;

namespace Database_Master
{
    public partial class frmFormView : System.Web.UI.Page
    {
        String connectionString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=External;Integrated Security=True;";
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                DataTable dt = GetCourse();
                frmViewCourse.DataSource = dt;
                frmViewCourse.DataBind();
            }
        }

        protected void frmViewCourse_PageIndexChanging(object sender, FormViewPageEventArgs e)
        {
            frmViewCourse.PageIndex = e.NewPageIndex;
            DataTable dt = GetCourse();
            frmViewCourse.DataSource=dt;
            frmViewCourse.DataBind();
        }
        private DataTable GetCourse()
        {
            SqlConnection sqlConn = new SqlConnection();
            sqlConn.ConnectionString=connectionString;
            SqlCommand sqlCommand = new SqlCommand("Select * from Course", sqlConn);
            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlCommand);
            DataTable dt= new DataTable();
            sqlDataAdapter.Fill(dt);
            foreach(DataRow dr in dt.Rows)
            {
                if (String.IsNullOrEmpty(dr["Logo"].ToString()))
                {
                    dr["Logo"] = "~/Image/course.png";
                }
                else
                {
                    dr["Logo"] = "~/Image/" + dr["Logo"];
                }
            }
            return dt;
        }

        protected void frmViewCourse_ModeChanging(object sender, FormViewModeEventArgs e)
        {

        }

        protected void frmViewCourse_ItemUpdating(object sender, FormViewUpdateEventArgs e)
        {
            if(Page.IsValid)
            {
                String name = e.NewValues["Name"].ToString();
                String desc = e.NewValues["Description"].ToString();
                FileUpload fileUpload=frmViewCourse.FindControl("FuLogo") as FileUpload;
                String filename = string.Empty;
                if(fileUpload.HasFile)
                {
                    filename=DateTime.Now.ToString("ddMMyyyymmss")+fileUpload.FileName;
                    String filepath = System.IO.Path.Combine(MapPath("~/Image/"), filename);
                    fileUpload.SaveAs(filepath);
                }
                String updateQuery = string.Empty;
                SqlConnection sqlConn = new SqlConnection();
                sqlConn.ConnectionString = connectionString;
                SqlCommand sqlCommand = new SqlCommand();
                if(filename!=string.Empty)
                {
                    updateQuery = "Update Course set Name=@Name,Description=@Description,Logo=@Logo where CourseId=@CourseId";
                    sqlCommand.Parameters.Add("@Logo",SqlDbType.VarChar,50).Value=filename;
                }
                else
                {
                    updateQuery = "Update Course set Name=@Name,Description=@Description where CourseId=@CourseId";
                }
                sqlCommand.CommandText=updateQuery;
                sqlCommand.Connection = sqlConn;
                sqlCommand.Parameters.Add("@Name",SqlDbType.VarChar,20).Value=name;
                sqlCommand.Parameters.Add("@Description", SqlDbType.VarChar, 100).Value = desc;
                sqlCommand.Parameters.Add("@CourseId", SqlDbType.Int).Value = frmViewCourse.DataKey.Value;
                sqlConn.Open();
                sqlCommand.ExecuteNonQuery();
                sqlConn.Close();
                sqlConn.Dispose();
                frmViewCourse.ChangeMode(FormViewMode.ReadOnly);
                frmViewCourse.AllowPaging = true;
                DataTable dt = GetCourse();
                frmViewCourse.DataSource = dt;
                frmViewCourse.DataBind();
            }
        }

        protected void frmViewCourse_ItemDeleting(object sender, FormViewDeleteEventArgs e)
        {
            SqlConnection sqlConn = new SqlConnection();
            sqlConn.ConnectionString = connectionString;
            String deleteQuery = "Delete from Course where CourseId=@CourseId";
            SqlCommand sqlCommand= new SqlCommand(deleteQuery, sqlConn);
            sqlCommand.Parameters.Add("@CourseId",SqlDbType.Int).Value=Convert.ToInt32(frmViewCourse.DataKey.Value);
            sqlConn.Open();
            sqlCommand.ExecuteNonQuery();
            sqlConn.Close();
            sqlConn.Dispose();
            frmViewCourse.ChangeMode(FormViewMode.ReadOnly);
            frmViewCourse.AllowPaging= true;
            DataTable dt = GetCourse();
            frmViewCourse.DataSource = dt;
            frmViewCourse.DataBind();
        }

        protected void frmViewCourse_ItemCommand(object sender, FormViewCommandEventArgs e)
        {
            DataTable dt = new DataTable();
            if(e.CommandName=="Delete")
            {
                frmViewCourse.ChangeMode(FormViewMode.ReadOnly);
                frmViewCourse.AllowPaging = true;
                dt = GetCourse();
                frmViewCourse.DataSource = dt;
                frmViewCourse.DataBind();
            }
            else if(e.CommandName=="Edit")
            {
                frmViewCourse.ChangeMode(FormViewMode.Edit);
                frmViewCourse.AllowPaging = false;
                dt = GetCourse();
                frmViewCourse.DataSource = dt;
                frmViewCourse.DataBind();
            }
            else if( e.CommandName=="New")
            {
                frmViewCourse.ChangeMode(FormViewMode.Insert);
                frmViewCourse.AllowPaging = true;
                dt = GetCourse();
                frmViewCourse.DataSource = dt;
                frmViewCourse.DataBind();
            }
            else if(e.CommandName=="Cancel")
            {
                frmViewCourse.ChangeMode(FormViewMode.ReadOnly);
                frmViewCourse.AllowPaging = true;
                dt = GetCourse();
                frmViewCourse.DataSource = dt;
                frmViewCourse.DataBind();
            }
        }

        protected void frmViewCourse_ItemInserting(object sender, FormViewInsertEventArgs e)
        {
            if(Page.IsValid)
            {
                String name = e.Values["Name"].ToString();
                String desc = e.Values["Description"].ToString();
                FileUpload fileUpload = frmViewCourse.FindControl("fuLogo") as FileUpload;
                String filename = string.Empty;
                if(fileUpload.HasFile)
                {
                    filename = DateTime.Now.ToString("ddMMyyyymmss") + fileUpload.FileName;
                    String filePath = System.IO.Path.Combine(MapPath("~/Image/"), filename);
                    fileUpload.SaveAs(filePath);
                }
                String insertQuery = string.Empty;
                SqlConnection sqlConn = new SqlConnection(connectionString);
                SqlCommand sqlCommand = new SqlCommand();
                if(filename!=string.Empty)
                {
                    insertQuery = "Insert into Course (Name,Description,Logo) values(@Name,@Description,@Logo)";
                    sqlCommand.Parameters.Add("@Logo",SqlDbType.VarChar,50).Value=filename;
                }
                else
                {
                    insertQuery = "Insert into Course (Name,Description) values(@Name,@Description)";
                }
                sqlCommand.CommandText = insertQuery;
                sqlCommand.Connection = sqlConn;
                sqlCommand.Parameters.Add("@Name", SqlDbType.VarChar, 20).Value = name;
                sqlCommand.Parameters.Add("@Description", SqlDbType.VarChar, 100).Value = desc;
                sqlConn.Open();
                sqlCommand.ExecuteNonQuery();
                sqlConn.Close();
                sqlConn.Dispose();
                frmViewCourse.ChangeMode(FormViewMode.ReadOnly);
                frmViewCourse.AllowPaging = true;
                DataTable dt = GetCourse();
                frmViewCourse.DataSource = dt;
                frmViewCourse.DataBind();
            }
        }
    }
}