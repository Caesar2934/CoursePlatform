using CoursePlatform.Core.Entities;
using CoursePlatform.Core.Models;
using Microsoft.EntityFrameworkCore;

namespace CoursePlatform.Data
{
    public class CourseRepository : ICourseRepository
    {
        private readonly OnlineCourseDbContext dbContext;
        public CourseRepository(OnlineCourseDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<List<CourseModel>> GetCoursesAsync(int? categoryID = null)
        {
            var query = dbContext.Courses
                .Include(c => c.Category)
                .AsQueryable();

            if (categoryID.HasValue)
            {
                query = query.Where(c => c.CategoryId == categoryID.Value);
            }

            var courses = await query.Select(s => new CourseModel()
            {
                CourseId = s.CourseId,
                Title = s.Title,
                Description = s.Description,
                Price = s.Price,
                CourseType = s.CourseType,
                SeatsAvailable = s.SeatsAvailable ?? 0,
                Duration = s.Duration,
                categoryId = s.CategoryId,
                InstructorId = s.InstructorId,
                StartDate = s.StartDate,
                EndDate = s.EndDate,
                Category = new CourseCategoryModel()
                {
                    CategoryId = s.Category.CategoryId,
                    CategoryName = s.Category.CategoryName,
                    Description = s.Category.Description
                },
                UserRating = new UserRatingModel
                {
                    CourseId = s.CourseId,
                    AverageRating = s.Reviews.Any() ? Convert.ToDecimal(s.Reviews.Average(r => r.Rating)) : 0,
                    TotalRatings = s.Reviews.Count()
                }
            }).ToListAsync();
            return courses;
        }

        public async Task<CourseDetaiModel> GetCourseDetailsAsync(int courseId)
        {
            var course = await dbContext.Courses
                .Include(c => c.Category)
                .Include(c => c.Reviews)
                .Include(c => c.SessionDetails)
                .Where(c => c.CourseId == courseId)
                .Select(c => new CourseDetaiModel()
                {
                    CourseId = c.CourseId,
                    Title = c.Title,
                    Description = c.Description,
                    Price = c.Price,
                    CourseType = c.CourseType,
                    SeatsAvailable = c.SeatsAvailable ?? 0,
                    Duration = c.Duration,
                    categoryId = c.CategoryId,
                    InstructorId = c.InstructorId,
                    StartDate = c.StartDate,
                    EndDate = c.EndDate,
                    Category = new CourseCategoryModel()
                    {
                        CategoryId = c.Category.CategoryId,
                        CategoryName = c.Category.CategoryName,
                        Description = c.Category.Description
                    },
                    Reviews = c.Reviews.Select(r=> new UserReviewModel()
                    {
                        CourseId = r.CourseId,
                        UserName = r.User.DisplayName,
                        Rating = r.Rating,
                        Comment = r.Comments,
                        ReviewDate = r.ReviewDate
                    }).OrderByDescending(o=>o.Rating).Take(10).ToList(),

                    SessionsDetails = c.SessionDetails.Select(sd => new SessionDetailModel()
                    {
                        SessionId = sd.SessionId,
                        CourseId = sd.CourseId,
                        Title = sd.Title,
                        Description = sd.Description,
                        VideoUrl = sd.VideoUrl,
                        VideoOrder = sd.VideoOrder
                    }).OrderBy(o => o.VideoOrder).ToList(),

                    UserRating = new UserRatingModel
                    {
                        CourseId = c.CourseId,
                        AverageRating = c.Reviews.Any() ? Convert.ToDecimal(c.Reviews.Average(r => r.Rating)) : 0,
                        TotalRatings = c.Reviews.Count()
                    }
                }).FirstOrDefaultAsync(); 
            return course ?? throw new KeyNotFoundException($"Course with id {courseId} was not found.");
        }
    }
}
