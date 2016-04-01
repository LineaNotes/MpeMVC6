using System;
using System.ComponentModel.DataAnnotations;

namespace WebApplication3.Models
{
	public class GasPrice
	{
		public int Id { get; set; }

		[Required]
		[Display(Name = "Datum vnosa")]
		[DataType(DataType.DateTime)]
		[DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd.MM.yyyy}")]
		public System.DateTime GDate { get; set; } = DateTime.UtcNow;

		[Required]
		[Display(Name = "Cena")]
		[Range(typeof(Decimal), "0.1", "9999")]
		public decimal GPrice { get; set; }
	}
}