using CoursePlatform.Core.Models;
using CoursePlatform.Service;
using Microsoft.AspNetCore.Mvc;

namespace CoursePlatform.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CourseController : ControllerBase
    {
        private readonly ICourseService courseService;
        public CourseController(ICourseService courseService)
        {
            this.courseService = courseService;
        }

        [HttpGet]
        public async Task<ActionResult<List<CourseModel>>> GetAllCourses()
        {
            var courses = await courseService.GetAllCoursesAsync();
            return Ok(courses);
        }

        [HttpGet("Category/{categoryId}")]
         public async Task<ActionResult<List<CourseModel>>> GetAllCourseByCategoryAsync([FromRoute] int categoryId)
        {
            var courses = await courseService.GetAllCoursesAsync(categoryId);
            return Ok(courses);
        }

        [HttpGet("Details/{courseId}")]
        public async Task<ActionResult<CourseDetaiModel>> GetCourseDetailsAsync(int courseId)
        {
            var courseDetails = await courseService.GetCourseDetailsAsync(courseId);
            if (courseDetails == null)
            {
                return NotFound();
            }
            return Ok(courseDetails);
        }


    }
}
