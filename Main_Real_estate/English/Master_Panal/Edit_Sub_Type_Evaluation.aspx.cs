﻿using Main_Real_estate.Utilities;
using MySql.Data.MySqlClient;
using System;
using System.Data;
using System.Security.Policy;

namespace Main_Real_estate.English.Master_Panal
{
    public partial class Edit_Sub_Type_Evaluation : System.Web.UI.Page
    {
        private readonly MySqlConnection _sqlCon = Helper.GetConnection();
        protected void Page_Load(object sender, EventArgs e)
        {
            string Sub_Type_EvaluationId = Request.QueryString["Id"];
            
            if (!this.IsPostBack)
            {
                language();


                _sqlCon.Open();
                DataTable getSub_Type_EvaluationDt = new DataTable();
                MySqlCommand getSub_Type_EvaluationCmd = new MySqlCommand("SELECT * FROM sub_type_evaluation WHERE Sub_Type_Evaluation_Id = @ID", _sqlCon);
                MySqlDataAdapter getSub_Type_EvaluationDa = new MySqlDataAdapter(getSub_Type_EvaluationCmd);
                getSub_Type_EvaluationCmd.Parameters.AddWithValue("@ID", Sub_Type_EvaluationId);
                getSub_Type_EvaluationDa.Fill(getSub_Type_EvaluationDt);
                if (getSub_Type_EvaluationDt.Rows.Count > 0)
                {
                    main_Type_DropDownList.SelectedValue = getSub_Type_EvaluationDt.Rows[0]["Main_Type_Evaluation_Id"].ToString();
                    txt_En_Sub_Type_Evaluation_Name.Text = getSub_Type_EvaluationDt.Rows[0]["En_Name"].ToString();
                    txt_Ar_Sub_Type_Evaluation_Name.Text = getSub_Type_EvaluationDt.Rows[0]["Ar_Name"].ToString();
                    txt_Sub_Type_Evaluation_Number.Text = getSub_Type_EvaluationDt.Rows[0]["Sub_Weight"].ToString();
                    txt_Sub_Type_Evaluation_Number_Persenteg.Text = getSub_Type_EvaluationDt.Rows[0]["R_Sub_Weight"].ToString();

                }
                _sqlCon.Close();


                string Wghit = "";
                _sqlCon.Open();
                string getSub_Type_Evaluation_Wghit_Qury = "select (Select Sum(Sub_Weight) from sub_type_evaluation Where Sub_Type_Evaluation_Id != '"+ Sub_Type_EvaluationId + "'" +
                    " and Main_Type_Evaluation_Id = '" + main_Type_DropDownList.SelectedValue + "')  Sum_Sub_Weight";
                DataTable getSub_Type_Evaluation_WghitDt = new DataTable();
                MySqlCommand getSub_Type_Evaluation_WghitCmd = new MySqlCommand(getSub_Type_Evaluation_Wghit_Qury, _sqlCon);
                MySqlDataAdapter getSub_Type_Evaluation_WghitDa = new MySqlDataAdapter(getSub_Type_Evaluation_WghitCmd);
                getSub_Type_Evaluation_WghitDa.Fill(getSub_Type_Evaluation_WghitDt);

                if (getSub_Type_Evaluation_WghitDt.Rows[0]["Sum_Sub_Weight"].ToString() == "") { txt_Sub_Type_Evaluation_Number.Attributes.Add("max", "100"); }
                else
                {
                    Wghit = (100 - (Convert.ToDouble(getSub_Type_Evaluation_WghitDt.Rows[0]["Sum_Sub_Weight"].ToString()))).ToString();
                    txt_Sub_Type_Evaluation_Number.Attributes.Add("max", Wghit);
                }
                _sqlCon.Close();
            }
        }

        protected void btn_Edit_Sub_Type_Evaluation_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                string Sub_Type_EvaluationId = Request.QueryString["Id"];
                string updateSub_Type_EvaluationQuary =
                    "UPDATE sub_type_evaluation SET" +
                    " Main_Type_Evaluation_Id=@Main_Type_Evaluation_Id , " +
                    " Ar_Name=@Ar_Name , " +
                    "EN_Name=@EN_Name , " +
                    "Sub_Weight=@Sub_Weight , R_Sub_Weight=@R_Sub_Weight" +
                    "WHERE Sub_Type_Evaluation_Id=@ID ";
                _sqlCon.Open();
                MySqlCommand updateSub_Type_EvaluationCmd = new MySqlCommand(updateSub_Type_EvaluationQuary, _sqlCon);
                updateSub_Type_EvaluationCmd.Parameters.AddWithValue("@ID", Sub_Type_EvaluationId);
                updateSub_Type_EvaluationCmd.Parameters.AddWithValue("@Main_Type_Evaluation_Id", main_Type_DropDownList.SelectedValue);
                updateSub_Type_EvaluationCmd.Parameters.AddWithValue("@EN_Name", txt_En_Sub_Type_Evaluation_Name.Text);
                updateSub_Type_EvaluationCmd.Parameters.AddWithValue("@Ar_Name", txt_Ar_Sub_Type_Evaluation_Name.Text);
                updateSub_Type_EvaluationCmd.Parameters.AddWithValue("@Sub_Weight", txt_Sub_Type_Evaluation_Number.Text);
                updateSub_Type_EvaluationCmd.Parameters.AddWithValue("@R_Sub_Weight", txt_Sub_Type_Evaluation_Number_Persenteg.Text);
                updateSub_Type_EvaluationCmd.ExecuteNonQuery();
                _sqlCon.Close();
                lbl_Success_Edit_New_Sub_Type_Evaluation.Text = "تم التعديل بنجاح";
                Response.Redirect("Sub_Type_Evaluation_List.aspx");
            }
        }

        protected void btn_Back_To_Sub_Type_Evaluation_List_Click(object sender, EventArgs e)
        {
            Response.Redirect("Sub_Type_Evaluation_List.aspx");
        }

        protected void txt_Sub_Type_Evaluation_Number_TextChanged(object sender, EventArgs e)
        {
            string Main_Wghit = ""; string R_Sub_Wghit = "";
            _sqlCon.Open();
            string getMain_Type_Evaluation_Wghit_Qury = "select Main_Weight From main_type_evaluation Where Main_Type_Evaluation_Id = '" + main_Type_DropDownList.SelectedValue + "'";
            DataTable getMain_Type_Evaluation_WghitDt = new DataTable();
            MySqlCommand getMain_Type_Evaluation_WghitCmd = new MySqlCommand(getMain_Type_Evaluation_Wghit_Qury, _sqlCon);
            MySqlDataAdapter getMain_Type_Evaluation_WghitDa = new MySqlDataAdapter(getMain_Type_Evaluation_WghitCmd);
            getMain_Type_Evaluation_WghitDa.Fill(getMain_Type_Evaluation_WghitDt);
            Main_Wghit = getMain_Type_Evaluation_WghitDt.Rows[0]["Main_Weight"].ToString();

            R_Sub_Wghit = (((Convert.ToDouble(Main_Wghit)) * ((Convert.ToDouble(txt_Sub_Type_Evaluation_Number.Text)))) / 100).ToString();
            txt_Sub_Type_Evaluation_Number_Persenteg.Text = R_Sub_Wghit;


            _sqlCon.Close();
        }

        protected void main_Type_DropDownList_SelectedIndexChanged(object sender, EventArgs e)
        {
            string Wghit = "";
            _sqlCon.Open();
            string getSub_Type_Evaluation_Wghit_Qury = "select (Select Sum(Sub_Weight) from sub_type_evaluation" +
                " where Main_Type_Evaluation_Id = '" + main_Type_DropDownList.SelectedValue + "')  Sum_Sub_Weight";
            DataTable getSub_Type_Evaluation_WghitDt = new DataTable();
            MySqlCommand getSub_Type_Evaluation_WghitCmd = new MySqlCommand(getSub_Type_Evaluation_Wghit_Qury, _sqlCon);
            MySqlDataAdapter getSub_Type_Evaluation_WghitDa = new MySqlDataAdapter(getSub_Type_Evaluation_WghitCmd);
            getSub_Type_Evaluation_WghitDa.Fill(getSub_Type_Evaluation_WghitDt);

            if (getSub_Type_Evaluation_WghitDt.Rows[0]["Sum_Sub_Weight"].ToString() == "") { txt_Sub_Type_Evaluation_Number.Attributes.Add("max", "100"); }
            else
            {
                Wghit = (100 - (Convert.ToDouble(getSub_Type_Evaluation_WghitDt.Rows[0]["Sum_Sub_Weight"].ToString()))).ToString();
                txt_Sub_Type_Evaluation_Number.Attributes.Add("max", Wghit);
            }

            _sqlCon.Close();
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
                    //Fill Tenant Name DropDownList
                    using (MySqlCommand getTenantNameDropDownListCmd = new MySqlCommand("SELECT * FROM main_type_evaluation"))
                    {
                        getTenantNameDropDownListCmd.CommandType = CommandType.Text;
                        getTenantNameDropDownListCmd.Connection = _sqlCon;
                        main_Type_DropDownList.DataSource = getTenantNameDropDownListCmd.ExecuteReader();
                        main_Type_DropDownList.DataTextField = "En_Name";
                        main_Type_DropDownList.DataValueField = "Main_Type_Evaluation_Id";
                        main_Type_DropDownList.DataBind();
                        main_Type_DropDownList.Items.Insert(0, "...............");
                    }



                    lbl_titel_Edit_New_Sub_Type_Evaluation.Text = Dt.Rows[112]["EN"].ToString();
                    lbl_main_Type.Text = Dt.Rows[107]["EN"].ToString();
                    lbl_En_Sub_Type_Evaluation_Name.Text = Dt.Rows[103]["EN"].ToString();
                    lbl_Ar_Sub_Type_Evaluation_Name.Text = Dt.Rows[104]["EN"].ToString();
                    lbl_Sub_Type_Evaluation_Number.Text = Dt.Rows[109]["EN"].ToString();
                    lbl_Sub_Type_Evaluation_Number_Persenteg.Text = Dt.Rows[110]["EN"].ToString();
                    btn_Edit_Sub_Type_Evaluation.Text = Dt.Rows[57]["EN"].ToString();
                    btn_Back_To_Sub_Type_Evaluation_List.Text = Dt.Rows[111]["EN"].ToString();

                    RegularExpressionValidator1.ErrorMessage = "English Only";
                    RegularExpressionValidator2.ErrorMessage = "Arabic Only";
                    RegularExpressionValidator3.ErrorMessage = "Olny Numbers";


                    main_Type_Req_Field_Val.ErrorMessage = "* Required";
                    reqFuild1.ErrorMessage = "* Required";
                    reqFuild2.ErrorMessage = "* Required";
                    reqFuild3.ErrorMessage = "* Required";

                }
                else
                {
                    //Fill Tenant Name DropDownList
                    using (MySqlCommand getTenantNameDropDownListCmd = new MySqlCommand("SELECT * FROM main_type_evaluation"))
                    {
                        getTenantNameDropDownListCmd.CommandType = CommandType.Text;
                        getTenantNameDropDownListCmd.Connection = _sqlCon;
                        _sqlCon.Open();
                        main_Type_DropDownList.DataSource = getTenantNameDropDownListCmd.ExecuteReader();
                        main_Type_DropDownList.DataTextField = "Ar_Name";
                        main_Type_DropDownList.DataValueField = "Main_Type_Evaluation_Id";
                        main_Type_DropDownList.DataBind();
                        main_Type_DropDownList.Items.Insert(0, "...............");
                        _sqlCon.Close();
                    }

                    lbl_titel_Edit_New_Sub_Type_Evaluation.Text = Dt.Rows[112]["AR"].ToString();
                    lbl_main_Type.Text = Dt.Rows[107]["AR"].ToString();
                    lbl_En_Sub_Type_Evaluation_Name.Text = Dt.Rows[103]["AR"].ToString();
                    lbl_Ar_Sub_Type_Evaluation_Name.Text = Dt.Rows[104]["AR"].ToString();
                    lbl_Sub_Type_Evaluation_Number.Text = Dt.Rows[109]["AR"].ToString();
                    lbl_Sub_Type_Evaluation_Number_Persenteg.Text = Dt.Rows[110]["AR"].ToString();
                    btn_Edit_Sub_Type_Evaluation.Text = Dt.Rows[57]["AR"].ToString();
                    btn_Back_To_Sub_Type_Evaluation_List.Text = Dt.Rows[111]["AR"].ToString();

                    RegularExpressionValidator1.ErrorMessage = "إنكليزي فقط";
                    RegularExpressionValidator2.ErrorMessage = "عربي فقط";
                    RegularExpressionValidator3.ErrorMessage = "أرقام فقط";

                    main_Type_Req_Field_Val.ErrorMessage = "* مطلوب";
                    reqFuild1.ErrorMessage = "* مطلوب";
                    reqFuild2.ErrorMessage = "* مطلوب";
                    reqFuild3.ErrorMessage = "* مطلوب";

                }
            }
            _sqlCon.Close();

        }
    }
}