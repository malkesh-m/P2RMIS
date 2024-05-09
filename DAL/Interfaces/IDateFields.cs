using System;

namespace Sra.P2rmis.Dal.Interfaces
{
    /// <summary>
    /// Interface 
    /// </summary>
    public interface IDateFields
    {
        //
        // Entity object must have these fields
        //
        int CreatedBy { get; set; }
        System.DateTime CreatedDate { get; set; }
        int ModifiedBy { get; set; }
        System.DateTime ModifiedDate { get; set; }
    }
    /// <summary>
    /// All new database tables should have these fields which are updated
    /// when the entry is created & updated.  
    /// <remarks>
    /// When the tables were laid out for PanelManagement a decision was made
    /// to change the types.  Hence the second interface.  Because there was
    /// a plain for changing the existing tables field types both interfaces 
    /// are located here.  Eventually the first interface can be deleted.
    /// </remarks>
    /// </summary>
    public interface IStandardDateFields
    {
        //
        // Entity object must have these fields
        //
        int? CreatedBy { get; set; }
        Nullable<System.DateTime> CreatedDate { get; set; }
        int? ModifiedBy { get; set; }
        Nullable<System.DateTime> ModifiedDate { get; set; }
        int? DeletedBy { get; set; }
        Nullable<System.DateTime> DeletedDate { get; set; }
    }
    /// <summary>
    /// All new database tables should have these fields which are updated
    /// when the entry is created & updated.  
    /// <remarks>
    /// When the tables were laid out for PanelManagement a decision was made
    /// to change the types.  Hence the second and third interface.  Because there was
    /// a plan for changing the existing tables field types all date interfaces 
    /// are located here.  Note: At some time in the future, these interfaces 
    /// may be resolved to a single interface.
    /// </remarks>
    /// </summary>
    public interface IAlternateStandardDateFields
    {
        //
        // Entity object must have these fields
        //
        int CreatedBy { get; set; }
        Nullable<System.DateTime> CreatedDate { get; set; }
        int? ModifiedBy { get; set; }
        Nullable<System.DateTime> ModifiedDate { get; set; }
    }
}
