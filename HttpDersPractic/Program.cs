using System.Net;

HttpListener listener = new HttpListener();

listener.Prefixes.Add("http://localhost:27001/");
int count = 0;
listener.Start();
var Files = Directory.GetFiles("Rubail");
while (true)
{

    var context=listener.GetContext();

    _ = Task.Run(() => 
    {
        HttpListenerRequest request=context.Request;
        HttpListenerResponse response=context.Response;

        var url=request.RawUrl;
        Console.WriteLine(url);
        if (url=="/")
        {
            using StreamWriter writer=new StreamWriter(response.OutputStream);
            var index=File.ReadAllText($"Rubail/index.html");
            writer.Write(index);
        }
        else
        {
            var uls = url.Split('/').ToList();
            if (uls[1]=="Rubail")
            {
                foreach (var file in Files)
                {
                    var temp = file.Split("\\").ToList();
                    var temp1 = $"{temp}.html";
                    var temp2= $"{uls[2]}.html";
                    if (temp[1] == uls[2]||temp1==temp2)
                    {
                        using StreamWriter writer = new StreamWriter(response.OutputStream);
                        var index = File.ReadAllText($"Rubail/{temp[1]}");
                        writer.Write(index);
                        count = 1;
                    }
                    
                    

                }
                if (count == 1)
                {
                    using StreamWriter writer = new StreamWriter(response.OutputStream);
                    var index = File.ReadAllText($"Rubail/404.html");
                    writer.Write(index);
                }
            }
            
        }
    });

}
