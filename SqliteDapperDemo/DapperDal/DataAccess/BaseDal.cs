using System;
using System.Collections.Generic;
using System.Data;
using Dapper;
using System.Data.SQLite;
using System.Linq.Expressions;
using SQLinq;
using SQLinq.Dapper;
namespace DapperDal
{
    public abstract class BaseDal<T> where T : class, new()
    {
        //set querys for different tables
        public BaseDal()
        {
            SetQuerys();
        }
        protected Querys querys = new Querys();
        public abstract void SetQuerys();

        //get a conncection
        private static readonly string sqlconnection = @"Data Source=" + Environment.CurrentDirectory.ToString() + @"\DapperDal\Data\AuthData.db;Version=3;";
        public IDbConnection GetOpenConnection()
        {
            IDbConnection connection = new SQLiteConnection(sqlconnection);
            connection.Open();
            return connection;
        }
        //getEntities
        public IEnumerable<T> GetEntities(Expression<Func<T, bool>> predicate)
        {
            using (IDbConnection conn = GetOpenConnection())
            {
                try
                {
                    SQLinq<T> query = new SQLinq<T>().Where(predicate);
                    return conn.Query<T>(query);
                }
                catch
                {
                    return null;
                }
            }
        }

        //getPagedEntities
        public IEnumerable<T> GetPagedEntities(int pageSize, int pageIndex, out int total, Expression<Func<T, bool>> predicate, Expression<Func<T, object>> keySelector, bool isAsc)
        {
            int skipCount = pageSize * (pageIndex - 1);
            total = 0;
            //create  sqlQuery
            SQLinq<T> query = new SQLinq<T>().Where(predicate);
            if (isAsc)
            {
                query = query.OrderBy(keySelector);
            }
            else
            {
                query = query.OrderByDescending(keySelector);
            }
            query.Skip(skipCount).Take(pageSize);
            //do  query
            return Query(query);
        }

        //insert
        public int Insert(T entity)
        {
            int a = 4;
            using (IDbConnection conn = GetOpenConnection())
            {
                string query = querys.Add;//"insert into Users(sName,sGender,sAge) values(@sNmae,@sGender,@sAge)";
                int row = conn.Execute(query, entity);//new {sName="GoldenKey",sGender=true,sAge=22 });
                return row;
            }
            return 2;
        }

        //update
        public int Update(T entity)
        {
            using (IDbConnection conn = GetOpenConnection())
            {
                string query = querys.Update;//"update Users set sName=@sName,sGender=@sGender,sAge=@sAge where sId=@sId";
                int row = conn.Execute(query, entity);
                return row;
            }
        }

        //delete
        public int Delete(T entity)
        {
            using (IDbConnection conn = GetOpenConnection())
            {
                string query = querys.Delete;//"delete from Users where sId=@sId";
                int row = conn.Execute(query, entity);
                return row;
            }
        }

        private IEnumerable<T> Query(SQLinq<T> query)
        {
            try
            {
                using (IDbConnection dbConnection = GetOpenConnection())
                {
                    return dbConnection.Query<T>(query);
                }
            }
            catch
            {
                return null;
            }
        }


    }
}
