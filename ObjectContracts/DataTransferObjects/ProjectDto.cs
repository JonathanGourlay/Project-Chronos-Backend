using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace ObjectContracts.DataTransferObjects
{
   
    public class ProjectDto 
    {
        public int ProjectId { get; set; }
        [Required]
        public string TrelloProjectId { get; set; }
        [Required]
        public string ProjectName { get; set; }
        [Required]
        public DateTime ProjectStartTime { get; set; }
        public DateTime ProjectEndTime { get; set; }
        public DateTime ExpectedEndTime { get; set; }
        public int PointsTotal { get; set; }
        public int AddedPoints { get; set; }
        public string ProjectComplete { get; set; }
        public string ProjectArchived { get; set; }
        public int TimeIncrement { get; set; }
        public IEnumerable<UserDto> Users { get; set; }
        public IEnumerable<ColumnDto> Columns { get; set; }


        public ProjectDto()
        {

        }
        public ProjectDto(ProjectViewDto viewDto, IEnumerable<UserDto> users, IEnumerable<ColumnDto> columnDtos)
        {
            ProjectId = viewDto.ProjectId;
            ProjectName = viewDto.ProjectName;
            ProjectStartTime = viewDto.ProjectStartTime;
            ProjectEndTime = viewDto.ProjectEndTime;
            ExpectedEndTime = viewDto.ExpectedEndTime;
            PointsTotal = viewDto.PointsTotal;
            AddedPoints = viewDto.AddedPoints;
            ProjectComplete = viewDto.ProjectComplete;
            ProjectArchived = viewDto.ProjectArchived;
            TimeIncrement = viewDto.TimeIncrement;
            Users = users.ToList();
            Columns = columnDtos.ToList();
        }
    }
}