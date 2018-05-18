using System.Collections.Generic;

namespace DataModels.RequestModels
{
    public class ExtendedGroupMessage
    {
        public long Id { get; set; }

        public string Name { get; set; }

        public int CountOfStudents { get; set; }

        public long FacultyId { get; set; }

        public IEnumerable<StudentMessage> Studnets { get; set; } = new List<StudentMessage>();
    }
}
