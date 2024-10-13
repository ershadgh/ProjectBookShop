
using MD.PersianDateTime.Core;
namespace ProjectBookShop.classes

{
    public class ConvertDate: IConvertDate
    {
        public DateTime ConvertShamsiToMiladi(string Date)
        {
            PersianDateTime persianDateTime = PersianDateTime.Parse(Date);
            return persianDateTime.ToDateTime();
        }
        public string ConverMiladitoShamsi(DateTime Date, string Form)
        {
            PersianDateTime persianDateTime = new PersianDateTime(Date);

            // return persianDateTime.ToString(Format);
            return persianDateTime.ToLongDateString();

        }
    }
}
