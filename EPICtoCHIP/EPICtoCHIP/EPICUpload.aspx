<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="EPICUpload.aspx.cs" Inherits="EPICUpload" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
  
                 <asp:Label runat="server" ID="EPICUpLbl" Text="Upload EPIC File:"></asp:Label>
          
                    <ajaxToolkit:AjaxFileUpload ID="EPICFileUpload" runat="server"  OnUploadComplete="EPICUpload_UploadComplete" />
         <br />
   <br />
    <asp:Label ID="errorLbl" runat="server" Text="Please choose a file." ForeColor="Red" Visible="false"></asp:Label>
                <asp:Button ID="nxtPage" runat="server" Text="Next" OnClick="nxtPage_Click" />
</asp:Content>
