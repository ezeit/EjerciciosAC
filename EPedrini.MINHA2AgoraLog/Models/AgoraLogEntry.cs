using System;
using EPedrini.MINHA2AgoraLog.Interfaces;

namespace EPedrini.MINHA2AgoraLog.Models
{
    class AgoraLogEntry
    {
        public string provider { get; set; }
        public string httpMethod { get; set; }
        public string statusCode { get; set; }
        public string uriPath { get; set; }
        public string timeTaken { get; set; }
        public string responseSize { get; set; }
        public string cacheStatus { get; set; }

        public static bool TryParse(string data, IAgoraLogParser parser, out AgoraLogEntry result)
        {
            result = new AgoraLogEntry();
            try
            {
                parser.SetData(data);
                result.provider = parser.GetProvider();
                result.httpMethod = parser.GetHttpMethod();
                result.statusCode = parser.GetStatusCode();
                result.uriPath = parser.GetUriPath();
                result.timeTaken = parser.GetTimeTaken();
                result.responseSize = parser.GetResponseSize();
                result.cacheStatus = parser.GetCacheStatus();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public new string ToString()
        {
            return $"\"{provider}\" {httpMethod} {statusCode} {uriPath} {timeTaken} {responseSize} {cacheStatus}";
        }
    }
}
