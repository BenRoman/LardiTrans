using System.Collections.Generic;
using GraphQL;
using System.Linq;
using GraphGL.Database;
using Microsoft.EntityFrameworkCore;
using MongoDB.Driver;
using SeleniumParserML.Model;
using System;

namespace GraphGL.Graphql
{
    public class Query
    {

        [GraphQLMetadata("books")]
        public IEnumerable<Book> GetBooks()
        {
            using (var db = new StoreContext())
            {
                return db.Books
                .Include(b => b.Author)
                .ToList();
            }
        }

        [GraphQLMetadata("authors")]
        public IEnumerable<Author> GetAuthors()
        {
            using (var db = new StoreContext())
            {
                return db.Authors
                .Include(a => a.Books)
                .ToList();
            }
        }

        [GraphQLMetadata("author")]
        public Author GetAuthor(int id)
        {
            using (var db = new StoreContext())
            {
                return db.Authors
                .Include(a => a.Books)
                .SingleOrDefault(a => a.Id == id);
            }
        }

        [GraphQLMetadata("transportation")]
        public IEnumerable<TransportationML> GetTransInfo()
        {
            using (var db = new TransContext())
            {
                var items = db.Transportations.AsQueryable().ToList();
                List<TransportationML> transportationMLs = new List<TransportationML>();
                double min = 0;
                double max = 0;
                foreach (var item in items)
                {
                    var input = new ModelInput();
                    input.VehicleType = item.vehicleType;
                    input.RoutToCountry = item.routToCountry;
                    ModelOutput result = ConsumeModel.Predict(input);
                    if(result.Score > max)
                    {
                        max = result.Score;
                    }
                    if(result.Score < min)
                    {
                        min = result.Score;
                    }
                    transportationMLs.Add(new TransportationML
                    {
                        transportationInfo = item,
                        likeProbability = result.Score
                    });
                }
                foreach (var item in transportationMLs)
                {
                    item.percentage = (int)Math.Round(100 * (item.likeProbability + Math.Abs(min)) / Math.Abs(max - min));
                }
                return transportationMLs;

            }
        }


        [GraphQLMetadata("hello")]
        public string GetHello()
        {
            return "World";
        }
    }
}