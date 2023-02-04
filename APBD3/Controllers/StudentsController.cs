using Microsoft.AspNetCore.Mvc;
using static APBD3.Services.Services;
using APBD3.Models;
using System.Text.Json;
namespace APBD3.Controllers
{
    [Route("api/students")]
    [ApiController]
    public class StudentsController : ControllerBase
    {
        [HttpGet("{indexNumber}")]
        public IActionResult GetStudent(string indexNumber)
        {
            string result = getStudentJson(indexNumber);
            if (result == null)
            {
                return NotFound("Didn't find student with index number: " + indexNumber);
            }
            else
            {
                return Ok(result);
            }
        }
        [HttpGet]
        public IActionResult GetStudents()
        {
            return Ok(getStudentsJson());
        }

        
        [HttpPut("{indexNumber}")]
        public IActionResult UpdateRecord(Student updatedStudent, string indexNumber)
        {
            if(updatedStudent.index != indexNumber || updatedStudent == null) {
                return BadRequest("Invalid data passed for updating student");
            }
            else
            {
                if(updateStudent(indexNumber, updatedStudent))
                {
                    return Ok(JsonSerializer.Serialize(updatedStudent));
                }
                else
                {
                    return BadRequest("Error encountered while attempting to update student " + indexNumber);
                }
            }
        }

        [HttpPost]
        public IActionResult AddStudent(Student newStudent) {

            if(studentHasEmptyFields(newStudent)){
                return BadRequest("All of the new student's properties must be filled in!");
            }
            if (!addNewStudent(newStudent))
            {
                return BadRequest("New student's index number is invalid or duplicate!");
            }
            return Ok("Added new student to database");
            
        }

        [HttpDelete("{indexNumber}")]
        public IActionResult DeleteStudent(string indexNumber)
        {
            if (deleteStudent(indexNumber))
            {
                return Ok("Deleted student with index number: " + indexNumber);
            }
            else
            {
                return BadRequest("Index number not valid, student NOT deleted.");
            }
        }

    }
}
