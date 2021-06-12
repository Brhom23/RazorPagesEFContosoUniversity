using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using RazorPagesEFContosoUniversity.Data;
using RazorPagesEFContosoUniversity.Models;

namespace RazorPagesEFContosoUniversity.Pages.Students
{
    public class IndexModel : PageModel
    {
        private readonly MyContext _context;
        private readonly MvcOptions _mvcOptions; //غير مستخدمة الآن
        private readonly IConfiguration _Configuration;
        public IndexModel(MyContext context, IOptions<MvcOptions> mvcOptions, IConfiguration configuration)
        {
            _context = context;
            _mvcOptions = mvcOptions.Value;
            _Configuration = configuration;
        }
        //public IList<Student> Students { get; set; }
        //Add paging 
        public PaginatedList<Student> Students { get; set; }

        //To add sorting and filter
        public string NameSort { get; set; }
        public string DateSort { get; set; }
        public string CurrentFilter { get; set; }
        public string CurrentSort { get; set; }



        public async Task OnGetAsync(string sortOrder, string currentFilter, string searchString, int? pageIndex)
        {
            ////Students = await _context.Students.ToListAsync();
            ////لعرض عدد محدد من السجلات في كل مرة وهذا العدد 
            ////يأتي من الخيارات المجهزة في MaxModelBindingCollectionSize
            //Students = await _context.Students.Take(
            //_mvcOptions.MaxModelBindingCollectionSize).ToListAsync();


            //Add paging
            if (searchString != null)
            {
                pageIndex = 1;
            }
            else
            {
                searchString = currentFilter;
            }


            //Add sorting and filter
            IQueryable<Student> studentsIQ = from s in _context.Students
                                             select s;


            //Add filter إضافة تصفية البيانات
            CurrentFilter = searchString;




            if (!String.IsNullOrEmpty(searchString))
            {
                studentsIQ = studentsIQ.Where(s => s.LastName.Contains(searchString)
                                       || s.FirstMidName.Contains(searchString));
            }

            //Add sorting لجعل الجدول يقوم بعمل الترتيب حسب العمود
            // using System;
            NameSort = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            DateSort = sortOrder == "Date" ? "date_desc" : "Date";



            switch (sortOrder)
            {
                case "name_desc":
                    studentsIQ = studentsIQ.OrderByDescending(s => s.LastName);
                    break;
                case "Date":
                    studentsIQ = studentsIQ.OrderBy(s => s.EnrollmentDate);
                    break;
                case "date_desc":
                    studentsIQ = studentsIQ.OrderByDescending(s => s.EnrollmentDate);
                    break;
                default:
                    studentsIQ = studentsIQ.OrderBy(s => s.LastName);
                    break;
            }

            //Students = await studentsIQ.AsNoTracking().ToListAsync();
            var pageSize = _Configuration.GetValue("PageSize", 4);
            Students = await PaginatedList<Student>.CreateAsync(
                studentsIQ.AsNoTracking(), pageIndex ?? 1, pageSize);

        }
    }
}
