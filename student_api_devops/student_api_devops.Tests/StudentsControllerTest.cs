using Microsoft.AspNetCore.Mvc;
using student_api_devops.Controllers;
using student_api_devops.Models;
using System;
using System.Collections.Generic;
using Xunit;

namespace student_api_devops.Tests
{
    public class StudentsControllerTests
    {
        [Fact]
        public void GetStudents_Returns_OkResult_With_Data()
        {
            // Arrange
            var controller = new StudentsController();
            var students = new List<Student>
            {
                new Student { Id = 1, Name = "John Doe", Age = 20 },
                new Student { Id = 2, Name = "Jane Smith", Age = 22 }
            };

            // Act
            var result = controller.GetStudents();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var returnedStudents = Assert.IsAssignableFrom<IEnumerable<Student>>(okResult.Value);
            Assert.NotEmpty(returnedStudents);
        }

        [Fact]
        public void GetStudent_With_Valid_Id_Returns_Student()
        {
            // Arrange
            var controller = new StudentsController();
            var students = new List<Student>
            {
                new Student { Id = 1, Name = "John Doe", Age = 20 },
                new Student { Id = 2, Name = "Jane Smith", Age = 22 }
            };
            var validId = 1;

            // Act
            var result = controller.GetStudent(validId);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var student = Assert.IsType<Student>(okResult.Value);
            Assert.Equal(validId, student.Id);
        }

        [Fact]
        public void GetStudent_With_Invalid_Id_Returns_NotFound()
        {
            // Arrange
            var controller = new StudentsController();
            var students = new List<Student>
            {
                new Student { Id = 1, Name = "John Doe", Age = 20 },
                new Student { Id = 2, Name = "Jane Smith", Age = 22 }
            };
            var invalidId = 99;

            // Act
            var result = controller.GetStudent(invalidId);

            // Assert
            Assert.IsType<NotFoundResult>(result.Result);
        }

        // Add similar test cases for CreateStudent, UpdateStudent, and DeleteStudent methods
    }
}
