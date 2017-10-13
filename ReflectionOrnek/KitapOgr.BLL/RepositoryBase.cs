using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using KitapOgr.DAL;
using System.Reflection;

namespace KitapOgr.BLL
{
   public class RepositoryBase<T> where T:class
    {
        DbContext db = new KitapOgrenciDBEntities();
        public void insert(T item)
        {
            db.Set(typeof(T)).Add(item);
            db.SaveChanges();
        }
        public void Update(T item) {
            PropertyInfo pInfo = null;
            foreach (var property in item.GetType().GetProperties())
            {
                if (property.Name.Contains("ID"))
                {
                    pInfo = property;
                    break;
                }
            }

            var guncellenecek = db.Set(typeof(T)).Find(pInfo.GetValue(item));
            db.Entry(guncellenecek).CurrentValues.SetValues(item);
            db.SaveChanges();

        }
        public void Delete(object id) {
            db.Set(typeof(T)).Remove(db.Set(typeof(T)).Find(id));
            db.SaveChanges();
        }
        public List<T> SelectAll() {
            return db.Set(typeof(T)).Cast<T>().ToList();
        }
     
        public T SelectById(object itemID) {
            return (T)db.Set(typeof(T)).Find(itemID);
        }
    }
}
