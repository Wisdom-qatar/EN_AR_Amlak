﻿<%@ Page Title="" Language="C#" MasterPageFile="~/English/Master_Panal/Admin_Panel.Master" AutoEventWireup="true" CodeBehind="Inventory_List.aspx.cs" Inherits="Main_Real_estate.English.Master_Panal.Inventory_List" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>
    <script type="text/javascript" src="https://cdn.datatables.net/1.13.4/js/jquery.dataTables.js"></script>
    <script src="../JS/DataTableScript.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">


    <link href="../CSS/DataTableCss.css" rel="stylesheet" />
     <!-- Container Fluid-->
    <div class="container-fluid" id="container-wrapper">
        <div class="row">
            <div class="col-lg-4 mb-3" style="">
                <h1 class="h3 mb-0 text-gray-800">
                    <asp:Label ID="lbl_titel" runat="server" Text="قائمة المخازن"></asp:Label>
                </h1>
            </div>
            <div class="col-lg-4">
                <button style="background-color:#52a2da; color:white; border-style:none; height:40px; border-radius:7px;" runat="server" onserverclick="GoToAdd"><i class="fa fa-plus-circle" aria-hidden="true"></i> إضافة </button>
            </div>
        </div>

        <div class="row">
            <div class="col-lg-12 mb-4">
                <!-- Simple Tables -->
                <div class="card">
                    <div class="table-responsive" style="border-radius: 10px;" id="Grid">
                        <asp:Repeater ID="The_Table" runat="server" ClientIDMode="Static">
                        <HeaderTemplate>
                            <table  cellspacing="0" style="width: 100%; font-size:11px" id="Table" class="datatable table table-striped table-bordered">
                                <thead>
                                    <th>اسم المخزن</th>
                                    <th>Inventory Name </th>
                                    <th>مكان المخزن</th>
                                    <th>أمين المخزن</th>
                                    <th></th>
                                </thead>
                            <tbody>
                        </HeaderTemplate>
                        <ItemTemplate>
                            <tr>
                                <td> <asp:Label ID="lbl_Inventory_arabic_name" runat="server" Text='<%# Eval("Inventory_arabic_name") %>' /></td>
                                <td> <asp:Label ID="lbl_Inventory_English_name" runat="server" Text='<%# Eval("Inventory_English_name") %>' /></td>
                                <td> <asp:Label ID="lbl_Inventory_Location" runat="server" Text='<%# Eval("Inventory_Location") %>' /></td>
                                <td> <asp:Label ID="lbl_Employee_Arabic_name" runat="server" Text='<%# Eval("Employee_Arabic_name") %>' /></td>
                                <td>
                                    <asp:LinkButton  runat="server" CommandArgument='<%# Eval("Inventory_Id") %>' OnClientClick="return confirm('هل أنت متأكد أنك تريد حذف؟');" OnClick="Delete" ><i class="fa fa-trash" style="font-size:18px;"></i></asp:LinkButton>
                                    <asp:LinkButton  runat="server" CommandArgument='<%# Eval("Inventory_Id") %>' OnClick="Edit"> <i class="fa fa-pencil" style="font-size:18px;"></i> </asp:LinkButton>
                                </td>
                            </tr>
                        </ItemTemplate>
                        <FooterTemplate>
                            </tbody>
                            </table>
                        </FooterTemplate>
                    </asp:Repeater>

                    </div>
                </div>
            </div>
        </div>
    </div>
    <!---Container Fluid-->
</asp:Content>
