using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoursesAPI.Models
{
	public class GradeViewModel
	{
		/// <summary>
		/// The name of the person
		/// </summary>
		public string PersonName { get; set; }

		/// <summary>
		/// The name of the course
		/// </summary>
		public string CourseName { get; set; }

		/// <summary>
		/// The name of the project
		/// </summary>
		public string ProjectName { get; set; }

		/// <summary>
		/// The project grade it self
		/// </summary>
		public float Grade { get; set; }

		/// <summary>
		/// Grade ranking compare to other grades
		/// </summary>
		public GradeRank Rank { get; set; }
	}

	public class GradeRank
	{
		/// <summary>
		/// Upper range for that grade
		/// </summary>
		public int Upper { get; set; }

		/// <summary>
		/// Lower range for that grade
		/// </summary>
		public int Lower { get; set; }

		/// <summary>
		/// Total grades in that comparison
		/// </summary>
		public int Total { get; set; }
	}

	public class CourseGradesViewModel
	{
		/// <summary>
		/// All grades in that course instace
		/// </summary>
		public List<GradeViewModel> Grades { get; set; }

		/// <summary>
		/// Total weight of the final grade
		/// </summary>
		public int TotalWeightOfFinalGrade { get; set; }

		/// <summary>
		/// Final grade it self with ranking calculations
		/// </summary>
		public GradeViewModel FinalGrade { get; set; }
	}
}
