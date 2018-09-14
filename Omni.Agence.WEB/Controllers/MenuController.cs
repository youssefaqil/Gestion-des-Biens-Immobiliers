using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Omni.Agence.DAL;
namespace Omni.Agence.WEB.Controllers
{
    //[Authorize (Roles="admin,user")]
    public class MenuController : Controller
    {
        private AgenceEntities db = new AgenceEntities();
        public ViewResult _CreateMenu()
        {

            return View("_CreateMenu");
        }      
    }
}