using System.Collections.Generic;
using System.Web.Http;
using CoursesAPI.Models;
using CoursesAPI.Services.DataAccess;
using CoursesAPI.Services.Models.Entities;
using CoursesAPI.Services.Services;
using CoursesAPI.Services.Exceptions;

namespace CoursesAPI.Controllers
{
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
		/// /api/courses/{courseInstanceID}/teachers
		[Route("{courseInstanceID}/teachers")]
		public IHttpActionResult GetCourseTeachers(int courseInstanceID)
		{
			try
			{
				List<Person> teachers = _service.GetCourseTeachers(courseInstanceID);
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
		/// /api/courses/{courseInstanceID}/students
		[Route("{courseInstanceID}/students")]
		public IHttpActionResult GetCourseStudents(int courseInstanceID)
		{
			try
			{
				List<Person> students = _service.GetCourseStudents(courseInstanceID);
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
		/// /api/courses/semester/{semester}
		[Route("semester/{semester}")]
		public IHttpActionResult GetCoursesOnSemester(string semester)
		{
			try
			{
				List<CourseInstanceDTO> courses = _service.GetSemesterCourses(semester);
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
		/// /api/courses/{courseInstanceID}/projects
		[HttpPost]
		[Route("{courseInstanceID}/projects")]
		public IHttpActionResult AddProjectToCourse(int courseInstanceID, Project model)
		{
			try
			{
				Project project = _service.AddProjectToCourse(courseInstanceID, model);
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
		/// /api/courses/projectgroup
		[Route("projectgroup")]
		public IHttpActionResult GetProjectGroups()
		{
			try
			{
				List<ProjectGroup> projectGroups = _service.GetProjectGroups();
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
		/// /api/courses/projectgroup
		[HttpPost]
		[Route("projectgroup")]
		public IHttpActionResult AddProjectGroup(ProjectGroup projectGroup)
		{
			try
			{
				ProjectGroup projGroup = _service.AddProjectGroup(projectGroup);
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
			try
			{
				GradeDTO grade = _service.AddGradeToProject(projectID, model);
				return Ok(grade);
			}
			catch (AppObjectNotFoundException ex)
			{
				return BadRequest(ex.Message);
			}
		}

		/// <summary>
		/// Returns grade of a given project
		/// </summary>
		/// <param name="projectID"></param>
		/// <returns>GradeViewModel</returns>
		/// /api/courses/project/{projectID}/{SSN}/grade
		[Route("project/{projectID}/{SSN}/grade")]
		public IHttpActionResult GetProjectGrades(int projectID, string SSN)
		{
			try
			{
				GradeViewModel grade = _service.GetProjectGrades(projectID, SSN);
				return Ok(grade);
			}
			catch (AppObjectNotFoundException ex)
			{
				return NotFound();
			}
			
		}

		/// <summary>
		/// Returns all grades of a given course instance
		/// </summary>
		/// <returns>CourseGradesViewModel</returns>
		/// /api/courses/{courseInstanceID}/{SSN}/grade
		[Route("{courseInstanceID}/{SSN}/grade")]
		public IHttpActionResult GetCourseGradesFromOneStudent(int courseInstanceID, string SSN)
		{
			try
			{
				CourseGradesViewModel courseGrades = _service.GetCourseGrades(courseInstanceID, SSN);
				return Ok(courseGrades);
			}
			catch (AppObjectNotFoundException ex)
			{
				return NotFound();
			}
		}

		/// <summary>
		/// Returns all grades of all students in given course instance
		/// </summary>
		/// <param name="courseInstanceID"></param>
		/// <returns>List<CourseGradesViewModel></returns>
		[Route("{courseInstanceID}/grade")]
		public IHttpActionResult GetCourseGrades(int courseInstanceID)
		{
			try
			{
				List<CourseGradesViewModel> courseGrades = _service.GetStudentsGrade(courseInstanceID);
				return Ok(courseGrades);
			}
			catch (AppObjectNotFoundException ex)
			{
				return NotFound();
			}
		}
	}
}
