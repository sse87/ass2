using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoursesAPI.Models
{
	public class ProjectGroupViewModel
	{
		/// <summary>
		/// Name of the project group
		/// </summary>
		public string Name { get; set; }

		/// <summary>
		/// Count of project that will be in the final grade calculations. For example only take 5 of the 9 weekly assignments or quiz
		/// </summary>
		public int GradedProjectCount { get; set; }
	}
}
