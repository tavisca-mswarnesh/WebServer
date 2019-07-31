using System.Threading;

namespace ExampleSimpleWebserver
{

    class Program
    {
        static void Main(string[] args)
        {
            var server = new HttpAsyncServer( "http://localhost:8080/" );
            server.RunServer();
            Thread.Sleep(30000);
            server.stop();
        }
    }
}
