namespace ProjectBookShop.Models.ViewModels
{
    public class TreeViewCategory
    {
      
        public TreeViewCategory()
        {
            subs = new List<TreeViewCategory>();

        }

        public TreeViewCategory(int[] catagoryArray)
        {
            CategoryID = catagoryArray;
        }

        public int[] CategoryID { get; set; }
        public int id { get; set; }
        public string? title { get; set; }
        public List<TreeViewCategory>? subs { get; set; }
    }
}
