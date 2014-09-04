using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OracleEasyBinding.Entities
{
    [AttributeUsage(AttributeTargets.Property,  AllowMultiple = false, Inherited = true)]
    public class DbColumnAttribute : Attribute
    {
        private string _ColumnName;
        public DbColumnAttribute(string ColumnName)
        {
            this._ColumnName = ColumnName;
        }
        public string GetColumnName()
        {
            return this._ColumnName;
        }
    }
}
