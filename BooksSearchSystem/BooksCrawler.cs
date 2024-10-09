using System;
using System.Net.Http;
using System.Threading.Tasks;
using HtmlAgilityPack;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using BooksSearchSystem.Models;

namespace WebApplicationTest
{
    public class BooksCrawler
    {
        public async Task<Book> GetBookInfo(string num)
        {
            string url = $"https://www.books.com.tw/products/{num}?sloc=main";

            var httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.UserAgent.ParseAdd("Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/122.0.0.0 Safari/537.36");

            string response = await httpClient.GetStringAsync(url);
            var htmlDocument = new HtmlDocument();
            htmlDocument.LoadHtml(response);

            // 用來儲存解析出來的書籍資訊
            var book = new Book();

            // 獲取標題
            var titleTag = htmlDocument.DocumentNode.SelectSingleNode("//div[@class='mod type02_p002 clearfix']");
            if (titleTag != null)
            {
                book.Title = titleTag.InnerText.Trim();
            }

            // 獲取作者
            var authorTag = htmlDocument.DocumentNode.SelectSingleNode("//div[@class='type02_p003 clearfix']");
            if (authorTag != null)
            {
                string author = Regex.Replace(authorTag.InnerText, @"\s+", " ").Trim();
                book.Author = author.Replace(" 新功能介紹", "").Replace("已追蹤作者： [ 修改 ] 確定 取消", "");
            }

            // 獲取ISBN
            var infoTags = htmlDocument.DocumentNode.SelectNodes("//div[@class='bd']");
            if (infoTags != null)
            {
                foreach (var infoTag in infoTags)
                {
                    var infoItems = infoTag.SelectNodes(".//li");
                    if (infoItems != null)
                    {
                        foreach (var item in infoItems)
                        {
                            string infoText = Regex.Replace(item.InnerText, @"\s+", "");
                            var keyValue = infoText.Split('：');
                            if (keyValue.Length == 2 && keyValue[0].Contains("ISBN"))
                            {
                                book.Isbn = keyValue[1]; // 保存ISBN
                            }
                        }
                    }
                }
            }

            // 獲取出版商
            var publisherTag = htmlDocument.DocumentNode.SelectSingleNode("//div[@class='type02_p003 clearfix']");
            if (publisherTag != null)
            {
                book.Publisher = publisherTag.InnerText.Trim();
            }

            // 返回書籍實例
            return book;
        }
    }
}
