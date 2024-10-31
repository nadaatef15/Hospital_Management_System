using static HMSContracts.Constants.SysEnums;
using static HMSContracts.Constants.SysConstants;

namespace HMSBusinessLogic.ManagePermissions
{
    public static class Permissions
    {
        public static List<string> GetPermissionforModel(string model)
        {
            return new List<string>()
            {
                 $"{Permission}.{model}.Create",
                 $"{Permission}.{model}.View",
                 $"{Permission}.{model}.DeleteRoleById",
                 $"{Permission}.{model}.Edit",
            };
        }

        public static List<string> GetAllPermissions()
        {
            List<string> AllPermissions = new List<string>();

            foreach (var model in Enum.GetValues(typeof(model)))
                AllPermissions.AddRange(GetPermissionforModel(model.ToString()));

            return AllPermissions;
        }
    }
}
