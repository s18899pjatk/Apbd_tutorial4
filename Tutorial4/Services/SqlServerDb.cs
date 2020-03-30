
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using Tutorial4.Models;

namespace Tutorial4.Services
{
    public class SqlServerDb : IStudentsDb
    {
        private string ConnString = "Data Source=db-mssql;Initial Catalog=s18899;Integrated Security=True";

        public IEnumerable<Student> GetStudents()
        {
            var students = new List<Student>();
            using (var sqlConnection = new SqlConnection(ConnString))
            using (var command = new SqlCommand())
            {
                    command.Connection = sqlConnection;
                    command.CommandText = "select s.IndexNumber, s.FirstName, s.LastName, s.BirthDate, st.Name as Studies, e.Semester " +
                        "from Student s " +
                        "join Enrollment e on e.IdEnrollment = s.IdEnrollment " +
                        "join Studies st on st.IdStudy = e.IdStudy; ";
                    sqlConnection.Open();
                    SqlDataReader responce = command.ExecuteReader();
                    while (responce.Read())
                    {
                        var st = new Student
                        {
                            IndexNumber = responce["IndexNumber"].ToString(),
                            FirstName = responce["FirstName"].ToString(),
                            LastName = responce["LastName"].ToString(),
                            Studies = responce["Studies"].ToString(),
                            BirthDate = DateTime.Parse(responce["BirthDate"].ToString()),
                            Semester = int.Parse(responce["Semester"].ToString())

                        };

                        students.Add(st);
                    }
            }
           
          return students;
        }

        public IEnumerable<string> GetSemester(string id)
        {
            var entriesList = new List<string>();
            using (var sqlConnection = new SqlConnection("Data Source=db-mssql;Initial Catalog=s18899;Integrated Security=True"))
            using (var command = new SqlCommand())
                {
                    command.Connection = sqlConnection;
                    command.CommandText = "select e.Semester " +
                        "from Student s " +
                        "join Enrollment e on e.IdEnrollment = s.IdEnrollment " +
                        "where IndexNumber like @index;";
                    // using parameter we cannot use injection
                    // id = "ss';DROP TABLE Student;--";
                    SqlParameter par1 = new SqlParameter();
                    par1.ParameterName = "index";
                    par1.Value = id;
                    command.Parameters.Add(par1);
                    //command.Parameters.AddWithValue("index", id);
                    sqlConnection.Open();
                    SqlDataReader responce = command.ExecuteReader();
                    while (responce.Read())
                        entriesList.Add(responce["Semester"].ToString());
            }
            
            if (entriesList.Count > 0)
            {
                return entriesList;
            }
            else
            {
               return null;
            }
        }
    }
}
