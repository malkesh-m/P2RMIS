using System.Collections.Generic;
using System.Xml.Serialization;
using System.Xml;
using System.Web;

namespace Sra.P2rmis.WebModels.Setup
{
    public class DataDeliverableModel
    {
        /// <summary>
        /// Create XML data element.
        /// </summary>
        /// <param name="rawObject"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public XmlDataElement CreateXmlDataElement(object rawObject, string type)
        {
            var doc = new XmlDocument();
            var ele = new XmlDataElement();
            ele.Type = rawObject == null ? "null" : type;
            var embeddedValue = rawObject == null ? "" : rawObject.ToString();
            // We currently don't encode the embedded value to be same as what 1.0 service provides
            ele.Value = doc.CreateCDataSection(embeddedValue);
            return ele;
        }
    }
    
    /// <summary>
    /// XML data element.
    /// </summary>
    public class XmlDataElement
    {
        /// <summary>
        /// Type attribute
        /// </summary>
        [XmlAttribute(AttributeName = "type")]
        public string Type { get; set; }
        /// <summary>
        /// Value
        /// </summary>
        [XmlAnyElement]
        public XmlCDataSection Value { get; set; }
    }
}
