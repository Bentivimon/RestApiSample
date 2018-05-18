namespace GroupsAPI.Data.Models
{
    public class GroupEntity
    {
        public long Id { get; set; }

        public string Name { get; set; }

        public int CountOfStudents { get; set; }

        public long FacultyId { get; set; }

        public string StudentIds { get; set; }
    }
}
