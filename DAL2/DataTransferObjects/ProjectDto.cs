using System.Collections.Generic;

namespace DAL.DataTransferObjects
{
    public class ProjectDto
    {
        public int ProjectId { get; set; }
        public string ProjectName { get; set; }
        public List<UserDto> Users { get; set; }
        public List<ColumnDto> Columns { get; set; }
    }
}