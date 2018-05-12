using System.Collections.Generic;

namespace RestApiTest.Data.Models
{
    public class GroupEntity
    {
        public long Id { get; set; }

        public string Name { get; set; }

        public int CountOfStudents { get; set; }

        public long FacultyId { get; set; }

        public List<StudentEntity> Students { get; set; }
    }
}
