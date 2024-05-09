using System.Collections.Generic;
using FluentValidation;
using Sra.P2rmis.CrossCuttingServices.MessageServices;
using Sra.P2rmis.WebModels.PanelManagement;

namespace Sra.P2rmis.Web.UI.Models
{
    public class RevStatusViewModel
    {
        #region Constants
        public class RevStatusActions
        {
            public const string Abstain = "Abstain";
            public const string MarkCoi = "MarkCoi";
        }
        #endregion
        #region Properties
        /// <summary>
        /// the panel application id
        /// </summary>
        public int PanelAppId { get; set; }
        /// <summary>
        /// the participant's panel user assignment id
        /// </summary>
        public int PanelUserAssignmentId { get; set; }
        /// <summary>
        /// the coi type to assign the specified user
        /// </summary>
        public int? ClientCoiTypeId { get; set; }
        /// <summary>
        /// comment describing the coi
        /// </summary>
        public string CoiComment { get; set; }
        /// <summary>
        /// The action to take on the application for specified reviewer
        /// </summary>
        public string RevStatusAction { get; set; }
        /// <summary>
        /// Dropdown list for COI selection
        /// </summary>
        public List<IClientCoiDropdownList> CoiDropdown { get; set; }
        /// <summary>
        /// SessionPanelId for the current panel
        /// </summary>
        public int SessionPanelId { get; set; }
        /// <summary>
        /// Whether the discussion is complete
        /// </summary>
        public bool ReviewDiscussionComplete { get; set; }

        /// <summary>
        /// FluentValidation Validator for ProfileViewModel
        /// </summary>
        public class RevStatusValidator : AbstractValidator<RevStatusViewModel>
        {
            public RevStatusValidator()
            {
                RuleFor(x => x.ClientCoiTypeId).NotEmpty()
                    .When(x => x.RevStatusAction == RevStatusActions.MarkCoi)
                    .WithMessage(MessageService.Constants.FieldRequired, "COI Type");
                RuleFor(x => x.CoiComment).NotEmpty()
                    .When(x => x.RevStatusAction == RevStatusActions.MarkCoi)
                    .WithMessage(MessageService.Constants.FieldRequired, "Comment");
            }
        }

        #endregion
    }
}