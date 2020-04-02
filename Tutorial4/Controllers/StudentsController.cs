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
            var resp = _dbService.GetSemester(id);
            if (resp != null)
            {
                return Ok(resp);
            }
            else return NotFound("Record has not been found");
        }
    }
}
