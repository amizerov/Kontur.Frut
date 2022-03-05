using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Frut1Cv1
{
	[Table("Машины")]
	public class Машина
    {
		[Key]
		public int ID { get; set; }
		public string Номер { get; set; }
		public DateTime Дата { get; set; }
		public string Посредник { get; set; }
		public string Контракт { get; set; }
		public string Пользователь { get; set; }
		public string Терминал { get; set; }
		public string НомерМашины { get; set; }
		public string ДТ { get; set; }
		public string ТаможеннаяДекларация { get; set; }
		public string НомерИнвойса { get; set; }
		public string Касса { get; set; }
		public DateTime ДатаДТ { get; set; }
		public string Комментарий { get; set; }
		public string ТипРасчетаПоРубКассе { get; set; }
		public double СуммаСЗП { get; set; }
		public string ТипРасчетаПоДолларКассе { get; set; }
		public string Организация { get; set; }
		public string НоменклатураВДокумента { get; set; }
		public string КраткаяИнформация { get; set; }

		public void Save()
		{
			Машина m = this;
			using (FrutDB db = new FrutDB()) //Создание подключения
			{
				if (!db.Машины.Any(м => м.Номер == Номер))
				{
					db.Машины.Add(m);
					db.SaveChanges();
				}
			}
		}
	}

	[Table("Товары")]
	public class Товар
    {
		[Key]
		public int ID { get; set; }
		public int CAR_ID { get; set; }
		public string Номенклатура { get; set; }
		public double Сумма { get; set; }
		public double СуммаРастоможки { get; set; }
		public double СуммаУслуги { get; set; }
		public double СуммаСкидки { get; set; }
		public double Штраф { get; set; }
		public double ИтогоПоКассе { get; set; }

		public void Save()
		{
			using (FrutDB db = new FrutDB()) //Создание подключения
			{
				db.Товары.Add(this);
				db.SaveChanges();
			}
		}
	}
}
