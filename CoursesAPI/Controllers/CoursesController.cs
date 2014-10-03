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
		
		[Route("semester/{semester}")]
		public List<CourseInstanceDTO> GetCoursesOnSemester(string semester)
		{
			return _service.GetSemesterCourses(semester);
		}

		[HttpPost]
		[Route("{courseInstanceID}/projects")]
		public Project AddProjectToCourse(int courseInstanceID, Project model)
		{
			return _service.AddProjectCourse(courseInstanceID, model);
		}

        [HttpPost]
        [Route("{projectID}/grades")]
         public GradeDTO AddGradeToProject(int projectID, AddGradeToProjectViewModel model)
        {
            return _service.AddGradeToProject(projectID, model);
        }
	}
}
