using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyClass.Models;

namespace MyClass.DAO
{
    public class PostDAO
    {
        private MyDBContext db = new MyDBContext();
        public List<PostInfo> getListByTopId(int? topid,string type = "Post", int? notid=null)
        {
            List<PostInfo> list = null;
            if(notid==null)
            {
                list = db.Posts
                 .Join(
                 db.Topics,
                 p => p.TopId,
                 t => t.Id,
                 (p, t) => new PostInfo
                 {
                     Id = p.Id,
                     TopId = p.TopId,
                     TopicName = t.Name,
                     Title = p.Title,
                     Detail = p.Detail,
                     Slug = p.Slug,
                     Img = p.Img,
                     Type = p.Type,
                     Metakey = p.Metakey,
                     Metadesc = p.Metadesc,
                     Create_At = p.Create_At,
                     Create_By = p.Create_By,
                     Update_At = p.Update_At,
                     Update_By = p.Update_By,
                     Status = p.Status
                 }
                 )
                 .Where(m => m.Status == 1 && m.Type == type && m.TopId == topid)
                 .ToList();
            }
            else
            {
                list = db.Posts
                 .Join(
                 db.Topics,
                 p => p.TopId,
                 t => t.Id,
                 (p, t) => new PostInfo
                 {
                     Id = p.Id,
                     TopId = p.TopId,
                     TopicName = t.Name,
                     Title = p.Title,
                     Detail = p.Detail,
                     Slug = p.Slug,
                     Img = p.Img,
                     Type = p.Type,
                     Metakey = p.Metakey,
                     Metadesc = p.Metadesc,
                     Create_At = p.Create_At,
                     Create_By = p.Create_By,
                     Update_At = p.Update_At,
                     Update_By = p.Update_By,
                     Status = p.Status
                 }
                 )
                 .Where(m => m.Status == 1 && m.Type == type && m.TopId == topid && m.Id!=notid)
                 .ToList();
            }
                
            return list;
        }
        public List<PostInfo> getList(string type="Post")
        {
            List<PostInfo> list = db.Posts
                .Join(
                db.Topics,
                p => p.TopId,
                t => t.Id,
                (p, t) => new PostInfo
                {
                    Id = p.Id,
                    TopId = p.TopId,
                    TopicName = t.Name,
                    Title = p.Title,
                    Detail  =p.Detail,
                    Slug = p.Slug,
                    Img = p.Img,
                    Type = p.Type,
                    Metakey = p.Metakey,
                    Metadesc = p.Metadesc,
                    Create_At = p.Create_At,
                    Create_By = p.Create_By,
                    Update_At = p.Update_At,
                    Update_By = p.Update_By,
                    Status = p.Status
                }
                )
                .Where(m => m.Status ==1 && m.Type == type)
                .ToList();
            return list;
        }
        public List<Post> getList(string status = "All", string type = "Post")
        {
            //Trả về danh sách các mẫu tin

            List<Post> list = null;
            switch (status)
            {
                case "Index":
                    {
                        list = db.Posts.Where(m => m.Status != 0 && m.Type == type).ToList();
                        break;
                    }
                case "Trash":
                    {
                        list = db.Posts.Where(m => m.Status == 0 && m.Type == type).ToList();
                        break;
                    }
                default:
                    {
                        list = db.Posts.Where(m => m.Type == type).ToList();
                        break;
                    }
            }
            return list;
        }
        // Trả về 1 mẫu tin
        public Post getRow(int? id)
        {
            if (id == null)
            {
                return null;
            }
            else
            {
                return db.Posts.Find(id);
            }
        }
        public Post getRow(string slug, string posttype)
        {
            return db.Posts
                .Where(m => m.Slug == slug && m.Type == posttype && m.Status == 1)
                .FirstOrDefault();
        }

        //Thêm mẫu tin
        public int Insert(Post row)
        {
            db.Posts.Add(row);
            return db.SaveChanges();
        }
        //Cập nhật mẫu tin
        public int Update(Post row)
        {
            db.Entry(row).State = EntityState.Modified;
            return db.SaveChanges();
        }
        //Xóa mẫu tin
        public int Delete(Post row)
        {
            db.Posts.Remove(row);
            return db.SaveChanges();
        }
    }
}
