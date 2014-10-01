using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Objects.DataClasses;
using System.Linq;
using CoursesAPI.Models;
using CoursesAPI.Services.DataAccess;
using CoursesAPI.Services.Models.Entities;
using CoursesAPI.Services.Exceptions;

namespace CoursesAPI.Services.Services
{
	public class CoursesServiceProvider
	{
		private readonly IUnitOfWork _uow;

		private readonly IRepository<CourseInstance> _courseInstances;
		private readonly IRepository<TeacherRegistration> _teacherRegistrations;
		private readonly IRepository<StudentRegistration> _studentRegistrations;
		private readonly IRepository<CourseTemplate> _courseTemplates; 
		private readonly IRepository<Person> _persons;

		public CoursesServiceProvider(IUnitOfWork uow)
		{
			_uow = uow;

			_courseInstances      = _uow.GetRepository<CourseInstance>();
			_courseTemplates      = _uow.GetRepository<CourseTemplate>();
			_teacherRegistrations = _uow.GetRepository<TeacherRegistration>();
			_studentRegistrations = _uow.GetRepository<StudentRegistration>();
			_persons              = _uow.GetRepository<Person>();
		}

		public List<Person> GetCourseStudents(int courseInstanceID)
		{
			var courseInstance = _courseInstances.All()
				.SingleOrDefault(ci => ci.ID == courseInstanceID);
			if (courseInstance == null)
			{
				throw new AppObjectNotFoundException("Course instance not found!");
			}

			var result = (from sr in _studentRegistrations.All()
						  join p in _persons.All() on sr.SSN equals p.SSN
						  where sr.CourseInstanceID == courseInstanceID
						  && sr.Status == 1//1 = virkur nemandi
						  select p).ToList();

			return result;
		}

		public List<Person> GetCourseTeachers(int courseInstanceID)
		{
			// TODO:
			return null;
		}

		public List<CourseInstanceDTO> GetCourseInstancesOnSemester(string semester)
		{
			// TODO:
			return null;
		}

		public List<CourseInstanceDTO> GetSemesterCourses(string semester)
		{
			// TODO
			return null;
		}
	}
}
