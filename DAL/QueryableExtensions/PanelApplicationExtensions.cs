using System.Linq;


namespace Sra.P2rmis.Dal
{
    public static class PanelApplicationExtensions
    {
        /// <summary>
        /// Gets the summary workflow.
        /// </summary>
        /// <param name="panApp">The panel application queryable.</param>
        /// <returns>IQueryable representing a single ApplicationWorkflow associated with the applications summary statement workflow</returns>
        public static IQueryable<ApplicationWorkflow> GetSummaryWorkflow(this IQueryable<PanelApplication> panApp)
        {
            return panApp.SelectMany(x => x.ApplicationStages).Where(x => x.ReviewStageId == ReviewStage.Summary).Select(x => x.ApplicationWorkflows.FirstOrDefault());
        }
        /// <summary>
        /// Gets the application templates.
        /// </summary>
        /// <param name="panApp">The pan application.</param>
        /// <returns></returns>
        public static IQueryable<ApplicationTemplate> GetApplicationTemplates(this IQueryable<PanelApplication> panApp)
        {
            return panApp.SelectMany(x => x.ApplicationStages).SelectMany(y => y.ApplicationTemplates);
        }
    }
}
