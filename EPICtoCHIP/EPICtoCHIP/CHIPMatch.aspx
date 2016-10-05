<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="CHIPMatch.aspx.cs" Inherits="CHIPMatch" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
      <asp:GridView ID="GridView1"  runat="server"  AutoGenerateColumns="False" OnRowUpdated="GridView1_RowUpdated"  OnRowUpdating="GridView1_RowUpdating" DataSourceID="InputDS" AutoGenerateEditButton="true" DataKeyNames="Id">
        <Columns>
            <asp:BoundField DataField="Id" HeaderText="EPICId" ReadOnly="True"  Visible="false" InsertVisible="False" SortExpression="Id"></asp:BoundField>
                        <asp:BoundField DataField="ChipId" HeaderText="CHIPId" ReadOnly="True" Visible="false" InsertVisible="False" SortExpression="Id"></asp:BoundField>

            <asp:BoundField DataField="Name" HeaderText="Name" ReadOnly="True" SortExpression="Name"></asp:BoundField>
            <asp:BoundField DataField="Match" HeaderText="Match" ReadOnly="True" SortExpression="Match"></asp:BoundField>
            <asp:BoundField DataField="DOB" HeaderText="DOB" ReadOnly="True" SortExpression="DOB"></asp:BoundField>
            <asp:BoundField DataField="MPI" HeaderText="MPI/MRN" ReadOnly="True" SortExpression="MPI"></asp:BoundField>
              <asp:TemplateField HeaderText="CHIP Patient" SortExpression="ImmType">
                <ItemTemplate>
                    <asp:Label runat="server" ID="chipPt" Text='<%# Eval("CHIPName") %>'/>
                </ItemTemplate>
                <EditItemTemplate>                
                <asp:DropDownList runat="server" ID="chipCB"  DataSourceID="CHIPDataODS"  DataTextField="ChipInfo" DataValueField="Id" ></asp:DropDownList>
                                                               </EditItemTemplate></asp:TemplateField>
        </Columns>
    </asp:GridView>
        
    <br />
    <br />
    <asp:Button runat="server" ID="genFile" Text="Generate File" OnClick="genFile_Click" />
     <br />
    <br />
    <asp:Button runat="server" ID="backBtn" Text="Back" OnClick="backBtn_Click"/>
    <asp:ObjectDataSource ID="InputDS" runat="server" SelectMethod="GetNoPatientMatches" InsertMethod="InsertChipData" TypeName="EPICtoCHIP.App_Code.Datalayer"  UpdateMethod="InsertPatientMatch">
        <InsertParameters>
            <asp:Parameter Name="FilePath" Type="String"></asp:Parameter>
        </InsertParameters>
        <UpdateParameters>
            <asp:Parameter Name="EpicId" Type="Int32"></asp:Parameter>
            <asp:Parameter Name="ChipId" Type="Int32"></asp:Parameter>
        </UpdateParameters>
    </asp:ObjectDataSource>
     <asp:ObjectDataSource ID="CHIPDataODS" runat="server" TypeName="EPICtoCHIP.App_Code.Datalayer" SelectMethod="GetChipInput"></asp:ObjectDataSource>
</asp:Content>

