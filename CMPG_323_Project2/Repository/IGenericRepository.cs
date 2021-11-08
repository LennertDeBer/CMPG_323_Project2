using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace CMPG_323_Project2.Repository
{
   public  interface IGenericRepository<T> where T: class
    {
        List<T> GetAll();
        T GetById(object Id);
        void Insert(T obj);
        void Update(T obj);
        void Delete(object Id);

        void Save();
        List<T> Find(string query);


    }
}
