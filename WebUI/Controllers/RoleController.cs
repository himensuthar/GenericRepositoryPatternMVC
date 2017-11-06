using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Service;
using Service.Model;

namespace WebUI.Controllers
{
    public class RoleController : ApiController
    {
        private readonly IRoleService _roleService;
        public RoleController()
        {
            _roleService = new RoleService();
        }
        [HttpPost]
        public HttpResponseMessage Post([FromBody]role _role)
        {
            _roleService.insert(_role);
            return new HttpResponseMessage(HttpStatusCode.OK);
        }
    }
}
