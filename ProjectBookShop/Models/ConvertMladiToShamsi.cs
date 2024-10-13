using System.Globalization;

namespace ProjectBookShop.Models
{
    public class ConvertMladiToShamsi
    {
      public   string PersianDate(DateTime DateTime1,string? Ispublishe)
        {
            if (DateTime1 == DateTime.MinValue)
                DateTime1 = DateTime.Now;
            if (DateTime1 != DateTime.MinValue && Ispublishe == "منتشر شده")
            {
                PersianCalendar PersianCalendar1 = new PersianCalendar();
                return string.Format(@"{0}/{1}/{2}",
                             PersianCalendar1.GetYear(DateTime1),
                             PersianCalendar1.GetMonth(DateTime1),
                             PersianCalendar1.GetDayOfMonth(DateTime1));
            }else
            {
                PersianCalendar PersianCalendar1 = new PersianCalendar();
                return string.Format(@"{0}/{1}/{2}", "بروی سایت منتشر نشده",null,null) ;
            }
        }
    }
    
}
