<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="WebApplication1._Default" %>


<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <div class="jumbotron">
        <h1>ADD & DELETE ROWS</h1>

       
        <asp:Button ID="Add" runat="server" CssClass="btn-info" OnClick="Button1_Click" Text="Add Rows" />
        <asp:Button ID="Delete" runat="server" Text="Delete Rows" OnClick="Delete_Click" BorderColor="#0099FF" />

        <asp:GridView ID="GridView1" DataKeyNames="serial" runat="server" AutoGenerateColumns="False">
            <Columns>
                <asp:TemplateField HeaderText="cb">
                    <ItemTemplate>
                        <asp:CheckBox ID="CheckBox1" runat="server" />
                    </ItemTemplate>
                </asp:TemplateField>

                <asp:BoundField DataField="serial" HeaderText="serial" SortExpression="serial" />
                <asp:TemplateField HeaderText="input">
                    <ItemTemplate>
                        <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>

                    </ItemTemplate>
                </asp:TemplateField>

            </Columns>
        </asp:GridView>

        <asp:Button ID="Save" runat="server" OnClick="Button3_Click" Text="Save" />


        <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:Myconnection %>" SelectCommand="SELECT * FROM [Test]"></asp:SqlDataSource>

        

    </div>




</asp:Content>
