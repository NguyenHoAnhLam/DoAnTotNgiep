using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyClass.Models;

namespace MyClass.DAO
{
    public class OrderDAO
    {
        private MyDBContext db = new MyDBContext();
        public List<Order> getList(string status = "All")
        {
            //Trả về danh sách các mẫu tin

            List<Order> list = null;
            switch (status)
            {
                case "Index":
                    {
                        list = db.Orders.Where(m => m.Status != 0).ToList();
                        break;
                    }
                case "Trash":
                    {
                        list = db.Orders.Where(m => m.Status == 0).ToList();
                        break;
                    }
                default:
                    {
                        list = db.Orders.ToList();
                        break;
                    }
            }
            return list;
        }
        public List<OrderInfo> getListJoin(string status = "All")
        {
            //Trả về danh sách các mẫu tin

            List<OrderInfo> list = null;
            switch (status)
            {
                case "Index":
                    {
                        list = db.Orders
                            .Join(db.OrderDetails,
                            o=>o.Id,
                            od=>od.OrderId,
                            (o,od)=>new OrderInfo
                            {
                                Id = o.Id,
                                UserId = o.UserId,
                                Address = o.Address,
                                Phone = o.Phone,
                                Email = o.Email,
                                Name = o.Name,
                                Note = o.Note,
                                Create_At = o.Create_At,
                                Create_By = o.Create_By,
                                Update_At = o.Update_At,
                                Update_By = o.Update_By,
                                Status = o.Status,
                                OrderId = od.OrderId,
                                ProductId = od.ProductId,
                                Price = od.Price,
                                Quantity = od.Quantity,
                                Amounts = od.Amounts
                            }
                            )
                            .Where(m => m.Status != 0).ToList();
                        break;
                    }
                case "Trash":
                    {
                        list = db.Orders
                            .Join(db.OrderDetails,
                            o => o.Id,
                            od => od.OrderId,
                            (o, od) => new OrderInfo
                            {
                                Id = o.Id,
                                UserId = o.UserId,
                                Address = o.Address,
                                Phone = o.Phone,
                                Email = o.Email,
                                Name = o.Name,
                                Note = o.Note,
                                Create_At = o.Create_At,
                                Create_By = o.Create_By,
                                Update_At = o.Update_At,
                                Update_By = o.Update_By,
                                Status = o.Status,
                                OrderId = od.OrderId,
                                ProductId = od.ProductId,
                                Price = od.Price,
                                Quantity = od.Quantity,
                                Amounts = od.Amounts
                            }
                            ).Where(m => m.Status == 0).ToList();
                        break;
                    }
                default:
                    {
                        list = db.Orders
                            .Join(db.OrderDetails,
                            o => o.Id,
                            od => od.OrderId,
                            (o, od) => new OrderInfo
                            {
                                Id = o.Id,
                                UserId = o.UserId,
                                Address = o.Address,
                                Phone = o.Phone,
                                Email = o.Email,
                                Name = o.Name,
                                Note = o.Note,
                                Create_At = o.Create_At,
                                Create_By = o.Create_By,
                                Update_At = o.Update_At,
                                Update_By = o.Update_By,
                                Status = o.Status,
                                OrderId = od.OrderId,
                                ProductId = od.ProductId,
                                Price = od.Price,
                                Quantity = od.Quantity,
                                Amounts = od.Amounts
                            }
                            ).ToList();
                        break;
                    }
            }
            return list;
        }

        // Trả về 1 mẫu tin
        public Order getRow(int? id)
        {
            if (id == null)
            {
                return null;
            }
            else
            {
                return db.Orders.Find(id);
            }
        }
        //Thêm mẫu tin
        public int Insert(Order row)
        {
            db.Orders.Add(row);
            return db.SaveChanges();
        }
        //Cập nhật mẫu tin
        public int Update(Order row)
        {
            db.Entry(row).State = EntityState.Modified;
            return db.SaveChanges();
        }
        //Xóa mẫu tin
        public int Delete(Order row)
        {
            db.Orders.Remove(row);
            return db.SaveChanges();
        }
    }
}
