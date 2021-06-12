using Microsoft.AspNetCore.Mvc.RazorPages;
using RazorPagesEFContosoUniversity.Data;
using RazorPagesEFContosoUniversity.Models;
using RazorPagesEFContosoUniversity.Models.SchoolViewModels;
using System.Collections.Generic;
using System.Linq;


namespace RazorPagesEFContosoUniversity.Pages.Instructions
{
    public class InstructorCoursesPageModel : PageModel
    {
        public List<AssignedCourseData> AssignedCourseDataList;

        public void PopulateAssignedCourseData(MyContext context,
                                               Instructor instructor)
        {
            var allCourses = context.Courses;
            var instructorCourses = new HashSet<int>(
                instructor.Courses.Select(c => c.CourseID));
            AssignedCourseDataList = new List<AssignedCourseData>();
            foreach (var course in allCourses)
            {
                AssignedCourseDataList.Add(new AssignedCourseData
                {
                    CourseID = course.CourseID,
                    Title = course.Title,
                    Assigned = instructorCourses.Contains(course.CourseID)
                });
            }
        }
    }
}