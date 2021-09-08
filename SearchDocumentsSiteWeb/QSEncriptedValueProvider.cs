using SearchDocumentsSiteWeb.General;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Mvc;

namespace SearchDocumentsSiteWeb
{
    public class QSEncriptedValueProvider : DictionaryValueProvider<string>
    {
        public QSEncriptedValueProvider(ControllerContext controllerContext)
            : base(GetQSDictionary(controllerContext), Thread.CurrentThread.CurrentCulture)
        {

        }
        [SessionExpireFilter]
        private static IDictionary<string, string> GetQSDictionary(ControllerContext controllerContext)
        {
            var dict = new Dictionary<string, string>();
            var req = controllerContext.HttpContext.Request;
            foreach (var key in req.QueryString.AllKeys.Where(x => x.First() == '$'))
            {
                var value = req.QueryString[key];
                if (value == "")
                {
                    HttpContext ctx = HttpContext.Current;
                    HttpContext.Current.Session.Abandon();
                    ctx.Response.Redirect("~");
                }
                else
                {
                    dict.Add(key.Substring(1), Desencriptador.Desencriptar(value));
                }
            }
            return dict;
        }
    }
}