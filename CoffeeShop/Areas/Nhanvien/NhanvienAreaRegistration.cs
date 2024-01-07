using System.Web.Mvc;

namespace CoffeeShop.Areas.Admin
{
    public class NhanvienAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "Nhanvien";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                name: "Nhanvien_default",
                url: "Nhanvien/{controller}/{action}/{id}",
                defaults: new { action = "Index", controller = "Nhanvien", id = "" }
            );
        }
    }
}