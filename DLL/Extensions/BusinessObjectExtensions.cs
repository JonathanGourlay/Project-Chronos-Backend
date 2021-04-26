using System.Collections.Generic;
using System.Linq;
using BLL.BusinessObjects;
using ObjectContracts.DataTransferObjects;

namespace BLL.Extensions
{
    public static class BusinessObjectExtensions
    {
        public static List<ProjectDto> ToDto(this List<ProjectObject> project)
        {
            return project.Select(t => t.ToDto()).ToList();
        }
        public static List<ColumnDto> ToDto(this List<ColumnObject> columns)
        {
            return columns.Select(col => col.ToDto()).ToList();
        }

        public static List<UserDto> ToDto(this List<UserObject> users)
        {
            return users.Select(u => u.ToDto()).ToList();
        }

        public static List<TimeLogDto> ToDto(this List<TimeLogObject> timelogs)
        {
            return timelogs.Select(t => t.ToDto()).ToList();
        }

        public static List<TaskDto> ToDto(this List<TaskObject> tasks)
        {
            return tasks.Select(t => t.ToDto()).ToList();
        }



    }
}
