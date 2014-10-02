using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoursesAPI.Services.Models.Entities
{
	/// <summary>
	/// An instance of this class represents the fact that a given
	/// person is a student in a given course instance.
	/// </summary>
	public class StudentRegistration
	{
		/// <summary>
		/// A database-generated ID of the course instance.
		/// </summary>
		public int    ID               { get; set; }

		/// <summary>
		/// The SSN of the person.
		/// </summary>
		public string SSN              { get; set; }

		/// <summary>
		/// The ID of the course instance the given student is studing
		/// </summary>
		public int    CourseInstanceID { get; set; }

		/// <summary>
		/// The type of teacher:
		/// 1: active student
		/// 2: shitty student
		/// 3: fucked up student
		/// </summary>
		public StudentRegistrationStatus Status { get; set; }



		/// <summary>
		/// The type of teacher:
		/// 1: active student
		/// 2: shitty student
		/// 3: fucked up student
		/// </summary>
		public enum StudentRegistrationStatus { Active = 1, Inactive = 2 }
	}
}
