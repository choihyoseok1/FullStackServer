using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

class MyHTTPHandler
{   
    void print_http_request_detail(HttpListenerRequest request)
    {
        client_address=request.RemoteEndPoint.Address.ToString();
        client_port=request.RemoteEndPoint.Port;
        request_command=request.HttpMethod;
        request_line=request.RawUrl;
        request_path=request.Url.AbsolutePath;
        request_version=request.ProtocolVersion.ToString();
        
        Console.WriteLine("::Client address   : {0}", client_address);
        Console.WriteLine("::Client port      : {0}", client_port);
        Console.WriteLine("::Request command  : {0}", request_command);
        Console.WriteLine("::Request line     : {0}", request_line);
        Console.WriteLine("::Request path     : {0}", request_path);
        Console.WriteLine("::Request version  : {0}", request_version);
    }

    void send_http_response_header(HttpListenerRequest response)
    {
        response.StatusCode = 200;
        response.ContentType = "text/html";
    }

    void do_GET(HttpListenerRequest request, HttpListenerResponse response)
    {
        Console.WriteLine(" do_GET() activated.");
        
        print_http_request_detail(request);
        send_http_response_header(response);
       
        string path = request.Url.Query;
        if (!string.IsNullOrEmpty(path)){
            path = path.Substring(1);
            int[] pamameters = parameter_retrieval(path);
            int result = simple_calc(pamameters[0], pamameters[1]);

            string html = "<html>";
            string get_response = string.Format("GET request for calculation => {0} x {1} = {2}", parameters[0], parameters[1], result);
            html += get_response;
            html += "</html>";

            byte[] buffer = Encoding.UTF8.GetBytes(html);
            response.ContentLength64 = buffer.Length;
            response.OutputStream.Write(buffer, 0, buffer.Length);
            response.OutputStream.Close();
            Console.WriteLine(" GET request for calculation => {0} x {1} = {2}.", parameter[0], parameter[1], result);
        }
        else
        {
            string html = "<html>";
            string get_response = string.Format("<p>HTTP Request GET for Path: {0}</p>", self.path);
            html += get_response;
            html += "</html>";

            byte[] buffer = Encoding.UTF8.GetBytes(html);
            response.ContentLength64 = buffer.Length;
            response.OutputStream.Write(buffer, 0, buffer.Length);
            response.OutputStream.Close();
            Console.WriteLine("GET request for directory => {0}.", path);
        }
    }

    void do_POST(HttpListenerRequest request, HttpListenerResponse response)
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

    void parameter_retrieval(msg)
    {
        int[] result = new int[2];
        string[] fields = msg.split('&');
        result[0]= int.Parse(fields[0].split('=')[1]);
        result[1]= int.Parse(fields[1].split('=')[1]);
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
                Handler.do_GET(request, response);
            }
            else if (request.HttpMethod == "POST")
            {
                Handler.do_POST(request, response);
            }                           
            Handler.log_message();
            response.Close();        
        }
    }

}
