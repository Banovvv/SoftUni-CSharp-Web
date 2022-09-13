using HtmlAgilityPack;
using System.Text;

namespace HTTPProtocolDemo.Scrapers
{
    public static class BeerScraper
    {
        private static readonly string[] _urls = new string[5]
        {
         @"https://pivoteka.bg/bg/bulgarian-craft-beer/",
         @"https://pivoteka.bg/bg/bulgarian-craft-beer/page-2/",
         @"https://pivoteka.bg/bg/bulgarian-craft-beer/page-3/",
         @"https://pivoteka.bg/bg/bulgarian-craft-beer/page-4/",
         @"https://pivoteka.bg/bg/bulgarian-craft-beer/page-5/"
        };

        public static async Task<ICollection<Beer>> Scrape()
        {
            Console.OutputEncoding = Encoding.UTF8;

            HttpClient client = new HttpClient();
            ICollection<Beer> beers = new List<Beer>();

            foreach (string url in _urls)
            {
                var content = await client.GetStringAsync(url);

                var htmlDocument = new HtmlDocument();
                htmlDocument.LoadHtml(content);

                var beersInfo = htmlDocument.DocumentNode
                    .Descendants("div")
                    .Where(x => x.GetAttributeValue("class", "").Contains("ty-grid-list__item ty-quick-view-button__wrapper"))
                    .ToList();


                foreach (var beerInfo in beersInfo)
                {
                    var name = beerInfo.Descendants("a").First(x => x.GetAttributeValue("class", "").Equals("product-title")).InnerText;

                    if (name.Contains("Български малки") || name.Contains("Клопатар") || name.Contains("Пакет"))
                    {
                        continue;
                    }

                    var relevantInfo = beerInfo.Descendants("span").Where(x => x.GetAttributeValue("class", "").Equals("")).Where(x => x.InnerText.Length > 3).ToArray();
                    var volume = decimal.Parse(relevantInfo[0].InnerText.Substring(0, relevantInfo[0].InnerText.Length - 2));
                    var avb = decimal.Parse(relevantInfo[1].InnerText.Substring(0, relevantInfo[1].InnerText.Length - 1));
                    var price = decimal.Parse(beerInfo.Descendants("span").First(x => x.GetAttributeValue("class", "").Equals("ty-price-num")).InnerText);
                    var style = relevantInfo[2].InnerText;
                    var imagePath = beerInfo.Descendants("img").First().Attributes.First(x => x.Name == "src").Value;

                    beers.Add(new Beer(name, volume, avb, price, style, imagePath));
                }
            }

            foreach (var beer in beers)
            {
                Console.WriteLine(beer);
            }

            return beers;
        }
    }
}
