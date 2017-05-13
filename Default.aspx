<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="_Default" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <div class="jumbotron">
        <h1>iAssist Web System</h1>
        <p class="lead">&nbsp;This is a makeshift User Interface (for demo purposes)&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:Image ID="azure" ImageUrl="C:\Users\Manoj\Desktop\azure.jpg" runat="server" /></p>
        <p><a href="/About" class="btn btn-primary btn-lg">Learn More &raquo;</a></p>
    </div>

    <div class="row">
        <div class="col-md-4">
            <h2>Getting started</h2>
            <p>
                If you do not have an account, please register yourself prior to using iAssist!
            </p>
            <p>
                <a class="btn btn-default" href="/Account/Register">Register now &raquo;</a>
            </p>
        </div>
        <div class="col-md-4">
            <h2>View Your Health Status</h2>
            <p>
                Please log in to view your health reports!
            </p>
            <p>
                <a class="btn btn-default" href="/Account/Login">Login now &raquo;</a>
            </p>
        </div>
        <div class="col-md-4">
            <h2>Have Questions?</h2>
            <p>
               Contact me directly using the link below!
            </p>
            <p>
                <a class="btn btn-default" href="/Contact">Contact Me &raquo;</a>
            </p>
        </div>
    </div>
</asp:Content>
