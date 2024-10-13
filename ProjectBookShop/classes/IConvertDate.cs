namespace ProjectBookShop.classes
{
    public interface IConvertDate
    {
         DateTime ConvertShamsiToMiladi(string Date);
        string ConverMiladitoShamsi(DateTime Date, string Format);
    }
}
