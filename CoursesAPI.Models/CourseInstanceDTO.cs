namespace CoursesAPI.Models
{
	/// <summary>
	/// 
	/// </summary>
	public class CourseInstanceDTO
	{
		/// <summary>
		/// A database-generated ID.
		/// </summary>
		public int    CourseInstanceID { get; set; }

		/// <summary>
		/// A human-readable ID of the course. Example: "T-514-VEFT".
		/// </summary>
		public string CourseID         { get; set; }

		/// <summary>
		/// The name of the course. Example: "Vefþjónustur".
		/// </summary>
		public string Name             { get; set; }

		/// <summary>
		/// The name of the main teacher of that course
		/// </summary>
		public string MainTeacher      { get; set; }
	}
}
