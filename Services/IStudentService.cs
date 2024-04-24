using WebStarterDBApp.DTO;
using WebStarterDBApp.Models;

namespace WebStarterDBApp.Services
{
    public interface IStudentService
    {
        Student? InsertStudent(StudentInsertDTO dto);
        Student? UpdateStudent(StudentUpdateDTO dto);
        Student? DeleteStudent(int id);
        Student? GetStudent(int id);
        IList<Student> GetAllStudents();
    }
}
