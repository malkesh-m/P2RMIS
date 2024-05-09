using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Sra.P2rmis.Bll.Views;
using Sra.P2rmis.CrossCuttingServices.MessageServices;
using Sra.P2rmis.Dal;
using Sra.P2rmis.WebModels.Security;
using System.Linq;
using System.Activities.Expressions;

namespace Sra.P2rmis.Bll.Security
{
    public partial interface ISecurityService
    {
        ServiceState AddPolicy(int? policyId, int clientId, int type, string name, string details, DateTime startDate, string startTime, DateTime? endDate, string endTime, int restrictionType,
                            string restrictionStartTime, string restrictionEndTime, string weekDays, int userId, string networkRanges);
        IEnumerable<PolicyWebModel> GetPolicies();
        bool ArchivePolicy(int policyId, int userId);
        bool ActivateOrDeactivatePolicy(int policyId, int userId);
        Policy GetPolicyById(int policyId);
        PolicyWebModel GetPolicyModelById(int policyId); 

    }
    public partial class SecurityService : ServerBase, ISecurityService
    {
        public SecurityService()
        {
            UnitOfWork = new UnitOfWork();
        }
        /// <summary>
        /// Add Policy
        /// </summary>
        /// <param name="policyId">Policy Id</param>
        /// <param name="clientId">Client Id</param>
        /// <param name="type">Policy Type</param>
        /// <param name="name">Policy Name</param>
        /// <param name="details">Policy Details</param>
        /// <param name="startDate">Policy Start Date</param>
        /// <param name="startTime">Policy Start Time</param>
        /// <param name="endDate">Policy End Date</param>
        /// <param name="endTime">Policy End Time</param>
        /// <param name="restrictionType">Policy Restriction Type (24hour or time)</param>
        /// <param name="restrictionStartTime">Policy Restriction Start Time</param>
        /// <param name="restrictionEndTime">Policy Restriction End Time</param>
        /// <param name="weekDays">Weeekdays to apply policy</param>
        /// <param name="userId">User Id</param>
        /// <param name="networkRanges">Network ranges</param>
        /// <returns></returns>
        public ServiceState AddPolicy(int? policyId, int clientId, int type, string name, string details, DateTime startDate, string startTime, DateTime? endDate, string endTime, int restrictionType, string restrictionStartTime, string restrictionEndTime, string weekDays, int userId, string networkRanges)
        {
            Policy policy;
            bool networkRangesUpdated = false, weekDaysUpdated = false; //Flg to check when edit policy existing records updated.

            if (policyId != null)
            {
                policy = GetPolicyById(policyId.Value);
                
                policy.Details = details;
                policy.StartDateTime = startDate.Add(DateTime.Parse(startTime).TimeOfDay);
            }
            else
            {
                policy = new Policy() { PolicyId = policyId != null ? policyId.Value : 0, Active = true, ClientId = clientId, TypeId = type, Name = name, Details = details, StartDateTime = startDate.Add(DateTime.Parse(startTime).TimeOfDay) };
                
            }

            if (endDate.HasValue && !string.IsNullOrEmpty(endTime))
                policy.EndDateTime = ((DateTime)endDate).Add(DateTime.Parse(endTime).TimeOfDay);
            policy.RestrictionTypeId = restrictionType;
            if (restrictionType == (int)PolicyRestrictionType.Time && !string.IsNullOrEmpty(restrictionStartTime))
                policy.RestrictionStartTime = DateTime.Parse(restrictionStartTime).TimeOfDay;
            if (restrictionType == (int)PolicyRestrictionType.Time && !string.IsNullOrEmpty(restrictionEndTime))
                policy.RestrictionEndTime = DateTime.Parse(restrictionEndTime).TimeOfDay;

            var newPolicyWeekDays = new List<PolicyWeekDay>();
            if (weekDays != string.Empty)
            {
                foreach (var day in JsonConvert.DeserializeObject<List<string>>(weekDays))
                {
                    var weekDay = new PolicyWeekDay { PolicyId = policy.PolicyId, WeekDayId = short.Parse(day) };
                    Helper.UpdateCreatedFields(weekDay, userId);
                    Helper.UpdateModifiedFields(weekDay, userId);
                    newPolicyWeekDays.Add(weekDay);
                }
            }
            var newPolicyNetworkRanges = new List<PolicyNetworkRange>();
            if(networkRanges != string.Empty)
            {
                foreach (var range in JsonConvert.DeserializeObject<List<string>>(networkRanges))
                {
                    if (!string.IsNullOrEmpty(range))
                    {
                        var addresses = range.Split(',');
                        var networkRange = new PolicyNetworkRange { PolicyID = policy.PolicyId, StartAddress = addresses.FirstOrDefault(), EndAddress = addresses.LastOrDefault() };
                        Helper.UpdateCreatedFields(networkRange, userId);
                        Helper.UpdateModifiedFields(networkRange, userId);
                        newPolicyNetworkRanges.Add(networkRange);
                    }
                }
            }
            
            if (policyId == null)
            {
                Helper.UpdateCreatedFields(policy, userId);
                policy.PolicyWeekDays = newPolicyWeekDays;
                policy.PolicyNetworkRanges = newPolicyNetworkRanges;
            }
            else
            {
                var existingWeekdays = policy.PolicyWeekDays.Select(x => x.WeekDayId);
                var newWeekdays = newPolicyWeekDays.Select(x => x.WeekDayId);
                //If policy days are updated, delete existing and add new ones.
                if (!Enumerable.SequenceEqual(existingWeekdays, newWeekdays))
                {
                    weekDaysUpdated = true; //Insert updated recrord into PolicyWeekDaysHistory when call to CreatePolicyHistory later in this method.

                    //Delete removed weekdays.
                    var daysDeleted = existingWeekdays.Except(newWeekdays);
                    if(daysDeleted.Count() > 0)
                    {
                        var deletedItems = new List<PolicyWeekDay>();
                        foreach (var weekday in daysDeleted)
                        {
                            var delItem = policy.PolicyWeekDays.Where(x => x.WeekDayId == weekday).FirstOrDefault();
                            Helper.UpdateDeletedFields(delItem, userId);
                            deletedItems.Add(delItem);
                        }
                        foreach (var deletedItem in deletedItems)
                        {
                            UnitOfWork.PolicyWeekDayRepository.Delete(deletedItem);
                        }
                    }
                    //Add new weekdays
                    var daysAdded = newWeekdays.Except(existingWeekdays);
                    if(daysAdded.Count() > 0)
                    {
                        foreach (var weekDay in newPolicyWeekDays.Where(x => daysAdded.Contains(x.WeekDayId)))
                        {
                            Helper.UpdateCreatedFields(weekDay, userId);
                            Helper.UpdateModifiedFields(weekDay, userId);
                            policy.PolicyWeekDays.Add(weekDay);
                        }
                    }
                }

                //If network range are updated, delete existing and add new ones.
                var deletedRanges = new List<PolicyNetworkRange>();
                foreach (var existingNetworkRange in policy.PolicyNetworkRanges)
                {
                    var existingRangeIsStillAvail = newPolicyNetworkRanges.Exists(x => x.StartAddress.Trim() == existingNetworkRange.StartAddress.Trim()
                                                                        && x.EndAddress.Trim() == existingNetworkRange.EndAddress.Trim());
                    if (!existingRangeIsStillAvail)
                    {
                        
                        Helper.UpdateDeletedFields(existingNetworkRange, userId);
                        deletedRanges.Add(existingNetworkRange);
                    }
                }
                foreach (var deletedItem in deletedRanges)
                {
                    networkRangesUpdated = true; //Insert updated recrord into PolicyNetworkRange when call to CreatePolicyHistory later in this method.
                    UnitOfWork.PolicyNetworkRangeRepository.Delete(deletedItem);
                }
                foreach (var newPolicyNetworkRange in newPolicyNetworkRanges)
                {
                    var newRangeIsAlreadyAvail = policy.PolicyNetworkRanges.ToList().Exists(x => x.StartAddress.Trim() == newPolicyNetworkRange.StartAddress.Trim()
                                                                        && x.EndAddress.Trim() == newPolicyNetworkRange.EndAddress.Trim());
                    if (!newRangeIsAlreadyAvail)
                    {
                        networkRangesUpdated = true; //Insert updated recrord into PolicyNetworkRange when call to CreatePolicyHistory later in this method.
                        policy.PolicyNetworkRanges.Add(newPolicyNetworkRange);
                    }
                }
                
            }

            Helper.UpdateModifiedFields(policy, userId);
            var valid = ValidatePolicy(policy);
            var s = new ServiceState((valid.Count == 0), valid);
            if (valid.Count == 0)
            {
                PolicyHistory policyHistory;
                if (policyId==null)
                    policyHistory = CreatePolicyHistory(policy, userId, PolicyStatus.Created);
                else
                    policyHistory = CreatePolicyHistory(policy, userId, PolicyStatus.Modified, weekDaysUpdated, networkRangesUpdated);
                policy.PolicyHistories.Add(policyHistory);

                if (policyId == null)
                    UnitOfWork.PolicyRepository.Add(policy);

                
                //UnitOfWork.PolicyHistoryRepository.Add(policyHistory);

                UnitOfWork.Save();

                //Add existing data in policy history
                //if (policyId == null)
                //{
                //    existingPolicy = policy.ShallowCopy();
                //    AddPolicyHistory(existingPolicy, userId, PolicyStatus.Created);
                //}
                //else
                //    AddPolicyHistory(existingPolicy, userId, PolicyStatus.Modified);
            }
            return s;
        }
        /// <summary>
        /// Validate Add Policy
        /// </summary>
        /// <param name="policy"></param>
        /// <returns></returns>
        private List<string> ValidatePolicy(Dal.Policy policy)
        {
            var errorMessages = new List<string>();
            if (policy.TypeId == 0)
            {
                errorMessages.Add(MessageService.PolicyTypeRequired);
            }
            if (string.IsNullOrEmpty(policy.Name))
            {
                errorMessages.Add(MessageService.PolicyNameRequired);
            }
            else if (UnitOfWork.PolicyRepository.Get(p => p.Name.Equals(policy.Name) && p.PolicyId != (policy.PolicyId == 0 ? p.PolicyId : policy.PolicyId)).Any())
            {
                errorMessages.Add(string.Format(MessageService.PolicyNameDuplicate, policy.Name));

            }
            if (policy.EndDateTime.HasValue && (policy.StartDateTime >= policy.EndDateTime))
            {
                errorMessages.Add(MessageService.PolicyStartDateGreaterThenEndDate);
            }
            if (policy.TypeId.Equals(PolicyType.Access))
            {
                if (policy.RestrictionTypeId.Equals(PolicyRestrictionType.Time) && !policy.EndDateTime.HasValue)
                {
                    errorMessages.Add(MessageService.RestrictionHoursRequired);
                }
                if (policy.PolicyWeekDays.Count == 0)
                {
                    errorMessages.Add(MessageService.PolicyDaysRequired);
                }
            }
            if (policy.TypeId.Equals(PolicyType.Network) && policy.PolicyNetworkRanges.Count == 0)
            {
                errorMessages.Add(MessageService.PolicyNetworkRangeRequired);
            }
            return (errorMessages);
        }

        /// <summary>
        /// Gets all policies
        /// </summary>
        /// <returns></returns>
        public IEnumerable<PolicyWebModel> GetPolicies()
        {
            var entities = UnitOfWork.PolicyRepository.Get(p => !p.Archived);
            var policies = entities.Select(p => new PolicyWebModel
            {
                Id = p.PolicyId,
                Name = p.Name,
                Type = p.PolicyType.Name,
                StartDateTime = p.StartDateTime,
                EndDateTime = p.EndDateTime,
                RestrictionStartTime = p.RestrictionStartTime == null ? null : new DateTime().Add(p.RestrictionStartTime.Value).ToString("h:mm tt ET"),
                RestrictionEndTime = p.RestrictionEndTime == null ? null : new DateTime().Add(p.RestrictionEndTime.Value).ToString("h:mm tt ET"),
                DaysApplied = string.Join(", ", p.PolicyWeekDays
                    .Select(wd => wd.WeekDay.Abbreviation.Trim())
                    .ToList()),
                NetworkRanges = string.Join(" ", p.PolicyNetworkRanges
                    .Select(nr => $"{nr.StartAddress}, {nr.EndAddress}")
                    .ToList()),
                Status = p.Active ? "Enabled" : "Disabled",
                CreatedBy = UnitOfWork.UserRepository.GetByID(p.CreatedBy).FullName(),
                CreatedDateTime = p.CreatedDate
            });
            return policies;
        }

        /// <summary>
        /// Service method to archive an existing Policy.
        /// </summary>
        /// <param name="programMechanismId">Identifies the Policy to be deleted</param>
        /// <param name="userId">User entity identifier of the user deleting the award</param>
        /// <returns>ServiceState object containing status & error messages</returns>
        public virtual bool ArchivePolicy(int policyId, int userId)
        {
            var archived = false;
            string name = FullName(nameof(SecurityService), nameof(ArchivePolicy));
            ValidateInteger(policyId, name, nameof(policyId));
            ValidateInteger(userId, name, nameof(userId));

            Policy policy = UnitOfWork.PolicyRepository.GetByID(policyId);
            if (policy != null)
            {
                //Add deleted record in policy history
                var policyHistory = CreatePolicyHistory(policy, userId, PolicyStatus.Deleted);
                policy.PolicyHistories.Add(policyHistory);

                policy.Archived = true;
                Helper.UpdateModifiedFields(policy, userId);

                UnitOfWork.Save();
                archived = true;
            }

            

            return archived;
        }

        /// <summary>
        /// Service method to activate/deactivate an existing Policy.
        /// </summary>
        /// <param name="programMechanismId">Identifies the Policy to be deleted</param>
        /// <param name="userId">User entity identifier of the user deleting the award</param>
        /// <returns>ServiceState object containing status & error messages</returns>
        public virtual bool ActivateOrDeactivatePolicy(int policyId, int userId)
        {
            var result = false;
            PolicyHistory policyHistory;
            string name = FullName(nameof(SecurityService), nameof(ActivateOrDeactivatePolicy));
            ValidateInteger(policyId, name, nameof(policyId));
            ValidateInteger(userId, name, nameof(userId));

            Policy policy = UnitOfWork.PolicyRepository.GetByID(policyId);
            if (policy != null)
            {
                //Add existing policy record in policy history
                if (policy.Active)
                {
                    policyHistory = CreatePolicyHistory(policy, userId, PolicyStatus.Enabled);
                }
                else
                {
                    policyHistory = CreatePolicyHistory(policy, userId, PolicyStatus.Disabled);
                }
                policy.PolicyHistories.Add(policyHistory);

                //Update policy activation
                policy.Active = !policy.Active;
                Helper.UpdateModifiedFields(policy, userId);
                
                UnitOfWork.Save();
                result = true;
            }
            return result;
        }

        /// <summary>
        /// Get policy by id.
        /// </summary>
        /// <param name="policyId"></param>
        /// <returns></returns>
        public virtual Policy GetPolicyById(int policyId)
        {
            string name = FullName(nameof(SecurityService), nameof(ActivateOrDeactivatePolicy));
            ValidateInteger(policyId, name, nameof(policyId));
            var result = UnitOfWork.PolicyRepository.GetByID(policyId);
            return result != null ? result : new Policy();
        }

        /// <summary>
        /// Get policy web model by id.
        /// </summary>
        /// <param name="policyId"></param>
        /// <returns></returns>
        public virtual PolicyWebModel GetPolicyModelById(int policyId)
        {
            string name = FullName(nameof(SecurityService), nameof(ActivateOrDeactivatePolicy));
            ValidateInteger(policyId, name, nameof(policyId));
            var result = UnitOfWork.PolicyRepository.GetByID(policyId);
            ////TODO: This needs to be instantiated once in StartUp and passed using DI.
            PolicyWebModel policyWebModel = new PolicyWebModel()
            {
                Id = result.PolicyId,
                Name = result.Name,
                Details = result.Details,
                PolicyTypeId = result.PolicyType.PolicyTypeId,
                Type = result.PolicyType.Name,
                StartDateTime = result.StartDateTime,
                EndDateTime = result.EndDateTime,
                RestrictionStartTimeSpan = result.RestrictionStartTime ,
                RestrictionEndTimeSpan = result.RestrictionEndTime ,
                RestrictionTypeId = result.RestrictionTypeId,
                RestrictionType = result.PolicyRestrictionType.Name,
                PolicyWeekDays = new List<PolicyWeekDayWebModel>(),
                PolicyNetworkRanges = new List<PolicyNetworkRangeWebModel>()
            };
            foreach(var weekWebModel in result.PolicyWeekDays)
            {
                policyWebModel.PolicyWeekDays?.Add(new PolicyWeekDayWebModel { WeekDayId = weekWebModel.WeekDayId });
            }
            foreach (var networkWebModel in result.PolicyNetworkRanges)
            {
                policyWebModel.PolicyNetworkRanges.Add(new PolicyNetworkRangeWebModel { StartAddress = networkWebModel.StartAddress, EndAddress = networkWebModel.EndAddress });
            }
            
            return policyWebModel;
        }

        /// <summary>
        /// Create policy history from existing policy.
        /// </summary>
        /// <param name="policy"></param>
        /// <param name="userId"></param>
        /// <param name="policyStatus"></param>
        /// <param name="policyWeekDaysUpdated"></param>
        /// <param name="policyNetworkRangeUpdated"></param>
        /// <returns></returns>
        private PolicyHistory CreatePolicyHistory(Policy policy, int userId, PolicyStatus policyStatus,bool policyWeekDaysUpdated=false, bool policyNetworkRangeUpdated=false)
        {
            PolicyHistory policyHistory = new PolicyHistory()
            {
                PolicyId = policy.PolicyId,
                ClientId = policy.ClientId,
                TypeId = policy.TypeId,
                Name = policy.Name,
                Details = policy.Details,
                StartDateTime = policy.StartDateTime,
                EndDateTime = policy.EndDateTime,
                RestrictionTypeId = policy.RestrictionTypeId,
                RestrictionStartTime = policy.RestrictionStartTime,
                RestrictionEndTime = policy.RestrictionEndTime,
                Active = policy.Active,
                PolicyStatusId = (int)policyStatus
            };
            Helper.UpdateCreatedFields(policyHistory, userId);
            Helper.UpdateModifiedFields(policyHistory, userId);

            if (policyStatus==PolicyStatus.Modified && policyWeekDaysUpdated)
            {
                foreach (var policyWeekDay in policy.PolicyWeekDays)
                {
                    var policyWeekDayHistory = new PolicyWeekDayHistory
                    {
                        PolicyId = policyWeekDay.PolicyId,
                        WeekDayId = policyWeekDay.WeekDayId
                    };
                    Helper.UpdateCreatedFields(policyWeekDayHistory, userId);
                    Helper.UpdateModifiedFields(policyWeekDayHistory, userId);
                    policyHistory.PolicyWeekDayHistories.Add(policyWeekDayHistory);
                }
            }

            if (policyStatus == PolicyStatus.Modified && policyNetworkRangeUpdated)
            {
                foreach (var policyNetworkRange in policy.PolicyNetworkRanges)
                {
                    var policyNetworkRaneHistory = new PolicyNetworkRangeHistory
                    {
                        PolicyId = policyNetworkRange.PolicyID,
                        EndAddress = policyNetworkRange.EndAddress,
                        StartAddress = policyNetworkRange.StartAddress
                    };
                    Helper.UpdateCreatedFields(policyNetworkRaneHistory, userId);
                    Helper.UpdateModifiedFields(policyHistory, userId);
                    policyHistory.PolicyNetworkRangeHistories.Add(policyNetworkRaneHistory);
                }
            }

                var lastPolicyHistory = UnitOfWork.PolicyHistoryRepository.GetLastByPolicyId(policy.PolicyId);
                if(lastPolicyHistory==null)
                    policyHistory.VersionId = 1;
                else
                    policyHistory.VersionId = 1 + lastPolicyHistory.VersionId;

            return policyHistory;
        }

        /// <summary>
        /// Validate parameters for SaveComment
        /// </summary>
        /// <param name="applicationId">Application entity identifier</param>
        /// <param name="userId">User entity identifier</param>
        private void ValidateSavePolicyHistoryParameters(int policyHistoryId, int userId)
        {
            this.ValidateInteger(policyHistoryId, "SecurityService.SavePolicyHistory", "policyHistoryId");
            this.ValidateInteger(userId, "SecurityService.SavePolicyHistory", "userId");
        }

        
    }
}
