using LMSPro.Api.Entities;

namespace LMSPro.Api.Repositories;

public interface ICourseRepository
{
    IQueryable<Course> GetAllQuery();
    Task<IEnumerable<Course>> GetAllAsync();
    //Task<Course?> GetByIdAsync(long courseId);
    Task AddAsync(Course course);
    void Update(Course course);
    void Delete(Course course);
    Task SaveChangesAsync();
}