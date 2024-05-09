using Sra.P2rmis.Bll.Rules.Setup;
using Sra.P2rmis.Dal;
using System.Collections.Generic;

namespace Sra.P2rmis.Bll.Rules
{
    /// <summary>
    /// RuleEngine Creators
    /// </summary>
    internal static class RuleEngineConstructors
    {
        /// <summary>
        /// Create a RuleEngine for the ProgramYear entity
        /// </summary>
        /// <param name="unitOfWork">UnitOfWork object providing access to database</param>
        /// <param name="entity">Entity to apply rule to</param>
        /// <param name="action">CRUD action engine will process</param>
        /// <param name="block">Parameter's sent to ServiceAction</param>
        /// <returns>RuleEngine for the ProgramYear entity</returns>
        public static RuleEngine<ProgramYear> CreateProgramYearEngine(IUnitOfWork unitOfWork, ProgramYear entity, CrudAction action, ICrudBlock block)
        {
            //
            // These are the rules that a ProgramYear entity must obey when any operations are performed
            //
            List<IRuleBase> ruleCollection = new List<IRuleBase>();
            ruleCollection.Add(new AwardsAssignedToProgram(unitOfWork, entity));
            ruleCollection.Add(new DuplicateProgramAbbreviationRule(unitOfWork, entity));
            ruleCollection.Add(new UniqueProgramFiscalYearRule(unitOfWork, entity));
            // 
            // Then we create the engine 
            //
            return new RuleEngine<ProgramYear>(ruleCollection, action, block);
        }
        /// <summary>
        /// Create a RuleEngine for the ProgramYear entity
        /// </summary>
        /// <param name="unitOfWork">UnitOfWork object providing access to database</param>
        /// <param name="entity">Entity to apply rule to</param>
        /// <param name="action">CRUD action engine will process</param>
        /// <param name="block">Parameter's sent to ServiceAction</param>
        /// <returns>RuleEngine for the ProgramYear entity</returns>
        public static RuleEngine<ProgramYear> CheckForLastProgramYear(IUnitOfWork unitOfWork, ProgramYear entity, CrudAction action, ICrudBlock block)
        {
            //
            // These are the rules that a ProgramYear entity must obey when any operations are performed
            //
            List<IRuleBase> ruleCollection = new List<IRuleBase>();
            ruleCollection.Add(new LastProgramCannotBeDeleted(unitOfWork, entity));
            // 
            // Then we create the engine 
            //
            return new RuleEngine<ProgramYear>(ruleCollection, action, block);
        }
        /// <summary>
        /// Create a RuleEngine for the ProgramMechanism entity
        /// </summary>
        /// <param name="unitOfWork">UnitOfWork object providing access to database</param>
        /// <param name="entity">Entity to apply rule to</param>
        /// <param name="action">CRUD action engine will process</param>
        /// <param name="block">Parameter's sent to ServiceAction</param>
        /// <returns>RuleEngine for the ProgramMechanism entity</returns>
        public static RuleEngine<ProgramMechanism> CreateAwardEngine(IUnitOfWork unitOfWork, ProgramMechanism entity, CrudAction action, ICrudBlock block)
        {
            //
            // These are the rules that a ProgramMechanism entity must obey when any operations are performed 
            //
            List<IRuleBase> ruleCollection = new List<IRuleBase>();
            ruleCollection.Add(new AwardWithCriteriaRule(unitOfWork, entity));
            ruleCollection.Add(new AwardWithApplicationRule(unitOfWork, entity));
            ruleCollection.Add(new AwardWithChildAwardApplicationRule(unitOfWork, entity));
            ruleCollection.Add(new DuplicateAwardRule(unitOfWork, entity));
            ruleCollection.Add(new DeleteAwardCannotHavePreAppRule(unitOfWork, entity));
            // 
            // Then we create the engine 
            //
            return new RuleEngine<ProgramMechanism>(ruleCollection, action, block);
        }
        /// <summary>
        /// Create a RuleEngine for the MechanismTemplateElement entity
        /// </summary>
        /// <param name="unitOfWork">UnitOfWork object providing access to database</param>
        /// <param name="entity">Entity to apply rule to</param>
        /// <param name="action">CRUD action engine will process</param>
        /// <param name="block">Parameter's sent to ServiceAction</param>
        /// <returns>RuleEngine for the ProgramMechanism entity</returns>
        public static RuleEngine<MechanismTemplateElement> CreateMechanismTemplateElementEngine(IUnitOfWork unitOfWork, MechanismTemplateElement entity, CrudAction action, ICrudBlock block)
        {
            //
            // These are the rules that a ProgramMechanism entity must obey when any operations are performed 
            //
            List<IRuleBase> ruleCollection = new List<IRuleBase>();
            ruleCollection.Add(new SingleOverallCriterionRule(unitOfWork, entity));
            ruleCollection.Add(new AwardsCriteriaRule(unitOfWork, entity)); 
            ruleCollection.Add(new ReleasedAssignmentsForAwardsRule(unitOfWork, entity));
            // 
            // Then we create the engine 
            //
            return new RuleEngine<MechanismTemplateElement>(ruleCollection, action, block);
        }
        /// <summary>
        /// Create a RuleEngine for the ClientMeeting entity
        /// </summary>
        /// <param name="unitOfWork">UnitOfWork object providing access to database</param>
        /// <param name="entity">Entity to apply rule to</param>
        /// <param name="action">CRUD action engine will process</param>
        /// <param name="block">Parameter's sent to ServiceAction</param>
        /// <returns>RuleEngine for the ClientMeeting entity</returns>
        public static RuleEngine<ClientMeeting> CreateClientMeetingEngine(IUnitOfWork unitOfWork, ClientMeeting entity, CrudAction action, ICrudBlock block)
        {
            //
            // These are the rules that a ProgramMechanism entity must obey when any operations are performed 
            //
            List<IRuleBase> ruleCollection = new List<IRuleBase>();
            ruleCollection.Add(new DuplicateMeetingRule(unitOfWork, entity));
            // 
            // Then we create the engine 
            //
            return new RuleEngine<ClientMeeting>(ruleCollection, action, block);
        }
        /// <summary>
        /// Create a RuleEngine for the ProgramMeeting entity
        /// </summary>
        /// <param name="unitOfWork">UnitOfWork object providing access to database</param>
        /// <param name="entity">Entity to apply rule to</param>
        /// <param name="action">CRUD action engine will process</param>
        /// <param name="block">Parameter's sent to ServiceAction</param>
        /// <returns>RuleEngine for the ProgramMeeting entity</returns>
        public static RuleEngine<ProgramMeeting> CreateProgramMeetingEngine(IUnitOfWork unitOfWork, ProgramMeeting entity, CrudAction action, ICrudBlock block)
        {
            //
            // These are the rules that a ProgramMeeting entity must obey when any operations are performed 
            //
            List<IRuleBase> ruleCollection = new List<IRuleBase>();
            // 
            // Then we create the engine 
            //
            return new RuleEngine<ProgramMeeting>(ruleCollection, action, block);
        }
        /// <summary>
        /// Create a RuleEngine for the MeetingSession entity
        /// </summary>
        /// <param name="unitOfWork">UnitOfWork object providing access to database</param>
        /// <param name="entity">Entity to apply rule to</param>
        /// <param name="action">CRUD action engine will process</param>
        /// <param name="block">Parameter's sent to ServiceAction</param>
        /// <returns>RuleEngine for the MeetingSession entity</returns>
        public static RuleEngine<MeetingSession> CreateMeetingSessionEngine(IUnitOfWork unitOfWork, MeetingSession entity, CrudAction action, ICrudBlock block)
        {
            //
            // These are the rules that a MeetingSession entity must obey when any operations are performed 
            //
            List<IRuleBase> ruleCollection = new List<IRuleBase>();
            ruleCollection.Add(new DuplicateSessionRule(unitOfWork, entity));
            // 
            // Then we create the engine 
            //
            return new RuleEngine<MeetingSession>(ruleCollection, action, block);
        }
        /// <summary>
        /// Create a RuleEngine for the SessionPanel entity
        /// </summary>
        /// <param name="unitOfWork">UnitOfWork object providing access to database</param>
        /// <param name="entity">Entity to apply rule to</param>
        /// <param name="action">CRUD action engine will process</param>
        /// <param name="block">Parameter's sent to ServiceAction</param>
        /// <returns>RuleEngine for the SessionPanel entity</returns>
        public static RuleEngine<SessionPanel> CreateSessionPanelEngine(IUnitOfWork unitOfWork, SessionPanel entity, CrudAction action, ICrudBlock block)
        {
            //
            // These are the rules that a MeetingSession entity must obey when any operations are performed 
            //
            List<IRuleBase> ruleCollection = new List<IRuleBase>();
            ruleCollection.Add(new DuplicatePanelRule(unitOfWork, entity));
            ruleCollection.Add(new ReleasedAssignmentsForPanelRule(unitOfWork, entity));
            // 
            // Then we create the engine 
            //
            return new RuleEngine<SessionPanel>(ruleCollection, action, block);
        }
        /// <summary>
        /// Creates the application workflow summary statement engine.
        /// </summary>
        /// <param name="unitOfWork">The unit of work.</param>
        /// <param name="entity">The entity.</param>
        /// <param name="action">The action.</param>
        /// <param name="block">The block.</param>
        /// <returns></returns>
        public static RuleEngine<ApplicationWorkflowSummaryStatement> CreateApplicationWorkflowSummaryStatementEngine(IUnitOfWork unitOfWork, ApplicationWorkflowSummaryStatement entity, CrudAction action, ICrudBlock block)
        {
            //
            // These are the rules that a MeetingSession entity must obey when any operations are performed 
            //
            List<IRuleBase> ruleCollection = new List<IRuleBase>();
            // 
            // Then we create the engine 
            //
            return new RuleEngine<ApplicationWorkflowSummaryStatement>(ruleCollection, action, block);
        }
        /// <summary>
        /// Creates the application workflow step element content engine.
        /// </summary>
        /// <param name="unitOfWork">The unit of work.</param>
        /// <param name="entity">The entity.</param>
        /// <param name="action">The action.</param>
        /// <param name="block">The block.</param>
        /// <returns></returns>
        public static RuleEngine<ApplicationWorkflowStepElementContent> CreateApplicationWorkflowStepElementContentEngine(IUnitOfWork unitOfWork, ApplicationWorkflowStepElementContent entity, CrudAction action, ICrudBlock block)
        {
            //
            // These are the rules that a MeetingSession entity must obey when any operations are performed 
            //
            List<IRuleBase> ruleCollection = new List<IRuleBase>();
            // 
            // Then we create the engine 
            //
            return new RuleEngine<ApplicationWorkflowStepElementContent>(ruleCollection, action, block);
        }
    }
}
