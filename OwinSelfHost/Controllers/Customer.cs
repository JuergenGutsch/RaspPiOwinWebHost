using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace OwinSelfHost.Controllers
{
	public class Customer
	{
        public int Id {
            get;
            set;
        }

        public string Name {
            get;
            set;
        }

        public string Email {
            get;
            set;
        }
	}

}

