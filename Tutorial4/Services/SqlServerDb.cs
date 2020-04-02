
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
            using var sqlConnection = new SqlConnection(ConnString);
            using var command = new SqlCommand();
            command.Connection = sqlConnection;
            command.CommandText = "select s.IndexNumber, s.FirstName, s.LastName, s.BirthDate, st.Name as Studies, e.Semester " +
                "from Student s " +
                "join Enrollment e on e.IdEnrollment = s.IdEnrollment " +
                "join Studies st on st.IdStudy = e.IdStudy; ";
            sqlConnection.Open();
            SqlDataReader response = command.ExecuteReader();
            while (response.Read())
            {
                var st = new Student
                {
                    IndexNumber = response["IndexNumber"].ToString(),
                    FirstName = response["FirstName"].ToString(),
                    LastName = response["LastName"].ToString(),
                    Studies = response["Studies"].ToString(),
                    BirthDate = DateTime.Parse(response["BirthDate"].ToString()),
                    Semester = int.Parse(response["Semester"].ToString())

                };

                students.Add(st);
            }

            return students;
        }

        public IEnumerable<string> GetSemester(string id)
        {
            using var sqlConnection = new SqlConnection("Data Source=db-mssql;Initial Catalog=s18899;Integrated Security=True");
            using var command = new SqlCommand();
            command.Connection = sqlConnection;
            command.CommandText = "select e.Semester " +
                "from Student s " +
                "join Enrollment e on e.IdEnrollment = s.IdEnrollment " +
                "where IndexNumber like @index;";
            SqlParameter par = new SqlParameter();
            par.ParameterName = "index";
            par.Value = id;
            command.Parameters.Add(par);
            sqlConnection.Open();
            SqlDataReader response = command.ExecuteReader();
            var entriesList = new List<string>();
            while (response.Read())
                entriesList.Add(response["Semester"].ToString());

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
