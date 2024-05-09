using System;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using Sra.P2rmis.Dal.Common;

namespace Sra.P2rmis.Dal
{
    [MetadataType(typeof(SystemTemplateVersionMetaData))]
    public partial class SystemTemplateVersion
    {

        public string ModifiedDateStr
        {

            get
            {
                string retStr = "";
                if (this.ModifiedDate != null)
                {
                    DateTime dateVal = (DateTime)this.ModifiedDate;
                    retStr = dateVal.ToString(Constants.SYSTEM_DATE_FORMAT);
                }
                return retStr;
            }
        }

        public string CreatedDateStr
        {

            get
            {
                string retStr = "";
                if (this.CreatedDate != null)
                {
                    DateTime dateVal = (DateTime)this.CreatedDate;
                    retStr = dateVal.ToString(Constants.SYSTEM_DATE_FORMAT);
                }
                return retStr;
            }
        }


        public class SystemTemplateVersionMetaData
        {
            [HiddenInput(DisplayValue = false)]
            public int VersionId { get; set; }

            [Required]
            [StringLength(100, ErrorMessage = "Length Must be under 100 characters")]
            public string Subject { get; set; }

            [Required]
            [StringLength(100, ErrorMessage = "Length Must be under 100 characters")]
            [DataType(DataType.MultilineText)]
            public string Description { get; set; }

            [Required]
            [AllowHtml]
            public string Body { get; set; }

            [HiddenInput(DisplayValue = false)]
            public Nullable<System.Guid> rowguid { get; set; }
        }

        
    }
}
