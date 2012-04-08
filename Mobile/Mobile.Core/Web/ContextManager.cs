using System;
using System.Collections.Generic;
using System.Text;
using System.Web;
using System.Web.Caching;
using System.Web.SessionState;

namespace Mobile.Core.Web
{
    [Serializable]
    public static class ContextManager
    {
        public static HttpContext CurrentContext
        {
            get { return HttpContext.Current; }
        }

        public static HttpRequest CurrentRequest
        {
            get { return ContextManager.CurrentContext.Request; }
        }

        public static HttpResponse CurrentResponse
        {
            get { return ContextManager.CurrentContext.Response; }
        }

        public static HttpApplicationState CurrentApplication
        {
            get { return ContextManager.CurrentContext.Application; }
        }

        public static Cache CurrentCache
        {
            get { return ContextManager.CurrentContext.Cache; }
        }

        public static HttpSessionState CurrentSession
        {
            get { return HttpContext.Current.Session; }
        }

        public static HttpServerUtility CurrentServer
        {
            get { return ContextManager.CurrentContext.Server; }
        }

        public static string GetQueryString(string name)
        {
            string result = string.Empty;
            if (CurrentContext != null && CurrentRequest.QueryString[name] != null)
                result = CurrentRequest.QueryString[name].ToString();
            return result;
        }

        public static int GetQueryInt(string name)
        {
            int result = 0;
            if (CurrentContext != null && CurrentRequest.QueryString[name] != null)
                result = Convert.ToInt32(CurrentRequest.QueryString[name].ToString());
            return result;
        }

        public static long GetQueryInt64(string name)
        {
            
            long result = 0;
            if (CurrentContext != null && CurrentRequest.QueryString[name] != null)
                result = Convert.ToInt64(CurrentRequest.QueryString[name].ToString());
            return result;
        }

        public static string UpdateQueryString(string QueryStringKey, string QueryStringValue, string Url = "")
        {
            string NewUrl = Url;
            if (string.IsNullOrEmpty(Url))
            {
                NewUrl = HttpContext.Current.Request.Url.PathAndQuery;
            }
            string PageUrl = NewUrl;
            if (NewUrl.IndexOf("?", 0) >= 0)
                PageUrl = NewUrl.Substring(0, NewUrl.IndexOf("?", 0));

            string NewKey = QueryStringKey + "=" + QueryStringValue;
            string NewQueryString = "";

            string OldQueryString = "";
            if (NewUrl.IndexOf("?", 0) >= 0)
                OldQueryString = NewUrl.Substring(NewUrl.IndexOf("?", 0) + 1);

            if (OldQueryString != "" && QueryStringKey != "")
            {
                string[] ArrayQuery = OldQueryString.Split('&');
                for (int i = 0; i < ArrayQuery.Length; i++)
                {
                    if (string.Compare(QueryStringKey, ArrayQuery[i].Substring(0, ArrayQuery[i].LastIndexOf("=")), true) == 0)
                    {
                        //NewQueryString += NewKey;
                    }
                    else
                    {
                        if (NewQueryString != "")
                            NewQueryString += "&";

                        NewQueryString += ArrayQuery[i];
                    }
                }


                if (QueryStringValue != "")
                {
                    if (NewQueryString != "")
                        NewQueryString += "&";

                    NewQueryString += NewKey;
                }


                if (NewQueryString != "")
                    NewUrl = PageUrl + "?" + NewQueryString;
                else
                    NewUrl = PageUrl;

            }
            else
            {
                if (QueryStringKey != "" && QueryStringValue != "")
                {
                    NewUrl = PageUrl + "?" + NewKey;
                }
            }
            return NewUrl;
        }

        public static string ApplicationVRoot()
        {
            if (HttpContext.Current.Request.ApplicationPath == "/")
                return "";
            else
                return HttpContext.Current.Request.ApplicationPath;
        }
    }
}
