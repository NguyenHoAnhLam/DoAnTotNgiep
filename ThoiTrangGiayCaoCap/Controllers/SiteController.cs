using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MyClass.Models;
using MyClass.DAO;
using PagedList;

namespace ThoiTrangGiayCaoCap.Controllers
{
    public class SiteController : Controller
    {
        private LinkDAO linkDAO = new LinkDAO();
        private PostDAO postDAO = new PostDAO();
        private ProductDAO productDAO = new ProductDAO();
        private CategoryDAO categoryDAO = new CategoryDAO();
        private TopicDAO topicDAO = new TopicDAO();
        // GET: Site
        public ActionResult Index(string slug=null, int? page=null)
        {
            if(slug==null)
            {
                return this.Home();
            }
            else
            {
                Link link = linkDAO.getRow(slug);
                if(link!=null)
                {
                    string typelink = link.TypeLink;
                    switch(typelink)
                    {
                        case "category":
                            {
                                return this.ProductCategory(slug, page);
                            }
                        case "topic":
                            {
                                return this.PostTopic(slug, page);
                            }
                        case "page":
                            {
                                return this.PostPage(slug);
                            }
                        default:
                            {
                                return this.Error404(slug);
                            }
                    }    
                }  
                else
                {
                    Product product = productDAO.getRow(slug);
                    if(product!=null)
                    {
                        return this.ProductDetail(slug);
                    }
                    else
                    {
                        Post post = postDAO.getRow(slug, "Post");
                        if(post!=null)
                        {
                            return this.PostDetail(post);
                        }
                        else
                        {
                            return this.Error404(slug);
                        }
                    }
                }
            }
        }
        private ActionResult Home()
        {
            List<Category> list = categoryDAO.getListByParentId(0);
            return View("Home",list);
        }
        public ActionResult HomeProduct(int id)
        {
            Category category = categoryDAO.getRow(id);
            ViewBag.Category = category;
            //3 cấp
            List<int> listcatid = new List<int>();
            listcatid.Add(id);//Cấp 1
            List<Category> listcategory2 = categoryDAO.getListByParentId(id);
            if(listcategory2.Count()!=0)
            {
                foreach(var category2 in listcategory2)
                {
                    listcatid.Add(category2.Id);//Cấp 2
                    List<Category> listcategory3 = categoryDAO.getListByParentId(category2.Id);
                    if(listcategory3.Count() != 0)
                    {
                        foreach(var category3 in listcategory3)
                        {
                            listcatid.Add(category3.Id);//Cấp 3
                        }    
                    }    

                }
            }    
            List<ProductInfo> list = productDAO.getListByListCatId(listcatid, 4);
            return View("HomeProduct",list);
        }
        public ActionResult Product(int? page)
        {
            int pageSize = 9;
            int pageNumber = page ?? 1;
            IPagedList<ProductInfo> list = productDAO.getList(pageSize, pageNumber);
            return View("Product",list);
        }
        public ActionResult Post(int? page)
        {
            List<PostInfo> list = postDAO.getList("Post");
            return View("Post", list);
        }
        private ActionResult ProductCategory(string slug, int? page)
        {
            int pageSize = 6;
            int pageNumber = page ?? 1;
            Category category = categoryDAO.getRow(slug);
            ViewBag.Category = category;
            //3 cấp
            List<int> listcatid = new List<int>();
            listcatid.Add(category.Id);//Cấp 1
            List<Category> listcategory2 = categoryDAO.getListByParentId(category.Id);
            if (listcategory2.Count() != 0)
            {
                foreach (var category2 in listcategory2)
                {
                    listcatid.Add(category2.Id);//Cấp 2
                    List<Category> listcategory3 = categoryDAO.getListByParentId(category2.Id);
                    if (listcategory3.Count() != 0)
                    {
                        foreach (var category3 in listcategory3)
                        {
                            listcatid.Add(category3.Id);//Cấp 3
                        }
                    }

                }
            }
            IPagedList<ProductInfo> list = productDAO.getListByListCatId(listcatid, pageSize, pageNumber);
            return View("ProductCategory",list);
        }
        private ActionResult ProductDetail(string slug)
        {
            int take = 8;
            var row = productDAO.getRow(slug);
            Category category = categoryDAO.getRow(slug);
            //3 cấp
            List<int> listcatid = new List<int>();
            int catid = row.Catid;
            listcatid.Add(catid);//Cấp 1
            List<Category> listcategory2 = categoryDAO.getListByParentId(catid);
            if (listcategory2.Count() != 0)
            {
                foreach (var category2 in listcategory2)
                {
                    listcatid.Add(category2.Id);//Cấp 2
                    List<Category> listcategory3 = categoryDAO.getListByParentId(category2.Id);
                    if (listcategory3.Count() != 0)
                    {
                        foreach (var category3 in listcategory3)
                        {
                            listcatid.Add(category3.Id);//Cấp 3
                        }
                    }

                }
            }
            List<ProductInfo> listother = productDAO.getListByListCatId(listcatid, take, row.Id,  true);
            ViewBag.Listother = listother;
            return View("ProductDetail", row);
        }
        private ActionResult PostTopic(string slug, int? page)
        {
            Topic topic = topicDAO.getRow(slug);
            ViewBag.Topic = topic;
            List<PostInfo> list = postDAO.getListByTopId(topic.Id, "Post", null);
            return View("PostTopic",list);
        }
        private ActionResult PostPage(string slug)
        {
            Post post = postDAO.getRow(slug,"page");
            return View("PostPage", post);
        }
        public ActionResult PostDetail(Post post)
        {
            ViewBag.ListOther = postDAO.getListByTopId(post.TopId,"Post",post.Id); 
            return View("PostDetail",post);
        }
        private ActionResult Error404(string slug)
        {
            return View("Error404");
        }
    }
}