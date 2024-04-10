using StudentManagementApi.Dto;
using StudentManagementApi.Models;

namespace StudentManagementApi.Mappings
{
    public static class StudentMappings
    {
        public static Student MapDtoToStudentClass(this AddStudentDto addStudentDto)
        {
            Student student = new Student()
            {
                FirstName = addStudentDto.FirstName,
                LastName = addStudentDto.LastName,
                OtherNames = addStudentDto.OtherNames,
                Email = addStudentDto.Email,
                DateOfBirth = addStudentDto.DateOfBirth,
                PhoneNumber = addStudentDto.PhoneNumber,
                Gender = addStudentDto.Gender,
                Address = addStudentDto.Address,
            };

            return student;
        }

        public static void MapUpdatDtoToStudentClass(this Student updateStudent, AddStudentDto addStudentDto)
        {
            updateStudent.FirstName = addStudentDto.FirstName;
            updateStudent.LastName = addStudentDto.LastName;
            updateStudent.OtherNames = addStudentDto.OtherNames;
            updateStudent.Email = addStudentDto.Email;
            updateStudent.DateOfBirth = addStudentDto.DateOfBirth;
            updateStudent.PhoneNumber = addStudentDto.PhoneNumber;
            updateStudent.Gender = addStudentDto.Gender;
            updateStudent.Address = addStudentDto.Address;
        }
    }
}