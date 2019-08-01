using System.Threading;

namespace ExampleSimpleWebserver
{
    class ServerApp
    {
        public void StartWebServer(Listener listener)
        {
            var server = new HttpAsyncServer(listener);
            server.RunServer();
            Thread.Sleep(30000);
            server.stop();
        }
    }
}
