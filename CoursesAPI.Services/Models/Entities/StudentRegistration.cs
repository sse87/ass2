using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoursesAPI.Services.Models.Entities
{
	public class StudentRegistration
	{
		public int    ID               { get; set; }
		public string SSN              { get; set; }
		public int    CourseInstanceID { get; set; }
		public int    Status           { get; set; }
	}
}
