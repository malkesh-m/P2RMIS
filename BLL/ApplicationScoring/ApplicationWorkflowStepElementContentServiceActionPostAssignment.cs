using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sra.P2rmis.Dal;

namespace Sra.P2rmis.Bll.ApplicationScoring
{
    /// <summary>
    /// Service Action method to Add/Modify/Delete ApplicationWorkflowStepElementContent represented
    /// by WebModel CritiqueContent
    /// </summary>
    public class ApplicationWorkflowStepElementContentServiceActionPostAssignment : ServiceAction<ApplicationWorkflowStepElementContent>
    {
        #region Constructor & set up
        /// <summary>
        /// Constructor
        /// </summary>
        public ApplicationWorkflowStepElementContentServiceActionPostAssignment()
        {
        }

        public void Populate(IGenericRepository<ApplicationWorkflowStepElement> applicationWorkflowStepElementRepository, decimal? score, bool abstain, int applicationWorkflowStepElementId)
        {
            this.Score = score;
            this.Abstain = abstain;
            this.ApplicationWorkflowStepElementId = applicationWorkflowStepElementId;
            this.ApplicationWorkflowStepElementRepository = applicationWorkflowStepElementRepository;
        }
        /// <summary>
        /// Populate the service action specific properties
        /// </summary>
        /// <param name="applicationWorkflowStepElementRepository">ApplicationWorkflowStepElement repository</param>
        /// <param name="critiqueText">Reviewers Critique text</param>
        /// <param name="score">Reviewer's score</param>
        /// <param name="abstain">Indicates if the user abstained from scoring</param>
        public void Populate(IGenericRepository<ApplicationWorkflowStepElement> applicationWorkflowStepElementRepository,  string critiqueText, decimal? score, bool abstain, int applicationWorkflowStepElementId)            
        {
            this.CritiqueText = critiqueText;
            this.Score = score;
            this.Abstain = abstain;
            this.ApplicationWorkflowStepElementId = applicationWorkflowStepElementId;
            this.ApplicationWorkflowStepElementRepository = applicationWorkflowStepElementRepository;
        } 
        /// <summary>
        /// Populate the service action specific properties
        /// </summary>
        /// <param name="applicationWorkflowStepElementRepository">ApplicationWorkflowStepElement repository</param>
        /// <param name="critiqueText">Reviewers Critique text</param>
        /// <param name="score">Reviewer's score</param>
        /// <param name="isRevised">Indicates if the score should be revised when updated</param>
        /// <param name="abstain">Indicates if the user abstained from scoring</param>
        public void Populate(IGenericRepository<ApplicationWorkflowStepElement> applicationWorkflowStepElementRepository, string critiqueText, decimal? score, bool abstain, int applicationWorkflowStepElementId, bool isRevised)
        {
            Populate(applicationWorkflowStepElementRepository, critiqueText,  score, abstain, applicationWorkflowStepElementId);
            this.IsRevised = isRevised;
        }
        #endregion
        #region Properties
        /// <summary>
        /// Critique text
        /// </summary>
        public string CritiqueText {get; private set;}
        /// <summary>
        /// Reviewer's score
        /// </summary>
        public decimal? Score {get; private set;}
        /// <summary>
        /// Indicates if the user abstained from scoring
        /// </summary>
        public bool Abstain {get; private set;}
        /// <summary>
        /// Parent ApplicationWorkflowStepElement.  Necessary for add.
        /// </summary>
        public int ApplicationWorkflowStepElementId { get; private set; }
        /// <summary>
        /// ApplicationWorkflowStepElement repository
        /// </summary>
        protected IGenericRepository<ApplicationWorkflowStepElement> ApplicationWorkflowStepElementRepository { get; set; }
        /// <summary>
        /// Indicates if the critique state should be set to a revised state when setting the criteria text.
        /// </summary>
        protected bool IsRevised { get; set; } = true;
        #endregion
        #region Required Overrides
        /// <summary>
        /// Populate the ApplicationWorkflowStepElementContent entity with information from the model.
        /// </summary>
        /// <param name="entity">ApplicationWorkflowStepElementContent entity</param>
        protected override void Populate(ApplicationWorkflowStepElementContent entity)
        {
            entity.Populate(ApplicationWorkflowStepElementId, CritiqueText, Score, Abstain, IsRevised);
        }
        /// <summary>
        /// Indicates if the ApplicationWorkflowStepElementContent has data.
        /// </summary>
        protected override bool HasData
        {
            get
            {
                //
                // By definition we are expecting that it will have data.  Even
                // null or the empty string should be stored.
                //
                return true;
            }
        }
        #endregion
    }
}
