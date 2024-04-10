using StudentManagementApi.Dto;
using StudentManagementApi.Models;

namespace StudentManagementApi.Mappings
{
    public static class AccountMapping
    {
        public static User MapAccountRegisterDtoToUser(this AccountRegisterDto accountRegister)
        {
            var user = new User
            {
                FirstName = accountRegister.FirstName,
                LastName = accountRegister.LastName,
                UserName = accountRegister.UserName,
                Email = accountRegister.Email,
            };
            return user;
        }
    }
}