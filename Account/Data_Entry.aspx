<%@ Page Title="Data_Entry" Language="C#" MasterPageFile="~/Site2.Master" AutoEventWireup="true" CodeFile="Data_Entry.aspx.cs" Inherits="Account_Data_Entry" %>

<asp:Content runat="server" ID="BodyContent" ContentPlaceHolderID="MainContent">
    <h2> Data Entry (Automatic from App)</h2>
    <p class="text-danger">
        &nbsp;</p>

    <div class="form-horizontal">
        <h4>
            <asp:Label ID="Top_Label" runat="server" Text="Hi "></asp:Label>Fill all details below ..</h4>
        <hr />
          <hr />
          <div class="form-group">
            <div class="col-md-offset-2 col-md-10">
                <asp:Button runat="server" OnClick="Fill_Data" Text="Get Data" CssClass="btn btn-default" />
            </div>
        </div>
        <hr />
        <div class="form-horizontal">
            <h3>Nutrition Details</h3> 
            <asp:Label ID="Report_Nutrition" runat="server" Text=""></asp:Label>
            <hr />
        </div>

        <div class="form-group">
            <asp:Label runat="server" AssociatedControlID="Caffeine" CssClass="col-md-2 control-label">Caffeine</asp:Label>
            <div class="col-md-10">
                
                <asp:TextBox runat="server" ID="Caffeine" CssClass="form-control" />
                <asp:Label ID="Report_Caffeine" runat="server" Text=""></asp:Label>
            </div>
        </div>
       
        <div class="form-group">
            <asp:Label runat="server" AssociatedControlID="Calcium" CssClass="col-md-2 control-label">Calcium</asp:Label>
            <div class="col-md-10">
                <asp:TextBox runat="server" ID="Calcium"  CssClass="form-control" />
                <asp:Label ID="Report_Calcium" runat="server" Text=""></asp:Label>
                </div>
        </div>
        <div class="form-group">
            <asp:Label runat="server" AssociatedControlID="Carbohydrates" CssClass="col-md-2 control-label">Carbohydrates</asp:Label>
            <div class="col-md-10">
                <asp:TextBox runat="server" ID="Carbohydrates"  CssClass="form-control" />
                <asp:Label ID="Report_Carbohydrates" runat="server" Text=""></asp:Label>
                </div>
        </div>
        <div class="form-group">
            <asp:Label runat="server" AssociatedControlID="Fibre" CssClass="col-md-2 control-label">Fibre</asp:Label>
            <div class="col-md-10">
                <asp:TextBox runat="server" ID="Fibre"  CssClass="form-control" />
                <asp:Label ID="Report_Fibre" runat="server" Text=""></asp:Label>
                </div>
        </div>
        <div class="form-group">
            <asp:Label runat="server" AssociatedControlID="Iodine" CssClass="col-md-2 control-label">Iodine</asp:Label>
            <div class="col-md-10">
                <asp:TextBox runat="server" ID="Iodine"  CssClass="form-control" />
                <asp:Label ID="Report_Iodine" runat="server" Text=""></asp:Label>
                </div>
        </div>
        <div class="form-group">
            <asp:Label runat="server" AssociatedControlID="Sugar" CssClass="col-md-2 control-label">Sugar</asp:Label>
            <div class="col-md-10">
                <asp:TextBox runat="server" ID="Sugar"  CssClass="form-control" />
                <asp:Label ID="Report_Sugar" runat="server" Text=""></asp:Label>
                </div>
        </div>
        <div class="form-group">
            <asp:Label runat="server" AssociatedControlID="Total_Fat" CssClass="col-md-2 control-label">Total Fat</asp:Label>
            <div class="col-md-10">
                <asp:TextBox runat="server" ID="Total_Fat"  CssClass="form-control" />
                <asp:Label ID="Report_Total_Fat" runat="server" Text=""></asp:Label>
                </div>
        </div>
     

        <div class="form-horizontal">
            <h3>Exercise Details</h3> 
            <asp:Label ID="Report_Exercise" runat="server" Text=""></asp:Label>
            <hr />
        </div>

        <div class="form-group">
            <asp:Label runat="server" AssociatedControlID="Active_Calories" CssClass="col-md-2 control-label">Active Calories</asp:Label>
            <div class="col-md-10">
                <asp:TextBox runat="server" ID="Active_Calories" CssClass="form-control" />
                <asp:Label ID="Report_Active_Calories" runat="server" Text=""></asp:Label>
            </div>
        </div>
        <div class="form-group">
            <asp:Label runat="server" AssociatedControlID="Cycling_Distance" CssClass="col-md-2 control-label">Cycling Distance</asp:Label>
            <div class="col-md-10">
                <asp:TextBox runat="server" ID="Cycling_Distance" CssClass="form-control" />
                <asp:Label ID="Report_Cycling_Distance" runat="server" Text=""></asp:Label>
            </div>
        </div>
         <div class="form-group">
            <asp:Label runat="server" AssociatedControlID="Flights_Climbed" CssClass="col-md-2 control-label">Flights Climbed</asp:Label>
            <div class="col-md-10">
                <asp:TextBox runat="server" ID="Flights_Climbed" CssClass="form-control" />
                <asp:Label ID="Report_Flights_Climbed" runat="server" Text=""></asp:Label>
            </div>
        </div>
         <div class="form-group">
            <asp:Label runat="server" AssociatedControlID="Resting_Calories" CssClass="col-md-2 control-label">Resting Calories</asp:Label>
            <div class="col-md-10">
                <asp:TextBox runat="server" ID="Resting_Calories" CssClass="form-control" />
                <asp:Label ID="Report_Resting_Calories" runat="server" Text=""></asp:Label>
            </div>
        </div>
         <div class="form-group">
            <asp:Label runat="server" AssociatedControlID="Steps" CssClass="col-md-2 control-label">Steps</asp:Label>
            <div class="col-md-10">
                <asp:TextBox runat="server" ID="Steps" CssClass="form-control" />
                <asp:Label ID="Report_steps" runat="server" Text=""></asp:Label>
            </div>
        </div>
        <div class="form-group">
            <asp:Label runat="server" AssociatedControlID="Walk_Run_Distance" CssClass="col-md-2 control-label">Walking & Running Distance</asp:Label>
            <div class="col-md-10">
                <asp:TextBox runat="server" ID="Walk_Run_Distance" CssClass="form-control" />
                <asp:Label ID="Report_Walk_Run_Distance" runat="server" Text=""></asp:Label>
            </div>
        </div>

        <div class="form-horizontal">
            <h3>Other Details</h3> 
            <asp:Label ID="Report_Body" runat="server" Text=""></asp:Label>
            <hr />
        </div>

        <div class="form-group">
            <asp:Label runat="server" AssociatedControlID="BMI" CssClass="col-md-2 control-label">BMI</asp:Label>
            <div class="col-md-10">
                <asp:TextBox runat="server" ID="BMI" CssClass="form-control" />
                <asp:Label ID="Report_BMI" runat="server" Text=""></asp:Label>
            </div>
        </div>
        <div class="form-group">
            <asp:Label runat="server" AssociatedControlID="Fat_Percent" CssClass="col-md-2 control-label">Fat Percent</asp:Label>
            <div class="col-md-10">
                <asp:TextBox runat="server" ID="Fat_Percent" CssClass="form-control" />
                <asp:Label ID="Report_Fat_Percent" runat="server" Text=""></asp:Label>
            </div>
        </div>


        <div class="form-group">
            <div class="col-md-offset-2 col-md-10">
                <asp:Button runat="server" OnClick="Get_Data" Text="Generate Report" CssClass="btn btn-default" />
            </div>
        </div>
         <div class="form-group">
             <asp:Label ID="Health_Percent_Display" runat="server" Text=""></asp:Label>
            </div>
    </div>
</asp:Content>

