using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Service.Model;
using Infrastructure.UnitOfWork;
using Infrastructure;

namespace Service
{
    public interface IRoleService
    {
       void insert(role _entity);
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
    }
}
