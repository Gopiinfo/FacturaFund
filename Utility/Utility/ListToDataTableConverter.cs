using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Utility
{
    public class ListToDataTableConverter
    {
        public static DataTable ConvertToDataTable<T>(List<T> list)
        {
            DataTable table = new DataTable();

            // Get properties of the type T
            PropertyInfo[] properties = typeof(T).GetProperties();

            // Create columns for the DataTable
            foreach (PropertyInfo property in properties)
            {
                table.Columns.Add(property.Name, property.PropertyType);
            }

            // Add rows to the DataTable
            foreach (T item in list)
            {
                DataRow row = table.NewRow();
                foreach (PropertyInfo property in properties)
                {
                    // Handle nullable types
                    if (property.PropertyType.IsGenericType && property.PropertyType.GetGenericTypeDefinition() == typeof(Nullable<>))
                    {
                        // Get the underlying type (e.g., int? -> int)
                        Type underlyingType = Nullable.GetUnderlyingType(property.PropertyType);
                        object value = property.GetValue(item); // Get the actual value
                        if (value != null)
                        {
                            row[property.Name] = Convert.ChangeType(value, underlyingType);
                        }
                        else
                        {
                            row[property.Name] = DBNull.Value; // or default(underlyingType); if you prefer
                        }
                    }
                    else
                    {
                        row[property.Name] = property.GetValue(item);
                    }


                }
                table.Rows.Add(row);
            }

            return table;
        }
    }
}
