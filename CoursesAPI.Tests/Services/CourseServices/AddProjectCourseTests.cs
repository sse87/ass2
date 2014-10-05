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
	public class AddProjectCourseTests
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
		[ExpectedExceptionWithMessage(typeof(AppObjectNotFoundException), "Course instance not found!")]
		public void CoursesAddProjectToInvalidCourse()
		{
			// Arrange:
			const int courseInstanceID = 3;
			// Note: no course instance with this ID found in test data

			// Act:
			var result = _service.AddProjectToCourse(courseInstanceID, new Project());
		}
	}
}
