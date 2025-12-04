using System;
using System.Net;
using System.Text;
using System.IO;
using System.Collections.Generic;

class MyHTTPHandler
{   
    void print_http_request_detail(HttpListenerRequest request)
    {
        string client_address=request.RemoteEndPoint.Address.ToString();
        int client_port=request.RemoteEndPoint.Port;
        string request_command=request.HttpMethod;
        string request_line=request.RawUrl;
        string request_path=request.Url.AbsolutePath;
        string request_version=request.ProtocolVersion.ToString();
    
        Console.WriteLine("::Client address   : {0}", client_address);
        Console.WriteLine("::Client port      : {0}", client_port);
        Console.WriteLine("::Request command  : {0}", request_command);
        Console.WriteLine("::Request line     : {0}", request_line);
        Console.WriteLine("::Request path     : {0}", request_path);
        Console.WriteLine("::Request version  : {0}", request_version);
    }

    void send_http_response_header(HttpListenerResponse response)
    {
        response.StatusCode = 200;
        response.ContentType = "text/html";
    }

    public void do_GET(HttpListenerRequest request, HttpListenerResponse response)
    {
        Console.WriteLine(" do_GET() activated.");
        
        print_http_request_detail(request);
        send_http_response_header(response);
       
        string path = request.Url.Query;
        if (!string.IsNullOrEmpty(path)){
            path = path.Substring(1);
            int[] parameters = parameter_retrieval(path);
            int result = simple_calc(parameters[0], parameters[1]);

            string html = "<html>";
            string get_response = string.Format("GET request for calculation => {0} x {1} = {2}", parameters[0], parameters[1], result);
            html += get_response;
            html += "</html>";

            byte[] buffer = Encoding.UTF8.GetBytes(html);
            response.ContentLength64 = buffer.Length;
            response.OutputStream.Write(buffer, 0, buffer.Length);
            response.OutputStream.Close();
            Console.WriteLine(" GET request for calculation => {0} x {1} = {2}.", parameters[0], parameters[1], result);
        }
        else
        {
            string html = "<html>";
            string get_response = string.Format("<p>HTTP Request GET for Path: {0}</p>", request.Url.AbsolutePath);
            html += get_response;
            html += "</html>";

            byte[] buffer = Encoding.UTF8.GetBytes(html);
            response.ContentLength64 = buffer.Length;
            response.OutputStream.Write(buffer, 0, buffer.Length);
            response.OutputStream.Close();
            Console.WriteLine("## GET request for directory => {0}.", request.Url.AbsolutePath);
        }
    }

    public void do_POST(HttpListenerRequest request, HttpListenerResponse response)
    {
        Console.WriteLine(" do_Post() activated.");
        print_http_request_detail(request);
        send_http_response_header(response);

        long contentLength = request.ContentLength64;
        byte[] readbuffer = new byte[contentLength];
        request.InputStream.Read(readbuffer, 0, readbuffer.Length);
        string post_data = Encoding.UTF8.GetString(readbuffer);
        int[] parameter = parameter_retrieval(post_data);
        int result = simple_calc(parameter[0], parameter[1]);

        string post_response = string.Format("POST request for calculation => {0} x {1} = {2}" ,parameter[0], parameter[1], result);
        byte[] buffer = Encoding.UTF8.GetBytes(post_response);
        response.ContentLength64 = buffer.Length;
        response.OutputStream.Write(buffer, 0, buffer.Length);
        response.OutputStream.Close();
        Console.WriteLine("POST request for calculation => {0} x {1} = {2}.", parameter[0], parameter[1], result);
    }

    int simple_calc(int para1, int para2)
    {
        return para1*para2;
    }

    int[] parameter_retrieval(string msg)
    {
        int[] result = new int[2];
        string[] fields = msg.Split('&');
        result[0]= int.Parse(fields[0].Split('=')[1]);
        result[1]= int.Parse(fields[1].Split('=')[1]);
        return result;
    }
}

class MainProgram
{
    static void Main(string[] args)
    {
        string server_name = "localhost";
        int server_port = 8080;

        HttpListener listener = new HttpListener();
        listener.Prefixes.Add(string.Format("http://{0}:{1}/", server_name, server_port));
        listener.Start();
        Console.WriteLine("HTTP server started at http://{0}:{1}/", server_name, server_port);
        MyHTTPHandler Handler = new MyHTTPHandler();
        var context = listener.GetContextAsync();
        while (true)
        {
            if (Console.KeyAvailable)
            {
                break;
            }
            if(context.IsCompleted) {
            var result = context.Result;
            var request = result.Request;
            var response = result.Response;
            
            if (request.HttpMethod == "GET")
            {
                Handler.do_GET(request, response);
            }
            else if (request.HttpMethod == "POST")
            {
                Handler.do_POST(request, response);
            }          
                context = listener.GetContextAsync();
            }                 
        }

        listener.Stop();
        Console.WriteLine("Server stopped.");
    }
}
