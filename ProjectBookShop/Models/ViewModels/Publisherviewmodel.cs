using System.ComponentModel.DataAnnotations;

namespace ProjectBookShop.Models.ViewModels
{
    public class Publisherviewmodel
    {
        public int PublisherID { get; set; }
        public int UserId { get; set; }

        [Display(Name = "ناشر")]
        [Required(ErrorMessage = "وارد نمودن{0} الزالمی است")]
        public string PublisherName { get; set; }
        public int[]? datachart { get; set; }
        public datasets[]? datasets { get; set; }
        public string[]? labels { get; set; }
        public string? backgroundColor { get; set; }
        public int borderWidth { get; set; }
        public List<ChartData>? chartDatas1 { get; set; }
        public List<ChartDataVM>? chartDatas { get; set; }
      
    }
    public class ChartDataVM
    {
         public string PublisherName { get; set; }
        public virtual int UserID { get; set; }
        public virtual int SentCount { get; set; }
        public virtual int Number { get; set; }
        public virtual DateTime DateTime { get; set; }
        public virtual int PublisherID { get; set; }
    }
}
