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
		private readonly IRepository<CourseTemplate> _courseTemplates; 
		private readonly IRepository<Person> _persons;
		private readonly IRepository<StudentRegistration> _studentRegistrations;
		private readonly IRepository<Project> _projects;
		private readonly IRepository<ProjectGroup> _projectGroups;
		private readonly IRepository<Grade> _grades;

		public CoursesServiceProvider(IUnitOfWork uow)
		{
			_uow = uow;

			_courseInstances      = _uow.GetRepository<CourseInstance>();
			_courseTemplates      = _uow.GetRepository<CourseTemplate>();
			_teacherRegistrations = _uow.GetRepository<TeacherRegistration>();
			_persons              = _uow.GetRepository<Person>();
			_studentRegistrations = _uow.GetRepository<StudentRegistration>();
			_projects             = _uow.GetRepository<Project>();
			_projectGroups        = _uow.GetRepository<ProjectGroup>();
			_grades               = _uow.GetRepository<Grade>();
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
						  && sr.Status == StudentRegistration.StudentRegistrationStatus.Active
						  select p).ToList();

			return result;
		}

		public List<Person> GetCourseTeachers(int courseInstanceID)
		{
			var courseInstance = _courseInstances.All()
				.SingleOrDefault(ci => ci.ID == courseInstanceID);
			if (courseInstance == null)
			{
				throw new AppObjectNotFoundException("Course instance not found!");
			}

			var result = (from tr in _teacherRegistrations.All()
							  join p in _persons.All() on tr.SSN equals p.SSN
							  where tr.CourseInstanceID == courseInstanceID
							  select p).ToList();

			return result;
		}

		public Project AddProjectToCourse(int courseInstanceID, Project model)
		{
			var courseInstance = _courseInstances.All().SingleOrDefault(ci => ci.ID == courseInstanceID);
			if (courseInstance == null)
			{
				throw new AppObjectNotFoundException("Course instance not found!");
			}

			var newProject = new Project();
			newProject.ID = 0;
			newProject.CourseInstanceID = courseInstanceID;
			newProject.Name = model.Name;
			newProject.ProjectType = model.ProjectType;
			newProject.Weight = model.Weight;

			// Nullable variables
			newProject.ProjectGroupID = model.ProjectGroupID;
			newProject.OnlyIfHigherThanProjectID = model.OnlyIfHigherThanProjectID;
			newProject.MinGradeToPassCourse = model.MinGradeToPassCourse;

			if (newProject.ProjectGroupID != null)
			{
				newProject.ProjectGroup = _projectGroups.All()
					.Where(x => x.ID == newProject.ProjectGroupID)
					.SingleOrDefault();
				if (newProject.ProjectGroup == null)
				{
					throw new AppObjectNotFoundException("ProjectGroup not found!");
				}
			}

			_projects.Add(newProject);
			_uow.Save();

			return newProject;
		}

        public ProjectGroup AddProjectGroup(ProjectGroup model)
		{
			if (string.IsNullOrEmpty(model.Name))
			{
				throw new AppObjectNotFoundException("ProjectGroup model has to have a name, given name is null or empty!");
			}
			if (model.GradedProjectCount == 0)
			{
				throw new AppObjectNotFoundException("ProjectGroup model has to have a GradedProjectCount that is higher then zero!");
			}

			var newPG = new ProjectGroup();
			newPG.Name = model.Name;
			newPG.GradedProjectCount = model.GradedProjectCount;

			_projectGroups.Add(newPG);
			_uow.Save();

			return _projectGroups.All().Where(x => x.Name.Equals(model.Name) && x.GradedProjectCount.Equals(model.GradedProjectCount)).SingleOrDefault();
		}

		/// <summary>
		/// Get list of project groups
		/// </summary>
		/// <returns></returns>
		public List<ProjectGroup> GetProjectGroups()
		{
			return _projectGroups.All().OrderBy(x => x.Name).ToList();
		}

		/// <summary>
		/// Get list of courses that is active during the semester
		/// </summary>
		/// <param name="semester"></param>
		/// <returns></returns>
		public List<CourseInstanceDTO> GetSemesterCourses(string semester)
		{
			var result = (from ci in _courseInstances.All()
						  join ct in _courseTemplates.All() on ci.CourseID equals ct.CourseID
						  where ci.SemesterID == semester
						  select new CourseInstanceDTO
						  {
							  CourseID = ct.CourseID,
							  CourseInstanceID = ci.ID,
							  Name = ct.Name
							  /*MainTeacher = (from tr in _teacherRegistrations.All()
											 join p in _persons.All() on tr.SSN equals p.SSN
											 where tr.CourseInstanceID == ci.ID && tr.Type == TeacherRegistration.TeacherRegistrationType.MainTeacher
											 select p).FirstOrDefault().Name*/
						  }).ToList();
			
			return null;
		}

		public GradeDTO AddGradeToProject(int projectID, GradeToProjectViewModel model)
		{
			var project = _projects.All().SingleOrDefault(p => p.ID == projectID);
			if (project == null)
			{
				throw new AppObjectNotFoundException("No project with that ID exists");
			}

			var studentRegistration = _studentRegistrations.All().SingleOrDefault(x => x.SSN == model.SSN && x.CourseInstanceID == project.CourseInstanceID);
			if (studentRegistration == null)
			{
				throw new AppObjectNotFoundException("No student with that SSN is registered in that course");
			}

			Grade gradeData = new Grade();
			gradeData.ProjectGrade = model.grade;
			gradeData.ProjectID = projectID;
			gradeData.SSN = model.SSN;

			_grades.Add(gradeData);
			_uow.Save();

			var person = _persons.All().SingleOrDefault(n => n.SSN == model.SSN);
			var course = _courseInstances.All().SingleOrDefault(ci => ci.ID == project.CourseInstanceID);
			
			GradeDTO gradeDTO = new GradeDTO();
			gradeDTO.studentName = person.Name;
			gradeDTO.grade = model.grade;
			gradeDTO.projectName = project.Name;
			gradeDTO.courseName = course.CourseID;

			return gradeDTO;
		}

		public GradeViewModel GetProjectGrades(int projectID, string SSN)
		{
			// Get given project
			var project = _projects.All().SingleOrDefault(p => p.ID == projectID);
			if (project == null)
			{
				throw new AppObjectNotFoundException("No project with that ID exists");
			}

			// Get given student
			var person = _persons.All().SingleOrDefault(n => n.SSN == SSN);
			if (person == null)
			{
				throw new AppObjectNotFoundException("No person with that SSN exists");
			}

			// Get project grade
			var grade = _grades.All().SingleOrDefault(x => x.SSN == SSN && x.ProjectID == project.ID);
			if (grade == null)
			{
				throw new AppObjectNotFoundException("No grade has been inserted for this project");
			}

			// Get course template
			var courseTemplate = (from ci in _courseInstances.All()
								 join ct in _courseTemplates.All() on ci.CourseID equals ct.CourseID
								 where ci.ID == project.CourseInstanceID
								 select ct).SingleOrDefault();

			// Build up return variable
			GradeViewModel gradeVM = new GradeViewModel();
			gradeVM.CourseName = courseTemplate.Name;
			gradeVM.ProjectName = project.Name;
			gradeVM.Grade = grade.ProjectGrade;

			// Find out where the user grade 
			var projectGrades = _grades.All().Where(x => x.ProjectID == project.ID).OrderByDescending(x => x.ProjectGrade);
			int rankedUpper = -1;
			int rankedLower = -1;
			int counter = 1;
			foreach (var thisGrade in projectGrades)
			{
				if (thisGrade.ProjectGrade == grade.ProjectGrade)
				{
					if (rankedUpper == -1)
					{
						rankedUpper = counter;
					}
					rankedLower = counter;
				}
				counter++;
			}

			gradeVM.Rank = new GradeRank
			{
				Upper = rankedUpper,
				Lower = rankedLower,
				Total = projectGrades.Count()
			};

			return gradeVM;
		}

		public GradeViewModel GetCourseFinalGrade(int courseInstanceID, string SSN)
		{
			// TODO: Spunring hvernig við ætlum að skila lokaeinkunn af okkur
			// Á lokaeinkunn að vera 10 eða 3,5 þegar TotalWeight er 35

			return new GradeViewModel
			{
				CourseName = "",
				ProjectName = "",
				Grade = 0.0,
				Rank = new GradeRank
				{
					Upper = 0,
					Lower = 0,
					Total = 0
				}
			};
		}

		public CourseGradesViewModel GetCourseGrades(int courseInstanceID, string SSN)
		{
			var courseInstance = _courseInstances.All().SingleOrDefault(ci => ci.ID == courseInstanceID);
			if (courseInstance == null)
			{
				throw new AppObjectNotFoundException("Course instance not found!");
			}

			var person = _persons.All().SingleOrDefault(n => n.SSN == SSN);
			if (person == null)
			{
				throw new AppObjectNotFoundException("No person with that SSN exists");
			}

			// Return variable
			var courseGrades = new CourseGradesViewModel();

			// Insert all course project grades
			List<GradeViewModel> grades = new List<GradeViewModel>();
			foreach (var project in _projects.All().Where(x => x.CourseInstanceID == courseInstance.ID))
			{
				// Try catch so project with no grade will be ignored
				try
				{
					grades.Add(GetProjectGrades(project.ID, person.SSN));
				}
				catch (AppObjectNotFoundException) { }
			}
			courseGrades.Grades = grades;

			// Insert final grade
			courseGrades.FinalGrade = GetCourseFinalGrade(courseInstance.ID, person.SSN);

			return courseGrades;
		}
	}
}
