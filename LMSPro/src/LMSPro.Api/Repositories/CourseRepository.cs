using LMSPro.Api.Data;
using LMSPro.Api.Entities;
using Microsoft.EntityFrameworkCore;

namespace LMSPro.Api.Repositories;

public class CourseRepository : ICourseRepository
{
    private readonly AppDbContext DbContext;

    public CourseRepository(AppDbContext dbContext)
    {
        DbContext = dbContext;
    }

    public async Task AddAsync(Course course)
    {
        await DbContext.Courses.AddAsync(course);
    }

    public void Delete(Course course)
    {
        //DbContext.Courses.Remove(course);
        if(DbContext.Entry(course).State != EntityState.Deleted)
        {
            DbContext.Entry(course).State = EntityState.Deleted;
        }
        
    }

    public async Task<IEnumerable<Course>> GetAllAsync()
    {
        return await DbContext.Courses.ToListAsync();
    }

    public IQueryable<Course> GetAllQuery()
    {
        return DbContext.Courses;
    }

    //public async Task<Course?> GetByIdAsync(long courseId)
    //{
    //    var course = await DbContext.Courses
    //        .FirstOrDefaultAsync(x => x.CourseId == courseId);

    //    return course;
    //}

    public async Task SaveChangesAsync()
    {
        await DbContext.SaveChangesAsync();
    }

    public void Update(Course course)
    {
        DbContext.Courses.Update(course);
    }
}
