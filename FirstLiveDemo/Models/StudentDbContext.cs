using System;
using System.Configuration;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;

namespace FirstLiveDemo.Models
{
    public class StudentDbContext : DbContext
    {
        //static string sqlConnection = ConfigurationManager.AppSettings["AzureWebJobsStorage"];
        //.ConnectionStrings["dbEntities"];

        static string cs = Environment.GetEnvironmentVariable("dbEntities");

        public StudentDbContext()
           : base(cs)//"name=dbEntities"
        {
            
        }
        public DbSet<Student> Students { get; set; }


        

    }
}