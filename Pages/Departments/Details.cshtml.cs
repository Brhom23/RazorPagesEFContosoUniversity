using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using RazorPagesEFContosoUniversity.Data;
using RazorPagesEFContosoUniversity.Models;

namespace RazorPagesEFContosoUniversity.Pages.Departments
{
    public class DetailsModel : PageModel
    {
        private readonly RazorPagesEFContosoUniversity.Data.MyContext _context;

        public DetailsModel(RazorPagesEFContosoUniversity.Data.MyContext context)
        {
            _context = context;
        }

        public Department Department { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Department = await _context.Departments
                .Include(d => d.Administrator).FirstOrDefaultAsync(m => m.DepartmentID == id);

            if (Department == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
