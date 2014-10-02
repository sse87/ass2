using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;

namespace CoursesAPI.Services.Models.Entities
{
	[Table("ProjectGroups")]
	public class ProjectGroup
	{
		public int ID { get; set; }
		public string Name { get; set; }
		public int GradedProjectCount { get; set; }
	}
}
