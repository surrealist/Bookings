using GMM.Bookings.Services.Data;
using Microsoft.AspNetCore.Mvc;
using GMM.Bookings.Models;
using GMM.Bookings.Models.DTOs;
using GMM.Bookings.Services;
using GMM.Bookings.APIs.Validators;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Diagnostics;

namespace GMM.Bookings.APIs.Controllers
{
  [Produces("application/json")]
  [Route("api/v1/courses")]
  [ApiController]
  public class CoursesController : ControllerBase
  {  
    private readonly App app;
    private readonly IValidator<NewCourse> validator;

    public CoursesController(App app, 
      IValidator<NewCourse> validator)
    {  
      this.app = app;
      this.validator = validator;
    }

    [Authorize]
    [HttpGet(Name = "Get courses")]
    public ActionResult<IEnumerable<CourseResponse>> GetAll()
    {
      var items = app.Courses.All().ToList();
      return items.ConvertAll(x => CourseResponse.FromModel(x));
    }

    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [HttpGet("{id}", Name = "Get by Course Id")]
    public ActionResult<CourseResponse> GetById(string id)
    {
      var item = app.Courses.All().SingleOrDefault(x => x.Id == id);
      if (item == null) return NotFound();

      return CourseResponse.FromModel(item);
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status201Created, Type=typeof(ProblemDetails))]
    public ActionResult<CourseResponse> Create(NewCourse item)
    {
      var result = validator.Validate(item);
      if (!result.IsValid)
      {
        //return BadRequest(new ProblemDetails {  })
        return BadRequest(result.Errors);
      }

      var m = app.Courses.CreateCourse(item);

      return CreatedAtAction("GetById",
         new { id = m.Id }, CourseResponse.FromModel(m));
    }

    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public ActionResult<CourseResponse> Put(string id, 
                                          UpdatedCourse item)
    {
      var course = app.Courses.UpdateCourse(id, item);
      return Ok(CourseResponse.FromModel(course));
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public ActionResult DeleteById(string id)
    {
      var course = app.Courses.Find(id);
      if (course == null) return NotFound();

      app.Courses.Remove(course);
      app.SaveChanges();

      //return Ok(CourseResponse.FromModel(course));
      return NoContent();
    }

    [HttpPost("{id}/activate")]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public ActionResult Activate(string id)
    {
      var course = app.Courses.Find(id);
      if (course == null) return NotFound();
      if (course.IsActive) return NoContent();

      course.IsActive = true;
      app.SaveChanges();
      return Ok();
    }

    [HttpPost("{id}/inactivate")]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public ActionResult Inactivate(string id)
    {
      var course = app.Courses.Find(id);
      if (course == null) return NotFound();
      if (!course.IsActive) return NoContent();

      course.IsActive = false;
      app.SaveChanges();
      return Ok();
    }

  }
} 
