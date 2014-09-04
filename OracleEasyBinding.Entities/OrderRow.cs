using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;
using System.Runtime.Serialization;

namespace OracleEasyBinding.Entities
{
    [DataContract]
    public class OrderRow
    {
        [DataMember]
        [DbColumnAttribute("ORDER_ID")]
        public long OrderID { get; set; }
        
        [DataMember]
        [DbColumnAttribute("CUSTOMER_NUMBER")]
        public long CustomerNumber { get;set;}

        [DataMember]
        [DbColumnAttribute("CUSTOMER_NAME")]
        public string CustomerName { get;set;}

        [DataMember]
        [DbColumnAttribute("AMOUNT")]
        public decimal Amount { get; set; }
    }
}
