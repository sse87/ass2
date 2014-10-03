using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using CoursesAPI.Services.Services;
using CoursesAPI.Tests.MockObjects;
using CoursesAPI.Services.Models.Entities;
using CoursesAPI.Services.Exceptions;
using CoursesAPI.Tests.TestExtensions;

namespace CoursesAPI.Tests.Services
{
	[TestClass]
	public class CourseServicesTests
	{
		private CoursesServiceProvider _service;
		private MockUnitOfWork<MockDataContext> _uow;

		[TestInitialize]
		public void Setup()
		{
			_uow = new MockUnitOfWork<MockDataContext>();

			var persons = new List<Person>
			{
				new Person
				{
					ID = 1,
					SSN = "2703874389",
					Name = "Sigurður Snær Eiríksson",
					Email = "sigurdurse09@ru.is"
				}
			};
			_uow.SetRepositoryData(persons);

			var courceInstance = new List<CourseInstance>
			{
				new CourseInstance
				{
					ID = 1,
					CourseID = "T-514-VEFT",
					SemesterID = "20143"
				},
				new CourseInstance
				{
					ID = 2,
					CourseID = "T-213-VEFF",
					SemesterID = "20141"
				}
			};
			_uow.SetRepositoryData(courceInstance);

			_service = new CoursesServiceProvider(_uow);
		}

		// ########## GetCourseStudents ########## //

		[TestMethod]
		public void CoursesGetStudentListInEmptyCourse()
		{
			// Arrange:
			const int courseInstanceID = 1;

			var studentRegistration = new List<StudentRegistration> { };
			_uow.SetRepositoryData(studentRegistration);

			// Act:
			var result = _service.GetCourseStudents(courseInstanceID);

			// Assert:
			Assert.AreEqual(0, result.Count, "Fjöldi nemenda er rangur!");
		}

		[TestMethod]
		public void CoursesGetStudentListInEmptyCourseWithStudentInOtherCourse()
		{
			// Arrange:
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


		// ########## GetCourseTeachers ########## //


		[TestMethod]
		public void CoursesGetTeacherListInEmptyCourse()
		{
			// Arrange:
			const int courseInstanceID = 1;

			var teacherRegistration = new List<TeacherRegistration> { };
			_uow.SetRepositoryData(teacherRegistration);

			// Act:
			var result = _service.GetCourseTeachers(courseInstanceID);

			// Assert:
			Assert.AreEqual(0, result.Count, "Fjöldi kennara er rangur!");
		}

		[TestMethod]
		public void CoursesGetTeacherListInEmptyCourseWithStudentInOtherCourse()
		{
			// Arrange:
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


		// ########## AddProjectCourse ########## //


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


		// ########## AddProjectGroup ########## //


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
		[ExpectedExceptionWithMessage(typeof(ApplicationException), "ProjectGroup model has to have a name, given name is null or empty!")]
		public void ProjectGroupAddWithInvalidName()
		{
			// Arrange:
			var newProjectGroup = new ProjectGroup();
			newProjectGroup.GradedProjectCount = 5;

			// Act:
			var result = _service.AddProjectGroup(newProjectGroup);
		}

		[TestMethod]
		[ExpectedExceptionWithMessage(typeof(ApplicationException), "ProjectGroup model has to have a GradedProjectCount that is higher then zero!")]
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
