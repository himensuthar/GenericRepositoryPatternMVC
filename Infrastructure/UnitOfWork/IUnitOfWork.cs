using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Infrastructure.Repository;

namespace Infrastructure.UnitOfWork
{
    public interface IUnitOfWork:IDisposable
    {
        IGenericRepository<tbl_user> UserRepository { get;}
        IGenericRepository<tbl_role> RoleRepository { get; }
        void Save();
    }

    public class UnitOfWork : IUnitOfWork
    {
        private IGenericRepository<tbl_user> _userRepository;
        private IGenericRepository<tbl_role> _roleRepository;
        public LearnMVCEntities context;
        public IGenericRepository<tbl_user> UserRepository
        {
            get {
                    if (_userRepository == null)
                        _userRepository = new GenericRepository<tbl_user>(context);
                    return _userRepository;
                }
        }

        public IGenericRepository<tbl_role> RoleRepository
        {
            get
            {
                if (_roleRepository == null)
                    _roleRepository = new GenericRepository<tbl_role>(context);
                return _roleRepository;
            }
        }

        public UnitOfWork()
        {
            this.context = new LearnMVCEntities();
        }

        public void Save()
        {
            this.context.SaveChanges();
        }

        public void Dispose()
        {
            
        }


    }
}
