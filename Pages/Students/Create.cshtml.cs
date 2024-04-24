using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebStarterDBApp.DTO;
using WebStarterDBApp.Models;
using WebStarterDBApp.Services;

namespace WebStarterDBApp.Pages.Students
{
    public class CreateModel : PageModel
    {
        public List<Error>? ErrorArray { get; set; } = new();
        public StudentInsertDTO? StudentInsertDTO { get; set; } = new();

        private readonly IStudentService? _studentService;
        private readonly IValidator<StudentInsertDTO>? _studentInsertValidator;
        
        public CreateModel(IStudentService? studentService, IValidator<StudentInsertDTO>? studentInsertValidator, 
            IMapper? mapper)
        {
            _studentService = studentService;
            _studentInsertValidator = studentInsertValidator;   
        }

        public void OnGet()
        {
            
        }

        public void OnPost(StudentInsertDTO dto)
        {
            StudentInsertDTO = dto;

            var validationResult = _studentInsertValidator!.Validate(dto);

            if (!validationResult.IsValid)
            {
                foreach (var error in validationResult.Errors)
                {
                    ErrorArray!.Add(new Error(error.ErrorCode, error.ErrorMessage, error.PropertyName));
                }
                return;
            }

            try
            {
                Student? student = _studentService!.InsertStudent(dto);
                Response.Redirect("/Students/getall");
            } catch (Exception e)
            {
                ErrorArray!.Add(new Error("", e.Message, ""));
            }
        }
    }
}
