using System.Collections.Generic;
using System.Web.Http;
using CoursesAPI.Models;
using CoursesAPI.Services.DataAccess;
using CoursesAPI.Services.Models.Entities;
using CoursesAPI.Services.Services;

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
		/// <returns></returns>
		/// /api/courses/{courseInstanceID}/teachers
		[Route("{courseInstanceID}/teachers")]
		public List<Person> GetCourseTeachers(int courseInstanceID)
		{
			return _service.GetCourseTeachers(courseInstanceID);
		}

		/// <summary>
		/// Get list of students in course instance
		/// </summary>
		/// <param name="courseInstanceID"></param>
		/// <returns></returns>
		/// /api/courses/{courseInstanceID}/students
		[Route("{courseInstanceID}/students")]
		public List<Person> GetCourseStudents(int courseInstanceID)
		{
			return _service.GetCourseStudents(courseInstanceID);
		}
		
		/// <summary>
		/// Get all courses on a given semester
		/// </summary>
		/// <param name="semester"></param>
		/// <returns></returns>
		/// /api/courses/semester/{semester}
		[Route("semester/{semester}")]
		public List<CourseInstanceDTO> GetCoursesOnSemester(string semester)
		{
			return _service.GetSemesterCourses(semester);
		}

		/// <summary>
		/// Add project to a course
		/// </summary>
		/// <param name="courseInstanceID"></param>
		/// <param name="model"></param>
		/// <returns></returns>
		/// /api/courses/{courseInstanceID}/projects
		[HttpPost]
		[Route("{courseInstanceID}/projects")]
		public Project AddProjectToCourse(int courseInstanceID, Project model)
		{
			return _service.AddProjectToCourse(courseInstanceID, model);
		}

		/// <summary>
		/// Get list of project groups
		/// </summary>
		/// <returns></returns>
		/// /api/courses/projectgroup
		[Route("projectgroup")]
		public List<ProjectGroup> GetProjectGroups()
		{
			return _service.GetProjectGroups();
		}

		/// <summary>
		/// Insert project group
		/// </summary>
		/// <param name="projectGroup"></param>
		/// <returns></returns>
		/// /api/courses/projectgroup
		[HttpPost]
		[Route("projectgroup")]
		public ProjectGroup AddProjectGroup(ProjectGroup projectGroup)
		{
			return _service.AddProjectGroup(projectGroup);
		}

		/// <summary>
		/// Add Grade to a project
		/// </summary>
		/// <param name="projectID"></param>
		/// <param name="model"></param>
		/// <returns></returns>
		/// /api/courses/project/{projectID}/grade
		[HttpPost]
		[Route("project/{projectID}/grade")]
		public GradeDTO AddGradeToProject(int projectID, GradeToProjectViewModel model)
		{
			return _service.AddGradeToProject(projectID, model);
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="projectID"></param>
		/// <returns></returns>
		/// /api/courses/project/{projectID}/{SSN}/grade
		[Route("project/{projectID}/{SSN}/grade")]
		public GradeDTO GetProjectGrade(int projectID, string SSN)
		{
			return _service.GetProjectGrade(projectID, SSN);
		}
	}
}
