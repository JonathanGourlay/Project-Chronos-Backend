using System.Collections.Generic;

namespace ObjectContracts.DataTransferObjects
{
    public class ColumnViewDto
    {
        public int ColumnId { get; set; }
        public string ColumnName { get; set; }
        public int PointsTotal { get; set; }
        public int AddedPoints { get; set; }
        public List<TaskDto> Tasks { get; set; }

       


        public ColumnViewDto()
        {
        }

        
    }
}