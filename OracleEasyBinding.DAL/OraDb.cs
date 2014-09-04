using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using System.Data;
using Oracle.DataAccess.Client;
using System.Reflection;
using OracleEasyBinding.Entities;

namespace OracleEasyBinding.DAL
{
    public class OraDb
    {
        #region static
        private static string ConnectionString = ConfigurationManager.ConnectionStrings["OracleConnectionString"].ConnectionString;
        public static DataTable ExecuteOraProcedure(string procName, List<OracleParameter> parameters, string outRefCursorName)
        {
            DataTable dtResult = new DataTable();

            using (OracleConnection con = new OracleConnection(ConnectionString))
            {
                using (OracleCommand cmd = new OracleCommand())
                {
                    cmd.Connection = con;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = procName;

                    if (parameters != null &&
                        parameters.Any())
                    {
                        foreach (OracleParameter prm in parameters)
                        {
                            cmd.Parameters.Add(prm);
                        }
                    }

                    if (!string.IsNullOrEmpty(outRefCursorName))
                    {
                        OracleParameter p = new OracleParameter(outRefCursorName,
                                                                OracleDbType.RefCursor,
                                                                ParameterDirection.Output);
                        cmd.Parameters.Add(p);
                    }

                    using (OracleDataAdapter da = new OracleDataAdapter(cmd))
                    {
                        da.Fill(dtResult);
                    }
                }

                return dtResult;
            }
        }
        #endregion

        #region public methods
        /// <summary>
        /// Call procedure which returns refcursor with multiple rows
        /// </summary>
        /// <param name="customerNumber"></param>
        /// <returns></returns>
        public List<OrderRow> GetOrders(long customerNumber)
        {
            List<OrderRow> orders = null;
            List<OracleParameter> oraParams = new List<OracleParameter>();
            OracleParameter p_customer_number = new OracleParameter("p_customer_number", OracleDbType.Int64);
            p_customer_number.Value = customerNumber;
            oraParams.Add(p_customer_number);

            DataTable dt = ExecuteOraProcedure("MYPACKAGE.get_orders", oraParams, "p_order_data");
            if (dt != null &&
                dt.Rows.Count > 0)
            {
                orders = DbColumnMapper.MapTable<OrderRow>(dt);
            }
            return orders;
        }

        /// <summary>
        /// Call procedure which returns refcursor with one row
        /// </summary>
        /// <param name="orderId"></param>
        /// <returns></returns>
        public OrderRow GetOrder(long orderId)
        {
            OrderRow order = null;
            List<OracleParameter> oraParams = new List<OracleParameter>();
            OracleParameter p_order_id = new OracleParameter("p_order_id", OracleDbType.Int64);
            p_order_id.Value = orderId;
            oraParams.Add(p_order_id);

            DataTable dt = ExecuteOraProcedure("MYPACKAGE.get_orders", oraParams, "p_order_data");
            if (dt != null &&
                dt.Rows.Count > 0)
            {
                order = DbColumnMapper.MapRow<OrderRow>(dt.Rows[0]);
            }
            return order;
        }

        /// <summary>
        /// Call function which returns scalar
        /// </summary>
        /// <param name="orderId"></param>
        /// <returns></returns>
        public long GetCustomerNumberByOrderId(long orderId)
        {

            List<OracleParameter> oraParams = new List<OracleParameter>();

            OracleParameter retResult = new OracleParameter("result", OracleDbType.Int64, ParameterDirection.ReturnValue);
            oraParams.Add(retResult);

            OracleParameter p_order_id = new OracleParameter("p_order_id", OracleDbType.Int64);
            p_order_id.Value = orderId;
            oraParams.Add(p_order_id);

            ExecuteOraProcedure("MYPACKAGE.getcustomer_number_by_order_id", oraParams, null);
            if (retResult == null ||
                retResult.Value == null ||
                string.IsNullOrEmpty(retResult.Value.ToString()) ||
                retResult.Value.ToString().ToLower() == "null")
            {
                return -1;
            }

            return Convert.ToInt64(retResult.Value.ToString());
        }    
        #endregion
    }
}
