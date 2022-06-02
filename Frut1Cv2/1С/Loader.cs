using System;
using System.Threading.Tasks;

namespace Frut1Cv2
{
    public class Loader
    {
		public static event Action<string, int>? OnProgress;
		public static event Action<string>? OnComplete;
		public static event Action<string>? OnError;

		public static void Start()
        {
			Task.Run(DoWork);
        }
		static void DoWork()
		{
			OnProgress?.Invoke("Грузим остатки", 0);
			Остатки.Load();
			OnProgress?.Invoke("Ок", 0);
			Thread.Sleep(1000);
			OnProgress?.Invoke("Грузим операции", 0);
			Операта.Load();
			OnProgress?.Invoke("Ок", 0);

			try { LoadOplata(); }
			catch (Exception ex) { OnError?.Invoke("Оплата ошибка: " + ex.ToString()); }
			try { LoadVagons(); }
			catch (Exception ex) { OnError?.Invoke("Вагон ошибка: " + ex.ToString()); }
			try { LoadMashin(); }
			catch (Exception ex) { OnError?.Invoke("Машина ошибка: " + ex.ToString()); }

			OnComplete?.Invoke("done");
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
					Оплата o = new Оплата();
					o.Load(оплата);
					o.Save();

					int i = (o.ID > 0 ? 1 : 0);
					string s = (o.ID > 0 ? "загружена" : "пропущена");
					OnProgress?.Invoke($"Оплата #{o.Номер} - {o.Дата.ToString("dd MMMM hh:mm")} " + s, i);
				}
				catch (Exception ex) { OnError?.Invoke("Оплата 1 ошибка: " + ex.ToString()); }
			}
		}

		static void MashinaError(string msg) => OnError?.Invoke(msg);
		private static void LoadMashin()
        {
			DateTime date = DateTime.Now;

			var машина = Com1C.База1С.Документы.Машины.Выбрать(date.AddDays(-10), date);

			while (машина?.Следующий() ?? false)
			{
				try { 
					Машина m = new Машина();
					m.OnError += MashinaError;
					m.Load(машина);
					m.Save();

					int i = (m.ID > 0 ? 1 : 0);
					string s = (m.ID > 0 ? "загружена" : "пропущена");
					OnProgress?.Invoke($"Машина #{m.Номер} - {m.Дата.ToString("dd MMMM hh:mm")} " + s, i);

					if (m.ID > 0)
					{
						var tov = машина?.товары;
						LoadTovars(tov, m.ID, 0);
					}
				}
				catch (Exception ex) { OnError?.Invoke("Машина 1 ошибка: " + ex.ToString()); }
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
					Вагон v = new Вагон();
					v.Load(вагон);
					v.Save();

					int i = (v.ID > 0 ? 1 : 0);
					string s = (v.ID > 0 ? "загружен" : "пропущен");
					OnProgress?.Invoke($"Вагон #{v.Номер} - {v.Дата.ToString("dd MMMM hh:mm")} " + s, i);

					if (v.ID > 0)
					{
						var tov = вагон?.товары;
						LoadTovars(tov, 0, v.ID);
					}
				}
				catch (Exception ex) { OnError?.Invoke("Вагон 1 ошибка: " + ex.ToString()); }
			}
		}

		static void LoadTovars(dynamic tov, int car_id, int vag_id)
        {
			foreach (var tt in tov)
			{
				try
				{
					Товар t = new Товар();
					t.Load(tt, car_id, vag_id);

					int i = (t.ID > 0 ? 1 : 0);
					string s = (t.ID > 0 ? "загружен" : "пропущен"); 
					OnProgress?.Invoke($"Товар {t.Номенклатура} " + s, i);
				}
				catch (Exception ex) {
					OnError?.Invoke($"Ошибка загрузки товара: " + ex.ToString()); 
				}
			}
		}
	}
}
