using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
namespace Sra.P2rmis.CrossCuttingServices
{
    /// <summary>
    /// Collection of c# class extension methods, making them available to all layers.  Extension methods
    /// should be grouped into a separate region for each c# foundation class.
    /// </summary>
    public static class ClassExtensionMethods
    {
        #region string extensions
        /// <summary>
        /// Removes all carriage returns (\n) from the specified string
        /// </summary>
        /// <param name="str">String to process</param>
        /// <returns>If null or empty the empty string; otherwise the original string without carriage returns ('\n')</returns>
        public static string RemoveCarriageReturns(this String str)
        {
            return (string.IsNullOrEmpty(str)) ? string.Empty : str.Replace("\n", string.Empty);
        }
        #endregion
        #region List<T> extensions
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        /// <param name="item"></param>
        public static void AddNonNull<T>(this List<T> list, T item)
        {
            if (item != null)
            {
                list.Add(item);
            }
        }
        /// <summary>
        /// Adds an item to the list if the result was false
        /// </summary>
        /// <typeparam name="T">List of type T items</typeparam>
        /// <param name="list">List of type T items</param>
        /// <param name="result">Result of test</param>
        /// <param name="item">item to add</param>
        public static void AddIfFailed<T>(this List<T> list, bool result, T item)
        {
            if (!result)
            {
                list.Add(item);
            }
        }
        #endregion
        #region Enumeration<T> extensions
        /// <summary>
        /// Checks if an enumeration is empty
        /// </summary>
        /// <typeparam name="T">Enumeration's elements type</typeparam>
        /// <param name="theEnumeration"></param>
        /// <returns></returns>
        public static bool IsEmpty<T>(this IEnumerable<T> theEnumeration)
        {
            return !theEnumeration.Any();
        }

        /// <summary>
        /// Converts enumerable collection to a data table.
        /// </summary>
        /// <typeparam name="T">Type of enumerable</typeparam>
        /// <param name="collection">The collection to be converted.</param>
        /// <returns>Data Table</returns>
        public static DataTable ToDataTable<T>(this IEnumerable<T> collection)
        {
            DataTable dt = new DataTable();
            Type t = typeof(T);
            PropertyInfo[] pia = t.GetProperties();
            var includedProperties = pia.Where(x => x.GetCustomAttribute<DataTableIgnoreAttribute>() == null);
            //Create the columns in the DataTable
            foreach (PropertyInfo pi in includedProperties)
            {
                var customName = pi.GetCustomAttribute<DataTableColumnNameAttribute>()?.Name;
                dt.Columns.Add(customName ?? pi.Name, Nullable.GetUnderlyingType(pi.PropertyType) ?? pi.PropertyType);
            }
            //Populate the table
            foreach (T item in collection)
            {
                DataRow dr = dt.NewRow();
                dr.BeginEdit();
                foreach (PropertyInfo pi in includedProperties)
                {
                    var customName = pi.GetCustomAttribute<DataTableColumnNameAttribute>()?.Name;
                    dr[customName ?? pi.Name] = pi.GetValue(item) ?? DBNull.Value;
                }
                dr.EndEdit();
                dt.Rows.Add(dr);
            }
            return dt;
        }
        #endregion
    }
}

