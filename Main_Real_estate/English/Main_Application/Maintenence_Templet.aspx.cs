﻿using Main_Real_estate.Utilities;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace Main_Real_estate.English.Main_Application
{
    public partial class Maintenence_Templet : System.Web.UI.Page
    {
        private readonly MySqlConnection _sqlCon = Helper.GetConnection();
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                Utilities.Roles.Singel_Page_permission(_sqlCon, Session["Role"].ToString(), "Asset_Management", 0, "R");
            }
            catch { Response.Redirect("Log_In.aspx"); }
            
            if (!this.IsPostBack)
            {
                language();
            }
        }
        //******************  Get The Building Of Selected Ownership ***************************************************
        protected void Ownership_Name_DropDownList_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (Session["Langues"].ToString() == "1")
            {
                //Fill Buildings Name DropDownList
                Helper.LoadDropDownList(
                "SELECT * FROM building where Active ='1' and owner_ship_Owner_Ship_Id = '" +
                Ownership_Name_DropDownList.SelectedValue + "'", _sqlCon, Building_Name_DropDownList,
                "Building_English_Name", "Building_Id");
                Building_Name_DropDownList.Items.Insert(0, "...............");
            }
            else
            {
                //Fill Buildings Name DropDownList
                Helper.LoadDropDownList(
                "SELECT * FROM building where Active ='1' and owner_ship_Owner_Ship_Id = '" +
                Ownership_Name_DropDownList.SelectedValue + "'", _sqlCon, Building_Name_DropDownList,
                "Building_Arabic_Name", "Building_Id");
                Building_Name_DropDownList.Items.Insert(0, "...............");
            }


                
        }
        //******************  Date ***************************************************
        protected void Sign_Date_Calendar_SelectionChanged1(object sender, EventArgs e)
        {
            txt_Sign_Date.Text = Sign_Date_Calendar.SelectedDate.ToShortDateString();
            if (txt_Sign_Date.Text != "")
            {
                Sign_Date_divCalendar.Visible = false;
                ImageButton1.Visible = false;
            }
        }
        protected void Date_Chosee_Click(object sender, EventArgs e)
        {
            Sign_Date_divCalendar.Visible = true;
            ImageButton1.Visible = true;
        }
        protected void ImageButton1_Click(object sender, System.Web.UI.ImageClickEventArgs e)
        {
            Sign_Date_divCalendar.Visible = false;
            ImageButton1.Visible = false;
        }

        //**************************************** Date One ********************************

        protected void One_Date_Calendar_SelectionChanged1(object sender, EventArgs e)
        {
            txt_One_Date.Text = One_Date_Calendar.SelectedDate.ToShortDateString();
            if (txt_One_Date.Text != "")
            {
                One_Date_divCalendar.Visible = false;
                ImageButton2.Visible = false;
            }
        }
        protected void One_Date_Chosee_Click(object sender, EventArgs e)
        {
            One_Date_divCalendar.Visible = true;
            ImageButton2.Visible = true;
        }
        protected void One_ImageButton1_Click(object sender, System.Web.UI.ImageClickEventArgs e)
        {
            One_Date_divCalendar.Visible = false;
            ImageButton2.Visible = false;
        }

        //**************************** Date Two ************************************************

        protected void Two_Date_Calendar_SelectionChanged1(object sender, EventArgs e)
        {
            txt_Two_Date.Text = Two_Date_Calendar.SelectedDate.ToShortDateString();
            if (txt_Two_Date.Text != "")
            {
                Two_Date_divCalendar.Visible = false;
                ImageButton3.Visible = false;
            }
        }
        protected void Two_Date_Chosee_Click(object sender, EventArgs e)
        {
            Two_Date_divCalendar.Visible = true;
            ImageButton3.Visible = true;
        }
        protected void Two_ImageButton1_Click(object sender, System.Web.UI.ImageClickEventArgs e)
        {
            Two_Date_divCalendar.Visible = false;
            ImageButton3.Visible = false;
        }

        protected void btn_Maintenence_Templet_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {

            
                    string addMaintenence_TempletQuary = "" +
                "Insert Into maintenece_templete " +
                "(Employee_ID , Building_ID , Type , Date ,  Clean , Save , Maintenece , disclaimer , Note ) " +
                "VALUES" +
                "(@Employee_ID , @Building_ID , @Type , @Date ,  @Clean , @Save , @Maintenece ,  @disclaimer , @Note )";

                _sqlCon.Open();
                MySqlCommand addMaintenence_TempletCmd = new MySqlCommand(addMaintenence_TempletQuary, _sqlCon);
                addMaintenence_TempletCmd.Parameters.AddWithValue("@Employee_ID", Employee_Name_DropDownList.SelectedValue);
                addMaintenence_TempletCmd.Parameters.AddWithValue("@Building_ID", Building_Name_DropDownList.SelectedValue);
                addMaintenence_TempletCmd.Parameters.AddWithValue("@Date", txt_Sign_Date.Text);
                addMaintenence_TempletCmd.Parameters.AddWithValue("@Type", "مواقف");
                if(CheckBox1.Checked==true) { addMaintenence_TempletCmd.Parameters.AddWithValue("@Clean", "1"); } else { addMaintenence_TempletCmd.Parameters.AddWithValue("@Clean", "0"); }
                if (CheckBox2.Checked == true) { addMaintenence_TempletCmd.Parameters.AddWithValue("@Save", "1"); } else { addMaintenence_TempletCmd.Parameters.AddWithValue("@Save", "0"); }
                if (CheckBox3.Checked == true) { addMaintenence_TempletCmd.Parameters.AddWithValue("@Maintenece", "1"); } else { addMaintenence_TempletCmd.Parameters.AddWithValue("@Maintenece", "0"); }
                if (CheckBox4.Checked == true) { addMaintenence_TempletCmd.Parameters.AddWithValue("@disclaimer", "1"); } else { addMaintenence_TempletCmd.Parameters.AddWithValue("@disclaimer", "0"); }
                addMaintenence_TempletCmd.Parameters.AddWithValue("@Note", TextBox1.Text);
                addMaintenence_TempletCmd.ExecuteNonQuery();
                _sqlCon.Close();



                _sqlCon.Open();
                MySqlCommand addMaintenence_Templet_2Cmd = new MySqlCommand(addMaintenence_TempletQuary, _sqlCon);
                addMaintenence_Templet_2Cmd.Parameters.AddWithValue("@Employee_ID", Employee_Name_DropDownList.SelectedValue);
                addMaintenence_Templet_2Cmd.Parameters.AddWithValue("@Building_ID", Building_Name_DropDownList.SelectedValue);
                addMaintenence_Templet_2Cmd.Parameters.AddWithValue("@Date", txt_Sign_Date.Text);
                addMaintenence_Templet_2Cmd.Parameters.AddWithValue("@Type", "مداخل");
                if (CheckBox5.Checked == true) { addMaintenence_Templet_2Cmd.Parameters.AddWithValue("@Clean", "1"); } else { addMaintenence_Templet_2Cmd.Parameters.AddWithValue("@Clean", "0"); }
                if (CheckBox6.Checked == true) { addMaintenence_Templet_2Cmd.Parameters.AddWithValue("@Save", "1"); } else { addMaintenence_Templet_2Cmd.Parameters.AddWithValue("@Save", "0"); }
                if (CheckBox7.Checked == true) { addMaintenence_Templet_2Cmd.Parameters.AddWithValue("@Maintenece", "1"); } else { addMaintenence_Templet_2Cmd.Parameters.AddWithValue("@Maintenece", "0"); }
                if (CheckBox8.Checked == true) { addMaintenence_Templet_2Cmd.Parameters.AddWithValue("@disclaimer", "1"); } else { addMaintenence_Templet_2Cmd.Parameters.AddWithValue("@disclaimer", "0"); }
                addMaintenence_Templet_2Cmd.Parameters.AddWithValue("@Note", TextBox2.Text);
                addMaintenence_Templet_2Cmd.ExecuteNonQuery();
                _sqlCon.Close();



                _sqlCon.Open();
                MySqlCommand addMaintenence_Templet_3Cmd = new MySqlCommand(addMaintenence_TempletQuary, _sqlCon);
                addMaintenence_Templet_3Cmd.Parameters.AddWithValue("@Employee_ID", Employee_Name_DropDownList.SelectedValue);
                addMaintenence_Templet_3Cmd.Parameters.AddWithValue("@Building_ID", Building_Name_DropDownList.SelectedValue);
                addMaintenence_Templet_3Cmd.Parameters.AddWithValue("@Date", txt_Sign_Date.Text);
                addMaintenence_Templet_3Cmd.Parameters.AddWithValue("@Type", "الدرج");
                if (CheckBox9.Checked == true) { addMaintenence_Templet_3Cmd.Parameters.AddWithValue("@Clean", "1"); } else { addMaintenence_Templet_3Cmd.Parameters.AddWithValue("@Clean", "0"); }
                if (CheckBox10.Checked == true) { addMaintenence_Templet_3Cmd.Parameters.AddWithValue("@Save", "1"); } else { addMaintenence_Templet_3Cmd.Parameters.AddWithValue("@Save", "0"); }
                if (CheckBox11.Checked == true) { addMaintenence_Templet_3Cmd.Parameters.AddWithValue("@Maintenece", "1"); } else { addMaintenence_Templet_3Cmd.Parameters.AddWithValue("@Maintenece", "0"); }
                if (CheckBox12.Checked == true) { addMaintenence_Templet_3Cmd.Parameters.AddWithValue("@disclaimer", "1"); } else { addMaintenence_Templet_3Cmd.Parameters.AddWithValue("@disclaimer", "0"); }
                addMaintenence_Templet_3Cmd.Parameters.AddWithValue("@Note", TextBox3.Text);
                addMaintenence_Templet_3Cmd.ExecuteNonQuery();
                _sqlCon.Close();



                _sqlCon.Open();
                MySqlCommand addMaintenence_Templet_4Cmd = new MySqlCommand(addMaintenence_TempletQuary, _sqlCon);
                addMaintenence_Templet_4Cmd.Parameters.AddWithValue("@Employee_ID", Employee_Name_DropDownList.SelectedValue);
                addMaintenence_Templet_4Cmd.Parameters.AddWithValue("@Building_ID", Building_Name_DropDownList.SelectedValue);
                addMaintenence_Templet_4Cmd.Parameters.AddWithValue("@Date", txt_Sign_Date.Text);
                addMaintenence_Templet_4Cmd.Parameters.AddWithValue("@Type", "السطح");
                if (CheckBox13.Checked == true) { addMaintenence_Templet_4Cmd.Parameters.AddWithValue("@Clean", "1"); } else { addMaintenence_Templet_4Cmd.Parameters.AddWithValue("@Clean", "0"); }
                if (CheckBox14.Checked == true) { addMaintenence_Templet_4Cmd.Parameters.AddWithValue("@Save", "1"); } else { addMaintenence_Templet_4Cmd.Parameters.AddWithValue("@Save", "0"); }
                if (CheckBox15.Checked == true) { addMaintenence_Templet_4Cmd.Parameters.AddWithValue("@Maintenece", "1"); } else { addMaintenence_Templet_4Cmd.Parameters.AddWithValue("@Maintenece", "0"); }
                if (CheckBox16.Checked == true) { addMaintenence_Templet_4Cmd.Parameters.AddWithValue("@disclaimer", "1"); } else { addMaintenence_Templet_4Cmd.Parameters.AddWithValue("@disclaimer", "0"); }
                addMaintenence_Templet_4Cmd.Parameters.AddWithValue("@Note", TextBox4.Text);
                addMaintenence_Templet_4Cmd.ExecuteNonQuery();
                _sqlCon.Close();


                _sqlCon.Open();
                MySqlCommand addMaintenence_Templet_5Cmd = new MySqlCommand(addMaintenence_TempletQuary, _sqlCon);
                addMaintenence_Templet_5Cmd.Parameters.AddWithValue("@Employee_ID", Employee_Name_DropDownList.SelectedValue);
                addMaintenence_Templet_5Cmd.Parameters.AddWithValue("@Building_ID", Building_Name_DropDownList.SelectedValue);
                addMaintenence_Templet_5Cmd.Parameters.AddWithValue("@Date", txt_Sign_Date.Text);
                addMaintenence_Templet_5Cmd.Parameters.AddWithValue("@Type", "القبو");
                if (CheckBox17.Checked == true) { addMaintenence_Templet_5Cmd.Parameters.AddWithValue("@Clean", "1"); } else { addMaintenence_Templet_5Cmd.Parameters.AddWithValue("@Clean", "0"); }
                if (CheckBox18.Checked == true) { addMaintenence_Templet_5Cmd.Parameters.AddWithValue("@Save", "1"); } else { addMaintenence_Templet_5Cmd.Parameters.AddWithValue("@Save", "0"); }
                if (CheckBox19.Checked == true) { addMaintenence_Templet_5Cmd.Parameters.AddWithValue("@Maintenece", "1"); } else { addMaintenence_Templet_5Cmd.Parameters.AddWithValue("@Maintenece", "0"); }
                if (CheckBox20.Checked == true) { addMaintenence_Templet_5Cmd.Parameters.AddWithValue("@disclaimer", "1"); } else { addMaintenence_Templet_5Cmd.Parameters.AddWithValue("@disclaimer", "0"); }
                addMaintenence_Templet_5Cmd.Parameters.AddWithValue("@Note", TextBox5.Text);
                addMaintenence_Templet_5Cmd.ExecuteNonQuery();
                _sqlCon.Close();


                _sqlCon.Open();
                MySqlCommand addMaintenence_Templet_6Cmd = new MySqlCommand(addMaintenence_TempletQuary, _sqlCon);
                addMaintenence_Templet_6Cmd.Parameters.AddWithValue("@Employee_ID", Employee_Name_DropDownList.SelectedValue);
                addMaintenence_Templet_6Cmd.Parameters.AddWithValue("@Building_ID", Building_Name_DropDownList.SelectedValue);
                addMaintenence_Templet_6Cmd.Parameters.AddWithValue("@Date", txt_Sign_Date.Text);
                addMaintenence_Templet_6Cmd.Parameters.AddWithValue("@Type", "غرفة المولد");
                if (CheckBox21.Checked == true) { addMaintenence_Templet_6Cmd.Parameters.AddWithValue("@Clean", "1"); } else { addMaintenence_Templet_6Cmd.Parameters.AddWithValue("@Clean", "0"); }
                if (CheckBox22.Checked == true) { addMaintenence_Templet_6Cmd.Parameters.AddWithValue("@Save", "1"); } else { addMaintenence_Templet_6Cmd.Parameters.AddWithValue("@Save", "0"); }
                if (CheckBox23.Checked == true) { addMaintenence_Templet_6Cmd.Parameters.AddWithValue("@Maintenece", "1"); } else { addMaintenence_Templet_6Cmd.Parameters.AddWithValue("@Maintenece", "0"); }
                if (CheckBox24.Checked == true) { addMaintenence_Templet_6Cmd.Parameters.AddWithValue("@disclaimer", "1"); } else { addMaintenence_Templet_6Cmd.Parameters.AddWithValue("@disclaimer", "0"); }
                addMaintenence_Templet_6Cmd.Parameters.AddWithValue("@Note", TextBox6.Text);
                addMaintenence_Templet_6Cmd.ExecuteNonQuery();
                _sqlCon.Close();


                _sqlCon.Open();
                MySqlCommand addMaintenence_Templet_7Cmd = new MySqlCommand(addMaintenence_TempletQuary, _sqlCon);
                addMaintenence_Templet_7Cmd.Parameters.AddWithValue("@Employee_ID", Employee_Name_DropDownList.SelectedValue);
                addMaintenence_Templet_7Cmd.Parameters.AddWithValue("@Building_ID", Building_Name_DropDownList.SelectedValue);
                addMaintenence_Templet_7Cmd.Parameters.AddWithValue("@Date", txt_Sign_Date.Text);
                addMaintenence_Templet_7Cmd.Parameters.AddWithValue("@Type", "غرفة الكهرباء");
                if (CheckBox25.Checked == true) { addMaintenence_Templet_7Cmd.Parameters.AddWithValue("@Clean", "1"); } else { addMaintenence_Templet_7Cmd.Parameters.AddWithValue("@Clean", "0"); }
                if (CheckBox26.Checked == true) { addMaintenence_Templet_7Cmd.Parameters.AddWithValue("@Save", "1"); } else { addMaintenence_Templet_7Cmd.Parameters.AddWithValue("@Save", "0"); }
                if (CheckBox27.Checked == true) { addMaintenence_Templet_7Cmd.Parameters.AddWithValue("@Maintenece", "1"); } else { addMaintenence_Templet_7Cmd.Parameters.AddWithValue("@Maintenece", "0"); }
                if (CheckBox28.Checked == true) { addMaintenence_Templet_7Cmd.Parameters.AddWithValue("@disclaimer", "1"); } else { addMaintenence_Templet_7Cmd.Parameters.AddWithValue("@disclaimer", "0"); }
                addMaintenence_Templet_7Cmd.Parameters.AddWithValue("@Note", TextBox7.Text);
                addMaintenence_Templet_7Cmd.ExecuteNonQuery();
                _sqlCon.Close();



                _sqlCon.Open();
                MySqlCommand addMaintenence_Templet_8Cmd = new MySqlCommand(addMaintenence_TempletQuary, _sqlCon);
                addMaintenence_Templet_8Cmd.Parameters.AddWithValue("@Employee_ID", Employee_Name_DropDownList.SelectedValue);
                addMaintenence_Templet_8Cmd.Parameters.AddWithValue("@Building_ID", Building_Name_DropDownList.SelectedValue);
                addMaintenence_Templet_8Cmd.Parameters.AddWithValue("@Date", txt_Sign_Date.Text);
                addMaintenence_Templet_8Cmd.Parameters.AddWithValue("@Type", "غرفة الدفاع المدني");
                if (CheckBox29.Checked == true) { addMaintenence_Templet_8Cmd.Parameters.AddWithValue("@Clean", "1"); } else { addMaintenence_Templet_8Cmd.Parameters.AddWithValue("@Clean", "0"); }
                if (CheckBox30.Checked == true) { addMaintenence_Templet_8Cmd.Parameters.AddWithValue("@Save", "1"); } else { addMaintenence_Templet_8Cmd.Parameters.AddWithValue("@Save", "0"); }
                if (CheckBox31.Checked == true) { addMaintenence_Templet_8Cmd.Parameters.AddWithValue("@Maintenece", "1"); } else { addMaintenence_Templet_8Cmd.Parameters.AddWithValue("@Maintenece", "0"); }
                if (CheckBox32.Checked == true) { addMaintenence_Templet_8Cmd.Parameters.AddWithValue("@disclaimer", "1"); } else { addMaintenence_Templet_8Cmd.Parameters.AddWithValue("@disclaimer", "0"); }
                addMaintenence_Templet_8Cmd.Parameters.AddWithValue("@Note", TextBox8.Text);
                addMaintenence_Templet_8Cmd.ExecuteNonQuery();
                _sqlCon.Close();



                _sqlCon.Open();
                MySqlCommand addMaintenence_Templet_9Cmd = new MySqlCommand(addMaintenence_TempletQuary, _sqlCon);
                addMaintenence_Templet_9Cmd.Parameters.AddWithValue("@Employee_ID", Employee_Name_DropDownList.SelectedValue);
                addMaintenence_Templet_9Cmd.Parameters.AddWithValue("@Building_ID", Building_Name_DropDownList.SelectedValue);
                addMaintenence_Templet_9Cmd.Parameters.AddWithValue("@Date", txt_Sign_Date.Text);
                addMaintenence_Templet_9Cmd.Parameters.AddWithValue("@Type", "مكب القمامة");
                if (CheckBox33.Checked == true) { addMaintenence_Templet_9Cmd.Parameters.AddWithValue("@Clean", "1"); } else { addMaintenence_Templet_9Cmd.Parameters.AddWithValue("@Clean", "0"); }
                if (CheckBox34.Checked == true) { addMaintenence_Templet_9Cmd.Parameters.AddWithValue("@Save", "1"); } else { addMaintenence_Templet_9Cmd.Parameters.AddWithValue("@Save", "0"); }
                if (CheckBox35.Checked == true) { addMaintenence_Templet_9Cmd.Parameters.AddWithValue("@Maintenece", "1"); } else { addMaintenence_Templet_9Cmd.Parameters.AddWithValue("@Maintenece", "0"); }
                if (CheckBox36.Checked == true) { addMaintenence_Templet_9Cmd.Parameters.AddWithValue("@disclaimer", "1"); } else { addMaintenence_Templet_9Cmd.Parameters.AddWithValue("@disclaimer", "0"); }
                addMaintenence_Templet_9Cmd.Parameters.AddWithValue("@Note", TextBox9.Text);
                addMaintenence_Templet_9Cmd.ExecuteNonQuery();
                _sqlCon.Close();


                _sqlCon.Open();
                MySqlCommand addMaintenence_Templet_10Cmd = new MySqlCommand(addMaintenence_TempletQuary, _sqlCon);
                addMaintenence_Templet_10Cmd.Parameters.AddWithValue("@Employee_ID", Employee_Name_DropDownList.SelectedValue);
                addMaintenence_Templet_10Cmd.Parameters.AddWithValue("@Building_ID", Building_Name_DropDownList.SelectedValue);
                addMaintenence_Templet_10Cmd.Parameters.AddWithValue("@Date", txt_Sign_Date.Text);
                addMaintenence_Templet_10Cmd.Parameters.AddWithValue("@Type", "المصاعد");
                if (CheckBox37.Checked == true) { addMaintenence_Templet_10Cmd.Parameters.AddWithValue("@Clean", "1"); } else { addMaintenence_Templet_10Cmd.Parameters.AddWithValue("@Clean", "0"); }
                if (CheckBox38.Checked == true) { addMaintenence_Templet_10Cmd.Parameters.AddWithValue("@Save", "1"); } else { addMaintenence_Templet_10Cmd.Parameters.AddWithValue("@Save", "0"); }
                if (CheckBox39.Checked == true) { addMaintenence_Templet_10Cmd.Parameters.AddWithValue("@Maintenece", "1"); } else { addMaintenence_Templet_10Cmd.Parameters.AddWithValue("@Maintenece", "0"); }
                if (CheckBox40.Checked == true) { addMaintenence_Templet_10Cmd.Parameters.AddWithValue("@disclaimer", "1"); } else { addMaintenence_Templet_10Cmd.Parameters.AddWithValue("@disclaimer", "0"); }
                addMaintenence_Templet_10Cmd.Parameters.AddWithValue("@Note", TextBox10.Text);
                addMaintenence_Templet_10Cmd.ExecuteNonQuery();
                _sqlCon.Close();



                _sqlCon.Open();
                MySqlCommand addMaintenence_Templet_11Cmd = new MySqlCommand(addMaintenence_TempletQuary, _sqlCon);
                addMaintenence_Templet_11Cmd.Parameters.AddWithValue("@Employee_ID", Employee_Name_DropDownList.SelectedValue);
                addMaintenence_Templet_11Cmd.Parameters.AddWithValue("@Building_ID", Building_Name_DropDownList.SelectedValue);
                addMaintenence_Templet_11Cmd.Parameters.AddWithValue("@Date", txt_Sign_Date.Text);
                addMaintenence_Templet_11Cmd.Parameters.AddWithValue("@Type", "الممرات");
                if (CheckBox41.Checked == true) { addMaintenence_Templet_11Cmd.Parameters.AddWithValue("@Clean", "1"); } else { addMaintenence_Templet_11Cmd.Parameters.AddWithValue("@Clean", "0"); }
                if (CheckBox42.Checked == true) { addMaintenence_Templet_11Cmd.Parameters.AddWithValue("@Save", "1"); } else { addMaintenence_Templet_11Cmd.Parameters.AddWithValue("@Save", "0"); }
                if (CheckBox43.Checked == true) { addMaintenence_Templet_11Cmd.Parameters.AddWithValue("@Maintenece", "1"); } else { addMaintenence_Templet_11Cmd.Parameters.AddWithValue("@Maintenece", "0"); }
                if (CheckBox44.Checked == true) { addMaintenence_Templet_11Cmd.Parameters.AddWithValue("@disclaimer", "1"); } else { addMaintenence_Templet_11Cmd.Parameters.AddWithValue("@disclaimer", "0"); }
                addMaintenence_Templet_11Cmd.Parameters.AddWithValue("@Note", TextBox11.Text);
                addMaintenence_Templet_11Cmd.ExecuteNonQuery();
                _sqlCon.Close();



                _sqlCon.Open();
                MySqlCommand addMaintenence_Templet_12Cmd = new MySqlCommand(addMaintenence_TempletQuary, _sqlCon);
                addMaintenence_Templet_12Cmd.Parameters.AddWithValue("@Employee_ID", Employee_Name_DropDownList.SelectedValue);
                addMaintenence_Templet_12Cmd.Parameters.AddWithValue("@Building_ID", Building_Name_DropDownList.SelectedValue);
                addMaintenence_Templet_12Cmd.Parameters.AddWithValue("@Date", txt_Sign_Date.Text);
                addMaintenence_Templet_12Cmd.Parameters.AddWithValue("@Type", "أجهزة عامة / أثاث عام");
                if (CheckBox45.Checked == true) { addMaintenence_Templet_12Cmd.Parameters.AddWithValue("@Clean", "1"); } else { addMaintenence_Templet_12Cmd.Parameters.AddWithValue("@Clean", "0"); }
                if (CheckBox46.Checked == true) { addMaintenence_Templet_12Cmd.Parameters.AddWithValue("@Save", "1"); } else { addMaintenence_Templet_12Cmd.Parameters.AddWithValue("@Save", "0"); }
                if (CheckBox47.Checked == true) { addMaintenence_Templet_12Cmd.Parameters.AddWithValue("@Maintenece", "1"); } else { addMaintenence_Templet_12Cmd.Parameters.AddWithValue("@Maintenece", "0"); }
                if (CheckBox48.Checked == true) { addMaintenence_Templet_12Cmd.Parameters.AddWithValue("@disclaimer", "1"); } else { addMaintenence_Templet_12Cmd.Parameters.AddWithValue("@disclaimer", "0"); }
                addMaintenence_Templet_12Cmd.Parameters.AddWithValue("@Note", TextBox12.Text);
                addMaintenence_Templet_12Cmd.ExecuteNonQuery();
                _sqlCon.Close();










                string addMaintenence_TempletQuary_13 = "" +
                "Insert Into maintenece_templete " +
                "(Employee_ID , Building_ID , Type , Date ,  Clean , Save , Maintenece , disclaimer , Note , Last_Maintenece_Date , Last_Maintenece_Type , Last_Clean_Date , Last_Clean_Type , Discription , Location_Ifo) " +
                "VALUES" +
                "(@Employee_ID , @Building_ID , @Type , @Date ,  @Clean , @Save , @Maintenece ,  @disclaimer , @Note , @Last_Maintenece_Date , @Last_Maintenece_Type , @Last_Clean_Date , @Last_Clean_Type , @Discription , @Location_Ifo)";
                _sqlCon.Open();
                MySqlCommand addMaintenence_Templet_13Cmd = new MySqlCommand(addMaintenence_TempletQuary_13, _sqlCon);
                addMaintenence_Templet_13Cmd.Parameters.AddWithValue("@Employee_ID", Employee_Name_DropDownList.SelectedValue);
                addMaintenence_Templet_13Cmd.Parameters.AddWithValue("@Building_ID", Building_Name_DropDownList.SelectedValue);
                addMaintenence_Templet_13Cmd.Parameters.AddWithValue("@Date", txt_Sign_Date.Text);
                addMaintenence_Templet_13Cmd.Parameters.AddWithValue("@Type", "معلومات");

                addMaintenence_Templet_13Cmd.Parameters.AddWithValue("@Clean", "");
                addMaintenence_Templet_13Cmd.Parameters.AddWithValue("@Save", "");
                addMaintenence_Templet_13Cmd.Parameters.AddWithValue("@Maintenece", "");
                addMaintenence_Templet_13Cmd.Parameters.AddWithValue("@disclaimer", "");

                addMaintenence_Templet_13Cmd.Parameters.AddWithValue("@Note", "");



                addMaintenence_Templet_13Cmd.Parameters.AddWithValue("@Last_Maintenece_Date", txt_One_Date.Text);
                addMaintenence_Templet_13Cmd.Parameters.AddWithValue("@Last_Maintenece_Type", Radio_1.SelectedValue);
                addMaintenence_Templet_13Cmd.Parameters.AddWithValue("@Last_Clean_Date", txt_Two_Date.Text);
                addMaintenence_Templet_13Cmd.Parameters.AddWithValue("@Last_Clean_Type", Radio_2.SelectedValue);
                addMaintenence_Templet_13Cmd.Parameters.AddWithValue("@Discription", txt_Discription.Text);
                addMaintenence_Templet_13Cmd.Parameters.AddWithValue("@Location_Ifo", txt_Location_IFO.Text);
                addMaintenence_Templet_13Cmd.ExecuteNonQuery();
                _sqlCon.Close();
            }
        }




















        //******************************************************************************************************************************************
        //************************************************** languages ****************************************************************
        //******************************************************************************************************************************************

        protected void language()
        {

            if (Session["Langues"] == null) { Session["Langues"] = "1"; }
            _sqlCon.Open();
            DataTable Dt = new DataTable();
            MySqlCommand Cmd = new MySqlCommand("SELECT * FROM languages_maintenance", _sqlCon);
            MySqlDataAdapter Da = new MySqlDataAdapter(Cmd);
            Da.Fill(Dt);
            if (Dt.Rows.Count > 0)
            {
                if (Session["Langues"].ToString() == "1")
                {
                    //Fill Ownership Name DropDownList
                    Helper.LoadDropDownList("SELECT * FROM owner_ship where Active !='1'", _sqlCon, Ownership_Name_DropDownList, "Owner_Ship_EN_Name", "Owner_Ship_Id");
                    Ownership_Name_DropDownList.Items.Insert(0, "...............");

                    //Fill Building Name DropDownList
                    Helper.LoadDropDownList("SELECT * FROM building Where Active ='1'", _sqlCon, Building_Name_DropDownList, "Building_English_Name", "Building_Id");
                    Building_Name_DropDownList.Items.Insert(0, "...............");

                    //Fill Employee Name DropDownList
                    Helper.LoadDropDownList("SELECT * FROM employee", _sqlCon, Employee_Name_DropDownList, "Employee_English_name", "Employee_Id");
                    Employee_Name_DropDownList.Items.Insert(0, "...............");



                    Radio_1.Items.Clear();
                    Radio_1.Items.Add(new ListItem("Periodically", "1"));
                    Radio_1.Items.Add(new ListItem("Non-Periodic", "2"));

                    Radio_2.Items.Clear();
                    Radio_2.Items.Add(new ListItem("Periodically", "1"));
                    Radio_2.Items.Add(new ListItem("Non-Periodic", "2"));


                    lbl_Titel.Text = Dt.Rows[111]["EN"].ToString();
                    lbl_Employee_Name.Text = Dt.Rows[44]["EN"].ToString();
                    lbl_Ownership_Name.Text = Dt.Rows[83]["EN"].ToString();
                    lbl_Building_Name.Text = Dt.Rows[7]["EN"].ToString();
                    lbl_Sign_Date.Text = Dt.Rows[64]["EN"].ToString();
                    lbl_Location_IFO.Text = Dt.Rows[88]["EN"].ToString();
                    lbl_One_Date_Type.Text = Dt.Rows[108]["EN"].ToString();
                    lbl_One_Date.Text = Dt.Rows[106]["EN"].ToString();
                    lbl_Two_Date.Text = Dt.Rows[107]["EN"].ToString();
                    lbl_Two_Date_Type.Text = Dt.Rows[108]["EN"].ToString();
                    lbl_Discription.Text = Dt.Rows[71]["EN"].ToString();
                    btn_Maintenence_Templet.Text = Dt.Rows[66]["EN"].ToString();
                    Two_Date_Chosee.Text = Dt.Rows[67]["EN"].ToString();
                    One_Date_Chosee.Text = Dt.Rows[67]["EN"].ToString();
                    Sign_Date_Chosee.Text = Dt.Rows[67]["EN"].ToString();

                    lbl_tbl_Titel_One.Text = Dt.Rows[89]["EN"].ToString();
                    lbl_tbl_Titel_Two.Text = Dt.Rows[90]["EN"].ToString();
                    lbl_tbl_Titel_Three.Text = Dt.Rows[91]["EN"].ToString();
                    lbl_tbl_Titel_Four.Text = Dt.Rows[92]["EN"].ToString();
                    lbl_tbl_Titel_Five.Text = Dt.Rows[93]["EN"].ToString();
                    lbl_tbl_Titel_Six.Text = Dt.Rows[71]["EN"].ToString();

                    lbl_tbl_One.Text = Dt.Rows[94]["EN"].ToString();
                    lbl_tbl_Two.Text = Dt.Rows[95]["EN"].ToString();
                    lbl_tbl_Three.Text = Dt.Rows[96]["EN"].ToString();
                    lbl_tbl_Four.Text = Dt.Rows[97]["EN"].ToString();
                    lbl_tbl_Five.Text = Dt.Rows[98]["EN"].ToString();
                    lbl_tbl_six.Text = Dt.Rows[99]["EN"].ToString();
                    lbl_tbl_Seven.Text = Dt.Rows[100]["EN"].ToString();
                    lbl_tbl_eight.Text = Dt.Rows[101]["EN"].ToString();
                    lbl_tbl_nine.Text = Dt.Rows[102]["EN"].ToString();
                    lbl_tbl_ten.Text = Dt.Rows[103]["EN"].ToString();
                    lbl_tbl_Eleven.Text = Dt.Rows[104]["EN"].ToString();
                    lbl_tbl_Twelev.Text = Dt.Rows[105]["EN"].ToString();
                }
                else
                {

                    //Fill Ownership Name DropDownList
                    Helper.LoadDropDownList("SELECT * FROM owner_ship where Active !='1'", _sqlCon, Ownership_Name_DropDownList,"Owner_Ship_AR_Name", "Owner_Ship_Id");
                    Ownership_Name_DropDownList.Items.Insert(0, "...............");

                    //Fill Building Name DropDownList
                    Helper.LoadDropDownList("SELECT * FROM building Where Active ='1'", _sqlCon, Building_Name_DropDownList,"Building_Arabic_Name", "Building_Id");
                    Building_Name_DropDownList.Items.Insert(0, "...............");

                    //Fill Employee Name DropDownList
                    Helper.LoadDropDownList("SELECT * FROM employee", _sqlCon, Employee_Name_DropDownList, "Employee_Arabic_name", "Employee_Id");
                    Employee_Name_DropDownList.Items.Insert(0, "...............");


                    Radio_1.Items.Clear();
                    Radio_1.Items.Add(new ListItem("دورية", "1"));
                    Radio_1.Items.Add(new ListItem("حادثة", "2"));

                    Radio_2.Items.Clear();
                    Radio_2.Items.Add(new ListItem("دورية", "1"));
                    Radio_2.Items.Add(new ListItem("حادثة", "2"));



                    lbl_Titel.Text = Dt.Rows[111]["AR"].ToString();
                    lbl_Employee_Name.Text = Dt.Rows[44]["AR"].ToString();
                    lbl_Ownership_Name.Text = Dt.Rows[83]["AR"].ToString();
                    lbl_Building_Name.Text = Dt.Rows[7]["AR"].ToString();
                    lbl_Sign_Date.Text = Dt.Rows[64]["AR"].ToString();
                    lbl_Location_IFO.Text = Dt.Rows[88]["AR"].ToString();
                    lbl_One_Date_Type.Text = Dt.Rows[108]["AR"].ToString();
                    lbl_One_Date.Text = Dt.Rows[106]["AR"].ToString();
                    lbl_Two_Date.Text = Dt.Rows[107]["AR"].ToString();
                    lbl_Two_Date_Type.Text = Dt.Rows[108]["AR"].ToString();
                    lbl_Discription.Text = Dt.Rows[71]["AR"].ToString();
                    btn_Maintenence_Templet.Text = Dt.Rows[66]["AR"].ToString();
                    Two_Date_Chosee.Text = Dt.Rows[67]["AR"].ToString();
                    One_Date_Chosee.Text = Dt.Rows[67]["AR"].ToString();
                    Sign_Date_Chosee.Text = Dt.Rows[67]["AR"].ToString();

                    lbl_tbl_Titel_One.Text = Dt.Rows[89]["AR"].ToString();
                    lbl_tbl_Titel_Two.Text = Dt.Rows[90]["AR"].ToString();
                    lbl_tbl_Titel_Three.Text = Dt.Rows[91]["AR"].ToString();
                    lbl_tbl_Titel_Four.Text = Dt.Rows[92]["AR"].ToString();
                    lbl_tbl_Titel_Five.Text = Dt.Rows[93]["AR"].ToString();
                    lbl_tbl_Titel_Six.Text = Dt.Rows[71]["AR"].ToString();

                    lbl_tbl_One.Text = Dt.Rows[94]["AR"].ToString();
                    lbl_tbl_Two.Text = Dt.Rows[95]["AR"].ToString();
                    lbl_tbl_Three.Text = Dt.Rows[96]["AR"].ToString();
                    lbl_tbl_Four.Text = Dt.Rows[97]["AR"].ToString();
                    lbl_tbl_Five.Text = Dt.Rows[98]["AR"].ToString();
                    lbl_tbl_six.Text = Dt.Rows[99]["AR"].ToString();
                    lbl_tbl_Seven.Text = Dt.Rows[100]["AR"].ToString();
                    lbl_tbl_eight.Text = Dt.Rows[101]["AR"].ToString();
                    lbl_tbl_nine.Text = Dt.Rows[102]["AR"].ToString();
                    lbl_tbl_ten.Text = Dt.Rows[103]["AR"].ToString();
                    lbl_tbl_Eleven.Text = Dt.Rows[104]["AR"].ToString();
                    lbl_tbl_Twelev.Text = Dt.Rows[105]["AR"].ToString();
                }
            }
            _sqlCon.Close();

        }
    }
}