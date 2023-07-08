﻿using Main_Real_estate.Utilities;
using MySql.Data.MySqlClient;
using System;
using System.Data;
using System.Drawing;
using System.Web.UI.WebControls;

namespace Main_Real_estate.English.Master_Panal
{
    public partial class Add_Role : System.Web.UI.Page
    {
        private readonly MySqlConnection _sqlCon = Helper.GetConnection();

        string Pro_R = ""; string Pro_A = ""; string Pro_E = ""; string Pro_D = "";
        string Con_R = ""; string Con_A = ""; string Con_E = ""; string Con_D = "";
        string Te_R = ""; string Te_A = ""; string Te_E = ""; string Te_D = "";
        string AsM_R = ""; string AsM_A = ""; string AsM_E = ""; string AsM_D = "";
        string Col_R = ""; string Col_E = "";
        string FS_R = ""; string FS_A = ""; string FS_E = ""; string FS_D = "";
        string Mar_R = ""; string Mar_A = ""; string Mar_E = ""; string Mar_D = "";
        string TM_R = ""; string TM_A = ""; string TM_E = ""; string TM_D = "";
        string MF_R = ""; string MF_E = "";
        string SS_R = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            language();
        }

        protected void btn_Add_Role_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                if (Pro_R_Cb.Checked == true) { Pro_R = "R"; } else { Pro_R = "0"; }
                if (Pro_A_Cb.Checked == true) { Pro_A = "A"; } else { Pro_A = "0"; }
                if (Pro_E_Cb.Checked == true) { Pro_E = "E"; } else { Pro_E = "0"; }
                if (Pro_D_Cb.Checked == true) { Pro_D = "D"; } else { Pro_D = "0"; }

                if (Con_R_Cb.Checked == true) { Con_R = "R"; } else { Con_R = "0"; }
                if (Con_A_Cb.Checked == true) { Con_A = "A"; } else { Con_A = "0"; }
                if (Con_E_Cb.Checked == true) { Con_E = "E"; } else { Con_E = "0"; }
                if (Con_D_Cb.Checked == true) { Con_D = "D"; } else { Con_D = "0"; }

                if (Te_R_Cb.Checked == true) { Te_R = "R"; } else { Te_R = "0"; }
                if (Te_A_Cb.Checked == true) { Te_A = "A"; } else { Te_A = "0"; }
                if (Te_E_Cb.Checked == true) { Te_E = "E"; } else { Te_E = "0"; }
                if (Te_D_Cb.Checked == true) { Te_D = "D"; } else { Te_D = "0"; }

                if (AsM_R_Cb.Checked == true) { AsM_R = "R"; } else { AsM_R = "0"; }
                if (AsM_A_Cb.Checked == true) { AsM_A = "A"; } else { AsM_A = "0"; }
                if (AsM_E_Cb.Checked == true) { AsM_E = "E"; } else { AsM_E = "0"; }
                if (AsM_D_Cb.Checked == true) { AsM_D = "D"; } else { AsM_D = "0"; }

                if (Col_R_Cb.Checked == true) { Col_R = "R"; } else { Col_R = "0"; }
                if (Col_E_Cb.Checked == true) { Col_E = "E"; } else { Col_E = "0"; }

                if (FS_R_Cb.Checked == true) { FS_R = "R"; } else { FS_R = "0"; }
                if (FS_A_Cb.Checked == true) { FS_A = "A"; } else { FS_A = "0"; }
                if (FS_E_Cb.Checked == true) { FS_E = "E"; } else { FS_E = "0"; }
                if (FS_D_Cb.Checked == true) { FS_D = "D"; } else { FS_D = "0"; }

                if (Mar_R_Cb.Checked == true) { Mar_R = "R"; } else { Mar_R = "0"; }
                if (Mar_A_Cb.Checked == true) { Mar_A = "A"; } else { Mar_A = "0"; }
                if (Mar_E_Cb.Checked == true) { Mar_E = "E"; } else { Mar_E = "0"; }
                if (Mar_D_Cb.Checked == true) { Mar_D = "D"; } else { Mar_D = "0"; }

                if (TM_R_Cb.Checked == true) { TM_R = "R"; } else { TM_R = "0"; }
                if (TM_A_Cb.Checked == true) { TM_A = "A"; } else { TM_A = "0"; }
                if (TM_E_Cb.Checked == true) { TM_E = "E"; } else { TM_E = "0"; }
                if (TM_D_Cb.Checked == true) { TM_D = "D"; } else { TM_D = "0"; }

                if (MF_R_Cb.Checked == true) { MF_R = "R"; } else { MF_R = "0"; }
                if (MF_E_Cb.Checked == true) { MF_E = "E"; } else { MF_E = "0"; }

                if (SS_R_Cb.Checked == true) { SS_R = "R"; } else { SS_R = "0"; }




                string Quary =
                "Insert Into roles (Role_name , properties , Contracting , Customer_Affairs , Asset_Management , Collecting , Financial_Statements , Marketing , Task_Management , Deficiency_Detection , System_Settings) " +
                "VALUES(@Role_name , @properties , @Contracting , @Customer_Affairs , @Asset_Management , @Collecting , @Financial_Statements , @Marketing , @Task_Management , @Deficiency_Detection , @System_Settings)";
                _sqlCon.Open();
                MySqlCommand Cmd = new MySqlCommand(Quary, _sqlCon);
                Cmd.Parameters.AddWithValue("@Role_name", txt_Role_Name.Text);

                Cmd.Parameters.AddWithValue("@properties", Pro_R + "," + Pro_A + "," + Pro_E + "," + Pro_D);
                Cmd.Parameters.AddWithValue("@Contracting", Con_R + "," + Con_A + "," + Con_E + "," + Con_D);
                Cmd.Parameters.AddWithValue("@Customer_Affairs", Te_R + "," + Te_A + "," + Te_E + "," + Te_D);
                Cmd.Parameters.AddWithValue("@Asset_Management", AsM_R + "," + AsM_A + "," + AsM_E + "," + AsM_D);
                Cmd.Parameters.AddWithValue("@Collecting", Col_R + ",0," + Col_E + ",0");
                Cmd.Parameters.AddWithValue("@Financial_Statements", FS_R + "," + FS_A + "," + FS_E + "," + FS_D);
                Cmd.Parameters.AddWithValue("@Marketing", Mar_R + "," + Mar_A + "," + Mar_E + "," + Mar_D);
                Cmd.Parameters.AddWithValue("@Task_Management", TM_R + "," + TM_A + "," + TM_E + "," + TM_D);
                Cmd.Parameters.AddWithValue("@Deficiency_Detection", MF_R + ",0," + MF_E + ",0");
                Cmd.Parameters.AddWithValue("@System_Settings", SS_R);

                Cmd.ExecuteNonQuery();
                _sqlCon.Close();
                lbl_Success_Add_New_Role.Text = "تمت الإضافة بنجاح ";
            }
        }

        protected void btn_Back_Click(object sender, EventArgs e)
        {
            Response.Redirect("Roles_List.aspx");
        }

        protected void Pro_A_Cb_CheckedChanged(object sender, EventArgs e)
        {
            if (Pro_A_Cb.Checked == true || Pro_E_Cb.Checked == true || Pro_D_Cb.Checked == true) { Pro_R_Cb.Checked = true; }
            if (Con_A_Cb.Checked == true || Con_E_Cb.Checked == true || Con_D_Cb.Checked == true) { Con_R_Cb.Checked = true; }
            if (Te_A_Cb.Checked == true || Te_E_Cb.Checked == true || Te_D_Cb.Checked == true) { Te_R_Cb.Checked = true; }
            if (AsM_A_Cb.Checked == true || AsM_E_Cb.Checked == true || AsM_D_Cb.Checked == true) { AsM_R_Cb.Checked = true; }
            if (Col_E_Cb.Checked == true) { Con_R_Cb.Checked = true; }

            if (FS_A_Cb.Checked == true || FS_E_Cb.Checked == true || FS_D_Cb.Checked == true) { FS_R_Cb.Checked = true; }
            if (Mar_A_Cb.Checked == true || Mar_E_Cb.Checked == true || Mar_D_Cb.Checked == true) { Mar_R_Cb.Checked = true; }
            if (TM_A_Cb.Checked == true || TM_E_Cb.Checked == true || TM_D_Cb.Checked == true) { TM_R_Cb.Checked = true; }
            if (MF_E_Cb.Checked == true) { MF_R_Cb.Checked = true; }
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
                    lbl_titel_Add_New_Role.Text = Dt.Rows[232]["EN"].ToString();
                    lbl_Role_Name.Text = Dt.Rows[233]["EN"].ToString();
                    btn_Add_Role.Text = Dt.Rows[54]["EN"].ToString();
                    btn_Back.Text = Dt.Rows[249]["EN"].ToString();


                    lbl_Module.Text = Dt.Rows[234]["EN"].ToString();
                    lbl_Read.Text = Dt.Rows[235]["EN"].ToString();
                    lbl_Add.Text = Dt.Rows[236]["EN"].ToString();
                    lbl_Edit.Text = Dt.Rows[237]["EN"].ToString();
                    lbl_Delete.Text = Dt.Rows[238]["EN"].ToString();
                    lbl_Module2.Text = Dt.Rows[234]["EN"].ToString();
                    lbl_Read2.Text = Dt.Rows[235]["EN"].ToString();
                    lbl_Add2.Text = Dt.Rows[236]["EN"].ToString();
                    lbl_Edit2.Text = Dt.Rows[237]["EN"].ToString();
                    lbl_Delete2.Text = Dt.Rows[238]["EN"].ToString();




                    lbl_Properties.Text = Dt.Rows[239]["EN"].ToString();
                    lbl_Contracting.Text = Dt.Rows[240]["EN"].ToString();
                    lbl_Customer_Affairs.Text = Dt.Rows[241]["EN"].ToString();
                    lbl_Asset_Management.Text = Dt.Rows[242]["EN"].ToString();
                    lbl_Collection.Text = Dt.Rows[243]["EN"].ToString();
                    lbl_Financial_Statements.Text = Dt.Rows[244]["EN"].ToString();
                    lbl_Marketing.Text = Dt.Rows[245]["EN"].ToString();
                    lbl_Task_Management.Text = Dt.Rows[246]["EN"].ToString();
                    lbl_Missing_Fields.Text = Dt.Rows[247]["EN"].ToString();
                    lbl_Settings.Text = Dt.Rows[248]["EN"].ToString();


                    Role_Name_Req_Val.ErrorMessage = "* Required";

                }
                else
                {
                    lbl_titel_Add_New_Role.Text = Dt.Rows[232]["AR"].ToString();
                    lbl_Role_Name.Text = Dt.Rows[233]["AR"].ToString();
                    btn_Add_Role.Text = Dt.Rows[54]["AR"].ToString();
                    btn_Back.Text = Dt.Rows[249]["AR"].ToString();


                    lbl_Module.Text = Dt.Rows[234]["AR"].ToString();
                    lbl_Read.Text = Dt.Rows[235]["AR"].ToString();
                    lbl_Add.Text = Dt.Rows[236]["AR"].ToString();
                    lbl_Edit.Text = Dt.Rows[237]["AR"].ToString();
                    lbl_Delete.Text = Dt.Rows[238]["AR"].ToString();
                    lbl_Module2.Text = Dt.Rows[234]["AR"].ToString();
                    lbl_Read2.Text = Dt.Rows[235]["AR"].ToString();
                    lbl_Add2.Text = Dt.Rows[236]["AR"].ToString();
                    lbl_Edit2.Text = Dt.Rows[237]["AR"].ToString();
                    lbl_Delete2.Text = Dt.Rows[238]["AR"].ToString();




                    lbl_Properties.Text = Dt.Rows[239]["AR"].ToString();
                    lbl_Contracting.Text = Dt.Rows[240]["AR"].ToString();
                    lbl_Customer_Affairs.Text = Dt.Rows[241]["AR"].ToString();
                    lbl_Asset_Management.Text = Dt.Rows[242]["AR"].ToString();
                    lbl_Collection.Text = Dt.Rows[243]["AR"].ToString();
                    lbl_Financial_Statements.Text = Dt.Rows[244]["AR"].ToString();
                    lbl_Marketing.Text = Dt.Rows[245]["AR"].ToString();
                    lbl_Task_Management.Text = Dt.Rows[246]["AR"].ToString();
                    lbl_Missing_Fields.Text = Dt.Rows[247]["AR"].ToString();
                    lbl_Settings.Text = Dt.Rows[248]["AR"].ToString();


                    Role_Name_Req_Val.ErrorMessage = "* مطلوب";

                }
            }
            _sqlCon.Close();

        }
    }
}