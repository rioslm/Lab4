<%@ Page Title="" Language="C#" MasterPageFile="~/DataMasterPage.master" AutoEventWireup="true" CodeFile="Bootstrap.aspx.cs" Inherits="Bootstrap" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" Runat="Server">
        <link href="/Content/bootstrap.css" rel="stylesheet" />
    <script src="/Scripts/jquery-3.3.1.slim.min.js"></script>
    <script src="/Scripts/popper.min.js"></script>
    <script src="/Scripts/bootstrap.bundle.min.js"></script>
        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        <asp:Label ID="Label2" runat="server" Text="Select Product to Edit"></asp:Label>
&nbsp;
    &nbsp;

    <asp:DropDownList 
        ID="SalesDropDown"
        runat="server"
        DataSourceID="srcSalesReason"
        DataTextField="SalesReasonID"
        DataValueField="SalesReasonID"
        AppendDataBoundItems="True"
        AutoPostBack="true"
        OnSelectedIndexChange="SalesDropDown_SelectedIndexChanged">
        </asp:DropDownList>

    <asp:SqlDataSource
        ID="srcSalesReason"
        SelectCommand="SELECT SalesReasonID FROM Sales.SalesReason"
        ConnectionString="Server=LEASPC\MSSQLSERVER01;Database=AdventureWorks2014; Trusted_Connection=Yes;"
        runat="server" />
        <br />
        <br />
        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        <asp:Label ID="SaleNameLabel" runat="server" Text="Sale Name"></asp:Label>
        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        <asp:TextBox ID="NameTextbox" runat="server"></asp:TextBox>
        <br />
        <br />
        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        <asp:Label ID="SaleReasonLabel" runat="server" Text="Sale Reason"></asp:Label>
        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        <asp:TextBox ID="SaleReasonTextbox" runat="server"></asp:TextBox>
        <br />
        <br />
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        <asp:Button ID="CommitButton" runat="server" Text="Commit Into Database" OnClick="CommitButton_Click" />
        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        <asp:Button ID="SaveButton" runat="server" Text="Save Edited Info" OnClick="SaveButton_Click"/>
&nbsp;<br />
        <br />
&nbsp;<br />
        <br />
        <asp:Label ID="UpdatedLabel" runat="server" Text="" />
        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        <br />
        <br />
</asp:Content>