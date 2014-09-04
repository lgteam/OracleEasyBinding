<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="OracleEasyBinding.Web._Default" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style type="text/css">
        .box {
            border: 2px solid silver;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div class="box">
            <asp:Button ID="btnGetManyOrders" runat="server" Text="Get multiple orders (procedure multiple rows)" OnClick="btnGetManyOrders_Click" />
            <asp:GridView ID="gdv" runat="server"></asp:GridView>
        </div>
        <div class="box">
            <asp:Button ID="btnGetOneOrder" runat="server" Text="Get one order (procedure one row)" OnClick="btnGetOneOrder_Click" />
            <asp:Panel runat="server" ID="pnl1" CssClass="box" Visible="false">
                <div>
                    OrderID:<asp:Label ID="lblOrderID" runat="server"></asp:Label>
                </div>
                <div>
                    CustomerNumber:<asp:Label ID="lblCustomerNumber" runat="server"></asp:Label>
                </div>
                <div>
                    CustomerName:<asp:Label ID="lblCustomerName" runat="server"></asp:Label>
                </div>
                <div>
                    Amount USD:<asp:Label ID="lblAmount" runat="server"></asp:Label>
                </div>
            </asp:Panel>
        </div>
        <div class="box">
            <asp:Button ID="btnGetCustomerNumber" runat="server" Text="Get customer number (function)" OnClick="btnGetCustomerNumber_Click"/>
            <asp:Panel runat="server" ID="pnl2" CssClass="box" Visible="false">
                <div>
                    CustomerNumber:<asp:Label ID="lblCustomerNumber2" runat="server"></asp:Label>
                </div>
            </asp:Panel>
        </div>
    </form>
</body>
</html>
