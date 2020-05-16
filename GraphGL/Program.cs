using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using GraphGL.Database;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace GraphGL
{
    public class Program
    {
        public static void Main(string[] args)
        {
            IWebHost host = CreateWebHostBuilder(args).Build();


            using (var db = new StoreContext())
            {
                var authorDbEntry = db.Authors.Add(
                    new Author
                    {
                        Name = "Stephen King",
                    }
                );

                db.SaveChanges();

                db.Books.AddRange(
                new Book
                {
                    Name = "IT",
                    Published = true,
                    AuthorId = authorDbEntry.Entity.Id,
                    Genre = "Mystery"
                },
                new Book
                {
                    Name = "The Langoleers",
                    Published = true,
                    AuthorId = authorDbEntry.Entity.Id,
                    Genre = "Mystery"
                }
                );
                
                db.SaveChanges();
            }

            host.Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>();
    }
}
