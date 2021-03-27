using System.Collections.Generic;

namespace DAL.DataTransferObjects
{
    public class ColumnDto
    {
        public int ColumnId { get; set; }
        public string ColumnName { get; set; }
        public List<TaskDto> Tasks { get; set; }
    }
}