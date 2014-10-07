using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoursesAPI.Models
{
	public class GradeDTO
	{
		/// <summary>
		/// The name of the student that is author of that project that is being graded
		/// </summary>
		public string studentName { get; set; }

		/// <summary>
		/// The grade it self of that project by the person, can be 2.7 or 11.3, no limit on decimal or maximum
		/// </summary>
		public float grade { get; set; }

		/// <summary>
		/// The name of the project that is currently graded
		/// </summary>
		public string projectName { get; set; }

		/// <summary>
		/// The name of the course that the project is part of
		/// </summary>
		public string courseName { get; set; }
	}
}
