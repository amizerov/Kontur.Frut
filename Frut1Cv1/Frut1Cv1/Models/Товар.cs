using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Frut1Cv1
{

	[Table("Товары")]
	public class Товар
	{
		[Key]
		public int ID { get; set; }
		public int CAR_ID { get; set; }
		public int VAG_ID { get; set; }
		public string Номенклатура { get; set; }
		public double Сумма { get; set; }
		public double СуммаРастоможки { get; set; }
		public double СуммаУслуги { get; set; }
		public double СуммаСкидки { get; set; }
		public double Штраф { get; set; }
		public double ИтогоПоКассе { get; set; }

		public Товар(dynamic t, int car_id, int vag_id)
		{
			CAR_ID = car_id;
			VAG_ID = vag_id;
			Сумма = t.Сумма;
			Номенклатура = t.Номенклатура.Наименование;
			СуммаРастоможки = t.СуммаРастоможки;
			СуммаУслуги = t.СуммаУслуги;
			СуммаСкидки = t.СуммаСкидки;
			Штраф = t.Штраф;
			ИтогоПоКассе = t.ИтогоПоКассе;

			using (FrutDB db = new FrutDB()) //Создание подключения
			{
				db.Товары.Add(this);
				db.SaveChanges();
			}
		}
	}
}
