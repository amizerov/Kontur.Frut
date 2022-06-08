﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Frut1Cv2
{

	[Table("Товары")]
	public class Товар
	{
		[Key]
		public int ID { get; set; }
		public int CAR_ID { get; set; }
		public int VAG_ID { get; set; }
		public string? Номенклатура { get; set; }
		public double Сумма { get; set; }
		public double СуммаРастоможки { get; set; }
		public double СуммаУслуги { get; set; }
		public double СуммаСкидки { get; set; }
		public double Курс { get; set; }
		public double Штраф { get; set; }
		public double ИтогоПоКассе { get; set; }

		public void Load(dynamic t, int car_id, int vag_id)
		{
			CAR_ID = car_id;
			VAG_ID = vag_id;
			Курс = vag_id > 0 ? t.Курс : 0;
			Сумма = t.Сумма;
			Номенклатура = t.Номенклатура.Наименование;
			СуммаРастоможки = t.СуммаРастоможки;
			СуммаУслуги = t.СуммаУслуги;
			СуммаСкидки = t.СуммаСкидки;
			Штраф = t.Штраф;
			ИтогоПоКассе = t.ИтогоПоКассе;

			using (FrutDB db = new FrutDB()) //Создание подключения
			{
				db.Товары!.Add(this);
				db.SaveChanges();
			}
		}
	}
}
