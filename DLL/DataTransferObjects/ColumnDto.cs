using System.Collections.Generic;

namespace DLL.DataTransferObjects
{
    public class ColumnDto
    {
        public int ColumnId { get; set; }
        public string ColumnName { get; set; }
        public int PointsTotal { get; set; }
        public int AddedPoints { get; set; }
        public List<TaskDto> Tasks { get; set; }

        public ColumnDto() {}

        public ColumnDto(int columnId, string columnName, int pointsTotal, int addedPoints, List<TaskDto> tasks)
        {
            ColumnId = columnId;
            ColumnName = columnName;
            PointsTotal = pointsTotal;
            AddedPoints = addedPoints;
            Tasks = tasks;
        }

        public void AddTask(TaskViewDto dto)
        {
            Tasks ??= new List<TaskDto>();
            Tasks.Add(new TaskDto(dto));
        }

        public void AddTask(TaskDto dto)
        {
            Tasks ??= new List<TaskDto>();
            Tasks.Add(dto);
        }
    }
}