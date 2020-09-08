using BL.Infrastructure;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BL.Secuirty
{
    public interface ISecurity
    {
        //void GiveGroupAllPermissions(int[] ModuleIds, int groupId, bool isAllowed);
        //bool GroupHasAllPermissions(long groupId);
        //bool GroupHasAllModulePermissions(long groupId, long moduleId);
        //bool UserHasAllPermissions(long userId);
        //bool UserHasAllModulePermissions(long userId, long moduleId);
        List<GroupPermissions> GetUserPermissions(long userId);
        List<Groups> GetUserGroups(long userId);
        List<GroupPermissions> GetGroupPermissions(long groupId);
        //List<UserPermissionsModel> GetAllSystemPermissions();
    }

    public class Security : ISecurity
    {
        private readonly IUnitOfWork _uow;
        public static string generalPassword = "PwTgSpWd@2013";
        public Security(IUnitOfWork uow)  { _uow = uow;  }

        //public List<UserPermissionsModel> GetAllSystemPermissions()
        //{
        //    var userPermissions = new List<UserPermissionsModel>();

        //    userPermissions = (from module in _uow.ModulesRepository.GetAll()
        //                       join screen in _uow.ScreensRepository.GetAll()
        //                       on module.Id equals screen.ModuleId

        //                       join screenProcedure in _uow.ScreensProceduresRepository.GetAll()
        //                       on screen.Id equals screenProcedure.ScreenId

        //                       join procedure in _uow.ProceduresRepository.GetAll()
        //                       on screenProcedure.ProcedureId equals procedure.Id

        //                       select (new UserPermissionsModel { ModuleId = module.Id, ScreenId = screen.Id, ProcedureId = procedure.Id })
        //                            ).Distinct().ToList();

        //    return userPermissions;
        //}

        //public List<GroupPermissionsModel> GetGroupPermissions(long groupId)
        //{
        //    var groupPermissions = new List<GroupPermissionsModel>();

        //    groupPermissions = (from groupProcedure in _uow.UsersGroupsProceduresRepository.GetAll()
        //                        join userGroup in _uow.UsersGroupsRepository.GetAll()
        //                        on groupProcedure.UserGroupId equals userGroup.Id
        //                        join screen in _uow.ScreensRepository.GetAll()
        //                        on groupProcedure.ScreenId equals screen.Id
        //                        join module in _uow.ModulesRepository.GetAll()
        //                        on screen.ModuleId equals module.Id
        //                        join procedure in _uow.ProceduresRepository.GetAll()
        //                    on groupProcedure.ProcedureId equals procedure.Id
        //                        join screenProcedure in _uow.ScreensProceduresRepository.GetAll()
        //                      on groupProcedure.ScreenId equals screenProcedure.ScreenId

        //                        where (groupProcedure.UserGroupId == groupId)
        //                        select (new GroupPermissionsModel { ModuleId = module.Id, ScreenId = screen.Id, ProcedureId = procedure.Id, GroupId = groupProcedure.UserGroupId })
        //                             ).Distinct().ToList();

        //    return groupPermissions;
        //}
        public List<GroupPermissions> GetGroupPermissions(long groupId)
        {
            var groupPermissions = _uow.GroupPermissionsRepository.GetMany(ent => ent.GroupId == groupId).Distinct().ToList();
            return groupPermissions;
        }
        public List<GroupPermissions> GetUserPermissions(long userId)
        {
            var userGroups = GetUserGroups(userId);
            var userPermissions = _uow.GroupPermissionsRepository.GetMany(ent => userGroups.Any(ug => ug.Id == ent.GroupId)).Distinct().ToList();

            return userPermissions;
        }

        public List<Groups> GetUserGroups(long userId)
        {
            var userGroupsIds = _uow.UserGroupsRepository.GetMany(ent => ent.UserId == userId).Select(ent=>ent.GroupId).ToList();
            var userGroups = _uow.GroupsRepository.GetMany(ent => userGroupsIds.Contains(ent.Id)).Distinct().ToList();
            return userGroups;
        }

        //public void GiveGroupAllPermissions(int[] ModuleIds, int groupId, bool isAllowed)
        //{
        //    if (ModuleIds.Count() > 0)
        //    {
        //        string strQuery = "";

        //        //get all screens
        //        var allScreensIds = _uow.ScreensRepository.GetMany(ent => ModuleIds.ToList().Contains(ent.ModuleId ?? -1)).Select(ent => ent.Id).Distinct().ToList();

        //        foreach (int screenId in allScreensIds)
        //        {
        //            //get screen Permissions ids
        //            var screenPermissionsIDs = _uow.ScreensProceduresRepository.GetMany(ent=>ent.ScreenId==screenId).Select(ent=>ent.ProcedureId);

        //            //Update Exsisting permissions
        //            var oldPermissions =_uow.UsersGroupsProceduresRepository.GetMany(ent => ent.ScreenId == screenId && screenPermissionsIDs.Contains(ent.ProcedureId) && ent.UserGroupId == groupId).ToList();

        //            var oldGroupPermissionsIDs = oldPermissions.Select(ent => ent.Id).ToList();

        //            var oldPermissionsIDs = oldPermissions.Select(ent => ent.ProcedureId).ToList();

        //            if (oldGroupPermissionsIDs.Count() > 0)
        //            {
        //                strQuery = "UPDATE TBL_UsersGroupsProcedures SET IsDeleted= " + (isAllowed ? "0" : "1") + " WHERE Id IN(" + string.Join(", ", oldGroupPermissionsIDs) + ")";
        //                _uow.ExecuteSqlCommand(strQuery);
        //            }

        //            //Insert New permissions
        //            var newPermissionsIDs = screenPermissionsIDs.Where(ent => !oldPermissionsIDs.Contains(ent)).ToList();
        //            foreach (var newPermissionsID in newPermissionsIDs)
        //            {
        //                Tbl_UsersGroupsProcedures obj = new Tbl_UsersGroupsProcedures();
        //                obj.ScreenId = screenId;
        //                obj.ProcedureId = newPermissionsID;
        //                obj.UserGroupId = groupId;
        //                obj.IsDeleted = !isAllowed;
        //                _uow.UsersGroupsProceduresRepository.Add(obj);
        //            }
        //        }
        //        _uow.Save();
        //    }
        //}

        //public bool GroupHasAllModulePermissions(long groupId, long moduleId)
        //{
        //    bool hasAllPermissions = false;

        //    var allSystemPermissions = GetAllSystemPermissions().Where(ent => ent.ModuleId == moduleId).ToList();

        //    if (allSystemPermissions.Count() > 0)
        //    {
        //        var allGroupPermissions = GetGroupPermissions(groupId).Where(ent => ent.ModuleId == moduleId).ToList();
        //        if (allGroupPermissions.Count() > 0)
        //        { hasAllPermissions = allGroupPermissions.Count() == allSystemPermissions.Count(); }
        //    }

        //    return hasAllPermissions;
        //}

        //public bool GroupHasAllPermissions(long groupId)
        //{
        //    bool hasAllPermissions = false;

        //    var allSystemPermissions = GetAllSystemPermissions();

        //    if (allSystemPermissions.Count() > 0)
        //    {
        //        var allGroupPermissions = GetGroupPermissions(groupId);

        //        if (allGroupPermissions.Count() > 0)
        //        { hasAllPermissions = allGroupPermissions.Count() == allSystemPermissions.Count(); }
        //    }

        //    return hasAllPermissions;
        //}

        //public bool UserHasAllModulePermissions(long userId, long moduleId)
        //{
        //    bool hasAllPermissions = false;
        //    var allSystemPermissions = GetAllSystemPermissions().Where(ent => ent.ModuleId == moduleId).ToList();

        //    if (allSystemPermissions.Count() > 0)
        //    {
        //        var allofall = GroupPermissions(userId);

        //        var allUserPermissions = allofall.Where(ent => ent.isGroupPermission == false && ent.ModuleId == moduleId).ToList();

        //        var allwedUserPermissions = allUserPermissions.Where(ent => ent.isAllowed == true && ent.isGroupPermission == false && ent.ModuleId == moduleId).ToList();
        //        var allwedGroupPermissions = allofall.Where(ent => ent.isAllowed == true && ent.isGroupPermission == true && ent.ModuleId == moduleId).ToList();

        //        if (allUserPermissions.Count() > 0)
        //        { hasAllPermissions = allwedUserPermissions.Count() == allSystemPermissions.Count(); }
        //        else
        //        { hasAllPermissions = allwedGroupPermissions.Count() == allSystemPermissions.Count(); }
        //    }

        //    return hasAllPermissions;
        //}

        //public bool UserHasAllPermissions(long userId)
        //{
        //    bool hasAllPermissions = false;

        //    var allSystemPermissions = GetAllSystemPermissions();

        //    if (allSystemPermissions.Count() > 0)
        //    {
        //        var allofall =GroupPermissions(userId);
        //        var allUserPermissions = allofall.Where(ent => ent.isGroupPermission == false).ToList();

        //        var allwedUserPermissions = allUserPermissions.Where(ent => ent.isAllowed == true && ent.isGroupPermission == false).ToList();
        //        var allwedGroupPermissions = allofall.Where(ent => ent.isAllowed == true && ent.isGroupPermission == true).ToList();

        //        if (allUserPermissions.Count() > 0)
        //        { hasAllPermissions = allwedUserPermissions.Count() == allSystemPermissions.Count(); }
        //        else
        //        { hasAllPermissions = allwedGroupPermissions.Count() == allSystemPermissions.Count(); }
        //    }

        //    return hasAllPermissions;
        //}
    }
}