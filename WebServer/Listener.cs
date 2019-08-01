using System.Net;

namespace ExampleSimpleWebserver
{
    public class Listener
    {
        public string Url { get; set; }
        public string Port { get; set; }
        private string _link;
        private HttpListener _httpListener;
        public void start()
        {
            _httpListener = new HttpListener();
            _link = "http://" + Url + ":" + Port + "/";
            _httpListener.Prefixes.Add(_link);
            _httpListener.Start();

        }
        public HttpListener GetListener()
        {
            return _httpListener;
        }
    }
    
}
