using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Data.SqlClient;
using System.Web.UI.WebControls;
using System.Data;

namespace Database_Master
{
    public partial class frmSubject : System.Web.UI.Page
    {
        String connectionString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=External;Integrated Security=True;";

        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                BindData();
                DataTable dt = new DataTable();
                dt = GetSubject();
                rptSubject.DataSource = dt;
                rptSubject.DataBind();
                ListItem item = new ListItem();
                item.Text = "All";
                item.Value = "0";
                ddlCourseSearch.DataSource = GetCourse();
                ddlCourseSearch.DataTextField = "Name";
                ddlCourseSearch.DataValueField = "CourseId";
                ddlCourseSearch.DataBind();
                ddlCourseSearch.Items.Insert(0, item);
            }
        }
        private DataTable GetSubject()
        {
            SqlConnection sqlConn = new SqlConnection(connectionString);
            //sqlConn.ConnectionString= connectionString;
            String query = "Select SubjectId,S.Name as Subject,Sem,S.Logo,S.CourseId,C.Name as Course from Subject as S inner join Course as C on S.CourseId=C.CourseId;";
            SqlCommand sqlCommand = new SqlCommand(query, sqlConn);
            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlCommand);
            DataTable dt = new DataTable();
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
        private DataTable GetSubjectByCourse(int courseId)
        {
            SqlConnection sqlConn = new SqlConnection();
            sqlConn.ConnectionString= connectionString;
            String query = string.Empty;
            if(courseId==0)
            {
                query = "Select SubjectId,S.Name as Subject,Sem,S.Logo,S.CourseId,C.Name as Course from Subject as S inner join Course as C on S.CourseId=C.CourseId";
            }
            else
            {
                query = "Select SubjectId,S.Name as Subject,Sem,S.Logo,S.CourseId,C.Name as Course from Subject as S inner join Course as C on S.CourseId=C.CourseId where S.CourseID=@CourseId";
            }
            SqlCommand sqlCommand = new SqlCommand(query,sqlConn);
            sqlCommand.Parameters.Add("@CourseId",SqlDbType.Int).Value=courseId;
            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlCommand);
            DataTable dt = new DataTable();
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
        private DataTable GetCourse()
        {
            SqlConnection sqlConn = new SqlConnection();
            sqlConn.ConnectionString= connectionString;
            SqlCommand sqlCommand = new SqlCommand("Select Courseid,Name from Course",sqlConn);
            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlCommand);
            DataTable dt = new DataTable();
            sqlDataAdapter.Fill(dt);
            return dt;
        }
        private void BindData()
        {
            DataTable dt = new DataTable();
            dlSubject.DataSource = dt;
            dlSubject.DataBind();
        }
        protected void dlSubject_EditCommand(object source, DataListCommandEventArgs e)
        {
            HiddenField hidden = dlSubject.Items[e.Item.ItemIndex].FindControl("hdfCourseId") as HiddenField;
            String courseId = hidden.Value;
            dlSubject.EditItemIndex = e.Item.ItemIndex;
            BindData();
            dlSubject.EditItemIndex = e.Item.ItemIndex;
            DropDownList ddlCourse = dlSubject.Items[e.Item.ItemIndex].FindControl("ddlCourse") as DropDownList;
            DataTable dt = GetCourse();
            ddlCourse.DataSource = dt;
            ddlCourseSearch.DataTextField = "Name";
            ddlCourseSearch.DataValueField = "CourseId";
            ddlCourse.DataBind();
            ddlCourse.SelectedValue = courseId;

        }

        protected void dlSubject_DeleteCommand(object source, DataListCommandEventArgs e)
        {
            SqlConnection sqlConn = new SqlConnection();
            int index = e.Item.ItemIndex;
            sqlConn.ConnectionString = connectionString;
            String deleteQuery = "Delete from Subject where SubjectId=@SubjectId";
            SqlCommand sqlCommand = new SqlCommand(deleteQuery, sqlConn);
            sqlCommand.Parameters.Add("@SubjectId", SqlDbType.Int).Value = Convert.ToInt32(dlSubject.DataKeys[index]);
            sqlConn.Open();
            sqlCommand.ExecuteNonQuery();
            sqlConn.Close();
            sqlConn.Dispose();
            BindData();
            Response.Write("<script>alert('Record deleted Suvvessfully');</script>");
        }

        protected void dlSubject_CancelCommand(object source, DataListCommandEventArgs e)
        {
            dlSubject.EditItemIndex = -1;
            BindData();
        }

        protected void dlSubject_UpdateCommand(object source, DataListCommandEventArgs e)
        {
            if(Page.IsValid)
            {
                int currentIndex=e.Item.ItemIndex;
                String name = ((TextBox)dlSubject.Items[currentIndex].FindControl("txtName")).Text;
                String courseId = ((DropDownList)dlSubject.Items[e.Item.ItemIndex].FindControl("ddlCourse")).SelectedValue;
                int subjectId = Convert.ToInt32(dlSubject.DataKeys[currentIndex]);
                String sem = ((TextBox)dlSubject.Items[currentIndex].FindControl("txtSem")).Text;
                FileUpload fileUpload = dlSubject.Items[currentIndex].FindControl("fuLogo") as FileUpload;
                String fileName = String.Empty;
                if(fileUpload.HasFile)
                {
                    fileName = DateTime.Now.ToString("ddMMyyyymmss") + fileUpload.FileName;
                    String filePath=System.IO.Path.Combine(MapPath("~/Image/"),fileName);
                    fileUpload.SaveAs(filePath);
                }
                String updateQuery=String.Empty;
                SqlConnection sqlConn = new SqlConnection();
                sqlConn.ConnectionString = connectionString;
                SqlCommand sqlCommand = new SqlCommand();
                if(fileName!=string.Empty)
                {
                    updateQuery = "Update Subject set Name=@Name,Sem=@Sem,Logo=@Logo,CourseId=@CourseId where SubjectId=@SubjectId";
                    sqlCommand.Parameters.Add("@Logo",SqlDbType.VarChar,50).Value=fileName;
                }
                else
                {
                    updateQuery = "Update Subject set Name=@Name,Sem=@Sem,CourseId=@CourseId where SubjectId=@SubjectId";

                }
                sqlCommand.CommandText=updateQuery;
                sqlCommand.Connection = sqlConn;
                sqlCommand.Parameters.Add("@Name",SqlDbType.VarChar,20).Value=name;
                sqlCommand.Parameters.Add("@Sem", SqlDbType.Int).Value = sem;
                sqlCommand.Parameters.Add("@CourseId",SqlDbType.Int).Value=courseId;
                sqlCommand.Parameters.Add("@SubjectId", SqlDbType.Int).Value = subjectId;

                sqlConn.Open();
                sqlCommand.ExecuteNonQuery();
                sqlConn.Close();
                sqlConn.Dispose();
                dlSubject.EditItemIndex = -1;
                BindData();
                Response.Write("<script>alert('Data Edit Successfully');</script>");

            }
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            int courseId = Convert.ToInt32(ddlCourseSearch.SelectedValue);
            dlSubject.DataSource=GetSubjectByCourse(courseId);
            dlSubject.DataBind();
        }

        protected void rptSubject_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if(e.CommandName=="Delete")
            {
                int subjectId = Convert.ToInt32(e.CommandArgument);
                SqlConnection sqlConn = new SqlConnection();
                sqlConn.ConnectionString = connectionString;
                String deleteQuery = "Delete from Subject where SubjectId=@SubjectId";
                SqlCommand sqlCommand = new SqlCommand(deleteQuery,sqlConn);
                sqlCommand.Parameters.Add("@SubjectId",SqlDbType.Int).Value=subjectId;
                sqlConn.Open();
                sqlCommand.ExecuteNonQuery();
                sqlConn.Close();
                sqlConn.Dispose();
                DataTable dt = new DataTable();
                dt = GetSubject();
                rptSubject.DataSource=dt;
                rptSubject.DataBind();
                Response.Write("<script>alert('Record Deleted Successfully');</script>");
            }
        }
    }
}