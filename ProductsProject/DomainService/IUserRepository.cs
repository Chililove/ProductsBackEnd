using Core.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProductsProject.Core.DomainService
{
    public interface IUserRepository<User>
    {
        IEnumerable<User> GetAll();
        User Get(long id);
        void Add(User entity);
        void Edit(User entity);
        void Remove(long id);
    }
}
