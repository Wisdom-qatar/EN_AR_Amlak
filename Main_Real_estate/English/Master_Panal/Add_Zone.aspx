﻿<%@ Page Title="" Language="C#" MasterPageFile="~/English/Master_Panal/Admin_Panel.Master" AutoEventWireup="true" CodeBehind="Add_Zone.aspx.cs" Inherits="Main_Real_estate.English.Master_Panal.Owners_QID.Add_Zone" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
     <div class="container-fluid" id="container-wrapper" style="margin:auto; ">
         <!----------------------------------------------------------------------------------------------------------->
        <div class="d-sm-flex align-items-center justify-content-between mb-4">
            <h1 class="h3 mb-0 text-gray-800">
                <asp:Label ID="lbl_titel_Add_New_Zone" runat="server" Text="إضافة منطقة جديدة"></asp:Label>
                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                <asp:Label ID="lbl_Success_Add_New_Zone" runat="server" ForeColor="#00FF40"></asp:Label></h1>
        </div>
        <!----------------------------------------------------------------------------------------------------------->
        <div class="row">
            <div class="col-lg-10">
                <div class="card mb-10" >
                     <div class="card-body">
                          
                              <div class="form-group">
                               <asp:Label ID="lbl_En_Zone_Name" runat="server" Text="إسم المنطفة بالإنكليزية"></asp:Label>
                                &nbsp;<asp:RegularExpressionValidator ID="En_Zone_Name_Reg_Exp_Vali" runat="server" ControlToValidate="txt_En_Zone_Name" 
                                EnableClientScript="True" ErrorMessage="!!! يُسمح فقط بالأحرف الإنكليزية" ForeColor="Red"
                                ValidationExpression="[a-z A-Z0-9]+"></asp:RegularExpressionValidator>  
                                <asp:TextBox ID="txt_En_Zone_Name" runat="server" CssClass="form-control" ></asp:TextBox>
                                <asp:RequiredFieldValidator ID="En_Zone_Name_reqFuild" ControlToValidate="txt_En_Zone_Name"  
                                runat="server" ErrorMessage="* حقل مطلوب !!!"  ForeColor="Red"></asp:RequiredFieldValidator>
                             </div>

                              <div class="form-group">
                               <asp:Label ID="lbl_Ar_Zone_Name" runat="server" Text="إسم المنطقة بالعربية"></asp:Label>
                              &nbsp;<asp:RegularExpressionValidator ID="Ar_Zone_Name_Reg_Exp_Vali" runat="server" ControlToValidate="txt_Ar_Zone_Name" 
                                EnableClientScript="True" ErrorMessage="!!! يُسمح فقط بالأحرف العربية" ForeColor="Red"
                                ValidationExpression="[ا-ي إ أ آ ى ة ئ ء ؤ 0-9 ]+"></asp:RegularExpressionValidator>
                              <asp:TextBox ID="txt_Ar_Zone_Name" runat="server" CssClass="form-control"></asp:TextBox>
                              <asp:RequiredFieldValidator ID="Ar_Zone_Name_reqFuild" ControlToValidate="txt_Ar_Zone_Name"  
                                runat="server" ErrorMessage="* حقل مطلوب !!!"  ForeColor="Red"></asp:RequiredFieldValidator>
                             </div>

                              <div class="form-group">
                               <asp:Label ID="lbl_Zone_Number" runat="server" Text="رقم المنطقة"></asp:Label>&nbsp;
                                <asp:RegularExpressionValidator ID="Zone_Number_Reg_Exp_Vali" runat="server" ControlToValidate="txt_Zone_Number" 
                                EnableClientScript="True" ErrorMessage="!!! يُسمح فقط بالأرقام" ForeColor="Red"
                                ValidationExpression="[0-9]+"></asp:RegularExpressionValidator>
                                <asp:TextBox ID="txt_Zone_Number" runat="server" CssClass="form-control"></asp:TextBox>
                              <asp:RequiredFieldValidator ID="Zone_Number_reqFuild" ControlToValidate="txt_Zone_Number"
                               runat="server" ErrorMessage="* حقل مطلوب !!!"  ForeColor="Red"></asp:RequiredFieldValidator>
                             </div>

                      </div>
                </div>
            </div>
        </div>
        <br />
        <asp:Button ID="btn_Add_Zone" runat="server" Text="إضافة منطقة" CssClass="btn btn" BackColor="#52a2da" ForeColor="White" Width="248px" OnClick="btn_Add_Zone_Click"    />
            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
            <asp:Button ID="btn_Back_To_Zone_List" runat="server" Text="العودة لقائمة المناطق" ValidationGroup="x" CssClass="btn btn-light mb-1" OnClick="btn_Back_To_Zone_List_Click"/>
     </div>
</asp:Content>
