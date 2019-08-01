namespace ExampleSimpleWebserver
{

    class Program
    {
        static void Main(string[] args)
        {
            Listener listener = new Listener();
            listener.Url = "localhost";
            listener.Port = "8080";
            ServerApp serverApp = new ServerApp();
            serverApp.StartWebServer(listener);
            
        }
    }
}
