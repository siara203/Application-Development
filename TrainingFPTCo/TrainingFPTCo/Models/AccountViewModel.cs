using System;
using System.ComponentModel.DataAnnotations;

namespace TrainingFPTCo.Models
{
	public class AccountViewModel
	{
		public List<AccountDetail> AccountDetailsList { get; set; }
	}
	public class AccountDetail
		{
			public int Id { get; set; }
			public int RoleId { get; set; }
			public string? ViewRoleName { get; set; }

			[Required(ErrorMessage = "Enter name's course, please")]
			public string Username { get; set; }
			public string Password { get; set; }
			public string ExtraCode { get; set; }
			public string Email { get; set; }
			public string Phone { get; set; }
			public string?	Address { get; set; }
			public string FullName { get; set; }
			public string? FirstName { get; set; }
			public string? LastName { get; set; }
			public DateTime Birthday { get; set; }
			[DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
			public string? Gender { get; set; }
			public string? Education { get; set; }
			public string? ProgrammingLanguage { get; set; }
			public int? ToeicScore { get; set; }
			public string? Skill { get; set; }
			public string? IpClient { get; set; }
			public DateTime? LastLogin { get; set; }
			[DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
			public DateTime? LastLogout { get; set; }
			[DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
			public DateTime? CreatedAt { get; set; }
			public DateTime? UpdatedAt { get; set; }
			public DateTime? DeletedAt { get; set; } 
	}
}
