using System.ComponentModel.DataAnnotations;

namespace App3.Data
{
	public class Setting
	{
		[Key]
		public string Key { get; set; }
		public string Value { get; set; }
	}
}