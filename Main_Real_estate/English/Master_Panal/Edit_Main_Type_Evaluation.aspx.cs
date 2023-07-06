﻿using Main_Real_estate.Utilities;
using MySql.Data.MySqlClient;
using System;
using System.Data;

namespace Main_Real_estate.English.Master_Panal
{
    public partial class Edit_Main_Type_Evaluation : System.Web.UI.Page
    {
        private readonly MySqlConnection _sqlCon = Helper.GetConnection();
        protected void Page_Load(object sender, EventArgs e)
        {
            language();
            string Main_Type_EvaluationId = Request.QueryString["Id"];

            _sqlCon.Open();
            if (!Page.IsPostBack)
            {
                

                DataTable getMain_Type_EvaluationDt = new DataTable();
                MySqlCommand getMain_Type_EvaluationCmd = new MySqlCommand("SELECT * FROM main_type_evaluation WHERE Main_Type_Evaluation_Id = @ID", _sqlCon);
                MySqlDataAdapter getMain_Type_EvaluationDa = new MySqlDataAdapter(getMain_Type_EvaluationCmd);
                getMain_Type_EvaluationCmd.Parameters.AddWithValue("@ID", Main_Type_EvaluationId);
                getMain_Type_EvaluationDa.Fill(getMain_Type_EvaluationDt);
                if (getMain_Type_EvaluationDt.Rows.Count > 0)
                {
                    txt_En_Main_Type_Evaluation_Name.Text = getMain_Type_EvaluationDt.Rows[0]["EN_Name"].ToString();
                    txt_Ar_Main_Type_Evaluation_Name.Text = getMain_Type_EvaluationDt.Rows[0]["Ar_Name"].ToString();
                    txt_Main_Type_Evaluation_Number.Text = getMain_Type_EvaluationDt.Rows[0]["Main_Weight"].ToString();

                }


                string getMain_Type_Evaluation_Wghit_Qury = "select (Select Sum(Main_Weight) from main_type_evaluation where Main_Type_Evaluation_Id != '"+ Main_Type_EvaluationId + "')  Sum_Main_Weight";
                DataTable getMain_Type_Evaluation_WghitDt = new DataTable();
                MySqlCommand getMain_Type_Evaluation_WghitCmd = new MySqlCommand(getMain_Type_Evaluation_Wghit_Qury, _sqlCon);
                MySqlDataAdapter getMain_Type_Evaluation_WghitDa = new MySqlDataAdapter(getMain_Type_Evaluation_WghitCmd);
                getMain_Type_Evaluation_WghitDa.Fill(getMain_Type_Evaluation_WghitDt);
                if (getMain_Type_Evaluation_WghitDt.Rows.Count > 0)
                {
                    string Wghit = (100 - (Convert.ToDouble(getMain_Type_Evaluation_WghitDt.Rows[0]["Sum_Main_Weight"].ToString()))).ToString();
                    txt_Main_Type_Evaluation_Number.Attributes.Add("max", Wghit);
                }

            }
            _sqlCon.Close();
        }

        protected void btn_Edit_Main_Type_Evaluation_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                string Main_Type_EvaluationId = Request.QueryString["Id"];
                string updateMain_Type_EvaluationQuary =
                    "UPDATE main_type_evaluation SET Ar_Name=@Ar_Name , EN_Name=@EN_Name , Main_Weight=@Main_Weight WHERE Main_Type_Evaluation_Id=@ID ";
                _sqlCon.Open();
                MySqlCommand updateMain_Type_EvaluationCmd = new MySqlCommand(updateMain_Type_EvaluationQuary, _sqlCon);
                updateMain_Type_EvaluationCmd.Parameters.AddWithValue("@ID", Main_Type_EvaluationId);
                updateMain_Type_EvaluationCmd.Parameters.AddWithValue("@EN_Name", txt_En_Main_Type_Evaluation_Name.Text);
                updateMain_Type_EvaluationCmd.Parameters.AddWithValue("@Ar_Name", txt_Ar_Main_Type_Evaluation_Name.Text);
                updateMain_Type_EvaluationCmd.Parameters.AddWithValue("@Main_Weight", txt_Main_Type_Evaluation_Number.Text);
                updateMain_Type_EvaluationCmd.ExecuteNonQuery();
                _sqlCon.Close();
                lbl_Success_Add_New_Main_Type_Evaluation.Text = "تم التعديل بنجاح";
                Response.Redirect("Main_Type_Evaluation_List.aspx");
            }
        }

        protected void btn_Back_To_Main_Type_Evaluation_List_Click(object sender, EventArgs e)
        {
            Response.Redirect("Main_Type_Evaluation_List.aspx");
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
                    lbl_titel_Add_New_Main_Type_Evaluation.Text = Dt.Rows[102]["EN"].ToString();
                    lbl_En_Main_Type_Evaluation_Name.Text = Dt.Rows[103]["EN"].ToString();
                    lbl_Ar_Main_Type_Evaluation_Name.Text = Dt.Rows[104]["EN"].ToString();
                    lbl_Main_Type_Evaluation_Number.Text = Dt.Rows[105]["EN"].ToString();
                    btn_Edit_Main_Type_Evaluation.Text = Dt.Rows[57]["EN"].ToString();
                    btn_Back_To_Main_Type_Evaluation_List.Text = Dt.Rows[106]["EN"].ToString();

                    RegularExpressionValidator1.ErrorMessage = "English Only";
                    RegularExpressionValidator2.ErrorMessage = "Arabic Only";
                    RegularExpressionValidator3.ErrorMessage = "Olny Numbers";

                    reqFuild1.ErrorMessage = "* Required";
                    reqFuild2.ErrorMessage = "* Required";
                    reqFuild3.ErrorMessage = "* Required";

                }
                else
                {
                    lbl_titel_Add_New_Main_Type_Evaluation.Text = Dt.Rows[102]["AR"].ToString();
                    lbl_En_Main_Type_Evaluation_Name.Text = Dt.Rows[103]["AR"].ToString();
                    lbl_Ar_Main_Type_Evaluation_Name.Text = Dt.Rows[104]["AR"].ToString();
                    lbl_Main_Type_Evaluation_Number.Text = Dt.Rows[105]["AR"].ToString();
                    btn_Edit_Main_Type_Evaluation.Text = Dt.Rows[57]["AR"].ToString();
                    btn_Back_To_Main_Type_Evaluation_List.Text = Dt.Rows[106]["AR"].ToString();

                    RegularExpressionValidator1.ErrorMessage = "إنكليزي فقط";
                    RegularExpressionValidator2.ErrorMessage = "عربي فقط";
                    RegularExpressionValidator3.ErrorMessage = "أرقام فقط";

                    reqFuild1.ErrorMessage = "* مطلوب";
                    reqFuild2.ErrorMessage = "* مطلوب";
                    reqFuild3.ErrorMessage = "* مطلوب";

                }
            }
            _sqlCon.Close();

        }
    }
}