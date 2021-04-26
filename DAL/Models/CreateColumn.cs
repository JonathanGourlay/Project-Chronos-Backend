namespace DAL.Models
{
    public class CreateColumn
    {
        public string columnName { get; set; }
        public int projectId { get; set; }
        public int pointsTotal { get; set; }
        public int addedPointsTotal { get; set; }
    }
}
