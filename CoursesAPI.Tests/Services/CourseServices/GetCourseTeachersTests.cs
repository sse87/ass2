using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using CoursesAPI.Services.Services;
using CoursesAPI.Tests.MockObjects;
using CoursesAPI.Services.Models.Entities;
using CoursesAPI.Services.Exceptions;
using CoursesAPI.Tests.TestExtensions;

namespace CoursesAPI.Tests.Services.CourseServices
{
	[TestClass]
	public class GetCourseTeachersTests
	{
		private CoursesServiceProvider _service;
		private MockUnitOfWork<MockDataContext> _uow;
		private CourseServiceMockDataContainer _mockData;

		[TestInitialize]
		public void Setup()
		{
			_uow = new MockUnitOfWork<MockDataContext>();
			_service = new CoursesServiceProvider(_uow);
			_mockData = new CourseServiceMockDataContainer();
		}


		[TestMethod]
		public void CoursesGetTeacherListInEmptyCourse()
		{
			// Arrange:
			_uow.SetRepositoryData(_mockData.getCourses());
			_uow.SetRepositoryData(_mockData.getSemesters());
			_uow.SetRepositoryData(_mockData.getCourseInstances());
			const int courseInstanceID = 1;

			var teacherRegistration = new List<TeacherRegistration> { };
			_uow.SetRepositoryData(teacherRegistration);

			// Act:
			var result = _service.GetCourseTeachers(courseInstanceID);

			// Assert:
			Assert.AreEqual(0, result.Count, "Fjöldi kennara er rangur!");
		}

		[TestMethod]
		public void CoursesGetTeacherListWithTeacherInCourse()
		{
			// Arrange:
			_uow.SetRepositoryData(_mockData.getPersons());
			_uow.SetRepositoryData(_mockData.getCourses());
			_uow.SetRepositoryData(_mockData.getSemesters());
			_uow.SetRepositoryData(_mockData.getCourseInstances());
			const int courseInstanceID = 1;

			var teacherRegistration = new List<TeacherRegistration>
			{
				new TeacherRegistration
				{
					ID = 1,
					SSN = "1203735289",
					CourseInstanceID = 1,
					Type = TeacherRegistration.TeacherRegistrationType.MainTeacher
				}
			};

			_uow.SetRepositoryData(teacherRegistration);

			// Act:
			var result = _service.GetCourseTeachers(courseInstanceID);

			// Assert:
			Assert.AreEqual(1, result.Count, "Fjöldi kennara er rangur!");
		}

		[TestMethod]
		public void CoursesGetTeacherListInEmptyCourseWithStudentInOtherCourse()
		{
			// Arrange:
			_uow.SetRepositoryData(_mockData.getPersons());
			_uow.SetRepositoryData(_mockData.getCourses());
			_uow.SetRepositoryData(_mockData.getSemesters());
			_uow.SetRepositoryData(_mockData.getCourseInstances());
			_uow.SetRepositoryData(_mockData.getStudentRegistrations());
			const int courseInstanceID = 1;

			// Create a list of student registrations,
			// none in the course we're testing though.
			var teacherRegistration = new List<TeacherRegistration>
			{
				new TeacherRegistration
				{
					ID = 1,
					SSN = "1203735289",
					CourseInstanceID = 2,
					Type = TeacherRegistration.TeacherRegistrationType.MainTeacher
				}
			};
			_uow.SetRepositoryData(teacherRegistration);

			// Act:
			var result = _service.GetCourseTeachers(courseInstanceID);

			// Assert:
			Assert.AreEqual(0, result.Count, "Fjöldi kennara er rangur!");
		}

		[TestMethod]
		[ExpectedExceptionWithMessage(typeof(AppObjectNotFoundException), "Course instance not found!")]
		public void CoursesGetTeacherListInInvalidCourse()
		{
			// Arrange:
			const int courseInstanceID = 3;
			// Note: no course instance with this ID found in test data

			// Act:
			var result = _service.GetCourseTeachers(courseInstanceID);
		}
	}
}
