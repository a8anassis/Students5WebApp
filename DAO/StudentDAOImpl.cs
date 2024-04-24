using Microsoft.AspNetCore.Identity;
using System.Data.SqlClient;
using WebStarterDBApp.Models;
using WebStarterDBApp.Services.DBHelper;

namespace WebStarterDBApp.DAO
{
    public class StudentDAOImpl : IStudentDAO
    {
        public void Delete(int id)
        {
            string sql = "DELETE FROM STUDENTS WHERE ID = @id";

            using SqlConnection? conn = DBHelper.GetConnection();
            if (conn is not null) conn.Open();

            using SqlCommand command = new(sql, conn);
            command.Parameters.AddWithValue("@id", id);
            command.ExecuteNonQuery();
        }

        public IList<Student> GetAll()
        {
            string sql = "SELECT * FROM STUDENTS";
            var students = new List<Student>();
            Student? student;

            using SqlConnection? conn = DBHelper.GetConnection();
            if (conn is not null) conn.Open();

            using SqlCommand command = new(sql, conn);
            using SqlDataReader reader = command.ExecuteReader();   

            while (reader.Read())
            {
                student = new()
                {
                    Id = reader.GetInt32(reader.GetOrdinal("ID")),
                    Firstname = reader.GetString(reader.GetOrdinal("FIRSTNAME")),
                    Lastname = reader.GetString(reader.GetOrdinal("LASTNAME"))
                };
                students.Add(student);
            }
            return students;
        }

        public Student? GetByID(int id)
        {
            string? sql = "SELECT * FROM STUDENTS WHERE ID = @id";
            Student? student = null;

            using SqlConnection? conn = DBHelper.GetConnection();
            if (conn is not null) conn.Open();

            using SqlCommand command = new(sql, conn);
            command.Parameters.AddWithValue("@id", id);

            using SqlDataReader reader = command.ExecuteReader();

            if (reader.Read())
            {
                student = new()
                {
                    Id = reader.GetInt32(reader.GetOrdinal("ID")),
                    Firstname = reader.GetString(reader.GetOrdinal("FIRSTNAME")),
                    Lastname = reader.GetString(reader.GetOrdinal("LASTNAME"))
                };
            }
            return student;
        }

        public Student? Insert(Student? student)
        {
            if (student == null) return null;
            string sql = "INSERT INTO STUDENTS (FIRSTNAME, LASTNAME) VALUES (@firstname, @lastname); " +
                "SELECT SCOPE_IDENTITY();";

            Student? studentToReturn = null;
            int insertedId = 0;

            using SqlConnection? conn = DBHelper.GetConnection();
            if (conn is not null) conn.Open();

            using SqlCommand command = new(sql, conn);
            command.Parameters.AddWithValue("@firstname", student.Firstname);
            command.Parameters.AddWithValue("@lastname", student.Lastname);

            object insertedObj = command.ExecuteScalar();
            if (insertedObj is not null)
            {
                if (!int.TryParse(insertedObj.ToString(), out insertedId))
                {
                    throw new Exception("Error in insert id");
                }
            }

            string? sqlSelect = "SELECT * FROM STUDENTS WHERE ID = @id";

            using SqlCommand sqlCommand = new(sqlSelect, conn);
            sqlCommand.Parameters.AddWithValue("@id", insertedId);

            using SqlDataReader reader = sqlCommand.ExecuteReader();

            if (reader.Read())
            {
                studentToReturn = new()
                {
                    Id = reader.GetInt32(reader.GetOrdinal("ID")),
                    Firstname = reader.GetString(reader.GetOrdinal("FIRSTNAME")),
                    Lastname = reader.GetString(reader.GetOrdinal("LASTNAME"))
                };
            }
            return studentToReturn;
        }

        public Student? Update(Student? student)
        {
            if (student == null) return null;
            string sql = "UPDATE STUDENTS SET FIRSTNAME = @firstname, LASTNAME = @lastname WHERE ID = @id";

            Student? studentToReturn = student;
            
            using SqlConnection? conn = DBHelper.GetConnection();
            if (conn is not null) conn.Open();

            using SqlCommand command = new(sql, conn);
            command.Parameters.AddWithValue("@firstname", student.Firstname);
            command.Parameters.AddWithValue("@lastname", student.Lastname);
            command.Parameters.AddWithValue("@id", student.Id);

            command.ExecuteNonQuery();
            return studentToReturn;
        }
    }
}
