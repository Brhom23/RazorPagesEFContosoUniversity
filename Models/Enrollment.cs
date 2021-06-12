using System.ComponentModel.DataAnnotations;

namespace RazorPagesEFContosoUniversity.Models
{
    public enum Grade
    {
        A, B, C, D, F
    }

    public class Enrollment
    {
        //الخاصية
        //EnrollmentID
        //هي المفتاح الأساسي؛ يستخدم هذا الكيان نمط
        //classnameID
        //بدلاً من ID
        //يختار العديد من المطورين نمطًا واحدًا ويستخدمونه باستمرار.
        //يستخدم هذا البرنامج التعليمي كلاهما فقط لتوضيح أن كلاهما يعمل.
        //يؤدي استخدام المعرف بدون اسم الفئة
        //إلى تسهيل تنفيذ بعض أنواع تغييرات نموذج البيانات. 
        public int EnrollmentID { get; set; }
        public int CourseID { get; set; }
        public int StudentID { get; set; }
        [DisplayFormat(NullDisplayText = "No grade")]
        public Grade? Grade { get; set; }

        public Course Course { get; set; }
        public Student Student { get; set; }
    }
}