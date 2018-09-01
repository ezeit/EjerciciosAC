using System.Collections.Generic;
using System.Text.RegularExpressions;
using EPedrini.MINHA2AgoraLog.Interfaces;

namespace EPedrini.MINHA2AgoraLog.Parsers
{
    public class MINHA2AgoraParser : IAgoraLogParser
    {
        private string[] _data;
        private const string _PROVIDER = "MINHA CDN";
        private readonly Dictionary<string, string> _cacheStatusTranslates = new Dictionary<string, string>() { { "INVALIDATE", "REFRESH_HIT" } };
        public void SetData(string data)
        {
            _data = data.Split('|');
        }

        public string GetProvider()
        {
            return _PROVIDER;
        }

        public string GetHttpMethod()
        {
            return _data[3].Split(' ')[0].Substring(1).ToUpper();
        }

        public string GetStatusCode()
        {
            return _data[1];
        }

        public string GetUriPath()
        {
            return _data[3].Split(' ')[1];
        }

        public string GetTimeTaken()
        {
            return Regex.Match(_data[4], @"\d+").Value;
        }

        public string GetResponseSize()
        {
            return _data[0];
        }

        public string GetCacheStatus()
        {
            return _cacheStatusTranslates.ContainsKey(_data[2].ToUpper()) ? _cacheStatusTranslates[_data[2].ToUpper()] : _data[2].ToUpper();
        }
    }
}
