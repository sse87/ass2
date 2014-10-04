using System.Collections.Generic;
using System.Web.Http;
using CoursesAPI.Models;
using CoursesAPI.Services.DataAccess;
using CoursesAPI.Services.Models.Entities;
using CoursesAPI.Services.Services;

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
        public ProjectGroup AddProjectGroup(AddProjectGroupViewModel model)
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
		/// <returns></returns>
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
		[HttpPost]
		[Route("projectgroup")]
		public ProjectGroup AddProjectGroup(AddProjectGroupViewModel projectGroup)
		{
			return _service.AddProjectGroup(projectGroup);
		}

		/// <summary>
		/// Add Grade to a project
		/// </summary>
		/// <param name="projectID"></param>
		/// <param name="model"></param>
		/// <returns></returns>
		[HttpPost]
		[Route("{projectID}/grades")]
			public GradeDTO AddGradeToProject(int projectID, AddGradeToProjectViewModel model)
		{
			return _service.AddGradeToProject(projectID, model);
		}
	}
}
