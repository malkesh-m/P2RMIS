using System;
using System.Collections.Generic;
using System.Linq;
using Sra.P2rmis.Dal.Interfaces;
using Sra.P2rmis.WebModels.ApplicationScoring;
using Sra.P2rmis.CrossCuttingServices;

namespace Sra.P2rmis.Dal
{
    /// <summary>
    /// Custom methods for Entity Framework's ApplicationWorkflowStep object.
    /// </summary>
    public partial class ApplicationWorkflowStep: IStandardDateFields
    {
        #region Constants
        /// <summary>
        /// Indicates that there is no next WorkflowStep
        /// </summary>
        public const int CompleteWorkflow = 0;
        /// <summary>
        /// Imaginary step that is at the end of Summary Statement workflows.
        /// </summary>
        public const string CompleteWorkflowStepName = "Complete";
        #endregion
        /// <summary>
        /// Updates the Resolution field and the associated date field.
        /// </summary>
        /// <param name="resolution">Indicates if the step is complete</param>
        public void SetResolution(bool resolution, int userId)
        {
            if (resolution)
            {
                this.Resolution = resolution;
                this.ResolutionDate = GlobalProperties.P2rmisDateTimeNow;
                Helper.UpdateModifiedFields(this, userId);
            }
        }
        /// <summary>
        /// Updates the Resolution field and the associated date field.
        /// </summary>
        public void ResetResolution()
        {
            this.Resolution = false;
            this.ResolutionDate = null;
            this.Active = true;
        }
        /// <summary>
        /// Promote an ApplicationWorkflowStep content.
        /// </summary>
        /// <param name="step">ApplicationWorkflowStep entity object</param>
        /// <param name="userId">User identifier</param>
        /// <returns>List of ApplicationWorkflowStepElementContent entity objects to be added</returns>
        public List<ApplicationWorkflowStepElementContent> Promote(ApplicationWorkflowStep nextStep, int userId)
        {
            //
            // Create the container to hold the individual promoted contents and then for each 
            // ApplicationWorkflowStepElement in this step element promote it.
            //
            var shouldRemoveMarkup = nextStep.ShouldRemoveMarkup();
            List<ApplicationWorkflowStepElementContent> results = new List<ApplicationWorkflowStepElementContent>();

            foreach (ApplicationWorkflowStepElement stepElement in ApplicationWorkflowStepElements)
            {
                ApplicationWorkflowStepElement stepElementInNextStep = nextStep.MatchStepElement(stepElement);
                var promoted = stepElement.Promote(stepElementInNextStep.ApplicationWorkflowStepElementId, userId, shouldRemoveMarkup);
                results.AddRange(promoted);
            }
            return results;
        }
        /// <summary>
        /// Retrieve a list of this steps content.
        /// </summary>
        /// <param name="userId">User identifier</param>
        /// <returns>List of ApplicationWorkflowStepElementContent entity objects</returns>
        public virtual List<ApplicationWorkflowStepElementContent> GetContentList(int userId)
        {
            return Promote(this, userId);
        }
        /// <summary>
        /// Locates the matching ApplicationWorkflowStepElement.  During the promotion process, new content
        /// entities are created.  The objects need an index into the ApplicationWorkflowStepElement that they
        /// will be attached to. The locates the correct ApplicationWorkflowStepElement in the Next Workflow
        /// Step.
        /// </summary>
        /// <param name="stepElement">ApplicationWorkflowStepElement to match</param>
        /// <returns>Matching ApplicationWorkflowStepElement</returns>
        public virtual ApplicationWorkflowStepElement MatchStepElement(ApplicationWorkflowStepElement stepElement)
        {
            return this.ApplicationWorkflowStepElements.FirstOrDefault(x => (x.ApplicationTemplateElementId == stepElement.ApplicationTemplateElementId));        
        }
        /// <summary>
        /// Check if the user has something checked-out for the current step.
        /// </summary>
        /// <param name="userId">User identifier</param>
        /// <returns>True if the current step is checked out; false otherwise</returns>
        public virtual bool IsCheckedOutByUser(int userId)
        {
            var result = this.ApplicationWorkflowStepWorkLogs.FirstOrDefault(x => (x.UserId == userId) && (x.ApplicationWorkflowStepId == this.ApplicationWorkflowStepId) && (x.CheckInDate == null));
            return (result != null);
        }
        /// <summary>
        /// Copies (updates or creates if not there) the content from one workflow step to another.
        /// </summary>
        /// <param name="sourceStep">Source Workflow step containing context to copy</param>
        /// <param name="userId">User identifier</param>
        public virtual void CopyContentFromOtherWorkflowStep(ApplicationWorkflowStep sourceStep, int userId)
        {
            foreach(var sourceStepElement in sourceStep.ApplicationWorkflowStepElements)
            {
                ApplicationWorkflowStepElement targetSourceStepElement = LocateMatchingStepElementByTemplateId(sourceStepElement.ApplicationTemplateElementId);
                //
                // Note:
                // The entity object defines the context as a collection.  However it should only be a single entry.
                //
                if (sourceStepElement.ApplicationWorkflowStepElementContents.Count > 0)
                {
                    ApplicationWorkflowStepElementContent sourceContent = sourceStepElement.ApplicationWorkflowStepElementContents.ElementAt(0);
                    ApplicationWorkflowStepElementContent targetContent = targetSourceStepElement.ApplicationWorkflowStepElementContents.ElementAtOrDefault(0);
                    if (targetContent != null)
                    {
                        //
                        // In this case both the source & target content element entity object exists.  So all we need
                        // to do is copy the text from one entity to the other.
                        //
                        targetContent.SaveModifiedContent(sourceContent.ContentText, userId);
                    }
                    else
                    {
                        //
                        // However in this case the target does not have a context element object.  An Example of this would be discussion
                        // notes.  So we need to create a new context entity object & populate the object with the contents of the 
                        // source text.  Same note applies to this context element concerning accessing the value.
                        //
                        ApplicationWorkflowStepElementContent newContent = new ApplicationWorkflowStepElementContent();
                        string content = sourceStepElement.ApplicationWorkflowStepElementContents.ElementAt(0).ContentText;
                        newContent.Populate(sourceStepElement.ApplicationWorkflowStepElementId, content, userId);
                        targetSourceStepElement.ApplicationWorkflowStepElementContents.Add(newContent);
                    }
                }
            }
        }
        /// <summary>
        /// Each Workflow Step Element is tied to a specific template element (identified by id).  When assigning
        /// (i.e. rollback) you need to locate the corresponding step element in the target workflow step.  This is
        /// done by matching template ids because they are an implementation of the same template item.
        /// </summary>
        /// <param name="templateElementIdToMatch">Target template identifier</param>
        /// <returns>Matching StepElement object or null if none exists</returns>
        public virtual ApplicationWorkflowStepElement LocateMatchingStepElementByTemplateId(int templateElementIdToMatch)
        {
            return this.ApplicationWorkflowStepElements.FirstOrDefault(x => x.ApplicationTemplateElementId == templateElementIdToMatch);
        }
        /// <summary>
        /// Constructs multiple ApplicationWorkflowStepElementContentHistory object for each
        /// ApplicationWorkflowStepElement object.
        /// </summary>
        /// <param name="userId">User identifier</param>
        /// <param name="worklogId">Worklog identifier</param>
        /// <returns>Collection of ApplicationWorkflowStepElementContentHistory objects<returns>
        public virtual ICollection<ApplicationWorkflowStepElementContentHistory> CreateHistory(int userId, int worklogId)
        {
            List<ApplicationWorkflowStepElementContentHistory> result = new List<ApplicationWorkflowStepElementContentHistory>();
            foreach (var item in this.ApplicationWorkflowStepElements)
            {
                ICollection<ApplicationWorkflowStepElementContentHistory> oneElementResult = item.CreateHistory(userId, worklogId);
                result.AddRange(oneElementResult);
            }
            return result;
        }
        /// <summary>
        /// Locates the ApplicationWorkflowStepElement in this workflow step with the matching ApplicationWorkflowStepElementId
        /// </summary>
        /// <param name="elementId">ApplicationWorkflowStepElement identifier</param>
        /// <returns>ApplicationWorkflowStepElement matching the id</returns>
        public virtual ApplicationWorkflowStepElement GetApplicationWorkflowStepElementById(int elementId)
        {
            return this.ApplicationWorkflowStepElements.FirstOrDefault(x => x.ApplicationWorkflowStepElementId == elementId);
        }
        /// <summary>
        /// Indicates if any content exists for this workflow step.
        /// </summary>
        /// <returns>True if content exists; false otherwise</returns>
        public bool HasContent()
        {
            return (this.ApplicationWorkflowStepElements.SelectMany(t => t.ApplicationWorkflowStepElementContents).Count() != 0);
        }
        /// <summary>
        /// Indicates if the critique can be submitted
        /// Has not been submitted and has content.
        /// </summary>
        /// <returns>True if critique can be submitted; false otherwise</returns>
        public bool IsCritiqueSubmittable()
        {
            return (!this.Resolution && HasContent());
        }
        /// <summary>
        /// Indicates whether review markup (track changes/comments) should be removed from step content after promotion to the step
        /// </summary>
        /// <returns>True if markup should be removed; false otherwise</returns>
        public virtual bool ShouldRemoveMarkup()
        {
            return (this.StepType.CleanMarkupFlag);
        }
        /// <summary>
        /// Gets the element content that represents an overall evaluation of an application.
        /// </summary>
        /// <returns>ApplicationWorkflowStepElement</returns>
        public ApplicationWorkflowStepElementContent GetOverallElementContent()
        {
            return
                this.ApplicationWorkflowStepElements.Where(
                    x => x.ApplicationTemplateElement.MechanismTemplateElement.OverallFlag)
                    .DefaultIfEmpty(new ApplicationWorkflowStepElement())
                    .First()
                    .ApplicationWorkflowStepElementContents.DefaultIfEmpty(new ApplicationWorkflowStepElementContent())
                    .First();
        }
        /// <summary>
        /// Indicates if the workflow step is the initial workflow step.
        /// </summary>
        /// <returns>True if the workflow is the initial step; false otherwise</returns>
        public bool IsFirstWorkflowStep()
        {
            return (this.StepOrder == 1);
        }
        /// <summary>
        /// Activates a client review workflow step.
        /// </summary>
        /// <param name="state">New Active state value</param>
        /// <param name="currentStepStepOrder">StepOrder value of the current workflow step</param>
        /// <param name="userId">User entity identifier</param>
        public void ActivateAClientReviweWorkflowStep(bool state, int currentStepStepOrder, int userId)
        {
            //
            // We make all steps active
            //
            this.Active = state;
            //
            // However if this workflow step is in the past (i.e. the current step is after it)
            // we mark it as resolved.
            //
            if (this.StepOrder <= currentStepStepOrder)
            {
                SetResolution(true, userId);
            }
            //
            // But if it is a future step we reset the resolution.
            else
            {
                this.UnResolve();
            }
        }
        /// <summary>
        /// Indicates if the step is a client review step.
        /// </summary>
        /// <returns>True if this step is a client review step</returns>
        public bool IsClientReviewStep()
        {
            return (this.StepTypeId == StepType.Indexes.Review);
        }
        /// <summary>
        /// Determines the following workflow step.  We override the default because the next step does not have to be a 
        /// active if it is a MOD step (stepTypeId = 7).
        /// </summary>
        /// <returns>Next ApplicationWorkflowStep entity if one exist; null otherwise</returns>
        public ApplicationWorkflowStep GetNextMODStep()
        {
            //
            // First we get the workflow from the step
            //
            return this.ApplicationWorkflow.
                //
                // then we get the workflow steps & order them by the StepOrder
                //
                ApplicationWorkflowSteps.OrderBy(x => x.StepOrder).
                //
                // Then we want the next step and it ...
                //  - can be active          or
                //  - can be inactive or be a MOD step
                //
                FirstOrDefault(x =>
                                    (x.StepOrder > this.StepOrder) &
                                    ((x.Active) || (x.StepTypeId == StepType.Indexes.Final))
                              );
        }
        /// <summary>
        /// Reopen the workflow step for MOD.
        /// </summary>
        /// <param name="userId">User entity identifier of user activating the MOD session</param>
        public void ReActivate(int userId)
        {
            //
            // To activate the workflow step for MOD we:
            //  - set the workflow step's active status to true
            //  - clear the resolution's state & Date/Time to enable the workflow step
            //  - update the workflow modification Date/Time & user making the update
            this.Active = true;
            ResetResolution();
            Helper.UpdateModifiedFields(this, userId);
        }
        /// <summary>
        /// Un resolve the workflow step.
        /// </summary>
        public void UnResolve()
        {
            this.Resolution = false;
            this.ResolutionDate = null;
        }
        /// <summary>
        /// Itemizes the Critique criteria that do not have scores (if they are required)
        /// </summary>
        /// <returns>List of IncompleteCriteriaNameModel indicating the criteria that do not have scores</returns>
        public List<IIncompleteCriteriaNameModel> CanSubmit()
        {
            List<IIncompleteCriteriaNameModel> result = new List<IIncompleteCriteriaNameModel>();
            ApplicationWorkflowStepElement applicationWorkflowStepElementEntity = this.ApplicationWorkflowStepElements.FirstOrDefault(); 
            //
            //
            //
            if (!applicationWorkflowStepElementEntity.IsResolved())
            {
                //
                // Get the workflow we are talking about
                //
                //ApplicationWorkflow applicationWorkflowEntity = ApplicationWorkflowStep.ApplicationWorkflow;
                //
                // Now we find all the workflow steps 
                //
                List<ApplicationWorkflowStepElement> ApplicationWorkflowStepElementList = this.ApplicationWorkflowStepElements.ToList();
                ApplicationWorkflowStepElementList.ForEach(x =>
                {
                    //
                    // Now ask each ApplicationWorkflowStepElement if it has data.  Some elements may require text or a score
                    // entry.  Others may require both.  The determination is done within the object.
                    //
                    if (!x.HasData())
                    {
                        result.Add(new IncompleteCriteriaNameModel(x.GetCriteriaElementDescription(), x.ApplicationWorkflowStepElementId));
                    }
                });
            }

            return result;
        }

        /// <summary>
        /// Gets the current document associated with the workflow step.
        /// </summary>
        /// <returns>byte representation of document data</returns>
        public byte[] GetCurrentDocument()
        {
            return this.ApplicationWorkflow.ApplicationWorkflowSummaryStatements.FirstOrDefault()?.DocumentFile;
        }
    }
}
 