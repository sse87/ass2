﻿using System;
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
		private CourseServiceMockDataContainer _mockData;

		[TestInitialize]
		public void Setup()
		{
			_uow = new MockUnitOfWork<MockDataContext>();
			_service = new CoursesServiceProvider(_uow);
			_mockData = new CourseServiceMockDataContainer();
		}

	}
}
