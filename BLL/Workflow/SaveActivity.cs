using System;
using System.Activities;
using System.Collections;
using Sra.P2rmis.Dal;

namespace Sra.P2rmis.Bll.Workflow
{
    /// <summary>
    /// Save activity
    /// </summary>
    public class SaveActivity : P2rmisActivity 
    {
        #region Constants
        private const string NotCheckedoutMessage = "Save was not accomplished.  User [{0}] did not have workflow step [{1}] Checked Out.";
        private const string ErrorMessage = "SaveActivity detected invalid arguments: step is null [{0}]; userId [{1}]; unit of work is null [{2}];  content is null [{3}]; contentId [{4}]";
        private const string ParameterErrorMessage = "SetParameters detected invalid arguments: list is null [{0}] list entries count [{1}]";
        /// <summary>
        /// Number of activity specific arguments.  Used to size the hashset.
        /// </summary>
        public static readonly int ActivityArgumentCount = 3;
        #endregion
        #region Classes
        //
        // Identify the activity specific parameters
        //
        public enum SaveParameters
        {
            Default = 0,
            //
            // This is the ApplicationWorkflowStepElementContent value
            //
            Content,
            //
            // This is the ApplicationWorkflowStepElementContentId value
            //
            ContentId,
            //
            // This is the ApplicationWorkflowStepElementId value
            //
            ElementId
        }
        #endregion
        #region Properties
        /// <summary>
        /// context to save
        /// </summary>
        public InArgument<string> Content { get; set; }
        /// <summary>
        /// ApplicationWorkflowStepElementContent identifier
        /// </summary>
        public InArgument<int> ContentId { get; set; }
        /// <summary>
        /// ApplicationWorkflowStepElementContent identifier
        /// </summary>
        public InArgument<int> ElementId { get; set; }
        #endregion
        #region Constructor & Setup
        /// <summary>
        /// Constructor
        /// </summary>
        public SaveActivity() : base() { }
        /// <summary>
        /// Initializes any Activity specific parameters.
        /// </summary>
        /// <param name="values">List of activity specific parameters</param>
        public override void SetParameters(IDictionary values)
        {
            //
            // just try setting the parameters.  If anything is wrong throw
            // and exception.
            //
            try
            {
                this.Content = values[SaveParameters.Content] as string;
                this.ContentId = (int)values[SaveParameters.ContentId];
                this.ElementId = (int)values[SaveParameters.ElementId];
            }
            catch
            {
                string message = string.Format(ParameterErrorMessage, (values == null), ((values != null) ? values.Count.ToString() : string.Empty));
                throw new ArgumentException(message);
            }
        }
        #endregion
        #region Business Rule Execution
        /// <summary>
        /// Executes the Save specific business rules.
        /// </summary>
        /// <param name="context">Workflow Activity Context object</param>
        protected override void Execute(CodeActivityContext context)
        {
            //
            // Get the parameters
            //
            ApplicationWorkflowStep step = this.WorkflowStep.Get(context);
            int userId = this.UserId.Get(context);
            IUnitOfWork unitOfWork = this.UnitOfWork.Get(context);
            string content = this.Content.Get(context);
            int contentId = this.ContentId.Get(context);
            int stepElementId = this.ElementId.Get(context);

            if (IsSaveActivityParametersValid(step, userId, unitOfWork, contentId, stepElementId))
            {
                ModifyContent(step, unitOfWork, content, contentId, userId, stepElementId);
                //
                // This is the state machine's state now.
                //
                this.OutState.Set(context, WorkflowState.Default);
            }
            else
            {
                String message = string.Format(ErrorMessage, (step == null), userId, (unitOfWork == null), (content == null), contentId );
                throw new ArgumentException(message);
            }
            return;
        }
        /// <summary>
        /// Modifies or adds content to the ApplicationWorkflowStep.
        /// </summary>
        /// <param name="step">Workflow step</param>
        /// <param name="unitOfWork">Unit of work</param>
        /// <param name="content">Content to update or add</param>
        /// <param name="contentId">Content identifier</param>
        /// <param name="userId">User identifier</param>
        /// <param name="stepElementId">Step element content identifier</param>
        private void ModifyContent(ApplicationWorkflowStep step, IUnitOfWork unitOfWork, string content, int contentId, int userId, int stepElementId)
        {
            //
            // Check that this user has the content 'checked-out'
            //
            if (step.IsCheckedOutByUser(userId))
            {
                //
                // If a contentId is supplied then we actually have an existing content to replace.  
                //
                if ((contentId > 0) && (!string.IsNullOrWhiteSpace(content)))
                {
                    ApplicationWorkflowStepElementContent theContent = unitOfWork.ApplicationWorkflowStepElementContentRepository.GetByID(contentId);
                    theContent.SaveModifiedContent(content, userId);
                }
                //
                // Well content exists and the user is trying to save nothing.  Basically
                // trying to delete what is there.
                //
                else if ((contentId > 0) && (string.IsNullOrWhiteSpace(content)))
                {
                    unitOfWork.ApplicationWorkflowStepElementContentRepository.Delete(contentId);
                }
                //
                // Well we may or may not have an existing content entry for this stepElemenId.
                // Try to locate it in the element.  If none is found then create one.  Otherwise
                // update the content of the returned ApplicationWorkflowStepElementContent.
                //
                else if ((contentId == 0) && (!string.IsNullOrWhiteSpace(content)))
                {
                    ApplicationWorkflowStepElement theElement = step.GetApplicationWorkflowStepElementById(stepElementId);
                    ApplicationWorkflowStepElementContent theContent = theElement.GetStepElementContentById(stepElementId);
                    if (theContent == null)
                    {
                        theContent = new ApplicationWorkflowStepElementContent();
                        theContent.Populate(stepElementId, content, userId);
                        theElement.ApplicationWorkflowStepElementContents.Add(theContent);
                    }
                    else
                    {
                        theContent.SaveModifiedContent(content, userId);
                    }
                }
                //
                // Finally the case where the user is trying to save nothing and there was nothing
                // there.  In which case one does not have to do anything.
                //
                else if ((contentId == 0) && (string.IsNullOrWhiteSpace(content)))
                {
                }
            }
            else
            {
                String message = string.Format(NotCheckedoutMessage, userId, step.StepName);
                throw new ArgumentException(message);
            }

        }
        #endregion
        #region Helpers
        /// <summary>
        /// Verify that the parameters for the save activity are valid
        ///    - the standard parameters are valid
        ///    - ApplicationWorkflowStepElementContentId > 0
        /// </summary>
        /// <param name="workflowStepId">Workflow identifier</param>
        /// <param name="userId">User identifier</param>
        /// <param name="unitOfWork">Unit of work provides access to the entity framework</param>
        /// <param name="content">Content value to save</param>
        /// <param name="contentId">Content identifier</param>
        /// <param name="elementId">Step Element identifier</param>
        /// <returns>True if parameters are valid; false otherwise</returns>
        private bool IsSaveActivityParametersValid(ApplicationWorkflowStep step, int userId, IUnitOfWork unitOfWork, int contentId, int elementId)
        {
            return (
                    (IsActivityParametersValid(step, userId, unitOfWork)) &&
                    (contentId >= 0) &&
                    (elementId > 0)
                    );
        }
        #endregion
    }
}
