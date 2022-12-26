using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Text;
using System.Windows;
using Newtonsoft.Json.Linq;
using System.Drawing;

namespace WebApplication1
{
    public partial class _Default : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                SetInitialRow();
                string mainconn = ConfigurationManager.ConnectionStrings["Myconnection"].ConnectionString;
                SqlConnection sqlconn = new SqlConnection(mainconn);
                string sqlquery = "SELECT [cb] ,[serial],[input]  FROM [TB].[dbo].[Test]";
                SqlCommand sqlcomm = new SqlCommand(sqlquery, sqlconn);
                sqlconn.Open();
                SqlDataAdapter sda = new SqlDataAdapter(sqlcomm);
                //DataTable dt = new DataTable();
                DataSet dt = new DataSet();
                sda.Fill(dt);
                GridView1.DataSource = dt;
                GridView1.DataBind();
               
                sqlconn.Close();
            }
            }
       

        protected void Delete_Click(object sender, EventArgs e)
        {
            foreach (GridViewRow grow in GridView1.Rows)
            {
                
                //Searching CheckBox("chkDel") in an individual row of Grid  
                CheckBox chkdel = (CheckBox)grow.FindControl("CheckBox1");
                

                    //If CheckBox is checked than delete the record with particular empid  
                    if (chkdel.Checked)
                {
                    
                    // int cb = Convert.ToInt32(GridView1.DataKeys[grow.RowIndex]);
                    // Response.Write(grow.Cells[0].Text);
                    int serial = Convert.ToInt32(GridView1.DataKeys[grow.RowIndex].Value);
                    string mainconn = ConfigurationManager.ConnectionStrings["Myconnection"].ConnectionString;
                    using (SqlConnection sqlconn = new SqlConnection(mainconn))
                    {
                        sqlconn.Open();
                        string sqlquery = "DELETE FROM [dbo].[Test]      WHERE serial=" ;
                        SqlCommand sqlcomm = new SqlCommand(sqlquery+serial, sqlconn);
                        sqlcomm.ExecuteNonQuery();
                        sqlconn.Close();
                    }

                    // int cb = Convert.ToInt32(grow.Cells[0].Text);
                    // DeleteRecord(cb);
                }
            }
            //Displaying the Data in GridView  
            showData();

        }

        protected void DeleteRecord(int cb)
        {
            //SqlConnection con = new SqlConnection(cs);
            //SqlCommand com = new SqlCommand("delete from Employee where EmpId=@ID", con);
            //com.Parameters.AddWithValue("@ID", empid);
            //con.Open();
            //com.ExecuteNonQuery();
            //con.Close();
        }

        //Method for Displaying Data  
        protected void showData()
        {
            DataTable dt = new DataTable();
            string mainconn = ConfigurationManager.ConnectionStrings["Myconnection"].ConnectionString;
            SqlConnection sqlconn = new SqlConnection(mainconn);
            string sqlquery = "SELECT [cb] ,[serial]    ,[input]  FROM [TB].[dbo].[Test]";
            SqlCommand sqlcomm = new SqlCommand(sqlquery, sqlconn);
            sqlconn.Open();
            SqlDataAdapter sda = new SqlDataAdapter(sqlcomm);
            sda.Fill(dt);
            sqlconn.Close();
            GridView1.DataSource = dt;
            GridView1.DataBind();
        }

        protected void Button3_Click(object sender, EventArgs e)
        {
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Save Successfully')", true);
            foreach (GridViewRow row in GridView1.Rows)
            {
                string mainconn = ConfigurationManager.ConnectionStrings["Myconnection"].ConnectionString;
                using (SqlConnection sqlconn = new SqlConnection(mainconn))
                {
                    TextBox TextBox1 = (TextBox)row.Cells[2].FindControl("TextBox1");
                    using (SqlCommand sqlcomm = new SqlCommand("INSERT INTO Test(cb,serial,input) VALUES(@cb,@serial,@input)", sqlconn))
                    {
                       sqlcomm.Parameters.AddWithValue("@cb", row.Cells[0].Text);
                        sqlcomm.Parameters.AddWithValue("@serial", row.Cells[1].Text);
                        sqlcomm.Parameters.AddWithValue("@input", row.Cells[2]).Value=TextBox1.Text;
 
                        sqlconn.Open();
                        sqlcomm.ExecuteNonQuery();
                        sqlconn.Close();
                    }
                }
            }


        }


        protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            AddNewRowToGrid();
           // ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Added Successfully')", true);
        }

        private void SetInitialRow()
        {
            DataTable dt = new DataTable();
            DataRow dr = null;
            dt.Columns.Add(new DataColumn("cb", typeof(bool)));
            dt.Columns.Add(new DataColumn("serial", typeof(string)));
            dt.Columns.Add(new DataColumn("input", typeof(string)));
            dr = dt.NewRow();
            dr["cb"] = false;
            dr["serial"] = 1;
            dr["input"] = string.Empty;
            dt.Rows.Add(dr);
           // dr = dt.NewRow();
            //Store the DataTable in ViewState
            ViewState["CurrentTable"] = dt;
            GridView1.DataSource = dt;
            GridView1.DataBind();
        }


        private void AddNewRowToGrid()
        {
            int rowIndex = 0;
            if (ViewState["CurrentTable"] != null)
            {
                DataTable dtCurrentTable = (DataTable)ViewState["CurrentTable"];
                DataRow drCurrentRow = null;
                if (dtCurrentTable.Rows.Count > 0)
                {
                    drCurrentRow = dtCurrentTable.NewRow();
                    drCurrentRow["serial"] = dtCurrentTable.Rows.Count + 1;
                    dtCurrentTable.Rows.Add(drCurrentRow);
                    ViewState["CurrentTable"] = dtCurrentTable;
                    for (int i = 1; i <= dtCurrentTable.Rows.Count-1; i++)
                    {
                        //extract the TextBox value
                        TextBox box3 = (TextBox)GridView1.Rows[rowIndex].Cells[2].FindControl("TextBox1");
                        dtCurrentTable.Rows[i]["input"] = box3.Text;
                        //drCurrentRow = dtCurrentTable.NewRow();
                        //drCurrentRow["serial"] = i + 1;

                        //  dtCurrentTable.Rows[i - 1]["input"] = box3.Text;
                        //rowIndex++;
                    }
                   // drCurrentRow["serial"] = (int)ViewState["serial"] + 1;
                   // dtCurrentTable.Rows.Add(drCurrentRow);
                   // ViewState["CurrentTable"] = dtCurrentTable;
                    GridView1.DataSource = dtCurrentTable;
                    GridView1.DataBind();
                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('No record')", true);
                }

            }
            else
            {
                Response.Write("ViewState is null");
            }
            //Set Previous Data on Postbacks
            SetPreviousData();
        }

        private void SetPreviousData()
        {
            int rowIndex = 0;
            if (ViewState["CurrentTable"] != null)
            {
                DataTable dt = (DataTable)ViewState["CurrentTable"];

                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        TextBox box3 = (TextBox)GridView1.Rows[rowIndex].Cells[2].FindControl("TextBox1");
                        if (i < dt.Rows.Count-1)
                        {
                            box3.Text = dt.Rows[i]["input"].ToString();
                        }
                            // rowIndex++;
                    }
                   
                }

            }

        }


        
    }
}