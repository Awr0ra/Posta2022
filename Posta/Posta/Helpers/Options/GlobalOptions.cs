namespace Posta.Helpers.Options
{
    public class GlobalOptions
    {
        Proxy Proxy { get; set; }
    }
    public class Proxy
    {
        string Url { get; set; } = "proxy.default";
        bool UseProxy { get; set; } = false;

    }
   
}
