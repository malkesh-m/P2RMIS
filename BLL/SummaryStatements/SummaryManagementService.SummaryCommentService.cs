using System;
using System.Collections.Generic;
using System.Linq;
using Sra.P2rmis.Bll.Views;
using Sra.P2rmis.Dal;
using Sra.P2rmis.WebModels.SummaryStatement;
using Sra.P2rmis.Bll.Views.ApplicationDetails;

namespace Sra.P2rmis.Bll.SummaryStatements
{
    public partial class SummaryManagementService : ISummaryManagementService
    {
        #region Provided Services
        /// <summary>
        /// Retrieves the summary comments for a specified application.
        /// </summary>
        /// <param name="panelApplicationId">Panel application identifier</param>
        /// <returns>Zero or more transactions of an application workflow</returns>
        public Container<ISummaryCommentModel> GetApplicationSummaryComments(int panelApplicationId)
        {
            ValidateInt(panelApplicationId, FullName(nameof(SummaryManagementService), nameof(GetApplicationSummaryComments)), nameof(panelApplicationId));
            ///
            /// Set up default return
            /// 
            Container<ISummaryCommentModel> container = new Container<ISummaryCommentModel>();

            //
            // Call the DL and retrieve the summary comments for this application
            //
            var results = UnitOfWork.SummaryManagementRepository.GetApplicationSummaryComments(panelApplicationId);

            container.SetModelList(results);

            return container;
        }
        /// <summary>
        /// Gets the user application comments.
        /// </summary>
        /// <param name="panelApplicationId">The panel application identifier.</param>
        /// <param name="canAccessAdminNote">if set to <c>true</c> [can access admin note].</param>
        /// <param name="canAccessDiscussionNote">if set to <c>true</c> [can access discussion note].</param>
        /// <param name="canAccessGeneralNote">if set to <c>true</c> [can access general note].</param>
        /// <param name="canAccessUnassignedReviewerNote">if set to <c>true</c> [can access unassigned reviewer note].</param>
        /// <returns></returns>
        public List<IUserApplicationCommentFacts> GetUserApplicationComments(int panelApplicationId, bool canAccessAdminNote, bool canAccessDiscussionNote,
                bool canAccessGeneralNote, bool canAccessUnassignedReviewerNote)
        {
            ValidateInt(panelApplicationId, FullName(nameof(SummaryManagementService), nameof(GetUserApplicationComments)), nameof(panelApplicationId));
            //
            // Call the DL and retrieve the summary comments for this application
            //
            var results = UnitOfWork.ProgramRepository.GetUserApplicationComments(panelApplicationId, canAccessAdminNote, canAccessDiscussionNote,
                canAccessGeneralNote, canAccessUnassignedReviewerNote).ToList()
                .ConvertAll(x => new UserApplicationCommentFacts(x) as IUserApplicationCommentFacts); 

            return results;
        }
        /// <summary>
        /// Adds an application summary comment.
        /// </summary>
        /// <param name="comment">Text of the comment</param>
        /// <param name="applicationId">Identifier for a generic application</param>
        /// <param name="panelApplicationId">PanelApplication entity identifier</param>
        /// <param name="userId">Identifier for a user</param>
        public void AddApplicationSummaryComment(string comment, string applicationId, int panelApplicationId, int userId)
        {
            ValidateString(comment, FullName(nameof(SummaryManagementService), nameof(AddApplicationSummaryComment)), nameof(comment));
            ValidateString(applicationId, FullName(nameof(SummaryManagementService), nameof(AddApplicationSummaryComment)), nameof(applicationId));
            ValidateInt(panelApplicationId, FullName(nameof(SummaryManagementService), nameof(AddApplicationSummaryComment)), nameof(panelApplicationId));
            ValidateInt(userId, FullName(nameof(SummaryManagementService), nameof(AddApplicationSummaryComment)), nameof(userId));

            // create summary note comment 
            UserApplicationComment usrAppComment = new UserApplicationComment();
            usrAppComment.Populate(comment, panelApplicationId, LookupCommentType.SummaryNoteTypeId, userId);
            //setup expect for the add
            UnitOfWork.UserApplicationCommentRepository.Add(usrAppComment);
            UnitOfWork.Save();

        }
        /// <summary>
        /// Edits an application summary comment.
        /// </summary>
        /// <param name="comment">the text of the comment</param>
        /// <param name="userApplicationCommentId">the id of the associated application</param>
        /// <param name="userId">the id of the user adding the comment</param>
        /// <returns></returns>
        public void EditApplicationSummaryComment(string comment, int userApplicationCommentId, int userId)
        {
            UserApplicationComment usrApplicationComment =
                UnitOfWork.UserApplicationCommentRepository.GetByID(userApplicationCommentId);
            if (usrApplicationComment != null)
            {
                // create summary note comment 
                usrApplicationComment.Modify(comment, LookupCommentType.SummaryNoteTypeId, userId);
                //setup expect for the add
                UnitOfWork.UserApplicationCommentRepository.Update(usrApplicationComment);
                UnitOfWork.Save();
            }
            else
            {
                throw new ArgumentException();
            }
        }
        /// <summary>
        /// Deletes an application summary comment.
        /// </summary>
        /// <param name="userApplicationCommentId">the id of the associated application</param>
        /// <param name="userId">the id of the user adding the comment</param>
        /// <returns></returns>
        public void DeleteApplicationSummaryComment(int userApplicationCommentId, int userId)
        {
            UserApplicationComment usrAppComment =
                UnitOfWork.UserApplicationCommentRepository.GetByID(userApplicationCommentId);
            if (usrAppComment != null)
            {
                // create summary note comment 
                usrAppComment.Remove(userId);
                //setup expect for the add
                UnitOfWork.UserApplicationCommentRepository.Delete(usrAppComment);
                UnitOfWork.Save();
            }
            else
            {
                throw new ArgumentException();
            }
        }
        #endregion

        #region Helpers
        #endregion

    }
}
