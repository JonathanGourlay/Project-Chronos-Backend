using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DAL.DataTransferObjects;
using Project_Chronos_Backend.BusinessObjects;

namespace Project_Chronos_Backend.Extensions
{
    public static class BusinessObjectExtensions
    {
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
