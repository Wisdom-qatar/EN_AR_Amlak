﻿using Main_Real_estate.Utilities;
using MySql.Data.MySqlClient;
using System;
using System.Data;
using System.Drawing;
using System.Web.UI;
using System.Web.UI.HtmlControls;
//using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace Main_Real_estate.English.Main_Application
{
    public partial class E_Task_List : System.Web.UI.Page
    {
        private readonly MySqlConnection _sqlCon = Helper.GetConnection();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                //   Fill Employee Name DropDownList
                DataTable get_Employee_DataTable = new DataTable();
                _sqlCon.Open();
                MySqlCommand get_Employee_Cmd =
                    new MySqlCommand("SELECT * FROM users WHERE Users_Name = @Users_Name", _sqlCon);
                MySqlDataAdapter get_Employee_Da = new MySqlDataAdapter(get_Employee_Cmd);
                if (Session["Users_Name"].ToString() != null)
                {
                    get_Employee_Cmd.Parameters.AddWithValue("@Users_Name", Session["Users_Name"].ToString());
                    get_Employee_Da.Fill(get_Employee_DataTable);
                    if (get_Employee_DataTable.Rows.Count > 0)
                    {
                        if (Session["Langues"].ToString() == "1")
                        {
                            lbl_titelt.Text = "Tasks Assigned To Mr :";
                            lbl_Employee.Text = get_Employee_DataTable.Rows[0]["Users_Name"].ToString();
                        }
                        else
                        {
                            lbl_titelt.Text = "المهام الموكلة للسيد :";
                            lbl_Employee.Text = get_Employee_DataTable.Rows[0]["Users_Ar_First_Name"].ToString() + " " + get_Employee_DataTable.Rows[0]["Users_Ar_Last_Name"].ToString();
                        }
                            
                    }
                }
                else { Response.Redirect("Log_In.aspx"); }
                _sqlCon.Close();



                Task_Repeater_DataBinder();
                Part_Task_Repeater_DataBinder();
            }
                
        }

        protected void Task_Repeater_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                var Task_Status_DropDownList = e.Item.FindControl("Task_Status_DropDownList") as DropDownList;
                string selected_Value = (string)((DataRowView)e.Item.DataItem)["Task_Status"];


                var lbl_Task_Priority = e.Item.FindControl("lbl_Task_Priority") as Label;
                var lbl_Task_Priority_Word = e.Item.FindControl("lbl_Task_Priority_Word") as Label;
                var But_Priority = e.Item.FindControl("But_Priority") as Button;
                if(lbl_Task_Priority.Text == "1") { But_Priority.BackColor = Color.Red; }
                else if (lbl_Task_Priority.Text == "2") { But_Priority.BackColor = Color.Green;  }
                else if (lbl_Task_Priority.Text == "3") { But_Priority.BackColor = Color.Blue; }


                //******************************************************************************************************************************************
                //************************************************** languages ****************************************************************
                //******************************************************************************************************************************************


                var lbl_Titel_Priority = e.Item.FindControl("lbl_Titel_Priority") as Label;
                var lbl_Titel_Task = e.Item.FindControl("lbl_Titel_Task") as Label;
                var lbl_Titel_Department = e.Item.FindControl("lbl_Titel_Department") as Label;
                var lbl_Titel_Employee = e.Item.FindControl("lbl_Titel_Employee") as Label;
                var lbl_Titel_Start = e.Item.FindControl("lbl_Titel_Start") as Label;
                var lbl_Titel_End = e.Item.FindControl("lbl_Titel_End") as Label;
                var lbl_Titel_Acual_End = e.Item.FindControl("lbl_Titel_Acual_End") as Label;
                var lbl_Titel_Note = e.Item.FindControl("lbl_Titel_Note") as Label;
                var lbl_Titel_Status = e.Item.FindControl("lbl_Titel_Status") as Label;

                var lbl_Department_Arabic_Name = e.Item.FindControl("lbl_Department_Arabic_Name") as Label;
                var lbl_Department_English_Name = e.Item.FindControl("lbl_Department_English_Name") as Label;
                var lbl_Employee_Arabic_name = e.Item.FindControl("lbl_Employee_Arabic_name") as Label;
                var lbl_Employee_English_name = e.Item.FindControl("lbl_Employee_English_name") as Label;

                var lbl_titel_Part_Task = e.Item.FindControl("lbl_titel_Part_Task") as Label;

                var Edit = e.Item.FindControl("Edit") as LinkButton;
                var Cancel_Edit = e.Item.FindControl("Cancel_Edit") as LinkButton;
                var OnEdit = e.Item.FindControl("OnEdit") as LinkButton;
                var parting = e.Item.FindControl("parting") as LinkButton;


                DataTable Dt = new DataTable();
                MySqlCommand Cmd = new MySqlCommand("SELECT * FROM languages_task", _sqlCon);
                MySqlDataAdapter Da = new MySqlDataAdapter(Cmd);
                Da.Fill(Dt);
                if (Dt.Rows.Count > 0)
                {
                    if (Session["Langues"].ToString() == "1")
                    {
                        //Get Task_Status_DropDownList
                        Task_Status_DropDownList.Items.Clear();
                        Task_Status_DropDownList.Items.Add(new ListItem("Underway", "1"));
                        Task_Status_DropDownList.Items.Add(new ListItem("Waiting", "2"));
                        Task_Status_DropDownList.Items.Add(new ListItem("Fragmented", "3"));
                        Task_Status_DropDownList.Items.Add(new ListItem("On Hold", "4"));
                        Task_Status_DropDownList.Items.Add(new ListItem("Canceled", "5"));
                        Task_Status_DropDownList.Items.Add(new ListItem("Done", "6"));

                        lbl_Titel_Priority.Text = Dt.Rows[7]["EN"].ToString();
                        lbl_Titel_Task.Text = Dt.Rows[0]["EN"].ToString();
                        lbl_Titel_Department.Text = Dt.Rows[13]["EN"].ToString();
                        lbl_Titel_Employee.Text = Dt.Rows[2]["EN"].ToString();
                        lbl_Titel_Start.Text = Dt.Rows[4]["EN"].ToString();
                        lbl_Titel_End.Text = Dt.Rows[5]["EN"].ToString();
                        lbl_Titel_Acual_End.Text = Dt.Rows[14]["EN"].ToString();
                        lbl_Titel_Note.Text = Dt.Rows[16]["EN"].ToString();
                        lbl_Titel_Status.Text = Dt.Rows[15]["EN"].ToString();
                        lbl_titel_Part_Task.Text = Dt.Rows[21]["EN"].ToString();

                        Edit.Text = Dt.Rows[17]["EN"].ToString();
                        Cancel_Edit.Text = Dt.Rows[19]["EN"].ToString();
                        OnEdit.Text = Dt.Rows[18]["EN"].ToString();
                        parting.Text = Dt.Rows[20]["EN"].ToString();

                        lbl_Department_Arabic_Name.Visible = false; lbl_Department_English_Name.Visible=true;
                        lbl_Employee_Arabic_name.Visible = false; lbl_Employee_English_name.Visible = true;
                    }
                    else
                    {
                        //Get Task_Status_DropDownList
                        Task_Status_DropDownList.Items.Clear();
                        Task_Status_DropDownList.Items.Add(new ListItem("قيد التنفيذ", "1"));
                        Task_Status_DropDownList.Items.Add(new ListItem("إنتظار", "2"));
                        Task_Status_DropDownList.Items.Add(new ListItem("مجزئة", "3"));
                        Task_Status_DropDownList.Items.Add(new ListItem("معلقة", "4"));
                        Task_Status_DropDownList.Items.Add(new ListItem("ملغاة", "5"));
                        Task_Status_DropDownList.Items.Add(new ListItem("منجزة", "6"));


                        lbl_Titel_Priority.Text = Dt.Rows[7]["AR"].ToString();
                        lbl_Titel_Task.Text = Dt.Rows[0]["AR"].ToString();
                        lbl_Titel_Department.Text = Dt.Rows[13]["AR"].ToString();
                        lbl_Titel_Employee.Text = Dt.Rows[2]["AR"].ToString();
                        lbl_Titel_Start.Text = Dt.Rows[4]["AR"].ToString();
                        lbl_Titel_End.Text = Dt.Rows[5]["AR"].ToString();
                        lbl_Titel_Acual_End.Text = Dt.Rows[14]["AR"].ToString();
                        lbl_Titel_Note.Text = Dt.Rows[16]["AR"].ToString();
                        lbl_Titel_Status.Text = Dt.Rows[15]["AR"].ToString();
                        lbl_titel_Part_Task.Text = Dt.Rows[21]["AR"].ToString();

                        Edit.Text = Dt.Rows[17]["AR"].ToString();
                        Cancel_Edit.Text = Dt.Rows[19]["AR"].ToString();
                        OnEdit.Text = Dt.Rows[18]["AR"].ToString();
                        parting.Text = Dt.Rows[20]["AR"].ToString();

                        lbl_Department_Arabic_Name.Visible = true; lbl_Department_English_Name.Visible = false;
                        lbl_Employee_Arabic_name.Visible = true; lbl_Employee_English_name.Visible = false;
                    }
                }

                
                if (selected_Value == "قيد التنفيذ" || selected_Value == "Underway") { Task_Status_DropDownList.SelectedValue = "1"; }
                else if (selected_Value == "إنتظار" || selected_Value == "Waiting") { Task_Status_DropDownList.SelectedValue = "2"; }
                else if (selected_Value == "مجزئة" || selected_Value == "Fragmented") { Task_Status_DropDownList.SelectedValue = "3"; }
                else if (selected_Value == "معلقة" || selected_Value == "On Hold") { Task_Status_DropDownList.SelectedValue = "4"; }
                else if (selected_Value == "ملغاة" || selected_Value == "Canceled") { Task_Status_DropDownList.SelectedValue = "5"; }
                else { Task_Status_DropDownList.SelectedValue = "6"; }
            }
        }
        protected void ON_Edit(object sender, EventArgs e)
        {
            RepeaterItem item = (sender as LinkButton).Parent as RepeaterItem;
            var lbl_Task_Descrioption = (item.FindControl("lbl_Task_Descrioption") as TextBox);
            var Task_Status_DropDownList = (item.FindControl("Task_Status_DropDownList") as DropDownList);
            var Edit = (item.FindControl("Edit") as LinkButton);
            var OnEdit = (item.FindControl("OnEdit") as LinkButton);
            var Cancel_Edit = (item.FindControl("Cancel_Edit") as LinkButton);
            lbl_Task_Descrioption.Enabled = true; Task_Status_DropDownList.Enabled = true;
            Edit.Visible=true  ; OnEdit.Visible=false; Cancel_Edit.Visible= true;
        }


        protected void Cancel_Edit(object sender, EventArgs e)
        {
            RepeaterItem item = (sender as LinkButton).Parent as RepeaterItem;
            var Task_Status_DropDownList = (item.FindControl("Task_Status_DropDownList") as DropDownList);
            var lbl_Task_Descrioption = (item.FindControl("lbl_Task_Descrioption") as TextBox);
            var Edit = (item.FindControl("Edit") as LinkButton);
            var OnEdit = (item.FindControl("OnEdit") as LinkButton);
            var Cancel_Edit = (item.FindControl("Cancel_Edit") as LinkButton);
            lbl_Task_Descrioption.Enabled = false; Task_Status_DropDownList.Enabled = false;
            Edit.Visible = false; OnEdit.Visible = true; Cancel_Edit.Visible = false;

            Response.Redirect(Request.RawUrl);
        }

        protected void Edit_Unit(object sender, EventArgs e)
        {
            RepeaterItem item = (sender as LinkButton).Parent as RepeaterItem;
            var Task_Status_DropDownList = (item.FindControl("Task_Status_DropDownList") as DropDownList);
            var lbl_Task_Descrioption = (item.FindControl("lbl_Task_Descrioption") as TextBox);
            var Edit = (item.FindControl("Edit") as LinkButton);
            var OnEdit = (item.FindControl("OnEdit") as LinkButton);
            var Cancel_Edit = (item.FindControl("Cancel_Edit") as LinkButton);


            string id = (sender as LinkButton).CommandArgument;
            string update_Task_Quary = "UPDATE task_management SET " +
                                       "Task_Status=@Task_Status , " +
                                       "Actual_End_Date=@Actual_End_Date , " +
                                       "Task_Descrioption=@Task_Descrioption " +
                                       "WHERE Task_Management_ID='" + id+"' ";
            _sqlCon.Open();
            MySqlCommand update_Task_Cmd = new MySqlCommand(update_Task_Quary, _sqlCon);
            update_Task_Cmd.Parameters.AddWithValue("@Task_Status", Task_Status_DropDownList.SelectedItem.Text);
            update_Task_Cmd.Parameters.AddWithValue("@Task_Descrioption", lbl_Task_Descrioption.Text);
            if (Task_Status_DropDownList.SelectedValue == "6")
            {
                update_Task_Cmd.Parameters.AddWithValue("@Actual_End_Date", DateTime.Now.ToString("dd/MM/yyyy"));
            }
            else { update_Task_Cmd.Parameters.AddWithValue("@Actual_End_Date", ""); }
            
                update_Task_Cmd.ExecuteNonQuery();
            _sqlCon.Close();


            lbl_Task_Descrioption.Enabled = false; Task_Status_DropDownList.Enabled = false;
            Edit.Visible = false; OnEdit.Visible = true; Cancel_Edit.Visible = false;

            Response.Redirect(Request.RawUrl);
        }

        protected void Task_Repeater_DataBinder()
        {
            string get_Task_Quari = "SELECT " +
                                "TM.* , " +
                                "D.Department_Arabic_Name , D.Department_English_Name ," +
                                "E.Employee_Arabic_name , E.Employee_English_name " +
                                "FROM task_management TM " +
                                "left join department D on(TM.Department_Id = D.Department_Id) " +
                                "left join employee E on(TM.Employee_Id = E.Employee_Id) Where TM.Employee_Id = '" + Session["Employee_Id"].ToString() + "' ORDER BY Task_Priority;";

            MySqlCommand get_Task_Cmd = new MySqlCommand(get_Task_Quari, _sqlCon);
            MySqlDataAdapter get_Task_Dt = new MySqlDataAdapter(get_Task_Cmd);
            get_Task_Cmd.Connection = _sqlCon;
            _sqlCon.Open();
            get_Task_Dt.SelectCommand = get_Task_Cmd;
            DataTable get_Task_DataTable = new DataTable();
            get_Task_Dt.Fill(get_Task_DataTable);
            Task_Repeater.DataSource = get_Task_DataTable;
            Task_Repeater.DataBind();
            _sqlCon.Close();
        }

        protected void Part_Task_Repeater_DataBinder()
        {
            foreach (RepeaterItem item in Task_Repeater.Items)
            {
                Repeater Part = item.FindControl("Part") as Repeater;
                Label Id = item.FindControl("Id") as Label;
                string get_Task_Quari = "SELECT * From task_part Where Task_Id = '" + Id.Text + "'";
                MySqlCommand get_Task_Cmd = new MySqlCommand(get_Task_Quari, _sqlCon);
                MySqlDataAdapter get_Task_Dt = new MySqlDataAdapter(get_Task_Cmd);
                get_Task_Cmd.Connection = _sqlCon;
                _sqlCon.Open();
                get_Task_Dt.SelectCommand = get_Task_Cmd;
                DataTable get_Task_DataTable = new DataTable();
                get_Task_Dt.Fill(get_Task_DataTable);
                Part.DataSource = get_Task_DataTable;
                Part.DataBind();
                _sqlCon.Close();
            }
        }

        protected void parting(object sender, EventArgs e)
        {

            string id = (sender as LinkButton).CommandArgument;
            RepeaterItem item = (sender as LinkButton).Parent as RepeaterItem;
            var PT_Div = (item.FindControl("PT_Div") as HtmlControl);
            var Tasl_C_ARG = (item.FindControl("Tasl_C_ARG") as Label);

            Tasl_C_ARG.Text=id;
            PT_Div.Visible = true;

        }

        protected void C_parting(object sender, EventArgs e)
        {

            foreach (RepeaterItem items in Task_Repeater.Items)
            {
                HtmlControl PT_Div = items.FindControl("PT_Div") as HtmlControl;
                TextBox txt_Part_Task_Discreption = items.FindControl("txt_Part_Task_Discreption") as TextBox;
                txt_Part_Task_Discreption.Text = "";
                PT_Div.Visible = false;
            }
            
        }


        protected void Add_Part(object sender, EventArgs e)
        {



            foreach (RepeaterItem items in Task_Repeater.Items)
            {
                TextBox txt_Part_Task_Discreption = items.FindControl("txt_Part_Task_Discreption") as TextBox;
                Label Tasl_C_ARG = items.FindControl("Tasl_C_ARG") as Label;

                string add_Part_TaskQuary = "Insert Into task_part " +
                                            "(Task_Id,Task_Part_Discription,Status) " +
                                            "VALUES" +
                                            "(@Task_Id,@Task_Part_Discription,@Status)";
                _sqlCon.Open();
                MySqlCommand add_Part_TaskCmd = new MySqlCommand(add_Part_TaskQuary, _sqlCon);
                add_Part_TaskCmd.Parameters.AddWithValue("@Task_Id", Tasl_C_ARG.Text);
                add_Part_TaskCmd.Parameters.AddWithValue("@Task_Part_Discription", txt_Part_Task_Discreption.Text);
                add_Part_TaskCmd.Parameters.AddWithValue("@Status", "قيد التنفيذ");
                add_Part_TaskCmd.ExecuteNonQuery();
                _sqlCon.Close();



                _sqlCon.Open();
                string deleteZoneQuary = " DELETE FROM task_part WHERE Task_Part_Discription = ''; ";
                MySqlCommand mySqlCmd = new MySqlCommand(deleteZoneQuary, _sqlCon);
                mySqlCmd.ExecuteNonQuery();
                _sqlCon.Close();

                Part_Task_Repeater_DataBinder();

            }
        }


        //**********************************  Part Task Controler **********************************************************
        protected void Part_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                var Task_Status_DropDownList = e.Item.FindControl("Task_Status_DropDownList") as DropDownList;
                string selected_Value = (string)((DataRowView)e.Item.DataItem)["Status"];

                var Edit = e.Item.FindControl("Edit") as LinkButton;
                var Cancel_Edit = e.Item.FindControl("Cancel_Edit") as LinkButton;
                var OnEdit = e.Item.FindControl("OnEdit") as LinkButton;
                var Delete_Pate_Task = e.Item.FindControl("Delete_Pate_Task") as LinkButton;

                DataTable Dt = new DataTable();
                MySqlCommand Cmd = new MySqlCommand("SELECT * FROM languages_task", _sqlCon);
                MySqlDataAdapter Da = new MySqlDataAdapter(Cmd);
                Da.Fill(Dt);
                if (Dt.Rows.Count > 0)
                {
                    if (Session["Langues"].ToString() == "1")
                    {
                        //Get Task_Status_DropDownList
                        Task_Status_DropDownList.Items.Clear();
                        Task_Status_DropDownList.Items.Add(new ListItem("Underway", "1"));
                        Task_Status_DropDownList.Items.Add(new ListItem("Waiting", "2"));
                        Task_Status_DropDownList.Items.Add(new ListItem("Fragmented", "3"));
                        Task_Status_DropDownList.Items.Add(new ListItem("On Hold", "4"));
                        Task_Status_DropDownList.Items.Add(new ListItem("Canceled", "5"));
                        Task_Status_DropDownList.Items.Add(new ListItem("Done", "6"));

                        Edit.Text = Dt.Rows[17]["EN"].ToString();
                        Cancel_Edit.Text = Dt.Rows[19]["EN"].ToString();
                        OnEdit.Text = Dt.Rows[18]["EN"].ToString();
                        Delete_Pate_Task.Text = Dt.Rows[22]["EN"].ToString();

                    }
                    else
                    {
                        //Get Task_Status_DropDownList
                        Task_Status_DropDownList.Items.Clear();
                        Task_Status_DropDownList.Items.Add(new ListItem("قيد التنفيذ", "1"));
                        Task_Status_DropDownList.Items.Add(new ListItem("إنتظار", "2"));
                        Task_Status_DropDownList.Items.Add(new ListItem("مجزئة", "3"));
                        Task_Status_DropDownList.Items.Add(new ListItem("معلقة", "4"));
                        Task_Status_DropDownList.Items.Add(new ListItem("ملغاة", "5"));
                        Task_Status_DropDownList.Items.Add(new ListItem("منجزة", "6"));



                        Edit.Text = Dt.Rows[17]["AR"].ToString();
                        Cancel_Edit.Text = Dt.Rows[19]["AR"].ToString();
                        OnEdit.Text = Dt.Rows[18]["AR"].ToString();
                        Delete_Pate_Task.Text = Dt.Rows[22]["AR"].ToString();

                    }

                    if (selected_Value == "قيد التنفيذ" || selected_Value == "Underway") { Task_Status_DropDownList.SelectedValue = "1"; }
                    else if (selected_Value == "إنتظار" || selected_Value == "Waiting") { Task_Status_DropDownList.SelectedValue = "2"; }
                    else if (selected_Value == "مجزئة" || selected_Value == "Fragmented") { Task_Status_DropDownList.SelectedValue = "3"; }
                    else if (selected_Value == "معلقة" || selected_Value == "On Hold") { Task_Status_DropDownList.SelectedValue = "4"; }
                    else if (selected_Value == "ملغاة" || selected_Value == "Canceled") { Task_Status_DropDownList.SelectedValue = "5"; }
                    else { Task_Status_DropDownList.SelectedValue = "6"; }
                }
            }
        }

        protected void PT_ON_Edit(object sender, EventArgs e)
        {
            RepeaterItem item = (sender as LinkButton).Parent as RepeaterItem;
            var lbl_Task_Descrioption = (item.FindControl("lbl_Part_Task_Description") as TextBox);
            var Task_Status_DropDownList = (item.FindControl("Task_Status_DropDownList") as DropDownList);
            var Edit = (item.FindControl("Edit") as LinkButton);
            var OnEdit = (item.FindControl("OnEdit") as LinkButton);
            var Cancel_Edit = (item.FindControl("Cancel_Edit") as LinkButton);
            lbl_Task_Descrioption.Enabled = true; Task_Status_DropDownList.Enabled = true;
            Edit.Visible = true; OnEdit.Visible = false; Cancel_Edit.Visible = true;
        }


        protected void PT_Cancel_Edit(object sender, EventArgs e)
        {
            RepeaterItem item = (sender as LinkButton).Parent as RepeaterItem;
            var Task_Status_DropDownList = (item.FindControl("Task_Status_DropDownList") as DropDownList);
            var lbl_Task_Descrioption = (item.FindControl("lbl_Part_Task_Description") as TextBox);
            var Edit = (item.FindControl("Edit") as LinkButton);
            var OnEdit = (item.FindControl("OnEdit") as LinkButton);
            var Cancel_Edit = (item.FindControl("Cancel_Edit") as LinkButton);
            lbl_Task_Descrioption.Enabled = false; Task_Status_DropDownList.Enabled = false;
            Edit.Visible = false; OnEdit.Visible = true; Cancel_Edit.Visible = false;
            Response.Redirect(Request.RawUrl);
        }

        protected void Edit_Part_Task(object sender, EventArgs e)
        {
            RepeaterItem item = (sender as LinkButton).Parent as RepeaterItem;
            var Task_Status_DropDownList = (item.FindControl("Task_Status_DropDownList") as DropDownList);
            var lbl_Task_Descrioption = (item.FindControl("lbl_Part_Task_Description") as TextBox);
            var Edit = (item.FindControl("Edit") as LinkButton);
            var OnEdit = (item.FindControl("OnEdit") as LinkButton);
            var Cancel_Edit = (item.FindControl("Cancel_Edit") as LinkButton);


            string id = (sender as LinkButton).CommandArgument;
            string update_Task_Quary = "UPDATE task_part SET " +
                                       "Task_Part_Discription=@Task_Part_Discription , " +
                                       "Status=@Status " +
                                       "WHERE Task_Part_Id='" + id + "' ";
            _sqlCon.Open();
            MySqlCommand update_Task_Cmd = new MySqlCommand(update_Task_Quary, _sqlCon);
            update_Task_Cmd.Parameters.AddWithValue("@Status", Task_Status_DropDownList.SelectedItem.Text);
            update_Task_Cmd.Parameters.AddWithValue("@Task_Part_Discription", lbl_Task_Descrioption.Text);

            update_Task_Cmd.ExecuteNonQuery();
            _sqlCon.Close();


            lbl_Task_Descrioption.Enabled = false; Task_Status_DropDownList.Enabled = false;
            Edit.Visible = false; OnEdit.Visible = true; Cancel_Edit.Visible = false;

            Response.Redirect(Request.RawUrl);
        }

        protected void Delete_Pate_Task(object sender, EventArgs e)
        {
            string id = (sender as LinkButton).CommandArgument;
            _sqlCon.Open();
            string delete_Part_Task_Quary = " DELETE FROM task_part WHERE Task_Part_Id = '" + id + "'; ";
            MySqlCommand mySqlCmd = new MySqlCommand(delete_Part_Task_Quary, _sqlCon);
            mySqlCmd.ExecuteNonQuery();
            _sqlCon.Close();

            Part_Task_Repeater_DataBinder();
        }


    }
}