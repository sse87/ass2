using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoursesAPI.Models
{
	public class GradeToProjectViewModel
	{
		/// <summary>
		/// The SSN of the person.
		/// </summary>
		public string SSN { get; set; }

		/// <summary>
		/// The grade it self of that project by the person, can be 2.7 or 11.3, no limit on decimal or maximum
		/// </summary>
		public float grade { get; set; }
	}
}
