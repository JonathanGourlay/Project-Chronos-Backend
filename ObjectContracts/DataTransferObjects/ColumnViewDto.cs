using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ObjectContracts.DataTransferObjects
{
    public class ColumnViewDto
    {
        public int ColumnId { get; set; }
        [Required]
        public string TrelloColumnId { get; set; }
        public string ColumnName { get; set; }
        public int PointsTotal { get; set; }
        public int AddedPoints { get; set; }
        public List<TaskDto> Tasks { get; set; }

       


        public ColumnViewDto()
        {
        }

        
    }
}