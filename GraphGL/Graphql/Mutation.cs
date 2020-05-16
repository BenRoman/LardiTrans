using GraphGL.Database;
using GraphQL;

namespace GraphGL.Graphql
{
    [GraphQLMetadata("Mutation")]
    public class Mutation
    {
        [GraphQLMetadata("addAuthor")]
        public Author Add(string name)
        {
            using (var db = new StoreContext())
            {
                var author = new Author() { Name = name };
                db.Authors.Add(author);
                db.SaveChanges();
                return author;
            }
        }


        [GraphQLMetadata("updateLikes")]
        public string UpdateItem(long id, int amountOfLikes)
        {
            using (var db = new TransContext())
            {
                return db.editLikes(id, amountOfLikes);
            }
        }
    }
}