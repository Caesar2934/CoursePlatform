using CoursePlatform.Core.Models;
using CoursePlatform.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoursePlatform.Service
{
    public interface ICourseCategoryService
    {
        Task<CourseCategoryModel?> GetByIdAsync(int id);
        Task<List<CourseCategoryModel>> GetCourseCategories();
        Task<List<CourseCategoryModel>> GetCourseCategoriesAsync();
    }

    public class CourseCategoryService : ICourseCategoryService
    {
        private readonly ICourseCategoryRepository categoryRepository;

        public CourseCategoryService(ICourseCategoryRepository categoryRepository)
        {
          this.categoryRepository = categoryRepository;
        }

        public async Task<CourseCategoryModel?> GetByIdAsync(int id)
        {
            var data = await categoryRepository.GetByIdAsync(id);
            if (data == null)
            {
                return null;
            }

            return data == null? null : new CourseCategoryModel()
            {
                CategoryId = data.CategoryId,
                CategoryName = data.CategoryName,
                Description = data.Description
            };
       
        }

        public async Task<List<CourseCategoryModel>> GetCourseCategories()
        {
            return await GetCourseCategoriesAsync();
        }

        public async Task<List<CourseCategoryModel>> GetCourseCategoriesAsync()
        {
            var data = await categoryRepository.GetCourseCategoriesAsync();
            var modelData = data.Select(x => new CourseCategoryModel()
            {
                CategoryId = x.CategoryId,
                CategoryName = x.CategoryName,
                Description = x.Description
            }).ToList();
            
            return modelData;
        }
    }
}
