using am.BL;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Frut1Cv2
{
	[Table("Машины")]
	public class Машина
    {
		[Key]
		public int ID { get; set; }
		public string? Номер { get; set; }
		public DateTime Дата { get; set; }
		public string? Посредник { get; set; }
		public string? Контракт { get; set; }
		public string? Пользователь { get; set; }
		public string? Терминал { get; set; }
		public string? НомерМашины { get; set; }
		public string? ДТ { get; set; }
		public string? ТаможеннаяДекларация { get; set; }
		public string? НомерИнвойса { get; set; }
		public string? Касса { get; set; }
		public DateTime ДатаДТ { get; set; }
		public string? Комментарий { get; set; }
		public string? ТипРасчетаПоРубКассе { get; set; }
		public double СуммаСЗП { get; set; }
		public string? ТипРасчетаПоДолларКассе { get; set; }
		public string? Организация { get; set; }
		public string? НоменклатураВДокумента { get; set; }
		public string? КраткаяИнформация { get; set; }
		public double Курс { get; set; }

		public void Load(dynamic машина)
        {
			Номер = машина.Номер;
			Дата = машина.Дата;
			Посредник = машина.Посредник.Наименование;
			Контракт = машина.Контракт.Наименование;
			Пользователь = машина.Пользователь.Наименование;
			Терминал = машина.Терминал.Наименование;
			НомерМашины = машина.НомерМашины;
			ДТ = машина.ДТ;
			ТаможеннаяДекларация = машина.ТаможеннаяДекларация;
			НомерИнвойса = машина.НомерИнвойса;
			Касса = машина.Касса.Наименование;
			ДатаДТ = машина.ДатаДТ < DateTime.MinValue ? DateTime.MinValue : машина.ДатаДТ;
			Комментарий = машина.Комментарий;
			ТипРасчетаПоРубКассе = машина.ТипРасчетаПоРубКассе;
			СуммаСЗП = машина.СуммаСЗП;
			ТипРасчетаПоДолларКассе = машина.ТипРасчетаПоДолларКассе;
			Организация = машина.Организация.Наименование;
			НоменклатураВДокумента = машина.НоменклатураВДокумента;
			КраткаяИнформация = машина.КраткаяИнформация;

			var s = КраткаяИнформация;
			var i = s.IndexOf("КУРС: ") + 6;
			if (i > 20)
			{
				var j = s.IndexOf(',', i);
				if (j > i)
				{
					s = s.Substring(i, j - i + 3);
					Курс = G._Double(s);
				}
			}
		}
		public void Save()
		{
			using (FrutDB db = new FrutDB()) //Создание подключения
			{
				try
				{
					if (!db.Машины!.Any(м => м.Номер == Номер))
					{
						db.Машины!.Add(this);
						db.SaveChanges();
					}
				}
				catch (Exception ex)
                {
					Console.WriteLine(ex.ToString());
                }
			}
		}
	}
}
