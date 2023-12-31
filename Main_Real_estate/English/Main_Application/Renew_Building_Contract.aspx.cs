﻿using Main_Real_estate.Utilities;
using MySql.Data.MySqlClient;
using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Main_Real_estate.English.Main_Application
{
    public partial class Renew_Building_Contract : System.Web.UI.Page
    {
        private readonly MySqlConnection _sqlCon = Helper.GetConnection();    

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                Utilities.Roles.Singel_Page_permission(_sqlCon, Session["Role"].ToString(), "Contracting", 0, "R");
            }
            catch { Response.Redirect("Log_In.aspx"); }
            
            //try
            //{
            if (!this.IsPostBack)
            {
                language();
                Helper.LoadDropDownList("SELECT * FROM company_representative where tenants_Tenants_ID ='" + Tenan_Name_DropDownList.SelectedValue + "'", _sqlCon, Com_Rep_DropDownList, "Com_rep_En_Name", "Company_representative_Id");
                Com_Rep_DropDownList.Items.Insert(0, "إختر اسم الممثل ....");

                //************************ get The Contract Information **************************************************

                string Contract_Id = Request.QueryString["Id"];
                DataTable get_Contract_Dt = new DataTable();
                _sqlCon.Open();
                MySqlCommand get_Contract_Cmd = new MySqlCommand("SELECT * FROM building_contract WHERE Contract_Id = @ID", _sqlCon);
                MySqlDataAdapter get_Contract_Da = new MySqlDataAdapter(get_Contract_Cmd);
                get_Contract_Cmd.Parameters.AddWithValue("@ID", Contract_Id);
                get_Contract_Da.Fill(get_Contract_Dt);
                if (get_Contract_Dt.Rows.Count > 0)
                {
                    if (get_Contract_Dt.Rows[0]["Com_rep"].ToString() == "1")
                    {
                        Com_Rep_Div.Visible = false;
                    }
                    else
                    {
                        Com_Rep_Div.Visible = true;
                    }
                    if (get_Contract_Dt.Rows[0]["contract_type_Contract_Type_Id"].ToString() == "1")
                    {
                        if (Session["Langues"].ToString() == "1") { lbl_No_Of_Months_Or_Years.Text = "The Number Of Years"; } else { lbl_No_Of_Months_Or_Years.Text = "عدد السنوات"; }
                        txt_No_Of_Months_Or_Years.ReadOnly = true;
                        txt_No_Of_Months_Or_Years.Text = get_Contract_Dt.Rows[0]["Number_Of_Years"].ToString();
                    }
                    else if (get_Contract_Dt.Rows[0]["contract_type_Contract_Type_Id"].ToString() == "2" || get_Contract_Dt.Rows[0]["contract_type_Contract_Type_Id"].ToString() == "3")
                    {
                        if (Session["Langues"].ToString() == "1") { lbl_No_Of_Months_Or_Years.Text = "The Number Of Months"; } else { lbl_No_Of_Months_Or_Years.Text = "عدد الأشهر"; }
                        txt_No_Of_Months_Or_Years.ReadOnly = true;
                        txt_No_Of_Months_Or_Years.Text = get_Contract_Dt.Rows[0]["Number_Of_Mounth"].ToString();
                    }
                    else
                    {
                        if (Session["Langues"].ToString() == "1") { lbl_No_Of_Months_Or_Years.Text = "The Number Of Months"; } else { lbl_No_Of_Months_Or_Years.Text = "عدد الأشهر"; }
                        txt_No_Of_Months_Or_Years.Text = get_Contract_Dt.Rows[0]["Number_Of_Mounth"].ToString();
                    }
                    txt_FREE_PERIOD.Text = get_Contract_Dt.Rows[0]["Start_Free_Period"].ToString();
                    txt_Duration_Of_The_Free_Period.Text = get_Contract_Dt.Rows[0]["Duration_free_period"].ToString();
                    Reason_For_Rent_DropDownList.SelectedValue = get_Contract_Dt.Rows[0]["reason_for_rent_Reason_For_Rent_Id"].ToString();
                    txt_Payment_Amount.Text = get_Contract_Dt.Rows[0]["Payment_Amount"].ToString();
                    txt_Payment_Amount_L.Text = get_Contract_Dt.Rows[0]["Payment_Amount_L"].ToString();
                    txt_Sign_Date.Text = get_Contract_Dt.Rows[0]["Date_Of_Sgin"].ToString();
                    txt_Start_Date.Text = get_Contract_Dt.Rows[0]["Start_Date"].ToString();
                    txt_End_Date.Text = get_Contract_Dt.Rows[0]["End_Date"].ToString();
                    txt_Contract_Details.Text = get_Contract_Dt.Rows[0]["Contract_Details"].ToString();
                    Tenan_Name_DropDownList.SelectedValue = get_Contract_Dt.Rows[0]["tenants_Tenants_ID"].ToString();
                    
                    Com_Rep_DropDownList.SelectedValue = get_Contract_Dt.Rows[0]["Com_rep"].ToString();

                    if (get_Contract_Dt.Rows[0]["Paymen_Method"].ToString() == "شيك") { Paymen_Method_RadioButtonList.SelectedValue = "1"; }
                    else if (get_Contract_Dt.Rows[0]["Paymen_Method"].ToString() == "تحويل") { Paymen_Method_RadioButtonList.SelectedValue = "2"; }
                    else if (get_Contract_Dt.Rows[0]["Paymen_Method"].ToString() == "نقداً") { Paymen_Method_RadioButtonList.SelectedValue = "3"; }


                    if (get_Contract_Dt.Rows[0]["Paymen_Method"].ToString() == "شيك")
                    {
                        Paymen_Method_RadioButtonList.SelectedValue = "1"; Cheque_Div.Visible = true; Cash_div.Visible = false; transformation_Div.Visible = false;
                    }
                    else if (get_Contract_Dt.Rows[0]["Paymen_Method"].ToString() == "تحويل")
                    {
                        Paymen_Method_RadioButtonList.SelectedValue = "2"; Cheque_Div.Visible = false; Cash_div.Visible = false; transformation_Div.Visible = true;
                    }
                    else if (get_Contract_Dt.Rows[0]["Paymen_Method"].ToString() == "نقداً")
                    {
                        Paymen_Method_RadioButtonList.SelectedValue = "3"; Cheque_Div.Visible = false; Cash_div.Visible = true; transformation_Div.Visible = false;
                    }

                    Building_Name_DropDownList.SelectedValue = get_Contract_Dt.Rows[0]["building_Building_Id"].ToString();
                    Contract_Type_DropDownList.SelectedValue = get_Contract_Dt.Rows[0]["contract_type_Contract_Type_Id"].ToString();

                    Contract_Templet_DropDownList.SelectedValue = get_Contract_Dt.Rows[0]["contract_template_Contract_template_Id"].ToString();
                 //   Payment_Frquancy_DropDownList.SelectedValue = get_Contract_Dt.Rows[0]["payment_frequency_Payment_Frequency_Id"].ToString();
                    Payment_Type_DropDownList.SelectedValue = get_Contract_Dt.Rows[0]["payment_type_payment_type_Id"].ToString();
                    lbl_Tenant_Name.Text = Tenan_Name_DropDownList.SelectedItem.Text;
                }
                _sqlCon.Close();

                using (MySqlCommand get_Contract_ownership_drowpdownlist_Cmd = new MySqlCommand("Edit_B_Contract_Ownership_Unit_dropdownlist", _sqlCon))
                {
                    _sqlCon.Open();
                    get_Contract_ownership_drowpdownlist_Cmd.CommandType = CommandType.StoredProcedure;
                    get_Contract_ownership_drowpdownlist_Cmd.Parameters.AddWithValue("@Id", Building_Name_DropDownList.SelectedValue);
                    using (MySqlDataAdapter get_Contract_ownership_drowpdownlist_Da = new MySqlDataAdapter(get_Contract_ownership_drowpdownlist_Cmd))
                    {
                        DataTable get_Contract_ownership_drowpdownlist_Dt = new DataTable();
                        get_Contract_ownership_drowpdownlist_Da.Fill(get_Contract_ownership_drowpdownlist_Dt);

                        Ownership_Name_DropDownList.SelectedValue = get_Contract_ownership_drowpdownlist_Dt.Rows[0]["Owner_Ship_Id"].ToString();
                    }
                    _sqlCon.Close();
                }
                //--------------------------------------- Fill Cheque GridView with Added Cheques --------------------------------------------------------------
                DataTable dt = new DataTable();
                dt.Columns.AddRange(new DataColumn[11]
                {
                    new DataColumn("Cheques_No"), new DataColumn("Cheques_Date"), new DataColumn("Cheques_Amount"),
                    new DataColumn("Cheque_Type"), new DataColumn("cheque_type_Cheque_Type_id"),
                    new DataColumn("Cheque_Bank_Name"),
                    new DataColumn("bank_Bank_Id"), new DataColumn("Tenant_Name"), new DataColumn("tenants_Tenants_ID"), new DataColumn("Cheque_Owner"),
                    new DataColumn("beneficiary_person")
                });
                ViewState["Customers"] = dt;
                this.BindGrid();
                //**************************************************************************************************************************************************

                //--------------------------------------- Fill transformation GridView with Added transformation --------------------------------------------------------------
                DataTable Tr_Dt = new DataTable();
                Tr_Dt.Columns.AddRange(new DataColumn[7]
                {
                    new DataColumn("transformation_No"),
                    new DataColumn("Bank_Name"),new DataColumn("transformation_Bank_ID"),
                    new DataColumn("transformation_Date"),
                    new DataColumn("Amount"),
                    new DataColumn("tenant_Name"), new DataColumn("tenant_Id")
                });
                ViewState["transformation_Customers"] = Tr_Dt;
                this.BindGrid();
                //**************************************************************************************************************************************************

                //--------------------------------------- Fill Cash GridView with Added Cash --------------------------------------------------------------

                DataTable Ch_Dt = new DataTable();
                Ch_Dt.Columns.AddRange(new DataColumn[4]
                {
                    new DataColumn("Cash_Amount"),
                    new DataColumn("Cash_Date"),
                    new DataColumn("tenant_Name"), new DataColumn("tenant_Id")
                });
                ViewState["Cash_Customers"] = Ch_Dt;
                this.BindGrid();
                //-----------------------------------------------------------------------------------------------------

                if (Contract_Templet_DropDownList.SelectedValue == "3")
                {
                    refreshdata();
                    Building_Name_DropDownList.Enabled = false;
                    if (Session["Langues"].ToString() == "1") { Half_Contract_Worrning.Text = "It is not possible to modify the building in the case of multiple contracts"; }
                    else { Half_Contract_Worrning.Text = "لا يمكن تعديل البناء بحالة العقود نص المجملة "; }

                }
            }
            //}
            //catch
            //{
            //    Response.Redirect("Contract_List.aspx");
            //    Response.Write(@"<script language='javascript'>alert('لايوجد عقد لتعديله')</script>");
            //}
        }



        //*********************************  Main Function of the contract ***********************************************************************************************************
        protected void btn_Add_Contract_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {

                //  Update New_ReNewed_Expaired to 2 in Contract Table
                string contractId = Request.QueryString["Id"];
                string New_ReNewed_ExpairedQuery = "UPDATE building_contract SET New_ReNewed_Expaired=@New_ReNewed_Expaired  WHERE Contract_Id=@ID ";
                _sqlCon.Open();
                using (MySqlCommand UpdateContractCmd = new MySqlCommand(New_ReNewed_ExpairedQuery, _sqlCon))
                {
                    UpdateContractCmd.Parameters.AddWithValue("@ID", contractId);
                    //Fill The Database with All DropDownLists Items
                    UpdateContractCmd.Parameters.AddWithValue("@New_ReNewed_Expaired", "2");
                    UpdateContractCmd.ExecuteNonQuery();
                    _sqlCon.Close();
                }


                //  insert The Contract Information in Contract Tabel in DB
                string RenewContractQuery = "Insert Into building_contract (" +
                                          "Contract_Details , " +
                                          "Payment_Amount  , " +
                                          "Payment_Amount_L  , " +
                                          "Date_Of_Sgin        , " +
                                          "Start_Date       , " +
                                          "End_Date         , " +
                                          "users_user_ID , " +
                                          "building_Building_Id , " +
                                          "New_ReNewed_Expaired , " +
                                          "Start_Free_Period , " +
                                          "Duration_free_period , " +
                                          "maintenance , " +
                                          "Rental_allowed_Or_Not_allowed , " +
                                          "reason_for_rent_Reason_For_Rent_Id   , " +
                                          "contract_type_Contract_Type_Id   , " +
                                          "contract_template_Contract_template_Id , " +
                                          "Number_Of_Mounth , " +
                                          "Number_Of_Years , " +
                                          "tenants_Tenants_ID , " +
                                          "Com_rep ," +
                                          "Paymen_Method ," +
                                          "payment_type_payment_type_Id) " +
                                          "VALUES( " +
                                          "@Contract_Details , " +
                                          "@Payment_Amount  , " +
                                          "@Payment_Amount_L  , " +
                                          "@Date_Of_Sgin        , " +
                                          "@Start_Date       , " +
                                          "@End_Date         , " +
                                          "@users_user_ID , " +
                                          "@building_Building_Id , " +
                                          "@New_ReNewed_Expaired , " +
                                          "@Start_Free_Period , " +
                                          "@Duration_free_period , " +
                                          "@maintenance , " +
                                          "@Rental_allowed_Or_Not_allowed , " +
                                          "@reason_for_rent_Reason_For_Rent_Id   , " +
                                          "@contract_type_Contract_Type_Id   , " +
                                          "@contract_template_Contract_template_Id , " +
                                          "@Number_Of_Mounth , " +
                                          "@Number_Of_Years , " +
                                          "@tenants_Tenants_ID , " +
                                          "@Com_rep ," +
                                          "@Paymen_Method ," +
                                          "@payment_type_payment_type_Id ) ";
                _sqlCon.Open();
                using (MySqlCommand RenewContractCmd = new MySqlCommand(RenewContractQuery, _sqlCon))
                {
                    //Fill The Database with All DropDownLists Items
                    RenewContractCmd.Parameters.AddWithValue("@contract_type_Contract_Type_Id",
                        Contract_Type_DropDownList.SelectedValue);
                    RenewContractCmd.Parameters.AddWithValue("@contract_template_Contract_template_Id",
                        Contract_Templet_DropDownList.SelectedValue);
                    //RenewContractCmd.Parameters.AddWithValue("@payment_frequency_Payment_Frequency_Id",
                    //    Payment_Frquancy_DropDownList.SelectedValue);
                    RenewContractCmd.Parameters.AddWithValue("@tenants_Tenants_ID", Tenan_Name_DropDownList.SelectedValue);
                    RenewContractCmd.Parameters.AddWithValue("@payment_type_payment_type_Id", Payment_Type_DropDownList.SelectedValue);
                    RenewContractCmd.Parameters.AddWithValue("@building_Building_Id", Building_Name_DropDownList.SelectedValue);
                    RenewContractCmd.Parameters.AddWithValue("@users_user_ID", Session["user_ID"]);
                    RenewContractCmd.Parameters.AddWithValue("@New_ReNewed_Expaired", "1");
                    RenewContractCmd.Parameters.AddWithValue("@reason_for_rent_Reason_For_Rent_Id", Reason_For_Rent_DropDownList.SelectedValue);
                    RenewContractCmd.Parameters.AddWithValue("@Start_Free_Period", txt_FREE_PERIOD.Text);
                    RenewContractCmd.Parameters.AddWithValue("@Duration_free_period", txt_Duration_Of_The_Free_Period.Text);
                    RenewContractCmd.Parameters.AddWithValue("@Com_rep", Com_Rep_DropDownList.SelectedValue);
                    RenewContractCmd.Parameters.AddWithValue("@maintenance", maintenance_RadioButtonList.SelectedValue);
                    RenewContractCmd.Parameters.AddWithValue("@Rental_allowed_Or_Not_allowed", Rental_allowed_Or_Not_allowed_RadioButtonList.SelectedValue);
                    RenewContractCmd.Parameters.AddWithValue("@Paymen_Method", Paymen_Method_RadioButtonList.SelectedItem.Text.Trim());
                    //Fill The Database with All Textbox Items
                    RenewContractCmd.Parameters.AddWithValue("@Contract_Details", txt_Contract_Details.Text);
                    RenewContractCmd.Parameters.AddWithValue("@Payment_Amount", txt_Payment_Amount.Text);
                    RenewContractCmd.Parameters.AddWithValue("@Payment_Amount_L", txt_Payment_Amount_L.Text);
                    RenewContractCmd.Parameters.AddWithValue("@Date_Of_Sgin", txt_Sign_Date.Text);
                    RenewContractCmd.Parameters.AddWithValue("@Start_Date", txt_Start_Date.Text);
                    RenewContractCmd.Parameters.AddWithValue("@End_Date", txt_End_Date.Text);
                    if (Contract_Type_DropDownList.SelectedValue == "1")
                    {
                        RenewContractCmd.Parameters.AddWithValue("@Number_Of_Years", txt_No_Of_Months_Or_Years.Text);
                        RenewContractCmd.Parameters.AddWithValue("@Number_Of_Mounth", "");
                    }
                    else
                    {
                        RenewContractCmd.Parameters.AddWithValue("@Number_Of_Years", "");
                        RenewContractCmd.Parameters.AddWithValue("@Number_Of_Mounth", txt_No_Of_Months_Or_Years.Text);
                    }

                    RenewContractCmd.ExecuteNonQuery();
                    _sqlCon.Close();
                }

                Renew_Arcive_Building_Contract();

                //    Get The Added Contract Id
                using (MySqlCommand Get_Contract_ID = new MySqlCommand("SELECT MAX(Contract_Id) AS LastInsertedID from building_contract", _sqlCon))
                {
                    _sqlCon.Open();
                    Get_Contract_ID.CommandType = CommandType.Text;
                    object Contract_ID = Get_Contract_ID.ExecuteScalar();
                    if (Contract_ID != null)
                    {
                        Contract_id.Text = Contract_ID.ToString();
                    }

                    _sqlCon.Close();
                }






                if (Paymen_Method_RadioButtonList.SelectedValue == "1")
                {
                    //    insert The Cheques Information in Cheques Tabel in DB
                    string Defult_Chque_Status = "غير محصل";
                    MySqlCommand com;
                    foreach (GridViewRow g1 in Cheque_GridView.Rows)
                    {
                        _sqlCon.Open();
                        com = new
                        MySqlCommand("INSERT INTO building_cheques (" +
                                     "Cheques_No," +
                                     "Cheques_Date  , " +
                                     "Cheques_Amount," +
                                     "Cheque_Owner," +
                                     "beneficiary_person," +
                                     "Cheques_Status," +
                                     "cheque_type_Cheque_Type_id," +
                                     "bank_Bank_Id  , " +
                                     "tenants_Tenants_ID  , " +
                                     "building_contract_Contract_Id)  " +
                                     "VALUES(" +
                                     "'" + g1.Cells[1].Text + "' ," +
                                     "'" + g1.Cells[2].Text + "' ," +
                                     "'" + g1.Cells[3].Text + "' ," +
                                     "'" + g1.Cells[11].Text + "' ," +
                                     "'" + g1.Cells[12].Text + "' ," +
                                     "'" + Defult_Chque_Status + "' ," +
                                     " '" + Convert.ToInt32(g1.Cells[6].Text) + "', " +
                                     "'" + Convert.ToInt32(g1.Cells[8].Text) + "' , " +
                                     "'" + Convert.ToInt32(g1.Cells[10].Text) + "' ," +
                                     "'" + Convert.ToInt32(Contract_id.Text) + "')", _sqlCon);

                        com.ExecuteNonQuery();
                        _sqlCon.Close();
                    }
                }
                else if (Paymen_Method_RadioButtonList.SelectedValue == "2")
                {
                    string Defult_transformation_Status = "غير محصل";
                    MySqlCommand com;
                    foreach (GridViewRow g1 in transformation_GridView.Rows)
                    {
                        _sqlCon.Open();
                        com = new
                            MySqlCommand("INSERT INTO building_transformation_table (" +
                                         "transformation_No," +
                                         "transformation_Bank_ID," +
                                         "transformation_Date," +
                                         "Amount," +
                                         "Status," +
                                         "tenant_Id  , " +
                                         "Contract_Id)  " +
                                         "VALUES(" +
                                         "'" + g1.Cells[0].Text + "' ," +
                                         "'" + g1.Cells[2].Text + "' ," +
                                         "'" + g1.Cells[3].Text + "' ," +
                                         "'" + g1.Cells[4].Text + "' ," +
                                         "'" + Defult_transformation_Status + "' ," +
                                         "'" + g1.Cells[6].Text + "' ," +
                                         "'" + Contract_id.Text + "')", _sqlCon);
                        com.ExecuteNonQuery();
                        _sqlCon.Close();
                    }
                }
                else
                {
                    string Defult_Cash_Status = "غير محصل";
                    MySqlCommand com;
                    foreach (GridViewRow g1 in Cash_GridView.Rows)
                    {
                        _sqlCon.Open();
                        com = new
                            MySqlCommand("INSERT INTO building_cash_amount (" +
                                         "Cash_Amount," +
                                         "Cash_Date," +
                                         "Satuts," +
                                         "tenant_Id," +
                                         "Contract_Id)  " +
                                         "VALUES(" +
                                         "'" + g1.Cells[0].Text + "' ," +
                                         "'" + g1.Cells[1].Text + "' ," +
                                          "'" + Defult_Cash_Status + "' ," +
                                         "'" + g1.Cells[3].Text + "' ," +
                                         "'" + Contract_id.Text + "')", _sqlCon);
                        com.ExecuteNonQuery();
                        _sqlCon.Close();
                    }
                }

                    

                lbl_Success_Add_New_Contract.Text = "تمت التجديد بنجاح";
            }
        }
        protected void btn_Back_To_Contract_List_Click(object sender, EventArgs e)
        {
            Response.Redirect("Contract_List.aspx");
        }














        //*****************************************************************************************************************
        //************************************   Cheuqe / Transformation / Cash  Operations    ****************************
        protected void BindGrid()
        {
            Cheque_GridView.DataSource = (DataTable)ViewState["Customers"];
            Cheque_GridView.DataBind();
        }
        protected void transformation_BindGrid()
        {
            transformation_GridView.DataSource = (DataTable)ViewState["transformation_Customers"];
            transformation_GridView.DataBind();
        }
        protected void Cash_BindGrid()
        {
            Cash_GridView.DataSource = (DataTable)ViewState["Cash_Customers"];
            Cash_GridView.DataBind();
        }
        protected void OnRowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            int index = Convert.ToInt32(e.RowIndex);
            DataTable dt = ViewState["Customers"] as DataTable;
            dt.Rows[index].Delete();
            ViewState["Customers"] = dt;
            BindGrid();
        }
        protected void transformation_GridView_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            int index = Convert.ToInt32(e.RowIndex);
            DataTable Tr_Dt = ViewState["transformation_Customers"] as DataTable;
            Tr_Dt.Rows[index].Delete();
            ViewState["transformation_Customers"] = Tr_Dt;
            transformation_BindGrid();
        }
        protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            int index = Convert.ToInt32(e.RowIndex);
            DataTable Tr_Dt = ViewState["Cash_Customers"] as DataTable;
            Tr_Dt.Rows[index].Delete();
            ViewState["Cash_Customers"] = Tr_Dt;
            Cash_BindGrid();
        }
        //************************************** Add Cheques in the View  ***************************************************************************************
        protected void ImageButton4_Click(object sender, ImageClickEventArgs e)
        {
            if (serial_CheckBox.Checked == true)
            {
                if (txt_Cheque_NO.Text != "" & txt_Cheque_Date.Text != ""
                && txt_Cheque_Value.Text != "" & Cheque_Type_DropDownList.SelectedItem.Text != "..............."
                && Bank_Cheque_Name_DropDownList.SelectedItem.Text != "..............." &&
                Tenan_Name_DropDownList.SelectedItem.Text != "..............." && Cheque_Owner.Text != "")
                {
                    for (int i = 0; i < Convert.ToInt32(txt_No_serial_Chques.Text); i++)
                    {
                        int Cheque_NO = Convert.ToInt32(txt_Cheque_NO.Text.Trim()) + i;
                        DateTime Cheque_Date = Convert.ToDateTime(txt_Cheque_Date.Text.Trim()).AddMonths(i);
                        string String_Cheque_Date = Cheque_Date.ToString("dd/MM/yyyy");
                        DataTable dt1 = (DataTable)ViewState["Customers"];
                        dt1.Rows.Add
                        (

                            Convert.ToString(Cheque_NO),
                            String_Cheque_Date,
                            txt_Cheque_Value.Text.Trim(),
                            Cheque_Type_DropDownList.SelectedItem.Text.Trim(),
                            Cheque_Type_DropDownList.SelectedValue,
                            Bank_Cheque_Name_DropDownList.SelectedItem.Text.Trim(),
                            Bank_Cheque_Name_DropDownList.SelectedValue,
                            Tenan_Name_DropDownList.SelectedItem.Text.Trim(),
                            Tenan_Name_DropDownList.SelectedValue,
                            Cheque_Owner.Text.Trim(),
                            txt_beneficiary_person.Text.Trim()
                        ); ;
                        ViewState["Customers"] = dt1;
                        this.BindGrid();
                        lbl_Worrnig_Cheque.Visible = false;
                    }
                }
                else
                {
                    lbl_Worrnig_Cheque.Visible = true;
                }
            }
            else
            {
                if (txt_Cheque_NO.Text != "" & txt_Cheque_Date.Text != ""
               && txt_Cheque_Value.Text != "" & Cheque_Type_DropDownList.SelectedItem.Text != "إخترنوع الشيك ...."
               && Bank_Cheque_Name_DropDownList.SelectedItem.Text != "إختراسم البنك ...." &&
               Tenan_Name_DropDownList.SelectedItem.Text != "إختر اسم المستأجر ...." && Cheque_Owner.Text != "")
                {
                    DataTable dt1 = (DataTable)ViewState["Customers"];
                    dt1.Rows.Add
                    (
                        txt_Cheque_NO.Text.Trim(),
                        txt_Cheque_Date.Text.Trim(),
                        txt_Cheque_Value.Text.Trim(),
                        Cheque_Type_DropDownList.SelectedItem.Text.Trim(),
                        Cheque_Type_DropDownList.SelectedValue,
                        Bank_Cheque_Name_DropDownList.SelectedItem.Text.Trim(),
                        Bank_Cheque_Name_DropDownList.SelectedValue,
                        Tenan_Name_DropDownList.SelectedItem.Text.Trim(),
                        Tenan_Name_DropDownList.SelectedValue,
                        Cheque_Owner.Text.Trim(),
                        txt_beneficiary_person.Text.Trim()
                    );
                    ViewState["Customers"] = dt1;
                    this.BindGrid();
                    lbl_Worrnig_Cheque.Visible = false;
                }
                else
                {
                    lbl_Worrnig_Cheque.Visible = true;
                }
            }


            ClientScript.RegisterClientScriptBlock(this.GetType(), "",
                "window.onload=function(){window.scrollTo(0,document.body.scrollHeight)};", true);
        }
        //***********************************************************************************************************************************
        protected void btn_Add_Transformation_Click(object sender, ImageClickEventArgs e)
        {

            DataTable dt1 = (DataTable)ViewState["transformation_Customers"];
            dt1.Rows.Add
            (
                txt_transformation_No.Text.Trim(),

                transformation_Bank_DropDownList.SelectedItem.Text.Trim(),
                transformation_Bank_DropDownList.SelectedValue,

                txt_transformation_Date.Text.Trim(),

                txt_transformation_Amount.Text.Trim(),

                Tenan_Name_DropDownList.SelectedItem.Text.Trim(),
                Tenan_Name_DropDownList.SelectedValue


            );
            ViewState["transformation_Customers"] = dt1;
            this.transformation_BindGrid();


            ClientScript.RegisterClientScriptBlock(this.GetType(), "", "window.onload=function(){window.scrollTo(0,document.body.scrollHeight)};", true);

        }
        //************************************************************************************************************************************************************************
        protected void Add_Cash_Click(object sender, ImageClickEventArgs e)
        {
            DataTable dt1 = (DataTable)ViewState["Cash_Customers"];
            dt1.Rows.Add
            (
                txt_Cash_Amount.Text.Trim(),
                txt_Cash_Date.Text.Trim(),
                Tenan_Name_DropDownList.SelectedItem.Text.Trim(),
                Tenan_Name_DropDownList.SelectedValue


            );
            ViewState["Cash_Customers"] = dt1;
            this.Cash_BindGrid();


            ClientScript.RegisterClientScriptBlock(this.GetType(), "", "window.onload=function(){window.scrollTo(0,document.body.scrollHeight)};", true);
        }









        //***********************************      SelectedIndexChanged    ***********************************************************
        //--------------------------------------  Ownership_Name_DropDownList --------------------------------------
        protected void Ownership_Name_DropDownList_SelectedIndexChanged(object sender, EventArgs e)
        {
            //    //Fill Buildings Name DropDownList
            Helper.LoadDropDownList("SELECT * FROM building where owner_ship_Owner_Ship_Id = '" + Ownership_Name_DropDownList.SelectedValue + "'", _sqlCon, Building_Name_DropDownList, "Building_Arabic_Name", "Building_Id");
            Building_Name_DropDownList.Items.Insert(0, "إختر البناء ....");
        }
        //-------------------------------------- Contract_Type_DropDownList --------------------------------------
        protected void Contract_Type_DropDownList_SelectedIndexChanged(object sender, EventArgs e)
        {
            div_No_Of_Months.Visible = true;
            if (Contract_Type_DropDownList.SelectedValue == "1")
            {
                if (Session["Langues"].ToString() == "1") { lbl_No_Of_Months_Or_Years.Text = "The Number Of Years"; } else { lbl_No_Of_Months_Or_Years.Text = "عدد السنوات"; }
                txt_No_Of_Months_Or_Years.ReadOnly = false;
                txt_No_Of_Months_Or_Years.Text = "1";

                if (txt_Start_Date.Text != "")
                {
                    if (txt_No_Of_Months_Or_Years.Text != "")
                    {
                        DateTime add_Months = Convert.ToDateTime(txt_Start_Date.Text).AddMonths(Convert.ToInt32(txt_No_Of_Months_Or_Years.Text) * 12);
                        txt_End_Date.Text = add_Months.ToString("dd/MM/yyyy");
                    }
                }
            }
            else if (Contract_Type_DropDownList.SelectedValue == "2")
            {
                if (Session["Langues"].ToString() == "1") { lbl_No_Of_Months_Or_Years.Text = "The Number Of Months"; } else { lbl_No_Of_Months_Or_Years.Text = "عدد الأشهر"; }
                txt_No_Of_Months_Or_Years.Text = "6";
                txt_No_Of_Months_Or_Years.ReadOnly = true;

                if (txt_Start_Date.Text != "")
                {
                    if (txt_No_Of_Months_Or_Years.Text != "")
                    {
                        DateTime add_Months = Convert.ToDateTime(txt_Start_Date.Text).AddMonths(6);
                        txt_End_Date.Text = add_Months.ToString("dd/MM/yyyy");
                    }
                }
            }
            else if (Contract_Type_DropDownList.SelectedValue == "3")
            {
                if (Session["Langues"].ToString() == "1") { lbl_No_Of_Months_Or_Years.Text = "The Number Of Months"; } else { lbl_No_Of_Months_Or_Years.Text = "عدد الأشهر"; }
                txt_No_Of_Months_Or_Years.Text = "3";
                txt_No_Of_Months_Or_Years.ReadOnly = true;

                if (txt_Start_Date.Text != "")
                {
                    if (txt_No_Of_Months_Or_Years.Text != "")
                    {
                        DateTime add_Months = Convert.ToDateTime(txt_Start_Date.Text).AddMonths(3);
                        txt_End_Date.Text = add_Months.ToString("dd/MM/yyyy");
                    }
                }
            }
            else
            {
                if (Session["Langues"].ToString() == "1") { lbl_No_Of_Months_Or_Years.Text = "The Number Of Months"; } else { lbl_No_Of_Months_Or_Years.Text = "عدد الأشهر"; }
                txt_No_Of_Months_Or_Years.ReadOnly = false;
                txt_No_Of_Months_Or_Years.Text = "1";

                if (txt_Start_Date.Text != "")
                {
                    if (txt_No_Of_Months_Or_Years.Text != "")
                    {
                        DateTime add_Months = Convert.ToDateTime(txt_Start_Date.Text).AddMonths(Convert.ToInt32(txt_No_Of_Months_Or_Years.Text));
                        txt_End_Date.Text = add_Months.ToString("dd/MM/yyyy");
                    }
                }
            }
        }
        //-------------------------------------- Paymen_Method_RadioButtonList --------------------------------------
        protected void Paymen_Method_RadioButtonList_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (Paymen_Method_RadioButtonList.SelectedValue == "1") { Cheque_Div.Visible = true; transformation_Div.Visible = false; Cash_div.Visible = false; }
            else if (Paymen_Method_RadioButtonList.SelectedValue == "2") { Cheque_Div.Visible = false; transformation_Div.Visible = true; Cash_div.Visible = false; ; }
            else if (Paymen_Method_RadioButtonList.SelectedValue == "3") { Cheque_Div.Visible = false; transformation_Div.Visible = false; Cash_div.Visible = true; }
            ClientScript.RegisterClientScriptBlock(this.GetType(), "", "window.onload=function(){window.scrollTo(0,document.body.scrollHeight)};", true);
        }
        //-------------------------------------- FREE_PERIOD_CheckBox Event --------------------------------------
        protected void FREE_PERIOD_CheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (FREE_PERIOD_CheckBox.Checked == true)
            {
                FREE_PERIOD_Div.Visible = true;
            }
            else if (FREE_PERIOD_CheckBox.Checked == false)
            {
                FREE_PERIOD_Div.Visible = false;
            }
        }
        //-------------------------------------- Additional_Items_CheckBox Event ---------------------------------
        protected void Additional_Items_CheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (Additional_Items_CheckBox.Checked == true)
            {
                Contract_Details_Div.Visible = true;
            }
            else if (Additional_Items_CheckBox.Checked == false)
            {
                Contract_Details_Div.Visible = false;
            }
        }
        //-------------------------------------- txt_No_Of_Months_Or_Years Event ---------------------------------
        protected void txt_No_Of_Months_Or_Years_TextChanged(object sender, EventArgs e)
        {
            if (txt_Start_Date.Text != "")
            {
                if (txt_No_Of_Months_Or_Years.Text != "")
                {
                    if (Contract_Type_DropDownList.SelectedValue == "1")
                    {
                        if (txt_Duration_Of_The_Free_Period.Text != "")
                        {
                            DateTime add_Months = Convert.ToDateTime(txt_Start_Date.Text).AddMonths(Convert.ToInt32(txt_No_Of_Months_Or_Years.Text) * 12);
                            DateTime add_Months_With_Free_Period = add_Months.AddMonths(Convert.ToInt32(txt_Duration_Of_The_Free_Period.Text));
                            txt_End_Date.Text = add_Months_With_Free_Period.ToString("dd/MM/yyyy");
                        }
                        else
                        {
                            DateTime add_Months = Convert.ToDateTime(txt_Start_Date.Text).AddMonths(Convert.ToInt32(txt_No_Of_Months_Or_Years.Text) * 12);
                            txt_End_Date.Text = add_Months.ToString("dd/MM/yyyy");
                        }
                    }
                    else
                    {
                        DateTime add_Months = Convert.ToDateTime(txt_Start_Date.Text).AddMonths(Convert.ToInt32(txt_No_Of_Months_Or_Years.Text));
                        txt_End_Date.Text = add_Months.ToString("dd/MM/yyyy");
                    }
                }
            }
        }
        //-------------------------------------- txt_Duration_Of_The_Free_Period Event ---------------------------------
        protected void txt_Duration_Of_The_Free_Period_TextChanged(object sender, EventArgs e)
        {
            txt_Start_Date.Text = Start_Date_Calendar.SelectedDate.ToShortDateString();
            if (txt_Start_Date.Text != "") { Start_Date_Div.Visible = false; ImageButton2.Visible = false; }

            if (txt_No_Of_Months_Or_Years.Text != "")
            {
                if (Contract_Type_DropDownList.SelectedValue == "1")
                {
                    if (txt_Duration_Of_The_Free_Period.Text != "")
                    {
                        DateTime add_Months = Convert.ToDateTime(txt_Start_Date.Text).AddMonths(Convert.ToInt32(txt_No_Of_Months_Or_Years.Text) * 12);
                        DateTime add_Months_With_Free_Period = add_Months.AddMonths(Convert.ToInt32(txt_Duration_Of_The_Free_Period.Text));
                        txt_End_Date.Text = add_Months_With_Free_Period.ToString("dd/MM/yyyy");
                    }
                    else
                    {
                        DateTime add_Months = Convert.ToDateTime(txt_Start_Date.Text).AddMonths(Convert.ToInt32(txt_No_Of_Months_Or_Years.Text) * 12);
                        txt_End_Date.Text = add_Months.ToString("dd/MM/yyyy");
                    }
                }
                else
                {
                    if (txt_Duration_Of_The_Free_Period.Text != "")
                    {
                        DateTime add_Months = Convert.ToDateTime(txt_Start_Date.Text).AddMonths(Convert.ToInt32(txt_No_Of_Months_Or_Years.Text));
                        DateTime add_Months_With_Free_Period = add_Months.AddMonths(Convert.ToInt32(txt_Duration_Of_The_Free_Period.Text));
                        txt_End_Date.Text = add_Months_With_Free_Period.ToString("dd/MM/yyyy");
                    }
                    else
                    {
                        DateTime add_Months = Convert.ToDateTime(txt_Start_Date.Text).AddMonths(Convert.ToInt32(txt_No_Of_Months_Or_Years.Text));
                        txt_End_Date.Text = add_Months.ToString("dd/MM/yyyy");
                    }
                }
            }
        }









        //***********************************      Calanders Operations    ***********************************************************
        //--------------------------------------  Sign_Date --------------------------------------
        protected void Sign_Date_Calendar_SelectionChanged1(object sender, EventArgs e)
        {
            txt_Sign_Date.Text = Sign_Date_Calendar.SelectedDate.ToShortDateString();
            if (txt_Sign_Date.Text != "") { Sign_Date_divCalendar.Visible = false; ImageButton1.Visible = false; }
        }
        protected void Date_Chosee_Click(object sender, EventArgs e)
        {
            Sign_Date_divCalendar.Visible = true; ImageButton1.Visible = true;
        }
        protected void ImageButton1_Click(object sender, System.Web.UI.ImageClickEventArgs e)
        {
            Sign_Date_divCalendar.Visible = false; ImageButton1.Visible = false;
        }

        //--------------------------------------  Start_Date --------------------------------------
        protected void Start_Date_Calendar_SelectionChanged2(object sender, EventArgs e)
        {
            txt_Start_Date.Text = Start_Date_Calendar.SelectedDate.ToShortDateString();
            if (txt_Start_Date.Text != "") { Start_Date_Div.Visible = false; ImageButton2.Visible = false; }

            if (txt_No_Of_Months_Or_Years.Text != "")
            {
                if (Contract_Type_DropDownList.SelectedValue == "1")
                {
                    DateTime add_Months = Convert.ToDateTime(txt_Start_Date.Text).AddMonths(Convert.ToInt32(txt_No_Of_Months_Or_Years.Text) * 12);
                    txt_End_Date.Text = add_Months.ToString("dd/MM/yyyy");
                }
                else
                {
                    DateTime add_Months = Convert.ToDateTime(txt_Start_Date.Text).AddMonths(Convert.ToInt32(txt_No_Of_Months_Or_Years.Text));
                    txt_End_Date.Text = add_Months.ToString("dd/MM/yyyy");
                }
            }
        }
        protected void Start_Date_Chosee_Click(object sender, EventArgs e)
        {
            Start_Date_Div.Visible = true; ImageButton2.Visible = true;
        }
        protected void ImageButton2_Click(object sender, System.Web.UI.ImageClickEventArgs e)
        {
            Start_Date_Div.Visible = false; ImageButton2.Visible = false;
        }

        //--------------------------------------  End_Date --------------------------------------
        protected void End_Date_Chosee_Click(object sender, EventArgs e)
        {
            End_Date_Div.Visible = true; ImageButton3.Visible = true;
        }
        protected void ImageButton3_Click(object sender, System.Web.UI.ImageClickEventArgs e)
        {
            End_Date_Div.Visible = false; ImageButton3.Visible = false;
        }
        protected void End_Date_Calendar_SelectionChanged1(object sender, EventArgs e)
        {
            txt_End_Date.Text = End_Date_Calendar.SelectedDate.ToShortDateString();
            if (txt_End_Date.Text != "") { End_Date_Div.Visible = false; ImageButton3.Visible = false; }
        }

        //--------------------------------------  Cheque_ Date -------------------------------------- 
        protected void btn_Cheque_Date_Chosee_Click(object sender, EventArgs e)
        {
            Cheque_Date_Div.Visible = true; Cheque_Date_ImageButton.Visible = true;
        }
        protected void Cheque_Date_Calendar_SelectionChanged(object sender, EventArgs e)
        {
            txt_Cheque_Date.Text = Cheque_Date_Calendar.SelectedDate.ToShortDateString();
            if (txt_Cheque_Date.Text != "") { Cheque_Date_Div.Visible = false; Cheque_Date_ImageButton.Visible = false; }
        }
        protected void Cheque_Date_ImageButton_Click(object sender, System.Web.UI.ImageClickEventArgs e)
        {
            Cheque_Date_Div.Visible = false; Cheque_Date_ImageButton.Visible = false;
        }
        //--------------------------------------   transformation_Date --------------------------------------
        protected void transformation_Date_Button_Click(object sender, EventArgs e)
        {
            transformation_Date_Div.Visible = true;
            ImageButton5.Visible = true;
        }
        protected void ImageButton5_Click(object sender, ImageClickEventArgs e)
        {
            transformation_Date_Div.Visible = false;
            ImageButton5.Visible = false;
        }
        protected void transformation_Date_Calendar_SelectionChanged(object sender, EventArgs e)
        {
            txt_transformation_Date.Text = transformation_Date_Calendar.SelectedDate.ToShortDateString();
            if (txt_transformation_Date.Text != "")
            {
                transformation_Date_Div.Visible = false;
                ImageButton5.Visible = false;
            }
        }
        //--------------------------------------   Cash_Date --------------------------------------
        protected void Cash_Date_Button_Click(object sender, EventArgs e)
        {
            Cash_Date_Div.Visible = true;
            ImageButton6.Visible = true;
        }
        protected void ImageButton6_Click(object sender, ImageClickEventArgs e)
        {
            Cash_Date_Div.Visible = false;
            ImageButton6.Visible = false;
        }
        protected void Cash_Date_Calendar_SelectionChanged(object sender, EventArgs e)
        {
            txt_Cash_Date.Text = Cash_Date_Calendar.SelectedDate.ToShortDateString();
            if (txt_Cash_Date.Text != "")
            {
                Cash_Date_Div.Visible = false;
                ImageButton6.Visible = false;
            }
        }







        protected void Cheque_GridView_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            string ChequeType;
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                try
                {
                    ChequeType = ((Label)e.Row.FindControl("lbl_cheque_type")).Text;
                    if (ChequeType == "شيك ضمان")
                    {
                        e.Row.ForeColor = System.Drawing.Color.Blue;
                        e.Row.BackColor = System.Drawing.Color.AliceBlue;
                    }
                }
                catch
                {
                    ChequeType = "";
                }
            }
        }
        public void refreshdata()
        {
            string getUnitQuari = "SELECT * FROM units where building_Building_Id = '" + Building_Name_DropDownList.SelectedValue + "'";
            MySqlCommand getUnitCmd = new MySqlCommand(getUnitQuari, _sqlCon);
            MySqlDataAdapter getUnitDt = new MySqlDataAdapter(getUnitCmd);
            getUnitCmd.Connection = _sqlCon;
            _sqlCon.Open();
            getUnitDt.SelectCommand = getUnitCmd;
            DataTable getUnitDataTable = new DataTable();
            getUnitDt.Fill(getUnitDataTable);
            Unit_GridView.DataSource = getUnitDataTable;
            Unit_GridView.DataBind();
            _sqlCon.Close();
        }
        protected void Unit_GridView_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            string Half; CheckBox CB;
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                try
                {
                    Half = ((Label)e.Row.FindControl("Half")).Text;
                    CB = ((CheckBox)e.Row.FindControl("CheckBox1"));
                    if (Half == "1")
                    {
                        e.Row.ForeColor = System.Drawing.Color.Blue;
                        e.Row.BackColor = System.Drawing.Color.AliceBlue;
                        CB.Checked = true;
                    }
                }
                catch
                {
                    Half = "";
                }
            }
        }
        protected void Renew_Arcive_Building_Contract()
        {
            //  Update New_ReNewed_Expaired to 2 in Contract Table
            string contractId = Request.QueryString["Id"];
            string New_ReNewed_ExpairedQuery = "UPDATE arcive_building_contract SET New_ReNewed_Expaired=@New_ReNewed_Expaired  WHERE Contract_Id=@ID ";
            _sqlCon.Open();
            using (MySqlCommand UpdateContractCmd = new MySqlCommand(New_ReNewed_ExpairedQuery, _sqlCon))
            {
                UpdateContractCmd.Parameters.AddWithValue("@ID", contractId);
                //Fill The Database with All DropDownLists Items
                UpdateContractCmd.Parameters.AddWithValue("@New_ReNewed_Expaired", "2");
                UpdateContractCmd.ExecuteNonQuery();
                _sqlCon.Close();
            }


            //  insert The Contract Information in Contract Tabel in DB
            string RenewContractQuery = "Insert Into arcive_building_contract (" +
                                      "Contract_Details , " +
                                      "Payment_Amount  , " +
                                      "Payment_Amount_L  , " +
                                      "Date_Of_Sgin        , " +
                                      "Start_Date       , " +
                                      "End_Date         , " +
                                      "users_user_ID , " +
                                      "building_Building_Id , " +
                                      "New_ReNewed_Expaired , " +
                                      "Start_Free_Period , " +
                                      "Duration_free_period , " +
                                      "maintenance , " +
                                      "Rental_allowed_Or_Not_allowed , " +
                                      "reason_for_rent_Reason_For_Rent_Id   , " +
                                      "contract_type_Contract_Type_Id   , " +
                                      "contract_template_Contract_template_Id , " +
                                      "Number_Of_Mounth , " +
                                      "Number_Of_Years , " +
                                      "tenants_Tenants_ID , " +
                                      "Com_rep ," +
                                      "Paymen_Method ," +
                                      "payment_type_payment_type_Id) " +
                                      "VALUES( " +
                                      "@Contract_Details , " +
                                      "@Payment_Amount  , " +
                                      "@Payment_Amount_L  , " +
                                      "@Date_Of_Sgin        , " +
                                      "@Start_Date       , " +
                                      "@End_Date         , " +
                                      "@users_user_ID , " +
                                      "@building_Building_Id , " +
                                      "@New_ReNewed_Expaired , " +
                                      "@Start_Free_Period , " +
                                      "@Duration_free_period , " +
                                      "@maintenance , " +
                                      "@Rental_allowed_Or_Not_allowed , " +
                                      "@reason_for_rent_Reason_For_Rent_Id   , " +
                                      "@contract_type_Contract_Type_Id   , " +
                                      "@contract_template_Contract_template_Id , " +
                                      "@Number_Of_Mounth , " +
                                      "@Number_Of_Years , " +
                                      "@tenants_Tenants_ID , " +
                                      "@Com_rep ," +
                                      "@Paymen_Method ," +
                                      "@payment_type_payment_type_Id ) ";
            _sqlCon.Open();
            using (MySqlCommand RenewContractCmd = new MySqlCommand(RenewContractQuery, _sqlCon))
            {
                //Fill The Database with All DropDownLists Items
                RenewContractCmd.Parameters.AddWithValue("@contract_type_Contract_Type_Id",
                    Contract_Type_DropDownList.SelectedValue);
                RenewContractCmd.Parameters.AddWithValue("@contract_template_Contract_template_Id",
                    Contract_Templet_DropDownList.SelectedValue);
                //RenewContractCmd.Parameters.AddWithValue("@payment_frequency_Payment_Frequency_Id",
                //    Payment_Frquancy_DropDownList.SelectedValue);
                RenewContractCmd.Parameters.AddWithValue("@tenants_Tenants_ID", Tenan_Name_DropDownList.SelectedValue);
                RenewContractCmd.Parameters.AddWithValue("@payment_type_payment_type_Id", Payment_Type_DropDownList.SelectedValue);
                RenewContractCmd.Parameters.AddWithValue("@building_Building_Id", Building_Name_DropDownList.SelectedValue);
                RenewContractCmd.Parameters.AddWithValue("@users_user_ID", Session["user_ID"]);
                RenewContractCmd.Parameters.AddWithValue("@New_ReNewed_Expaired", "1");
                RenewContractCmd.Parameters.AddWithValue("@reason_for_rent_Reason_For_Rent_Id", Reason_For_Rent_DropDownList.SelectedValue);
                RenewContractCmd.Parameters.AddWithValue("@Start_Free_Period", txt_FREE_PERIOD.Text);
                RenewContractCmd.Parameters.AddWithValue("@Duration_free_period", txt_Duration_Of_The_Free_Period.Text);
                RenewContractCmd.Parameters.AddWithValue("@Com_rep", Com_Rep_DropDownList.SelectedValue);
                RenewContractCmd.Parameters.AddWithValue("@maintenance", maintenance_RadioButtonList.SelectedValue);
                RenewContractCmd.Parameters.AddWithValue("@Rental_allowed_Or_Not_allowed", Rental_allowed_Or_Not_allowed_RadioButtonList.SelectedValue);
                RenewContractCmd.Parameters.AddWithValue("@Paymen_Method", Paymen_Method_RadioButtonList.SelectedItem.Text.Trim());
                //Fill The Database with All Textbox Items
                RenewContractCmd.Parameters.AddWithValue("@Contract_Details", txt_Contract_Details.Text);
                RenewContractCmd.Parameters.AddWithValue("@Payment_Amount", txt_Payment_Amount.Text);
                RenewContractCmd.Parameters.AddWithValue("@Payment_Amount_L", txt_Payment_Amount_L.Text);
                RenewContractCmd.Parameters.AddWithValue("@Date_Of_Sgin", txt_Sign_Date.Text);
                RenewContractCmd.Parameters.AddWithValue("@Start_Date", txt_Start_Date.Text);
                RenewContractCmd.Parameters.AddWithValue("@End_Date", txt_End_Date.Text);
                if (Contract_Type_DropDownList.SelectedValue == "1")
                {
                    RenewContractCmd.Parameters.AddWithValue("@Number_Of_Years", txt_No_Of_Months_Or_Years.Text);
                    RenewContractCmd.Parameters.AddWithValue("@Number_Of_Mounth", "");
                }
                else
                {
                    RenewContractCmd.Parameters.AddWithValue("@Number_Of_Years", "");
                    RenewContractCmd.Parameters.AddWithValue("@Number_Of_Mounth", txt_No_Of_Months_Or_Years.Text);
                }

                RenewContractCmd.ExecuteNonQuery();
                _sqlCon.Close();
            }
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
                    //    //Fill Tenant Name DropDownList
                    Helper.LoadDropDownList("SELECT * FROM tenants", _sqlCon, Tenan_Name_DropDownList, "Tenants_English_Name", "Tenants_ID");
                    Tenan_Name_DropDownList.Items.Insert(0, "...............");

                    //    //Fill Com_Rep_DropDownList
                    string Tenan_Name_ID = Tenan_Name_DropDownList.SelectedValue;
                    Helper.LoadDropDownList(
                        "SELECT * FROM company_representative where tenants_Tenants_ID ='" + Tenan_Name_ID + "'", _sqlCon, Com_Rep_DropDownList, "Com_rep_En_Name", "Company_representative_Id"); Com_Rep_DropDownList.Items.Insert(0, "إختر اسم الممثل ....");
                    Com_Rep_DropDownList.SelectedValue = "1";

                    //    //Fill Ownership Name DropDownList
                    Helper.LoadDropDownList("SELECT * FROM owner_ship where Active !='1'", _sqlCon, Ownership_Name_DropDownList, "Owner_Ship_En_Name", "Owner_Ship_Id");
                    Ownership_Name_DropDownList.Items.Insert(0, "...............");

                    //    //Fill Building Name DropDownList
                    Helper.LoadDropDownList("SELECT * FROM building ", _sqlCon, Building_Name_DropDownList, "Building_English_Name", "Building_Id");
                    Building_Name_DropDownList.Items.Insert(0, "...............");


                    //    //Fill contract_type DropDownList
                    Helper.LoadDropDownList("SELECT * FROM contract_type", _sqlCon, Contract_Type_DropDownList, "Contract_English_Type", "Contract_Type_Id");
                    Contract_Type_DropDownList.Items.Insert(0, "...............");

                    //    //Fill Contract templet DropDownList
                    Helper.LoadDropDownList("SELECT * FROM contract_template", _sqlCon, Contract_Templet_DropDownList, "Contract_English_template", "Contract_template_Id");
                    Contract_Templet_DropDownList.Items.Insert(0, "...............");

                    //    //Fill Paymaent Type DropDownList
                    Helper.LoadDropDownList("SELECT * FROM payment_type", _sqlCon, Payment_Type_DropDownList, "payment_English_type", "payment_type_Id");
                    Payment_Type_DropDownList.Items.Insert(0, "...............");

                    //    //Fill Cheque_type DropDownList
                    Helper.LoadDropDownList("SELECT * FROM cheque_type", _sqlCon, Cheque_Type_DropDownList, "Cheque_English_Type", "Cheque_Type_id");
                    Cheque_Type_DropDownList.Items.Insert(0, "...............");

                    //    //Fill Bank_Cheque_Name_DropDownList DropDownList
                    Helper.LoadDropDownList("SELECT * FROM bank", _sqlCon, Bank_Cheque_Name_DropDownList, "Bank_English_Name", "Bank_Id");
                    Bank_Cheque_Name_DropDownList.Items.Insert(0, "...............");

                    //    //Fill Reason_For_Rent_DropDownList
                    Reason_For_Rent_DropDownList.Items.Insert(0, "...............");

                    //    //Fill Tenant Name DropDownList
                    Helper.LoadDropDownList("SELECT * FROM transformation_bank", _sqlCon, transformation_Bank_DropDownList, "Bank_Name_En", "transformation_Bank_ID");
                    transformation_Bank_DropDownList.Items.Insert(0, "...............");


                    //Get Reason_For_Rent_DropDownList DropDownList
                    Reason_For_Rent_DropDownList.Items.Clear();
                    Reason_For_Rent_DropDownList.Items.Add(new ListItem("Family Accommodation", "1"));
                    Reason_For_Rent_DropDownList.Items.Add(new ListItem("Business", "2"));
                    Reason_For_Rent_DropDownList.Items.Add(new ListItem("Singles housing", "3"));
                    Reason_For_Rent_DropDownList.Items.Insert(0, "...............");

                    //Get maintenance_RadioButtonList DropDownList
                    maintenance_RadioButtonList.Items.Clear();
                    maintenance_RadioButtonList.Items.Add(new ListItem("On The Lessor", "1"));
                    maintenance_RadioButtonList.Items.Add(new ListItem("On TThe Tenant", "2"));

                    //Get Rental_allowed_Or_Not_allowed_RadioButtonList DropDownList
                    Rental_allowed_Or_Not_allowed_RadioButtonList.Items.Clear();
                    Rental_allowed_Or_Not_allowed_RadioButtonList.Items.Add(new ListItem("Allowed", "1"));
                    Rental_allowed_Or_Not_allowed_RadioButtonList.Items.Add(new ListItem("Not Allowed", "2"));


                    //Get Paymen_Method_RadioButtonList DropDownList
                    Paymen_Method_RadioButtonList.Items.Clear();
                    Paymen_Method_RadioButtonList.Items.Add(new ListItem("Cheques", "1"));
                    Paymen_Method_RadioButtonList.Items.Add(new ListItem("Transformation", "2"));
                    Paymen_Method_RadioButtonList.Items.Add(new ListItem("Cash", "3"));


                    lbl_titel_Add_New_Tenant.Text = Dt.Rows[44]["En"].ToString();
                    lbl_Contract_Type.Text = Dt.Rows[1]["EN"].ToString();
                    lbl_Tenan_Name.Text = Dt.Rows[2]["EN"].ToString();
                    lbl_Com_Rep.Text = Dt.Rows[3]["EN"].ToString();
                    lbl_Ownership_Name.Text = Dt.Rows[4]["EN"].ToString();
                    lbl_Building_Name.Text = Dt.Rows[5]["EN"].ToString();
                    lbl_Reason_For_Rent.Text = Dt.Rows[7]["EN"].ToString();
                    lbl_Contact_Period.Text = Dt.Rows[8]["EN"].ToString();
                    lbl_Sign_Date.Text = Dt.Rows[9]["EN"].ToString();
                    lbl_Start_Date.Text = Dt.Rows[10]["EN"].ToString();
                    lbl_End_Date.Text = Dt.Rows[11]["EN"].ToString();
                    lbl_Payment_Type.Text = Dt.Rows[12]["EN"].ToString();
                    lbl_Payment_Amount.Text = Dt.Rows[13]["EN"].ToString();
                    lbl_Payment_Amount_L.Text = Dt.Rows[14]["EN"].ToString();
                    lbl_maintenance.Text = Dt.Rows[15]["EN"].ToString();
                    lbl_Rental_allowed_Or_Not_allowed.Text = Dt.Rows[16]["EN"].ToString();
                    lbl_Contract_Details.Text = Dt.Rows[17]["EN"].ToString();
                    lbl_FREE_PERIOD.Text = Dt.Rows[18]["EN"].ToString();
                    lbl_Duration_Of_The_Free_Period.Text = Dt.Rows[19]["EN"].ToString();
                    lbl_Paymen_Method.Text = Dt.Rows[20]["EN"].ToString();
                    serial_CheckBox.Text = "Entering Serial Checks";
                    lbl_Worrnig_Cheque.Text = "Incomplete Check Information (Tenant name / Cheque type / Bank name)";
                    lbl_Cheque_NO.Text = Dt.Rows[22]["EN"].ToString();
                    lbl_Cheque_Date.Text = Dt.Rows[23]["EN"].ToString();
                    lbl_Cheque_Value.Text = Dt.Rows[24]["EN"].ToString();
                    lbl_Cheque_Type.Text = Dt.Rows[25]["EN"].ToString();
                    lbl_Bank_Name.Text = Dt.Rows[26]["EN"].ToString();
                    lbl_Owner.Text = Dt.Rows[27]["EN"].ToString();
                    lbl_beneficiary.Text = Dt.Rows[28]["EN"].ToString();
                    btn_Cheque_Date_Chosee.Text = Dt.Rows[21]["EN"].ToString();
                    lbl_Cash_Amount.Text = Dt.Rows[32]["EN"].ToString();
                    lbl_Cash_Date.Text = Dt.Rows[33]["EN"].ToString();
                    Cash_Date_Button.Text = Dt.Rows[21]["EN"].ToString();
                    lbl_transformation_No.Text = Dt.Rows[29]["EN"].ToString();
                    lbl_transformation_Bank.Text = Dt.Rows[26]["EN"].ToString();
                    lbl_transformation_Date.Text = Dt.Rows[30]["EN"].ToString();
                    btn_Add_Contract.Text = Dt.Rows[34]["EN"].ToString();
                    btn_Back_To_Contract_List.Text = Dt.Rows[35]["EN"].ToString();
                    FREE_PERIOD_CheckBox.Text = Dt.Rows[18]["EN"].ToString();
                    Additional_Items_CheckBox.Text = Dt.Rows[17]["EN"].ToString();
                    Sign_Date_Chosee.Text = Dt.Rows[21]["EN"].ToString();
                    Start_Date_Chosee.Text = Dt.Rows[21]["EN"].ToString();
                    End_Date_Chosee.Text = Dt.Rows[21]["EN"].ToString();
                    lbl_transformation_Amount.Text = Dt.Rows[31]["EN"].ToString();
                    transformation_Date_Button.Text = Dt.Rows[21]["EN"].ToString();


                    Unit_GridView.Columns[6].HeaderText = Dt.Rows[37]["EN"].ToString();
                    Unit_GridView.Columns[7].HeaderText = Dt.Rows[38]["EN"].ToString();
                    Unit_GridView.Columns[8].HeaderText = Dt.Rows[39]["EN"].ToString();
                    Unit_GridView.Columns[9].HeaderText = Dt.Rows[40]["EN"].ToString();
                    Unit_GridView.Columns[10].HeaderText = Dt.Rows[41]["EN"].ToString();
                    Unit_GridView.Columns[11].HeaderText = Dt.Rows[42]["EN"].ToString();
                    Unit_GridView.Columns[12].HeaderText = Dt.Rows[43]["EN"].ToString();


                    Cheque_GridView.Columns[1].HeaderText = Dt.Rows[22]["EN"].ToString();
                    Cheque_GridView.Columns[2].HeaderText = Dt.Rows[23]["EN"].ToString();
                    Cheque_GridView.Columns[3].HeaderText = Dt.Rows[24]["EN"].ToString();
                    Cheque_GridView.Columns[4].HeaderText = Dt.Rows[25]["EN"].ToString();
                    Cheque_GridView.Columns[7].HeaderText = Dt.Rows[26]["EN"].ToString();
                    Cheque_GridView.Columns[9].HeaderText = Dt.Rows[2]["EN"].ToString();
                    Cheque_GridView.Columns[11].HeaderText = Dt.Rows[27]["EN"].ToString();
                    Cheque_GridView.Columns[12].HeaderText = Dt.Rows[28]["EN"].ToString();

                    transformation_GridView.Columns[0].HeaderText = Dt.Rows[29]["EN"].ToString();
                    transformation_GridView.Columns[1].HeaderText = Dt.Rows[26]["EN"].ToString();
                    transformation_GridView.Columns[3].HeaderText = Dt.Rows[30]["EN"].ToString();
                    transformation_GridView.Columns[4].HeaderText = Dt.Rows[31]["EN"].ToString();
                    transformation_GridView.Columns[5].HeaderText = Dt.Rows[2]["EN"].ToString();

                    Cash_GridView.Columns[0].HeaderText = Dt.Rows[32]["EN"].ToString();
                    Cash_GridView.Columns[1].HeaderText = Dt.Rows[33]["EN"].ToString();
                    Cash_GridView.Columns[2].HeaderText = Dt.Rows[2]["EN"].ToString();




                    //Contract_Templet_RequiredFieldValidator.ErrorMessage = "* Required ";
                    Tenan_Name_RequiredFieldValidator.ErrorMessage = "* Required ";
                    //Com_Rep_RequiredFieldValidator.ErrorMessage = "* Required ";
                    Ownership_Name_RequiredFieldValidator.ErrorMessage = "* Required ";
                    Building_Name_RequiredFieldValidator.ErrorMessage = "* Required ";
                    // Units_RequiredFieldValidator.ErrorMessage = "* Required ";
                    Reason_For_Rent_RequiredFieldValidator.ErrorMessage = "* Required ";
                    Contract_Type_RequiredFieldValidator.ErrorMessage = "* Required ";
                    No_Of_Months_RequiredFieldValidator.ErrorMessage = "* Required ";
                    Payment_Type_RequiredFieldValidator.ErrorMessage = "* Required ";
                    Payment_Amount_RequiredFieldValidator.ErrorMessage = "* Required ";
                    Payment_Amount_L_RequiredFieldValidator.ErrorMessage = "* Required ";
                    maintenance_RequiredFieldValidator.ErrorMessage = "* Required ";
                    Rental_allowed_Or_Not_allowed_RequiredFieldValidator.ErrorMessage = "* Required ";
                    Paymen_Method_Req_Fiel_Val.ErrorMessage = "* Required ";
                    Contract_Details_RegularExpressionValidator.ErrorMessage = "Only letters";
                    Payment_Amount_RegularExpressionValidator.ErrorMessage = "Only Numbers";

                }
                else
                {
                    //    //Fill Tenant Name DropDownList
                    Helper.LoadDropDownList("SELECT * FROM tenants", _sqlCon, Tenan_Name_DropDownList, "Tenants_Arabic_Name", "Tenants_ID");
                    Tenan_Name_DropDownList.Items.Insert(0, "...............");

                    //    //Fill Com_Rep_DropDownList
                    string Tenan_Name_ID = Tenan_Name_DropDownList.SelectedValue;
                    Helper.LoadDropDownList(
                        "SELECT * FROM company_representative where tenants_Tenants_ID ='" + Tenan_Name_ID + "'", _sqlCon, Com_Rep_DropDownList, "Com_rep_En_Name", "Company_representative_Id"); Com_Rep_DropDownList.Items.Insert(0, "إختر اسم الممثل ....");
                    Com_Rep_DropDownList.SelectedValue = "1";

                    //    //Fill Ownership Name DropDownList
                    Helper.LoadDropDownList("SELECT * FROM owner_ship where Active !='1'", _sqlCon, Ownership_Name_DropDownList, "Owner_Ship_AR_Name", "Owner_Ship_Id");
                    Ownership_Name_DropDownList.Items.Insert(0, "...............");

                    //    //Fill Building Name DropDownList
                    Helper.LoadDropDownList("SELECT * FROM building ", _sqlCon, Building_Name_DropDownList, "Building_Arabic_Name", "Building_Id");
                    Building_Name_DropDownList.Items.Insert(0, "...............");

                    //    //Fill contract_type DropDownList
                    Helper.LoadDropDownList("SELECT * FROM contract_type", _sqlCon, Contract_Type_DropDownList, "Contract_Arabic_Type", "Contract_Type_Id");
                    Contract_Type_DropDownList.Items.Insert(0, "...............");

                    //    //Fill Contract templet DropDownList
                    Helper.LoadDropDownList("SELECT * FROM contract_template", _sqlCon, Contract_Templet_DropDownList, "Contract_Arabic_template", "Contract_template_Id");
                    Contract_Templet_DropDownList.Items.Insert(0, "...............");

                    //    //Fill Paymaent Type DropDownList
                    Helper.LoadDropDownList("SELECT * FROM payment_type", _sqlCon, Payment_Type_DropDownList, "payment_Arabic_type", "payment_type_Id");
                    Payment_Type_DropDownList.Items.Insert(0, "...............");

                    //    //Fill Cheque_type DropDownList
                    Helper.LoadDropDownList("SELECT * FROM cheque_type", _sqlCon, Cheque_Type_DropDownList, "Cheque_arabic_Type", "Cheque_Type_id");
                    Cheque_Type_DropDownList.Items.Insert(0, "...............");

                    //    //Fill Bank_Cheque_Name_DropDownList DropDownList
                    Helper.LoadDropDownList("SELECT * FROM bank", _sqlCon, Bank_Cheque_Name_DropDownList, "Bank_Arabic_Name", "Bank_Id");
                    Bank_Cheque_Name_DropDownList.Items.Insert(0, "...............");

                    //    //Fill Reason_For_Rent_DropDownList
                    Reason_For_Rent_DropDownList.Items.Insert(0, "...............");

                    //    //Fill Tenant Name DropDownList
                    Helper.LoadDropDownList("SELECT * FROM transformation_bank", _sqlCon, transformation_Bank_DropDownList, "Bank_Name", "transformation_Bank_ID");
                    transformation_Bank_DropDownList.Items.Insert(0, "...............");

                    //Get Reason_For_Rent_DropDownList DropDownList
                    Reason_For_Rent_DropDownList.Items.Clear();
                    Reason_For_Rent_DropDownList.Items.Add(new ListItem("سكن عائلي", "1"));
                    Reason_For_Rent_DropDownList.Items.Add(new ListItem("عمل تجاري", "2"));
                    Reason_For_Rent_DropDownList.Items.Add(new ListItem("سكن عزاب", "3"));
                    Reason_For_Rent_DropDownList.Items.Insert(0, "...............");

                    //Get maintenance_RadioButtonList DropDownList
                    maintenance_RadioButtonList.Items.Clear();
                    maintenance_RadioButtonList.Items.Add(new ListItem("على المؤجر", "1"));
                    maintenance_RadioButtonList.Items.Add(new ListItem("على المستأجر", "2"));

                    //Get maintenance_RadioButtonList DropDownList
                    Rental_allowed_Or_Not_allowed_RadioButtonList.Items.Clear();
                    Rental_allowed_Or_Not_allowed_RadioButtonList.Items.Add(new ListItem("يجوز", "1"));
                    Rental_allowed_Or_Not_allowed_RadioButtonList.Items.Add(new ListItem("لا يجوز", "2"));

                    //Get Paymen_Method_RadioButtonList DropDownList
                    Paymen_Method_RadioButtonList.Items.Clear();
                    Paymen_Method_RadioButtonList.Items.Add(new ListItem("شيكات", "1"));
                    Paymen_Method_RadioButtonList.Items.Add(new ListItem("تحويل", "2"));
                    Paymen_Method_RadioButtonList.Items.Add(new ListItem("نقداً", "3"));




                    lbl_titel_Add_New_Tenant.Text = Dt.Rows[44]["AR"].ToString();
                    lbl_Contract_Type.Text = Dt.Rows[1]["AR"].ToString();
                    lbl_Tenan_Name.Text = Dt.Rows[2]["AR"].ToString();
                    lbl_Com_Rep.Text = Dt.Rows[3]["AR"].ToString();
                    lbl_Ownership_Name.Text = Dt.Rows[4]["AR"].ToString();
                    lbl_Building_Name.Text = Dt.Rows[5]["AR"].ToString();
                    lbl_Reason_For_Rent.Text = Dt.Rows[7]["AR"].ToString();
                    lbl_Contact_Period.Text = Dt.Rows[8]["AR"].ToString();
                    lbl_Sign_Date.Text = Dt.Rows[9]["AR"].ToString();
                    lbl_Start_Date.Text = Dt.Rows[10]["AR"].ToString();
                    lbl_End_Date.Text = Dt.Rows[11]["AR"].ToString();
                    lbl_Payment_Type.Text = Dt.Rows[12]["AR"].ToString();
                    lbl_Payment_Amount.Text = Dt.Rows[13]["AR"].ToString();
                    lbl_Payment_Amount_L.Text = Dt.Rows[14]["AR"].ToString();
                    lbl_maintenance.Text = Dt.Rows[15]["AR"].ToString();
                    lbl_Rental_allowed_Or_Not_allowed.Text = Dt.Rows[16]["AR"].ToString();
                    lbl_Contract_Details.Text = Dt.Rows[17]["AR"].ToString();
                    lbl_FREE_PERIOD.Text = Dt.Rows[18]["AR"].ToString();
                    lbl_Duration_Of_The_Free_Period.Text = Dt.Rows[19]["AR"].ToString();
                    lbl_Paymen_Method.Text = Dt.Rows[20]["AR"].ToString();
                    serial_CheckBox.Text = "إدخال شيكات متسلسلة ";
                    lbl_Worrnig_Cheque.Text = "معلومات الشيك غير كاملة ( اسم المستأجر  /  نوع الشيك  /  اسم البنك)";
                    lbl_Cheque_NO.Text = Dt.Rows[22]["AR"].ToString();
                    lbl_Cheque_Date.Text = Dt.Rows[23]["AR"].ToString();
                    lbl_Cheque_Value.Text = Dt.Rows[24]["AR"].ToString();
                    lbl_Cheque_Type.Text = Dt.Rows[25]["AR"].ToString();
                    lbl_Bank_Name.Text = Dt.Rows[26]["AR"].ToString();
                    lbl_Owner.Text = Dt.Rows[27]["AR"].ToString();
                    lbl_beneficiary.Text = Dt.Rows[28]["AR"].ToString();
                    btn_Cheque_Date_Chosee.Text = Dt.Rows[21]["AR"].ToString();
                    lbl_Cash_Amount.Text = Dt.Rows[32]["AR"].ToString();
                    lbl_Cash_Date.Text = Dt.Rows[33]["AR"].ToString();
                    Cash_Date_Button.Text = Dt.Rows[21]["AR"].ToString();
                    lbl_transformation_No.Text = Dt.Rows[29]["AR"].ToString();
                    lbl_transformation_Bank.Text = Dt.Rows[26]["AR"].ToString();
                    lbl_transformation_Date.Text = Dt.Rows[30]["AR"].ToString();
                    lbl_transformation_Amount.Text = Dt.Rows[31]["AR"].ToString();
                    transformation_Date_Button.Text = Dt.Rows[21]["AR"].ToString();
                    FREE_PERIOD_CheckBox.Text = Dt.Rows[18]["AR"].ToString();
                    Additional_Items_CheckBox.Text = Dt.Rows[17]["AR"].ToString();
                    Sign_Date_Chosee.Text = Dt.Rows[21]["AR"].ToString();
                    Start_Date_Chosee.Text = Dt.Rows[21]["AR"].ToString();
                    End_Date_Chosee.Text = Dt.Rows[21]["AR"].ToString();
                    btn_Add_Contract.Text = Dt.Rows[34]["AR"].ToString();
                    btn_Back_To_Contract_List.Text = Dt.Rows[35]["AR"].ToString();

                    Unit_GridView.Columns[6].HeaderText = Dt.Rows[37]["AR"].ToString();
                    Unit_GridView.Columns[7].HeaderText = Dt.Rows[38]["AR"].ToString();
                    Unit_GridView.Columns[8].HeaderText = Dt.Rows[39]["AR"].ToString();
                    Unit_GridView.Columns[9].HeaderText = Dt.Rows[40]["AR"].ToString();
                    Unit_GridView.Columns[10].HeaderText = Dt.Rows[41]["AR"].ToString();
                    Unit_GridView.Columns[11].HeaderText = Dt.Rows[42]["AR"].ToString();
                    Unit_GridView.Columns[12].HeaderText = Dt.Rows[43]["AR"].ToString();

                    Cheque_GridView.Columns[1].HeaderText = Dt.Rows[22]["AR"].ToString();
                    Cheque_GridView.Columns[2].HeaderText = Dt.Rows[23]["AR"].ToString();
                    Cheque_GridView.Columns[3].HeaderText = Dt.Rows[24]["AR"].ToString();
                    Cheque_GridView.Columns[4].HeaderText = Dt.Rows[25]["AR"].ToString();
                    Cheque_GridView.Columns[7].HeaderText = Dt.Rows[26]["AR"].ToString();
                    Cheque_GridView.Columns[9].HeaderText = Dt.Rows[2]["AR"].ToString();
                    Cheque_GridView.Columns[11].HeaderText = Dt.Rows[27]["AR"].ToString();
                    Cheque_GridView.Columns[12].HeaderText = Dt.Rows[28]["AR"].ToString();

                    transformation_GridView.Columns[0].HeaderText = Dt.Rows[29]["AR"].ToString();
                    transformation_GridView.Columns[1].HeaderText = Dt.Rows[26]["AR"].ToString();
                    transformation_GridView.Columns[3].HeaderText = Dt.Rows[30]["AR"].ToString();
                    transformation_GridView.Columns[4].HeaderText = Dt.Rows[31]["AR"].ToString();
                    transformation_GridView.Columns[5].HeaderText = Dt.Rows[2]["AR"].ToString();

                    Cash_GridView.Columns[0].HeaderText = Dt.Rows[32]["AR"].ToString();
                    Cash_GridView.Columns[1].HeaderText = Dt.Rows[33]["AR"].ToString();
                    Cash_GridView.Columns[2].HeaderText = Dt.Rows[2]["AR"].ToString();





                    //Contract_Templet_RequiredFieldValidator.ErrorMessage = "*مطلوب ";
                    Tenan_Name_RequiredFieldValidator.ErrorMessage = "*مطلوب ";
                    //Com_Rep_RequiredFieldValidator.ErrorMessage = "*مطلوب ";
                    Ownership_Name_RequiredFieldValidator.ErrorMessage = "*مطلوب ";
                    Building_Name_RequiredFieldValidator.ErrorMessage = "*مطلوب ";
                    //Units_RequiredFieldValidator.ErrorMessage = "*مطلوب ";
                    Reason_For_Rent_RequiredFieldValidator.ErrorMessage = "*مطلوب ";
                    Contract_Type_RequiredFieldValidator.ErrorMessage = "*مطلوب ";
                    No_Of_Months_RequiredFieldValidator.ErrorMessage = "*مطلوب ";
                    Payment_Type_RequiredFieldValidator.ErrorMessage = "*مطلوب ";
                    Payment_Amount_RequiredFieldValidator.ErrorMessage = "*مطلوب ";
                    Payment_Amount_L_RequiredFieldValidator.ErrorMessage = "*مطلوب ";
                    maintenance_RequiredFieldValidator.ErrorMessage = "*مطلوب ";
                    Rental_allowed_Or_Not_allowed_RequiredFieldValidator.ErrorMessage = "*مطلوب ";
                    Paymen_Method_Req_Fiel_Val.ErrorMessage = "*مطلوب ";
                    Contract_Details_RegularExpressionValidator.ErrorMessage = "أحرف فقط";
                    Payment_Amount_RegularExpressionValidator.ErrorMessage = "أرقام فقط";

                }
            }
            _sqlCon.Close();
        }







    }
}