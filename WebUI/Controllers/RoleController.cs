using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Service.Model;
using Service;

namespace WebUI.Controllers
{
    public class RoleController : Controller
    {
        private readonly IRoleService _roleService;
        public RoleController()
        {
            this._roleService = new RoleService();
        }
        // GET: Role
        public ActionResult Index()
        {
            return View(_roleService.getAll());
        }

        public JsonResult getPaginated(DataTableAjaxPostModel model)
        {
            // action inside a standard controller
            int filteredResultsCount;
            int totalResultsCount;
            var res = CustomSearchFunction(model, out filteredResultsCount, out totalResultsCount);

            throw new NotImplementedException();
        }

        private IList<role> CustomSearchFunction(DataTableAjaxPostModel model, out int filteredResultsCount, out int totalResultsCount)
        {
            var searchBy = (model.search != null) ? model.search.value : null;
            var take = model.length;
            string sortBy = "";
            bool sortDir = true;

            if (model.order != null)
            {
                // in this example we just default sort on the 1st column
                sortBy = model.columns[model.order[0].column].data;
                sortDir = model.order[0].dir.ToLower() == "asc";
            }

            var result =   _roleService.GetDataFromDbaseForPagination(searchBy, take, skip, sortBy, sortDir, out filteredResultsCount, out totalResultsCount);
            if (result == null)
            {
                // empty collection...
                return new List<role>();
            }
            return result;
           
        }
    }
}