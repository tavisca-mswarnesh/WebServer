using System;
using System.IO;
using System.Net;
using System.Text;
using System.Threading;

namespace ExampleSimpleWebserver
{
    class HttpAsyncServer
    {
        private string listenedAddress;
        private bool isWorked;
        private HttpListener listener;

        public HttpAsyncServer(string listenedAddress)
        {
            this.listenedAddress = listenedAddress;
            isWorked = false;
        }

        

        private void work()
        {
            listener = new HttpListener();
            listener.Prefixes.Add(listenedAddress);

            listener.Start();

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
                        FileStream fileStream = new FileStream(fileName, FileMode.Open, FileAccess.Read);
                        byte[] buffer = File.ReadAllBytes(fileName);
                        fileStream.Read(buffer, 0, Convert.ToInt32(fileStream.Length));
                        context.Response.ContentLength64 = buffer.Length;
                        System.IO.Stream output = context.Response.OutputStream;
                        output.Write(buffer, 0, buffer.Length);
                        output.Close();
                    }
                    catch (Exception)
                    {

                        FileStream fileStream = new FileStream("NotFound.html", FileMode.Open, FileAccess.Read);
                        byte[] buffer = File.ReadAllBytes("NotFound.html");
                        fileStream.Read(buffer, 0, Convert.ToInt32(fileStream.Length));
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
