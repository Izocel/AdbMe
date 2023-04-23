using System.Reflection;
using LiteDB;

namespace Database.Dao
{
    class BaseDao<TEntity> : IDao<TEntity>
    {
        protected string? TableName { get; set; }
        protected string ConnectionString { get; set; } = "Database/Data/AdbMe.db";
        protected Type TType { get; set; } = typeof(TEntity);

        public BaseDao(string tablename)
        {
            this.TableName = tablename;
        }

        public virtual List<TEntity> Read(BsonExpression whereCond, BsonExpression? orderBy = null)
        {
            using (LiteDatabase db = GetDatabase())
            {
                var res = GetCollection(db).Query()
                .Where(whereCond ?? "1=1")
                .OrderBy(orderBy ?? "Id")
                .Limit(1)
                .ToList();

                return res;
            }
        }

        public virtual List<TEntity> ReadAll(BsonExpression? whereCond = null, BsonExpression? orderBy = null, int? limit = null)
        {
            using (LiteDatabase db = GetDatabase())
            {
                if (limit == null)
                {
                    return GetCollection(db).Query()
                    .Where(whereCond ?? "1=1")
                    .OrderBy(orderBy ?? "id")
                    .ToList();
                }

                return GetCollection(db).Query()
                .Where(whereCond ?? "1=1")
                .OrderBy(orderBy ?? "id")
                .Limit((int)limit)
                .ToList();
            }
        }

        public virtual TEntity Write(TEntity model)
        {
            using (LiteDatabase db = this.GetDatabase())
            {
                this.GetCollection(db).Upsert(model);
                return model;
            }
        }

        public virtual LiteDatabase GetDatabase()
        {
            return new LiteDatabase(@"" + this.ConnectionString);
        }

        public virtual ILiteCollection<TEntity> GetCollection(LiteDatabase db)
        {
            return db.GetCollection<TEntity>(this.TableName);
        }

        public virtual PropertyInfo? GetPropertyInfos(string propName)
        {
            return TType.GetProperty(propName);
        }

        public virtual object? GetCheckPropValue(string propName, object entity)
        {
            PropertyInfo? infos = this.GetPropertyInfos(propName);
            return infos?.GetValue(entity);
        }
    }
}
