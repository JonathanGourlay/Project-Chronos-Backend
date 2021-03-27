using Project_Chronos_Backend.Objects;

namespace Project_Chronos_Backend.API.Models
{

    // Using the https://refactoring.guru/design-patterns/command pattern
    public class CreateProject
    {
        public ProjectObject Project { get; set; }
        public TaskObject Task { get; set; }
    }
}
