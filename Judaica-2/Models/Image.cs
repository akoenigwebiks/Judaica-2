using System.ComponentModel.DataAnnotations;

namespace Judaica_2.Models
{
    public class Image
    {
        public Image() { }
        public Image(Item item, byte[] myImage)
        {
            Item = item;
            MyImage = myImage;
        }
        [Key]
        public int ID { get; set; }
        public Item Item { get; set; }
        public byte[] MyImage { get; set; }
    }
}
