<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ListSailor.ascx.cs" Inherits="JKLSite.Views.JKL.ListSailor" %>

<div>
<form id="test" runat="server">
<asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <div class="row">
        <asp:Label ID="_errorMessage" runat="server"></asp:Label>
        
        <asp:DataGrid ID="SailorGrid" runat="server"
              EnableViewState="true"
              AutoGenerateColumns="False"
            CssClass="form-group col-lg-12"

            AllowCustomPaging="true"  AllowSorting="True" AllowPaging="True" BorderStyle="Dashed" PageSize="10">
            <HeaderStyle BackColor="Black" />
            <PagerStyle NextPageText="Next" PrevPageText="Prev" VerticalAlign="Middle" />
            <SelectedItemStyle BackColor="#663300" />

             <Columns>
        <asp:BoundColumn DataField="SailorId" HeaderText="ID" HeaderStyle-HorizontalAlign="Center">
            <ItemStyle HorizontalAlign="center"></ItemStyle>
        </asp:BoundColumn> 

        <asp:BoundColumn DataField="SailorName" HeaderText="Далайчны нэр" HeaderStyle-HorizontalAlign="Center">
            <ItemStyle HorizontalAlign="center"></ItemStyle>
        </asp:BoundColumn> 
        <asp:BoundColumn DataField="DateOfBirth" HeaderText="Төрсөн огноо" DataFormatString="{0:d}" HeaderStyle-HorizontalAlign="Center">
            <ItemStyle HorizontalAlign="center"></ItemStyle>
        </asp:BoundColumn>
        <asp:BoundColumn DataField="MaritialStatus" HeaderText="Гэр бүлийн байдал" HeaderStyle-HorizontalAlign="Center">
            <ItemStyle HorizontalAlign="center"></ItemStyle>
        </asp:BoundColumn> 

        <asp:BoundColumn DataField="Address" HeaderText="Хаяг" HeaderStyle-HorizontalAlign="Center">
            <ItemStyle HorizontalAlign="center"></ItemStyle>
        </asp:BoundColumn> 
        <asp:BoundColumn DataField="Height" HeaderText="Өндөр" HeaderStyle-HorizontalAlign="Center">
            <ItemStyle HorizontalAlign="center"></ItemStyle>
        </asp:BoundColumn> 
        <asp:BoundColumn DataField="Weight" HeaderText="Жин" HeaderStyle-HorizontalAlign="Center">
            <ItemStyle HorizontalAlign="center"></ItemStyle>
        </asp:BoundColumn> 
        <asp:BoundColumn DataField="BloodType" HeaderText="Цусны төрөл" HeaderStyle-HorizontalAlign="Center">
            <ItemStyle HorizontalAlign="center"></ItemStyle>
        </asp:BoundColumn>   
        <asp:BoundColumn DataField="ShoeSize" HeaderText="Гуталны размер" HeaderStyle-HorizontalAlign="Center">
            <ItemStyle HorizontalAlign="center"></ItemStyle>
        </asp:BoundColumn> 
        <asp:BoundColumn DataField="JobStatus" HeaderText="Ажлын төлөв" HeaderStyle-HorizontalAlign="Center">
            <ItemStyle HorizontalAlign="center"></ItemStyle>
        </asp:BoundColumn> 
    
    </Columns>
              </asp:DataGrid>

    </div>
</form>
</div>