using ApiDemo.Models;
using ApiDemo.Services.Abstraction;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ApiDemo.Controllers;

[Route("api/[controller]")]
[ApiController]
public class DepartmentsController : ControllerBase
{
    private readonly IDepartmentService _departmentService;

    public DepartmentsController(IDepartmentService departmentService)
    {
        _departmentService = departmentService;
    }

    [HttpGet]
    public IActionResult GetAllDetails()
    {
        return Ok(_departmentService.GetDepartments());
    }

    [HttpGet("{id}")]
    public IActionResult Details(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var department = _departmentService.GetDepartmentById(id);
        if (department == null)
        {
            return NotFound();
        }

        return Ok(department);
    }

    [HttpPost]
    public IActionResult Create(Department department)
    {
        if (ModelState.IsValid)
        {
            _departmentService.AddDepartment(department);
        }
        return Ok(department);
    }


    [HttpPut("{id}")]
    public IActionResult Edit(int id, Department department)
    {
        if (id != department.Id)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            try
            {
                _departmentService.UpdateDepartment(department);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_departmentService.DepartmentExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return Ok(department);
        }
        return BadRequest(ModelState.ToList());
    }

    [HttpDelete("{id}")]
    public IActionResult DeleteConfirmed(int id)
    {
        var department = _departmentService.GetDepartmentById(id);

        if (department != null)
        {
            _departmentService.DeleteDepartment(department);
        }

        return Ok(department);
    }
}
