<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="ListRecord.aspx.vb" Inherits="Reference.John.WebASP.ListRecord" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h2>List Records</h2>
    <hr />
    <div class="well">
     <asp:GridView ID="FormsGrid" runat="server" AutoGenerateColumns="false" ItemType="Reference.John.Domain.SearchResult" DataKeyNames="ClientToken" SelectMethod="GetForms"
         AllowPaging="true" AllowSorting="true" PageSize="10" CssClass="table table-striped table-bordered table-condensed">
    <Columns>
      <asp:BoundField DataField="FormID" HeaderText="ID" SortExpression="FormID" />
      <asp:BoundField DataField="FirstName" HeaderText="First Name" SortExpression="FirstName" />
      <asp:BoundField DataField="LastName" HeaderText="LastName" SortExpression="LastName" />
      <asp:BoundField DataField="EthnicityName" HeaderText="Ethnicity" SortExpression="EthnicityName" />
      <asp:BoundField DataField="GenderName" HeaderText="Gender" SortExpression="GenderName" />
        <asp:BoundField DataField="LastChangeDate" HeaderText="Last Change" SortExpression="LastChangeDate" DataFormatString="{0:d}" />
        <asp:BoundField DataField="ClientToken" HeaderText="Client Token" SortExpression="ClientToken" />
    </Columns>
         <EmptyDataTemplate>
                 No categories found.
    </EmptyDataTemplate>
    <SortedAscendingHeaderStyle CssClass="asc" />
    <SortedDescendingHeaderStyle CssClass="desc" />
  </asp:GridView>
        </div>
</asp:Content>
