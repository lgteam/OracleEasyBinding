using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using OracleEasyBinding.Entities;

namespace OracleEasyBinding.DAL
{
    public class DbColumnMapper
    {
        // function that creates an object from the given data row
        public static T MapRow<T>(DataRow row) where T : new()
        {
            // create a new object
            T item = new T();

            #region set the item
            // go through each column
            foreach (DataColumn c in row.Table.Columns)
            {
                // find the property for the column by name
                var allProps = item.GetType().GetProperties();
                foreach (var prop in allProps)
                {
                    string colName = null;
                    var allAttrs = prop.GetCustomAttributes(typeof(DbColumnAttribute), true);
                    if (allAttrs != null &&
                        allAttrs.Any())
                    {
                        colName = ((DbColumnAttribute)allAttrs.First()).GetColumnName();
                    }

                    if ((!string.IsNullOrEmpty(colName) && c.ColumnName.Equals(colName, StringComparison.InvariantCulture)) ||
                        prop.Name.Equals(c.ColumnName, StringComparison.InvariantCultureIgnoreCase) ||
                        prop.Name.Equals(c.ColumnName.Replace("_", string.Empty), StringComparison.InvariantCultureIgnoreCase))
                    {
                        // if exists, set the value
                        if (row[c] != null &&
                            row[c] != DBNull.Value)
                        {
                            try
                            {
                                if (prop.PropertyType.Equals(c.DataType))
                                {
                                    prop.SetValue(item, row[c], null);
                                }
                                else
                                {
                                    var val = Convert.ChangeType(row[c], prop.PropertyType);
                                    prop.SetValue(item, val, null);
                                }
                            }
                            catch
                            {
                                //ignore
                            }                            
                            break;
                        }
                    }
                }
            }
            #endregion
            // return 
            return item;
        }

        // function that creates a list of an object from the given data table
        public static List<T> MapTable<T>(DataTable tbl) where T : new()
        {
            // define return list
            List<T> lst = new List<T>();

            // go through each row
            foreach (DataRow r in tbl.Rows)
            {
                // add to the list
                lst.Add(MapRow<T>(r));
            }

            // return the list
            return lst;
        }
    }
}
