using Newtonsoft.Json;
using Sra.P2rmis.WebModels.Lists;

namespace Sra.P2rmis.Web.UI.Models
{
    public class AwardTypeViewModel
    {
        public AwardTypeViewModel() { }

        public AwardTypeViewModel(IListDescription awardType) {
            AwardTypeId = awardType.Index;
            AwardName = awardType.Value;
            AwardAbbr = awardType.Description;
            MechanismRelationshiptypeId = awardType.BooleanValue;
        }

        /// <summary>
        /// Gets the award type identifier.
        /// </summary>
        /// <value>
        /// The award type identifier.
        /// </value>
        [JsonProperty("awardTypeId")]
        public int AwardTypeId { get; set; }

        /// <summary>
        /// Gets the name of the award.
        /// </summary>
        /// <value>
        /// The name of the award.
        /// </value>

        [JsonProperty("awardName")]
        public string AwardName { get; set; }

        /// <summary>
        /// Gets the award abbr.
        /// </summary>
        /// <value>
        /// The award abbr.
        /// </value>

        [JsonProperty("awardAbbr")]
        public string AwardAbbr { get; set; }

        /// <summary>
        /// Gets the award abbr.
        /// </summary>
        /// <value>
        /// The award abbr.
        /// </value>

        [JsonProperty("mechanismRelationshiptypeId")]
        public bool MechanismRelationshiptypeId { get; set; }
    }
}