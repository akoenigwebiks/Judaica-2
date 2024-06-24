using Judaica_2.Services;
using System.ComponentModel.DataAnnotations;

namespace Judaica_2.Models
{
    public class Item
    {
        public Item() { }
        public Item(Category category, string name)
        {
            Category = category;
            Name = name;
            Images = new List<Image> { };
            Prices = new List<Price> { };
        }

        [Key]
        public int ID { get; set; }

        [Display(Name = "שם פריט")]
        public string Name { get; set; }

        [Display(Name = "תמונה")]
        public List<Image> Images { get; set; }

        public Category Category { get; set; }

        public List<Price> Prices { get; set; }

        //פונקציות להוספת תמונות
        public void AddImage(IFormFile file)
        {

            byte[] image = new ImageService(file).Image!;//imageservice can handle null

            if (image != null)
            {
                Image imageToAdd = new Image(this, image);
                Images.Add(imageToAdd);
            }
        }
        //פונקציות להוספת מחירים
        public void AddPrice(decimal price)
        {
            Price priceToAdd = new Price(this);
            priceToAdd.MyPrice = price;
            Prices.Add(priceToAdd);
        }
        public void AddPrice(decimal price, DateTime end)
        {
            Price priceToAdd = new Price(this);
            priceToAdd.MyPrice = price;
            priceToAdd.End = end;
            Prices.Add(priceToAdd);
        }
        public void AddPrice(decimal price, DateTime start, DateTime end)
        {
            Price priceToAdd = new Price(this);
            priceToAdd.MyPrice = price;
            priceToAdd.End = end;
            priceToAdd.Start = start;
            Prices.Add(priceToAdd);
        }

    }
}
