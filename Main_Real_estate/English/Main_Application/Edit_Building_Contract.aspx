﻿<%@ Page Title="" Language="C#" MasterPageFile="~/English/Main_Application/English.Master" AutoEventWireup="true" CodeBehind="Edit_Building_Contract.aspx.cs" Inherits="Main_Real_estate.English.Main_Application.Edit_Building_Contract" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <style>
        table {
            width: 100%;
        }

        th {
            border: 1px solid #dddddd;
            text-align: center;
            padding: 8px;
        }

        td {
            border: 1px solid #dddddd;
            padding: 8px;
        }
    </style>

    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>

    <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.3.1/jquery.min.js"></script>
    <script src="https://cdnjs.cloudflare.com/ajax/libs/chosen/1.8.7/chosen.jquery.min.js"></script>
    <link href="../CSS/DDL.css" rel="stylesheet" />

    <div class="container-fluid" id="container-wrapper">
        <!----------------------------------------------------------------------------------------------------------->
        <div class="d-sm-flex align-items-center justify-content-between mb-4">
            <h1 class="h3 mb-0 text-gray-800">
                <asp:Label ID="lbl_titel_Add_New_Tenant" runat="server" Text="تعديل عقد المستأجر : "></asp:Label>
                <asp:Label ID="Contract_id" runat="server"></asp:Label>
                <asp:Label ID="Contarct_tenant_Name" runat="server"></asp:Label>
                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
      <asp:Label ID="lbl_Success_Add_New_Contract" runat="server" ForeColor="#00FF40"></asp:Label>
            </h1>
        </div>
        <!----------------------------------------------------------------------------------------------------------->
        <div class="row">
            <div class="col-lg-12">
                <div class="card mb-12">
                    <div class="card-body">

                        <div class="row">
                            <div class="col-lg">
                                <div class="form-group">
                                    <asp:Label ID="lbl_Contract_Type" runat="server" ></asp:Label>
                                    <asp:DropDownList ID="Contract_Templet_DropDownList" runat="server" Enabled="false" CssClass="form-control">
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="Contract_Templet_RequiredFieldValidator" ValidationGroup="Contract_RequiredField" ControlToValidate="Contract_Templet_DropDownList"
                                        InitialValue="..............." runat="server" ErrorMessage="* حقل مطلوب !!!" ForeColor="Red"></asp:RequiredFieldValidator>
                                </div>
                            </div>
                            <div class="col-lg">
                                <div class="form-group">
                                    <asp:Label ID="lbl_Tenan_Name" runat="server" ></asp:Label>
                                    <asp:DropDownList ID="Tenan_Name_DropDownList" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="Tenan_Name_DropDownList_SelectedIndexChanged">
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="Tenan_Name_RequiredFieldValidator" ControlToValidate="Tenan_Name_DropDownList"
                                        InitialValue="..............." runat="server" ValidationGroup="Contract_RequiredField" ErrorMessage="* حقل مطلوب !!!" ForeColor="Red"></asp:RequiredFieldValidator>
                                </div>
                            </div>
                            <div class="col-lg" id="Com_Rep_Div" runat="server">
                                <div class="form-group">
                                    <asp:Label ID="lbl_Com_Rep" runat="server" ></asp:Label>
                                    <asp:DropDownList ID="Com_Rep_DropDownList" runat="server" CssClass="form-control">
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="Com_Rep_RequiredFieldValidator" ControlToValidate="Com_Rep_DropDownList"
                                        InitialValue="..............." runat="server" ValidationGroup="Contract_RequiredField" ErrorMessage="* حقل مطلوب !!!" ForeColor="Red"></asp:RequiredFieldValidator>
                                </div>
                            </div>
                        </div>


                        <div class="row">


                            <div class="col-lg">
                                <div class="form-group">
                                    <asp:Label ID="lbl_Ownership_Name" runat="server" ></asp:Label>
                                    <asp:DropDownList ID="Ownership_Name_DropDownList" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="Ownership_Name_DropDownList_SelectedIndexChanged">
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="Ownership_Name_RequiredFieldValidator" ControlToValidate="Ownership_Name_DropDownList"
                                        InitialValue="..............." runat="server" ValidationGroup="Contract_RequiredField" ErrorMessage="* حقل مطلوب !!!" ForeColor="Red"></asp:RequiredFieldValidator>
                                </div>
                            </div>

                            <div class="col-lg">
                                <div class="form-group">
                                    <asp:Label ID="lbl_Building_Name" runat="server" ></asp:Label>
                                    &nbsp;
                                    <asp:Label ID="Half_Contract_Worrning" runat="server" ForeColor="Blue"></asp:Label>
                                    <asp:DropDownList ID="Building_Name_DropDownList" runat="server" CssClass="form-control" AutoPostBack="true">
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="Building_Name_RequiredFieldValidator" ControlToValidate="Building_Name_DropDownList"
                                        InitialValue="..............." runat="server" ValidationGroup="Contract_RequiredField" ErrorMessage="* حقل مطلوب !!!" ForeColor="Red"></asp:RequiredFieldValidator>
                                </div>
                            </div>
                        </div>



                        <div class="row">
                            <div class="col-lg-12">
                                <div class="container-fluid" style="border-style: solid; border-color: #efefef">
                                    <!-- Simple Tables -->
                                    <asp:GridView ID="Unit_GridView" runat="server" AutoGenerateColumns="False"
                                        BackColor="White" BorderColor="#DEDFDE" BorderStyle="None" BorderWidth="1px" CellPadding="4" OnRowDataBound="Unit_GridView_RowDataBound"
                                        ForeColor="Black" GridLines="Both">
                                        <AlternatingRowStyle BackColor="White" />
                                        <Columns>
                                            <asp:TemplateField HeaderText="" Visible="false"> <%--0--%>
                                                <ItemTemplate>
                                                    <asp:Label ID="lbl_Unit_Id" runat="server" Text='<%# Bind("Unit_ID") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="حالة" Visible="false"><%--1--%>
                                                <ItemTemplate>
                                                    <asp:Label ID="unit_condition_Unit_Condition_Id" runat="server" Text='<%# Bind("unit_condition_Unit_Condition_Id") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="تفصيل" Visible="false"><%--2--%>
                                                <ItemTemplate>
                                                    <asp:Label ID="unit_detail_Unit_Detail_Id" runat="server" Text='<%# Bind("unit_detail_Unit_Detail_Id") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="نوع" Visible="false"><%--3--%>
                                                <ItemTemplate>
                                                    <asp:Label ID="unit_type_Unit_Type_Id" runat="server" Text='<%# Bind("unit_type_Unit_Type_Id") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="بناء" Visible="false"><%--4--%>
                                                <ItemTemplate>
                                                    <asp:Label ID="building_Building_Id" runat="server" Text='<%# Bind("building_Building_Id") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="فرش" Visible="false"><%--5--%>
                                                <ItemTemplate>
                                                    <asp:Label ID="furniture_case_Furniture_case_Id" runat="server" Text='<%# Bind("furniture_case_Furniture_case_Id") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="رقم الوحدة"><%--6--%>
                                                <ItemTemplate>
                                                    <asp:Label ID="Unit_Number" runat="server" Text='<%# Bind("Unit_Number") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="طابق"><%--7--%>
                                                <ItemTemplate>
                                                    <asp:Label ID="Floor_Number" runat="server" Text='<%# Bind("Floor_Number") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="مساحة"><%--8--%>
                                                <ItemTemplate>
                                                    <asp:Label ID="Unit_Space" runat="server" Text='<%# Bind("Unit_Space") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="وضع"><%--9--%>
                                                <ItemTemplate>
                                                    <asp:Label ID="current_situation" runat="server" Text='<%# Bind("current_situation") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="أوريدو"><%--10--%>
                                                <ItemTemplate>
                                                    <asp:Label ID="Oreedo_Number" runat="server" Text='<%# Bind("Oreedo_Number") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="كهرباء"><%--11--%>
                                                <ItemTemplate>
                                                    <asp:Label ID="Electricityt_Number" runat="server" Text='<%# Bind("Electricityt_Number") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="المياه"><%--12--%>
                                                <ItemTemplate>
                                                    <asp:Label ID="Water_Number" runat="server" Text='<%# Bind("Water_Number") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Half" Visible="false"><%--13--%>
                                                <ItemTemplate>
                                                    <asp:Label ID="Half" runat="server" Text='<%# Bind("Half") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="" ControlStyle-Width="25" Visible="false"><%--14--%>
                                                <EditItemTemplate>
                                                    <asp:CheckBox ID="CheckBox1" runat="server" />
                                                </EditItemTemplate>
                                                <ItemTemplate>
                                                    <asp:CheckBox ID="CheckBox1" runat="server" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                        <EditRowStyle HorizontalAlign="Center" />
                                        <RowStyle Font-Size="15px" />
                                        <FooterStyle BackColor="#CCCC99" ForeColor="Black" HorizontalAlign="Center" />
                                        <HeaderStyle BackColor="#eaecf4" Font-Bold="false" ForeColor="Black" Font-Size="15px" HorizontalAlign="Center" />
                                        <PagerStyle BackColor="White" ForeColor="Black" HorizontalAlign="Center" />
                                        <RowStyle HorizontalAlign="Center" />
                                        <SelectedRowStyle BackColor="#CC3333" Font-Bold="false" ForeColor="White" />
                                        <SortedAscendingCellStyle BackColor="#F7F7F7" />
                                        <SortedAscendingHeaderStyle BackColor="#4B4B4B" />
                                        <SortedDescendingCellStyle BackColor="#E5E5E5" />
                                        <SortedDescendingHeaderStyle BackColor="#242121" />
                                    </asp:GridView>
                                </div>
                            </div>
                            <asp:Label ID="Half_Building_id" runat="server"></asp:Label>

                        </div>




                        <%--------------------------------------------------------------------------------------------------------------------------------------------------------------------%>
                        <div class="row">



                            <div class="col-lg">
                                <div id="div_Reason_For_Rent" runat="server">
                                    <div class="form-group">
                                        <asp:Label ID="lbl_Reason_For_Rent" runat="server" ></asp:Label>
                                        <asp:DropDownList ID="Reason_For_Rent_DropDownList" runat="server" CssClass="form-control"></asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="Reason_For_Rent_RequiredFieldValidator" ValidationGroup="Contract_RequiredField" ControlToValidate="Reason_For_Rent_DropDownList"
                                            InitialValue="..............." runat="server" ErrorMessage="* حقل مطلوب !!!" ForeColor="Red"></asp:RequiredFieldValidator>
                                    </div>
                                </div>
                            </div>

                            <div class="col-lg">
                                <div class="form-group">
                                    <asp:Label ID="lbl_Contact_Period" runat="server" ></asp:Label>
                                    <asp:DropDownList ID="Contract_Type_DropDownList" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="Contract_Type_DropDownList_SelectedIndexChanged">
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="Contract_Type_RequiredFieldValidator" ValidationGroup="Contract_RequiredField" ControlToValidate="Contract_Type_DropDownList"
                                        InitialValue="..............." runat="server" ErrorMessage="* حقل مطلوب !!!" ForeColor="Red"></asp:RequiredFieldValidator>
                                </div>
                            </div>
                            <div class="col-lg">
                                <div id="div_No_Of_Months" runat="server">
                                    <div class="form-group">
                                        <asp:Label ID="lbl_No_Of_Months_Or_Years" runat="server"></asp:Label>
                                        <asp:TextBox ID="txt_No_Of_Months_Or_Years" runat="server" TextMode="Number" min="1"  step="1" CssClass="form-control" AutoPostBack="true" OnTextChanged="txt_No_Of_Months_Or_Years_TextChanged"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="No_Of_Months_RequiredFieldValidator" ValidationGroup="Contract_RequiredField" ControlToValidate="Contract_Type_DropDownList"
                                         runat="server" ErrorMessage="* حقل مطلوب !!!" ForeColor="Red"></asp:RequiredFieldValidator>
                                    </div>
                                </div>
                            </div>


                        </div>
                       <%--------------------------------------------------------------------------------------------------------------------------------------------------------------------%>
<div class="row">
    <div class="col-lg-3">
        <div class="form-group">
            <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                <ContentTemplate>
                    <asp:Label ID="lbl_Sign_Date" runat="server" ></asp:Label>&nbsp;
			<asp:RegularExpressionValidator runat="server" ControlToValidate="txt_Sign_Date" ValidationExpression="(((0|1)[0-9]|2[0-9]|3[0-1])\/(0[1-9]|1[0-2])\/((19|20)\d\d))$"
                    ErrorMessage="dd/MM/yyyy" ValidationGroup="Contract_RequiredField"  ForeColor="Red"/>
                    <asp:TextBox ID="txt_Sign_Date" runat="server" CssClass="form-control"></asp:TextBox>
                    <asp:Button ID="Sign_Date_Chosee" runat="server"  OnClick="Date_Chosee_Click"/>
                    <asp:ImageButton ID="ImageButton1" ImageUrl="Main_Image/Calander_Close.png" Width="10px" Height="10px" Visible="false" OnClick="ImageButton1_Click" runat="server"/>
                    <div id="Sign_Date_divCalendar" runat="server" style="position: page;" visible="false">

                        <asp:Calendar ID="Sign_Date_Calendar" runat="server" Height="200px" Width="220px" BackColor="White" BorderColor="#3366CC" BorderWidth="1px" CellPadding="1" DayNameFormat="Shortest" Font-Names="Verdana" Font-Size="8pt" ForeColor="#003399" OnSelectionChanged="Sign_Date_Calendar_SelectionChanged1">
                            <DayHeaderStyle BackColor="#99CCCC" ForeColor="#336666" Height="1px"/>
                            <NextPrevStyle Font-Size="8pt" ForeColor="#CCCCFF"/>
                            <OtherMonthDayStyle ForeColor="#999999"/>
                            <SelectedDayStyle BackColor="#009999" Font-Bold="True" ForeColor="#CCFF99"/>
                            <SelectorStyle BackColor="#99CCCC" ForeColor="#336666"/>
                            <TitleStyle BackColor="#003399" BorderColor="#3366CC" BorderWidth="1px" Font-Bold="True" Font-Size="10pt" ForeColor="#CCCCFF" Height="25px"/>
                            <TodayDayStyle BackColor="#99CCCC" ForeColor="White"/>
                            <WeekendDayStyle BackColor="#CCCCFF"/>
                        </asp:Calendar>
                    </div>
                </ContentTemplate>
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="Sign_Date_Chosee" EventName="Click"/>
                    <asp:AsyncPostBackTrigger ControlID="Sign_Date_Calendar" EventName="SelectionChanged"/>
                    <asp:AsyncPostBackTrigger ControlID="ImageButton3" EventName="Click"/>
                </Triggers>
            </asp:UpdatePanel>
        </div>
    </div>
    <div class="col-lg-3">
        <asp:UpdatePanel ID="Start_Date_UpdatePanel" runat="server">
            <ContentTemplate>
                <asp:Label ID="lbl_Start_Date" runat="server" ></asp:Label>&nbsp;
			<asp:RegularExpressionValidator runat="server" ControlToValidate="txt_Start_Date" ValidationExpression="(((0|1)[0-9]|2[0-9]|3[0-1])\/(0[1-9]|1[0-2])\/((19|20)\d\d))$"
                    ErrorMessage="dd/MM/yyyy" ValidationGroup="Contract_RequiredField"  ForeColor="Red"/>
                <asp:TextBox ID="txt_Start_Date" runat="server" CssClass="form-control"></asp:TextBox>
                <asp:Button ID="Start_Date_Chosee" runat="server"  OnClick="Start_Date_Chosee_Click"/>
                <asp:ImageButton ID="ImageButton2" ImageUrl="Main_Image/Calander_Close.png" Width="10px" Height="10px" Visible="false" OnClick="ImageButton2_Click" runat="server"/>
                <div id="Start_Date_Div" runat="server" visible="false" style="position: page;">
                    <asp:Calendar ID="Start_Date_Calendar" runat="server" Height="200px" Width="220px" BackColor="White" BorderColor="#3366CC" BorderWidth="1px" CellPadding="1" DayNameFormat="Shortest" Font-Names="Verdana" Font-Size="8pt" ForeColor="#003399" OnSelectionChanged="Start_Date_Calendar_SelectionChanged2">
                        <DayHeaderStyle BackColor="#99CCCC" ForeColor="#336666" Height="1px"/>
                        <NextPrevStyle Font-Size="8pt" ForeColor="#CCCCFF"/>
                        <OtherMonthDayStyle ForeColor="#999999"/>
                        <SelectedDayStyle BackColor="#009999" Font-Bold="True" ForeColor="#CCFF99"/>
                        <SelectorStyle BackColor="#99CCCC" ForeColor="#336666"/>
                        <TitleStyle BackColor="#003399" BorderColor="#3366CC" BorderWidth="1px" Font-Bold="True" Font-Size="10pt" ForeColor="#CCCCFF" Height="25px"/>
                        <TodayDayStyle BackColor="#99CCCC" ForeColor="White"/>
                        <WeekendDayStyle BackColor="#CCCCFF"/>
                    </asp:Calendar>
                </div>
            </ContentTemplate>
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="Start_Date_Calendar" EventName="SelectionChanged"/>
                <asp:AsyncPostBackTrigger ControlID="Start_Date_Chosee" EventName="Click"/>
                <asp:AsyncPostBackTrigger ControlID="ImageButton2" EventName="Click"/>
            </Triggers>
        </asp:UpdatePanel>
    </div>
    <div class="col-lg-3">
        <asp:UpdatePanel ID="End_Date_UpdatePanel" runat="server">
            <ContentTemplate>
                <asp:Label ID="lbl_End_Date" runat="server" ></asp:Label>&nbsp;
			<asp:RegularExpressionValidator runat="server" ControlToValidate="txt_End_Date" ValidationExpression="(((0|1)[0-9]|2[0-9]|3[0-1])\/(0[1-9]|1[0-2])\/((19|20)\d\d))$"
                    ErrorMessage="dd/MM/yyyy" ValidationGroup="Contract_RequiredField"  ForeColor="Red"/>
                <asp:TextBox ID="txt_End_Date" runat="server" CssClass="form-control" Enabled="false"></asp:TextBox>
                <asp:Button ID="End_Date_Chosee" runat="server"  OnClick="End_Date_Chosee_Click" Visible="false"/>
                <asp:ImageButton ID="ImageButton3" ImageUrl="Main_Image/Calander_Close.png" Width="10px" Height="10px" Visible="false" OnClick="ImageButton3_Click" runat="server"/>
                <div id="End_Date_Div" runat="server" visible="false" style="position: page;">
                    <asp:Calendar ID="End_Date_Calendar" runat="server" Height="200px" Width="220px" BackColor="White" BorderColor="#3366CC" BorderWidth="1px" CellPadding="1" DayNameFormat="Shortest" Font-Names="Verdana" Font-Size="8pt" ForeColor="#003399" OnSelectionChanged="End_Date_Calendar_SelectionChanged1">
                        <DayHeaderStyle BackColor="#99CCCC" ForeColor="#336666" Height="1px"/>
                        <NextPrevStyle Font-Size="8pt" ForeColor="#CCCCFF"/>
                        <OtherMonthDayStyle ForeColor="#999999"/>
                        <SelectedDayStyle BackColor="#009999" Font-Bold="True" ForeColor="#CCFF99"/>
                        <SelectorStyle BackColor="#99CCCC" ForeColor="#336666"/>
                        <TitleStyle BackColor="#003399" BorderColor="#3366CC" BorderWidth="1px" Font-Bold="True" Font-Size="10pt" ForeColor="#CCCCFF" Height="25px"/>
                        <TodayDayStyle BackColor="#99CCCC" ForeColor="White"/>
                        <WeekendDayStyle BackColor="#CCCCFF"/>
                    </asp:Calendar>
                </div>
            </ContentTemplate>
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="Sign_Date_Calendar" EventName="SelectionChanged"/>
                <asp:AsyncPostBackTrigger ControlID="Start_Date_Chosee" EventName="Click"/>
                <asp:AsyncPostBackTrigger ControlID="ImageButton3" EventName="Click"/>
            </Triggers>
        </asp:UpdatePanel>
    </div>
    <div class="col-lg-3">
        <div class="form-group">
            <asp:Label ID="lbl_Payment_Type" runat="server" ></asp:Label>
            <asp:DropDownList ID="Payment_Type_DropDownList" runat="server" CssClass="form-control">
            </asp:DropDownList>
            <asp:RequiredFieldValidator ID="Payment_Type_RequiredFieldValidator" ValidationGroup="Contract_RequiredField" ControlToValidate="Payment_Type_DropDownList"
            InitialValue="..............." runat="server" ErrorMessage="* حقل مطلوب !!!" ForeColor="Red"> </asp:RequiredFieldValidator>
        </div>
    </div>
</div>
<br/>
<%--------------------------------------------------------------------------------------------------------------------------------------------------------------------%>
<div class="row">
    <div class="col-lg-3">
        <div class="form-group">
            <asp:Label ID="lbl_Payment_Amount" runat="server" ></asp:Label>&nbsp;
            <asp:RegularExpressionValidator ID="Payment_Amount_RegularExpressionValidator" runat="server" ControlToValidate="txt_Payment_Amount" Font-Size="15px"
            EnableClientScript="True" ErrorMessage="أرقام فقط" ForeColor="Red"  ValidationExpression="[0-9]+"> </asp:RegularExpressionValidator>
            <asp:TextBox ID="txt_Payment_Amount" runat="server" CssClass="form-control"></asp:TextBox>
            <asp:RequiredFieldValidator ID="Payment_Amount_RequiredFieldValidator" ValidationGroup="Contract_RequiredField" ControlToValidate="txt_Payment_Amount"
            runat="server" ErrorMessage="* حقل مطلوب !!!" ForeColor="Red">  </asp:RequiredFieldValidator>
        </div>
    </div>
    <div class="col-lg-3">
        <div class="form-group">
            <asp:Label ID="lbl_Payment_Amount_L" runat="server"></asp:Label>&nbsp;
            <asp:TextBox ID="txt_Payment_Amount_L" runat="server" CssClass="form-control"></asp:TextBox>
            <asp:RequiredFieldValidator ID="Payment_Amount_L_RequiredFieldValidator" ValidationGroup="Contract_RequiredField" ControlToValidate="txt_Payment_Amount_L"
            runat="server" ErrorMessage="* حقل مطلوب !!!" ForeColor="Red"> </asp:RequiredFieldValidator>
        </div>
    </div>

    <div class="col-lg-3">
        <asp:Label ID="lbl_maintenance" runat="server" ></asp:Label>
        <asp:RadioButtonList ID="maintenance_RadioButtonList" runat="server" RepeatDirection="Horizontal"> </asp:RadioButtonList>
        <asp:RequiredFieldValidator ID="maintenance_RequiredFieldValidator" ValidationGroup="Contract_RequiredField" ControlToValidate="maintenance_RadioButtonList"
        runat="server" ErrorMessage="* حقل مطلوب !!!" ForeColor="Red">  </asp:RequiredFieldValidator>
    </div>

    <div class="col-lg-3">
        <asp:Label ID="lbl_Rental_allowed_Or_Not_allowed" runat="server" ></asp:Label>
        <asp:RadioButtonList ID="Rental_allowed_Or_Not_allowed_RadioButtonList" RepeatDirection="Horizontal" runat="server">  </asp:RadioButtonList>
        <asp:RequiredFieldValidator ID="Rental_allowed_Or_Not_allowed_RequiredFieldValidator" ValidationGroup="Contract_RequiredField"
        ControlToValidate="Rental_allowed_Or_Not_allowed_RadioButtonList" runat="server" ErrorMessage="* حقل مطلوب !!!" ForeColor="Red"> </asp:RequiredFieldValidator>
    </div>
    
</div>
<%--------------------------------------------------------------------------------------------------------------------------------------------------------------------%>
<%--------------------------------------------------------------------------------------------------------------------------------------------------------------------%>
<asp:UpdatePanel ID="Additional_Items_UpdatePanel" runat="server">
    <ContentTemplate>
        <div class="row">
            <div class="col-lg-6">
                <div class="form-group">
                    <asp:CheckBox ID="FREE_PERIOD_CheckBox" runat="server"   AutoPostBack="true" OnCheckedChanged="FREE_PERIOD_CheckBox_CheckedChanged" />
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:CheckBox ID="Additional_Items_CheckBox" runat="server"   AutoPostBack="true" OnCheckedChanged="Additional_Items_CheckBox_CheckedChanged" />
                </div>
            </div>
            <div class="col-lg-4">
                <div class="form-group">
                    <asp:Label ID="lbl_Real_Contract" runat="server" ></asp:Label>
                    <asp:FileUpload ID="Real_Contract_FileUpload" runat="server" CssClass="form-control" />
                    <asp:Label ID="Real_Contract_FileName" runat="server" Text="" Visible="false"></asp:Label>
                    <asp:Label ID="Real_Contract_Path" runat="server" Text="" Visible="false"></asp:Label>
                </div>
                <div runat="server" id="Real_Contract_Div">
                    <a runat="server" id="Link_Real_Contract" style="font-size: 15px;">
                        <i class="fa fa-paperclip" style="font-size: 20px;"></i>
                        <asp:Label ID="lbl_Link_Real_Contract" runat="server" Text=""></asp:Label>
                    </a>
                    <asp:ImageButton ID="Btn_Remove_Link_Real_Contract" OnClick="Btn_Remove_Link_Real_Contract_Click" runat="server" Width="15px" Height="15px" ImageUrl="Main_Image/bin.png" />
                </div>
            </div>
            <div class="col-lg-12" id="Contract_Details_Div" runat="server" visible="false">
                <div class="form-group">
                    <asp:Label ID="lbl_Contract_Details" runat="server"  ></asp:Label>
                    &nbsp;
                    <asp:RegularExpressionValidator ID="Contract_Details_RegularExpressionValidator" runat="server" ControlToValidate="txt_Contract_Details"
                    EnableClientScript="True" ErrorMessage="!!! يُسمح فقط بالأحرف العربية" ForeColor="Red" ValidationExpression="[ا-ي أآى ة ئ ء]+"> </asp:RegularExpressionValidator>
                    <asp:TextBox ID="txt_Contract_Details" runat="server" CssClass="form-control" TextMode="MultiLine" ReadOnly="false"></asp:TextBox>
                </div>
            </div>
        </div>
        <div class="row" id="FREE_PERIOD_Div" runat="server" visible="false">
            <div class="col-lg-3">
                <div class="form-group">
                    <asp:Label ID="lbl_FREE_PERIOD" runat="server"  ></asp:Label>
                    <asp:TextBox ID="txt_FREE_PERIOD" runat="server" CssClass="form-control"></asp:TextBox>
                </div>
            </div>
            <div class="col-lg-3">
                <div class="form-group">
                    <asp:Label ID="lbl_Duration_Of_The_Free_Period" runat="server"  ></asp:Label>
                    <asp:TextBox ID="txt_Duration_Of_The_Free_Period" runat="server" TextMode="Number" min="1"  step="1" CssClass="form-control" AutoPostBack="true" OnTextChanged="txt_Duration_Of_The_Free_Period_TextChanged"></asp:TextBox>
                </div>
            </div>
        </div>
    </ContentTemplate>
    <Triggers>
        <asp:AsyncPostBackTrigger ControlID="Additional_Items_CheckBox" EventName="CheckedChanged"/>
        <asp:AsyncPostBackTrigger ControlID="FREE_PERIOD_CheckBox" EventName="CheckedChanged"/>
    </Triggers>
</asp:UpdatePanel>
<hr/>
<%--------------------------------------------------------------Cheque / transformation / Cash Gridviwes ------------------------------------------------------------------------------------------------------%>
   

    <div class="col-lg-3">
        <asp:Label ID="lbl_Paymen_Method" runat="server" ></asp:Label>
        <asp:RadioButtonList ID="Paymen_Method_RadioButtonList" runat="server" RepeatDirection="Horizontal" AutoPostBack="true" OnSelectedIndexChanged="Paymen_Method_RadioButtonList_SelectedIndexChanged">
        </asp:RadioButtonList>
        <asp:RequiredFieldValidator ID="Paymen_Method_Req_Fiel_Val" ValidationGroup="Contract_RequiredField"
        ControlToValidate="Paymen_Method_RadioButtonList" runat="server" ErrorMessage="* حقل مطلوب !!!" ForeColor="Red"> </asp:RequiredFieldValidator>
    </div>

                        <div id="container" class="container-fluid" style="border-radius: 10px;">
                            <div calss="row">
                                <h4>
                                    <asp:Label ID="lbl_Tenant_Cheque" runat="server"></asp:Label></h4>

                                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                    <ContentTemplate>
                                        <div class="row">
                                            <div class="table-responsive">
                                                <asp:GridView ID="Contract_Cheque_List" DataKeyNames="Cheques_Id" runat="server" AutoGenerateColumns="false"
                                                     OnRowDataBound="RowDataBound" OnRowDeleting="Contract_Cheque_List_RowDeleting" >
                                                    <Columns>
                                                        <asp:TemplateField HeaderText="#" ItemStyle-Width="10">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblRowNumber" Text='<%# Container.DataItemIndex + 1 %>' runat="server" />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <%--------------------------------------------------------------------------------------------------%>
                                                        <asp:TemplateField HeaderText="رقم الشيك">
                                                            <EditItemTemplate>
                                                                <asp:TextBox ID="txt_Cheques_No" runat="server" Text='<%# Bind("Cheques_No") %>'>  
                                                                </asp:TextBox>
                                                            </EditItemTemplate>
                                                            <ItemTemplate>
                                                                <asp:Label ID="lbl_Cheques_No" runat="server" Text='<%# Eval("Cheques_No") %>'>  
                                                                </asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <%--------------------------------------------------------------------------------------------------%>
                                                        <asp:TemplateField HeaderText="تاريخ الشيك">
                                                            <EditItemTemplate>
                                                                <asp:Label ID="lbl_Cheques_Date" runat="server" Text='<%# Eval("Cheques_Date") %>'>  </asp:Label>
                                                                <asp:Calendar ID="Calendar2" runat="server">
                                                                    <DayHeaderStyle BackColor="#99CCCC" ForeColor="#336666" Height="1px" />
                                                                    <NextPrevStyle Font-Size="8pt" ForeColor="#CCCCFF" />
                                                                    <OtherMonthDayStyle ForeColor="#999999" />
                                                                    <TitleStyle BackColor="#003399" BorderColor="#3366CC" BorderWidth="1px" Font-Bold="True" Font-Size="10pt" ForeColor="#CCCCFF" Height="25px" />
                                                                    <WeekendDayStyle BackColor="#CCCCFF" />
                                                                </asp:Calendar>
                                                            </EditItemTemplate>
                                                            <ItemTemplate>
                                                                <asp:Label ID="lbl_Cheques_Date" runat="server" Text='<%# Eval("Cheques_Date") %>'>  </asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <%------------------------------------------------------------------------%>
                                                        <asp:TemplateField HeaderText="قيمة الشيك">
                                                            <EditItemTemplate>
                                                                <asp:TextBox ID="txt_Cheques_Amount" runat="server" Text='<%# Bind("Cheques_Amount") %>'>  
                                                                </asp:TextBox>
                                                            </EditItemTemplate>
                                                            <ItemTemplate>
                                                                <asp:Label ID="lbl_Cheques_Amount" runat="server" Text='<%# String.Format("{0:###,###,####}",Convert.ToInt64(DataBinder.Eval(Container.DataItem, "Cheques_Amount")))%>'>   
                                                                </asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <%------------------------------------------------------------------------%>
                                                        <asp:TemplateField HeaderText="نوع الشيك">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lbl_cheque_type" runat="server" Text='<%# Eval("Cheque_arabic_Type")%>'></asp:Label>
                                                                <asp:Label ID="lbl_Cheque_English_Type" runat="server" Text='<%# Eval("Cheque_english_Type")%>'></asp:Label>
                                                            </ItemTemplate>
                                                            <EditItemTemplate>
                                                                <asp:Label Visible="false" ID="lbl_cheque_Type" runat="server" Text='<%# Eval("Cheque_arabic_Type")%>'></asp:Label>
                                                                <asp:DropDownList ID="cheque_type_DropDownList" runat="server">
                                                                </asp:DropDownList>
                                                            </EditItemTemplate>
                                                        </asp:TemplateField>
                                                        <%-- ---------------------------------------------------------------------------------%>
                                                        <asp:TemplateField HeaderText="اسم البنك">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lbl_bank" runat="server" Text='<%# Eval("Bank_Arabic_Name")%>'></asp:Label>
                                                                <asp:Label ID="lbl_Bank_English_Name" runat="server" Text='<%# Eval("Bank_English_Name")%>'></asp:Label>
                                                            </ItemTemplate>
                                                            <EditItemTemplate>
                                                                <asp:DropDownList ID="bank_DropDownList" runat="server">
                                                                </asp:DropDownList>
                                                            </EditItemTemplate>
                                                        </asp:TemplateField>
                                                        <%-----------------------------------------------------------------------------------%>
                                                        <asp:TemplateField HeaderText="صاحب الشيك">
                                                            <EditItemTemplate>
                                                                <asp:TextBox ID="txt_Cheque_Owner" runat="server" Text='<%# Bind("Cheque_Owner") %>'>  
                                                                </asp:TextBox>
                                                            </EditItemTemplate>
                                                            <ItemTemplate>
                                                                <asp:Label ID="lbl_Cheque_Owner" runat="server" Text='<%# Eval("Cheque_Owner") %>'>  
                                                                </asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                         <%--------------------------------------------------------------------------------------------------%>
                                                        <asp:TemplateField HeaderText="اسم المستفيد">
                                                            <EditItemTemplate>
                                                                <asp:TextBox ID="txt_beneficiary_person" runat="server" Text='<%# Bind("beneficiary_person") %>'>  
                                                                </asp:TextBox>
                                                            </EditItemTemplate>
                                                            <ItemTemplate>
                                                                <asp:Label ID="lbl_beneficiary_person" runat="server" Text='<%# Eval("beneficiary_person") %>'>  
                                                                </asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <%--------------------------------------------------------------------------------------------------%>
                                                        <asp:CommandField ShowDeleteButton="true"  ButtonType="Image" DeleteImageUrl="~/English/Main_Application/Main_Image/Calander_Close.png" ControlStyle-Width="25px" ControlStyle-Height="25px" />
                                                    </Columns>
                                                    <RowStyle HorizontalAlign="Center" />
                                                </asp:GridView>
                                            </div>
                                        </div>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </div>
                             <%--************************************************************** transformation Gridviwes *********************************************************************************************************************--%>
                            <div class="row">
                                <div class="table-responsive">
                                    <asp:GridView Width="100%" ID="transformation_GridView" DataKeyNames="transformation_Table_ID" runat="server" AutoGenerateColumns="false" 
                                        OnRowDeleting="transformation_GridView_RowDeleting" OnRowDataBound="transformation_GridView_RowDataBound">
                                        <Columns>
                                            <asp:BoundField DataField="transformation_No" HeaderText="رقم الحوالة"  />
                                          
                                            <asp:TemplateField HeaderText="اسم البنك"><%--1--%>
                                                <ItemTemplate>
                                                    <asp:Label ID="lbl_AR_Bank_Name" runat="server" Text='<%# Eval("Bank_Name") %>' />  
                                                    <asp:Label ID="lbl_EN_Bank_Name" runat="server" Text='<%# Eval("Bank_Name_En") %>' />
                                                </ItemTemplate>
                                            </asp:TemplateField>


                                            <asp:BoundField DataField="transformation_Date" HeaderText="تاريخ التحويل"  />
                                            <asp:BoundField DataField="Amount" HeaderText="قيمة التحويل"  />
                                            

                                             <asp:TemplateField HeaderText="اسم المستأجر"><%--4--%>
                                                <ItemTemplate>
                                                    <asp:Label ID="lbl_Tenants_Arabic_Name" runat="server" Text='<%# Eval("Tenants_Arabic_Name") %>' />  
                                                    <asp:Label ID="lbl_Tenants_English_Name" runat="server" Text='<%# Eval("Tenants_English_Name") %>' />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <%-------------------------------------------------------------------------------------------------%>
                                            <asp:CommandField ItemStyle-Width="10px" ShowDeleteButton="True" ButtonType="Image" DeleteImageUrl="~/English/Main_Application/Main_Image/Delete.png" ControlStyle-Width="25px" ControlStyle-Height="25px" />
                                        </Columns>
                                        <RowStyle HorizontalAlign="Center" />
                                    </asp:GridView>
                                </div>
                            </div>
                            <%--************************************************************** Cash Gridviwes *********************************************************************************************************************--%>
                            <div class="row">
                                <div class="table-responsive">
                                    <asp:GridView Width="100%" ID="Cash_GridView" runat="server" AutoGenerateColumns="false" DataKeyNames="Cash_Amount_ID" 
                                        OnRowDeleting="Cash_GridView_RowDeleting" OnRowDataBound="Cash_GridView_RowDataBound">
                                        <Columns>
                                            <asp:BoundField DataField="Cash_Amount" HeaderText="قيمة الدفعة"  />
                                            <asp:BoundField DataField="Cash_Date" HeaderText="تاريخ الدفعة"  />
                                            

                                            <asp:TemplateField HeaderText="اسم المستأجر"><%--2--%>
                                                <ItemTemplate>
                                                    <asp:Label ID="lbl_Tenants_Arabic_Name" runat="server" Text='<%# Eval("Tenants_Arabic_Name") %>' />  
                                                    <asp:Label ID="lbl_Tenants_English_Name" runat="server" Text='<%# Eval("Tenants_English_Name") %>' />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <%-------------------------------------------------------------------------------------------------%>
                                            <asp:CommandField ItemStyle-Width="10px" ShowDeleteButton="True" ButtonType="Image" DeleteImageUrl="~/English/Main_Application/Main_Image/Delete.png" ControlStyle-Width="25px" ControlStyle-Height="25px" />
                                        </Columns>
                                        <RowStyle HorizontalAlign="Center" />
                                    </asp:GridView>
                                </div>
                            </div>
                        </div>
                    </div>
                        <%--************************************************************** transformation Input Feild *********************************************************************************************************************--%>
                    </div>
                    <div class="container-fluid" style="border-style: solid; border-radius: 10px;">
                        <%--************************************************************** transformation Input Feild *********************************************************************************************************************--%>

                        <div class="row" runat="server" id="transformation_Div" visible="false">
                            <div class="col-lg-3">
                                <div class="form-group">
                                    <asp:Label ID="lbl_transformation_No" runat="server" ></asp:Label>
                                    <asp:TextBox ID="txt_transformation_No" runat="server" CssClass="form-control"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-lg-2">
                                <div class="form-group">
                                    <asp:Label ID="lbl_transformation_Bank" runat="server" ></asp:Label>
                                    <asp:DropDownList ID="transformation_Bank_DropDownList" runat="server" CssClass="form-control">
                                    </asp:DropDownList>
                                </div>
                            </div>
                            <div class="col-lg-3">
                                <div class="form-group">
                                    <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                        <ContentTemplate>
                                            <asp:Label ID="lbl_transformation_Date" runat="server" ></asp:Label>&nbsp;
                            <asp:TextBox ID="txt_transformation_Date" runat="server" CssClass="form-control"></asp:TextBox>
                                            <asp:Button ID="transformation_Date_Button" runat="server"  OnClick="transformation_Date_Button_Click" />
                                            <asp:ImageButton ID="ImageButton5" ImageUrl="Main_Image/Calander_
                                                
                                                .png" Width="10px" Height="10px" Visible="false" OnClick="ImageButton5_Click" runat="server" />
                                            <div id="transformation_Date_Div" runat="server" style="position: page;" visible="false">

                                                <asp:Calendar ID="transformation_Date_Calendar" runat="server" Height="200px" Width="220px" BackColor="White" BorderColor="#3366CC" BorderWidth="1px" CellPadding="1" DayNameFormat="Shortest" Font-Names="Verdana" Font-Size="8pt" ForeColor="#003399" OnSelectionChanged="transformation_Date_Calendar_SelectionChanged">
                                                    <DayHeaderStyle BackColor="#99CCCC" ForeColor="#336666" Height="1px" />
                                                    <NextPrevStyle Font-Size="8pt" ForeColor="#CCCCFF" />
                                                    <OtherMonthDayStyle ForeColor="#999999" />
                                                    <SelectedDayStyle BackColor="#009999" Font-Bold="True" ForeColor="#CCFF99" />
                                                    <SelectorStyle BackColor="#99CCCC" ForeColor="#336666" />
                                                    <TitleStyle BackColor="#003399" BorderColor="#3366CC" BorderWidth="1px" Font-Bold="True" Font-Size="10pt" ForeColor="#CCCCFF" Height="25px" />
                                                    <TodayDayStyle BackColor="#99CCCC" ForeColor="White" />
                                                    <WeekendDayStyle BackColor="#CCCCFF" />
                                                </asp:Calendar>
                                            </div>
                                        </ContentTemplate>
                                        <Triggers>
                                            <asp:AsyncPostBackTrigger ControlID="transformation_Date_Button" EventName="Click" />
                                            <asp:AsyncPostBackTrigger ControlID="transformation_Date_Calendar" EventName="SelectionChanged" />
                                            <asp:AsyncPostBackTrigger ControlID="ImageButton5" EventName="Click" />
                                        </Triggers>
                                    </asp:UpdatePanel>
                                </div>
                            </div>
                            <div class="col-lg-2">
                                <div class="form-group">
                                    <asp:Label ID="lbl_transformation_Amount" runat="server" ></asp:Label>
                                    <asp:TextBox ID="txt_transformation_Amount" runat="server" CssClass="form-control"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-lg-2">
                                <br />
                                <asp:ImageButton ID="btn_Add_Transformation" ImageUrl="Main_Image/plus.png" Width="25px" Height="25px" runat="server" OnClick="btn_Add_Transformation_Click" />
                            </div>
                        </div>

                        <%--************************************************************** Cash Input Feild *********************************************************************************************************************--%>


                        <div class="row" runat="server" id="Cash_div" visible="false">
                            <div class="col-lg-4">
                                <div class="form-group">
                                    <asp:Label ID="lbl_Cash_Amount" runat="server" ></asp:Label>
                                    <asp:TextBox ID="txt_Cash_Amount" runat="server" CssClass="form-control"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-lg-4">
                                <div class="form-group">
                                    <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                                        <ContentTemplate>
                                            <asp:Label ID="lbl_Cash_Date" runat="server" ></asp:Label>&nbsp;
                            <asp:TextBox ID="txt_Cash_Date" runat="server" CssClass="form-control"></asp:TextBox>
                                            <asp:Button ID="Cash_Date_Button" runat="server"  OnClick="Cash_Date_Button_Click" />
                                            <asp:ImageButton ID="ImageButton6" ImageUrl="Main_Image/Calander_Close.png" Width="10px" Height="10px" Visible="false" OnClick="ImageButton6_Click" runat="server" />
                                            <div id="Cash_Date_Div" runat="server" style="position: page;" visible="false">
                                                <asp:Calendar ID="Cash_Date_Calendar" runat="server" Height="200px" Width="220px" BackColor="White" BorderColor="#3366CC" BorderWidth="1px" CellPadding="1" DayNameFormat="Shortest" Font-Names="Verdana" Font-Size="8pt" ForeColor="#003399" OnSelectionChanged="Cash_Date_Calendar_SelectionChanged">
                                                    <DayHeaderStyle BackColor="#99CCCC" ForeColor="#336666" Height="1px" />
                                                    <NextPrevStyle Font-Size="8pt" ForeColor="#CCCCFF" />
                                                    <OtherMonthDayStyle ForeColor="#999999" />
                                                    <SelectedDayStyle BackColor="#009999" Font-Bold="True" ForeColor="#CCFF99" />
                                                    <SelectorStyle BackColor="#99CCCC" ForeColor="#336666" />
                                                    <TitleStyle BackColor="#003399" BorderColor="#3366CC" BorderWidth="1px" Font-Bold="True" Font-Size="10pt" ForeColor="#CCCCFF" Height="25px" />
                                                    <TodayDayStyle BackColor="#99CCCC" ForeColor="White" />
                                                    <WeekendDayStyle BackColor="#CCCCFF" />
                                                </asp:Calendar>
                                            </div>
                                        </ContentTemplate>
                                        <Triggers>
                                            <asp:AsyncPostBackTrigger ControlID="transformation_Date_Button" EventName="Click" />
                                            <asp:AsyncPostBackTrigger ControlID="transformation_Date_Calendar" EventName="SelectionChanged" />
                                            <asp:AsyncPostBackTrigger ControlID="ImageButton5" EventName="Click" />
                                        </Triggers>
                                    </asp:UpdatePanel>
                                </div>
                            </div>
                            <div class="col-lg-4">
                                <br />
                                <asp:ImageButton ID="Add_Cash" ImageUrl="Main_Image/plus.png" Width="25px" Height="25px" runat="server" OnClick="Add_Cash_Click" />
                            </div>
                        </div>

                        <%--******************************************************************** Cheque Input Feild *******************************************************************************************--%>
                        <div calss="row">
                            <div class="row" style="overflow-x: auto;" runat="server" id="Cheque_Div">
                                &nbsp;&nbsp;
                                <asp:Label ID="lbl_Worrnig_Cheque"  runat="server" ForeColor="Red" Visible="false"></asp:Label>
                                <div class="col-lg-12">
                                    <div class="form-group">

                                        <table>
                                            <tr>
                                                <th><asp:Label ID="lbl_Cheque_NO" runat="server" /></th>
                                                <th><asp:Label ID="lbl_Cheque_Date" runat="server" /></th>
                                                <th><asp:Label ID="lbl_Cheque_Value" runat="server" /></th>
                                                <th><asp:Label ID="lbl_Cheque_Type" runat="server" /></th>
                                                <th><asp:Label ID="lbl_Bank_Name" runat="server" /></th>
                                                <th><asp:Label ID="lbl_Owner" runat="server" /></th>
                                                <th><asp:Label ID="lbl_beneficiary" runat="server" /></th>
                                                <th></th>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:TextBox ID="txt_Cheque_NO" runat="server"></asp:TextBox></td>
                                                <td>
                                                    <asp:UpdatePanel ID="Cheque_Date_UpdatePanel" runat="server">
                                                        <ContentTemplate>
                                                            <asp:TextBox ID="txt_Cheque_Date" runat="server" Enabled="false"></asp:TextBox>
                                                            <asp:Button ID="btn_Cheque_Date_Chosee" runat="server"  OnClick="btn_Cheque_Date_Chosee_Click" />
                                                            <asp:ImageButton ID="Cheque_Date_ImageButton" ImageUrl="Main_Image/Calander_Close.png" Width="10px" Height="10px" Visible="false" OnClick="Cheque_Date_ImageButton_Click" runat="server" />
                                                            <div id="Cheque_Date_Div" runat="server" visible="false" style="position: page;">
                                                                <asp:Calendar ID="Cheque_Date_Calendar" runat="server" Height="200px" Width="220px" BackColor="White" BorderColor="#3366CC" BorderWidth="1px" CellPadding="1" DayNameFormat="Shortest" Font-Names="Verdana" Font-Size="8pt" ForeColor="#003399" OnSelectionChanged="Cheque_Date_Calendar_SelectionChanged">
                                                                    <DayHeaderStyle BackColor="#99CCCC" ForeColor="#336666" Height="1px" />
                                                                    <NextPrevStyle Font-Size="8pt" ForeColor="#CCCCFF" />
                                                                    <OtherMonthDayStyle ForeColor="#999999" />
                                                                    <SelectedDayStyle BackColor="#009999" Font-Bold="True" ForeColor="#CCFF99" />
                                                                    <SelectorStyle BackColor="#99CCCC" ForeColor="#336666" />
                                                                    <TitleStyle BackColor="#003399" BorderColor="#3366CC" BorderWidth="1px" Font-Bold="True" Font-Size="10pt" ForeColor="#CCCCFF" Height="25px" />
                                                                    <TodayDayStyle BackColor="#99CCCC" ForeColor="White" />
                                                                    <WeekendDayStyle BackColor="#CCCCFF" />
                                                                </asp:Calendar>
                                                            </div>
                                                        </ContentTemplate>
                                                        <Triggers>
                                                            <asp:AsyncPostBackTrigger ControlID="Cheque_Date_Calendar" EventName="SelectionChanged" />
                                                            <asp:AsyncPostBackTrigger ControlID="btn_Cheque_Date_Chosee" EventName="Click" />
                                                            <asp:AsyncPostBackTrigger ControlID="Cheque_Date_ImageButton" EventName="Click" />
                                                        </Triggers>
                                                    </asp:UpdatePanel>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txt_Cheque_Value" runat="server"></asp:TextBox></td>
                                                <td>
                                                    <asp:DropDownList ID="Cheque_Type_DropDownList" runat="server"></asp:DropDownList></td>
                                                <td>
                                                    <asp:DropDownList ID="Bank_Cheque_Name_DropDownList" runat="server" Width="150px"></asp:DropDownList></td>
                                                <td>
                                                    <asp:TextBox ID="txt_Cheque_Owner" runat="server"></asp:TextBox></td>
                                                <td>
                                                    <asp:TextBox ID="txt_beneficiary_person" runat="server"></asp:TextBox></td>
                                                <td>
                                                    <asp:ImageButton ID="ImageButton4" ImageUrl="Main_Image/plus.png" Width="25px" Height="25px" runat="server" OnClick="ImageButton4_Click" /></td>
                                            </tr>
                                        </table>
                                    </div>
                                </div>
                            </div>
                        </div>
                </div>
            </div>
        </div>
    </div><br />
    <%-----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------%>
        <div class="row">
            <div class="col-lg-3">
                <br />
                <asp:Button ID="btn_Add_Contract" runat="server"  CssClass="btn  mb-1" BackColor="#52a2da" ForeColor="White" Width="248px" ValidationGroup="Contract_RequiredField" OnClick="btn_Add_Contract_Click" />
            </div>
            <div class="col-lg-3">
                <br />
                <asp:Button ID="btn_Back_To_Contract_List" runat="server"  ValidationGroup="x" CssClass="btn btn-light mb-1" OnClick="btn_Back_To_Contract_List_Click" />
            </div>
             <div >
                <br />
                <asp:LinkButton ID="Delete_Contract"  runat="server" ValidationGroup="Delete" OnClick="Delete_Contract_Click" OnClientClick="return confirm('Are you sure you want to delete?');"  ><i class="fa fa-trash" style="font-size:40px; color:#0779c9"></i></asp:LinkButton>
            </div>
            <div class="col-lg-3">
                 <div class="form-group" runat="server" id="Reason_Div">
                    <asp:Label ID="lbl_Reason_Delete" runat="server"  ></asp:Label>
                    <asp:TextBox ID="txt_Reason_Delete" TextMode="MultiLine" runat="server" CssClass="form-control"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="Delete_ReqFieVal" ControlToValidate="txt_Reason_Delete"
                    runat="server" ErrorMessage="* يرجى توضيح سبب الحذف" ForeColor="Red" ValidationGroup="Delete"></asp:RequiredFieldValidator>
                </div>
            </div>
        </div>
    <%-----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------%>

    <script>$('#<%=Tenan_Name_DropDownList.ClientID%>').chosen();</script>
    <script>$('#<%=Payment_Type_DropDownList.ClientID%>').chosen();</script>
    <%--<script>$('#<%=Payment_Frquancy_DropDownList.ClientID%>').chosen();</script>--%>
    <script>$('#<%=Contract_Type_DropDownList.ClientID%>').chosen();</script>
    <script>$('#<%=Contract_Templet_DropDownList.ClientID%>').chosen();</script>
    <script>$('#<%=Ownership_Name_DropDownList.ClientID%>').chosen();</script>
    <script>$('#<%=Building_Name_DropDownList.ClientID%>').chosen();</script>
    <script>$('#<%=Com_Rep_DropDownList.ClientID%>').chosen();</script>
    <script>$('#<%=Reason_For_Rent_DropDownList.ClientID%>').chosen();</script>
</asp:Content>
