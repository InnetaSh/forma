using System.Reflection.PortableExecutable;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();


app.UseStaticFiles();


app.Use(async (context,next) =>
{
    if (context.Request.Path == "/")
    {
        context.Response.ContentType = "text/html; charset=utf-8";
        await context.Response.SendFileAsync("html/index.html");
    }
    await next();
});


app.Run(async (context) =>
{
    context.Response.ContentType = "text/html; charset=utf-8";

    
    if (context.Request.Path == "/main.html")
    {
        var form = context.Request.Form;
        var info = "";
        
            string UserName = form["userName"];
            string userPassword = form["userPassword"];
            string userPhone = form["userPhone"];
            string userGmail = form["userGmail"];

        info = "<table>" +
              "<tr><td><strong>Name:</strong></td><td>" + UserName + "</td></tr>" +
              "<tr><td><strong>Password:</strong></td><td>" + userPassword + "</td></tr>";

        if (userPhone.Length != 0 && userGmail.Length != 0)
        {
           

            info += "<tr><td><strong>Phone:</strong></td><td>" + userPhone + "</td></tr>" +
                    "<tr><td><strong>Gmail:</strong></td><td>" + userGmail + "</td></tr>";
        }

        info += "</table>";



        var page = $@"
        <!DOCTYPE html>
        <html lang='ru'>
        <head>
            <meta charset='utf-8' />
            <link href='/Styles/styles.css' rel='stylesheet' type='text/css'>
            <title>Form info</title>
        </head>
        <body>
            <div class='main'>
                <div class='container'>
                   
                    <div id='formData'>
                      <p><h2>Form Result</h2></p>
                      {info}
                    </div>
                </div>
            </div>
        </body>
        </html>";

      
        context.Response.ContentType = "text/html; charset=utf-8";
        await context.Response.WriteAsync(page);
    }
    else
    {
        await context.Response.SendFileAsync("html/index.html");
    }
});




app.Run();

