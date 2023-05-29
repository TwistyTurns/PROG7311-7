<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="FarmerPage.aspx.cs" Inherits="PROG7311_7.FarmerPage" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <form>
        <div class="form-group">
            <label for="txtProdName">Product Name</label>
            <input type="text" class="form-control" id="txtProdName" placeholder="Daniels Potatos" runat="server">
        </div>
        
        <div class="form-group">
            <label for="selectProd">Product Type</label>
            <select class="form-control" id="selectProd" runat="server">
                <option>Potato</option>
                <option>Onion</option>
                <option>Carrot</option>
                <option>Grapes</option>
                <option>Lettuce</option>
            </select>
        </div>

        <div class="form-group">
            <label for="txtProdQuant">Product Quantity</label>
            <input type="text" class="form-control" id="txtProdQuant" placeholder="100" runat="server">
        </div>

        <div class="form-group">
            <label for="txtProdPrice">Product Price Per Unit (R)</label>
            <input type="text" class="form-control" id="txtProdPrice" placeholder="350" runat="server">
        </div>
    </form>

    <button id="btnSubmitProd" OnClick="btnSubmitProd_Click" type="submit" class="btn btn-primary" runat="server">Submit</button>

</asp:Content>
