using System;
using System.Net;
using System.Text;
using System.Threading;

namespace ExampleSimpleWebserver
{
    class HttpAsyncServer
    {
        //private string listenedAddress;
        private bool isWorked;
        private HttpListener listener;
        private Listener _server;
        public HttpAsyncServer(Listener server)
        {
            _server = server;
            isWorked = false;
        }

        

        private void work()
        {
            _server.start();
            listener = _server.GetListener();

            while (isWorked)
            {
                try
                {
                    var context = listener.GetContext();
                    var fileName = context.Request.RawUrl;
                    fileName = fileName.Remove(0, 1);
                    Console.WriteLine(fileName);
                    try
                    {
                        FileParser fileParser = new FileParser(fileName);
                        byte[] buffer =fileParser.Parse();
                        context.Response.ContentLength64 = buffer.Length;
                        System.IO.Stream output = context.Response.OutputStream;
                        output.Write(buffer, 0, buffer.Length);
                        output.Close();
                    }
                    catch (Exception)
                    {

                        FileParser fileParser = new FileParser(fileName);
                        byte[] buffer = fileParser.Parse();
                        context.Response.ContentLength64 = buffer.Length;
                        System.IO.Stream output = context.Response.OutputStream;
                        output.Write(buffer, 0, buffer.Length);
                        output.Close();
                    }
                    

                    
                    
                    
                }
                catch (Exception)
                {
                    
                }
            }
            stop();
        }

        public void stop()
        {
            isWorked = false;
            listener.Stop();
        }


        public void RunServer()
        {
            if (isWorked)
                throw new Exception("server alredy started");

            isWorked = true;

            Timer t = new Timer((thread) =>
            {
                work();
            });
            t.Change(1, Timeout.Infinite);
            Thread.Sleep(10);
        }

    }
    
}
