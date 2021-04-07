namespace Dragonfly.SiteAuditor.Models
{
    using System;
    using Umbraco.Core;
    using Umbraco.Core.Models;
    using Umbraco.Core.Services;
    using Umbraco.Web;

    public class AuditableDocType
    {
     
        #region Public Props

        public IContentType ContentType { get; set; }

        public string Name { get; set; }
        public string Alias { get; set; }
        public Guid GUID { get; set; }
        public string DefaultTemplateName { get; set; }

        //TODO: Add Info about compositions/parents/folders: IsComposition, HasCompositions, etc.

        #endregion

        #region Constructor

        /// <inheritdoc />
        public AuditableDocType(IContentType ContentType)
        {
            this.ContentType = ContentType;
            this.Name = ContentType.Name;
            this.Alias = ContentType.Alias;
            this.GUID = ContentType.Key;

            if (ContentType.DefaultTemplate != null)
            {
                this.DefaultTemplateName = ContentType.DefaultTemplate.Name;
            }
            else
            {
                this.DefaultTemplateName = "NONE";
            }

            // var x = ContentType.AllowedTemplates
        }

        public AuditableDocType()
        {
            
        }

        #endregion
    }
}
