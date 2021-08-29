using DevFreela.Application.InputModels;
using DevFreela.Application.Services.Interfaces;
using DevFreela.Application.ViewModels;
using DevFreela.Core.Entities;
using DevFreela.Infrastructure.Persistence.DevFreelaDbContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevFreela.Application.Services.Implementations
{
    public class UserService : IUserService 
    {
        readonly DevFreelaDbContext _dbContext;

        public UserService(DevFreelaDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public int CreateUser(CreateUserInputModel inputModel)
        {
            User user = new User(inputModel.FullName, inputModel.Email, inputModel.BirthDate);

            _dbContext.Users.Add(user);

            return user.Id;
        }

        public UserViewModel GetById(int id)
        {
            var user = _dbContext.Users.SingleOrDefault(u => u.Id == id);

            if (user == null)
            {
                return null;
            }

            var userView = new UserViewModel(user.FullName, user.Email);

            return userView;

        }
    }
}
