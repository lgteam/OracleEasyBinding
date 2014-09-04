using OracleEasyBinding.DAL;
using OracleEasyBinding.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace OracleEasyBinding.Web
{
    public partial class _Default : System.Web.UI.Page
    {
        private OraDb DB;
        public _Default()
        {
            this.DB = new DAL.OraDb();
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            
        }

        protected void btnGetManyOrders_Click(object sender, EventArgs e)
        {
            int customerNumber = 1;
            List<OrderRow> orders = this.DB.GetOrders(customerNumber);
            gdv.DataSource = orders;
            gdv.DataBind();
        }

        protected void btnGetOneOrder_Click(object sender, EventArgs e)
        {
            long orderId = 111;
            OrderRow order = this.DB.GetOrder(orderId);
            if (order != null)
            {
                lblAmount.Text = order.Amount.ToString();
                lblCustomerName.Text = order.CustomerName;
                lblCustomerNumber.Text = order.CustomerNumber.ToString();
                lblOrderID.Text = order.OrderID.ToString();
                pnl1.Visible = true;
            }
        }

        protected void btnGetCustomerNumber_Click(object sender, EventArgs e)
        {
            long orderId = 111;
            long customerNumber = this.DB.GetCustomerNumberByOrderId(orderId);
            if (customerNumber != -1)
            {
                lblCustomerNumber2.Text = customerNumber.ToString();
                pnl2.Visible = true;
            }
        }
    }
}