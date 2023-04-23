using System.Reflection;
using LiteDB;

namespace Database.Dao
{
    public interface IDao<TEntity>
    {
        List<TEntity> Read(BsonExpression whereCond, BsonExpression? orderBy);
        List<TEntity> ReadAll(BsonExpression? whereCond, BsonExpression? orderBy, int? limit);
        TEntity Write(TEntity model);
        LiteDatabase GetDatabase();
        ILiteCollection<TEntity> GetCollection(LiteDatabase db);
        PropertyInfo? GetPropertyInfos(string propName);
        object? GetCheckPropValue(string propName, object entity);
    }
}