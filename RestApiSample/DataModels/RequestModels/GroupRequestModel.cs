using System.Collections.Generic;

namespace DataModels.RequestModels
{
    public class GroupRequestModel
    {
        public long Id { get; set; }

        public string Name { get; set; }

        public int CountOfStudents { get; set; }

        public long FacultyId { get; set; }

        public List<long> StudentIds { get; set; }
    }
}
