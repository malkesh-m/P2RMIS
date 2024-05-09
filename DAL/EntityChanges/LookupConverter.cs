using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sra.P2rmis.Dal.Interfaces;

namespace Sra.P2rmis.Dal.EntityChanges
{
    /// <summary>
    /// 
    /// </summary>
    public interface ILookupConverter
    {
    }
    /// <summary>
    /// 
    /// </summary>
    public class BaseLookupConverter
    {
        /// <summary>
        /// Retrieves the specified value from the LookUp repository
        /// </summary>
        /// <typeparam name="T">Entity type</typeparam>
        /// <param name="repository">Entity repository</param>
        /// <param name="propertyGetter">Entity property returning the LookUp value</param>
        /// <param name="index">Entity index</param>
        /// <returns>LookUp value for the index specified</returns>
        protected string LookUp<T>(IGenericRepository<T> repository, Func<T, string> propertyGetter, int index)
        {
            var entity = repository.GetByID(index);
            return propertyGetter(entity);
        }
        /// <summary>
        /// Converts the index (as a string) into the LookUp table value.
        /// </summary>
        /// <typeparam name="T">Entity type</typeparam>
        /// <param name="repository">Entity repository</param>
        /// <param name="propertyGetter">Entity property returning the LookUp value</param>
        /// <param name="indexAsString">Entity index</param>
        /// <returns>Look up value for the index specified</returns>
        protected string Convert<T>(IGenericRepository<T> repository, Func<T, string> propertyGetter, string indexAsString)
        {
            //
            // Initialize the return value.  If the indexAsString is not convertible to an integer
            // the empty string is returned.
            //
            string result = string.Empty;

            if (!string.IsNullOrWhiteSpace(indexAsString))
            {
                try
                {
                    //
                    // Convert the index to an integer.  If it barfs then 
                    // we return the default
                    //
                    int index = System.Convert.ToInt32(indexAsString);
                    result = LookUp<T>(repository, propertyGetter, index);
                }
                catch { }
            }
            return result;
        }
    }
    /// <summary>
    /// LookUp converter for the ProfessionalAffiliation LookUp table.
    /// </summary>
    public class ProfessionalAffilationLookupConverter: BaseLookupConverter, ILookupConverter
    {
        /// <summary>
        /// Converts ProfessionalAffiliation indexes maintained as strings when ......
        /// </summary>
        /// <param name="repository">LookUp repository</param>
        /// <param name="oldIndex">Repository index of old value</param>
        /// <param name="newIndex">Repository index of new value</param>
        /// <returns>Tuple of old text value, new text value</returns>
        public Tuple<string, string> Convert(IGenericRepository<ProfessionalAffiliation> repository, string oldIndex, string newIndex) 
        {
            return Tuple.Create<string, string>(
                                                Convert<ProfessionalAffiliation>(repository, x => x.Type, oldIndex),
                                                Convert<ProfessionalAffiliation>(repository, x => x.Type, newIndex));

        }
    }
    /// <summary>
    /// LookUp converter for the State LookUp table.
    /// </summary>
    public class StateLookupConverter : BaseLookupConverter, ILookupConverter
    {
        /// <summary>
        /// Converts State indexes maintained as strings when ......
        /// </summary>
        /// <param name="repository">LookUp repository</param>
        /// <param name="oldIndex">Repository index of old value</param>
        /// <param name="newIndex">Repository index of new value</param>
        /// <returns>Tuple of old text value, new text value</returns>
        public Tuple<string, string> Convert(IGenericRepository<State> repository, string oldIndex, string newIndex)
        {
            return Tuple.Create<string, string>(
                                                Convert<State>(repository, x => x.StateName, oldIndex),
                                                Convert<State>(repository, x => x.StateName, newIndex));
        }
     }
    /// <summary>
    /// LookUp converter for the Degree LookUp table.
    /// </summary>
    public class DegreeLookupConverter : BaseLookupConverter, ILookupConverter
    {
        /// <summary>
        /// Converts Degree indexes maintained as strings when ......
        /// </summary>
        /// <param name="repository">LookUp repository</param>
        /// <param name="oldIndex">Repository index of old value</param>
        /// <param name="newIndex">Repository index of new value</param>
        /// <returns>Tuple of old text value, new text value</returns>
        public Tuple<string, string> Convert(IGenericRepository<Degree> repository, string oldIndex, string newIndex)
        {
            return Tuple.Create<string, string>(
                                                Convert<Degree>(repository, x => x.DegreeName, oldIndex),
                                                Convert<Degree>(repository, x => x.DegreeName, newIndex));
        }
    }

}
