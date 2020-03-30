using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tutorial4.Models;

namespace Tutorial4.Services
{
    public interface IStudentsDb
    {
        IEnumerable<Student> GetStudents();

        IEnumerable<string> GetSemester(string id);
    }
}
