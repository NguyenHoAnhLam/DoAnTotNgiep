using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyClass.Models;

namespace MyClass.DAO
{
    public class ContactDAO
    {
        private MyDBContext db = new MyDBContext();
        public List<Contact> getList(string status = "All")
        {
            //Trả về danh sách các mẫu tin

            List<Contact> list = null;
            switch (status)
            {
                case "Index":
                    {
                        list = db.Contacts.Where(m => m.Status != 0).ToList();
                        break;
                    }
                case "Trash":
                    {
                        list = db.Contacts.Where(m => m.Status == 0).ToList();
                        break;
                    }
                default:
                    {
                        list = db.Contacts.ToList();
                        break;
                    }
            }
            return list;
        }
        // Trả về 1 mẫu tin
        public Contact getRow(int? id)
        {
            if (id == null)
            {
                return null;
            }
            else
            {
                return db.Contacts.Find(id);
            }
        }
        //Thêm mẫu tin
        public int Insert(Contact row)
        {
            db.Contacts.Add(row);
            return db.SaveChanges();
        }
        //Cập nhật mẫu tin
        public int Update(Contact row)
        {
            db.Entry(row).State = EntityState.Modified;
            return db.SaveChanges();
        }
        //Xóa mẫu tin
        public int Delete(Contact row)
        {
            db.Contacts.Remove(row);
            return db.SaveChanges();
        }
    }
}
