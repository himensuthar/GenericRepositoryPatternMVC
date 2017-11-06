using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Service.Model;
using Infrastructure.UnitOfWork;
using Infrastructure;
using System.Linq.Expressions;
using LinqKit;

namespace Service
{
    public interface IRoleService
    {
        role getById(long id);
        IQueryable<role> getAll();
        void insert(role _entity);

        void update(role _entity);

        void delete(role id);
        IQueryable<role> GetDataFromDbaseForPagination(string searchBy, int take, int skip, string sortBy, bool sortDir, out int filteredResultsCount, out int totalResultsCount);
        Expression<Func<tbl_role, bool>> BuildDynamicWhereClause(string searchBy);
    }

    public class RoleService : IRoleService
    {
        private readonly IUnitOfWork _uow;
        public RoleService()
        {
            this._uow = new UnitOfWork();
        }
        public void insert(role _entity)
        {
            tbl_role entity = new tbl_role { name = _entity.name, description = _entity.description };
            _uow.RoleRepository.Insert(entity);
            _uow.Save();
        }

        public role getById(long id)
        {
            tbl_role _tbl_role = _uow.RoleRepository.GetById(id);
            return new role
                {
                    id = _tbl_role.id,
                    name = _tbl_role.name,
                    description = _tbl_role.description };
        }

        public IQueryable<role> getAll()
        {
            IQueryable<tbl_role> tbl_role = _uow.RoleRepository.GetAll();

            IQueryable<role> roles = from a in tbl_role
                                     select new role
                                     {
                                         id = a.id,
                                         name = a.name,
                                         description = a.description
                                     };
            return roles;
        }

        public void update(role _entity)
        {
            tbl_role _tbl_role = new tbl_role
                                {
                                    id =_entity.id,
                                    name =_entity.name,
                                    description =_entity.description
                                };

            _uow.RoleRepository.Edit(_tbl_role);
            _uow.Save();
        }

        public void delete(role _entity)
        {
            tbl_role _tbl_role = new tbl_role
            {
                id = _entity.id,
                name = _entity.name,
                description = _entity.description
            };
            _uow.RoleRepository.Delete(_tbl_role);
            _uow.Save();
        }
        public IQueryable<role> GetDataFromDbaseForPagination(string searchBy, int take, int skip, string sortBy, bool sortDir, out int filteredResultsCount, out int totalResultsCount)
        {
            var query = _uow.RoleRepository.GetAll();
            dynamic whereClause = BuildDynamicWhereClause(searchBy);
            IQueryable<tbl_role> _tbl_role =  _uow.RoleRepository.GetPaginated(query, whereClause, searchBy, take, skip, out totalResultsCount, out filteredResultsCount, sortBy, sortDir);
            IQueryable<role> roleQ = from a in _tbl_role
                                     select new role
                                     {
                                         id = a.id,
                                         name = a.name,
                                         description = a.name
                                     };
            return roleQ;
        }

        public Expression<Func<tbl_role, bool>> BuildDynamicWhereClause(string searchValue)
        {
            // simple method to dynamically plugin a where clause
            var predicate = PredicateBuilder.New<tbl_role>(true); // true -where(true) return all
            if (String.IsNullOrWhiteSpace(searchValue) == false)
            {
                // as we only have 2 cols allow the user type in name 'firstname lastname' then use the list to search the first and last name of dbase
                var searchTerms = searchValue.Split(' ').ToList().ConvertAll(x => x.ToLower());
                predicate = predicate.Or(s => searchTerms.Any(srch => s.name.ToLower().Contains(srch)));
                predicate = predicate.Or(s => searchTerms.Any(srch => s.description.ToLower().Contains(srch)));
            }
            return predicate;
        }
    
    }
}
