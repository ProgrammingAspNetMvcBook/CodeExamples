using System.Web;

namespace Ebuy
{
    public static class JsonRequestExtensions
    {
        public static bool IsJsonRequest(this HttpRequestBase request)
        {
            return string.Equals(request["format"], "json");
        }
    }
}