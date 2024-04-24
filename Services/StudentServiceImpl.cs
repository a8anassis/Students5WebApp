using AutoMapper;
using System.Transactions;
using WebStarterDBApp.DAO;
using WebStarterDBApp.DTO;
using WebStarterDBApp.Models;

namespace WebStarterDBApp.Services
{
    public class StudentServiceImpl : IStudentService
    {
        private readonly IStudentDAO? _studentDAO;
        private readonly IMapper? _mapper;
        private readonly ILogger<StudentServiceImpl>? _logger;

        public StudentServiceImpl(IStudentDAO? studentDAO, IMapper? mapper, 
            ILogger<StudentServiceImpl>? logger)
        {
            _studentDAO = studentDAO;
            _mapper = mapper;
            _logger = logger;
        }

        public Student? DeleteStudent(int id)
        {
            Student? studentToReturn = null;

            try
            {
                using TransactionScope scope = new();     
                studentToReturn = _studentDAO!.GetByID(id);
                if (studentToReturn == null) return null;
                _studentDAO.Delete(id);
                scope.Complete();
                 
                _logger!.LogInformation("Delete Success");
                return studentToReturn;
            } catch (Exception e)
            {
                _logger!.LogError("An error occured while deleting student: " + e.Message);
                throw;
            }
        }

        public IList<Student> GetAllStudents()
        {
            try
            {
                IList<Student> students = _studentDAO!.GetAll();
                return students;
            } catch (Exception e)
            {
                _logger!.LogError("An error occured while fetching students: " + e.Message);
                throw;
            }
        }

        public Student? GetStudent(int id)
        {
            try
            {
                return _studentDAO!.GetByID(id);
            } catch (Exception e)
            {
                _logger!.LogError("An error occured while fetching a student: " + e.Message);
                throw;
            }
        }

        public Student? InsertStudent(StudentInsertDTO dto)
        {
            if (dto is null) return null;
            
            try
            {
                var student = _mapper!.Map<Student>(dto);
                using TransactionScope scope = new();
                Student? insertedStudent = _studentDAO!.Insert(student);
                scope.Complete();
                _logger!.LogInformation("Success in insert");
                return insertedStudent;
            } catch (Exception e)
            {
                _logger!.LogError("An error occured while inserting a student: " + e.Message);
                throw;
            }
        }

        public Student? UpdateStudent(StudentUpdateDTO dto)
        {
            if (dto is null) return null;
            Student? studentToReturn = null;

            try
            {
                var student = _mapper!.Map<Student>(dto);
                using TransactionScope scope = new();
                studentToReturn = _studentDAO!.Update(student);
                scope.Complete();
                _logger!.LogInformation("Success in updating student");
                return studentToReturn;           
            } catch (Exception e)
            {
                _logger!.LogError("An error occured while inserting a student: " + e.Message);
                throw;
            }

        }
    }
}
