﻿using Main_Real_estate.Utilities;
using MySql.Data.MySqlClient;
using System;
using System.Data;

namespace Main_Real_estate.English.Master_Panal
{
    public partial class Edit_Building_Condition : System.Web.UI.Page
    {
        private readonly MySqlConnection _sqlCon = Helper.GetConnection();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                language();


                string buildingId = Request.QueryString["Id"];
                DataTable getBuildingConditionDt = new DataTable();
                _sqlCon.Open();
                MySqlCommand getBuildingConditionCmd = new MySqlCommand(
                    "SELECT Building_Condition_id , Building_English_Condition , Building_Arabic_Condition  FROM Building_Condition WHERE Building_Condition_id = @ID",
                    _sqlCon);
                MySqlDataAdapter getBuildingConditionDa = new MySqlDataAdapter(getBuildingConditionCmd);

                getBuildingConditionCmd.Parameters.AddWithValue("@ID", buildingId);
                getBuildingConditionDa.Fill(getBuildingConditionDt);
                if (getBuildingConditionDt.Rows.Count > 0)
                {
                    txt_En_Building_Condition_Name.Text = getBuildingConditionDt.Rows[0]["Building_English_Condition"].ToString();
                    txt_Ar_Building_Condition_Name.Text = getBuildingConditionDt.Rows[0]["Building_Arabic_Condition"].ToString();


                    if (Session["Langues"].ToString() == "1") { lbl_Name_Of_Building_Condition.Text = getBuildingConditionDt.Rows[0]["Building_English_Condition"].ToString(); }
                    else { lbl_Name_Of_Building_Condition.Text = getBuildingConditionDt.Rows[0]["Building_Arabic_Condition"].ToString(); }
                        
                }

                _sqlCon.Close();
            }
        }

        protected void btn_Back_To_Building_Condition_List_Click(object sender, EventArgs e)
        {
            Response.Redirect("Building_Condition_List.aspx");
        }

        protected void btn_Edit_Building_Condition_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                string buildingId = Request.QueryString["Id"];
                string updateBuildingConditionQuary =
                    "UPDATE Building_Condition SET Building_English_Condition=@Building_English_Condition , Building_Arabic_Condition=@Building_Arabic_Condition  WHERE Building_Condition_id=@ID ";
                _sqlCon.Open();
                MySqlCommand updateBuildingConditionCmd = new MySqlCommand(updateBuildingConditionQuary, _sqlCon);
                updateBuildingConditionCmd.Parameters.AddWithValue("@ID", buildingId);
                updateBuildingConditionCmd.Parameters.AddWithValue("@Building_English_Condition",
                    txt_En_Building_Condition_Name.Text);
                updateBuildingConditionCmd.Parameters.AddWithValue("@Building_Arabic_Condition",
                    txt_Ar_Building_Condition_Name.Text);
                updateBuildingConditionCmd.ExecuteNonQuery();
                _sqlCon.Close();
                lbl_Success_Edit_New_Building_Condition.Text = "Edit successfully";
                Response.Redirect("Building_Condition_List.aspx");
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
            MySqlCommand Cmd = new MySqlCommand("SELECT * FROM languages_master", _sqlCon);
            MySqlDataAdapter Da = new MySqlDataAdapter(Cmd);
            Da.Fill(Dt);
            if (Dt.Rows.Count > 0)
            {
                if (Session["Langues"].ToString() == "1")
                {
                    lbl_titel_Edit_New_Building_Condition.Text = Dt.Rows[73]["EN"].ToString();
                    lbl_En_Building_Condition_Name.Text = Dt.Rows[70]["EN"].ToString();
                    lbl_Ar_Building_Condition_Name.Text = Dt.Rows[71]["EN"].ToString();
                    btn_Edit_Building_Condition.Text = Dt.Rows[57]["EN"].ToString();
                    btn_Back_To_Building_Condition_List.Text = Dt.Rows[72]["EN"].ToString();


                    RegularExpressionValidator1.ErrorMessage = "Only English";
                    RegularExpressionValidator2.ErrorMessage = "Only Arabic";

                    reqFuild1.ErrorMessage = "* Required";
                    RequiredFieldValidator1.ErrorMessage = "* Required";
                }
                else
                {
                    lbl_titel_Edit_New_Building_Condition.Text = Dt.Rows[73]["AR"].ToString();
                    lbl_En_Building_Condition_Name.Text = Dt.Rows[70]["AR"].ToString();
                    lbl_Ar_Building_Condition_Name.Text = Dt.Rows[71]["AR"].ToString();
                    btn_Edit_Building_Condition.Text = Dt.Rows[57]["AR"].ToString();
                    btn_Back_To_Building_Condition_List.Text = Dt.Rows[72]["AR"].ToString();


                    RegularExpressionValidator1.ErrorMessage = "فقط إنكليزي";
                    RegularExpressionValidator2.ErrorMessage = "فقط عربي";

                    reqFuild1.ErrorMessage = "* مطلوب";
                    RequiredFieldValidator1.ErrorMessage = "* مطلوب";

                }
            }
            _sqlCon.Close();

        }
    }
}