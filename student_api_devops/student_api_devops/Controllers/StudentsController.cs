using Microsoft.AspNetCore.Mvc;
using student_api_devops.Models;
using System.Text.Json;

namespace student_api_devops.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentsController : ControllerBase
    {
        private readonly string _jsonFilePath = Path.Combine(Directory.GetCurrentDirectory(), "Data", "students.json");


        [HttpGet]
        public ActionResult<IEnumerable<Student>> GetStudents()
        {
            string jsonData = System.IO.File.ReadAllText(_jsonFilePath);
            var students = JsonSerializer.Deserialize<List<Student>>(jsonData);
            return Ok(students);
        }

        [HttpGet("{id}")]
        public ActionResult<Student> GetStudent(int id)
        {
            string jsonData = System.IO.File.ReadAllText(_jsonFilePath);
            var students = JsonSerializer.Deserialize<List<Student>>(jsonData);
            var student = students.Find(s => s.Id == id);

            if (student == null)
            {
                return NotFound();
            }

            return Ok(student);
        }

        [HttpPost]
        public async Task<ActionResult<Student>> CreateStudent(Student student)
        {
            string jsonData = System.IO.File.ReadAllText(_jsonFilePath);
            var students = JsonSerializer.Deserialize<List<Student>>(jsonData);

            // Generate unique ID for the new student
            int maxId = students.Count > 0 ? students.Max(s => s.Id) : 0;
            student.Id = maxId + 1;

            students.Add(student);
            string updatedJsonData = JsonSerializer.Serialize(students);
            System.IO.File.WriteAllText(_jsonFilePath, updatedJsonData);

            return CreatedAtAction(nameof(GetStudent), new { id = student.Id }, student);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateStudent(int id, Student student)
        {
            string jsonData = System.IO.File.ReadAllText(_jsonFilePath);
            var students = JsonSerializer.Deserialize<List<Student>>(jsonData);
            var existingStudentIndex = students.FindIndex(s => s.Id == id);

            if (existingStudentIndex == -1)
            {
                return NotFound();
            }

            student.Id = id;
            students[existingStudentIndex] = student;

            string updatedJsonData = JsonSerializer.Serialize(students);
            System.IO.File.WriteAllText(_jsonFilePath, updatedJsonData);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteStudent(int id)
        {
            string jsonData = System.IO.File.ReadAllText(_jsonFilePath);
            var students = JsonSerializer.Deserialize<List<Student>>(jsonData);
            var existingStudent = students.Find(s => s.Id == id);

            if (existingStudent == null)
            {
                return NotFound();
            }

            students.Remove(existingStudent);
            string updatedJsonData = JsonSerializer.Serialize(students);
            System.IO.File.WriteAllText(_jsonFilePath, updatedJsonData);

            return NoContent();
        }
    }
}
