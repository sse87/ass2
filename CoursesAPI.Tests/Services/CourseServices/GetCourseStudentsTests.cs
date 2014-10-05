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
	public class GetCourseStudentsTests
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

		// ########## GetCourseStudents ########## //

		[TestMethod]
		public void CoursesGetStudentListInEmptyCourse()
		{
			// Arrange:
			_uow.SetRepositoryData(_mockData.getCourses());
			_uow.SetRepositoryData(_mockData.getSemesters());
			_uow.SetRepositoryData(_mockData.getCourseInstances());
			const int courseInstanceID = 1;

			var studentRegistration = new List<StudentRegistration> { };
			_uow.SetRepositoryData(studentRegistration);

			// Act:
			var result = _service.GetCourseStudents(courseInstanceID);

			// Assert:
			Assert.AreEqual(0, result.Count, "Fjöldi nemenda er rangur!");
		}

		[TestMethod]
		public void CoursesGetStudentListWithStudentInCourse()
		{
			// Arrange:
			_uow.SetRepositoryData(_mockData.getPersons());
			_uow.SetRepositoryData(_mockData.getCourses());
			_uow.SetRepositoryData(_mockData.getSemesters());
			_uow.SetRepositoryData(_mockData.getCourseInstances());
			const int courseInstanceID = 1;

			var studentRegistration = new List<StudentRegistration>
			{
				new StudentRegistration
				{
					ID = 1,
					SSN = "1203735289",
					CourseInstanceID = 1,
					Status = StudentRegistration.StudentRegistrationStatus.Active
				}
			};

			_uow.SetRepositoryData(studentRegistration);

			// Act:
			var result = _service.GetCourseStudents(courseInstanceID);

			// Assert:
			Assert.AreEqual(1, result.Count, "Fjöldi nemenda er rangur!");
		}

		[TestMethod]
		public void CoursesGetStudentListInEmptyCourseWithStudentInOtherCourse()
		{
			// Arrange:
			_uow.SetRepositoryData(_mockData.getCourses());
			_uow.SetRepositoryData(_mockData.getSemesters());
			_uow.SetRepositoryData(_mockData.getCourseInstances());
			const int courseInstanceID = 1;

			// Create a list of student registrations,
			// none in the course we're testing though.
			var studentRegistration = new List<StudentRegistration>
			{
				new StudentRegistration
				{
					ID = 1,
					SSN = "1203735289",
					CourseInstanceID = 2,
					Status = StudentRegistration.StudentRegistrationStatus.Active
				}
			};
			_uow.SetRepositoryData(studentRegistration);

			// Act:
			var result = _service.GetCourseStudents(courseInstanceID);

			// Assert:
			Assert.AreEqual(0, result.Count, "Fjöldi nemenda er rangur!");
		}

		[TestMethod]
		[ExpectedExceptionWithMessage(typeof(AppObjectNotFoundException), "Course instance not found!")]
		public void CoursesGetStudentListInInvalidCourse()
		{
			// Arrange:
			const int courseInstanceID = 3;
			// Note: no course instance with this ID found in test data

			// Act:
			var result = _service.GetCourseStudents(courseInstanceID);
		}
	}
}
