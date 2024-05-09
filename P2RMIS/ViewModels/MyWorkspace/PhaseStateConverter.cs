using System.Collections.Generic;
using Sra.P2rmis.WebModels.ApplicationScoring;

namespace Sra.P2rmis.Web.ViewModels
{
    /// <summary>
    /// Interprets the StateResult for the UI
    /// </summary>
    public static class PhaseStateConverter
    {
        #region Deprecrated Conversion tables
        /// <summary>
        /// States that the user can access the View command for critiques
        /// </summary>
        private static readonly List<StateResult> CanViewStates = new List<StateResult>()
        {
            StateResult.Phase01,
            StateResult.Phase02,
            StateResult.Phase05,
            StateResult.Phase06,
            StateResult.Phase09,
            StateResult.Phase11,
            StateResult.Phase13,
            StateResult.Phase15,
            StateResult.Phase17,
            StateResult.Phase18,
            StateResult.Phase21,
            StateResult.Phase22,
            StateResult.Phase25,
            StateResult.Phase27,
            StateResult.Phase29,
            StateResult.Phase31
        };

        /// <summary>
        /// States that the user can access the Start command for critiques
        /// </summary>
        private static readonly List<StateResult> StartStates = new List<StateResult>()
        {
            StateResult.Phase26,
            StateResult.Phase28
        };

        /// <summary>
        /// States that the user can access the Edit command for critiques
        /// </summary>
        private static readonly List<StateResult> EditStates = new List<StateResult>()
        {
            StateResult.Phase10,
            StateResult.Phase12
        };

        /// <summary>
        /// States that the user can access the View command for critiques
        /// </summary>
        private static readonly List<StateResult> ExpiredStates = new List<StateResult>()
        {
            StateResult.Phase14,
            StateResult.Phase16,
            StateResult.Phase19,
            StateResult.Phase30,
            StateResult.Phase32
        };

        private static readonly List<StateResult> EmptyStates = new List<StateResult>()
        {
            StateResult.Phase03,
            StateResult.Phase04,
            StateResult.Phase07,
            StateResult.Phase08,
            StateResult.Phase19,
            StateResult.Phase20,
            StateResult.Phase23,
            StateResult.Phase24
        };
        #endregion
        #region Conversion tables
        /// <summary>
        /// When the user should be shown the icon for Enabled Normal View
        /// </summary>
        private static readonly List<StateResult> EnabledNormalViewStates = new List<StateResult>()
        {
            StateResult.Phase101,
            StateResult.Phase102,
            StateResult.Phase103,
            StateResult.Phase104,
            StateResult.Phase105,
            StateResult.Phase106,
            StateResult.Phase107,
            StateResult.Phase108,
            StateResult.Phase118,
            StateResult.Phase120,
            StateResult.Phase122,
            StateResult.Phase124
        };
        /// <summary>
        /// When the user should be shown the icon for Enabled Normal Edit
        /// </summary>
        private static readonly List<StateResult> EnabledNormalEditStates = new List<StateResult>()
        {
            StateResult.Phase129,
            StateResult.Phase130,
            StateResult.Phase131,
            StateResult.Phase132
        };
        /// <summary>
        /// When the user should be shown the icon for Disabled Normal View
        /// </summary>
        private static readonly List<StateResult> DisabledNormalViewStates = new List<StateResult>()
        {
            StateResult.Phase109,
            StateResult.Phase110,
            StateResult.Phase111,
            StateResult.Phase112,
            StateResult.Phase113,
            StateResult.Phase114,
            StateResult.Phase115,
            StateResult.Phase116
        };
        /// <summary>
        /// When the user should be shown the icon for Disabled Normal Edit
        /// </summary>
        private static readonly List<StateResult> DisabledAbnormalEditStates = new List<StateResult>()
        {
            StateResult.Phase125,
            StateResult.Phase126
        };
        /// <summary>
        /// When the user should be shown the icon for Enabled Abnormal Edit
        /// </summary>
        private static readonly List<StateResult> EnabledAbnormalEditStates = new List<StateResult>()
        {
            StateResult.Phase127,
            StateResult.Phase128
        };
        /// <summary>
        /// When the user should be shown the icon for Enabled Abnormal Edit
        /// </summary>
        private static readonly List<StateResult> EnabledSubmittedViewState = new List<StateResult>()
        {
            StateResult.Phase117,
            StateResult.Phase119,
            StateResult.Phase121,
            StateResult.Phase123
        };
        /// <summary>
        /// When the user should be shown the icon for Enabled Abnormal Edit
        /// </summary>
        private static readonly List<StateResult> DisabledSubmittedViewState = new List<StateResult>()
        {            
        };
        #endregion
        #region deprecated Converter interface
        /// <summary>
        /// Determines whether this instance can view the specified  critique state.
        /// </summary>
        /// <param name="state">The state.</param>
        /// <returns></returns>
        public static bool CanView(StateResult state)
        {
            return CanViewStates.Contains(state);
        }

        /// <summary>
        /// Determines whether this instance can start the specified critique.
        /// </summary>
        /// <param name="state">The state.</param>
        /// <returns></returns>
        public static bool CanStart(StateResult state)
        {
            return StartStates.Contains(state);
        }
      
        /// <summary>
        /// Determines whether this instance is in an expired critique state.
        /// </summary>
        /// <param name="state">The state.</param>
        /// <returns></returns>
        public static bool IsExpired(StateResult state)
        {
            return ExpiredStates.Contains(state);
        }

        /// <summary>
        /// Determines whether this instance is in an edit critique state.
        /// </summary>
        /// <param name="state">The state.</param>
        /// <returns></returns>
        public static bool CanEdit(StateResult state)
        {
            return EditStates.Contains(state);
        }

        /// <summary>
        /// Determines whether this instace is in an empty critique state (no action available).
        /// </summary>
        /// <param name="state">The state.</param>
        /// <returns></returns>
        public static bool IsEmpty(StateResult state)
        {
            return EmptyStates.Contains(state);
        }
        #endregion
        #region Converter interface
        /// <summary>
        /// Determines if the user should be shown an icon for Enabled Normal View
        /// </summary>
        /// <param name="state">A StateResult to test</param>
        /// <returns>True if the user should be shown the icon for Enabled Normal View</returns>
        public static bool EnabledNormalView(StateResult state)
        {
            return EnabledNormalViewStates.Contains(state);
        }
        /// <summary>
        /// Determines if the user should be shown an icon for Disabled Normal View
        /// </summary>
        /// <param name="state">A StateResult to test</param>
        /// <returns>True if the user should be shown the icon for Disabled Normal View</returns>
        public static bool DisabledNormalView(StateResult state)
        {
            return DisabledNormalViewStates.Contains(state);
        }
        /// <summary>
        /// Determines if the user should be shown an icon for Disabled Abnormal Edit
        /// </summary>
        /// <param name="state">A StateResult to test</param>
        /// <returns>True if the user should be shown the icon for Disabled Abnormal Edit</returns>
        public static bool DisabledAbnormalEdit(StateResult state)
        {
            return DisabledAbnormalEditStates.Contains(state);
        }
        /// <summary>
        /// Determines if the user should be shown an icon for Enabled Abnormal Edit
        /// </summary>
        /// <param name="state">A StateResult to test</param>
        /// <returns>True if the user should be shown the icon for Enabled Abnormal Edit</returns>
        public static bool EnabledAbnormalEdit(StateResult state)
        {
            return EnabledAbnormalEditStates.Contains(state);
        }
        /// <summary>
        /// Determines if the user should be shown an icon for Enabled Normal Edit
        /// </summary>
        /// <param name="state">A StateResult to test</param>
        /// <returns>True if the user should be shown the icon for Enabled Normal Edit</returns>
        public static bool EnabledNormalEdit(StateResult state)
        {
            return EnabledNormalEditStates.Contains(state);
        }
        /// <summary>
        /// Determines if the user should be shown an icon for Enabled Submitted View
        /// </summary>
        /// <param name="state">A StateResult to test</param>
        /// <returns>True if the user should be shown the icon for Enabled Submitted View</returns>
        public static bool EnabledSubmittedView(StateResult state)
        {
            return EnabledSubmittedViewState.Contains(state);
        }
        /// <summary>
        /// Determines if the user should be shown an icon for Disabled Submitted View
        /// </summary>
        /// <param name="state">A StateResult to test</param>
        /// <returns>True if the user should be shown the icon for Disabled Submitted View</returns>
        public static bool DisabledSubmittedView(StateResult state)
        {
            return DisabledSubmittedViewState.Contains(state);
        }
        #endregion
    }
}