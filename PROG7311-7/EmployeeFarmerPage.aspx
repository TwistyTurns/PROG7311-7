<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="EmployeeFarmerPage.aspx.cs" Inherits="PROG7311_7.EmployeeFarmerPage" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <form>
        <div class="form-group">
            <label for="txtFarmerName">Farmer Name</label>
            <input type="text" class="form-control" id="txtFarmerName" placeholder="Daniels Potatos" runat="server">
        </div>

        <div class="form-group">
            <label for="txtPassword">Password</label>
            <input type="password" class="form-control" id="txtPassword" placeholder="100" runat="server">
        </div>

    </form>

    <button id="btnSubmitProd" OnClick="btnSubmitProd_Click" type="submit" class="btn btn-primary" runat="server">Register</button>
</asp:Content>
