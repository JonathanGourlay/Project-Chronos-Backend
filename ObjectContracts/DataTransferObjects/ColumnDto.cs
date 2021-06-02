using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace ObjectContracts.DataTransferObjects
{
    public class ColumnDto
    {
        public int ColumnId { get; set; }
        public int ProjectId { get; set; }
        [Required]
        public string TrelloColumnId { get; set; }
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
        public ColumnDto()
        {
        }
        public ColumnDto(ColumnViewDto dto)
        {
            ColumnId = dto.ColumnId;
            TrelloColumnId = dto.TrelloColumnId;
            ColumnName = dto.ColumnName;
            PointsTotal = dto.PointsTotal;
            AddedPoints = dto.AddedPoints;
            Tasks = dto.Tasks;
        }

       

        public void AddTask(TaskViewDto dto)
        {
            Tasks ??= new List<TaskDto>();
            Tasks.ToList().Add(new TaskDto(dto));
        }

        public void AddTask(TaskDto dto)
        {
            if (Tasks == null)
            {
                Tasks = new List<TaskDto>();
            }
            Tasks.ToList().Add(dto);
        }
    }
}