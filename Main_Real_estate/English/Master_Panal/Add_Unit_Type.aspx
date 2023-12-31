﻿<%@ Page Title="" Language="C#" MasterPageFile="~/English/Master_Panal/Admin_Panel.Master" AutoEventWireup="true" CodeBehind="Add_Unit_Type.aspx.cs" Inherits="Main_Real_estate.English.Master_Panal.Add_Unit_Type" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
     <div class="container-fluid" id="container-wrapper" style="margin:auto; ">
         <!----------------------------------------------------------------------------------------------------------->
        <div class="d-sm-flex align-items-center justify-content-between mb-4">
            <h1 class="h3 mb-0 text-gray-800">
                <asp:Label ID="lbl_titel_Add_New_Unit_Type" runat="server" Text="إضافة نوع وحدة جديد"></asp:Label>
                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                <asp:Label ID="lbl_Success_Add_New_Unit_Type" runat="server" ForeColor="#00FF40"></asp:Label></h1>
        </div>
        <!----------------------------------------------------------------------------------------------------------->
        <div class="row">
            <div class="col-lg-10">
                <div class="card mb-10" >
                     <div class="card-body">
                          
                              <div class="form-group">
                               <asp:Label ID="lbl_En_Unit_Type_Name" runat="server" Text="نوع الوحدة بالانكليزية"></asp:Label>
                                <asp:TextBox ID="txt_En_Unit_Type_Name" runat="server" CssClass="form-control" ></asp:TextBox>
                             </div>

                              <div class="form-group">
                               <asp:Label ID="lbl_Ar_Unit_Type_Name" runat="server" Text="نوع الوحدة بالعربية"></asp:Label>
                              <asp:TextBox ID="txt_Ar_Unit_Type_Name" runat="server" CssClass="form-control"></asp:TextBox>
                             </div>
                      </div>
                </div>
            </div>
        </div>
        <br />
        <asp:Button ID="btn_Add_Unit_Type" runat="server" Text="إضافة نوع الوحدة" CssClass="btn btn" BackColor="#52a2da" ForeColor="White" Width="248px" OnClick="btn_Add_Unit_Type_Click"    />
            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
            <asp:Button ID="btn_Back_To_Unit_Type_List" runat="server" Text="العودة لقائمة أنوع الوحدات" ValidationGroup="x" CssClass="btn btn-light mb-1" OnClick="btn_Back_To_Unit_Type_List_Click" />
    </div>
</asp:Content>
