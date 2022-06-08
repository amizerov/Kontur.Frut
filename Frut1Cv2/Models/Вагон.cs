using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Frut1Cv2
{
    [Table("Вагоны")]
    public class Вагон
    {
        [Key]
        public int ID { get; set; }
		public int Номер { get; set; }
		public DateTime Дата { get; set; }
        public string? Посредник { get; set; }
        public string? Контракт { get; set; }
        public string? Пользователь { get; set; }
        public string? Терминал { get; set; }
        public string? НомерВагона { get; set; }
        public bool ОплаченГДТ { get; set; }
        public bool Растоможен { get; set; }
        public string? ДТ { get; set; }
        public double ПроцентСтраховки { get; set; }
        public string? ТаможеннаяДекларация { get; set; }
        public string? НомерИнвойса { get; set; }
        public string? Касса { get; set; }
        public DateTime ДатаДТ { get; set; }
        public string? Комментарий { get; set; }
        public string? ТипРасчетаПоРубКассе { get; set; }
        public string? НомерСМР { get; set; }
        public string? Брокер { get; set; }
        public double СуммаСЗП { get; set; }
        public string? ТипРасчетаПоДолларКассе { get; set; }
        public string? Организация { get; set; }
        public bool ОплаченУслуга { get; set; }
        public string? НоменклатураВДокумента { get; set; }
        public double Курс { get; set; }

        public void Load(dynamic вагон)
        {
            Номер = int.Parse(вагон.Номер);
            Дата = вагон.Дата;
            Посредник = вагон.Посредник.Наименование;
            Контракт = вагон.Контракт.Наименование;
            Пользователь = вагон.Пользователь.Наименование;
            Терминал = вагон.Терминал.Наименование;
            НомерВагона = вагон.НомерВагона;
            ОплаченГДТ = вагон.ОплаченГДТ;
            Растоможен = вагон.Растоможен;
            ДТ = вагон.ДТ;
            ПроцентСтраховки = вагон.ПроцентСтраховки;
            ТаможеннаяДекларация = вагон.ТаможеннаяДекларация;
            НомерИнвойса = вагон.НомерИнвойса;
            Касса = вагон.Касса.Наименование;
            ДатаДТ = вагон.ДатаДТ < DateTime.MinValue ? DateTime.MinValue : вагон.ДатаДТ;
            Комментарий = вагон.Комментарий;
            ТипРасчетаПоРубКассе = вагон.ТипРасчетаПоРубКассе;
            НомерСМР = вагон.НомерСМР;
            Брокер = вагон.Брокер?.Наименование;
            СуммаСЗП = вагон.СуммаСЗП;
            ТипРасчетаПоДолларКассе = вагон.ТипРасчетаПоДолларКассе;
            Организация = вагон.Организация.Наименование;
            ОплаченУслуга = вагон.ОплаченУслуга;
            НоменклатураВДокумента = вагон.НоменклатураВДокумента;
            int i = 0; var tt = вагон.товары;
            foreach(var t in tt)
            {
                i++;
                Курс += t.Курс;
            }
            Курс = Курс / i;
        }
        public void Save()
        {
            using (FrutDB db = new FrutDB()) //Создание подключения
            {
                try
                {
                    if (!db.Вагоны!.Any(в => в.Номер == Номер))
                    {
                        db.Вагоны!.Add(this);
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
