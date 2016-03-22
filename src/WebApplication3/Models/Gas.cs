using System.ComponentModel.DataAnnotations;

namespace WebApplication3.Models
{
	public class Gas
	{
		public int Id { get; set; }

		[Display(Name = "Datum vnosa")]
		[DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd.MM.yyyy}")]
		public System.DateTime datum { get; set; }

		[Display(Name = "Prvi vnos")]
		[DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:HH:mm}")]
		public System.DateTime ura_zacetnega_vpisa { get; set; }

		[Display(Name = "Zadnji vnos")]
		[DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:HH:mm}")]
		public System.DateTime ura_koncnega_vpisa { get; set; }

		public int zacetno_stanje { get; set; }
		public int koncno_stanje { get; set; }

		[Display(Name = "Proizvedeno")]
		public float proizvedena_kolicina { get; set; }

		public int zacetno_stanje_cela { get; set; }
		public int zacetno_stanje_cifra { get; set; }
		public int koncno_stanje_cela { get; set; }
		public int koncno_stanje_cifra { get; set; }
	}
}