using System;
using System.Threading.Tasks;

namespace Frut1Cv1
{
    public class Loader
    {
		public static event Action<string> OnProgress;
		public static event Action<string> OnComplete;
		public static event Action<string> OnError;

		public static void Start()
        {
			Task.Run(DoWork);
        }
		static void DoWork()
		{
			try	{ LoadVagons(); }
			catch (Exception ex) { OnError("Вагон ошибка: " + ex.ToString()); }
			try { LoadMashin(); }
			catch (Exception ex) { OnError("Машина ошибка: " + ex.ToString()); }
            try { LoadOplata(); }
			catch (Exception ex) { OnError("Оплата ошибка: " + ex.ToString()); }

			OnComplete("done");
		}
	
		private static void LoadOplata()
        {
			DateTime date = DateTime.Now;

			var оплата = Com1C.База1С.Документы.Оплата.Выбрать(date.AddDays(-10), date);
			var взаиморасчеты = Com1C.База1С.РегистрыНакопления;
			
			while (оплата?.Следующий() ?? false)
			{
				try
				{
					Оплата o = new Оплата(оплата);
					o.Save();

					OnProgress($"Оплата #{o.Номер} - {o.Дата.ToString("dd MMMM hh:mm")} " + (o.ID > 0 ? "загружена" : "пропущена"));
				}
				catch (Exception ex) { OnError("Оплата 1 ошибка: " + ex.ToString()); }
			}

		}

		private static void LoadMashin()
        {
			DateTime date = DateTime.Now;

			var машина = Com1C.База1С.Документы.Машины.Выбрать(date.AddDays(-10), date);

			while (машина?.Следующий() ?? false)
			{
				try { 
					Машина m = new Машина(машина);
					m.Save();

					OnProgress($"Машина #{m.Номер} - {m.Дата.ToString("dd MMMM hh:mm")} " + (m.ID > 0 ? "загружена" : "пропущена"));

					if (m.ID > 0)
					{
						var tov = машина.товары;
						LoadTovars(tov, m.ID, 0);
					}
				}
				catch (Exception ex) { OnError("Машина 1 ошибка: " + ex.ToString()); }
			}
		}

		private static void LoadVagons()
        {
			DateTime date = DateTime.Now;

			var вагон = Com1C.База1С.Документы.Вагоны.Выбрать(date.AddDays(-100), date);

			while (вагон?.Следующий() ?? false)
			{
				try
				{
					Вагон v = new Вагон(вагон);
					v.Save();

					OnProgress($"Вагон #{v.Номер} - {v.Дата.ToString("dd MMMM hh:mm")} " + (v.ID > 0 ? "загружен" : "пропущен"));

					if (v.ID == 0) continue;

					var tov = вагон.товары;
					LoadTovars(tov, 0, v.ID);

				}
				catch (Exception ex) { OnError("Вагон 1 ошибка: " + ex.ToString()); }
			}
		}

		static void LoadTovars(dynamic tov, int car_id, int vag_id)
        {
			foreach (var tt in tov)
			{
				try
				{
					Товар t = new Товар(tt, car_id, vag_id);
					OnProgress($"Товар {t.Номенклатура} загружен");
				}
				catch (Exception ex) {
					OnError($"Ошибка загрузки товара: " + ex.ToString()); 
				}
			}
		}
	}
}
