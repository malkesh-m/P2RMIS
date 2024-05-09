using Sra.P2rmis.CrossCuttingServices.MessageServices;
using Sra.P2rmis.WebModels.Setup;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sra.P2rmis.Dal
{
    internal partial class RepositoryHelpers
    {
        /// <summary>
        /// Gets the referral mapping.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        internal static IEnumerable<ReferralMappingModel> GetReferralMapping(P2RMISNETEntities context, int id)
        {
            var result = from ReferralMappingData in context.ReferralMappingDatas
                         join SessionPanel in context.SessionPanels on ReferralMappingData.SessionPanelId equals SessionPanel.SessionPanelId
                         join Application in context.Applications on ReferralMappingData.ApplicationId equals Application.ApplicationId
                         join PanelApplication in context.PanelApplications on Application.ApplicationId equals PanelApplication.ApplicationId into pa
                         from PanelApplication in pa.DefaultIfEmpty()
                         join ApplicationCompliance in context.ApplicationCompliances on ReferralMappingData.ApplicationId equals ApplicationCompliance.ApplicationId into ac
                         from ApplicationCompliance in ac.DefaultIfEmpty()
                         join ComplianceStatus in context.ComplianceStatus on ApplicationCompliance.ComplianceStatusId equals ComplianceStatus.ComplianceStatusId into cs
                         from ComplianceStatus in cs.DefaultIfEmpty()
                         where ReferralMappingData.ReferralMappingId == id
                         select new ReferralMappingModel
                         {
                             ReferralMappingId = id,
                             PanelName = SessionPanel.PanelName,
                             SessionPanelId = SessionPanel.SessionPanelId,
                             WithdrawalStatus = Application.WithdrawnFlag,
                             NonCompliant = ComplianceStatus.ComplianceStatusId,
                             PanelApplicationId = PanelApplication.PanelApplicationId,
                             AssignedToPanelId = PanelApplication.SessionPanelId
                         };
            return result;
        }
        /// <summary>
        /// Gets the existing application.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <param name="programYearId">The program year identifier.</param>
        /// <param name="receiptCycle">The receipt cycle.</param>
        /// <returns></returns>
        internal static IEnumerable<ReferralMappingModel> GetExistingApplication(P2RMISNETEntities context, int programYearId, int receiptCycle)
        {
            var r1 = (from ClientProgram in context.ClientPrograms
                      join ProgramYear in context.ProgramYears on ClientProgram.ClientProgramId equals ProgramYear.ClientProgramId
                      join ProgramMechanism in context.ProgramMechanism on ProgramYear.ProgramYearId equals ProgramMechanism.ProgramYearId
                      join Application in context.Applications on ProgramMechanism.ProgramMechanismId equals Application.ProgramMechanismId
                      join rm in context.ReferralMappings
                      on new { ProgramYear.ProgramYearId, ProgramMechanism.ReceiptCycle }
                      equals new { rm.ProgramYearId, ReceiptCycle = (int?)rm.ReceiptCycle }
                      join rmd in context.ReferralMappingDatas
                      on new { rm.ReferralMappingId, Application.ApplicationId }
                      equals new { rmd.ReferralMappingId, rmd.ApplicationId }
                      join SessionPanel in context.SessionPanels on rmd.SessionPanelId equals SessionPanel.SessionPanelId
                      join ApplicationCompliance in context.ApplicationCompliances on Application.ApplicationId equals ApplicationCompliance.ApplicationId
                      join ComplianceStatus in context.ComplianceStatus on ApplicationCompliance.ComplianceStatusId equals ComplianceStatus.ComplianceStatusId
                      join PanelApplication in context.PanelApplications on
                      Application.ApplicationId equals PanelApplication.ApplicationId into pa2
                      from pa in pa2.DefaultIfEmpty()  
                      where ProgramYear.ProgramYearId == programYearId && ProgramMechanism.ReceiptCycle == receiptCycle
                      select new ReferralMappingModel
                      {
                          ApplicationId = Application.ApplicationId,
                          PanelName = SessionPanel.PanelName,
                          SessionPanelId = SessionPanel.SessionPanelId,
                          WithdrawalStatus = Application.WithdrawnFlag,
                          NonCompliant = ComplianceStatus.ComplianceStatusId,
                          PanelApplicationId = pa.PanelApplicationId,
                          AssignedToPanelId = pa.SessionPanelId,
                          ReferralMappingId = rm.ReferralMappingId
                      });
            var r2 = (from ClientProgram in context.ClientPrograms
                           join ProgramYear in context.ProgramYears on ClientProgram.ClientProgramId equals ProgramYear.ClientProgramId
                           join ProgramMechanism in context.ProgramMechanism on ProgramYear.ProgramYearId equals ProgramMechanism.ProgramYearId
                           join Application in context.Applications on ProgramMechanism.ProgramMechanismId equals Application.ProgramMechanismId                         
                           join PanelApplication in context.PanelApplications on
                           Application.ApplicationId equals PanelApplication.ApplicationId
                           join SessionPanel in context.SessionPanels on PanelApplication.SessionPanelId equals SessionPanel.SessionPanelId
                           join ApplicationCompliance in context.ApplicationCompliances on Application.ApplicationId equals ApplicationCompliance.ApplicationId
                           join ComplianceStatus in context.ComplianceStatus on ApplicationCompliance.ComplianceStatusId equals ComplianceStatus.ComplianceStatusId
                           where ProgramYear.ProgramYearId == programYearId && ProgramMechanism.ReceiptCycle == receiptCycle
                           select new ReferralMappingModel
                           {
                               ApplicationId = Application.ApplicationId,
                               PanelName = SessionPanel.PanelName,
                               SessionPanelId = SessionPanel.SessionPanelId,
                               WithdrawalStatus = Application.WithdrawnFlag,
                               NonCompliant = ComplianceStatus.ComplianceStatusId,
                               PanelApplicationId = PanelApplication.PanelApplicationId,
                               AssignedToPanelId = PanelApplication.SessionPanelId,
                               ReferralMappingId = null
                           });
            var results = r1.Concat(r2.Where(x => r1.All(y => y.ApplicationId != x.ApplicationId)));
            return results;
        }
        /// <summary>
        /// Gets the referral mapping data model list.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <param name="referrals">The referrals.</param>
        /// <param name="programYearId">The program year identifier.</param>
        /// <param name="receiptCycle">The receipt cycle.</param>
        /// <returns></returns>
        internal static IEnumerable<ReferralMappingDataModel> GetReferralMappingDataByReferrals(P2RMISNETEntities context, 
            IList<KeyValuePair<string, string>> referrals,
                int programYearId, int receiptCycle)
        {
            var pnls = from pnl1 in context.SessionPanels
                       join pp in context.ProgramPanels
                       on new { SessionPanelId = (int?)pnl1.SessionPanelId ?? 0, ProgramYearId = programYearId }
                       equals new { pp.SessionPanelId, pp.ProgramYearId }
                       select pnl1;
            var logNos = referrals.ToList().ConvertAll(x => x.Key);
            var apps = from app in context.Applications
                       join pnlapp in context.PanelApplications
                       on app.ApplicationId equals pnlapp.ApplicationId into pnlapp2
                       from pnlapp in pnlapp2.DefaultIfEmpty()
                     where logNos.Contains(app.LogNumber)
                     select new { app, pnlapp };
            var results = from rs in referrals.AsEnumerable()
                          join ap in apps
                          on rs.Key equals ap.app.LogNumber into app2
                          from ap in app2.DefaultIfEmpty()
                          join pm in context.ProgramMechanism
                          on ap?.app.ProgramMechanismId equals pm.ProgramMechanismId into pm2
                          from pm in pm2.DefaultIfEmpty()
                          join pnl in pnls
                          on rs.Value equals pnl.PanelAbbreviation into pnl2
                          from pnl in pnl2.DefaultIfEmpty()
                          select new ReferralMappingDataModel
                          {
                              ApplicationId = ap?.app?.ApplicationId,
                              AppLogNumber = rs.Key,
                              SessionPanelId = pnl?.SessionPanelId,
                              PanelAbbreviation = rs.Value,
                              Cycle = pm?.ReceiptCycle,
                              PanelApplicationId = ap?.pnlapp?.PanelApplicationId
                          };
            return results;
        }
        /// <summary>
        /// Gets the referral mapping data by identifier.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <param name="referralMappingId">The referral mapping identifier.</param>
        /// <returns></returns>
        internal static IEnumerable<ReferralMappingDataModel> GetReferralMappingDataById(P2RMISNETEntities context, int referralMappingId)
        {
            var results = from rm in context.ReferralMappings
                          join rmd in context.ReferralMappingDatas
                          on rm.ReferralMappingId equals rmd.ReferralMappingId
                          join app in context.Applications
                          on rmd.ApplicationId equals app.ApplicationId into app2
                          from app in app2.DefaultIfEmpty()
                          join pnlapp in context.PanelApplications
                          on new { app.ApplicationId, rmd.SessionPanelId }
                          equals new { pnlapp.ApplicationId, pnlapp.SessionPanelId } into pnlapp2
                          from pnlapp in pnlapp2.DefaultIfEmpty()
                          join pm in context.ProgramMechanism
                          on app.ProgramMechanismId equals pm.ProgramMechanismId into pm2
                          from pm in pm2.DefaultIfEmpty()
                          join pnl in context.SessionPanels
                          on pnlapp.SessionPanelId equals pnl.SessionPanelId into pnl2
                          from pnl in pnl2.DefaultIfEmpty()
                          join pp in context.ProgramPanels
                          on new { pnl.SessionPanelId, rm.ProgramYearId }
                          equals new { pp.SessionPanelId, pp.ProgramYearId } into pp2
                          from pp in pp2.DefaultIfEmpty()
                          where rm.ReferralMappingId == referralMappingId
                          select new ReferralMappingDataModel
                          {
                              ApplicationId = app.ApplicationId,
                              AppLogNumber = app.LogNumber,
                              SessionPanelId = pnl.SessionPanelId,
                              PanelAbbreviation = pnl.PanelAbbreviation,
                              Cycle = pm.ReceiptCycle,
                              PanelApplicationId = pnlapp.PanelApplicationId
                          };
            return results;
        }

        /// <summary>
        /// Finds the applications.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <param name="programYearId">The program year identifier.</param>
        /// <param name="receiptCycle">The receipt cycle.</param>
        /// <returns></returns>
        internal static List<string> FindApplications(P2RMISNETEntities context, int programYearId, int receiptCycle)
        {
            var results = from Application in context.Applications
                          join ProgramMechanism in context.ProgramMechanism on Application.ProgramMechanismId equals ProgramMechanism.ProgramMechanismId
                          where ProgramMechanism.ProgramYearId == programYearId && ProgramMechanism.ReceiptCycle == receiptCycle
                          select Application.LogNumber;

            return results.ToList();
        }
        /// <summary>
        /// Finds the panel abbreviations.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <param name="programYearId">The program year identifier.</param>
        /// <param name="receiptCycle">The receipt cycle.</param>
        /// <returns></returns>
        internal static List<string> FindPanelAbbreviations(P2RMISNETEntities context, int programYearId, int receiptCycle)
        {
            var results = from ProgramPanel in context.ProgramPanels
                          join SessionPanel in context.SessionPanels on ProgramPanel.SessionPanelId equals SessionPanel.SessionPanelId
                          join ProgramMechanism in context.ProgramMechanism on ProgramPanel.ProgramYearId equals ProgramMechanism.ProgramYearId
                          where ProgramPanel.ProgramYearId == programYearId && ProgramMechanism.ReceiptCycle == receiptCycle
                          select SessionPanel.PanelAbbreviation;
            return results.ToList();
        }
    }
}
