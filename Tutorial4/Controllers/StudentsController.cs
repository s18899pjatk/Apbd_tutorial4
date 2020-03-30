using Microsoft.AspNetCore.Mvc;
using Tutorial4.Services;

namespace Tutorial4.Controllers
{
    [ApiController]
    [Route("api/students")]
    public class StudentsController : ControllerBase
    {
        private IStudentsDb _dbService;

        public StudentsController(IStudentsDb db)
        {
            _dbService = db;
        }

        [HttpGet]
        public IActionResult GetStudents(string orderBy)
        {
            return Ok(_dbService.GetStudents());
        }

        [HttpGet("entries/{id}")]
        public IActionResult GetSemester(string id)
        {
            if (_dbService.GetSemester(id) != null)
            {
                return Ok(_dbService.GetSemester(id));
            }
            else return NotFound("Record has not been found");
        }       
        



        //[HttpPost]
        //public IActionResult CreateStudent(Student student)
        //{
        //    student.IndexNumber = $"s{new Random().Next(1, 20000)}";
        //    // _dbService.GetStudents().ToList().Add(student);
        //    return Ok(student);
        //}

        //[HttpPut("{id}")]
        //public IActionResult UpdateStudent(Student student, int id)
        //{
        //    if(student.idStudent != id)
        //    {
        //         return NotFound("Student Not Found");
        //    }
        //    // updating object 
        //    // student.FirstName = "James";
        //    // _dbService.GetStudents().ToList().Insert(id, student);
        //    return Ok("Update completed");
        //}

        //[HttpDelete("{id}")]
        //public IActionResult DeleteStudent(int id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound("Student Not Found");
        //    }
        //    // deleting object 
        //    //_dbService.GetStudents().ToList().RemoveAt(id);
        //    return Ok("Delete completed");
        //}
    }
}
