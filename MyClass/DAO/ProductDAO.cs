using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using System.Text;
using System.Threading.Tasks;
using MyClass.Models;
using PagedList;

namespace MyClass.DAO
{
    public class ProductDAO
    {
        private MyDBContext db = new MyDBContext();
        public List<ProductInfo> getListByListCatId(List<int> listcatid, int take)
        {
            List<ProductInfo> list = db.Products
                            .Join(
                            db.Categorys,
                            p => p.Catid,
                            c => c.Id,
                            (p, c) => new ProductInfo
                            {
                                Id = p.Id,
                                Catid = p.Catid,
                                Name = p.Name,
                                Img = p.Img,
                                Slug = p.Slug,
                                Price = p.Price,
                                Pricesale = p.Pricesale,
                                Create_At = p.Create_At,
                                Status = p.Status,
                                CategoryName = c.Name

                            }
                            )
                .Where(m => m.Status == 1 && listcatid.Contains(m.Catid))
                .OrderByDescending(m => m.Create_At)
                .Take(take)
                .ToList();
            return list;
        }
        public List<ProductInfo> getListByListCatId(List<int> listcatid, int take, int notid, bool check = true)
        {
            List<ProductInfo> list = db.Products
                            .Join(
                            db.Categorys,
                            p => p.Catid,
                            c => c.Id,
                            (p, c) => new ProductInfo
                            {
                                Id = p.Id,
                                Catid = p.Catid,
                                Name = p.Name,
                                Img = p.Img,
                                Slug = p.Slug,
                                Price = p.Price,
                                Pricesale = p.Pricesale,
                                Create_At = p.Create_At,
                                Status = p.Status,
                                CategoryName = c.Name

                            }
                            )
                .Where(m => m.Status == 1 && m.Id != notid && listcatid.Contains(m.Catid))
                .OrderByDescending(m => m.Create_At)
                .Take(take)
                .ToList();
            return list;
        }

        public IPagedList<ProductInfo> getListByListCatId(List<int> listcatid, int pageSize, int pageNumber)
        {
            IPagedList<ProductInfo> list = db.Products
                            .Join(
                            db.Categorys,
                            p => p.Catid,
                            c => c.Id,
                            (p, c) => new ProductInfo
                            {
                                Id = p.Id,
                                Catid = p.Catid,
                                Name = p.Name,
                                Img = p.Img,
                                Slug = p.Slug,
                                Price = p.Price,
                                Pricesale = p.Pricesale,
                                Create_At = p.Create_At,
                                Status = p.Status,
                                CategoryName = c.Name

                            }
                            )
                .Where(m => m.Status == 1 && listcatid.Contains(m.Catid))
                .OrderByDescending(m => m.Create_At)
                  .ToPagedList(pageNumber, pageSize);
            return list;
        }

        public IPagedList<ProductInfo> getList(int pageSize, int pageNumber)
        {
            IPagedList<ProductInfo> list = db.Products
                            .Join(
                            db.Categorys,
                            p => p.Catid,
                            c => c.Id,
                            (p, c) => new ProductInfo
                            {
                                Id = p.Id,
                                Catid = p.Catid,
                                Name = p.Name,
                                Img = p.Img,
                                Slug = p.Slug,
                                Price = p.Price,
                                Pricesale = p.Pricesale,
                                Create_At = p.Create_At,
                                Status = p.Status,
                                CategoryName = c.Name

                            }
                            )
                .Where(m => m.Status == 1)
                .OrderByDescending(m => m.Create_At)
                .ToPagedList(pageNumber, pageSize);
            return list;
        }
        public List<ProductInfo> getList(int take)
        {
            List<ProductInfo> list = db.Products
                            .Join(
                            db.Categorys,
                            p => p.Catid,
                            c => c.Id,
                            (p, c) => new ProductInfo
                            {
                                Id = p.Id,
                                Catid = p.Catid,
                                Name = p.Name,
                                Img = p.Img,
                                Slug = p.Slug,
                                Price = p.Price,
                                Pricesale = p.Pricesale,
                                Create_At = p.Create_At,
                                Status = p.Status,
                                CategoryName = c.Name

                            }
                            )
                .Where(m => m.Status == 1)
                .OrderByDescending(m => m.Create_At)
                .Take(take)
                .ToList();
            return list;
        }
        public List<ProductInfo> getList(string status = "All")
        {
            //Trả về danh sách các mẫu tin

            List<ProductInfo> list = null;
            switch (status)
            {
                case "Index":
                    {
                        list = db.Products
                            .Join(
                            db.Categorys,
                            p => p.Catid,
                            c => c.Id,
                            (p, c) => new ProductInfo
                            {
                                Id = p.Id,
                                Catid = p.Catid,
                                Name = p.Name,
                                Img = p.Img,
                                Slug = p.Slug,
                                Price = p.Price,
                                Pricesale = p.Pricesale,
                                Create_At = p.Create_At,
                                Status = p.Status,
                                CategoryName = c.Name

                            }
                            )
                            .Where(m => m.Status != 0)
                            .ToList();
                        break;
                    }
                case "Trash":
                    {
                        list = db.Products
                            .Join(
                            db.Categorys,
                            p => p.Catid,
                            c => c.Id,
                            (p, c) => new ProductInfo
                            {
                                Id = p.Id,
                                Catid = p.Catid,
                                Name = p.Name,
                                Img = p.Img,
                                Slug = p.Slug,
                                Price = p.Price,
                                Pricesale = p.Pricesale,
                                Create_At = p.Create_At,
                                Status = p.Status,
                                CategoryName = c.Name

                            }
                            )
                          .Where(m => m.Status == 0)
                          .ToList();
                        break;
                    }
                default:
                    {
                        list = db.Products
                            .Join(
                            db.Categorys,
                            p => p.Catid,
                            c => c.Id,
                            (p, c) => new ProductInfo
                            {
                                Id = p.Id,
                                Catid = p.Catid,
                                Name = p.Name,
                                Img = p.Img,
                                Slug = p.Slug,
                                Price = p.Price,
                                Pricesale = p.Pricesale,
                                Create_At = p.Create_At,
                                Status = p.Status,
                                CategoryName = c.Name

                            }
                            )
                          .ToList();
                        break;
                    }
            }
            return list;
        }
        // Trả về 1 mẫu tin
        public Product getRow(int? id)
        {
            if (id == null)
            {
                return null;
            }
            else
            {
                return db.Products.Find(id);
            }
        }
        public Product getRow(string slug)
        {
            return db.Products.Where(m => m.Slug == slug && m.Status == 1).FirstOrDefault();
        }
        //Thêm mẫu tin
        public int Insert(Product row)
        {
            db.Products.Add(row);
            return db.SaveChanges();
        }
        //Cập nhật mẫu tin
        public int Update(Product row)
        {
            db.Entry(row).State = EntityState.Modified;
            return db.SaveChanges();
        }
        //Xóa mẫu tin
        public int Delete(Product row)
        {
            db.Products.Remove(row);
            return db.SaveChanges();
        }
        public List<ProductInfo> getByTimKiem(string sTuKhoa )
        {
            List<ProductInfo> list = db.Products
                            .Join(
                            db.Categorys,
                            p => p.Catid,
                            c => c.Id,
                            (p, c) => new ProductInfo
                            {
                                Id = p.Id,
                                Catid = p.Catid,
                                Name = p.Name,
                                Img = p.Img,
                                Slug = p.Slug,
                                Price = p.Price,
                                Pricesale = p.Pricesale,
                                Create_At = p.Create_At,
                                Status = p.Status,
                                CategoryName = c.Name

                            }
                            )
                .Where(m => m.Status == 1 && m.Name.Contains(sTuKhoa)).ToList();
            return list;
        }
    }
}
