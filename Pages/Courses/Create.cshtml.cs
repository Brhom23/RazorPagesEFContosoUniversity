using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using RazorPagesEFContosoUniversity.Data;
using RazorPagesEFContosoUniversity.Models;

namespace RazorPagesEFContosoUniversity.Pages.Courses
{
    public class CreateModel : DepartmentNamePageModel
    {
        private readonly RazorPagesEFContosoUniversity.Data.MyContext _context;

        public CreateModel(RazorPagesEFContosoUniversity.Data.MyContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            //ViewData["DepartmentID"] = new SelectList(_context.Departments, "DepartmentID", "DepartmentID");
            PopulateDepartmentsDropDownList(_context);

            return Page();
        }

        [BindProperty]
        public Course Course { get; set; }

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            var emptyCourse = new Course();
            if (await TryUpdateModelAsync<Course>(
                 emptyCourse,
                 "course",   // Prefix for form value.
                 s => s.CourseID, s => s.DepartmentID, s => s.Title, s => s.Credits))
            {
                _context.Courses.Add(emptyCourse);
                await _context.SaveChangesAsync();
                return RedirectToPage("./Index");
            }

            // Select DepartmentID if TryUpdateModelAsync fails.
            PopulateDepartmentsDropDownList(_context, emptyCourse.DepartmentID);
            return Page();


            //if (!ModelState.IsValid)
            //{
            //    return Page();
            //}

            //_context.Courses.Add(Course);
            //await _context.SaveChangesAsync();

            //return RedirectToPage("./Index");
        }
    }
}
