using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

class MyHTTPHandler
{   
    void print_http_request_detail()
    {
        client_address=context.Request.RemoteEndPoint.Address.ToString();
        client_port=context.Request.RemoteEndPoint.Port;
        request_command=context.Request.HttpMethod;
        request_line=context.Request.RawUrl;
        request_path=context.Request.Url.AbsolutePath;
        request_version=context.Request.ProtocolVersion.ToString();
        
        Console.WriteLine("::Client address   : {0}", client_address);
        Console.WriteLine("::Client port      : {0}", client_port);
        Console.WriteLine("::Request command  : {0}", request_command);
        Console.WriteLine("::Request line     : {0}", request_line);
        Console.WriteLine("::Request path     : {0}", request_path);
        Console.WriteLine("::Request version  : {0}", request_version);
    }

    void send_http_response_header()
    {
        response.StatusCode = 200;
        response.ContentType = "text/html";
        response.ContentLength64 = 0;
        response.OutputStream.Close();
    }

    void do_GET()
    {
        Console.WriteLine(" do_GET() activated.");
        print_http_request_detail();
        send_http_response_header();



    }

    void do_POST()
    {
        Console.WriteLine(" do_Post() activated.");
        print_http_request_detail();
        send_http_response_header();
    }

    void log_message(self)
    {
        
    }

    int simple_calc(int para1, int para2)
    {
        return para1*para2;
    }

    void parameter_retrieval(self, msg)
    {
        result = [];
        fields = msg.split('&');
        result.append(int.Parse(fields[0].split('=')[1]));
        result.append(int.Parse(fields[1].split('=')[1]));
        return result;
    }
}

class MainProgram
{
    static void Main(string[] args)
    {
        server_name = "localhost";
        server_port = 8080;

        HttpListener listener = new HttpListener();
        listener.Prefixes.Add("http://{0}:{1}", server_name, server_port);
        listener.Start();
        Console.WriteLine("## HTTP server started at http://{0}:{1}", server_name, server_port);

        while (true)
        {
            var context = listener.GetContext();
            var request = context.Request;
            var response = context.Response;
            MyHTTPHandler Handler = new MyHTTPHandler();
            if (request.HttpMethod == "GET")
            {
                Handler.do_GET();
            }
            else if (request.HttpMethod == "POST")
            {
                Handler.do_POST();
            }                           
            Handler.log_message();
            response.Close();        
        }
    }

}
