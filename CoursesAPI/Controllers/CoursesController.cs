using System.Collections.Generic;
using System.Web.Http;
using CoursesAPI.Models;
using CoursesAPI.Services.DataAccess;
using CoursesAPI.Services.Models.Entities;
using CoursesAPI.Services.Services;
using CoursesAPI.Services.Exceptions;

namespace CoursesAPI.Controllers
{
	[RoutePrefix("api/projectgroups")]
	public class ProjectGroupController  : ApiController
	{
		private readonly CoursesServiceProvider _service;

		public ProjectGroupController()
		{
			_service = new CoursesServiceProvider(new UnitOfWork<AppDataContext>());
		}

		public List<ProjectGroup> GetProjectGroups()
		{
			return _service.GetProjectGroups();
		}

		[HttpPost]
		public ProjectGroup AddProjectGroup(ProjectGroup model)
		{
			return _service.AddProjectGroup(model);
		}
	}

	[RoutePrefix("api/courses")]
	public class CoursesController : ApiController
	{
		private readonly CoursesServiceProvider _service;

		public CoursesController()
		{
			_service = new CoursesServiceProvider(new UnitOfWork<AppDataContext>());
		}

		/// <summary>
		/// Get list of teachers in course instance
		/// </summary>
		/// <param name="courseInstanceID"></param>
		/// <returns>List<Person></returns>
		[Route("{courseInstanceID}/teachers")]
		public IHttpActionResult GetCourseTeachers(int courseInstanceID)
		{
			List<Person> teachers;
			try
			{
				teachers = _service.GetCourseTeachers(courseInstanceID);
				return Ok(teachers);
			}
			catch (AppObjectNotFoundException ex)
			{
				return NotFound();
			}
			
		}

		/// <summary>
		/// Get list of students in course instance
		/// </summary>
		/// <param name="courseInstanceID"></param>
		/// <returns>List<Person></returns>
		[Route("{courseInstanceID}/students")]
		public IHttpActionResult GetCourseStudents(int courseInstanceID)
		{
			List<Person> students;
			try
			{
				students = _service.GetCourseStudents(courseInstanceID);
				return Ok(students);
			}
			catch (AppObjectNotFoundException ex)
			{
				return NotFound();
			}

		}
		
		/// <summary>
		/// Get all courses on a given semester
		/// </summary>
		/// <param name="semester"></param>
		/// <returns>List<CourseInstanceDTO></returns>
		[Route("semester/{semester}")]
		public IHttpActionResult GetCoursesOnSemester(string semester)
		{
			List<CourseInstanceDTO> courses;
			try
			{
				courses = _service.GetSemesterCourses(semester);
				return Ok(courses);
			}
			catch (AppObjectNotFoundException ex)
			{
				return NotFound();
			}
		}

		/// <summary>
		/// Add project to a course
		/// </summary>
		/// <param name="courseInstanceID"></param>
		/// <param name="model"></param>
		/// <returns>Project</returns>
		[HttpPost]
		[Route("{courseInstanceID}/projects")]
		public IHttpActionResult AddProjectToCourse(int courseInstanceID, Project model)
		{
			Project project;
			try
			{
				project = _service.AddProjectToCourse(courseInstanceID, model);
				return Ok(project);
			}
			catch (AppObjectNotFoundException ex)
			{
				return BadRequest(ex.Message);
			}
		}

		/// <summary>
		/// Get list of project groups
		/// </summary>
		/// <returns>List<ProjectGroup></returns>
		[Route("projectgroup")]
		public IHttpActionResult GetProjectGroups()
		{
			List<ProjectGroup> projectGroups;
			try
			{
				projectGroups = _service.GetProjectGroups();
				return Ok(projectGroups);
			}
			catch (AppObjectNotFoundException ex)
			{
				return NotFound();
			}
		}

		/// <summary>
		/// Insert project group
		/// </summary>
		/// <param name="projectGroup"></param>
		/// <returns>ProjectGroup</returns>
		[HttpPost]
		[Route("projectgroup")]
		public IHttpActionResult AddProjectGroup(ProjectGroup projectGroup)
		{
			ProjectGroup projGroup;
			try
			{
				projGroup = _service.AddProjectGroup(projectGroup);
				return Ok(projGroup);
			}
			catch (AppObjectNotFoundException ex)
			{
				return BadRequest(ex.Message);
			}
		}

		/// <summary>
		/// Add Grade to a project
		/// </summary>
		/// <param name="projectID"></param>
		/// <param name="model"></param>
		/// <returns>GradeDTO</returns>
		[HttpPost]
		[Route("{projectID}/grades")]
		public IHttpActionResult AddGradeToProject(int projectID, GradeToProjectViewModel model)
		{
			GradeDTO grade;
			try
			{
				grade = _service.AddGradeToProject(projectID, model);
				return Ok(grade);
			}
			catch (AppObjectNotFoundException ex)
			{
				return BadRequest(ex.Message);
			}
		}
	}
}
