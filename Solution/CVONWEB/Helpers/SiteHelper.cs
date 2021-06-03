using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Umbraco.Core.Models.PublishedContent;
using Umbraco.Core.Services;
using Umbraco.Web;

namespace CVOnWeb.Helpers
{
    public class SiteHelper
    {
        internal static T GetNodeValue<T>(IPublishedContent node, string propertyName)
        {
            //var helper = Umbraco.Web.Composing.Current.UmbracoHelper;
            //var home = contentService.GetRootContent().FirstOrDefault();

            if(node.HasValue(propertyName))
            {
                return node.GetProperty(propertyName).Value<T>();
            } else
            {
                return default(T);
            }
        }
        internal static string GetRegistrationConfirmationPageUrl(IContentService contentService)
        {
            var helper = Umbraco.Web.Composing.Current.UmbracoHelper;
            var home = contentService.GetRootContent().FirstOrDefault();

            string url = "/";
            try
            {
                var register = helper.Content(home.Id).Children.Where(x => x.ContentType.Alias == "register").FirstOrDefault().Value<IPublishedContent>("registrationConfirmationPage");
                url = register.Url();
            }
            catch
            { }
            return url;
        }
        internal static string GetNodeUrlByAlias(IContentService contentService, string alias)
        {
            var helper = Umbraco.Web.Composing.Current.UmbracoHelper;
            var home = contentService.GetRootContent().FirstOrDefault();

            string url = "#";
            try
            {
                var node = helper.Content(home.Id).Children(x => x.ContentType.Alias.ToLower() == alias.ToLower()).FirstOrDefault();
                url = node.Url();
            }
            catch
            { }
            return url;
        }
        internal static IPublishedContent GetEmailRepository(IContentService contentService, string templateName)
        {
            var helper = Umbraco.Web.Composing.Current.UmbracoHelper;
            var home = contentService.GetRootContent().FirstOrDefault();

            var settings = helper.Content(home.Id).Children.Where(x => x.ContentType.Alias == "settings").FirstOrDefault();
            var emailTemplates = settings != null ? settings.Children(y => y.ContentType.Alias == "emailTemplates").FirstOrDefault() : null;

            var template = (emailTemplates != null ? emailTemplates.Children(x => x.Name.ToLower() == templateName.ToLower()).FirstOrDefault() : null);
            return template;
        }
    }
}