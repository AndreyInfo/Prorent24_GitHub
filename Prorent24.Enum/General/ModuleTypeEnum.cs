namespace Prorent24.Enum.General
{
    public enum ModuleTypeEnum
    {
        [RoleManage(Order = 1, IsAlwaysTrue = true, IsMenuItem = false, Description = "")]
        General = 0,
        [RoleManage(Order = 0, Description = "Users only see information in the dashboard that they are also allowed to see in other modules.")]
        Dashboard = 1,
        [RoleManage(Order = 3, Description = "To view all projects in the warehouse you need the 'All projects' permissions in the project module")]
        Warehouse = 2,
        [RoleManage(Order = 4, Description = "Access to this module automatically allows viewing access to projects of which this crew member is the account manager.")]
        Projects = 3,
        [RoleManage(Order = 5, Description = "The crew member sees all projects that are visible to him/her in the project module.")]
        CrewPlanner = 4,
        [RoleManage(Order = 6, Description = "Access to the subhire module")]
        Subhire = 5,
        [RoleManage(Order = 7, Description = "")]
        Invoices = 6,
        [RoleManage(Order = 8, Description = "")]
        Equipment = 7,
        [RoleManage(Order = 9, Description = "")]
        Contacts = 8,
        [RoleManage(Order = 10, Description = "")]
        CrewMembers = 9,
        [RoleManage(Order = 11, Description = "")]
        Vehicles = 10,
        [RoleManage(Order = 12, Description = "This setting also influences the task widget in the sidebar.")]
        Tasks = 11,
        [RoleManage(Order = 13, Description = "")]
        TimeRegistration = 12,
        [RoleManage(Order = 14, Description = "")]
        Maintenance = 13,
        [RoleManage(Order = 15, Description = "Statistics on projects, equipment, crew members. Access to the statistics module provides insight into confidential information.")]
        Statistics = 14,
        [RoleManage(Order = 16, Description = "All important settings can be customized in the configuration menu.")]
        Configuration = 15,
        [RoleManage(Order = 2, IsAlwaysTrue = true, Description = "Every user has access to their own schedule.")]
        MySchedule = 16,
        [RoleManage(Order = 17, IsMenuItem = false, Description = "Every user has access to their own schedule.")]
        Messages = 17,
        [RoleManage(Order = 18, IsMenuItem = false, Description = "")]
        SubhireShortage =18,
        [RoleManage(Order = 19, IsMenuItem = false, Description = "", IsRoleModule = false)]
        Repairs = 19,
        [RoleManage(Order = 20, IsMenuItem = false, Description = "", IsRoleModule = false)]
        Inspections = 20,
    }
}
