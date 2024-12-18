﻿namespace SchoolManagementSystem.Models
{
    public class StudentCourseSelection
    {
        public int SelectionID { get; set; }
        public int StudentID { get; set; }
        public int CourseID { get; set; }
        public DateTime SelectionDate { get; set; }
        public bool IsApproved { get; set; }

        // İlişkili tablolar için navigasyon özellikleri
        public Student Student { get; set; } = null!;
        public Course Course { get; set; } = null!;
    }
}
