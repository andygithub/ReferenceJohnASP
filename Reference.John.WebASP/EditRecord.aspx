<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="EditRecord.aspx.vb" Inherits="Reference.John.WebASP.EditRecord" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

       <h2>Edit Record</h2>
    <hr />
    <asp:ValidationSummary runat="server" ShowModelStateErrors="true"
        ForeColor="Red" HeaderText="Please check the following errors:" />

    <asp:FormView runat="server" ID="editForm" DefaultMode="Edit" UpdateMethod="UpdateFormItem" SelectMethod="SelectFormItem" ItemType="Reference.John.Domain.FormSimpleZero"
        DataKeyNames="ClientToken,RowVersion">
        <EditItemTemplate>
                <div class="form-group">
                    <label for="firstname"><%# Reference.John.Resources.resources.Names.FirstName %></label>
                    <asp:TextBox runat="server" ID="firstname" ClientIDMode="Static" Text='<%#: BindItem.FirstName %>' CssClass="form-control" placeholder="<%#Reference.John.Resources.resources.Names.FirstName %>" />                
                </div>
                <div class="form-group">
                    <label for="lastName"><%#Reference.John.Resources.resources.Names.LastName %></label>
                    <asp:TextBox runat="server" ID="lastname" ClientIDMode="Static" Text='<%#: BindItem.LastName %>' CssClass="form-control" placeholder="<%#Reference.John.Resources.resources.Names.LastName %>" />                
                </div>
                <div class="form-group">
                    <label for="genderid"><%#Reference.John.Resources.resources.Names.GenderId %></label>
                    <asp:DropDownList runat="server" ID="GenderId" ClientIDMode="Static" cssclass="form-control"  AppendDataBoundItems="true"
                        DataTextField="Name" DataValueField="GenderId" SelectMethod="GetGenderOptionList" ItemType="Reference.John.Domain.GenderOptionList"
                        SelectedValue='<%# Bind("GenderId")  %>' >                
                            <asp:ListItem Text="" Value="" Selected="True" />
                        </asp:DropDownList>
                </div>
                <div class="form-group">
                    <label for="raceid"><%#Reference.John.Resources.resources.Names.RaceId %></label>
                    <asp:DropDownList runat="server" ID="raceid" ClientIDMode="Static" cssclass="form-control" AppendDataBoundItems="true" 
                        DataTextField="Name" DataValueField="RaceId" SelectMethod="GetRaceOptionList" ItemType="Reference.John.Domain.RaceOptionList"
                        SelectedValue='<%# Bind("RaceId")  %>' >                
                            <asp:ListItem Text="" Value="" Selected="True" />
                        </asp:DropDownList>
                </div>
                <div class="form-group">
                    <label for="regionid"><%#Reference.John.Resources.resources.Names.RegionId %></label>
                    <asp:DropDownList runat="server" ID="regionid" ClientIDMode="Static" cssclass="form-control" AppendDataBoundItems="true" 
                        DataTextField="Name" DataValueField="RegionId" SelectMethod="GetRegionOptionList" ItemType="Reference.John.Domain.RegionOptionList"
                        SelectedValue='<%# Bind("RegionId")  %>' >                
                            <asp:ListItem Text="" Value="" Selected="True" />
                        </asp:DropDownList>
                </div>
                <div class="form-group">
                    <label for="ethnicityid"><%#Reference.John.Resources.resources.Names.EthnicityId %></label>
                    <asp:DropDownList runat="server" ID="ethnicityid" ClientIDMode="Static" cssclass="form-control" AppendDataBoundItems="true" 
                        DataTextField="Name" DataValueField="EthnicityId" SelectMethod="GetEthnicityOptionList" ItemType="Reference.John.Domain.EthnicityOptionList"
                        SelectedValue='<%# Bind("EthnicityId")  %>' >              
                            <asp:ListItem Text="" Value="" Selected="True" />
                        </asp:DropDownList>
                </div>
                <asp:Button runat="server" Text="Update" CommandName="Update" cssclass="btn btn-primary" />
                <asp:Button runat="server" Text="Cancel" CausesValidation="false" OnClick="cancelButton_Click" cssclass="btn btn-default" />
        </EditItemTemplate>
    </asp:FormView>

</asp:Content>
