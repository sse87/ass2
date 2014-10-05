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
	public class AddProjectGroupTests
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
		public void ProjectGroupAddWithValidInputsWorks()
		{
			// Arrange:
			var newProjectGroup = new ProjectGroup();
			newProjectGroup.Name = "Netpróf";
			newProjectGroup.GradedProjectCount = 5;

			// Act:
			var result = _service.AddProjectGroup(newProjectGroup);

			// Assert:
			Assert.AreEqual("Netpróf", result.Name, "ProjectGroup Name er rangur");
			Assert.AreEqual(5, result.GradedProjectCount, "ProjectGroup GradedProjectCount er rangur!");
		}

		[TestMethod]
		[ExpectedExceptionWithMessage(typeof(AppObjectNotFoundException), "ProjectGroup model has to have a name, given name is null or empty!")]
		public void ProjectGroupAddWithInvalidName()
		{
			// Arrange:
			var newProjectGroup = new ProjectGroup();
			newProjectGroup.GradedProjectCount = 5;

			// Act:
			var result = _service.AddProjectGroup(newProjectGroup);
		}

		[TestMethod]
		[ExpectedExceptionWithMessage(typeof(AppObjectNotFoundException), "ProjectGroup model has to have a GradedProjectCount that is higher then zero!")]
		public void ProjectGroupAddWithInvalidGradedProjectCount()
		{
			// Arrange:
			var newProjectGroup = new ProjectGroup();
			newProjectGroup.Name = "Netpróf";

			// Act:
			var result = _service.AddProjectGroup(newProjectGroup);
		}
	}
}
