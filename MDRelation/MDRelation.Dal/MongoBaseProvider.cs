using MongoDB.Bson.Serialization;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace MDRelation.Dal
{
    public abstract class MongoBaseProvider<T> where T : class
    {

        public string databaseName = "NoDatabaseName";
        public string collectionName = "NoName";

        public MongoClient client = null;
        public IMongoDatabase database = null;
        public MongoBaseProvider(string conUrl)
        {
      
            var setting = MongoClientSettings.FromUrl(new MongoUrl(conUrl));
            client = new MongoClient(setting);
        }

        public IMongoCollection<T> GetCollection()
        {
            var database = client.GetDatabase(databaseName);
            return database.GetCollection<T>(collectionName);
        }

        #region 新增
        public T FindOneAndReplace(Expression<Func<T, bool>> filter, T replacement, bool isUpsert = false)
        {
            try
            {
                var database = client.GetDatabase(databaseName);
                var myCollection = database.GetCollection<T>(collectionName);
                FindOneAndReplaceOptions<T> options = new FindOneAndReplaceOptions<T> { IsUpsert = isUpsert };
                return myCollection.FindOneAndReplaceAsync(filter, replacement, options).GetAwaiter().GetResult();
            }
            catch (Exception ex)
            {
                throw;
                // return null;
            }
        }

        public bool Insert(T model)
        {
            try
            {
                var database = client.GetDatabase(databaseName);
                var myCollection = database.GetCollection<T>(collectionName);
                var task = myCollection.InsertOneAsync(model);
                task.Wait();
                return task.IsCompleted;
            }
            catch (Exception ex)
            {
                throw;
            }
        }


        #endregion

        #region 修改

        public UpdateResult UpdateOne(FilterDefinition<T> filter, UpdateDefinition<T> update, bool isUpsert = false)
        {
            try
            {
                var database = client.GetDatabase(databaseName);
                var myCollection = database.GetCollection<T>(collectionName);
                UpdateOptions options = new UpdateOptions() { IsUpsert = isUpsert };
                return myCollection.UpdateOneAsync(filter, update, options).GetAwaiter().GetResult();

            }
            catch (Exception ex)
            {
                throw;
                // return null;
            }
        }

        public UpdateResult UpdateOne(Expression<Func<T, bool>> filter, UpdateDefinition<T> update, bool isUpsert = false)
        {
            try
            {
                var database = client.GetDatabase(databaseName);
                var myCollection = database.GetCollection<T>(collectionName);
                UpdateOptions options = new UpdateOptions() { IsUpsert = isUpsert };

                return myCollection.UpdateOneAsync<T>(filter, update, options).GetAwaiter().GetResult();
            }
            catch (Exception ex)
            {
                throw;
                //  return null;
            }
        }

        public UpdateResult UpdateAll(FilterDefinition<T> filter, UpdateDefinition<T> update, bool isUpsert = false)
        {
            try
            {
                var database = client.GetDatabase(databaseName);
                var myCollection = database.GetCollection<T>(collectionName);
                UpdateOptions options = new UpdateOptions() { IsUpsert = isUpsert };
                return myCollection.UpdateManyAsync(filter, update, options).GetAwaiter().GetResult();
            }
            catch (Exception ex)
            {
                throw;
                // return null;
            }

        }

        public UpdateResult UpdateAll(Expression<Func<T, bool>> filter, UpdateDefinition<T> update, bool isUpsert = false)
        {
            try
            {
                var database = client.GetDatabase(databaseName);
                var myCollection = database.GetCollection<T>(collectionName);
                UpdateOptions options = new UpdateOptions() { IsUpsert = isUpsert };
                return myCollection.UpdateManyAsync<T>(filter, update, options).GetAwaiter().GetResult();
            }
            catch (Exception ex)
            {
                throw;
                // return null;
            }

        }

        public T FindOneAndUpdate(FilterDefinition<T> filter, UpdateDefinition<T> update, bool isUpsert = false)
        {
            try
            {
                var database = client.GetDatabase(databaseName);
                var myCollection = database.GetCollection<T>(collectionName);
                FindOneAndUpdateOptions<T> options = new FindOneAndUpdateOptions<T> { IsUpsert = isUpsert };
                return myCollection.FindOneAndUpdateAsync(filter, update, options).GetAwaiter().GetResult();
            }
            catch (Exception ex)
            {
                throw;
                // return null;
            }
        }
        #endregion

        #region 删除
        public DeleteResult DeleteOne(FilterDefinition<T> filter)
        {
            try
            {
                var database = client.GetDatabase(databaseName);
                var myCollection = database.GetCollection<T>(collectionName);
                return myCollection.DeleteOneAsync(filter).GetAwaiter().GetResult();
            }
            catch (Exception ex)
            {
                throw;
                //  return null;
            }
        }

        public DeleteResult DeleteOne(Expression<Func<T, bool>> filter)
        {
            try
            {
                var database = client.GetDatabase(databaseName);
                var myCollection = database.GetCollection<T>(collectionName);
                return myCollection.DeleteOneAsync<T>(filter).GetAwaiter().GetResult();
            }
            catch (Exception ex)
            {
                throw;
                // return null;
            }
        }
        public DeleteResult DeleteMany(FilterDefinition<T> filter)
        {
            try
            {
                var database = client.GetDatabase(databaseName);
                var myCollection = database.GetCollection<T>(collectionName);
                return myCollection.DeleteManyAsync(filter).GetAwaiter().GetResult();
            }
            catch (Exception ex)
            {
                throw;
                //  return null;
            }
        }

        public DeleteResult DeleteMany(Expression<Func<T, bool>> filter)
        {
            try
            {
                var database = client.GetDatabase(databaseName);
                var myCollection = database.GetCollection<T>(collectionName);
                return myCollection.DeleteManyAsync<T>(filter).GetAwaiter().GetResult();
            }
            catch (Exception ex)
            {
                throw;
                // return null;
            }
        }


        #endregion

        #region  查询

        public List<TEntity> Find<TEntity>(FilterDefinition<T> filter, FindOptions<T, TEntity> options = null)
        {
            var result = new List<TEntity>();
            try
            {
                var database = client.GetDatabase(databaseName);
                var myCollection = database.GetCollection<T>(collectionName);
                result = myCollection.FindAsync<TEntity>(filter, options).GetAwaiter().GetResult().ToListAsync().GetAwaiter().GetResult();
            }
            catch (Exception ex)
            {
                throw;
                //  throw;
            }
            return result;
        }

        public List<U> Find<T, U>(FilterDefinition<T> filter, FindOptions<T, U> options = null)
        {
            var result = new List<U>();
            try
            {
                var database = client.GetDatabase(databaseName);
                var myCollection = database.GetCollection<T>(collectionName);
                result = myCollection.FindAsync<U>(filter, options).GetAwaiter().GetResult().ToListAsync().GetAwaiter().GetResult();
            }
            catch (Exception ex)
            {
                throw;
            }
            return result;
        }

        public List<T> Find(FilterDefinition<T> filter, FindOptions<T, T> options = null)
        {
            var result = new List<T>();
            try
            {
                var database = client.GetDatabase(databaseName);
                var myCollection = database.GetCollection<T>(collectionName);
                result = myCollection.FindAsync<T>(filter, options).GetAwaiter().GetResult().ToListAsync().GetAwaiter().GetResult();
            }
            catch (Exception ex)
            {
                throw;
            }
            return result;
        }

        public List<T> Find(Expression<Func<T, bool>> filter, FindOptions<T, T> options = null)
        {
            var result = new List<T>();
            try
            {
                var database = client.GetDatabase(databaseName);
                var myCollection = database.GetCollection<T>(collectionName);
                result = myCollection.FindAsync<T>(filter, options).GetAwaiter().GetResult().ToListAsync().GetAwaiter().GetResult();
            }
            catch (Exception ex)
            {
                throw;
            }
            return result;
        }

        public List<TEntity> Aggregate<TEntity>(PipelineDefinition<T, TEntity> filter, AggregateOptions options = null)
        {
            var result = new List<TEntity>();
            try
            {
                var database = client.GetDatabase(databaseName);
                var myCollection = database.GetCollection<T>(collectionName);
                result = myCollection.AggregateAsync<TEntity>(filter, options).GetAwaiter().GetResult().ToListAsync().GetAwaiter().GetResult();
            }
            catch (Exception ex)
            {
                throw;
            }
            return result;
        }

        public List<T> Aggregate(PipelineDefinition<T, T> pipeline, AggregateOptions options = null)
        {
            var result = new List<T>();
            try
            {
                var database = client.GetDatabase(databaseName);
                var myCollection = database.GetCollection<T>(collectionName);
                result = myCollection.AggregateAsync<T>(pipeline, options).GetAwaiter().GetResult().ToListAsync().GetAwaiter().GetResult();
            }
            catch (Exception ex)
            {
                throw;
            }
            return result;
        }

        public T FindOne(FilterDefinition<T> filter, FindOptions<T, T> options = null)
        {
            try
            {
                options = options ?? new FindOptions<T, T> { Limit = 1 };
                return Find(filter, options).FirstOrDefault();
            }
            catch (Exception ex)
            {
                throw;
                //  return default(T);
            }
        }

        public U FindOne<T, U>(FilterDefinition<T> filter, FindOptions<T, U> options = null)
        {
            try
            {
                options = options ?? new FindOptions<T, U>();
                if (options == null)
                {
                    options = new FindOptions<T, U>() { Limit = 1 };
                }
                else
                {
                    options.Limit = 1;
                }
                var result = Find(filter, options);
                return result.FirstOrDefault();
            }
            catch (Exception ex)
            {
                throw;
                // return default(U);
            }
        }

        public T FindOne(Expression<Func<T, bool>> filter, FindOptions<T, T> options = null)
        {
            try
            {
                options = options ?? new FindOptions<T, T>();
                options.Limit = 1;
                if (options == null)
                {
                    options = new FindOptions<T, T>() { Limit = 1 };
                }
                else
                {
                    options.Limit = 1;
                }
                var result = Find(filter, options);
                return result.FirstOrDefault();
            }
            catch (Exception ex)
            {
                throw;
                // return null;
            }
        }

       


        public long Count(Expression<Func<T, bool>> filter)
        {
            try
            {
                var database = client.GetDatabase(databaseName);
                var myCollection = database.GetCollection<T>(collectionName);
                return myCollection.CountAsync<T>(filter).GetAwaiter().GetResult();
            }
            catch (Exception ex)
            {
                throw;
                // return 0;
            }
        }

   
 

        #endregion


        #region 批量写入
        public BulkWriteResult BulkWrite(IEnumerable<WriteModel<T>> models)
        {
            try
            {
                var database = client.GetDatabase(databaseName);
                var myCollection = database.GetCollection<T>(collectionName);
                return myCollection.BulkWriteAsync(models).GetAwaiter().GetResult();
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        /// <summary>
        /// 新增一条记录
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public bool BulkWriteOne(T entity)
        {
            InsertOneModel<T> model = new InsertOneModel<T>(entity);
            var inserResult = this.BulkWrite(new[] { model });
            if (inserResult != null && inserResult.InsertedCount == 1)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        #endregion



        #region 辅助方法

        public List<string> ListStringToLower(List<string> list)
        {
            if (list == null)
                return null;
            list.RemoveAll(m => string.IsNullOrEmpty(m));
            List<string> reList = new List<string>();
            return (from l in list where l != null select l.ToLower()).ToList();
        }



        #endregion

    }
}
