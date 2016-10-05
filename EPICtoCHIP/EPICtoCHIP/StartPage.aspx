<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="StartPage.aspx.cs" Inherits="StartPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
    <h2>Welcome to the EPIC -> CHIP Conversion Tool.</h2>
    <br />
    <h4>Please click the button to get started.</h4>
    <br />
     <asp:Button ID="nxtPage" runat="server" Text="Next" OnClick="nxtPage_Click" />
</asp:Content>

