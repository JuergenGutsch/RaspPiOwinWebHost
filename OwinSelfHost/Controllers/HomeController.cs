using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace OwinSelfHost.Controllers
{
	public class HomeController : ApiController
	{
        [HttpGet]
        public Customer Get(int id)
        {
            return new Customer {
                Id = id,
                Name = "Jürgen Gutsch",
                Email = "juergen@gutsch-online.de"
            };
        }
    }
}

