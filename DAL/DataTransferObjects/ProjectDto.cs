using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace DAL.DataTransferObjects
{
    public class ProjectDto
    {
        public int ProjectId { get; set; }
        public string ProjectName { get; set; }
        public List<UserDto> Users { get; set; }
        public List<ColumnDto> Columns { get; set; }

        public ProjectDto() {}

        public ProjectDto(ProjectViewDto viewDto, IEnumerable<UserDto> users, IEnumerable<ColumnDto> columnDtos)
        {
            ProjectId = viewDto.ProjectId;
            ProjectName = viewDto.ProjectName;
            Users = users.ToList();
            Columns = columnDtos.ToList();
        }
    }
}