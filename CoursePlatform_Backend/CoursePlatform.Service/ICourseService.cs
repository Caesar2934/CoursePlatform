using CoursePlatform.Core.Models;
using CoursePlatform.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoursePlatform.Service
{
    public interface ICourseService
    {
        Task<List<CourseModel>> GetAllCoursesAsync(int? category = null);
        Task<CourseDetaiModel>  GetCourseDetailsAsync(int courseId);
    }
    public class CourseService : ICourseService
    {
        private readonly ICourseRepository courseRepository;
        public CourseService(ICourseRepository courseRepository)
        {
            this.courseRepository = courseRepository;
        }
        public async Task<List<CourseModel>> GetAllCoursesAsync(int? category = null)
        {
            return await courseRepository.GetCoursesAsync(category);
        }
        public async Task<CourseDetaiModel> GetCourseDetailsAsync(int courseId)
        {
            return await courseRepository.GetCourseDetailsAsync(courseId);
        }
    }
}
