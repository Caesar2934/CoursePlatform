using CoursePlatform.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoursePlatform.Data
{
    public interface ICourseRepository
    {
        Task<List<CourseModel>> GetCoursesAsync(int? categoryID = null);
        Task<CourseDetaiModel> GetCourseDetailsAsync(int courseId);
    }
}
