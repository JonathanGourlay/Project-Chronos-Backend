using System;
using System.Collections.Generic;
using System.Linq;

namespace ObjectContracts.DataTransferObjects
{
    public class ColumnDto
    {
        public int ColumnId { get; set; }
        public string ColumnName { get; set; }
        public int PointsTotal { get; set; }
        public int AddedPoints { get; set; }
        public IEnumerable<TaskDto> Tasks { get; set; }

       

        //public ColumnDto(int columnId, string columnName, int pointsTotal, int addedPoints, List<TaskDto> tasks)
        //{
        //    ColumnId = columnId;
        //    ColumnName = columnName;
        //    PointsTotal = pointsTotal;
        //    AddedPoints = addedPoints;
        //    Tasks = tasks;
        //}

        public ColumnDto(ColumnViewDto dto)
        {
            ColumnId = dto.ColumnId;
            ColumnName = dto.ColumnName;
            PointsTotal = dto.PointsTotal;
            AddedPoints = dto.AddedPoints;
            Tasks = dto.Tasks;
        }

        public ColumnDto()
        {
            ColumnId = new int();
            ColumnName = new string(ColumnName);
            PointsTotal = new int();
            AddedPoints = new int();
            Tasks = new List<TaskDto>();
        }

        public void AddTask(TaskViewDto dto)
        {
            Tasks ??= new List<TaskDto>();
            Tasks.ToList().Add(new TaskDto(dto));
        }

        public void AddTask(TaskDto dto)
        {
            Tasks ??= new List<TaskDto>();
            Tasks.ToList().Add(dto);
        }
    }
}