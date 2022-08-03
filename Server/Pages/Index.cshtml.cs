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

            return Page();
        }

        private static HtmlDocument LoadHtml(string html)
        {
            HtmlDocument doc = new HtmlDocument();
            doc.LoadHtml(html);
            string s = "";

            HtmlNode docNode = doc.DocumentNode;
 
           var node=  doc.DocumentNode.SelectSingleNode("//input[@name='Input.Email']");

            node.SetAttributeValue("value", "rajasekhar.d@ensurity.com");

            //HtmlNodeCollection nodes = docNode.SelectNodes("//input"); //SelectNodes takes a XPath expression
            //foreach (HtmlNode node in nodes)
            //{
            //    var attributes = node.GetAttributes();
            //    foreach (var item in attributes)
            //    {
            //        Console.WriteLine("*****************");
            //        Console.WriteLine(item.Name);
            //        Console.WriteLine("*****************");

            //    }
            //}

            return doc;
        }
    }
}
