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
		public int ID { get; set; }
		public string SSN { get; set; }
		public int ProjectID { get; set; }
		public double ProjectGrade { get; set; }
	}
}
