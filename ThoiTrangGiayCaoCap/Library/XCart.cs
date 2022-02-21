using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ThoiTrangGiayCaoCap
{
    public class XCart
    {
        public List<CartItem> AddCart(CartItem cartitem)
        {

            List<CartItem> listcart;
            if (System.Web.HttpContext.Current.Session["MyCart"].Equals(""))
            {
                listcart = new List<CartItem>();
                listcart.Add(cartitem);
                System.Web.HttpContext.Current.Session["MyCart"] = listcart;
            }
            else
            {
                listcart = (List<CartItem>)System.Web.HttpContext.Current.Session["MyCart"];// Eps kiểu
                //Kiểm tra productid trong danh sách có hay chưa
                if (listcart.Where(m => m.ProductId == cartitem.ProductId).Count() != 0)
                {
                    //Đã có
                    int vt = 0;
                    foreach (var item in listcart)
                    {
                        if (item.ProductId == cartitem.ProductId)
                        {
                            listcart[vt].Quantity += 1;
                            listcart[vt].Amount = (listcart[vt].Quantity* listcart[vt].Price);
                        }
                        vt++;
                    }
                    System.Web.HttpContext.Current.Session["MyCart"] = listcart;

                }
                else
                {
                    //Chưa có
                    listcart.Add(cartitem);
                    System.Web.HttpContext.Current.Session["MyCart"] = listcart;
                }
            }
            return listcart;
        }
        public void UpdateCart(string[] arrquantity)
        {
            List<CartItem> listcart = this.getCart();
            int vt = 0;
            foreach(CartItem cartItem in listcart)
            {
                listcart[vt].Quantity = int.Parse(arrquantity[vt]);
                listcart[vt].Amount = (listcart[vt].Quantity * listcart[vt].Price);
                vt++;
            }
            System.Web.HttpContext.Current.Session["MyCart"] = listcart;
        }
        public void DelCart(int? productid=null)
        {
            if(productid!=null)
            {
                if (!System.Web.HttpContext.Current.Session["MyCart"].Equals(""))
                {
                    List<CartItem> listcart = (List<CartItem>)System.Web.HttpContext.Current.Session["MyCart"];
                    int vt = 0;
                    foreach (var item in listcart)
                    {
                        if (item.ProductId == productid)
                        {
                            listcart.RemoveAt(vt);
                            break;
                        }
                        vt++;
                    }
                    System.Web.HttpContext.Current.Session["MyCart"] = listcart;
                }

            }
            else
            {
                System.Web.HttpContext.Current.Session["MyCart"] = "";
            }
        }
        public List<CartItem> getCart()
        {
            if (System.Web.HttpContext.Current.Session["MyCart"].Equals(""))
            {
                return null;
            }
            return (List<CartItem>)System.Web.HttpContext.Current.Session["MyCart"];
        }
    }
}