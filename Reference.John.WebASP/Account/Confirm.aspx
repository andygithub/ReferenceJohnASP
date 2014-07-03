<%@ Page Title="Account Confirmation" Language="vb" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Confirm.aspx.vb" Inherits="Reference.John.WebASP.Confirm" Async="true" %>

<asp:Content runat="server" ID="BodyContent" ContentPlaceHolderID="MainContent">
    <h2><%: Title %>.</h2>

    <div>
        <asp:PlaceHolder runat="server" ID="status" ViewStateMode="Disabled">
            <p><%: StatusMessage %></p>
        </asp:PlaceHolder>
    </div>
</asp:Content>
