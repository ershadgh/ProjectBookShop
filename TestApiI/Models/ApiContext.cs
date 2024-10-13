using Microsoft.EntityFrameworkCore;

namespace TestApiI.Models
{
    public class ApiContext:DbContext
    {
        public ApiContext(DbContextOptions<ApiContext> options):base()
        {
                
        }
    }
}
