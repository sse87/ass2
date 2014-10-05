using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoursesAPI.Models
{
	public class GradeDTO
	{
		public string studentName { get; set; }
		public double grade { get; set; }
		public string projectName { get; set; }
		public string courseName { get; set; }
	}
}
