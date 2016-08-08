using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


using MongoDB.Driver;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using System.Linq.Expressions;

using MD.Configuration;
using MDRelation.Dal.Entity;

namespace MDRelation.Dal
{
    public class RelationProvider: MongoBaseProvider<Relation>
    {



        private readonly string collection = "relation";
        
        public RelationProvider(string connectionUri)
            :base(connectionUri)
        {
            databaseName = "mdrelation";
            collectionName = collection;
        }

        //private static readonly Lazy<RelationProvider> lazy = new Lazy<RelationProvider>(() => new RelationProvider());

     
        public Entity.Relation AddRelation(Entity.Relation relation)
        {
            FindOneAndUpdateOptions<Entity.Relation> options = new FindOneAndUpdateOptions<Entity.Relation> { IsUpsert = true };

            Expression<Func<Entity.Relation, bool>> exp = o => o.AccountId == relation.AccountId && o.EntityId == relation.EntityId && o.Type == relation.Type;

            var update = Builders<Relation>.Update
                .Set(r => r.AccountId, relation.AccountId)
                .Set(r=>r.EntityId,relation.EntityId)
                .Set(r => r.Type, relation.Type) ;

            var re = this.FindOneAndReplace(exp, relation, true);

            return re ?? relation;
        }


        public bool DeleteRelationByAccountId(string accountId)
        {
            var bsonDocument = new BsonDocument();
            bsonDocument.Add("aid", accountId);

            return this.DeleteOne(bsonDocument).DeletedCount > 0;
        }

        public bool DeleteRelationByEntityId(string entityId)
        {
            var bsonDocument = new BsonDocument();
            bsonDocument.Add("aid", entityId);

            return this.DeleteOne(bsonDocument).DeletedCount > 0;
        }

        public bool DeleteRelationByAccountAndEntityId(string accountId, string entityId)
        {
            var bsonDocument = new BsonDocument();
            bsonDocument.Add("aid", accountId);
            bsonDocument.Add("eid", entityId);
            return this.DeleteOne(bsonDocument).DeletedCount > 0;
        }




    }
}
