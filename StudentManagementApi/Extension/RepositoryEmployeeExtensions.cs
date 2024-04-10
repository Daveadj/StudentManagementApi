using StudentManagementApi.Models;

namespace StudentManagementApi.Extension
{
    public static class RepositoryEmployeeExtensions
    {
        public static IQueryable<Student> Search(this IQueryable<Student> students, string searchTerm)
        {
            if (string.IsNullOrWhiteSpace(searchTerm))
            {
                return students;
            }
            var lowerCaseTerm = searchTerm.Trim().ToLower();

            return students.Where(s =>
            s.FirstName.ToLower().Contains(searchTerm) ||
            s.LastName.ToLower().Contains(searchTerm) ||
            s.Email.ToLower().Contains(searchTerm) ||
            s.Address.ToLower().Contains(searchTerm) ||
            s.Gender.ToLower().Contains(searchTerm)
        );
        }
    }
}