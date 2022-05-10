using BlazorProject.Shared;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorProject.Server.Models
{
    public class DepartmentRepository : IDepartmentRepository
    {
        private readonly AppDbContext appDbContext;

        public DepartmentRepository(AppDbContext appDbContext)
        {
            this.appDbContext = appDbContext;
        }

        public async Task<Course> GetDepartment(int departmentId)
        {
            return await appDbContext.Courses
                .FirstOrDefaultAsync(d => d.CourseId == departmentId);
        }

        public async Task<IEnumerable<Course>> GetDepartments()
        {
            return await appDbContext.Courses.ToListAsync();
        }
    }
}
