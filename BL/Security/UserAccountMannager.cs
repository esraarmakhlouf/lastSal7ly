using BL.Infrastructure;
using BL.Secuirty;
using BL.Security;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using static BL.SharedCS.Enumrations;

namespace BL.Security
{
    public static class UserAccountMannager
    {
        public static bool AuthenticateCustomer(LoginViewModel user, ISecurity sec, IUnitOfWork uow)
        {
            bool isValid = false;
            if (!isValid)
            {
                string encPassword = EncryptANDDecrypt.EncryptText(user.Password);
                var result = uow.CustomerRepository.GetMany(ent => ent.Email.ToLower() == user.UserName.ToLower() && ent.Password == encPassword && !ent.IsDeleted && ent.IsActive).ToHashSet();
                isValid = result.Count() == 1;
            }
            return isValid;
        }

        public static bool AuthenticateUser(LoginViewModel user, ISecurity sec, IUnitOfWork uow)
        {
            bool isValid = false;

            if (!isValid)
            {
                string encPassword = EncryptANDDecrypt.EncryptText(user.Password);
                var result = uow.UsersRepository.GetMany(ent => ent.UserName.ToLower() == user.UserName.ToLower() && ent.Password == encPassword && !ent.IsDeleted && ent.IsActive).ToList();

                isValid = result.Count() == 1;
            }

            return isValid;
        }

        public static bool HasPermission(IUnitOfWork uow, ISecurity sec, long userId, EN_Screens screen, EN_Permissions permission)
        {
            bool hasPermission = false;
            var userEntity = uow.UsersRepository.GetById(userId);
            if (userEntity.IsMaster) return true;
            hasPermission = sec.GetUserPermissions(userId).Where(ent => ent.ScreenId == (int)screen && ent.PermissionId == (int)permission).Count() > 0;

            return hasPermission;
        }

        public static bool HasPermission(IUnitOfWork uow, ISecurity sec, long userId, EN_Modules moudule)
        {
            bool hasPermission = false;
            var userEntity = uow.UsersRepository.GetById(userId);
            if (userEntity.IsMaster) return true;
            var moduleScreens = uow.ScreensRepository.GetMany(ent => ent.IsActive && !ent.IsDeleted && ent.ModuleId == (int)moudule).ToList();
            var userPsermissions = sec.GetUserPermissions(userId).Where(ent => ent.PermissionId == (int)EN_Permissions.View && moduleScreens.Any(ms => ms.Id == ent.ScreenId));
            hasPermission = userPsermissions.Count() > 0;
            return hasPermission;
        }
    }
}
