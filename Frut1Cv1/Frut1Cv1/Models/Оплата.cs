using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Frut1Cv1
{
	[Table("Оплаты")]
	public class Оплата
    {
		[Key]
		public int ID { get; set; }
		public int Номер { get; set; }
		public DateTime Дата { get; set; }
		public double Сумма { get; set; }
		public string Касса { get; set; }
		public string Посредник { get; set; }
		public string Организация { get; set; }
		public string Контракт { get; set; }
		public string НаименованиеФирмы { get; set; }
		public string Пользователь { get; set; }
		public bool ПометкаУдаления { get; set; }
		public bool Проведен { get; set; }

		public void Save()
		{
			using (FrutDB db = new FrutDB()) //Создание подключения
			{
				if (!db.Оплаты.Any(o => o.Номер == Номер))
				{
					db.Оплаты.Add(this);
					db.SaveChanges();
				}
			}
		}
	}
}
