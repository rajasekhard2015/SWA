using HtmlAgilityPack;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace SecureWebAuthentication.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;

        public IndexModel(ILogger<IndexModel> logger)
        {
            _logger = logger;
        }

        public async Task<IActionResult> OnGetAsync()
        {
            using var client = new HttpClient();
         
            var content = await client.GetStringAsync("http://localhost:57666/Identity/Account/Login");

            var doc= LoadHtml(content);

            return new ContentResult
            {
                ContentType = "text/html",
                Content = doc.DocumentNode.OuterHtml
            };
        }

        private static HtmlDocument LoadHtml(string html)
        {
            HtmlDocument doc = new HtmlDocument();
            doc.LoadHtml(html);
        
            HtmlNode docNode = doc.DocumentNode;
 
            var Emailnode=  doc.DocumentNode.SelectSingleNode("//input[@name='Input.Email']");

            Emailnode.SetAttributeValue("value", "rajasekhar.d@ensurity.com");

            var Passwordnode = doc.DocumentNode.SelectSingleNode("//input[@name='Input.Password']");

            Passwordnode.SetAttributeValue("value", "Unik@123");

            return doc;
        }
    }
}
