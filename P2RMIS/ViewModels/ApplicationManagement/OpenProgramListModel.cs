using System;
using System.Collections.Generic;
using Sra.P2rmis.Bll.Views;
using Sra.P2rmis.Web.Models;

namespace Sra.P2rmis.Web.UI.Models
{
    /// <summary>
    /// TODO:: document me
    /// </summary>
    public class Program
    {
        #region Constructors
        public Program() { }
        /// <summary>
        /// TODO:: document me
        /// </summary>
        /// <param name="theApplication">-----</param>
        public Program(Application theApplication)
        {
            this.ProgramID = theApplication.ProgramID;
            this.FiscalYear = theApplication.FiscalYear;
            this.Description = theApplication.Description;
            this.Abbreviation = theApplication.Abbreviation;
        }
        #endregion
        #region Properties
        public int ProgramID { get; set; }
        public string FiscalYear { get; set; }
        public string Description { get; set; }
        public string Abbreviation { get; set; }
        public string GetConcatProgram
        {
            get 
            { 
                string concatProgram = Abbreviation + " - " + Description + " (" + FiscalYear + ")";
                return concatProgram;
            }
        }
        #endregion
    }
    /// <summary>
    /// TODO:: document me
    /// </summary>
    public class OpenProgramListModel
    {
        public IEnumerable <Program> Model;
        #region Constructors
        public OpenProgramListModel()
        {
            Model = new List<Program>();
        }
        /// <summary>
        /// TODO:: document me
        /// </summary>
        /// <param name="theBlView">-----</param>
        public OpenProgramListModel(ApplicationManagementView theBlView)
        {
            this.Model = new List<Application>(theBlView.Applications).ConvertAll(new Converter<Application, Program>(ApplicationToProgram));
            this.PersonID = theBlView.PersonID;
        }
        #endregion

        #region Properties
        public int? PersonID { get; set; }
        #endregion

        #region Helpers
        /// <summary>
        /// Converts a business layer SessionView object into
        /// a presentation layer SessionDetail object.
        /// </summary>
        /// <param name="theApplication">Business layer Application view</param>
        /// <returns>Program object created from Application object</returns>
        private static Program ApplicationToProgram(Application theApplication)
        {
            return new Program(theApplication);
        }
        #endregion
    }

}
