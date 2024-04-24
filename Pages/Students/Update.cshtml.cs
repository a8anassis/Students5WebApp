using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebStarterDBApp.DTO;
using WebStarterDBApp.Models;
using WebStarterDBApp.Services;
using WebStarterDBApp.Validators;

namespace WebStarterDBApp.Pages.Students
{
    public class UpdateModel : PageModel
    {
        public StudentUpdateDTO? StudentUpdateDTO { get; set; } = new();
        public List<Error> ErrorArray { get; set; } = new();

        public readonly IStudentService? _studentService;
        public readonly IValidator<StudentUpdateDTO>? _studentUpdateValidator;
        public readonly IMapper? _mapper;

        public UpdateModel(IStudentService? studentService, IValidator<StudentUpdateDTO>? studentUpdateValidator, 
            IMapper? mapper)
        {
            _studentService = studentService;
            _studentUpdateValidator = studentUpdateValidator;
            _mapper = mapper;
        }


        public IActionResult OnGet(int id)
        {
            try
            {
                Student? student = _studentService!.GetStudent(id);
                StudentUpdateDTO = _mapper!.Map<StudentUpdateDTO>(student);
            } catch (Exception e)
            {
                ErrorArray.Add(new Error("", e.Message, ""));
            }
            return Page();
        }

        public void OnPost(StudentUpdateDTO dto)
        {
            StudentUpdateDTO = dto;

            var validationResult = _studentUpdateValidator!.Validate(dto);

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
                Student? student = _studentService!.UpdateStudent(dto);
                Response.Redirect("/Students/getall");
            }
            catch (Exception e)
            {
                ErrorArray!.Add(new Error("", e.Message, ""));
            }
        }



    }
}
