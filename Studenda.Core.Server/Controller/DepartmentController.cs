using Microsoft.AspNetCore.Mvc;
using Studenda.Core.Data;
using Studenda.Core.Model.Common;

namespace Studenda.Core.Server.Controller;

[Route("department")]
[ApiController]
public class DepartmentController : ControllerBase
{
    public DepartmentController(DataContext dataContext, IConfiguration configuration)
    {
        DataContext = dataContext;
        Configuration = configuration;
    }

    private DataContext DataContext { get; }
    private IConfiguration Configuration { get; }

    [Route("get")]
    [HttpGet]
    public ActionResult<List<Department>> GetAllDepartments()
    {
        var departments = DataContext.Departments.ToList();
        return departments;
    }

    [Route("get/{id:int}")]
    [HttpGet]
    public ActionResult<Department> GetDepartmentById(int id)
    {
        var department = DataContext.Departments.FirstOrDefault(x => x.Id == id)!;
        return department;
    }

    [Route("post")]
    [HttpPost]
    public IActionResult PostSDepartments([FromBody] List<Department> subjects)
    {
        try
        {
            DataContext.Departments.AddRange(subjects);
            DataContext.SaveChanges();
            return Ok();
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [Route("update")]
    [HttpPut]
    public IActionResult UpdateDepartment([FromBody] List<Department> subjects)
    {
        try
        {
            foreach (var subject in subjects)
            {
                var department = DataContext.Departments.FirstOrDefault(x => x.Id == subject.Id);

                if (department != null)
                {
                    DataContext.Departments.Update(department);
                }
                else
                {
                    DataContext.Departments.Add(department!);
                }
            }

            DataContext.SaveChanges();
            return Ok();
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [Route("delete")]
    [HttpDelete]
    public IActionResult DeleteDepartment([FromBody] List<Department> subjects)
    {
        try
        {
            foreach (var subject in subjects)
            {
                var department = DataContext.Departments.FirstOrDefault(x => x.Id == subject.Id);

                if (department != null)
                {
                    DataContext.Departments.Remove(department);
                }
            }

            DataContext.SaveChanges();
            return Ok();
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
}