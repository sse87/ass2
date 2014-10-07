using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;

namespace CoursesAPI.Services.Models.Entities
{
	[Table("Grades")]
	public class Grade
	{
		/// <summary>
		/// A database-generated ID.
		/// </summary>
		public int ID { get; set; }

		/// <summary>
		/// Foreign key to the SSN of the person.
		/// </summary>
		public string SSN { get; set; }

		/// <summary>
		/// Foreign key to a database-generated ID of the project
		/// </summary>
		public int ProjectID { get; set; }

		/// <summary>
		/// The grade it self of that project by the person, can be 2.7 or 11.3, no limit on decimal or maximum
		/// </summary>
		public double ProjectGrade { get; set; }
	}
}
