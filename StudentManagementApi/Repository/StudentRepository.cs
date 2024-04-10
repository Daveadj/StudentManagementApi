using Microsoft.EntityFrameworkCore;
using StudentManagementApi.Contracts;
using StudentManagementApi.Extension;
using StudentManagementApi.Models;
using StudentManagementApi.RequestFeatures;

namespace StudentManagementApi.Repository
{
    public class StudentRepository : IStudentRepository
    {
        private readonly AppDbcontext _dbcontext;

        public StudentRepository(AppDbcontext dbcontext)
        {
            _dbcontext = dbcontext;
        }

        public async Task CreateStudent(Student student)
        {
            await _dbcontext.Students.AddAsync(student);
            await _dbcontext.SaveChangesAsync();
        }

        public async Task DeleteStudent(int id)
        {
            var student = await _dbcontext.Students.FirstOrDefaultAsync(x => x.Id == id);
            _dbcontext.Students.Remove(student);
            await _dbcontext.SaveChangesAsync();
        }

        public async Task<ICollection<Student>> GetAllStudents(StudentParameters studentParameters)
        {
            return await _dbcontext.Students
                .Search(studentParameters.SearchTerm)
                .OrderBy(e => e.FirstName)
                .Skip((studentParameters.PageNumber - 1) * studentParameters.PageSize)
                .Take(studentParameters.PageSize)
                .ToListAsync();
        }

        public async Task<Student> GetStudentById(int id)
        {
            var student = await _dbcontext.Students.FirstOrDefaultAsync(x => x.Id == id);
            return student;
        }

        public async Task UpdateStudent(Student student)
        {
            _dbcontext.Students.Update(student);
            await _dbcontext.SaveChangesAsync();
        }
    }
}