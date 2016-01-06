using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using System.Configuration;
using System.Data.SQLite;

namespace DapperDal
{
    public abstract class BaseDal<T> where T:new()
    {
        //set querys for different tables
        public BaseDal()
        {
            SetQuerys();
        }
        protected Querys querys = new Querys();
        public abstract void SetQuerys();

        //get a conncection
        private static readonly string sqlconnection = @"Data Source=" + Environment.CurrentDirectory.ToString() + @"\Data\AuthData.db;Version=3;";
        public SQLiteConnection GetOpenConnection()
        {
            SQLiteConnection connection = new SQLiteConnection(sqlconnection);
            connection.Open();
            return connection;
        }



        //getEntities
        public List<T> GetEntities()
        {
            using (IDbConnection conn = GetOpenConnection())
            {
                string query = querys.GetEntities;//"select * from Users order by sid desc";
                IEnumerable<T> users = conn.Query<T>(query, null);
                return users.ToList<T>();
            }
        }

        /*
        //getPagedEntities
        public List<T> GetPagedEntities<Tkey>(int pageSize, int pageIndex, out int total, Func<T, bool> whereLambda, Func<T, Tkey> orderbyLambda, bool isAsc)
        {
            total = GetEntities(whereLambda).Count;
            if (isAsc)
            {
                var temp = GetEntities(whereLambda)
                    .OrderBy<T, Tkey>(orderbyLambda)
                    .Skip(pageSize * (pageIndex - 1))
                    .Take(pageSize);
                return temp.ToList<T>();
            }
            else
            {
                var temp = GetEntities(whereLambda)
                    .OrderByDescending<T, Tkey>(orderbyLambda)
                    .Skip(pageSize * (pageIndex - 1))
                    .Take(pageSize);
                return temp.ToList<T>();
            }
        }*/

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



    }
}
