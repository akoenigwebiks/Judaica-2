using System.ComponentModel.DataAnnotations;

namespace Judaica_2.Models
{
    public class Price
    {
        public Price() { }
        public Price(Item item)
        {
            Item = item;
        }

        [Key]
        public int ID { get; set; }

        public Item Item { get; set; }

        [Display(Name = "מחיר"),DataType("decimal(18,2)")]
        public decimal MyPrice { get; set; }

        [Display(Name = "תאריך התחלה"), DataType(DataType.Date)]
        public DateTime Start { get; set; } = DateTime.Now;

        [Display(Name = "תאריך סיום"), DataType(DataType.Date)]
        public DateTime End { get; set; } = DateTime.Now.AddYears(1);
    }
}
