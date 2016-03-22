using System.ComponentModel.DataAnnotations;

namespace WebApplication3.Models
{
	public class GasPrice
	{
		public int Id { get; set; }

		[Display(Name = "Datum vnosa")]
		[DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd.MM.yyyy}")]
		public System.DateTime GDate { get; set; }

		[Display(Name = "Cena")]
		public decimal GPrice { get; set; }
	}
}