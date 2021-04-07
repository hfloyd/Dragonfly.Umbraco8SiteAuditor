﻿namespace Dragonfly.SiteAuditor.Models
{
    using System.Collections.Generic;
    using System.Runtime.Serialization;
    using Umbraco.Core.Models;
    using Umbraco.Core.Models.PublishedContent;
    using Umbraco.Web;

    /// <summary>
    /// Meta data about a Content Node for Auditing purposes.
    /// </summary>
    [DataContract]
    public class AuditableContent
    {
        #region Public Props
        /// <summary>
        /// Gets or sets the content node as an IContent
        /// </summary>
        public IContent UmbContentNode { get; internal set; }

        /// <summary>
        /// Gets or sets the content node and an IPublishedContent
        /// </summary>
        public IPublishedContent UmbPublishedNode { get; internal set; }

        /// <summary>
        /// The node path.
        /// </summary>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        public IEnumerable<string> NodePath { get; internal set; }

        /// <summary>
        /// Default string used for NodePathAsText
        /// ' » ' unless explicitly changed
        /// </summary>
        public string DefaultDelimiter
        {
            get { return _defaultDelimiter; }
            internal set { _defaultDelimiter = value; }
        }
        private string _defaultDelimiter = " » ";

        /// <summary>
        /// Full path to node in a single delimited string using object's default delimiter
        /// </summary>
        public string NodePathAsText
        {
            get
            {
                var nodePath = string.Join(this.DefaultDelimiter, this.NodePath);
                return nodePath;
            }
        }

        /// <summary>
        /// Alias of the Template assigned to this Content Node. Returns "NONE" if there is no template.
        /// </summary>
        public string TemplateAlias { get; set; } = "Unknown";


        /// <summary>
        /// Url with domain name. Returns "UNPUBLISHED" if there is no public url.
        /// </summary>
        public string FullNiceUrl
        {
            get
            {
                string niceUrl;

                if (this.UmbContentNode.Published & this.UmbPublishedNode != null)
                {
                    niceUrl = this.UmbPublishedNode.Url(mode: UrlMode.Absolute);
                }
                else
                {
                    niceUrl = "UNPUBLISHED";
                }

                return niceUrl;
            }
        }

        /// <summary>
        /// Path-only Url. Returns "UNPUBLISHED" if there is no public url.
        /// </summary>
        public string RelativeNiceUrl
        {
            get
            {
                string niceUrl;

                if (this.UmbContentNode.Published)
                {

                    niceUrl = this.UmbPublishedNode.Url(mode: UrlMode.Relative);
                }
                else
                {
                    niceUrl = "UNPUBLISHED";
                }

                return niceUrl;
            }
        }

        #endregion

        #region Methods

        public string NodePathAsCustomText(string Separator = " » ")
        {
            var nodePath = string.Join(Separator, this.NodePath);
            return nodePath;
        }

        #endregion

    }
}