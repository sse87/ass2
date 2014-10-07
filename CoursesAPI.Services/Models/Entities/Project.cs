using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;

namespace CoursesAPI.Services.Models.Entities
{
	[Table("Projects")]
	public class Project
	{
		/// <summary>
		/// A database-generated ID of the project
		/// </summary>
		public int ID { get; set; }

		/// <summary>
		/// Name of the project
		/// </summary>
		public string Name { get; set; }

		/// <summary>
		/// The type of teacher:
		/// 1: Verkefni
		/// 2: Netpróf
		/// 3: Miðannarpróf
		/// 4: Lokapróf
		/// 4: Endurtektarpróf
		/// </summary>
		public ProjectTypeEnum ProjectType { get; set; }

		/// <summary>
		/// Weight of the project in percentages
		/// </summary>
		public int Weight { get; set; }

		/// <summary>
		/// The ID of the course instance the given teacher is teaching
		/// </summary>
		public int CourseInstanceID { get; set; }

		/// <summary>
		/// The ID of a project group if there is one
		/// </summary>
		public int? ProjectGroupID { get; set; }

		/// <summary>
		/// The ID of the project .... TODO!
		/// </summary>
		public int? OnlyIfHigherThanProjectID { get; set; }

		/// <summary>
		/// The minimum grade of this project to pass the cource
		/// </summary>
		public double? MinGradeToPassCourse { get; set; }

		[ForeignKey("ProjectGroupID")]
		public ProjectGroup ProjectGroup { get; set; }



		/// <summary>
		/// The type of teacher:
		/// 1: Verkefni
		/// 2: Netpróf
		/// 3: Miðannarpróf
		/// 4: Lokapróf
		/// 4: Endurtektarpróf
		/// </summary>
		public enum ProjectTypeEnum { Verkefni = 1, Netprof = 2, Midannarprof = 3, Lokaprof = 4, Endurtektarprof = 5 }
	}
}
