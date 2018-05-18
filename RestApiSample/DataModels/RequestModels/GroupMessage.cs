using System.Collections.Generic;

namespace DataModels.RequestModels
{
    public class GroupMessage
    {
        public long Id { get; set; }

        public string Name { get; set; }

        public int CountOfStudents { get; set; }

        public long FacultyId { get; set; }

        public IEnumerable<long> StudentIds { get; set; } = new List<long>();
    }
}
