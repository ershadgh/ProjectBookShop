using ProjectBookShop.Models.ViewModels;

namespace ProjectBookShop.Models.Repository
{
    public interface IBookRepository
    {
        List<BooksIndexViewModel> GetAllBook(string title, string ISBN, string Language, string PublusherName, string Author, string Translator, string Category);
        List<TreeViewCategory> GetAllCategories(int[]? catagoryArayy);
        void BindSubCategories(TreeViewCategory category);
        Task<bool> CreateBookAsync(BooksCreateEditViewModel booksCreate);
        Task<bool> EditBookAsync(BooksCreateEditViewModel viewModel);
        Task<List< ProductsViewModel>> BuyBook(int[] ids);
    }
}
