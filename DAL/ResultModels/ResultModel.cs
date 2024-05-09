using System.Collections.Generic;

namespace Sra.P2rmis.Dal.ResultModels
{
    public class ResultModel<T>: IResultModel<T>
    {
        /// <summary>
        /// List of model containing the panels for a programs and fiscal
        /// </summary>
        private IEnumerable<T> _modelList;
        public IEnumerable<T> ModelList 
        {
            get { return _modelList; } 
            set 
            {
                if (value == null)
                {
                    _modelList = new List<T>();
                }
                else
                {
                    _modelList = value;
                }
            } 
        }
    }
}
