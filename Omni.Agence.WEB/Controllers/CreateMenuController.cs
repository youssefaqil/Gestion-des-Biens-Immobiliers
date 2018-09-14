using Omni.Agence.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Omni.Agence.WEB.Controllers
{
    public class CreateMenuController : ControllerAuth
    {
        private AgenceEntities context = new AgenceEntities();
      
        public ActionResult Index()
        {
            List<Omni.Agence.DAL.MenuItem> menu = context.MenuItems.Where(m => m.Parent_Id == 3 ).OrderBy(m=> m.Order).ToList();
            return PartialView("_CreateMenu", menu);
        }
    }
}