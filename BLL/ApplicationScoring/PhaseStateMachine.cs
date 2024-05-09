using Sra.P2rmis.WebModels.ApplicationScoring;
using System.Collections.Generic;
using System.Linq;

namespace Sra.P2rmis.Bll.ApplicationScoring
{
    /// <summary>
    /// Represents & determines a phase state for the display of critique icons
    /// </summary>
    internal static class PhaseStateMachine
    {
        #region Interal Classes
        /// <summary>
        /// Description of a PhaseState table entry.  
        /// </summary>
        public class PhaseState2
        {
            /// <summary>
            /// The PhaseState to return
            /// </summary>
            public StateResult Result { get; set; }
            /// <summary>
            /// Is the reviewer assigned to the application?
            /// </summary>
            public bool AssignedToApplication { get; set; }
            /// <summary>
            /// Whether the critique has been submitted for the current phase
            /// </summary>
            public bool ApplicationCritiqueSubmitted { get; set; }
            /// <summary>
            /// Whether all assigned critiques has been submitted for the current phase
            /// </summary>
            public bool PhaseCritiqueSubmitted { get; set; }
            /// <summary>
            /// Whether the panel phase is open 
            /// </summary>
            public bool IsOpen { get; set; }
            /// <summary>
            /// Whether the panel phase is  reopened
            /// </summary>
            public bool IsReopened { get; set; }
            /// <summary>
            /// State as text.
            /// </summary>
            /// <returns></returns>
            public override string ToString()
            {
                //
                // TODO: format the state as a text string
                //
                return "This should not happen";
            }
        }
        #endregion
        #region Statics
        /// <summary>
        /// Enumeration of all states as shown in Confluence table on 
        /// page https://prsm-confluence.srahosting.com/display/p2rmis/My+Workspace
        /// </summary>
        private static readonly List<PhaseState2> StateTable = new List<PhaseState2>()
        {
            new PhaseState2
            {
                Result = StateResult.Phase101,
                AssignedToApplication = false,
                ApplicationCritiqueSubmitted = true,
                PhaseCritiqueSubmitted = true,
                IsOpen = true,
                IsReopened = true
            },
            new PhaseState2
            {
                Result = StateResult.Phase102,
                AssignedToApplication = false,
                ApplicationCritiqueSubmitted = false,
                PhaseCritiqueSubmitted = true,
                IsOpen = true,
                IsReopened = true
            },
            new PhaseState2
            {
                Result = StateResult.Phase103,
                AssignedToApplication = false,
                ApplicationCritiqueSubmitted = true,
                PhaseCritiqueSubmitted = true,
                IsOpen = false,
                IsReopened = true
            },
            new PhaseState2
            {
                Result = StateResult.Phase104,
                AssignedToApplication = false,
                ApplicationCritiqueSubmitted = false,
                PhaseCritiqueSubmitted = true,
                IsOpen = false,
                IsReopened = true
            },
            new PhaseState2
            {
                Result = StateResult.Phase105,
                AssignedToApplication = false,
                ApplicationCritiqueSubmitted = true,
                PhaseCritiqueSubmitted = true,
                IsOpen = true,
                IsReopened = false
            },
            new PhaseState2
            {
                Result = StateResult.Phase106,
                AssignedToApplication = false,
                ApplicationCritiqueSubmitted = false,
                PhaseCritiqueSubmitted = true,
                IsOpen = true,
                IsReopened = false
            },
            new PhaseState2
            {
                Result = StateResult.Phase107,
                AssignedToApplication = false,
                ApplicationCritiqueSubmitted = true,
                PhaseCritiqueSubmitted = true,
                IsOpen = false,
                IsReopened = false
            },
            new PhaseState2
            {
                Result = StateResult.Phase108,
                AssignedToApplication = false,
                ApplicationCritiqueSubmitted = false,
                PhaseCritiqueSubmitted = true,
                IsOpen = false,
                IsReopened = false
            },
            new PhaseState2
            {
                Result = StateResult.Phase109,
                AssignedToApplication = false,
                ApplicationCritiqueSubmitted = true,
                PhaseCritiqueSubmitted = false,
                IsOpen = true,
                IsReopened = true
            },
            new PhaseState2
            {
                Result = StateResult.Phase110,
                AssignedToApplication = false,
                ApplicationCritiqueSubmitted = false,
                PhaseCritiqueSubmitted = false,
                IsOpen = true,
                IsReopened = true
            },
            new PhaseState2
            {
                Result = StateResult.Phase111,
                AssignedToApplication = false,
                ApplicationCritiqueSubmitted = true,
                PhaseCritiqueSubmitted = false,
                IsOpen = false,
                IsReopened = true
            },
            new PhaseState2
            {
                Result = StateResult.Phase112,
                AssignedToApplication = false,
                ApplicationCritiqueSubmitted = false,
                PhaseCritiqueSubmitted = false,
                IsOpen = false,
                IsReopened = true
            },
            new PhaseState2
            {
                Result = StateResult.Phase113,
                AssignedToApplication = false,
                ApplicationCritiqueSubmitted = true,
                PhaseCritiqueSubmitted = false,
                IsOpen = true,
                IsReopened = false
            },
            new PhaseState2
            {
                Result = StateResult.Phase114,
                AssignedToApplication = false,
                ApplicationCritiqueSubmitted = false,
                PhaseCritiqueSubmitted = false,
                IsOpen = true,
                IsReopened = false
            },
            new PhaseState2
            {
                Result = StateResult.Phase115,
                AssignedToApplication = false,
                ApplicationCritiqueSubmitted = true,
                PhaseCritiqueSubmitted = false,
                IsOpen = false,
                IsReopened = false
            },
            new PhaseState2
            {
                Result = StateResult.Phase116,
                AssignedToApplication = false,
                ApplicationCritiqueSubmitted = false,
                PhaseCritiqueSubmitted = false,
                IsOpen = false,
                IsReopened = false
            },
            new PhaseState2
            {
                Result = StateResult.Phase117,
                AssignedToApplication = true,
                ApplicationCritiqueSubmitted = true,
                PhaseCritiqueSubmitted = true,
                IsOpen = true,
                IsReopened = true
            },
            new PhaseState2
            {
                Result = StateResult.Phase118,
                AssignedToApplication = true,
                ApplicationCritiqueSubmitted = true,
                PhaseCritiqueSubmitted = false,
                IsOpen = true,
                IsReopened = true
            },
            new PhaseState2
            {
                Result = StateResult.Phase119,
                AssignedToApplication = true,
                ApplicationCritiqueSubmitted = true,
                PhaseCritiqueSubmitted = true,
                IsOpen = false,
                IsReopened = true
            },
            new PhaseState2
            {
                Result = StateResult.Phase120,
                AssignedToApplication = true,
                ApplicationCritiqueSubmitted = true,
                PhaseCritiqueSubmitted = false,
                IsOpen = false,
                IsReopened = true
            },
            new PhaseState2
            {
                Result = StateResult.Phase121,
                AssignedToApplication = true,
                ApplicationCritiqueSubmitted = true,
                PhaseCritiqueSubmitted = true,
                IsOpen = true,
                IsReopened = false
            },
            new PhaseState2
            {
                Result = StateResult.Phase122,
                AssignedToApplication = true,
                ApplicationCritiqueSubmitted = true,
                PhaseCritiqueSubmitted = false,
                IsOpen = true,
                IsReopened = false
            },
            new PhaseState2
            {
                Result = StateResult.Phase123,
                AssignedToApplication = true,
                ApplicationCritiqueSubmitted = true,
                PhaseCritiqueSubmitted = true,
                IsOpen = false,
                IsReopened = false
            },
            new PhaseState2
            {
                Result = StateResult.Phase124,
                AssignedToApplication = true,
                ApplicationCritiqueSubmitted = true,
                PhaseCritiqueSubmitted = false,
                IsOpen = false,
                IsReopened = false
            },
            new PhaseState2
            {
                Result = StateResult.Phase125,
                AssignedToApplication = true,
                ApplicationCritiqueSubmitted = false,
                PhaseCritiqueSubmitted = true,
                IsOpen = false,
                IsReopened = false
            },
            new PhaseState2
            {
                Result = StateResult.Phase126,
                AssignedToApplication = true,
                ApplicationCritiqueSubmitted = false,
                PhaseCritiqueSubmitted = false,
                IsOpen = false,
                IsReopened = false
            },
            new PhaseState2
            {
                Result = StateResult.Phase127,
                AssignedToApplication = true,
                ApplicationCritiqueSubmitted = false,
                PhaseCritiqueSubmitted = true,
                IsOpen = false,
                IsReopened = true
            },
            new PhaseState2
            {
                Result = StateResult.Phase128,
                AssignedToApplication = true,
                ApplicationCritiqueSubmitted = false,
                PhaseCritiqueSubmitted = false,
                IsOpen = false,
                IsReopened = true
            },
            new PhaseState2
            {
                Result = StateResult.Phase129,
                AssignedToApplication = true,
                ApplicationCritiqueSubmitted = false,
                PhaseCritiqueSubmitted = true,
                IsOpen = true,
                IsReopened = true
            },
            new PhaseState2
            {
                Result = StateResult.Phase130,
                AssignedToApplication = true,
                ApplicationCritiqueSubmitted = false,
                PhaseCritiqueSubmitted = false,
                IsOpen = true,
                IsReopened = true
            },
            new PhaseState2
            {
                Result = StateResult.Phase131,
                AssignedToApplication = true,
                ApplicationCritiqueSubmitted = false,
                PhaseCritiqueSubmitted = true,
                IsOpen = true,
                IsReopened = false
            },
            new PhaseState2
            {
                Result = StateResult.Phase132,
                AssignedToApplication = true,
                ApplicationCritiqueSubmitted = false,
                PhaseCritiqueSubmitted = false,
                IsOpen = true,
                IsReopened = false
            }
        };
        #endregion
        #region Services
        /// <summary>
        /// Determines the user's Phase state
        /// </summary>
        /// <param name="assignedToApplication">Indicates if the user is assigned to the application</param>
        /// <param name="applicationCritiqueSubmitted">Indicates if the user's critique for the application has been submitted</param>
        /// <param name="phaseCritiqueSubmitted">Indicates if the user has submitted all assigned critiques for the current phase</param>
        /// <param name="isOpen">Indicates if the current phase is open(but not reopened)</param>
        /// <param name="isReopened">Indicates if the current phase is reopened</param>
        /// <returns>StateResult ENUM representing the user state in the phase.</returns>
        public static StateResult PhaseState(bool assignedToApplication, bool applicationCritiqueSubmitted, bool phaseCritiqueSubmitted, bool isOpen, bool isReopened)
        {
            //
            // Find where this entry is in the state table.  The StateTable should contain every combination of the parameters that will result in a phase of interest.
            //
            var result = StateTable.Where(x => (
                                                (x.ApplicationCritiqueSubmitted == applicationCritiqueSubmitted) &&
                                                (x.AssignedToApplication == assignedToApplication) &&
                                                (x.PhaseCritiqueSubmitted == phaseCritiqueSubmitted) &&
                                                (x.IsOpen == isOpen) &&
                                                (x.IsReopened == isReopened)
                                               ));

            return (result.Count() == 1) ? result.First().Result : StateResult.Default;
        }
        #endregion
    }
}
