namespace EPedrini.MINHA2AgoraLog.Interfaces
{
    public interface IAgoraLogParser
    {
        void SetData(string data);
        string GetProvider();
        string GetHttpMethod();
        string GetStatusCode();
        string GetUriPath();
        string GetTimeTaken();
        string GetResponseSize();
        string GetCacheStatus();
    }
}
