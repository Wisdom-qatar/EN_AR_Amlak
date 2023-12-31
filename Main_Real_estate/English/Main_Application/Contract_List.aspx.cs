﻿using Main_Real_estate.Utilities;
using MySql.Data.MySqlClient;
using Org.BouncyCastle.Asn1.X509;
using Syncfusion.JavaScript.Models;
using System;
using System.Data;
using System.Web;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace Main_Real_estate.English.Main_Application
{
    public partial class Contract_List : System.Web.UI.Page
    {
        private readonly MySqlConnection _sqlCon = Helper.GetConnection();

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                Utilities.Roles.Add_permission(_sqlCon, Session["Role"].ToString(), "Contracting", Add);
                Utilities.Roles.Singel_Page_permission(_sqlCon, Session["Role"].ToString(), "Contracting", 0, "R");
            }
            catch { Response.Redirect("Log_In.aspx"); }
            
            if (!IsPostBack)
            {
                language();
                BindDataAll();
                BindData();
                Building_BindData();
            }
        }


        protected void BindDataAll(string sortExpression = null)
        {
            try
            {
                using (MySqlCommand cmd = new MySqlCommand("All_Contract_List", _sqlCon))
                {
                    using (MySqlDataAdapter sda = new MySqlDataAdapter(cmd))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        DataTable dt = new DataTable();
                        sda.Fill(dt);
                        Repeater1.DataSource = dt;
                        Repeater1.DataBind();
                    }
                }
        }
            catch
            {
                Response.Write(
                    @"<script language='javascript'>alert('OOPS!!! The Contract List Cannt Display')</script>");
            }
}
        protected void contract_List_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            Label EndDate = null;
            Label New_ReNewed_Expaired = null;
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                HtmlTableRow tr = e.Item.FindControl("row") as HtmlTableRow;
                LinkButton ReNew_btn = (e.Item.FindControl("U_Renew") as LinkButton);
                LinkButton U_Finsh = (e.Item.FindControl("U_Finsh") as LinkButton);


                LinkButton U_delete = (e.Item.FindControl("U_delete") as LinkButton);
                LinkButton U_details = (e.Item.FindControl("U_details") as LinkButton);
                Label lbl_Done_Renew = (e.Item.FindControl("lbl_Done_Renew") as Label);
                EndDate = e.Item.FindControl("lbl_End_Date") as Label;
                New_ReNewed_Expaired = e.Item.FindControl("lbl_New_ReNewed_Expaired") as Label;

                string[] Array_End_Date = EndDate.Text.Split(new char[] { '/' });
                var prevDate = new DateTime(Convert.ToInt32(Array_End_Date[2]), Convert.ToInt32(Array_End_Date[1]), Convert.ToInt32(Array_End_Date[0]));
                var today = DateTime.Now;
                var diffOfDates = prevDate - today;
                int sub = diffOfDates.Days;



                if (New_ReNewed_Expaired.Text == "1")
                {
                    // If the days difference is > 60 and [New_ReNewed_Expaired = 1 ], the line color is green and the renewal button is hidden. 
                    if (diffOfDates.Days > 60)
                    {
                        tr.Attributes.Add("style", "background-color:#c5f8eb;color:#000000;");
                        ReNew_btn.Visible = false;
                    }
                    // If the days difference is <= 60 (2 months left until expiration) and New_ReNewed_Expaired = 1, the line color is red, and the renewal button is shown.
                    else if (diffOfDates.Days >= 0 && diffOfDates.Days <= 60)
                    {
                        tr.Attributes.Add("style", "background-color:#faced2;color:#000000;");
                        ReNew_btn.Visible = true;
                    }
                    // If days difference <= 0 (2 months more than expiry) and New_ReNewed_Expaired = 1 line color black show renewal button
                    else if (diffOfDates.Days <= 0)
                    {
                        tr.Attributes.Add("style", "background-color:#cbd0d8;color:#000000;");
                        ReNew_btn.Visible = true; U_Finsh.Visible = false; tr.Visible = false;
                    }
                }
                else if (New_ReNewed_Expaired.Text == "2")
                {
                    tr.Attributes.Add("style", "background-color:#cbd0d8;color:#000000;");
                    ReNew_btn.Visible = false; U_Finsh.Visible = false; tr.Visible = false;
                }
                else if (New_ReNewed_Expaired.Text == "3")
                {
                    tr.Attributes.Add("style", "background-color:#cbd0d8;color:#000000;");
                    ReNew_btn.Visible = false; U_Finsh.Visible = false; tr.Visible = false;
                }
            }




            try
            {
                DataTable Dt = new DataTable();
                _sqlCon.Open();
                MySqlCommand Cmd = new MySqlCommand("SELECT * FROM roles WHERE Role_ID = @ID", _sqlCon);
                MySqlDataAdapter Da = new MySqlDataAdapter(Cmd);
                Cmd.Parameters.AddWithValue("@ID", Session["Role"].ToString());
                Da.Fill(Dt);
                if (Dt.Rows.Count > 0)
                {
                    string[] Page = Dt.Rows[0]["Contracting"].ToString().Split(',');
                    if (Page[2] != "E")
                    {
                        if (e.Item.ItemType == ListItemType.Header)
                        {
                            var H_One = e.Item.FindControl("H_One") as HtmlTableCell;
                            H_One.Visible = false;
                        }
                        if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
                        {
                            var B_One = e.Item.FindControl("B_One") as HtmlTableCell;
                            B_One.Visible = false;
                        }
                    }
                }
                _sqlCon.Close();
            }
            catch
            {
                Response.Redirect("Log_In.aspx");
            }



            //******************** Langueges *********************************************************
            if (e.Item.ItemType == ListItemType.Header)
            {
                var lbl_Contract_NO = e.Item.FindControl("lbl_Contract_NO") as Label;
                var lbl_Zone = e.Item.FindControl("lbl_Zone") as Label;
                var lbl_Code = e.Item.FindControl("lbl_Code") as Label;
                var lbl_Ownership = e.Item.FindControl("lbl_Ownership") as Label;
                var lbl_Rented_Item = e.Item.FindControl("lbl_Rented_Item") as Label;
                var lbl_Tenant = e.Item.FindControl("lbl_Tenant") as Label;
                var lbl_Nationality = e.Item.FindControl("lbl_Nationality") as Label;
                var lbl_Contract_type = e.Item.FindControl("lbl_Contract_type") as Label;
                var lbl_Years = e.Item.FindControl("lbl_Years") as Label;
                var lbl_Value = e.Item.FindControl("lbl_Value") as Label;
                var lbl_Start = e.Item.FindControl("lbl_Start") as Label;
                var lbl_End = e.Item.FindControl("lbl_End") as Label;


                _sqlCon.Open();
                DataTable Dt1 = new DataTable();
                MySqlCommand Cmd1 = new MySqlCommand("SELECT * FROM languages_contract", _sqlCon);
                MySqlDataAdapter Da1 = new MySqlDataAdapter(Cmd1);
                Da1.Fill(Dt1);
                if (Dt1.Rows.Count > 0)
                {
                    if (Session["Langues"].ToString() == "1")
                    {
                        lbl_Contract_NO.Text = Dt1.Rows[53]["EN"].ToString();
                        lbl_Zone.Text = Dt1.Rows[54]["EN"].ToString();
                        lbl_Code.Text = Dt1.Rows[55]["EN"].ToString();
                        lbl_Ownership.Text = Dt1.Rows[4]["EN"].ToString();
                        lbl_Rented_Item.Text = "Rented Item";
                        lbl_Tenant.Text = Dt1.Rows[2]["EN"].ToString();
                        lbl_Nationality.Text = Dt1.Rows[56]["EN"].ToString();
                        lbl_Contract_type.Text = Dt1.Rows[1]["EN"].ToString();
                        lbl_Years.Text = Dt1.Rows[57]["EN"].ToString();
                        lbl_Value.Text = Dt1.Rows[58]["EN"].ToString();
                        lbl_Start.Text = Dt1.Rows[10]["EN"].ToString();
                        lbl_End.Text = Dt1.Rows[11]["EN"].ToString();

                    }
                    else
                    {
                        lbl_Contract_NO.Text = Dt1.Rows[53]["AR"].ToString();
                        lbl_Zone.Text = Dt1.Rows[54]["AR"].ToString();
                        lbl_Code.Text = Dt1.Rows[55]["AR"].ToString();
                        lbl_Ownership.Text = Dt1.Rows[4]["AR"].ToString();
                        lbl_Rented_Item.Text = "العنصر المؤجر";
                        lbl_Tenant.Text = Dt1.Rows[2]["AR"].ToString();
                        lbl_Nationality.Text = Dt1.Rows[56]["AR"].ToString();
                        lbl_Contract_type.Text = Dt1.Rows[1]["AR"].ToString();
                        lbl_Years.Text = Dt1.Rows[57]["AR"].ToString();
                        lbl_Value.Text = Dt1.Rows[58]["AR"].ToString();
                        lbl_Start.Text = Dt1.Rows[10]["AR"].ToString();
                        lbl_End.Text = Dt1.Rows[11]["AR"].ToString();

                    }
                }
                _sqlCon.Close();
            }
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                var lbl_zone_arabic_name = e.Item.FindControl("lbl_zone_arabic_name") as Label;
                var lbl_zone_English_name = e.Item.FindControl("lbl_zone_English_name") as Label;
                var lbl_Owner_Ship_AR_Name = e.Item.FindControl("lbl_Owner_Ship_AR_Name") as Label;
                var lbl_Owner_Ship_EN_Name = e.Item.FindControl("lbl_Owner_Ship_EN_Name") as Label;
                var lbl_Unit_Number = e.Item.FindControl("lbl_Unit_Number") as Label;
                var lbl_EN_Unit_Number = e.Item.FindControl("lbl_EN_Unit_Number") as Label;
                var lbl_Tenants_Arabic_Name = e.Item.FindControl("lbl_Tenants_Arabic_Name") as Label;
                var lbl_Tenants_English_Name = e.Item.FindControl("lbl_Tenants_English_Name") as Label;
                var lbl_Arabic_nationality = e.Item.FindControl("lbl_Arabic_nationality") as Label;
                var lbl_English_nationality = e.Item.FindControl("lbl_English_nationality") as Label;
                var lbl_Contract_Type = e.Item.FindControl("lbl_Contract_Type") as Label;
                var lbl_Contract_English_Type = e.Item.FindControl("lbl_Contract_English_Type") as Label;

                if (Session["Langues"].ToString() == "1")
                {
                    lbl_zone_arabic_name.Visible= false; lbl_zone_English_name.Visible = true;
                    lbl_Owner_Ship_AR_Name.Visible = false; lbl_Owner_Ship_EN_Name.Visible = true;
                    lbl_Unit_Number.Visible = false; lbl_EN_Unit_Number.Visible = true;
                    lbl_Tenants_Arabic_Name.Visible = false; lbl_Tenants_English_Name.Visible = true;
                    lbl_Arabic_nationality.Visible = false; lbl_English_nationality.Visible = true;
                    lbl_Contract_Type.Visible = false; lbl_Contract_English_Type.Visible = true;
                }
                else
                {
                    lbl_zone_arabic_name.Visible = true; lbl_zone_English_name.Visible = false;
                    lbl_Owner_Ship_AR_Name.Visible = true; lbl_Owner_Ship_EN_Name.Visible = false;
                    lbl_Unit_Number.Visible = true; lbl_EN_Unit_Number.Visible = false;
                    lbl_Tenants_Arabic_Name.Visible = true; lbl_Tenants_English_Name.Visible = false;
                    lbl_Arabic_nationality.Visible = true; lbl_English_nationality.Visible = false;
                    lbl_Contract_Type.Visible = true; lbl_Contract_English_Type.Visible = false;
                }
            }


        }

        protected void BindData(string sortExpression = null)
        {
            try
            {
                using (MySqlCommand cmd = new MySqlCommand("Contarct_list", _sqlCon))
                {
                    using (MySqlDataAdapter sda = new MySqlDataAdapter(cmd))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        DataTable dt = new DataTable();
                        sda.Fill(dt);
                        contract_List.DataSource = dt;
                        contract_List.DataBind();
                    }
                }
            }
            catch
            {
                Response.Write(
                    @"<script language='javascript'>alert('OOPS!!! The Contract List Cannt Display')</script>");
            }
        }

        protected void Building_BindData(string sortExpression = null)
        {
            try
            {
                using (MySqlCommand cmd = new MySqlCommand("Building_Contarct_list", _sqlCon))
                {
                    using (MySqlDataAdapter sda = new MySqlDataAdapter(cmd))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        DataTable dt = new DataTable();
                        sda.Fill(dt);
                        Building_Contarct_List.DataSource = dt;
                        Building_Contarct_List.DataBind();
                    }
                }
            }
            catch
            {
                Response.Write(@"<script language='javascript'>alert('OOPS!!! The Contract List Cannt Display')</script>");
            }
        }

        protected void Delete_Contract(object sender, EventArgs e)
        {
            try
            {
                string id = (sender as LinkButton).CommandArgument;
                _sqlCon.Open();
                string quary1 = "DELETE FROM contract WHERE Contract_Id=@ID ";
                MySqlCommand mySqlCmd = new MySqlCommand(quary1, _sqlCon);
                mySqlCmd.Parameters.AddWithValue("@ID", id);
                mySqlCmd.ExecuteNonQuery();
                _sqlCon.Close();

                Response.Redirect(Request.RawUrl);
            }
            catch
            {
                Response.Write(
                    @"<script language='javascript'>alert('This Unit Cannot Be Removed!!! Because It Contains  Tenants')</script>");
            }
        }



        //****************************************************************************************************************************************
        protected void U_Edit_Click(object sender, EventArgs e)
        {
            string id = (sender as LinkButton).CommandArgument;
            string[] Array_id = id.Split('/');
            if (Array_id[0] == "U") { Response.Redirect("Edit_Contract.aspx?Id=" + Array_id[1]); }
            else { Response.Redirect("Edit_Building_Contract.aspx?Id=" + Array_id[1]); }
        }
        protected void Edit_Contract(object sender, EventArgs e)
        {
            string id = (sender as LinkButton).CommandArgument;
            Response.Redirect("Edit_Contract.aspx?Id=" + id);
        }

        protected void Edit_B_Contract(object sender, EventArgs e)
        {
            string id = (sender as LinkButton).CommandArgument;
            Response.Redirect("Edit_Building_Contract.aspx?Id=" + id);
        }

        //****************************************************************************************************************************************
        protected void Details_Contract(object sender, EventArgs e)
        {
            string id = (sender as LinkButton).CommandArgument;
            Response.Redirect("Contract_Details.aspx?Id=" + id);


            //Response.Write("<script>window.open ('Contract_Details.aspx?Id=" + id + "','_blank');</script>");
        }

        protected void Details_B_Contract(object sender, EventArgs e)
        {
            string id = (sender as LinkButton).CommandArgument;
            Response.Redirect("Building_Contract_Details.aspx?Id=" + id);
        }

        //****************************************************************************************************************************************
        protected void U_Renew_Click(object sender, EventArgs e)
        {
            string id = (sender as LinkButton).CommandArgument;
            string[] Array_id = id.Split('/');
            if (Array_id[0] == "U") { Response.Redirect("Renew_Contract.aspx?Id=" + Array_id[1]); }
            else { Response.Redirect("Renew_Building_Contract.aspx?Id=" + Array_id[1]); }
        }
        protected void Renew_Contract(object sender, EventArgs e)
        {
            string id = (sender as LinkButton).CommandArgument;
            Response.Redirect("Renew_Contract.aspx?Id=" + id);
        }

        protected void Renew_B_Contract(object sender, EventArgs e)
        {
            string id = (sender as LinkButton).CommandArgument;
            Response.Redirect("Renew_Building_Contract.aspx?Id=" + id);
        }

        //****************************************************************************************************************************************

        

        protected void contract_List_ItemDataBound1(object sender, RepeaterItemEventArgs e)
        {
            Label EndDate = null;
            Label New_ReNewed_Expaired = null;
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                HtmlTableRow tr = e.Item.FindControl("row") as HtmlTableRow;
                LinkButton ReNew_btn = (e.Item.FindControl("U_Renew") as LinkButton);
                LinkButton U_Finsh = (e.Item.FindControl("U_Finsh") as LinkButton);


                LinkButton U_delete = (e.Item.FindControl("U_delete") as LinkButton);
                LinkButton U_details = (e.Item.FindControl("U_details") as LinkButton);
                Label lbl_Done_Renew = (e.Item.FindControl("lbl_Done_Renew") as Label);
                EndDate = e.Item.FindControl("lbl_End_Date") as Label;
                New_ReNewed_Expaired = e.Item.FindControl("lbl_New_ReNewed_Expaired") as Label;

                string[] Array_End_Date = EndDate.Text.Split(new char[] { '/' });
                var prevDate = new DateTime(Convert.ToInt32(Array_End_Date[2]), Convert.ToInt32(Array_End_Date[1]), Convert.ToInt32(Array_End_Date[0]));
                var today = DateTime.Now;
                var diffOfDates = prevDate - today;
                int sub = diffOfDates.Days;



                if (New_ReNewed_Expaired.Text == "1")
                {
                    // If the days difference is > 60 and [New_ReNewed_Expaired = 1 ], the line color is green and the renewal button is hidden. 
                    if (diffOfDates.Days > 60)
                    {
                        tr.Attributes.Add("style", "background-color:#c5f8eb;color:#000000;");
                        ReNew_btn.Visible = false;
                    }
                    // If the days difference is <= 60 (2 months left until expiration) and New_ReNewed_Expaired = 1, the line color is red, and the renewal button is shown.
                    else if (diffOfDates.Days >= 0 && diffOfDates.Days <= 60)
                    {
                        tr.Attributes.Add("style", "background-color:#faced2;color:#000000;");
                        ReNew_btn.Visible = true;
                    }
                    // If days difference <= 0 (2 months more than expiry) and New_ReNewed_Expaired = 1 line color black show renewal button
                    else if (diffOfDates.Days <= 0)
                    {
                        tr.Attributes.Add("style", "background-color:#cbd0d8;color:#000000;");
                        ReNew_btn.Visible = true; U_Finsh.Visible = false; tr.Visible = false;
                    }
                }
                else if (New_ReNewed_Expaired.Text == "2")
                {
                    tr.Attributes.Add("style", "background-color:#cbd0d8;color:#000000;");
                    ReNew_btn.Visible = false; U_Finsh.Visible = false; tr.Visible = false;
                }
                else if (New_ReNewed_Expaired.Text == "3")
                {
                    tr.Attributes.Add("style", "background-color:#cbd0d8;color:#000000;");
                    ReNew_btn.Visible = false; U_Finsh.Visible = false; tr.Visible = false;
                }
            }




            try
            {
                DataTable Dt = new DataTable();
                _sqlCon.Open();
                MySqlCommand Cmd = new MySqlCommand("SELECT * FROM roles WHERE Role_ID = @ID", _sqlCon);
                MySqlDataAdapter Da = new MySqlDataAdapter(Cmd);
                Cmd.Parameters.AddWithValue("@ID", Session["Role"].ToString());
                Da.Fill(Dt);
                if (Dt.Rows.Count > 0)
                {
                    string[] Page = Dt.Rows[0]["Contracting"].ToString().Split(',');
                    if (Page[2] != "E")
                    {
                        if (e.Item.ItemType == ListItemType.Header)
                        {
                            var H_One = e.Item.FindControl("H_One") as HtmlTableCell;
                            H_One.Visible = false;
                        }
                        if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
                        {
                            var B_One = e.Item.FindControl("B_One") as HtmlTableCell;
                            B_One.Visible = false;
                        }
                    }
                }
                _sqlCon.Close();
            }
            catch
            {
                Response.Redirect("Log_In.aspx");
            }


            //******************** Langueges *********************************************************
            if (e.Item.ItemType == ListItemType.Header)
            {
                var lbl_Contract_NO = e.Item.FindControl("lbl_Contract_NO") as Label;
                var lbl_Zone = e.Item.FindControl("lbl_Zone") as Label;
                var lbl_Code = e.Item.FindControl("lbl_Code") as Label;
                var lbl_Ownership = e.Item.FindControl("lbl_Ownership") as Label;
                var lbl_Building = e.Item.FindControl("lbl_Building") as Label;
                var lbl_Unit = e.Item.FindControl("lbl_Unit") as Label;
                var lbl_Tenant = e.Item.FindControl("lbl_Tenant") as Label;
                var lbl_Nationality = e.Item.FindControl("lbl_Nationality") as Label;
                var lbl_Contract_type = e.Item.FindControl("lbl_Contract_type") as Label;
                var lbl_Years = e.Item.FindControl("lbl_Years") as Label;
                var lbl_Value = e.Item.FindControl("lbl_Value") as Label;
                var lbl_Start = e.Item.FindControl("lbl_Start") as Label;
                var lbl_End = e.Item.FindControl("lbl_End") as Label;


                _sqlCon.Open();
                DataTable Dt1 = new DataTable();
                MySqlCommand Cmd1 = new MySqlCommand("SELECT * FROM languages_contract", _sqlCon);
                MySqlDataAdapter Da1 = new MySqlDataAdapter(Cmd1);
                Da1.Fill(Dt1);
                if (Dt1.Rows.Count > 0)
                {
                    if (Session["Langues"].ToString() == "1")
                    {
                        lbl_Contract_NO.Text = Dt1.Rows[53]["EN"].ToString();
                        lbl_Zone.Text = Dt1.Rows[54]["EN"].ToString();
                        lbl_Code.Text = Dt1.Rows[55]["EN"].ToString();
                        lbl_Ownership.Text = Dt1.Rows[4]["EN"].ToString();
                        lbl_Building.Text = Dt1.Rows[5]["EN"].ToString();
                        lbl_Unit.Text = Dt1.Rows[6]["EN"].ToString();
                        lbl_Tenant.Text = Dt1.Rows[2]["EN"].ToString();
                        lbl_Nationality.Text = Dt1.Rows[56]["EN"].ToString();
                        lbl_Contract_type.Text = Dt1.Rows[1]["EN"].ToString();
                        lbl_Years.Text = Dt1.Rows[57]["EN"].ToString();
                        lbl_Value.Text = Dt1.Rows[58]["EN"].ToString();
                        lbl_Start.Text = Dt1.Rows[10]["EN"].ToString();
                        lbl_End.Text = Dt1.Rows[11]["EN"].ToString();

                    }
                    else
                    {
                        lbl_Contract_NO.Text = Dt1.Rows[53]["AR"].ToString();
                        lbl_Zone.Text = Dt1.Rows[54]["AR"].ToString();
                        lbl_Code.Text = Dt1.Rows[55]["AR"].ToString();
                        lbl_Ownership.Text = Dt1.Rows[4]["AR"].ToString();
                        lbl_Building.Text = Dt1.Rows[5]["AR"].ToString();
                        lbl_Unit.Text = Dt1.Rows[6]["AR"].ToString();
                        lbl_Tenant.Text = Dt1.Rows[2]["AR"].ToString();
                        lbl_Nationality.Text = Dt1.Rows[56]["AR"].ToString();
                        lbl_Contract_type.Text = Dt1.Rows[1]["AR"].ToString();
                        lbl_Years.Text = Dt1.Rows[57]["AR"].ToString();
                        lbl_Value.Text = Dt1.Rows[58]["AR"].ToString();
                        lbl_Start.Text = Dt1.Rows[10]["AR"].ToString();
                        lbl_End.Text = Dt1.Rows[11]["AR"].ToString();

                    }
                }
                _sqlCon.Close();
            }

            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                var lbl_zone_arabic_name = e.Item.FindControl("lbl_zone_arabic_name") as Label;
                var lbl_zone_English_name = e.Item.FindControl("lbl_zone_English_name") as Label;
                var lbl_Owner_Ship_AR_Name = e.Item.FindControl("lbl_Owner_Ship_AR_Name") as Label;
                var lbl_Owner_Ship_EN_Name = e.Item.FindControl("lbl_Owner_Ship_EN_Name") as Label;

                var lbl_Building_Arabic_Name = e.Item.FindControl("lbl_Building_Arabic_Name") as Label;
                var lbl_Building_English_Name = e.Item.FindControl("lbl_Building_English_Name") as Label;

                var lbl_Tenants_Arabic_Name = e.Item.FindControl("lbl_Tenants_Arabic_Name") as Label;
                var lbl_Tenants_English_Name = e.Item.FindControl("lbl_Tenants_English_Name") as Label;
                var lbl_Arabic_nationality = e.Item.FindControl("lbl_Arabic_nationality") as Label;
                var lbl_English_nationality = e.Item.FindControl("lbl_English_nationality") as Label;
                var lbl_Contract_Type = e.Item.FindControl("lbl_Contract_Type") as Label;
                var lbl_Contract_English_Type = e.Item.FindControl("lbl_Contract_English_Type") as Label;

                if (Session["Langues"].ToString() == "1")
                {
                    lbl_zone_arabic_name.Visible = false; lbl_zone_English_name.Visible = true;
                    lbl_Owner_Ship_AR_Name.Visible = false; lbl_Owner_Ship_EN_Name.Visible = true;
                    lbl_Building_Arabic_Name.Visible = false; lbl_Building_English_Name.Visible = true;
                    lbl_Tenants_Arabic_Name.Visible = false; lbl_Tenants_English_Name.Visible = true;
                    lbl_Arabic_nationality.Visible = false; lbl_English_nationality.Visible = true;
                    lbl_Contract_Type.Visible = false; lbl_Contract_English_Type.Visible = true;
                }
                else
                {
                    lbl_zone_arabic_name.Visible = true; lbl_zone_English_name.Visible = false;
                    lbl_Owner_Ship_AR_Name.Visible = true; lbl_Owner_Ship_EN_Name.Visible = false;
                    lbl_Building_Arabic_Name.Visible = true; lbl_Building_English_Name.Visible = false;
                    lbl_Tenants_Arabic_Name.Visible = true; lbl_Tenants_English_Name.Visible = false;
                    lbl_Arabic_nationality.Visible = true; lbl_English_nationality.Visible = false;
                    lbl_Contract_Type.Visible = true; lbl_Contract_English_Type.Visible = false;
                }
            }

        }

        protected void Building_Contarct_List_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            Label EndDate = null;
            Label New_ReNewed_Expaired = null;
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                HtmlTableRow tr = e.Item.FindControl("row") as HtmlTableRow;
                LinkButton ReNew_btn = (e.Item.FindControl("B_Renew") as LinkButton);
                LinkButton B_Edit = (e.Item.FindControl("B_Edit") as LinkButton);
                LinkButton B_Finsh = (e.Item.FindControl("B_Finsh") as LinkButton);
                LinkButton B_details = (e.Item.FindControl("B_details") as LinkButton);
                Label lbl_Done_Renew = (e.Item.FindControl("lbl_Done_Renew") as Label);
                EndDate = e.Item.FindControl("lbl_End_Date") as Label;
                New_ReNewed_Expaired = e.Item.FindControl("lbl_New_ReNewed_Expaired") as Label;


                string[] Array_End_Date = EndDate.Text.Split(new char[] { '/' });
                var prevDate = new DateTime(Convert.ToInt32(Array_End_Date[2]), Convert.ToInt32(Array_End_Date[1]),
                    Convert.ToInt32(Array_End_Date[0]));
                var today = DateTime.Now;
                var diffOfDates = prevDate - today;
                int sub = diffOfDates.Days;


                if (New_ReNewed_Expaired.Text == "1")
                {
                    // If the days difference is > 60 and [New_ReNewed_Expaired = 1 ], the line color is green and the renewal button is hidden. 
                    if (diffOfDates.Days > 60)
                    {
                        tr.Attributes.Add("style", "background-color:#c5f8eb;color:#000000;");
                        ReNew_btn.Visible = false;
                    }
                    // If the days difference is <= 60 (2 months left until expiration) and New_ReNewed_Expaired = 1, the line color is red, and the renewal button is shown.
                    else if (diffOfDates.Days >= 0 && diffOfDates.Days <= 60)
                    {
                        tr.Attributes.Add("style", "background-color:#faced2;color:#000000;");
                        ReNew_btn.Visible = true;
                    }
                    // If days difference <= 0 (2 months more than expiry) and New_ReNewed_Expaired = 1 line color black show renewal button
                    else if (diffOfDates.Days <= 0)
                    {
                        tr.Attributes.Add("style", "background-color:#cbd0d8;color:#000000;");
                        ReNew_btn.Visible = true; B_Finsh.Visible = false; tr.Visible = false;
                    }
                }
                else if (New_ReNewed_Expaired.Text == "2")
                {
                    tr.Attributes.Add("style", "background-color:#cbd0d8;color:#000000;");
                    ReNew_btn.Visible = false; B_Finsh.Visible = false; tr.Visible = false;
                }
                else if (New_ReNewed_Expaired.Text == "3")
                {
                    tr.Attributes.Add("style", "background-color:#cbd0d8;color:#000000;");
                    ReNew_btn.Visible = false; B_Finsh.Visible = false; tr.Visible = false;
                }
            }




            try
            {
                DataTable Dt = new DataTable();
                _sqlCon.Open();
                MySqlCommand Cmd = new MySqlCommand("SELECT * FROM roles WHERE Role_ID = @ID", _sqlCon);
                MySqlDataAdapter Da = new MySqlDataAdapter(Cmd);
                Cmd.Parameters.AddWithValue("@ID", Session["Role"].ToString());
                Da.Fill(Dt);
                if (Dt.Rows.Count > 0)
                {
                    string[] Page = Dt.Rows[0]["Contracting"].ToString().Split(',');
                    if (Page[2] != "E")
                    {
                        if (e.Item.ItemType == ListItemType.Header)
                        {
                            var H_Two = e.Item.FindControl("H_Two") as HtmlTableCell;
                            var H_Two_Two = e.Item.FindControl("H_Two_Two") as HtmlTableCell;
                            H_Two.Visible = false; H_Two_Two.Visible = false;
                        }
                        if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
                        {
                            var B_Two = e.Item.FindControl("B_Two") as HtmlTableCell;
                            var B_Two_Two = e.Item.FindControl("B_Two_Two") as HtmlTableCell;
                            B_Two.Visible = false; B_Two_Two.Visible = false;
                        }
                    }
                }
                _sqlCon.Close();
            }
            catch
            {
                Response.Redirect("Log_In.aspx");
            }


            //******************** Langueges *********************************************************
            if (e.Item.ItemType == ListItemType.Header)
            {
                var lbl_Contract_NO = e.Item.FindControl("lbl_Contract_NO") as Label;
                var lbl_Zone = e.Item.FindControl("lbl_Zone") as Label;
                var lbl_Code = e.Item.FindControl("lbl_Code") as Label;
                var lbl_Ownership = e.Item.FindControl("lbl_Ownership") as Label;
                var lbl_Building = e.Item.FindControl("lbl_Building") as Label;
                var lbl_Unit = e.Item.FindControl("lbl_Unit") as Label;
                var lbl_Tenant = e.Item.FindControl("lbl_Tenant") as Label;
                var lbl_Nationality = e.Item.FindControl("lbl_Nationality") as Label;
                var lbl_Contract_type = e.Item.FindControl("lbl_Contract_type") as Label;
                var lbl_Years = e.Item.FindControl("lbl_Years") as Label;
                var lbl_Value = e.Item.FindControl("lbl_Value") as Label;
                var lbl_Start = e.Item.FindControl("lbl_Start") as Label;
                var lbl_End = e.Item.FindControl("lbl_End") as Label;


                _sqlCon.Open();
                DataTable Dt1 = new DataTable();
                MySqlCommand Cmd1 = new MySqlCommand("SELECT * FROM languages_contract", _sqlCon);
                MySqlDataAdapter Da1 = new MySqlDataAdapter(Cmd1);
                Da1.Fill(Dt1);
                if (Dt1.Rows.Count > 0)
                {
                    if (Session["Langues"].ToString() == "1")
                    {
                        lbl_Contract_NO.Text = Dt1.Rows[53]["EN"].ToString();
                        lbl_Zone.Text = Dt1.Rows[54]["EN"].ToString();
                        lbl_Code.Text = Dt1.Rows[55]["EN"].ToString();
                        lbl_Ownership.Text = Dt1.Rows[4]["EN"].ToString();
                        lbl_Building.Text = Dt1.Rows[5]["EN"].ToString();
                        lbl_Tenant.Text = Dt1.Rows[2]["EN"].ToString();
                        lbl_Nationality.Text = Dt1.Rows[56]["EN"].ToString();
                        lbl_Contract_type.Text = Dt1.Rows[1]["EN"].ToString();
                        lbl_Years.Text = Dt1.Rows[57]["EN"].ToString();
                        lbl_Value.Text = Dt1.Rows[58]["EN"].ToString();
                        lbl_Start.Text = Dt1.Rows[10]["EN"].ToString();
                        lbl_End.Text = Dt1.Rows[11]["EN"].ToString();

                    }
                    else
                    {
                        lbl_Contract_NO.Text = Dt1.Rows[53]["AR"].ToString();
                        lbl_Zone.Text = Dt1.Rows[54]["AR"].ToString();
                        lbl_Code.Text = Dt1.Rows[55]["AR"].ToString();
                        lbl_Ownership.Text = Dt1.Rows[4]["AR"].ToString();
                        lbl_Building.Text = Dt1.Rows[5]["AR"].ToString();
                        lbl_Tenant.Text = Dt1.Rows[2]["AR"].ToString();
                        lbl_Nationality.Text = Dt1.Rows[56]["AR"].ToString();
                        lbl_Contract_type.Text = Dt1.Rows[1]["AR"].ToString();
                        lbl_Years.Text = Dt1.Rows[57]["AR"].ToString();
                        lbl_Value.Text = Dt1.Rows[58]["AR"].ToString();
                        lbl_Start.Text = Dt1.Rows[10]["AR"].ToString();
                        lbl_End.Text = Dt1.Rows[11]["AR"].ToString();

                    }
                }
                _sqlCon.Close();
            }

            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                var lbl_zone_arabic_name = e.Item.FindControl("lbl_zone_arabic_name") as Label;
                var lbl_zone_English_name = e.Item.FindControl("lbl_zone_English_name") as Label;
                var lbl_Owner_Ship_AR_Name = e.Item.FindControl("lbl_Owner_Ship_AR_Name") as Label;
                var lbl_Owner_Ship_EN_Name = e.Item.FindControl("lbl_Owner_Ship_EN_Name") as Label;
                var lbl_Building_Arabic_Name = e.Item.FindControl("lbl_Building_Arabic_Name") as Label;
                var lbl_Building_English_Name = e.Item.FindControl("lbl_Building_English_Name") as Label;
                var lbl_Tenants_Arabic_Name = e.Item.FindControl("lbl_Tenants_Arabic_Name") as Label;
                var lbl_Tenants_English_Name = e.Item.FindControl("lbl_Tenants_English_Name") as Label;
                var lbl_Arabic_nationality = e.Item.FindControl("lbl_Arabic_nationality") as Label;
                var lbl_English_nationality = e.Item.FindControl("lbl_English_nationality") as Label;
                var lbl_Contract_Type = e.Item.FindControl("lbl_Contract_Type") as Label;
                var lbl_Contract_English_Type = e.Item.FindControl("lbl_Contract_English_Type") as Label;

                if (Session["Langues"].ToString() == "1")
                {
                    lbl_zone_arabic_name.Visible = false; lbl_zone_English_name.Visible = true;
                    lbl_Owner_Ship_AR_Name.Visible = false; lbl_Owner_Ship_EN_Name.Visible = true;
                    lbl_Building_Arabic_Name.Visible = false; lbl_Building_English_Name.Visible = true;
                    lbl_Tenants_Arabic_Name.Visible = false; lbl_Tenants_English_Name.Visible = true;
                    lbl_Arabic_nationality.Visible = false; lbl_English_nationality.Visible = true;
                    lbl_Contract_Type.Visible = false; lbl_Contract_English_Type.Visible = true;
                }
                else
                {
                    lbl_zone_arabic_name.Visible = true; lbl_zone_English_name.Visible = false;
                    lbl_Owner_Ship_AR_Name.Visible = true; lbl_Owner_Ship_EN_Name.Visible = false;
                    lbl_Building_Arabic_Name.Visible = true; lbl_Building_English_Name.Visible = false;
                    lbl_Tenants_Arabic_Name.Visible = true; lbl_Tenants_English_Name.Visible = false;
                    lbl_Arabic_nationality.Visible = true; lbl_English_nationality.Visible = false;
                    lbl_Contract_Type.Visible = true; lbl_Contract_English_Type.Visible = false;
                }
            }
        }

        //*************************************************************************************************************

        protected void btn_New_Contarct_Click(object sender, EventArgs e)
        {
            Label EndDate = null;
            Label New_ReNewed_Expaired = null;
            BindDataAll();
            foreach (RepeaterItem ri in Repeater1.Items)
            {
                HtmlTableRow tr = ri.FindControl("row") as HtmlTableRow;
                LinkButton ReNew_btn = (ri.FindControl("U_Renew") as LinkButton);
                EndDate = ri.FindControl("lbl_End_Date") as Label;
                New_ReNewed_Expaired = ri.FindControl("lbl_New_ReNewed_Expaired") as Label;
                string[] Array_End_Date = EndDate.Text.Split(new char[] { '/' });
                var prevDate = new DateTime(Convert.ToInt32(Array_End_Date[2]), Convert.ToInt32(Array_End_Date[1]),
                    Convert.ToInt32(Array_End_Date[0]));
                var today = DateTime.Now;
                var diffOfDates = prevDate - today;
                int sub = diffOfDates.Days;


                if (New_ReNewed_Expaired.Text == "1")
                {
                    // If the days difference is > 60 and [New_ReNewed_Expaired = 1 ], the line color is green and the renewal button is hidden. 
                    if (diffOfDates.Days > 60)
                    {
                        tr.Attributes.Add("style", "background-color:#c5f8eb;color:#000000;");
                        ReNew_btn.Visible = false;
                    }
                    // If the days difference is <= 60 (2 months left until expiration) and New_ReNewed_Expaired = 1, the line color is red, and the renewal button is shown.
                    else if (diffOfDates.Days >= 0 && diffOfDates.Days <= 60)
                    {
                        tr.Visible = false;
                    }
                    // If days difference <= 0 (2 months more than expiry) and New_ReNewed_Expaired = 1 line color black show renewal button
                    else if (diffOfDates.Days <= 0)
                    {
                        tr.Visible = false;
                    }
                }
                else if (New_ReNewed_Expaired.Text == "2")
                {
                    tr.Visible = false;
                }
                else if (New_ReNewed_Expaired.Text == "3")
                {
                    tr.Visible = false;
                }
            }
            //----------------------------------------------------------------------------------------------------------------------------------
            BindData();
            foreach (RepeaterItem ri in contract_List.Items)
            {
                HtmlTableRow tr = ri.FindControl("row") as HtmlTableRow;
                LinkButton ReNew_btn = (ri.FindControl("U_Renew") as LinkButton);
                EndDate = ri.FindControl("lbl_End_Date") as Label;
                New_ReNewed_Expaired = ri.FindControl("lbl_New_ReNewed_Expaired") as Label;
                string[] Array_End_Date = EndDate.Text.Split(new char[] { '/' });
                var prevDate = new DateTime(Convert.ToInt32(Array_End_Date[2]), Convert.ToInt32(Array_End_Date[1]),
                    Convert.ToInt32(Array_End_Date[0]));
                var today = DateTime.Now;
                var diffOfDates = prevDate - today;
                int sub = diffOfDates.Days;
                if (New_ReNewed_Expaired.Text == "1")
                {
                    // If the days difference is > 60 and [New_ReNewed_Expaired = 1 ], the line color is green and the renewal button is hidden. 
                    if (diffOfDates.Days > 60)
                    {
                        tr.Attributes.Add("style", "background-color:#c5f8eb;color:#000000;");
                        ReNew_btn.Visible = false;
                    }
                    // If the days difference is <= 60 (2 months left until expiration) and New_ReNewed_Expaired = 1, the line color is red, and the renewal button is shown.
                    else if (diffOfDates.Days >= 0 && diffOfDates.Days <= 60)
                    {
                        tr.Visible = false;
                    }
                    // If days difference <= 0 (2 months more than expiry) and New_ReNewed_Expaired = 1 line color black show renewal button
                    else if (diffOfDates.Days <= 0)
                    {
                        tr.Visible = false;
                    }
                }
                else if (New_ReNewed_Expaired.Text == "2")
                {
                    tr.Visible = false;
                }
                else if (New_ReNewed_Expaired.Text == "3")
                {
                    tr.Visible = false;
                }
            }
            //----------------------------------------------------------------------------------------------------------------------------------

            Building_BindData();
            Label B_EndDate = null;
            Label B_New_ReNewed_Expaired = null;
            foreach (RepeaterItem ri in Building_Contarct_List.Items)
            {
                HtmlTableRow tr = ri.FindControl("row") as HtmlTableRow;
                LinkButton ReNew_btn = (ri.FindControl("B_Renew") as LinkButton);
                B_EndDate = ri.FindControl("lbl_End_Date") as Label;
                B_New_ReNewed_Expaired = ri.FindControl("lbl_New_ReNewed_Expaired") as Label;
                string[] Array_End_Date = B_EndDate.Text.Split(new char[] { '/' });
                var prevDate = new DateTime(Convert.ToInt32(Array_End_Date[2]), Convert.ToInt32(Array_End_Date[1]),
                    Convert.ToInt32(Array_End_Date[0]));
                var today = DateTime.Now;
                var diffOfDates = prevDate - today;
                int sub = diffOfDates.Days;
                if (New_ReNewed_Expaired.Text == "1")
                {
                    // If the days difference is > 60 and [New_ReNewed_Expaired = 1 ], the line color is green and the renewal button is hidden. 
                    if (diffOfDates.Days > 60)
                    {
                        tr.Attributes.Add("style", "background-color:#c5f8eb;color:#000000;");
                        ReNew_btn.Visible = false;
                    }
                    // If the days difference is <= 60 (2 months left until expiration) and New_ReNewed_Expaired = 1, the line color is red, and the renewal button is shown.
                    else if (diffOfDates.Days >= 0 && diffOfDates.Days <= 60)
                    {
                        tr.Visible = false;
                    }
                    // If days difference <= 0 (2 months more than expiry) and New_ReNewed_Expaired = 1 line color black show renewal button
                    else if (diffOfDates.Days <= 0)
                    {
                        tr.Visible = false;
                    }
                }
                else if (New_ReNewed_Expaired.Text == "2")
                {
                    tr.Visible = false;
                }
                else if (New_ReNewed_Expaired.Text == "3")
                {
                    tr.Visible = false;
                }
            }
        }

        //*************************************************************************************************************
        protected void btn_Under_Expaired_Contract_Click(object sender, EventArgs e)
        {
            BindDataAll();
            Label EndDate = null;
            Label New_ReNewed_Expaired = null;
            foreach (RepeaterItem ri in Repeater1.Items)
            {
                HtmlTableRow tr = ri.FindControl("row") as HtmlTableRow;
                LinkButton ReNew_btn = (ri.FindControl("U_Renew") as LinkButton);
                EndDate = ri.FindControl("lbl_End_Date") as Label;
                New_ReNewed_Expaired = ri.FindControl("lbl_New_ReNewed_Expaired") as Label;
                string[] Array_End_Date = EndDate.Text.Split(new char[] { '/' });
                var prevDate = new DateTime(Convert.ToInt32(Array_End_Date[2]), Convert.ToInt32(Array_End_Date[1]),
                    Convert.ToInt32(Array_End_Date[0]));
                var today = DateTime.Now;
                var diffOfDates = prevDate - today;
                int sub = diffOfDates.Days;


                if (New_ReNewed_Expaired.Text == "1")
                {
                    // If the days difference is > 60 and [New_ReNewed_Expaired = 1 ], the line color is green and the renewal button is hidden. 
                    if (diffOfDates.Days > 60)
                    {
                        tr.Visible = false;
                    }
                    // If the days difference is <= 60 (2 months left until expiration) and New_ReNewed_Expaired = 1, the line color is red, and the renewal button is shown.
                    else if (diffOfDates.Days >= 0 && diffOfDates.Days <= 60)
                    {
                        tr.Attributes.Add("style", "background-color:#faced2;color:#000000;");
                        ReNew_btn.Visible = true;
                    }
                    // If days difference <= 0 (2 months more than expiry) and New_ReNewed_Expaired = 1 line color black show renewal button
                    else if (diffOfDates.Days <= 0)
                    {
                        tr.Visible = false;
                    }
                }
                else if (New_ReNewed_Expaired.Text == "2")
                {
                    tr.Visible = false;
                }
                else if (New_ReNewed_Expaired.Text == "3")
                {
                    tr.Visible = false;
                }



                //if (diffOfDates.Days <= 60 && New_ReNewed_Expaired.Text == "1")
                //{
                //    ReNew_btn.Visible = true;
                //}

                //if (diffOfDates.Days > 60)
                //{
                //    tr.Attributes.Add("style", "display:none");
                //}
                //else if (diffOfDates.Days <= 0)
                //{
                //    tr.Attributes.Add("style", "display:none");
                //}
            }

            //----------------------------------------------------------------------------------------------------------------------------------
            BindData();
            foreach (RepeaterItem ri in contract_List.Items)
            {
                HtmlTableRow tr = ri.FindControl("row") as HtmlTableRow;
                LinkButton ReNew_btn = (ri.FindControl("U_Renew") as LinkButton);
                EndDate = ri.FindControl("lbl_End_Date") as Label;
                New_ReNewed_Expaired = ri.FindControl("lbl_New_ReNewed_Expaired") as Label;
                string[] Array_End_Date = EndDate.Text.Split(new char[] { '/' });
                var prevDate = new DateTime(Convert.ToInt32(Array_End_Date[2]), Convert.ToInt32(Array_End_Date[1]),
                    Convert.ToInt32(Array_End_Date[0]));
                var today = DateTime.Now;
                var diffOfDates = prevDate - today;
                int sub = diffOfDates.Days;



                if (New_ReNewed_Expaired.Text == "1")
                {
                    // If the days difference is > 60 and [New_ReNewed_Expaired = 1 ], the line color is green and the renewal button is hidden. 
                    if (diffOfDates.Days > 60)
                    {
                        tr.Visible = false;
                    }
                    // If the days difference is <= 60 (2 months left until expiration) and New_ReNewed_Expaired = 1, the line color is red, and the renewal button is shown.
                    else if (diffOfDates.Days >= 0 && diffOfDates.Days <= 60)
                    {
                        tr.Attributes.Add("style", "background-color:#faced2;color:#000000;");
                        ReNew_btn.Visible = true;
                    }
                    // If days difference <= 0 (2 months more than expiry) and New_ReNewed_Expaired = 1 line color black show renewal button
                    else if (diffOfDates.Days <= 0)
                    {
                        tr.Visible = false;
                    }
                }
                else if (New_ReNewed_Expaired.Text == "2")
                {
                    tr.Visible = false;
                }
                else if (New_ReNewed_Expaired.Text == "3")
                {
                    tr.Visible = false;
                }
            }

            //----------------------------------------------------------------------------------------------------------------------------------

            Building_BindData();
            Label B_EndDate = null;
            Label B_New_ReNewed_Expaired = null;
            foreach (RepeaterItem ri in Building_Contarct_List.Items)
            {
                HtmlTableRow tr = ri.FindControl("row") as HtmlTableRow;
                LinkButton ReNew_btn = (ri.FindControl("B_Renew") as LinkButton);
                B_EndDate = ri.FindControl("lbl_End_Date") as Label;
                B_New_ReNewed_Expaired = ri.FindControl("lbl_New_ReNewed_Expaired") as Label;
                string[] Array_End_Date = B_EndDate.Text.Split(new char[] { '/' });
                var prevDate = new DateTime(Convert.ToInt32(Array_End_Date[2]), Convert.ToInt32(Array_End_Date[1]),
                    Convert.ToInt32(Array_End_Date[0]));
                var today = DateTime.Now;
                var diffOfDates = prevDate - today;
                int sub = diffOfDates.Days;




                if (New_ReNewed_Expaired.Text == "1")
                {
                    // If the days difference is > 60 and [New_ReNewed_Expaired = 1 ], the line color is green and the renewal button is hidden. 
                    if (diffOfDates.Days > 60)
                    {
                        tr.Visible = false;
                    }
                    // If the days difference is <= 60 (2 months left until expiration) and New_ReNewed_Expaired = 1, the line color is red, and the renewal button is shown.
                    else if (diffOfDates.Days >= 0 && diffOfDates.Days <= 60)
                    {
                        tr.Attributes.Add("style", "background-color:#faced2;color:#000000;");
                        ReNew_btn.Visible = true;
                    }
                    // If days difference <= 0 (2 months more than expiry) and New_ReNewed_Expaired = 1 line color black show renewal button
                    else if (diffOfDates.Days <= 0)
                    {
                        tr.Visible = false;
                    }
                }
                else if (New_ReNewed_Expaired.Text == "2")
                {
                    tr.Visible = false;
                }
                else if (New_ReNewed_Expaired.Text == "3")
                {
                    tr.Visible = false;
                }
            }
        }

        //*************************************************************************************************************
        protected void btn_Expaired_Contract_Click(object sender, EventArgs e)
        {
            
            Label EndDate = null;
            Label New_ReNewed_Expaired = null;

            BindDataAll();
            foreach (RepeaterItem ri in Repeater1.Items)
            {
                HtmlTableRow tr = ri.FindControl("row") as HtmlTableRow;
                LinkButton ReNew_btn = (ri.FindControl("U_Renew") as LinkButton);
                EndDate = ri.FindControl("lbl_End_Date") as Label;
                New_ReNewed_Expaired = ri.FindControl("lbl_New_ReNewed_Expaired") as Label;
                string[] Array_End_Date = EndDate.Text.Split(new char[] { '/' });
                var prevDate = new DateTime(Convert.ToInt32(Array_End_Date[2]), Convert.ToInt32(Array_End_Date[1]),
                    Convert.ToInt32(Array_End_Date[0]));
                var today = DateTime.Now;
                var diffOfDates = prevDate - today;
                int sub = diffOfDates.Days;


                if (New_ReNewed_Expaired.Text == "1")
                {
                    // If the days difference is > 60 and [New_ReNewed_Expaired = 1 ], the line color is green and the renewal button is hidden. 
                    if (diffOfDates.Days > 60)
                    {
                        tr.Visible = false;
                    }
                    // If the days difference is <= 60 (2 months left until expiration) and New_ReNewed_Expaired = 1, the line color is red, and the renewal button is shown.
                    else if (diffOfDates.Days >= 0 && diffOfDates.Days <= 60)
                    {
                        tr.Visible = false;
                    }
                    // If days difference <= 0 (2 months more than expiry) and New_ReNewed_Expaired = 1 line color black show renewal button
                    else if (diffOfDates.Days <= 0)
                    {
                        tr.Visible = true;
                    }
                }
                else if (New_ReNewed_Expaired.Text == "2")
                {
                    tr.Attributes.Add("style", "background-color:#cbd0d8;color:#000000;");
                    tr.Visible = true;
                }
                else if (New_ReNewed_Expaired.Text == "3")
                {
                    tr.Attributes.Add("style", "background-color:#cbd0d8;color:#000000;");
                    tr.Visible = true;
                }
            }

            //----------------------------------------------------------------------------------------------------------------------------------
            BindData();
            foreach (RepeaterItem ri in contract_List.Items)
            {
                HtmlTableRow tr = ri.FindControl("row") as HtmlTableRow;
                LinkButton ReNew_btn = (ri.FindControl("U_Renew") as LinkButton);
                EndDate = ri.FindControl("lbl_End_Date") as Label;
                New_ReNewed_Expaired = ri.FindControl("lbl_New_ReNewed_Expaired") as Label;
                string[] Array_End_Date = EndDate.Text.Split(new char[] { '/' });
                var prevDate = new DateTime(Convert.ToInt32(Array_End_Date[2]), Convert.ToInt32(Array_End_Date[1]),
                    Convert.ToInt32(Array_End_Date[0]));
                var today = DateTime.Now;
                var diffOfDates = prevDate - today;
                int sub = diffOfDates.Days;
                if (New_ReNewed_Expaired.Text == "1")
                {
                    // If the days difference is > 60 and [New_ReNewed_Expaired = 1 ], the line color is green and the renewal button is hidden. 
                    if (diffOfDates.Days > 60)
                    {
                        tr.Visible = false;
                    }
                    // If the days difference is <= 60 (2 months left until expiration) and New_ReNewed_Expaired = 1, the line color is red, and the renewal button is shown.
                    else if (diffOfDates.Days >= 0 && diffOfDates.Days <= 60)
                    {
                        tr.Visible = false;
                    }
                    // If days difference <= 0 (2 months more than expiry) and New_ReNewed_Expaired = 1 line color black show renewal button
                    else if (diffOfDates.Days <= 0)
                    {
                        tr.Visible = true;
                    }
                }
                else if (New_ReNewed_Expaired.Text == "2")
                {
                    tr.Attributes.Add("style", "background-color:#cbd0d8;color:#000000;");
                    tr.Visible = true;
                }
                else if (New_ReNewed_Expaired.Text == "3")
                {
                    tr.Attributes.Add("style", "background-color:#cbd0d8;color:#000000;");
                    tr.Visible = true;
                }
            }

            //----------------------------------------------------------------------------------------------------------------------------------
            Building_BindData();
            Label B_EndDate = null;
            Label B_New_ReNewed_Expaired = null;
            foreach (RepeaterItem ri in Building_Contarct_List.Items)
            {
                HtmlTableRow tr = ri.FindControl("row") as HtmlTableRow;
                LinkButton ReNew_btn = (ri.FindControl("B_Renew") as LinkButton);
                B_EndDate = ri.FindControl("lbl_End_Date") as Label;
                B_New_ReNewed_Expaired = ri.FindControl("lbl_New_ReNewed_Expaired") as Label;
                string[] Array_End_Date = B_EndDate.Text.Split(new char[] { '/' });
                var prevDate = new DateTime(Convert.ToInt32(Array_End_Date[2]), Convert.ToInt32(Array_End_Date[1]),
                    Convert.ToInt32(Array_End_Date[0]));
                var today = DateTime.Now;
                var diffOfDates = prevDate - today;
                int sub = diffOfDates.Days;
                if (New_ReNewed_Expaired.Text == "1")
                {
                    // If the days difference is > 60 and [New_ReNewed_Expaired = 1 ], the line color is green and the renewal button is hidden. 
                    if (diffOfDates.Days > 60)
                    {
                        tr.Visible = false;
                    }
                    // If the days difference is <= 60 (2 months left until expiration) and New_ReNewed_Expaired = 1, the line color is red, and the renewal button is shown.
                    else if (diffOfDates.Days >= 0 && diffOfDates.Days <= 60)
                    {
                        tr.Visible = false;
                    }
                    // If days difference <= 0 (2 months more than expiry) and New_ReNewed_Expaired = 1 line color black show renewal button
                    else if (diffOfDates.Days <= 0)
                    {
                        tr.Visible = true;
                    }
                }
                else if (New_ReNewed_Expaired.Text == "2")
                {
                    tr.Attributes.Add("style", "background-color:#cbd0d8;color:#000000;");
                    tr.Visible = true;
                }
                else if (New_ReNewed_Expaired.Text == "3")
                {
                    tr.Attributes.Add("style", "background-color:#cbd0d8;color:#000000;");
                    tr.Visible = true;
                }
            }
        }

        protected void btn_all_Contract_Click(object sender, EventArgs e)
        {
            BindDataAll();
            BindData();
            Building_BindData();
        }

        protected void A_1_ServerClick(object sender, EventArgs e)
        {
            Repeater1.Visible = false;  contract_List.Visible = true; Building_Contarct_List.Visible = false;
            if (Session["Langues"].ToString() == "1") { Label1.Text = "Single Contracts"; } else { Label1.Text = "العقود المفردة"; }
                
        }

        protected void A_2_ServerClick(object sender, EventArgs e)
        {
            Repeater1.Visible = false; contract_List.Visible = false; Building_Contarct_List.Visible = true;
            if (Session["Langues"].ToString() == "1") { Label1.Text = "Multiple Contracts"; } else { Label1.Text = "العقود المجملة"; }
            
        }

        protected void A_3_ServerClick(object sender, EventArgs e)
        {
            Repeater1.Visible = true; contract_List.Visible = false; Building_Contarct_List.Visible = false;
            if (Session["Langues"].ToString() == "1") { Label1.Text = "All Contracts"; } else { Label1.Text = " كافة العقود"; }
            
        }

        protected void C_ID_ServerClick(object sender, EventArgs e)
        {
            string id = (sender as LinkButton).CommandArgument;
            Response.Redirect("Contract_Details.aspx?Id=" + id);
        }

        protected void B_Contract_NO_Click(object sender, EventArgs e)
        {
            string id = (sender as LinkButton).CommandArgument;
            Response.Redirect("Building_Contract_Details.aspx?Id=" + id);
        }

        protected void Contract_NO_Click(object sender, EventArgs e)
        {
            string id = (sender as LinkButton).CommandArgument;
            string[] Array_id = id.Split('/');
            if (Array_id[0] == "U") { Response.Redirect("Contract_Details.aspx?Id=" + Array_id[1]); }
            else { Response.Redirect("Building_Contract_Details.aspx?Id=" + Array_id[1]); }
        }

        protected void Finsh_Click(object sender, EventArgs e)
        {
            string id = (sender as LinkButton).CommandArgument;
            string[] Array_id = id.Split('/');
            if (Array_id[0] == "U") 
            {
                //  Update New_ReNewed_Expaired to 3 in Contract Table
                string New_ReNewed_ExpairedQuery = "UPDATE contract SET New_ReNewed_Expaired=@New_ReNewed_Expaired  WHERE Contract_Id=@ID ";
                _sqlCon.Open();
                using (MySqlCommand UpdateContractCmd = new MySqlCommand(New_ReNewed_ExpairedQuery, _sqlCon))
                {
                    UpdateContractCmd.Parameters.AddWithValue("@ID", Array_id[1]);
                    //Fill The Database with All DropDownLists Items
                    UpdateContractCmd.Parameters.AddWithValue("@New_ReNewed_Expaired", "3");
                    UpdateContractCmd.ExecuteNonQuery();
                    _sqlCon.Close();
                }
                Response.Redirect(Request.RawUrl);
            }
            else 
            {
                //  Update New_ReNewed_Expaired to 3 in Contract Table
                string New_ReNewed_ExpairedQuery = "UPDATE building_contract SET New_ReNewed_Expaired=@New_ReNewed_Expaired  WHERE Contract_Id=@ID ";
                _sqlCon.Open();
                using (MySqlCommand UpdateContractCmd = new MySqlCommand(New_ReNewed_ExpairedQuery, _sqlCon))
                {
                    UpdateContractCmd.Parameters.AddWithValue("@ID", Array_id[1]);
                    //Fill The Database with All DropDownLists Items
                    UpdateContractCmd.Parameters.AddWithValue("@New_ReNewed_Expaired", "3");
                    UpdateContractCmd.ExecuteNonQuery();
                    _sqlCon.Close();
                }
                Response.Redirect(Request.RawUrl);
            }
        }

        protected void U_Finsh_Click(object sender, EventArgs e)
        {
            string id = (sender as LinkButton).CommandArgument;
            //  Update New_ReNewed_Expaired to 3 in Contract Table
            string New_ReNewed_ExpairedQuery = "UPDATE contract SET New_ReNewed_Expaired=@New_ReNewed_Expaired  WHERE Contract_Id=@ID ";
            _sqlCon.Open();
            using (MySqlCommand UpdateContractCmd = new MySqlCommand(New_ReNewed_ExpairedQuery, _sqlCon))
            {
                UpdateContractCmd.Parameters.AddWithValue("@ID", id);
                //Fill The Database with All DropDownLists Items
                UpdateContractCmd.Parameters.AddWithValue("@New_ReNewed_Expaired", "3");
                UpdateContractCmd.ExecuteNonQuery();
                _sqlCon.Close();
            }
            Response.Redirect(Request.RawUrl);
        }

        protected void B_Finsh_Click(object sender, EventArgs e)
        {
            string id = (sender as LinkButton).CommandArgument;
            //  Update New_ReNewed_Expaired to 3 in Contract Table
            string New_ReNewed_ExpairedQuery = "UPDATE building_contract SET New_ReNewed_Expaired=@New_ReNewed_Expaired  WHERE Contract_Id=@ID ";
            _sqlCon.Open();
            using (MySqlCommand UpdateContractCmd = new MySqlCommand(New_ReNewed_ExpairedQuery, _sqlCon))
            {
                UpdateContractCmd.Parameters.AddWithValue("@ID", id);
                //Fill The Database with All DropDownLists Items
                UpdateContractCmd.Parameters.AddWithValue("@New_ReNewed_Expaired", "3");
                UpdateContractCmd.ExecuteNonQuery();
                _sqlCon.Close();
            }
            Response.Redirect(Request.RawUrl);
        }





















        protected void language()
        {

            if (Session["Langues"] == null) { Session["Langues"] = "1"; }
            _sqlCon.Open();
            DataTable Dt = new DataTable();
            MySqlCommand Cmd = new MySqlCommand("SELECT * FROM languages_contract", _sqlCon);
            MySqlDataAdapter Da = new MySqlDataAdapter(Cmd);
            Da.Fill(Dt);
            if (Dt.Rows.Count > 0)
            {
                if (Session["Langues"].ToString() == "1")
                {
                    lbl_titel_Contracts_List.Text= Dt.Rows[46]["EN"].ToString();
                    Add.Text = Dt.Rows[0]["EN"].ToString();
                    lbl_A_3.Text = Dt.Rows[47]["EN"].ToString();
                    lbl_A_1.Text = Dt.Rows[51]["EN"].ToString();
                    lbl_A_2.Text = Dt.Rows[52]["EN"].ToString();
                    lbl_all_Contract.Text = Dt.Rows[47]["EN"].ToString();
                    lbl_New_Contract.Text = Dt.Rows[48]["EN"].ToString();
                    lbl_Under_Expaired_Contract.Text = Dt.Rows[50]["EN"].ToString();
                    lbl_Expaired_Contract.Text = Dt.Rows[49]["EN"].ToString();
                    Label1.Text = "All Contracts";
                }
                else
                {
                    lbl_titel_Contracts_List.Text = Dt.Rows[46]["AR"].ToString();
                    Add.Text = Dt.Rows[0]["AR"].ToString();
                    lbl_A_3.Text = Dt.Rows[47]["AR"].ToString();
                    lbl_A_1.Text = Dt.Rows[51]["AR"].ToString();
                    lbl_A_2.Text = Dt.Rows[52]["AR"].ToString();
                    lbl_all_Contract.Text = Dt.Rows[47]["AR"].ToString();
                    lbl_New_Contract.Text = Dt.Rows[48]["AR"].ToString();
                    lbl_Under_Expaired_Contract.Text = Dt.Rows[50]["AR"].ToString();
                    lbl_Expaired_Contract.Text = Dt.Rows[49]["AR"].ToString();
                    Label1.Text = "كافة العقود";
                }
            }
            _sqlCon.Close();
        }

        
    }
}