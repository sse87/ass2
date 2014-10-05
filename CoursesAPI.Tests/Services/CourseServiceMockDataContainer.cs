using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CoursesAPI.Services.Models.Entities;

namespace CoursesAPI.Tests.Services
{
	public class CourseServiceMockDataContainer
	{
		public CourseServiceMockDataContainer()
		{ }

		public List<Person> getPersons()
		{
			var persons = new List<Person>
			{
				new Person { ID = 1, SSN = "2703874389", Name = "Sigurður Snær Eiríksson", Email = "sigurdurse09@ru.is" },
				new Person { ID = 2, SSN = "0607842569", Name = "Bjarni Egill Ögmundsson", Email = "bjarni12@ru.is"},
				new Person { ID = 3, SSN = "1203735289", Name = "Daníel B. Sigurgeirsson", Email = "dabs@ru.is" }
			};

			return persons;
		}

		public List<CourseTemplate> getCourses()
		{
			var courses = new List<CourseTemplate>
			{
				new CourseTemplate { CourseID = "T-514-VEFT", Name = "Vefþjónustur", 
					Description = "Sniðugur vefþjónustu texti" },
				new CourseTemplate {CourseID = "T-213-VEFF", Name = "Vefforritun", 
					Description = "Sniðugur vefforritunar texti" }
			};

			return courses;
		}

		public List<Semester> getSemesters()
		{
			var semesters = new List<Semester>
			{
				new Semester { ID = "20143", Name =  "Haustönn 2014", DateBegins = new DateTime(2014, 8, 18), 
					DateEnds = new DateTime(2014, 12, 31) },
				new Semester { ID = "20141", Name =  "Vorönn 2014", DateBegins = new DateTime(2014, 1, 1), 
					DateEnds = new DateTime(2014, 5, 31) }
			};

			return semesters;
		}

		public List<CourseInstance> getCourseInstances()
		{
			var courseInstances = new List<CourseInstance>
			{
				new CourseInstance { ID = 1, CourseID = "T-514-VEFT", SemesterID = "20143" },
				new CourseInstance { ID = 2, CourseID = "T-213-VEFF", SemesterID = "20141" }
			};

			return courseInstances;
		}

		public List<StudentRegistration> getStudentRegistrations()
		{
			var studentRegistrations = new List<StudentRegistration>
			{
				new StudentRegistration { ID = 1, SSN = "0607842569", CourseInstanceID = 1,
					Status = StudentRegistration.StudentRegistrationStatus.Active }
			};

			return studentRegistrations;
		}

		public List<TeacherRegistration> getTeacherRegistrations()
		{
			var teacherRegistrations = new List<TeacherRegistration>
			{
				new TeacherRegistration { ID = 1,CourseInstanceID = 1, SSN = "1203735289", 
					Type = TeacherRegistration.TeacherRegistrationType.MainTeacher }
			};

			return teacherRegistrations;
		}

		public List<Project> getProjects()
		{
			var projects = new List<Project>
			{
				new Project{ ID = 1, Name = "Skilaverkefni 1", ProjectType = Project.ProjectTypeEnum.Verkefni,
					Weight = 10, CourseInstanceID = 1}
			};

			return projects;
		}
	}
}
