namespace Dragonfly.SiteAuditor.Models
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Runtime.Serialization;
    using Dragonfly.SiteAuditor.Helpers;
    using Newtonsoft.Json;
    using Umbraco.Core.Models;

    [DataContract]
    public class AuditableProperty
    {
        private List<PropertyDoctypeInfo> _docTypes;

        #region Public Props
        [DataMember]
        public PropertyType UmbPropertyType { get; internal set; }

        [DataMember]
        public string InDocType { get; internal set; }

        [DataMember]
        public string InDocTypeGroup { get; internal set; }

        [DataMember]
        public List<PropertyDoctypeInfo> AllDocTypes
        {
            get { return _docTypes; }
            internal set { _docTypes = value; }
        }

        [DataMember]
        public IDataType DataType { get; internal set; }

        [DataMember]
        public Dictionary<string, string> DataTypeConfig { get; internal set; }

        [DataMember]
        public bool IsNestedContent { get; internal set; }
        [DataMember]
        public IEnumerable<NestedContentContentTypesConfigItem> NestedContentDocTypesConfig { get; internal set; }

        #endregion

 #region Methods

        
        #endregion

    }

    public class SiteAuditableProperties
    {
        public string PropsForDoctype { get; internal set; }
        public IEnumerable<AuditableProperty> AllProperties { get; internal set; }

    }

    public class PropertyDoctypeInfo
    {
        public string DocTypeAlias { get; set; }

        public string GroupName { get; set; }
    }
}
