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
        return DataContext.Departments.ToList();
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
    public IActionResult AddSDepartments([FromBody] List<Department> departments)
    {
        try
        {
            DataContext.Departments.AddRange(departments);
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
    public IActionResult UpdateDepartment([FromBody] List<Department> departments)
    {
        try
        {
            foreach (var department in departments)
            {

                if (DataContext.Departments.Any(u=>u.Id==department.Id))
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
    public IActionResult DeleteDepartments([FromBody] List<int> departmentsIds)
    {
        try
        {
            foreach (var id in departmentsIds)
            {
                var department = DataContext.Departments.FirstOrDefault(x => x.Id == id);

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