using System;
using System.Collections.Generic;
using System.Linq;

namespace DLL.DataTransferObjects
{
    public class ProjectDto
    {
        public int ProjectId { get; set; }
        public string ProjectName { get; set; }
        public DateTime ProjectStartTime { get; set; }
        public DateTime ProjectEndTime { get; set; }
        public DateTime ExpectedEndTime { get; set; }
        public int PointsTotal { get; set; }
        public int AddedPoints { get; set; }
        public string ProjectCompleated { get; set; }
        public string ProjectArchived { get; set; }
        public int TimeIncrement { get; set; }
        public List<UserDto> Users { get; set; }
        public List<ColumnDto> Columns { get; set; }

        public ProjectDto() {}

        public ProjectDto(ProjectViewDto viewDto, IEnumerable<UserDto> users, IEnumerable<ColumnDto> columnDtos)
        {
            ProjectId = viewDto.ProjectId;
            ProjectName = viewDto.ProjectName;
            ProjectStartTime = viewDto.ProjectStartTime;
            ProjectEndTime = viewDto.ProjectEndTime;
            ExpectedEndTime = viewDto.ExpectedEndTime;
            PointsTotal = viewDto.PointsTotal;
            AddedPoints = viewDto.AddedPoints;
            ProjectCompleated = viewDto.ProjectCompleated;
            ProjectArchived = viewDto.ProjectArchived;
            TimeIncrement = viewDto.TimeIncrement;
            Users = users.ToList();
            Columns = columnDtos.ToList();
        }
    }
}