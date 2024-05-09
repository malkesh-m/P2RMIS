namespace Sra.P2rmis.Web.ViewModels
{
    /// <summary>
    /// Implements rules determining enabling & disabling of the on line discussion icon
    /// </summary>
    public static class OnLineDiscussions
    {
        /// <summary>
        /// Determines if the discussion board exists.
        /// </summary>
        /// <param name="isAssigned">Indicates if the user is assigned to the application</param>
        /// <param name="isDiscussionBoardActive">Indicates if the discussion board is active</param>
        /// <param name="isDiscussionBoardClosed">Indicates if the discussion board is closed</param>
        /// <param name="hasDiscussionBoardPhase">Indicates if this phase of the workflow can have a discussion board</param>
        /// <returns>True if the user can access the discussion board; false otherwise</returns>
        public static bool AccessDiscussionBoard(bool isAssigned, bool isDiscussionBoardActive, bool isDiscussionBoardClosed, bool hasDiscussionBoardPhase)
        {
            return isAssigned && hasDiscussionBoardPhase && (isDiscussionBoardActive || isDiscussionBoardClosed);
        }
        /// <summary>
        /// Accesses the discussion board.
        /// </summary>
        /// <param name="isAssigned">if set to <c>true</c> [is assigned].</param>
        /// <param name="isDiscussionBoardActive">if set to <c>true</c> [is discussion board active].</param>
        /// <param name="isDiscussionBoardClosed">if set to <c>true</c> [is discussion board closed].</param>
        /// <param name="isDiscussionBoardReady">if set to <c>true</c> [is discussion board ready].</param>
        /// <param name="hasDiscussionBoardPhase">if set to <c>true</c> [has discussion board phase].</param>
        /// <returns></returns>
        public static bool AccessDiscussionBoard(bool isAssigned, bool isDiscussionBoardActive, bool isDiscussionBoardClosed, bool isDiscussionBoardReady, bool hasDiscussionBoardPhase)
        {
            return isAssigned && hasDiscussionBoardPhase && (isDiscussionBoardActive || (isDiscussionBoardClosed && isDiscussionBoardReady));
        }
        /// <summary>
        /// Determines if the discussion icon should be displayed.
        /// </summary>
        /// <param name="isChairperson">Indicates if the reviewer is a chairperson</param>
        /// <param name="isAssigned">Indicates if the user is assigned to the application</param>
        /// <param name="isThereADiscussion">Indicates if a discussion board exists with 1 or more comments</param>
        /// <returns>True if the user can see the discussion board icon; false otherwise</returns>
        public static bool ShowDiscussionBoard(bool isChairperson, bool isAssigned, bool isThereADiscussion)
        {
            return ((isAssigned || isChairperson) && isThereADiscussion);
        }
    }
}