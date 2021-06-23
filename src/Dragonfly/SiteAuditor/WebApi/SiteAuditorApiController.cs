﻿namespace Dragonfly.SiteAuditor.WebApi
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net.Http;
    using System.Text;
    using System.Web;
    using System.Web.Mvc;
    using Dragonfly.NetModels;
    using Dragonfly.SiteAuditor.Models;
    using Dragonfly.UmbracoHelpers;

    using Newtonsoft.Json;
    using Umbraco.Web.WebApi;

    // /Umbraco/Api/SiteAuditorApi <-- UmbracoApiController
    // /Umbraco/backoffice/Api/SiteAuditorApi <-- UmbracoAuthorizedApiController [IsBackOffice]


    /// <inheritdoc />
    //[PluginController("SiteAuditor")]
    // [UmbracoUserTimeoutFilter]
    [IsBackOffice]
    public class SiteAuditorApiController : UmbracoAuthorizedApiController
    {
        /// /Umbraco/backoffice/Api/SiteAuditorApi/Help
        [System.Web.Http.AcceptVerbs("GET")]
        public HttpResponseMessage Help()
        {
            var returnSB = new StringBuilder();

            returnSB.AppendLine("<h1>Site Auditor</h1>");

            returnSB.AppendLine("<h2>Content</h2>");

            returnSB.AppendLine("<h3>All Content Nodes</h3>");
            returnSB.AppendLine("<p>These will take a long time to run for large sites. Please be patient.</p>");
            returnSB.AppendLine("<ul>");
            returnSB.AppendLine("<li><a target=\"_blank\" href=\"/Umbraco/backoffice/Api/SiteAuditorApi/GetAllContentAsXml\">Get All Content As Xml</a> [no parameters]</li>");
            returnSB.AppendLine("<li><a target=\"_blank\" href=\"/Umbraco/backoffice/Api/SiteAuditorApi/GetAllContentAsJson\">Get All Content As Json</a> [no parameters]</li>");
            returnSB.AppendLine("<li><a target=\"_blank\" href=\"/Umbraco/backoffice/Api/SiteAuditorApi/GetAllContentAsHtml\">Get All Content As HtmlTable</a> [no parameters]</li>");
            returnSB.AppendLine("<li><a target=\"_blank\" href=\"/Umbraco/backoffice/Api/SiteAuditorApi/GetAllContentAsCsv\">Get All Content As Csv</a> [no parameters]</li>");
            returnSB.AppendLine("</ul>");

            returnSB.AppendLine("<h3>Content Nodes with Property Data</h3>");
            //returnSB.AppendLine("<p>Note</p>");
            returnSB.AppendLine("<ul>");
            returnSB.AppendLine("<li><a target=\"_blank\" href=\"/Umbraco/backoffice/Api/SiteAuditorApi/GetContentWithValues\">Get Content With Values</a></li>");
            returnSB.AppendLine("</ul>");


            returnSB.AppendLine("<h2>Document Types</h2>");

            returnSB.AppendLine("<h3>All DocTypes</h3>");
            //returnSB.AppendLine("<p>Note</p>");
            returnSB.AppendLine("<ul>");
            returnSB.AppendLine("<li><a target=\"_blank\" href=\"/Umbraco/backoffice/Api/SiteAuditorApi/GetAllDocTypesAsXml\">Get All Doctypes As Xml</a> [no parameters]</li>");
            returnSB.AppendLine("<li><a target=\"_blank\" href=\"/Umbraco/backoffice/Api/SiteAuditorApi/GetAllDocTypesAsJson\">Get All Doctypes As Json</a> [no parameters]</li>");
            returnSB.AppendLine("<li><a target=\"_blank\" href=\"/Umbraco/backoffice/Api/SiteAuditorApi/GetAllDocTypesAsHtml\">Get All Doctypes As Html</a> [no parameters]</li>");
            returnSB.AppendLine("<li><a target=\"_blank\" href=\"/Umbraco/backoffice/Api/SiteAuditorApi/GetAllDocTypesAsCsv\">Get All Doctypes As Csv</a> [no parameters]</li>");
            returnSB.AppendLine("</ul>");

            returnSB.AppendLine("<h2>Document Type Properties</h2>");

            returnSB.AppendLine("<h3>All Properties</h3>");
            //returnSB.AppendLine("<p>Note</p>");
            returnSB.AppendLine("<ul>");
            returnSB.AppendLine("<li><a target=\"_blank\" href=\"/Umbraco/backoffice/Api/SiteAuditorApi/GetAllPropertiesAsXml\">Get All Properties As Xml</a> [no parameters]</li>");
            returnSB.AppendLine("<li><a target=\"_blank\" href=\"/Umbraco/backoffice/Api/SiteAuditorApi/GetAllPropertiesAsJson\">Get All Properties As Json</a> [no parameters]</li>");
            returnSB.AppendLine("<li><a target=\"_blank\" href=\"/Umbraco/backoffice/Api/SiteAuditorApi/GetAllPropertiesAsHtml\">Get All Properties As Html</a> [no parameters]</li>");
            returnSB.AppendLine("<li><a target=\"_blank\" href=\"/Umbraco/backoffice/Api/SiteAuditorApi/GetAllPropertiesAsCsv\">Get All Properties As Csv</a> [no parameters]</li>");
            returnSB.AppendLine("</ul>");

            returnSB.AppendLine("<h2>Data Types</h2>");

            returnSB.AppendLine("<h3>All DataTypes</h3>");
            //returnSB.AppendLine("<p>Note</p>");
            returnSB.AppendLine("<ul>");
            returnSB.AppendLine("<li><a target=\"_blank\" href=\"/Umbraco/backoffice/Api/SiteAuditorApi/GetAllDataTypesAsXml\">Get All DataTypes As Xml</a> [no parameters]</li>");
            returnSB.AppendLine("<li><a target=\"_blank\" href=\"/Umbraco/backoffice/Api/SiteAuditorApi/GetAllDataTypesAsJson\">Get All DataTypes As Json</a> [no parameters]</li>");
            returnSB.AppendLine("<li><a target=\"_blank\" href=\"/Umbraco/backoffice/Api/SiteAuditorApi/GetAllDataTypesAsHtml\">Get All DataTypes As Html</a> [no parameters]</li>");
            returnSB.AppendLine("<li><a target=\"_blank\" href=\"/Umbraco/backoffice/Api/SiteAuditorApi/GetAllDataTypesAsCsv\">Get All DataTypes As Csv</a> [no parameters]</li>");
            returnSB.AppendLine("</ul>");


           
            //returnSB.AppendLine("<h3>All Content Nodes</h3>");
            //returnSB.AppendLine("<p>Note</p>");
            //returnSB.AppendLine("<ul>");
            //returnSB.AppendLine("<li><a target=\"_blank\" href=\"\"></a></li>");
            //returnSB.AppendLine("</ul>");

            return new HttpResponseMessage()
            {
                Content = new StringContent(
                    returnSB.ToString(),
                    Encoding.UTF8,
                    "text/html"
                )
            };
        }

        private SiteAuditorService GetSiteAuditorService()
        {
            return new SiteAuditorService(Umbraco, UmbracoContext, Services, Logger);
        }
        
        #region Content Nodes

        /// /Umbraco/backoffice/Api/SiteAuditorApi/GetAllContentAsXml
        [System.Web.Http.AcceptVerbs("GET")]
        public List<AuditableContent> GetAllContentAsXml()
        {
            var saService = GetSiteAuditorService();
            var allNodes = saService.GetContentNodes();

            return allNodes;
        }

        /// /Umbraco/backoffice/Api/SiteAuditorApi/GetAllContentAsJson
        [System.Web.Http.AcceptVerbs("GET")]
        public HttpResponseMessage GetAllContentAsJson()
        {
            var saService = GetSiteAuditorService();
            var returnSB = new StringBuilder();

            var allNodes = saService.GetContentNodes();

            string json = JsonConvert.SerializeObject(allNodes);

            returnSB.AppendLine(json);

            return new HttpResponseMessage()
            {
                Content = new StringContent(
                    returnSB.ToString(),
                    Encoding.UTF8,
                    "application/json"
                )
            };
        }

        /// /Umbraco/backoffice/Api/SiteAuditorApi/GetAllContentAsHtmlTable
        [System.Web.Http.AcceptVerbs("GET")]
        public HttpResponseMessage GetAllContentAsHtmlTable()
        {
            var returnSB = new StringBuilder();
            var saService = GetSiteAuditorService();

            var pvPath = "/App_Plugins/Dragonfly.SiteAuditor/Views/AllContentAsHtmlTable.cshtml"; // _TesterConfig.GetAppPluginsPath() + "Views/Start.cshtml";
            
            var allNodes = saService.GetContentNodes();

            //var tableStart = @"
            //    <table>
            //        <tr>
            //            <th>Node Name</th>
            //            <th>NodeID</th>
            //            <th>Node Path</th>
            //            <th>DocType</th>
            //            <th>ParentID</th>
            //            <th>Full URL</th>
            //            <th>Level</th>
            //            <th>Sort Order</th>
            //            <th>Template Name</th>
            //            <th>Create Date</th>
            //            <th>Update Date</th>
            //        </tr>";

            //var tableEnd = @"</table>";

            //var tableData = new StringBuilder();

            //foreach (var auditNode in allNodes)
            //{
            //    tableData.AppendLine("<tr>");

            //    tableData.AppendLine(string.Format("<td>{0}</td>", auditNode.UmbContentNode.Name));
            //    tableData.AppendLine(string.Format("<td>{0}</td>", auditNode.UmbContentNode.Id));
            //    tableData.AppendLine(string.Format("<td>{0}</td>", auditNode.NodePathAsText));
            //    tableData.AppendLine(string.Format("<td>{0}</td>", auditNode.UmbContentNode.ContentType.Alias));
            //    tableData.AppendLine(string.Format("<td>{0}</td>", auditNode.UmbContentNode.ParentId));
            //    tableData.AppendLine(string.Format("<td><a href=\"{0}\" target=\"_blank\">{0}</a></td>", auditNode.FullNiceUrl));
            //    tableData.AppendLine(string.Format("<td>{0}</td>", auditNode.UmbContentNode.Level));
            //    tableData.AppendLine(string.Format("<td>{0}</td>", auditNode.UmbContentNode.SortOrder));
            //    tableData.AppendLine(string.Format("<td>{0}</td>", auditNode.TemplateAlias));
            //    tableData.AppendLine(string.Format("<td>{0}</td>", auditNode.UmbContentNode.CreateDate));
            //    tableData.AppendLine(string.Format("<td>{0}</td>", auditNode.UmbContentNode.UpdateDate));

            //    tableData.AppendLine("</tr>");
            //}

            //returnSB.AppendLine(tableStart);
            //returnSB.Append(tableData);
            //returnSB.AppendLine(tableEnd);
            //VIEW DATA 
            var viewData = new ViewDataDictionary();
            viewData.Model = allNodes;
            //viewData.Add("FilesList", dataTypes);
            //viewData.Add("SpecialMessage", specialMessage);
            //viewData.Add("SpecialMessageClass", specialMessageClass);
            //viewData.Add("DeleteOptions", FilesIO.GetFilesOptions());

            //RENDER
            try
            {
                var controllerContext = this.ControllerContext;
                var displayHtml =
                    ApiControllerHtmlHelper.GetPartialViewHtml(controllerContext, pvPath, viewData, HttpContext.Current);
                returnSB.AppendLine(displayHtml);
            }
            catch (System.ArgumentNullException eNull)
            {
                if (eNull.Message.Contains("Parameter name: view"))
                {
                    throw new ArgumentNullException($"The View file '{pvPath}' is missing and cannot be rendered.", eNull);
                }
                else
                {
                    throw;
                }
            }
            catch (Exception e)
            { throw; }

            //RETURN AS HTML
            return new HttpResponseMessage()
            {
                Content = new StringContent(
                    returnSB.ToString(),
                    Encoding.UTF8,
                    "text/html"
                )
            };
        }

        /// /Umbraco/backoffice/Api/SiteAuditorApi/GetAllContentAsCsv
        [System.Web.Http.AcceptVerbs("GET")]
        public HttpResponseMessage GetAllContentAsCsv()
        {
            var saService = GetSiteAuditorService();
            var returnSB = new StringBuilder();

            var allNodes = saService.GetContentNodes();

            var tableData = new StringBuilder();

            tableData.AppendLine(
                "\"Node Name\",\"NodeID\",\"Node Path\",\"DocType\",\"ParentID\",\"Full URL\",\"Level\",\"Sort Order\",\"Template Name\",\"Create Date\",\"Update Date\"");

            foreach (var auditNode in allNodes)
            {
                tableData.AppendFormat(
                    "\"{0}\",{1},\"{2}\",\"{3}\",{4},\"{5}\",{6},{7},\"{8}\",{9},{10}{11}",
                    auditNode.UmbContentNode.Name,
                    auditNode.UmbContentNode.Id,
                    auditNode.NodePathAsCustomText(" > "),
                    auditNode.UmbContentNode.ContentType.Alias,
                    auditNode.UmbContentNode.ParentId,
                    auditNode.FullNiceUrl,
                    auditNode.UmbContentNode.Level,
                    auditNode.UmbContentNode.SortOrder,
                    auditNode.TemplateAlias,
                    auditNode.UmbContentNode.CreateDate,
                    auditNode.UmbContentNode.UpdateDate,
                    Environment.NewLine);
            }
            returnSB.Append(tableData);

            return Dragonfly.NetHelpers.Http.StringBuilderToFile(returnSB, "AllNodes.csv");
        }

        #endregion

        #region GetContentWithValues

        /// /Umbraco/backoffice/Api/SiteAuditorApi/GetContentWithValues?PropertyAlias=xxx&IncludeUnpublished=false
        [System.Web.Http.AcceptVerbs("GET")]
        public HttpResponseMessage GetContentWithValues(string PropertyAlias, bool IncludeUnpublished = false)
        {
            var saService = GetSiteAuditorService();
            var returnSB = new StringBuilder();
            var fancyFormat = true;

            var pvPath = "~/App_Plugins/Dragonfly.SiteAuditor/Views/ContentWithValuesTable.cshtml";

            //FIND NODES TO DISPLAY
            var allNodes = saService.GetAllNodes().ToList();
            var nodesWithValue = allNodes.Where(n => n.Properties.Where(p => p.Alias == PropertyAlias && p.HasValue()).Any()).ToList();

            //VIEW DATA 
            var viewData = new ViewDataDictionary();
            viewData.Model = nodesWithValue;
            viewData.Add("PropertyAlias", PropertyAlias);
            viewData.Add("IncludeUnpublished", IncludeUnpublished);

            //RENDER
            var controllerContext = this.ControllerContext;
            var displayHtml = ApiControllerHtmlHelper.GetPartialViewHtml(controllerContext, pvPath, viewData, HttpContext.Current);
            returnSB.AppendLine(displayHtml);

            return new HttpResponseMessage()
            {
                Content = new StringContent(
                    returnSB.ToString(),
                    Encoding.UTF8,
                    "text/html"
                )
            };
        }

        /// /Umbraco/backoffice/Api/SiteAuditorApi/GetContentWithValues
        [System.Web.Http.AcceptVerbs("GET")]
        public HttpResponseMessage GetContentWithValues()
        {
            var saService = GetSiteAuditorService();
            var returnSB = new StringBuilder();

            //BUILD HTML
            var allSiteDocTypes = saService.GetAllDocTypes();

            var allProps = allSiteDocTypes.SelectMany(n => n.PropertyTypes);

            var allPropsAliases = allProps.Select(n => n.Alias).Distinct();

            //BUILD HTML
            returnSB.AppendLine($"<h1>Get Content with Values</h1>");
            returnSB.AppendLine($"<h3>Available Properties</h3>");
            returnSB.AppendLine("<p>Note: Choosing the 'All' option will take significantly longer to load than the 'Published' option because we need to bypass the cache and query the database directly.</p>");

            returnSB.AppendLine("<ul>");

            foreach (var propAlias in allPropsAliases.OrderBy(n => n))
            {
                var url1 =
                    $"/Umbraco/backoffice/Api/SiteAuditorApi/GetContentWithValues?PropertyAlias={propAlias}&IncludeUnpublished=false";
                var url2 =
                    $"/Umbraco/backoffice/Api/SiteAuditorApi/GetContentWithValues?PropertyAlias={propAlias}&IncludeUnpublished=true";

                returnSB.AppendLine($"<li>{propAlias} <a target=\"_blank\" href=\"{url1}\">Published</a> | <a target=\"_blank\" href=\"{url2}\">All</a></li>");
            }

            returnSB.AppendLine("</ul>");

            return new HttpResponseMessage()
            {
                Content = new StringContent(
                    returnSB.ToString(),
                    Encoding.UTF8,
                    "text/html"
                )
            };
        }

        #endregion

        #region Properties Info

        // /Umbraco/backoffice/Api/SiteAuditorApi/GetAllPropertiesAsXml
        [System.Web.Http.AcceptVerbs("GET")]
        public IEnumerable<AuditableProperty> GetAllPropertiesAsXml()
        {
            var saService = GetSiteAuditorService();
            var siteProps = saService.AllProperties();
            var propertiesList = siteProps.AllProperties;
            return propertiesList;
        }

        /// /Umbraco/backoffice/Api/SiteAuditorApi/GetAllPropertiesAsJson
        [System.Web.Http.AcceptVerbs("GET")]
        public HttpResponseMessage GetAllPropertiesAsJson()
        {
            var saService = GetSiteAuditorService();
            var returnSB = new StringBuilder();

            var siteProps = saService.AllProperties();
            var propertiesList = siteProps.AllProperties;
            string json = JsonConvert.SerializeObject(propertiesList);

            returnSB.AppendLine(json);

            return new HttpResponseMessage()
            {
                Content = new StringContent(
                    returnSB.ToString(),
                    Encoding.UTF8,
                    "application/json"
                )
            };
        }

        // /Umbraco/backoffice/Api/SiteAuditorApi/GetAllPropertiesAsHtml
        [System.Web.Http.AcceptVerbs("GET")]
        public HttpResponseMessage GetAllPropertiesAsHtmlTable()
        {
            var saService = GetSiteAuditorService();
            var returnSB = new StringBuilder();

            var siteProps = saService.AllProperties();
            var propertiesList = siteProps.AllProperties;

            var tableStart = @"
                <table>
                    <tr>
                        <th>Property Name</th>
                        <th>Property Alias</th>
                        <th>DataType Name</th>
                        <th>DataType Property Editor</th>
                        <th>DataType Database Type</th>
                        <th>DocumentTypes Used In</th>
                        <th>Qty of DocumentTypes</th>
                    </tr>";

            var tableEnd = @"</table>";

            var tableData = new StringBuilder();

            foreach (var prop in propertiesList)
            {
                tableData.AppendLine("<tr>");

                tableData.AppendLine(string.Format("<td>{0}</td>", prop.UmbPropertyType.Name));
                tableData.AppendLine(string.Format("<td>{0}</td>", prop.UmbPropertyType.Alias));
                tableData.AppendLine(string.Format("<td>{0}</td>", prop.DataType.Name));
                tableData.AppendLine(string.Format("<td>{0}</td>", prop.DataType.EditorAlias));
                tableData.AppendLine(string.Format("<td>{0}</td>", prop.DataType.DatabaseType));
                tableData.AppendLine(string.Format("<td>{0}</td>", string.Join(", ", prop.AllDocTypes.Select(n => n.DocTypeAlias))));
                tableData.AppendLine(string.Format("<td>{0}</td>", prop.AllDocTypes.Count()));

                tableData.AppendLine("</tr>");
            }

            returnSB.AppendLine(tableStart);
            returnSB.Append(tableData);
            returnSB.AppendLine(tableEnd);

            return new HttpResponseMessage()
            {
                Content = new StringContent(
                    returnSB.ToString(),
                    Encoding.UTF8,
                    "text/html"
                )
            };
        }


        /// /Umbraco/backoffice/Api/SiteAuditorApi/GetAllPropertiesAsCsv
        [System.Web.Http.AcceptVerbs("GET")]
        public HttpResponseMessage GetAllPropertiesAsCsv()
        {
            var saService = GetSiteAuditorService();
            var returnSB = new StringBuilder();

            var siteProps = saService.AllProperties();
            var propertiesList = siteProps.AllProperties;

            var tableData = new StringBuilder();

            tableData.AppendLine(
                "\"Property Name\",\"Property Alias\",\"DataType Name\",\"DataType Property Editor\",\"DataType Database Type\",\"DocumentTypes Used In\",\"Qty of DocumentTypes\"");

            foreach (var prop in propertiesList)
            {
                tableData.AppendFormat(
                    "\"{0}\",\"{1}\",\"{2}\",\"{3}\",\"{4}\",\"{5}\",{6}{7}",
                    prop.UmbPropertyType.Name,
                    prop.UmbPropertyType.Alias,
                    prop.DataType.Name,
                    prop.DataType.EditorAlias,
                    prop.DataType.DatabaseType,
                    string.Join(", ", prop.AllDocTypes.Select(n => n.DocTypeAlias)),
                    prop.AllDocTypes.Count(),
                    Environment.NewLine);
            }

            returnSB.Append(tableData);

            return Dragonfly.NetHelpers.Http.StringBuilderToFile(returnSB, "AllProperties.csv");
        }

        //public IHttpActionResult GetProperty(string PropertyAlias)
        //{
        //    var AllPropertiesList = GetAllProperties();
        //    var property = AllPropertiesList.FirstOrDefault((p) => p.Alias == PropertyAlias);
        //    if (property == null)
        //    {
        //        return NotFound();
        //    }
        //    return Ok(property);
        //}

        #endregion

        #region DataType Info

        // /Umbraco/backoffice/Api/SiteAuditorApi/GetAllDataTypesAsXml
        [System.Web.Http.AcceptVerbs("GET")]
        public IEnumerable<AuditableDataType> GetAllDataTypesAsXml()
        {
            var saService = GetSiteAuditorService();
            var dataTypes = saService.AllDataTypes();

            return dataTypes;
        }

        /// /Umbraco/backoffice/Api/SiteAuditorApi/GetAllDataTypesAsJson
        [System.Web.Http.AcceptVerbs("GET")]
        public HttpResponseMessage GetAllDataTypesAsJson()
        {
            var saService = GetSiteAuditorService();
            var returnSB = new StringBuilder();

            var dataTypes = saService.AllDataTypes();
            string json = JsonConvert.SerializeObject(dataTypes);

            returnSB.AppendLine(json);

            return new HttpResponseMessage()
            {
                Content = new StringContent(
                    returnSB.ToString(),
                    Encoding.UTF8,
                    "application/json"
                )
            };
        }

        // /Umbraco/backoffice/Api/SiteAuditorApi/GetAllDataTypesAsHtml
        [System.Web.Http.AcceptVerbs("GET")]
        public HttpResponseMessage GetAllDataTypesAsHtmlTable()
        {
            var pvPath = "/App_Plugins/Dragonfly.SiteAuditor/Views/AllDataTypesAsHtmlTable.cshtml"; // _TesterConfig.GetAppPluginsPath() + "Views/Start.cshtml";

            //var returnStatusMsg = new StatusMessage(true); //assume success
            var saService = GetSiteAuditorService();
            var returnSB = new StringBuilder();

            var dataTypes = saService.AllDataTypes();

           //VIEW DATA 
            var viewData = new ViewDataDictionary();
            viewData.Model = dataTypes;
            //viewData.Add("FilesList", dataTypes);
            //viewData.Add("SpecialMessage", specialMessage);
            //viewData.Add("SpecialMessageClass", specialMessageClass);
            //viewData.Add("DeleteOptions", FilesIO.GetFilesOptions());

            //RENDER
            try
            {
                var controllerContext = this.ControllerContext;
                var displayHtml =
                    ApiControllerHtmlHelper.GetPartialViewHtml(controllerContext, pvPath, viewData, HttpContext.Current);
                returnSB.AppendLine(displayHtml);
            }
            catch (System.ArgumentNullException eNull)
            {
                if (eNull.Message.Contains("Parameter name: view"))
                {
                    throw new ArgumentNullException($"The View file '{pvPath}' is missing and cannot be rendered.",eNull);
                }
                else
                {
                    throw;
                }
            }
            catch(Exception e)
            { throw; }

            //RETURN AS HTML
            return new HttpResponseMessage()
            {
                Content = new StringContent(
                    returnSB.ToString(),
                    Encoding.UTF8,
                    "text/html"
                )
            };
        }


        /// /Umbraco/backoffice/Api/SiteAuditorApi/GetAllDataTypesAsCsv
        [System.Web.Http.AcceptVerbs("GET")]
        public HttpResponseMessage GetAllDataTypesAsCsv()
        {
            var saService = GetSiteAuditorService();
            var returnSB = new StringBuilder();

            var dataTypes = saService.AllDataTypes();

            var tableData = new StringBuilder();

            tableData.AppendLine(
                "\"DataType Name\",\"Property Editor Alias\",\"Id\",\"Guid Key\",\"Used On Properties\",\"Qty of Properties\"");

            foreach (var dt in dataTypes)
            {
                tableData.AppendFormat(
                    "\"{0}\",\"{1}\",{2},\"{3}\",\"{4}\",{5}{6}",
                    dt.Name,
                    dt.EditorAlias,
                    dt.Id,
                    dt.Guid.ToString(),
                    string.Join(" | ", dt.UsedOnProperties.Select(n => $"{n.Value} [{n.Key.Alias}]")),
                    dt.UsedOnProperties.Count(),
                    Environment.NewLine);
            }

            returnSB.Append(tableData);

            return Dragonfly.NetHelpers.Http.StringBuilderToFile(returnSB, "AllDataTypes.csv");
        }

        ///// /Umbraco/backoffice/Api/SiteAuditorApi/ListAllDataTypes?ForExport=false
        //[System.Web.Http.AcceptVerbs("GET")]
        //public StatusMessage ListAllDataTypes(bool ForExport)
        //{
        //    var returnMsg = new StatusMessage();
        //    var msgSB = new StringBuilder();

        //    var datatypeService = this.Services.DataTypeService;

        //    var datatypes = datatypeService.GetAll();

        //    if (ForExport)
        //    {
        //        msgSB.AppendLine(" ");
        //        msgSB.AppendLine(string.Format("{0}|{1}|{2}", "Name", "Property Editor Alias", "GUID"));
        //    }
        //    else
        //    {
        //        msgSB.AppendLine(string.Format("{0} [{1}] {2}", "Name", "Property Editor Alias", "GUID"));
        //    }


        //    foreach (var dt in datatypes)
        //    {
        //        if (ForExport)
        //        {
        //            msgSB.AppendLine(string.Format("{0}|{1}|{2}", dt.Name, dt.EditorAlias, dt.Key));
        //        }
        //        else
        //        {
        //            msgSB.AppendLine(string.Format("{0} [{1}] {2}", dt.Name, dt.EditorAlias, dt.Key));
        //        }
        //    }

        //    returnMsg.Message = msgSB.ToString();
        //    returnMsg.Success = true;
        //    return returnMsg;
        //}


        #endregion

        #region DocTypes Info

        /// /Umbraco/backoffice/Api/SiteAuditorApi/GetAllDocTypesAsXml
        [System.Web.Http.AcceptVerbs("GET")]
        public IEnumerable<AuditableDocType> GetAllDocTypesAsXml()
        {
            var saService = GetSiteAuditorService();
            var allDts = saService.GetAuditableDocTypes();

            return allDts;
        }

        /// /Umbraco/backoffice/Api/SiteAuditorApi/GetAllDocTypesAsJson
        [System.Web.Http.AcceptVerbs("GET")]
        public HttpResponseMessage GetAllDocTypesAsJson()
        {
            var saService = GetSiteAuditorService();
            var returnSB = new StringBuilder();

            var allDts = saService.GetAuditableDocTypes();

            string json = JsonConvert.SerializeObject(allDts);

            returnSB.AppendLine(json);

            return new HttpResponseMessage()
            {
                Content = new StringContent(
                    returnSB.ToString(),
                    Encoding.UTF8,
                    "application/json"
                )
            };
        }

        /// /Umbraco/backoffice/Api/SiteAuditorApi/GetAllDocTypesAsHtml
        [System.Web.Http.AcceptVerbs("GET")]
        public HttpResponseMessage GetAllDocTypesAsHtmlTable()
        {
            var saService = GetSiteAuditorService();
            var returnSB = new StringBuilder();

            var allDts = saService.GetAuditableDocTypes();

            var tableStart = @"
                <table>
                    <tr>
                        <th>Doctype Name</th>
                        <th>Alias</th>
                        <th>Default Template</th>
                        <th>Id</th>
                        <th>GUID</th>
                        <th>Create Date</th>
                        <th>Update Date</th>
                    </tr>";

            var tableEnd = @"</table>";

            var tableData = new StringBuilder();

            foreach (var item in allDts)
            {
                tableData.AppendLine("<tr>");

                tableData.AppendLine(string.Format("<td>{0}</td>", item.Name));
                tableData.AppendLine(string.Format("<td>{0}</td>", item.Alias));
                tableData.AppendLine(string.Format("<td>{0}</td>", item.DefaultTemplateName));
                tableData.AppendLine(string.Format("<td>{0}</td>", item.Id));
                tableData.AppendLine(string.Format("<td>{0}</td>", item.GUID));
                tableData.AppendLine(string.Format("<td>{0}</td>", item.ContentType.CreateDate));
                tableData.AppendLine(string.Format("<td>{0}</td>", item.ContentType.UpdateDate));

                tableData.AppendLine("</tr>");
            }

            returnSB.AppendLine(tableStart);
            returnSB.Append(tableData);
            returnSB.AppendLine(tableEnd);

            return new HttpResponseMessage()
            {
                Content = new StringContent(
                    returnSB.ToString(),
                    Encoding.UTF8,
                    "text/html"
                )
            };
        }

        /// /Umbraco/backoffice/Api/SiteAuditorApi/GetAllDocTypesAsCsv
        [System.Web.Http.AcceptVerbs("GET")]
        public HttpResponseMessage GetAllDocTypesAsCsv()
        {
            var saService = GetSiteAuditorService();
            var returnSB = new StringBuilder();

            var allDts = saService.GetAuditableDocTypes();

            var tableData = new StringBuilder();

            tableData.AppendLine(
                "\"Doctype Name\",\"Alias\",\"Default Template\",\"GUID\",\"Create Date\",\"Update Date\"");

            foreach (var item in allDts)
            {
                tableData.AppendFormat(
                    "\"{0}\",\"{1}\",\"{2}\",\"{3}\",{4},{5}{6}",
                    item.Name,
                    item.Alias,
                    item.DefaultTemplateName,
                    item.GUID,
                    item.ContentType.CreateDate,
                    item.ContentType.UpdateDate,
                    Environment.NewLine);
            }
            returnSB.Append(tableData);

            return Dragonfly.NetHelpers.Http.StringBuilderToFile(returnSB, "AllDoctypes.csv");
        }

        #endregion

 

        #region Tests & Examples

        /// /Umbraco/backoffice/Api/AuthorizedApi/Test
        [System.Web.Http.AcceptVerbs("GET")]
        public bool Test()
        {
            //LogHelper.Info<PublicApiController>("Test STARTED/ENDED");
            return true;
        }

        /// /Umbraco/backoffice/Api/AuthorizedApi/ExampleReturnHtml
        [System.Web.Http.AcceptVerbs("GET")]
        public HttpResponseMessage ExampleReturnHtml()
        {
            var returnSB = new StringBuilder();

            returnSB.AppendLine("<h1>Hello! This is HTML</h1>");
            returnSB.AppendLine("<p>Use this type of return when you want to exclude &lt;XML&gt;&lt;/XML&gt; tags from your output and don\'t want it to be encoded automatically.</p>");

            return new HttpResponseMessage()
            {
                Content = new StringContent(
                    returnSB.ToString(),
                    Encoding.UTF8,
                    "text/html"
                )
            };
        }

        /// /Umbraco/backoffice/Api/AuthorizedApi/ExampleReturnJson
        [System.Web.Http.AcceptVerbs("GET")]
        public HttpResponseMessage ExampleReturnJson()
        {
            var returnSB = new StringBuilder();

            var testData = new StatusMessage(true, "This is a test object so you can see JSON!");
            string json = JsonConvert.SerializeObject(testData);

            returnSB.AppendLine(json);

            return new HttpResponseMessage()
            {
                Content = new StringContent(
                    returnSB.ToString(),
                    Encoding.UTF8,
                    "application/json"
                )
            };
        }

        /// /Umbraco/backoffice/Api/SiteAuditorApi/ExampleReturnCsv
        [System.Web.Http.AcceptVerbs("GET")]
        public HttpResponseMessage ExampleReturnCsv()
        {
            var returnSB = new StringBuilder();
            var tableData = new StringBuilder();

            for (int i = 0; i < 10; i++)
            {
                tableData.AppendFormat(
                    "\"{0}\",{1},\"{2}\",{3}{4}",
                    "Name " + i,
                    i,
                    string.Format("Some text about item #{0} for demo.", i),
                    DateTime.Now,
                    Environment.NewLine);
            }
            returnSB.Append(tableData);

            return Dragonfly.NetHelpers.Http.StringBuilderToFile(returnSB, "Example.csv");
        }

        /// /Umbraco/backoffice/Api/SiteAuditorApi/TestCSV
        [System.Web.Http.AcceptVerbs("GET")]
        public HttpResponseMessage TestCsv()
        {
            var returnSB = new StringBuilder();

            var tableData = new StringBuilder();

            for (int i = 0; i < 10; i++)
            {
                tableData.AppendFormat(
                    "\"{0}\",{1},\"{2}\",{3}{4}",
                    "Name " + i,
                    i,
                    string.Format("Some text about item #{0} for demo.", i),
                    DateTime.Now,
                    Environment.NewLine);
            }
            returnSB.Append(tableData);

            return Dragonfly.NetHelpers.Http.StringBuilderToFile(returnSB, "Test.csv");
        }

        #endregion
    }
}
