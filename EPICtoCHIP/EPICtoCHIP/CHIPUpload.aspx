<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="CHIPUpload.aspx.cs" Inherits="CHIPUpload" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
        <asp:Label runat="server" ID="CHIPUpLbl" Text="Upload CHIP File:"></asp:Label>
    <br />
    <ajaxToolkit:AjaxFileUpload ID="CHIPFileUpload" runat="server" OnUploadComplete="CHIPUpload_UploadComplete" />
    <br />
        <asp:Label ID="errorLbl" runat="server" Text="Please choose a file." ForeColor="Red" Visible="false"></asp:Label>

    <br />
   <br />
    <asp:Button runat="server" ID="nextPage" Text="Next" OnClick="nextPage_Click" />
     <br />
    <br />
    <asp:Button runat="server" ID="backBtn" Text="Back" OnClick="backBtn_Click"/>
  


</asp:Content>

