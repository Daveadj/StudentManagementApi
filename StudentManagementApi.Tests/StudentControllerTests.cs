using AutoFixture;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using StudentManagementApi.Contracts;
using StudentManagementApi.Controllers;
using StudentManagementApi.Dto;
using StudentManagementApi.Models;
using StudentManagementApi.RequestFeatures;

namespace StudentManagementApi.Tests
{
    public class StudentControllerTests
    {
        private StudentsController _studentsController;
        private Fixture _fixture;
        private Mock<IStudentRepository> _mockRepository = new Mock<IStudentRepository>();
        private Mock<ILogger<StudentsController>> _mockLogger = new Mock<ILogger<StudentsController>>();

        [SetUp]
        public void Setup()
        {
            _fixture = new Fixture();
            _studentsController = new StudentsController(_mockRepository.Object, _mockLogger.Object);
        }

        [Test]
        public async Task GetAllStudents_Returns_Ok()
        {
            ICollection<Student>? students = _fixture.CreateMany<Student>(4).ToList();
            _mockRepository.Setup(repo => repo.GetAllStudents(new StudentParameters())).ReturnsAsync(students);

            var result = await _studentsController.GetAllStudents(new StudentParameters());
            var obj = result as ObjectResult;
            Assert.AreEqual(200, obj.StatusCode);
        }

        [Test]
        public async Task Post_Student_Returning_created_at_route()
        {
            var student = _fixture.Create<AddStudentDto>();
            _mockRepository.Setup(repo => repo.CreateStudent(It.IsAny<Student>()));

            var results = await _studentsController.AddStudent(student);
            var obj = results as CreatedAtRouteResult;
            Assert.AreEqual(201, obj.StatusCode);
        }

        [Test]
        public async Task Put_Student_Returning_Ok()
        {
            var studentDto = _fixture.Create<AddStudentDto>();
            var student = _fixture.Create<Student>();
            _mockRepository.Setup(repo => repo.GetStudentById(It.IsAny<int>())).ReturnsAsync(student);
            _mockRepository.Setup(repo => repo.UpdateStudent(It.IsAny<Student>()));

            var result = await _studentsController.UpdateStudentDetails(student.Id, studentDto);
            var obj = result as ObjectResult;
            Assert.AreEqual(200, obj.StatusCode);
        }

        [Test]
        public async Task Get_Student_By_Id_Return_Ok()
        {
            var student = _fixture.Create<Student>();

            _mockRepository.Setup(repo => repo.GetStudentById(It.IsAny<int>())).ReturnsAsync(student);

            var result = await _studentsController.GetStudentById(student.Id);
            var obj = result as ObjectResult;
            Assert.AreEqual(200, obj.StatusCode);
        }

        [Test]
        public async Task Delete_Student_Return_Ok()
        {
            var student = _fixture.Create<Student>();
            _mockRepository.Setup(repo => repo.GetStudentById(It.IsAny<int>())).ReturnsAsync(student);
            _mockRepository.Setup(repo => repo.DeleteStudent(It.IsAny<int>()));

            var result = await _studentsController.RemoveStudent(student.Id);
            var obj = result as ObjectResult;
            Assert.AreEqual(200, obj.StatusCode);
        }
    }
}