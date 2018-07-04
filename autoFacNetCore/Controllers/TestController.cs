using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Library;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NetCore.IService;

namespace autoFacNetCore.Controllers
{
    public class TestController : Controller
    {
        public IInjectionTestService _testService;
        public TestController(IInjectionTestService testService)
        {
            _testService = testService;
        }
        public string Test()
        {
            return _testService.Test();
        }
        // GET: Test
        public ActionResult Index()
        {
            UserContext context = HttpContext.RequestServices.GetService(typeof(UserContext)) as UserContext;
            var data = context.GetAllUser();
            return View(data);
        } 
    }
}