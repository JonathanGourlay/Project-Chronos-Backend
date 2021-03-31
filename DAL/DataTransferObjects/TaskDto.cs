using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DAL.DataTransferObjects
{
    public class TaskDto
    {
        public int TaskId { get; set; }
        public string TaskName { get; set; }
        public string Comments { get; set; }
        public List<TimeLogDto> Timelogs { get; set; }
        public List<UserDto> Users { get; set; }

        public TaskDto(TaskViewDto dto)
        {
            TaskId = dto.TaskId;
            TaskName = dto.TaskName;
            Comments = dto.Comments;
        }

        public TaskDto()
        {
            Timelogs = new List<TimeLogDto>();
            Users = new List<UserDto>();
        }

        public void AddTimelog(TimeLogDto timelog)
        {
            // for safety as the constructor might not have run
            if (Timelogs == null)
            {
                Timelogs = new List<TimeLogDto>();
            }

            Timelogs.Add(timelog);
        }

        public void AddTimelog(IEnumerable<TimeLogDto> timelogs)
        {
            if (Timelogs == null)
            {
                Timelogs = new List<TimeLogDto>();
            }
            
            Timelogs.AddRange(timelogs);
        }
        public void AddUser(UserDto user)
        {
            // for safety as the constructor might not have run
            if (Users == null)
            {
                Users = new List<UserDto>();
            }

            Users.Add(user);
        }

        public void AddUser(IEnumerable<UserDto> users)
        {
            if (Users == null)
            {
                Users = new List<UserDto>();
            }

            Users.AddRange(users);
        }
    }
}