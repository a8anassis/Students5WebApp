using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebStarterDBApp.DTO;
using WebStarterDBApp.Models;
using WebStarterDBApp.Services;

namespace WebStarterDBApp.Pages.Students
{
    public class IndexModel : PageModel
    {
        public List<StudentReadOnlyDTO>? Students { get; set; } = new();    
        public Error? ErrorObj { get; set; } = new();

        private readonly IMapper? _mapper;
        private readonly IStudentService? _studentService;

        public IndexModel(IMapper? mapper, IStudentService? studentService)
        {
            _mapper = mapper;
            _studentService = studentService;
        }

        public IActionResult OnGet()
        {
            try
            {
                ErrorObj = null;
                IList<Student> students = _studentService!.GetAllStudents();
                
                foreach (var student in students)
                {
                    StudentReadOnlyDTO? studentDTO = _mapper!.Map<StudentReadOnlyDTO>(student);
                    Students!.Add(studentDTO);
                }
            } catch (Exception e)
            {
                ErrorObj = new Error("", e.Message, "");
            }

            return Page();
        }
    }
}
