﻿using Main_Real_estate.Utilities;
using MySql.Data.MySqlClient;
using System;
using System.Data;
using System.Web.UI.WebControls;

namespace Main_Real_estate.English.Master_Panal
{
    public partial class Sub_Type_Evaluation_List : System.Web.UI.Page
    {
        private readonly MySqlConnection _sqlCon = Helper.GetConnection();
        protected void Page_Load(object sender, EventArgs e)
        {
            language();
            string getSub_Type_EvaluationQuari = "SELECT Sub.* ,  " +
                    "(Main.Ar_Name) Main_Ar_Name , (Main.EN_Name) Main_EN_Name , (Main.Main_Weight) Main_Weight  " +
                    "FROM  sub_type_evaluation Sub  " +
                    "left join  main_type_evaluation Main on (Sub.Main_Type_Evaluation_Id = Main.Main_Type_Evaluation_Id) Order By Main.Main_Type_Evaluation_Id";


            try { Helper.GetDataReader(getSub_Type_EvaluationQuari, _sqlCon, The_Table); }
            catch { Response.Write(@"<script language='javascript'>alert('لا يمكن عرض هذه الصفحة')</script>"); };
        }
        protected void Delete(object sender, EventArgs e)
        {
            try
            {
                string id = (sender as LinkButton).CommandArgument;
                _sqlCon.Open();
                string quary1 = "DELETE FROM sub_type_evaluation WHERE Sub_Type_Evaluation_Id=@ID ";
                MySqlCommand mySqlCmd = new MySqlCommand(quary1, _sqlCon);
                mySqlCmd.Parameters.AddWithValue("@ID", id);
                mySqlCmd.ExecuteNonQuery();
                _sqlCon.Close();
                Response.Redirect(Request.RawUrl);
            }
            catch
            {
                Response.Write(@"<script language='javascript'>alert('لا يمكن الحذف')</script>");
            };
        }
        protected void Edit(object sender, EventArgs e)
        {
            string id = (sender as LinkButton).CommandArgument;
            Response.Redirect("Edit_Sub_Type_Evaluation.aspx?Id=" + id);
        }
        protected void GoToAdd(object sender, EventArgs e)
        {
            Response.Redirect("Sub_Main_Type_Evaluation.aspx");
        }







































        protected void Delete_Sub_Type_Evaluation(object sender, EventArgs e)
        {
            string Sub_Type_EvaluationeRowId = (sender as LinkButton).CommandArgument;
            _sqlCon.Open();
            string deleteSub_Type_EvaluationeQuary = "DELETE FROM sub_type_evaluation WHERE Sub_Type_Evaluation_Id=@ID ";
            MySqlCommand mySqlCmd = new MySqlCommand(deleteSub_Type_EvaluationeQuary, _sqlCon);
            mySqlCmd.Parameters.AddWithValue("@ID", Sub_Type_EvaluationeRowId);
            mySqlCmd.ExecuteNonQuery();
            _sqlCon.Close();
            Response.Redirect(Request.RawUrl);
        }

        protected void Unnamed_ServerClick(object sender, EventArgs e)
        {
            Response.Redirect("Sub_Main_Type_Evaluation.aspx");
        }

        protected void The_Table_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (Session["Langues"] == null) { Session["Langues"] = "1"; }
            if (e.Item.ItemType == ListItemType.Header)
            {
                Label lbl_1 = (e.Item.FindControl("lbl_1") as Label);
                Label lbl_2 = (e.Item.FindControl("lbl_2") as Label);
                Label lbl_3 = (e.Item.FindControl("lbl_3") as Label);
                Label lbl_4 = (e.Item.FindControl("lbl_4") as Label);

                DataTable Dt = new DataTable();
                MySqlCommand Cmd = new MySqlCommand("SELECT * FROM languages_master", _sqlCon);
                MySqlDataAdapter Da = new MySqlDataAdapter(Cmd);
                Da.Fill(Dt);
                if (Dt.Rows.Count > 0)
                {
                    if (Session["Langues"].ToString() == "1")
                    {
                        lbl_1.Text = Dt.Rows[272]["EN"].ToString();
                        lbl_2.Text = Dt.Rows[273]["EN"].ToString();
                        lbl_3.Text = Dt.Rows[109]["EN"].ToString();
                        lbl_4.Text = Dt.Rows[110]["EN"].ToString();
                    }
                    else
                    {
                        lbl_1.Text = Dt.Rows[272]["AR"].ToString();
                        lbl_2.Text = Dt.Rows[273]["AR"].ToString();
                        lbl_3.Text = Dt.Rows[109]["AR"].ToString();
                        lbl_4.Text = Dt.Rows[110]["AR"].ToString();
                    }
                }
            }

            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                Label lbl_Main_Ar_Name = (e.Item.FindControl("lbl_Main_Ar_Name") as Label);
                Label lbl_Main_En_Name = (e.Item.FindControl("lbl_Main_En_Name") as Label);
                Label lbl_Ar_Name = (e.Item.FindControl("lbl_Ar_Name") as Label);
                Label lbl_En_Name = (e.Item.FindControl("lbl_En_Name") as Label);

                if (Session["Langues"].ToString() == "1")
                {
                    lbl_Main_Ar_Name.Visible = false; lbl_Main_En_Name.Visible = true;
                    lbl_Ar_Name.Visible = false; lbl_En_Name.Visible = true;
                }
                else
                {
                    lbl_Main_Ar_Name.Visible = true; lbl_Main_En_Name.Visible = false;
                    lbl_Ar_Name.Visible = true; lbl_En_Name.Visible = false;
                }
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
                    lbl_titel.Text = Dt.Rows[271]["EN"].ToString();
                    ADD.Text = Dt.Rows[54]["EN"].ToString();
                }
                else
                {
                    lbl_titel.Text = Dt.Rows[271]["AR"].ToString();
                    ADD.Text = Dt.Rows[54]["AR"].ToString();
                }
            }
            _sqlCon.Close();

        }
    }
}