﻿namespace DataModels.RequestModels
{
    public class StudentRequestModel
    {
        public long Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public int Age { get; set; }

        public long GroupId { get; set; }
    }
}
