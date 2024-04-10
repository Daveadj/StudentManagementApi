using StudentManagementApi.Models;
using StudentManagementApi.RequestFeatures;

namespace StudentManagementApi.Contracts
{
    public interface IStudentRepository
    {
        Task<ICollection<Student>> GetAllStudents(StudentParameters studentParameters);

        Task<Student> GetStudentById(int id);

        Task CreateStudent(Student student);

        Task UpdateStudent(Student student);

        Task DeleteStudent(int id);
    }
}