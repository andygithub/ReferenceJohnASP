<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="ListRecord.aspx.vb" Inherits="Reference.John.WebASP.ListRecord" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
     <asp:GridView ID="FormsGrid" runat="server" AutoGenerateColumns="false" ItemType="Reference.John.Domain.SearchResult" DataKeyNames="FormID" SelectMethod="GetForms">
    <Columns>
      <asp:BoundField DataField="FormID" HeaderText="ID" SortExpression="FormID" />
      <asp:BoundField DataField="FirstName" HeaderText="First Name" SortExpression="FirstName" />
      <asp:BoundField DataField="LastName" HeaderText="LastName" SortExpression="LastName" />
      <asp:BoundField DataField="EthnicityName" HeaderText="Ethnicity" SortExpression="EthnicityName" />
      <asp:BoundField DataField="GenderName" HeaderText="Gender" SortExpression="GenderName" />
        <asp:BoundField DataField="LastChangeDate" HeaderText="Last Change" SortExpression="LastChangeDate" />
    </Columns>
  </asp:GridView>
</asp:Content>
