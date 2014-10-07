using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoursesAPI.Models
{
	public class GradeViewModel
	{
		public string CourseName { get; set; }
		public string ProjectName { get; set; }
		public float Grade { get; set; }
		public GradeRank Rank { get; set; }
	}

	public class GradeRank
	{
		public int Upper { get; set; }
		public int Lower { get; set; }
		public int Total { get; set; }
	}

	public class CourseGradesViewModel
	{
		public List<GradeViewModel> Grades { get; set; }
		public GradeViewModel FinalGrade { get; set; }
	}
}
