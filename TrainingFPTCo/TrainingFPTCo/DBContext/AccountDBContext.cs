using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace TrainingFPTCo.DBContext
{
	public class AccountDBContext
	{
		// Define the properties to represent the fields in the Users table
		[Key]
		public int Id { get; set; }

		[Column("RoleId"), Required]
		public int RoleId { get; set; }

		[ForeignKey("RoleId")]
		public RoleDBContext Role { get; set; }

		[Column("Username", TypeName = "nvarchar(100)"), Required]
		public string Username { get; set; }

		[Column("Password", TypeName = "nvarchar(100)"), Required]
		public string Password { get; set; }

		[Column("ExtraCode", TypeName = "nvarchar(100)")]
		public string ExtraCode { get; set; }

		[Column("Email", TypeName = "nvarchar(100)"), Required]
		public string Email { get; set; }

		[Column("Phone", TypeName = "nvarchar(20)"), Required]
		public string Phone { get; set; }

		[Column("Address", TypeName = "nvarchar(200)")]
		public string Address { get; set; }

		[Column("FullName", TypeName = "nvarchar(100)"), Required]
		public string FullName { get; set; }

		[Column("FirstName", TypeName = "nvarchar(50)")]
		public string FirstName { get; set; }

		[Column("LastName", TypeName = "nvarchar(50)")]
		public string LastName { get; set; }

		[Column("Birthday"), Required]
		public DateTime Birthday { get; set; }

		[Column("Gender", TypeName = "nvarchar(10)")]
		public string Gender { get; set; }

		[Column("Education", TypeName = "nvarchar(100)")]
		public string Education { get; set; }

		[Column("ProgramingLanguage", TypeName = "nvarchar(100)")]
		public string ProgramingLanguage { get; set; }

		[Column("ToeicScore")]
		public int? ToeicScore { get; set; }

		[Column("Skill", TypeName = "nvarchar(100)")]
		public string Skill { get; set; }

		[Column("IpClient", TypeName = "nvarchar(100)")]
		public string IpClient { get; set; }

		[Column("LastLogin")]
		public DateTime? LastLogin { get; set; }

		[Column("LastLogout")]
		public DateTime? LastLogout { get; set; }

		[AllowNull]
		public DateTime? CreatedAt { get; set; }

		[AllowNull]
		public DateTime? UpdatedAt { get; set; }

		[AllowNull]
		public DateTime? DeletedAt { get; set; }
	}
}
