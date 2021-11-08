using CMPG_323_Project2.Data;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace CMPG_323_Project2.Repository
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private CMPG_DBContext _DBContext;
        private DbSet<T> table;
        public GenericRepository(CMPG_DBContext DBContext)
        {
            _DBContext=DBContext;
            table=_DBContext.Set<T>();
        }
        public void Delete(object Id)
        {
            T exists=table.Find(Id);
            table.Remove(exists);
            Save();
        }

        public List<T> GetAll()
        {
            return table.ToList();
        }

        public T GetById(object Id)
        {
            
            return table.Find(Id);
        }

        public void Insert(T obj)
        {
            table.Add(obj);
            Save();
        }

        public void Save()
        {
            _DBContext.SaveChanges();
        }
     

        public void Update(T obj)
        {
            table.Attach(obj);
            _DBContext.Entry(obj).State=EntityState.Modified;
            Save();
        }
        public List<T> Find(string query)
        {
            return table.FromSqlRaw<T>(query).ToList();
        }
    }
}
