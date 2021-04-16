using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.Controllers
{
    public class MagicController : Controller
    {
        public IActionResult Index()
        {
            return Content("В рот мне ноги!!О_о");
        }
    }
}
