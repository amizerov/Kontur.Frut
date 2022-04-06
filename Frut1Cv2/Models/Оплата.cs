using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Frut1Cv2
{
	[Table("Оплаты")]
	public class Оплата
    {
		[Key]
		public int ID { get; set; }
		public int Номер { get; set; }
		public DateTime Дата { get; set; }
		public double Сумма { get; set; }
		public string? Касса { get; set; }
		public string? Посредник { get; set; }
		public string? Организация { get; set; }
		public string? Контракт { get; set; }
		public string? НаименованиеФирмы { get; set; }
		public string? Пользователь { get; set; }
		public bool ПометкаУдаления { get; set; }
		public bool Проведен { get; set; }

		public void Load(dynamic оплата)
        {
			Номер = int.Parse(оплата.Номер);
			Дата = оплата.Дата;
			Сумма = оплата.Сумма;
			Касса = оплата.Касса.Наименование;
			Посредник = оплата.Посредник.Наименование;
			Организация = оплата.Организация.Наименование;
			Контракт = оплата.Контракт.Наименование;
			НаименованиеФирмы = оплата.НаименованиеФирмы.Наименование;
			Пользователь = оплата.Пользователь.Наименование;
			ПометкаУдаления = оплата.ПометкаУдаления;
			Проведен = оплата.Проведен;
		}
		public void Save()
		{
			using (FrutDB db = new FrutDB()) //Создание подключения
			{
				try
				{
					if (!db.Оплаты.Where(o => o.Номер == Номер).Any())
					{
						db.Оплаты.Add(this);
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
