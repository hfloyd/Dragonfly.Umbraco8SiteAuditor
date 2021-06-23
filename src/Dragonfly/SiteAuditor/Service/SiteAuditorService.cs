namespace Dragonfly.SiteAuditor
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Dragonfly.SiteAuditor.Helpers;
    using Dragonfly.SiteAuditor.Models;
    using Newtonsoft.Json;
    using Umbraco.Core;
    using Umbraco.Core.Cache;
    using Umbraco.Core.Composing.CompositionExtensions;
    using Umbraco.Core.Logging;
    using Umbraco.Core.Models;
    using Umbraco.Core.Models.PublishedContent;
    using Umbraco.Core.Services;
    using Umbraco.Web;
    using Umbraco.Web.Composing;
    using Umbraco.Web.PropertyEditors;
    using Umbraco.Web.Security;

    public class SiteAuditorService
    {
        private UmbracoHelper _umbracoHelper;
        private readonly ILogger _logger;
        //private readonly AppCaches _appCaches;
        private readonly UmbracoContext _umbracoContext;
        private readonly ServiceContext _services;

        #region ctor
        public SiteAuditorService()
        {
            //Services
            this._umbracoHelper = Current.UmbracoHelper;
            this._services = Current.Services;
            this._logger = Current.Logger;
            //this._appCaches = Current.AppCaches;
            this._umbracoContext = Current.UmbracoContext;
        }

        public SiteAuditorService(UmbracoHelper UmbHelper, UmbracoContext UmbContext, ServiceContext Services, ILogger Logger)
        {
            //Services
            this._umbracoHelper = UmbHelper;
            this._services = Services;
            this._logger = Logger;
            //this._appCaches = Current.AppCaches;
            this._umbracoContext = UmbContext;
        }

        #endregion

        #region All Nodes (IPublishedContent)

        /// <summary>
        /// Gets all site nodes as IPublishedContent
        /// </summary>
        /// <param name="IncludeUnpublished">Should unpublished nodes be included? (They will be returned as 'virtual' IPublishedContent models)</param>
        /// <returns></returns>
        public IEnumerable<IPublishedContent> GetAllNodes()
        {
            var nodesList = new List<IPublishedContent>();

            //if (IncludeUnpublished)
            //{
            //    //Get nodes as IContent
            //    var topLevelNodes = _services.ContentService.GetRootContent().OrderBy(n => n.SortOrder);

            //    foreach (var thisNode in topLevelNodes)
            //    {
            //        nodesList.AddRange(LoopNodes(thisNode));
            //    }
            //}
            //else
            //{
            //Get nodes as IPublishedContent
            var topLevelNodes = _umbracoHelper.ContentAtRoot().OrderBy(n => n.SortOrder);

            foreach (var thisNode in topLevelNodes)
            {
                nodesList.AddRange(LoopNodes(thisNode));
            }
            // }

            return nodesList;
        }

        private IEnumerable<IPublishedContent> LoopNodes(IPublishedContent ThisNode)
        {
            var nodesList = new List<IPublishedContent>();

            //Add current node, then loop for children
            try
            {
                nodesList.Add(ThisNode);

                if (ThisNode.Children().Any())
                {
                    foreach (var childNode in ThisNode.Children().OrderBy(n => n.SortOrder))
                    {
                        nodesList.AddRange(LoopNodes(childNode));
                    }
                }
            }
            catch (Exception e)
            {
                //skip
            }

            return nodesList;
        }

        //private IEnumerable<IPublishedContent> LoopNodes(IContent ThisNode)
        //{
        //    var nodesList = new List<IPublishedContent>();

        //    //Add current node, then loop for children
        //    try
        //    {
        //        nodesList.Add(ThisNode.ToPublishedContent());

        //        if (ThisNode.Children().Any())
        //        {
        //            foreach (var childNode in ThisNode.Children().OrderBy(n => n.SortOrder))
        //            {
        //                nodesList.AddRange(LoopNodes(childNode));
        //            }
        //        }
        //    }
        //    catch (Exception e)
        //    {
        //        //skip
        //    }

        //    return nodesList;
        //}

        #endregion

        #region AuditableContent

        /// <summary>
        /// Gets all site nodes as AuditableContent models
        /// </summary>
        /// <returns></returns>
        public List<AuditableContent> GetContentNodes()
        {
            var nodesList = new List<AuditableContent>();

            var topLevelContentNodes = _services.ContentService.GetRootContent().OrderBy(n => n.SortOrder);

            foreach (var thisNode in topLevelContentNodes)
            {
                nodesList.AddRange(LoopForAuditableContentNodes(thisNode));
            }

            return nodesList;
        }


        /// <summary>
        /// Gets all descendant nodes from provided Root Node Id as AuditableContent models
        /// </summary>
        /// <param name="RootNodeId">Integer Id of Root Node</param>
        /// <returns></returns>
        public List<AuditableContent> GetContentNodes(int RootNodeId)
        {
            var nodesList = new List<AuditableContent>();

            //var TopLevelNodes = umbHelper.ContentAtRoot();
            var topLevelNodes = _services.ContentService.GetByIds(RootNodeId.AsEnumerableOfOne()).OrderBy(n => n.SortOrder);

            foreach (var thisNode in topLevelNodes)
            {
                nodesList.AddRange(LoopForAuditableContentNodes(thisNode));
            }

            return nodesList;
        }


        /// <summary>
        /// Gets all descendant nodes from provided Root Node Id as AuditableContent models
        /// </summary>
        /// <param name="RootNodeUdi">Udi of Root Node</param>
        /// <returns></returns>
        public List<AuditableContent> GetContentNodes(Udi RootNodeUdi)
        {
            var nodesList = new List<AuditableContent>();

            //var TopLevelNodes = umbHelper.ContentAtRoot();
            var topLevelNodes = _services.ContentService.GetByIds(RootNodeUdi.AsEnumerableOfOne()).OrderBy(n => n.SortOrder);

            foreach (var thisNode in topLevelNodes)
            {
                nodesList.AddRange(LoopForAuditableContentNodes(thisNode));
            }

            return nodesList;
        }

        internal List<AuditableContent> LoopForAuditableContentNodes(IContent ThisNode)
        {
            var nodesList = new List<AuditableContent>();

            //Add current node, then loop for children
            AuditableContent auditContent = ConvertIContentToAuditableContent(ThisNode);
            nodesList.Add(auditContent);

            //figure out num of children
            long countChildren;
            var test = _services.ContentService.GetPagedChildren(ThisNode.Id, 0, 1, out countChildren);
            if (countChildren > 0)
            {
                long countTest;
                var allChildren = _services.ContentService.GetPagedChildren(ThisNode.Id, 0, Convert.ToInt32(countChildren), out countTest);
                foreach (var childNode in allChildren.OrderBy(n => n.SortOrder))
                {
                    nodesList.AddRange(LoopForAuditableContentNodes(childNode));
                }
            }

            return nodesList;
        }

        private AuditableContent ConvertIContentToAuditableContent(IContent ThisIContent)
        {
            var ac = new AuditableContent();
            ac.UmbContentNode = ThisIContent;
            try
            {
                var iPub = _umbracoHelper.Content(ThisIContent.Id);
                ac.UmbPublishedNode = iPub;
            }
            catch (Exception e)
            {
                //Get preview - unpublished
                var iPub = _umbracoContext.Content.GetById(true, ThisIContent.Id);
                ac.UmbPublishedNode = iPub;
            }

            ac.NodePath = AuditHelper.NodePath(ThisIContent);
            //this.DocTypes = new List<string>();

            return ac;
        }

        private AuditableContent ConvertIPubContentToAuditableContent(IPublishedContent PubContentNode)
        {
            var ac = new AuditableContent();
            ac.UmbContentNode = _services.ContentService.GetById(PubContentNode.Id);
            ac.UmbPublishedNode = PubContentNode;
            ac.NodePath = AuditHelper.NodePath(ac.UmbContentNode);
            ac.TemplateAlias = GetTemplateAlias(ac.UmbContentNode);
            //this.DocTypes = new List<string>();

            return ac;
        }

        private string GetTemplateAlias(IContent Content)
        {
            string templateAlias = "NONE";
            if (Content.TemplateId != null)
            {
                var template = _services.FileService.GetTemplate((int)Content.TemplateId);
                templateAlias = template.Alias;
            }

            return templateAlias;
        }

        #endregion

        #region DocTypes

        /// <summary>
        /// Gets list of all DocTypes on site as IContentType models
        /// </summary>
        /// <returns></returns>
        public IEnumerable<IContentType> GetAllDocTypes()
        {
            var list = new List<IContentType>();

            var doctypes = _services.ContentTypeService.GetAll();

            foreach (var type in doctypes)
            {
                if (type != null)
                {
                    list.Add(type);
                }
            }

            return list;
        }


        /// <summary>
        /// Gets list of all DocTypes on site as AuditableDoctype models
        /// </summary>
        /// <returns></returns>
        public IEnumerable<AuditableDocType> GetAuditableDocTypes()
        {
            var list = new List<AuditableDocType>();

            var doctypes = _services.ContentTypeService.GetAll();

            foreach (var type in doctypes)
            {
                if (type != null)
                {
                    list.Add(new AuditableDocType(type));
                }
            }

            return list;
        }


        ///// <summary>
        ///// Gets list of all DocTypes on site as AuditableDoctype models
        ///// </summary>
        ///// <returns></returns>
        //public static IEnumerable<AuditableDocType> GetAuditableDocTypes()
        //{
        //    var list = new List<AuditableDocType>();

        //    var doctypes = umbDocTypeService.GetAllContentTypes();

        //    foreach (var type in doctypes)
        //    {
        //        if (type != null)
        //        {
        //            list.Add(new AuditableDocType(type));
        //        }
        //    }

        //    return list;
        //}

        #endregion

        #region AuditableProperties

        public SiteAuditableProperties AllProperties()
        {
            var allProps = new SiteAuditableProperties();
            allProps.PropsForDoctype = "[All]";
            List<AuditableProperty> propertiesList = new List<AuditableProperty>();

            var allDocTypes = _services.ContentTypeService.GetAll();

            foreach (var docType in allDocTypes)
            {
                //var ct = _services.ContentTypeService.Get(docTypeAlias);

                foreach (var prop in docType.PropertyTypes)
                {
                    //test for the same property already in list
                    if (propertiesList.Exists(i => i.UmbPropertyType.Alias == prop.Alias & i.UmbPropertyType.Name == prop.Name & i.UmbPropertyType.DataTypeId == prop.DataTypeId))
                    {
                        //Add current DocType to existing property
                        var info = new PropertyDoctypeInfo();
                        info.DocTypeAlias = docType.Alias;
                        info.GroupName = "";
                        propertiesList.Find(i => i.UmbPropertyType.Alias == prop.Alias).AllDocTypes.Add(info);
                    }
                    else
                    {
                        //Add new property
                        AuditableProperty auditProp = PropertyTypeToAuditableProperty(prop);

                        var info = new PropertyDoctypeInfo();
                        info.DocTypeAlias = docType.Alias;
                        info.GroupName = "";

                        auditProp.AllDocTypes.Add(info);
                        propertiesList.Add(auditProp);
                    }
                }
            }

            allProps.AllProperties = propertiesList;
            return allProps;
        }

        /// <summary>
        /// Get a ContentType model for a Doctype by its alias
        /// </summary>
        /// <param name="DocTypeAlias"></param>
        /// <returns></returns>
        public IContentType GetContentTypeByAlias(string DocTypeAlias)
        {
            return _services.ContentTypeService.Get(DocTypeAlias);
        }

        public SiteAuditableProperties AllPropertiesForDocType(string DocTypeAlias)
        {
            var allProps = new SiteAuditableProperties();
            allProps.PropsForDoctype = DocTypeAlias;
            List<AuditableProperty> propertiesList = new List<AuditableProperty>();

            var ct = _services.ContentTypeService.Get(DocTypeAlias);

            foreach (var prop in ct.PropertyTypes)
            {
                //Add property
                AuditableProperty auditProp = PropertyTypeToAuditableProperty(prop);

                var info = new PropertyDoctypeInfo();
                info.DocTypeAlias = DocTypeAlias;
                info.GroupName = "";

                auditProp.AllDocTypes.Add(info);
                propertiesList.Add(auditProp);
            }

            allProps.AllProperties = propertiesList;
            return allProps;
        }

        /// <summary>
        /// Meta data about a Property for Auditing purposes.
        /// </summary>
        /// <param name="UmbPropertyType"></param>
        private AuditableProperty PropertyTypeToAuditableProperty(PropertyType UmbPropertyType)
        {
            var ap = new AuditableProperty();
            ap.UmbPropertyType = UmbPropertyType;

            ap.DataType = _services.DataTypeService.GetDataType(UmbPropertyType.DataTypeId);

            ap.DataTypeConfigType = ap.DataType.Configuration.GetType();
            try
            {
                var configDict = (Dictionary<string, string>)ap.DataType.Configuration;
                ap.DataTypeConfigDictionary = configDict;
            }
            catch (Exception e)
            {
                //ignore
                ap.DataTypeConfigDictionary = new Dictionary<string, string>();
            }

            //var  docTypes = AuditHelper.GetDocTypesForProperty(UmbPropertyType.Alias);
            // this.DocTypes = new List<string>();

            if (ap.DataType.EditorAlias.Contains("NestedContent"))
            {
                ap.IsNestedContent = true;
                var config = (NestedContentConfiguration)ap.DataType.Configuration;
                //var contentJson = ["contentTypes"];

                //var types = JsonConvert
                //    .DeserializeObject<IEnumerable<NestedContentContentTypesConfigItem>>(contentJson);
                ap.NestedContentDocTypesConfig = config.ContentTypes;
            }

            return ap;
        }

        /// <summary>
        /// Get a list of all DocTypes which contain a property of a specified Alias
        /// </summary>
        /// <param name="PropertyAlias"></param>
        /// <returns></returns>
        public List<PropertyDoctypeInfo> GetDocTypesForProperty(string PropertyAlias)
        {
            var docTypesList = new List<PropertyDoctypeInfo>();

            var allDocTypes = _services.ContentTypeService.GetAll();

            foreach (var docType in allDocTypes)
            {
                var matchingProps = docType.CompositionPropertyTypes.Where(n => n.Alias == PropertyAlias).ToList();
                if (matchingProps.Any())
                {
                    foreach (var prop in matchingProps)
                    {
                        var x = new PropertyDoctypeInfo();
                        x.DocTypeAlias = docType.Alias;

                        var matchingGroups = docType.PropertyGroups.Where(n => n.PropertyTypes.Contains(prop.Alias)).ToList();
                        if (matchingGroups.Any())
                        {
                            x.GroupName = matchingGroups.First().Name;
                        }

                        docTypesList.Add(x);
                    }
                }
            }

            return docTypesList;
        }

        #endregion

        #region AuditableDataTypes

        public IEnumerable<AuditableDataType> AllDataTypes()
        {
            var list = new List<AuditableDataType>();
            var datatypes = _services.DataTypeService.GetAll();

            var properties = PropsWithDocTypes();


            foreach (var dt in datatypes)
            {
                var adt = new AuditableDataType();
                adt.Name = dt.Name;
                adt.EditorAlias = dt.EditorAlias;
                adt.Guid = dt.Key;
                adt.Id = dt.Id;
                adt.FolderPath = GetFolderContainerPath(dt);

                var matchingProps = properties.Where(p => p.Key.DataTypeId == dt.Id);
                adt.UsedOnProperties = matchingProps;

                list.Add(adt);
            }

            return list;
        }

        private List<string> GetFolderContainerPath(IDataType DataType)
        {
            var folders = new List<string>();
            var ids = DataType.Path.Split(',');

            try
            {
                //The final one is the DataType, so exclude it
                foreach (var sId in ids.Take(ids.Length - 1))
                {
                    if (sId != "-1")
                    {
                        var container = _services.DataTypeService.GetContainer(Convert.ToInt32(sId));
                        folders.Add(container.Name);
                    }
                }
            }
            catch (Exception e)
            {
                folders.Add("~ERROR~");
                var msg = $"Error in 'GetFolderContainerPath()' for DataType {DataType.Id} - '{DataType.Name}'";
                _logger.Error(typeof(SiteAuditorService),e, msg);
            }

            return folders;
        }

        #endregion

        private Dictionary<PropertyType, string> PropsWithDocTypes()
        {
            var properties = new Dictionary<PropertyType, string>();
            var docTypes = _services.ContentTypeService.GetAll();
            foreach (var doc in docTypes)
            {
                foreach (var prop in doc.PropertyTypes)
                {
                    properties.Add(prop, doc.Alias);
                }
            }

            return properties;
        }

    }
}
