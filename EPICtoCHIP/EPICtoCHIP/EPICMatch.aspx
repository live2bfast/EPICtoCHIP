<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="EPICMatch.aspx.cs" Inherits="EPICMatch" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
    
                <asp:GridView ID="GridView1" runat="server"  OnRowUpdating="GridView1_RowUpdating" DataSourceID="InputDS" DataKeyNames="Id" AutoGenerateEditButton="true" AutoGenerateColumns="False">
        <Columns>
            <asp:BoundField DataField="Id" HeaderText="Id" InsertVisible="False" Visible="false" ReadOnly="True" SortExpression="Id" />
            <asp:BoundField DataField="ImmHistType" HeaderText="ImmHistType" ReadOnly="true" SortExpression="ImmHistType" />
            <asp:BoundField DataField="Id1" HeaderText="Id1" InsertVisible="False" Visible="false" ReadOnly="True" SortExpression="Id1" />
            
            <asp:TemplateField HeaderText="ImmType" SortExpression="ImmType">
                <ItemTemplate>
                    <asp:Label runat="server" ID="ImmTypeLbl" Text='<%# Eval("ImmType") %>'/>
                </ItemTemplate>
                <EditItemTemplate>                
                <asp:DropDownList runat="server" ID="ImmType" DataSourceID="ImmTypeODS" DataTextField="ImmType" DataValueField="ImmType" AppendDataBoundItems="true">
                    <asp:ListItem Text="Please Select"></asp:ListItem>
                </asp:DropDownList>
                                                               </EditItemTemplate></asp:TemplateField>
             <asp:TemplateField HeaderText="ImmType2" SortExpression="ImmType2">
                <ItemTemplate>
                    <asp:Label runat="server" ID="ImmTypeLbl2" Text='<%# Eval("ImmType") %>'/>
                </ItemTemplate>
                <EditItemTemplate>                
                <asp:DropDownList runat="server" ID="ImmType2" DataSourceID="ImmTypeODS" DataTextField="ImmType" DataValueField="ImmType" AppendDataBoundItems="true">
                    <asp:ListItem Text="Please Select"></asp:ListItem>
                </asp:DropDownList>
                                                               </EditItemTemplate></asp:TemplateField>
             <asp:TemplateField HeaderText="ImmType3" SortExpression="ImmType3">
                <ItemTemplate>
                    <asp:Label runat="server" ID="ImmTypeLbl3" Text='<%# Eval("ImmType") %>'/>
                </ItemTemplate>
                <EditItemTemplate>                
                <asp:DropDownList runat="server" ID="ImmType3" DataSourceID="ImmTypeODS" DataTextField="ImmType" DataValueField="ImmType" AppendDataBoundItems="true">
                    <asp:ListItem Text="Please Select"></asp:ListItem>
                </asp:DropDownList>
                                                               </EditItemTemplate></asp:TemplateField>
        </Columns>
    </asp:GridView>
    
          <br />
                <asp:Button ID="nxtPage" runat="server" Text="Next" OnClick="nxtPage_Click" />
            
    
    
    
   
    <asp:ObjectDataSource ID="InputDS" runat="server" UpdateMethod="UpdateImmHistType"  SelectMethod="GetNoImmMatches" InsertMethod="InsertEpicData" TypeName="EPICtoCHIP.App_Code.Datalayer" OldValuesParameterFormatString="{0}" >
        <InsertParameters>
            <asp:Parameter Name="FilePath" Type="String"></asp:Parameter>
        </InsertParameters>
        <UpdateParameters>
            <asp:Parameter Name="Id" Type="Int16"></asp:Parameter>
            <asp:Parameter Name="ImmType" Type="String"></asp:Parameter>
            <asp:Parameter Name="ImmType2" Type="String"></asp:Parameter>
            <asp:Parameter Name="ImmType3" Type="String"></asp:Parameter>
        </UpdateParameters>
    </asp:ObjectDataSource>

 <asp:ObjectDataSource ID="ImmTypeODS" runat="server" TypeName="EPICtoCHIP.App_Code.Datalayer" SelectMethod="GetImmType"></asp:ObjectDataSource>
</asp:Content>


