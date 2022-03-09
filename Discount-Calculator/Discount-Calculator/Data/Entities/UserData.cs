using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Discount.Entities
{
	[Table("UserData")]
	public class UserData
	{
		[Key]
		public int UserID { get; set; }
		public string UserName { get; set; }
		public string Password { get; set; }
		public bool isActive { get; set; }
		public string CreatedBy { get; set; }
		[Required]
		public DateTime CreatedTS { get; set; }
	}
	}
