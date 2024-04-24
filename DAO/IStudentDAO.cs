using WebStarterDBApp.Models;

namespace WebStarterDBApp.DAO
{
    public interface IStudentDAO
    {
        Student? Insert(Student? student);
        Student? Update(Student? student);
        void Delete(int id);
        Student? GetByID(int id);
        IList<Student> GetAll();
    }
}
