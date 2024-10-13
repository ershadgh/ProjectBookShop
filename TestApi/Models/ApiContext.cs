using Microsoft.EntityFrameworkCore;

namespace TestApi.Models
{
    public class ApiContext:DbContext
    {
        public ApiContext(DbContextOptions<ApiContext> options):base()
        {
                
        }
    }
}
