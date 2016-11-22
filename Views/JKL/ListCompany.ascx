<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ListCompany.ascx.cs" Inherits="JKLSite.Views.JKL.ListCompany" %>
<div>
<form id="test" runat="server">
<asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <div class="row">
        <asp:Label ID="_errorMessage" runat="server"></asp:Label>
        
        <asp:DataGrid ID="CompanyGrid" runat="server" AutoGenerateColumns="False"
            CssClass="form-group col-lg-12"
            AllowCustomPaging="True"  AllowSorting="True" OnDeleteCommand="CompanyGrid_DeleteCommand1" AllowPaging="True" BorderStyle="Dashed" ShowFooter="True">
            <HeaderStyle BackColor="Black" />
            <PagerStyle NextPageText="Next" PrevPageText="Prev" VerticalAlign="Middle" />
            <SelectedItemStyle BackColor="#663300" />

             <Columns>
        <asp:BoundColumn DataField="CompanyId" HeaderText="ID" HeaderStyle-HorizontalAlign="Center">
<HeaderStyle HorizontalAlign="Center"></HeaderStyle>

            <ItemStyle HorizontalAlign="center"></ItemStyle>
        </asp:BoundColumn> 

        <asp:BoundColumn DataField="CompanyName" HeaderText="Компаны нэр" HeaderStyle-HorizontalAlign="Center">
<HeaderStyle HorizontalAlign="Center"></HeaderStyle>

            <ItemStyle HorizontalAlign="center"></ItemStyle>
        </asp:BoundColumn> 
        <asp:BoundColumn DataField="ContactPerson" HeaderText="Холбогдох хүн" HeaderStyle-HorizontalAlign="Center">
<HeaderStyle HorizontalAlign="Center"></HeaderStyle>

            <ItemStyle HorizontalAlign="center"></ItemStyle>
        </asp:BoundColumn> 
        
        <asp:BoundColumn DataField="Email" HeaderText="Мэйл хаяг" HeaderStyle-HorizontalAlign="Center">
<HeaderStyle HorizontalAlign="Center"></HeaderStyle>

            <ItemStyle HorizontalAlign="center"></ItemStyle>
        </asp:BoundColumn>
        <asp:BoundColumn DataField="Phone" HeaderText="Утас" HeaderStyle-HorizontalAlign="Center">
<HeaderStyle HorizontalAlign="Center"></HeaderStyle>

            <ItemStyle HorizontalAlign="center"></ItemStyle>
        </asp:BoundColumn> 
        <asp:BoundColumn DataField="Address" HeaderText="Хаяг" HeaderStyle-HorizontalAlign="Center">
<HeaderStyle HorizontalAlign="Center"></HeaderStyle>

            <ItemStyle HorizontalAlign="center"></ItemStyle>
        </asp:BoundColumn>
    </Columns>
              </asp:DataGrid>

    </div>
</form>
</div>