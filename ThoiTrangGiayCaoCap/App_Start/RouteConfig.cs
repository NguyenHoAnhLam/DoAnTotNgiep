using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace ThoiTrangGiayCaoCap
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            //Khai báo url cố định
            routes.MapRoute(
                name: "TatCaSanPham",
                url: "tat-ca-san-pham",
                defaults: new { controller = "Site", action = "Product", id = UrlParameter.Optional }
            );
            routes.MapRoute(
                name: "TatCaBaiViet",
                url: "tat-ca-bai-viet",
                defaults: new { controller = "Site", action = "Post", id = UrlParameter.Optional }
            );
            routes.MapRoute(
                name: "LienHe",
                url: "lien-he",
                defaults: new { controller = "Lienhe", action = "Index", id = UrlParameter.Optional }
            );
            routes.MapRoute(
               name: "GioHang",
               url: "gio-hang",
               defaults: new { controller = "Giohang", action = "Index", id = UrlParameter.Optional }
           );
            routes.MapRoute(
               name: "ThanhToan",
               url: "thanh-toan",
               defaults: new { controller = "Giohang", action = "ThanhToan", id = UrlParameter.Optional }
           );
            routes.MapRoute(
               name: "KetQuaTimKiem",
               url: "tim-kiem",
               defaults: new { controller = "Timkiem", action = "KetQuaTimKiem", id = UrlParameter.Optional }
           );
            routes.MapRoute(
              name: "DangNhap",
              url: "dang-nhap",
              defaults: new { controller = "Khachhang", action = "DangNhap", id = UrlParameter.Optional }
          );
            routes.MapRoute(
              name: "DangXuat",
              url: "dang-xuat",
              defaults: new { controller = "Khachhang", action = "DangXuat", id = UrlParameter.Optional }
          );
            routes.MapRoute(
              name: "DangKy",
              url: "dang-ky",
              defaults: new { controller = "Khachhang", action = "DangKy", id = UrlParameter.Optional }
          );
            routes.MapRoute(
              name: "DangKyThanhCong",
              url: "dang-ky-thanh-cong",
              defaults: new { controller = "Khachhang", action = "DangKyThanhCong", id = UrlParameter.Optional }
          );
            routes.MapRoute(
             name: "DatMuaThanhCong",
             url: "dat-mua-thanh-cong",
             defaults: new { controller = "Giohang", action = "DatMuaThanhCong", id = UrlParameter.Optional }
         );

            //Khai báo động
            routes.MapRoute(
                name: "SiteSlug",
                url: "{slug}",
                defaults: new { controller = "Site", action = "Index", id = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Site", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
