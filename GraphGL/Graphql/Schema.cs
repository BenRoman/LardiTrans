using GraphQL.Types;
using GraphQL;
using GraphGL.Database;

namespace GraphGL.Graphql
{
    public class MySchema
    {
        private ISchema _schema { get; set; }
        public ISchema GraphQLSchema
        {
            get
            {
                return this._schema;
            }
        }

        public MySchema()
        {
            this._schema = Schema.For(@"
          type Book {
            id: ID
            name: String,
            genre: String,
            published: Date,
            Author: Author
          }

          type Author {
            id: ID,
            name: String,
            books: [Book]
          }

          type TransportationInfo{
            loadingDate: String
            vehicleType: String
            cargoDescription: String
            paymentType: String
            routFrom: String
            routTo: String
            routFromCountry: String
            routToCountry: String
          }

          type Mutation {
            addAuthor(name: String): Author
          }

          type Query {
              books: [Book]
              author(id: ID): Author,
              authors: [Author]
              hello: String
              transportation: [TransportationInfo]
          }
      ", _ =>
            {
                _.Types.Include<Query>();
                _.Types.Include<Mutation>();
            });
        }

    }
}