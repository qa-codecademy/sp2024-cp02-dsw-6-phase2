using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebShopApp.DataAccess.Interface
{
    public interface IRepository<T>
    {
        List<T> GetAll();
        T GetById(int id);
        Task  Add(T entity);
        Task Update(T entity);
        void Delete(T entity);
    }
}
