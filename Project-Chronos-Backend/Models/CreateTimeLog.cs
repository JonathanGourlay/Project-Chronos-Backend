using Project_Chronos_Backend.BusinessObjects;

namespace Project_Chronos_Backend.Models
{
    public class CreateTimeLog
    {
        public TimeLogObject timeLog { get; set; }
        public int userId { get; set; }
        public int taskId { get; set; }
    }
}
