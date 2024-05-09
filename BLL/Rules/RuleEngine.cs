using System.Collections.Generic;
using System.Linq;

namespace Sra.P2rmis.Bll.Rules
{
    /// <summary>
    /// Simple rule engine to execute Rules classes
    /// </summary>
    internal class RuleEngine<Tentity> where Tentity : class
    {
        #region Construction & Setup
        /// <summary>
        /// Default constructor.
        /// </summary>
        public RuleEngine() { }
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="ruleCollection">The rule collection to execute</param>
        /// <param name="action">Action to run the engine for</param>
        /// <param name="block">Parameter block</param>
        public RuleEngine(List<IRuleBase> ruleCollection, CrudAction action, ICrudBlock block)
        {
            this.RuleCollection = ruleCollection;
            this.Action = action;
            this.Block = block;
        }
        #endregion
        #region Atttributes
        /// <summary>
        /// Count of rules applies
        /// </summary>
        internal int RulesApplied { get; private set; }
        /// <summary>
        /// Action (i.e. Add, Modify, Delete) to run the engine for.
        /// </summary>
        internal CrudAction Action { get; private set; } = CrudAction.Default;
        /// <summary>
        /// Messages return from rule application.
        /// </summary>
        public IList<string> Messages { get; private set; } = new List<string>();
        /// <summary>
        /// The engine state after rules have been applied.
        /// </summary>
        public bool IsBroken { get; private set; }
        /// <summary>
        /// The rules to apply.
        /// </summary>
        private IList<IRuleBase> RuleCollection { get; set; } = new List<IRuleBase>();
        /// <summary>
        /// Crud parameter block to pass to rules
        /// </summary>
        public ICrudBlock Block { get; set; }
        #endregion
        #region Services
        /// <summary>
        /// Run the engine
        /// </summary>
        public void Apply()
        {
            //
            // For each of the rules in the rule engine collection
            // apply them and determine if the engine is now broken.
            //
            foreach(var rule in RuleCollection.Where(x => x.Actions.Contains(Action)))
            {
                rule.Apply(this.Block);
                IsBroken |= rule.IsBroken;
                if (rule.IsBroken)
                {
                    this.Messages.Add(rule.Message);
                }
                this.RulesApplied++;
            }
        }
        #endregion
    }
}
