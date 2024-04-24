using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebStarterDBApp.Models;
using WebStarterDBApp.Services;

namespace WebStarterDBApp.Pages.Students
{
    public class DeleteModel : PageModel
    {
        public List<Error> ErrorArray { get; set; } = new();
        private readonly IStudentService _studentService;

        public DeleteModel(IStudentService studentService)
        {
            _studentService = studentService;
        }

        public void OnGet(int id)
        {
            try
            {
                Student? student = _studentService?.DeleteStudent(id);
                Response.Redirect("/Students/getall");
            }
            catch (Exception e)
            {
                ErrorArray.Add(new Error("", e.Message, ""));
            }
        }
    }
}
