namespace HTTPProtocolDemo.Scrapers
{
    public class Beer
    {
        public Beer(string name, decimal volume, decimal avb, decimal price, string style, string imagePath)
        {
            Name = name;
            Volume = volume;
            Avb = avb;
            Price = price;
            Style = style;
            ImagePath = imagePath;
        }
        public string Name { get; set; }
        public decimal Volume { get; set; }
        public decimal Avb { get; set; }
        public decimal Price { get; set; }
        public string Style { get; set; }
        public string ImagePath { get; set; }

        public override string ToString() => $"{Name} - {Volume}l. {Avb}% => {Price} leva";
    }
}
