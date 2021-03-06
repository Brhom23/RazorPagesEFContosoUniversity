using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using RazorPagesEFContosoUniversity.Data;
using RazorPagesEFContosoUniversity.Models;

namespace RazorPagesEFContosoUniversity.Pages.Courses
{
    public class IndexModel : PageModel
    {
        private readonly RazorPagesEFContosoUniversity.Data.MyContext _context;

        public IndexModel(RazorPagesEFContosoUniversity.Data.MyContext context)
        {
            _context = context;
        }


        public IList<Course> Courses { get;set; }

        public async Task OnGetAsync()
        {

            Courses = await _context.Courses
                .Include(c => c.Department)
                .AsNoTracking()
                .ToListAsync();

            //Courses = await _context.Courses
            //    .Include(c => c.Department).ToListAsync();
        }
    }
}
