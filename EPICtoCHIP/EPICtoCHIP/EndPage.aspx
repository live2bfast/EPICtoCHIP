<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="EndPage.aspx.cs" Inherits="EndPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
    <h2>File Generation Successful!</h2>
    <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl="file:///C:\ConversionFiles\TestOutput.csv">Click here to view file.</asp:HyperLink>
</asp:Content>

