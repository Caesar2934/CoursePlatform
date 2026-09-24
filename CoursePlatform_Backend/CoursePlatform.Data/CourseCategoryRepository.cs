using CoursePlatform.Core.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoursePlatform.Data
{
    /*
    public class CourseCategoryRepository(OnlineCourseDbContext dbContext) : ICourseCategoryRepository
    {
        private readonly OnlineCourseDbContext dbContext = dbContext;
        public CourseCategory? GetById(int id)
        {
            var data = dbContext.CourseCategories.Find(id);
            return data; 
        }

        public List<CourseCategory> GetCourseCategories()
        {
            var data = dbContext.CourseCategories.ToList();
            return data;
        }
    }
    */

    public class CourseCategoryRepository : ICourseCategoryRepository
    {
        private readonly OnlineCourseDbContext dbContext;

        public CourseCategoryRepository(OnlineCourseDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public Task GetById(int id)
        {
            throw new NotImplementedException();
        }

        public Task<CourseCategory?> GetByIdAsync(int id)
        {
            var data = dbContext.CourseCategories.FindAsync(id).AsTask();
            return data;
        }
        public Task<List<CourseCategory>> GetCourseCategoriesAsync()
        {
            var data = dbContext.CourseCategories.ToListAsync();
            return data;
        }
    }
}
