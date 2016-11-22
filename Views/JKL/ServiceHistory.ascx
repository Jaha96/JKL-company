<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ServiceHistory.ascx.cs" Inherits="JKLSite.Views.JKL.ServiceHistory" %>

<div>
<form id="test" runat="server">
<asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <div class="row">
        <asp:Label ID="_errorMessage" runat="server"></asp:Label>
        <asp:DataGrid id="HistoryGrid" runat="server"
              EnableViewState="true"
              AutoGenerateColumns="False"
            CssClass="form-group col-lg-12"

            AllowCustomPaging="True" AllowSorting="True" AllowPaging="True" BorderStyle="Dashed" PageSize="10">
            <HeaderStyle BackColor="Black" />
            <PagerStyle NextPageText="Next" PrevPageText="Prev" VerticalAlign="Middle" />
            <SelectedItemStyle BackColor="#663300" />

             <Columns>
        <asp:BoundColumn DataField="SailorName" HeaderText="Далайчны нэр" HeaderStyle-HorizontalAlign="Center">
            <ItemStyle HorizontalAlign="center"></ItemStyle>
        </asp:BoundColumn> 

        <asp:BoundColumn DataField="Flag" HeaderText="Хаана" HeaderStyle-HorizontalAlign="Center">
            <ItemStyle HorizontalAlign="center"></ItemStyle>
        </asp:BoundColumn> 

        <asp:BoundColumn DataField="SignOnDate" HeaderText="Зарагдсан огноо" DataFormatString="{0:d}" HeaderStyle-HorizontalAlign="Center">
            <ItemStyle HorizontalAlign="center"></ItemStyle>
        </asp:BoundColumn>   
        <asp:BoundColumn DataField="ContractPerion" HeaderText="Ажиллах сар" HeaderStyle-HorizontalAlign="Center">
            <ItemStyle HorizontalAlign="center"></ItemStyle>
        </asp:BoundColumn> 

        <asp:BoundColumn DataField="CompanyName" HeaderText="Компани" HeaderStyle-HorizontalAlign="Center">
            <ItemStyle HorizontalAlign="center"></ItemStyle>
        </asp:BoundColumn> 

        <asp:BoundColumn DataField="DescMon" HeaderText="Зэрэглэл" HeaderStyle-HorizontalAlign="Center">
            <ItemStyle HorizontalAlign="center"></ItemStyle>
        </asp:BoundColumn> 
        <asp:BoundColumn DataField="ContractEndDate" HeaderText="Дуусах хугацаа" DataFormatString="{0:d}" HeaderStyle-HorizontalAlign="Center">
            <ItemStyle HorizontalAlign="center"></ItemStyle>
        </asp:BoundColumn>
    
    </Columns>
              </asp:DataGrid>
    </div>
</form>
</div>