<%@ Page Title="Settings" Language="C#" MasterPageFile="~/Site2.Master" AutoEventWireup="true" CodeFile="Settings.aspx.cs" Inherits="Account_Data_Entry" %>

<asp:Content runat="server" ID="BodyContent" ContentPlaceHolderID="MainContent">
    <h2> Settings</h2>
    <p class="text-danger">
        <asp:Literal runat="server" ID="ErrorMessage" />
    </p>

    <div class="form-horizontal">
        <h4>Set your limits here...</h4>
        <hr />
        <asp:ValidationSummary runat="server" CssClass="text-danger" />
          
        <div class="form-horizontal">
            <h3>Nutrition Details</h3> 
            <hr />
        </div>

        <div class="form-group">
            <asp:Label runat="server" AssociatedControlID="Caffeine_Max" CssClass="col-md-2 control-label">Caffeine - Max</asp:Label>
            <div class="col-md-10">
                
                <asp:TextBox runat="server" ID="Caffeine_Max" CssClass="form-control" />
                <asp:RequiredFieldValidator runat="server" ControlToValidate="Caffeine_Max"
                    CssClass="text-danger" ErrorMessage="The caffeine max field is required." />
            </div>
        </div>
              
        <div class="form-group">
            <asp:Label runat="server" AssociatedControlID="Sugar_Max" CssClass="col-md-2 control-label">Sugar - Max</asp:Label>
            <div class="col-md-10">
                <asp:TextBox runat="server" ID="Sugar_Max"  CssClass="form-control" />
                <asp:RequiredFieldValidator runat="server" ControlToValidate="Sugar_Max"
                    CssClass="text-danger" Display="Dynamic" ErrorMessage="The Sugar max field is required." />
                </div>
        </div>
       
        <div class="form-horizontal">
            <h3>Body Vitals Details</h3> 
            <hr />
        </div>

        <div class="form-group">
            <asp:Label runat="server" AssociatedControlID="BP_Max" CssClass="col-md-2 control-label">BP - Max</asp:Label>
            <div class="col-md-10">
                <asp:TextBox runat="server" ID="BP_Max" CssClass="form-control" />
                
                <asp:RequiredFieldValidator runat="server" ControlToValidate="BP_Max"
                    CssClass="text-danger" ErrorMessage="The BP - Max field is required." />
               
            </div>
            <asp:Label runat="server" AssociatedControlID="BP_Min" CssClass="col-md-2 control-label">BP - Min</asp:Label>
       <div class="col-md-10">
                <asp:TextBox runat="server" ID="BP_Min" CssClass="form-control" /> 
                <asp:RequiredFieldValidator runat="server" ControlToValidate="BP_Min"
                    CssClass="text-danger" ErrorMessage="The BP - Min field is required." />
            </div>
             </div>
        <div class="form-group">
            <asp:Label runat="server" AssociatedControlID="Heart_Rate_Max" CssClass="col-md-2 control-label">Heart Rate - Max</asp:Label>
            <div class="col-md-10">
                <asp:TextBox runat="server" ID="Heart_Rate_Max" CssClass="form-control" />
                <asp:RequiredFieldValidator runat="server" ControlToValidate="Heart_Rate_Max"
                    CssClass="text-danger" ErrorMessage="The Heart Rate Max field is required." />
            </div>
            <asp:Label runat="server" AssociatedControlID="Heart_Rate_Min" CssClass="col-md-2 control-label">Heart Rate - Min</asp:Label>
            <div class="col-md-10">
                <asp:TextBox runat="server" ID="Heart_Rate_Min" CssClass="form-control" />
                <asp:RequiredFieldValidator runat="server" ControlToValidate="Heart_Rate_Min"
                    CssClass="text-danger" ErrorMessage="The Heart Rate Min field is required." />
            </div>
        </div>

        
            <hr />
        
        


        <div class="form-group">
            <div class="col-md-offset-2 col-md-10">
                <asp:Button runat="server" OnClick="Set_Data" Text="Set Limits" CssClass="btn btn-default" />
            </div>
        </div>
    </div>
</asp:Content>

