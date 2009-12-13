#define INSERT_DATA 
//#define LOG_SQL
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AutoMapper;
using DataGenerator.Extensions;
using DataGenerator.Scheme;
using MathNet.Numerics.Distributions;
using MathNet.Numerics.RandomSources;
using Configuration=DataGenerator.MappingConfiguration.Configuration;

namespace DataGenerator
{
	

	class Program
	{
		public static int NumberOfRowsInFactTable = 10000;
		static DateRange dateRange = new DateRange(new DateTime(2007, 1, 1), new DateTime(2009, 11, 30));

		static void Main(string[] args)
		{
			var mappingConfiguration = new Configuration();
			mappingConfiguration.CreateMappings(typeof(Program).Assembly);
			var db = new HdDataContext
			         	{
#if LOG_SQL
			         		Log = new Log4NetWriter(typeof (Program))
#endif
			         	};
						
            db.DeleteAllAndSubmit(db.Zamowienies);
			db.DeleteAllAndSubmit(db.Sprzedazs);
			db.DeleteAllAndSubmit(db.Reklamacjas);
			
			db.DeleteAllAndSubmit(db.Czesc_Zamiennas);
			db.DeleteAllAndSubmit(db.Kategoria_Czescis);
			db.DeleteAllAndSubmit(db.Firmas);
			db.DeleteAllAndSubmit(db.Magazyns);
			db.DeleteAllAndSubmit(db.Przedstawicielstwos);
			db.DeleteAllAndSubmit(db.Lokalizacjas);
			db.DeleteAllAndSubmit(db.Dostawcas);
			db.DeleteAllAndSubmit(db.Models);
			
#if INSERT_DATA			

			var krajs = Mapper.Map<IEnumerable<string>, IEnumerable<Lokalizacja>>(RootEntityNames.Lokalizacje);
			db.Lokalizacjas.InsertAllOnSubmit(krajs);
			db.SubmitChanges();

			var przedstawiclestwa =
				Mapper.Map<IEnumerable<string>, IEnumerable<Przedstawicielstwo>>(RootEntityNames.Przedstawicielstwa);
			przedstawiclestwa.Each(p => p.Lokalizacja = db.Lokalizacjas.NormalDistribution());
			db.Przedstawicielstwos.InsertAllOnSubmit(przedstawiclestwa);
			db.SubmitChanges();

			var magazyns = Mapper.Map<IEnumerable<string>, IEnumerable<Magazyn>>(RootEntityNames.Magazyny);
			int magazynIndex = 0;			
			magazyns.Each(magazyn =>
			              	{
			              		var lokalizacje = db.Lokalizacjas.ToList();
			              		magazyn.Lokalizacja = lokalizacje[magazynIndex];
			              		magazynIndex++;
			              	});
			db.Magazyns.InsertAllOnSubmit(magazyns);
			
			var dostawcy = Mapper.Map<IEnumerable<string>, IEnumerable<Dostawca>>(RootEntityNames.Dostawcy);
			db.Dostawcas.InsertAllOnSubmit(dostawcy);

			var modele = Mapper.Map<IEnumerable<string>, IEnumerable<Model>>(RootEntityNames.Modele);
			db.Models.InsertAllOnSubmit(modele);
			db.SubmitChanges();

			var firmy = Mapper.Map<IEnumerable<string>, IEnumerable<Firma>>(RootEntityNames.Firmy);
			firmy.Each(f => f.Lokalizacja = db.Lokalizacjas.NormalDistribution());
			db.Firmas.InsertAllOnSubmit(firmy);
			db.SubmitChanges();

			var kategorie = Mapper.Map<IEnumerable<string>, IEnumerable<Kategoria_Czesci>>(RootEntityNames.Kategorie);
			db.Kategoria_Czescis.InsertAllOnSubmit(kategorie);
			db.SubmitChanges();

			var czesci = new List<Czesc_Zamienna>();

			var oleje = Mapper.Map<IEnumerable<string>, IEnumerable<Czesc_Zamienna>>(RootEntityNames.Oleje);
			var kategoriaOlejow = db.Kategoria_Czescis.Where(kc => kc.Nazwa.StartsWith("Olej")).FirstOrDefault();
			oleje.Each(olej => olej.Kategoria_Czesci = kategoriaOlejow);
			oleje.Each(czesci.Add);
						
			var klocki = Mapper.Map<IEnumerable<string>, IEnumerable<Czesc_Zamienna>>(RootEntityNames.Klocki);
			var kategoriaKlockow = db.Kategoria_Czescis.Where(kc => kc.Nazwa.StartsWith("Układ hamulcowy")).FirstOrDefault();
			klocki.Each(kl => kl.Kategoria_Czesci = kategoriaKlockow);
			klocki.Each(czesci.Add);

			var filtry = Mapper.Map<IEnumerable<string>, IEnumerable<Czesc_Zamienna>>(RootEntityNames.Filtry);
			var kategoriaFiltrow = db.Kategoria_Czescis.Where(kc => kc.Nazwa.StartsWith("Silnik")).First();
			filtry.Each(fl => fl.Kategoria_Czesci = kategoriaFiltrow);
			filtry.Each(czesci.Add);

			var lozyska = Mapper.Map<IEnumerable<string>, IEnumerable<Czesc_Zamienna>>(RootEntityNames.Lozyska);
			var kategoriaLozyska = db.Kategoria_Czescis.Where(kc => kc.Nazwa.StartsWith("Korbowód")).First();
			lozyska.Each(loz => loz.Kategoria_Czesci = kategoriaLozyska);
			lozyska.Each(czesci.Add);

			var rozne = Mapper.Map<IEnumerable<string>, IEnumerable<Czesc_Zamienna>>(RootEntityNames.Rozne);
			var kategoriaOswietlenie = db.Kategoria_Czescis.Where(kc => kc.Nazwa.StartsWith("Światła")).First();
			rozne.Where(r => r.Nazwa.StartsWith("BEZ") || r.Nazwa.Contains("ZAROWKA")).Each( cz => cz.Kategoria_Czesci= kategoriaOswietlenie);
			var kategoriaWtryski = db.Kategoria_Czescis.Where(kc => kc.Nazwa.StartsWith("Układ wtryskowy")).First();
			rozne.Where(r => r.Nazwa.Contains("SWIECA")).Each(cz => cz.Kategoria_Czesci = kategoriaWtryski);
			var kategoriaChlodzeeni = db.Kategoria_Czescis.Where(kc => kc.Nazwa.StartsWith("Układ chłodzenia")).First();
			rozne.Where(r => r.Nazwa.Contains("SILIK")).Each(cz => cz.Kategoria_Czesci = kategoriaChlodzeeni);
			var kategoriaWydech = db.Kategoria_Czescis.Where(kc => kc.Nazwa.Contains("wydechowy")).First();
			rozne.Where(r => r.Nazwa.Contains("OPASKA")).Each(cz => cz.Kategoria_Czesci = kategoriaWydech);
			rozne.Each(czesci.Add);
			czesci.Each(cz => cz.Model = db.Models.NormalDistribution());
			db.Czesc_Zamiennas.InsertAllOnSubmit(czesci);
			db.SubmitChanges();
			
			// zamowienia
			
			var czasDostarczeniaDoMagazynuDistribition = new NormalDistribution(10, 5);
			
			DateTime[] dates = GetDates(NumberOfRowsInFactTable);			
			dates.Each(day =>
							{								
								var data = db.FindDate(day);
								var zam = new Zamowienie();
								zam.Czas_Dostarczenia_Do_Magazynu = (int) Math.Round(Math.Abs(czasDostarczeniaDoMagazynuDistribition.NextDouble()));
								zam.Czas_Realizacji = (int)Math.Round(Math.Abs(czasDostarczeniaDoMagazynuDistribition.NextDouble()));
								zam.Czesc_Zamienna = db.Czesc_Zamiennas.NormalDistribution();
								zam.Data = data;
								zam.Dostawca = db.Dostawcas.NormalDistribution();
								zam.Wielkosc = 100.Random(10);
								zam.Wartosc_Zamowienia = zam.Wielkosc*1000.Random(20);
								zam.Koszt_Obslugi = zam.Wartosc_Zamowienia/5.Random(2);
								zam.Zysk = zam.Wartosc_Zamowienia/3.Random(2) - zam.Wartosc_Zamowienia/10.Random(4);
								zam.Magazyn = db.Magazyns.NormalDistribution();
								zam.Przedstawicielstwo = db.Przedstawicielstwos.NormalDistribution();
								db.Zamowienies.InsertOnSubmit(zam);															
							});
			db.SubmitChanges();
			// reklamacje

			GetDates(NumberOfRowsInFactTable / 6)
				.Each(day =>
				      	{
				      		var data = db.FindDate(day);
				      		var reklamacja = new Reklamacja
				      		                 	{
				      		                 		Czas_Obslugi_Reklamacji = (int) Math.Round(Math.Abs(czasDostarczeniaDoMagazynuDistribition.NextDouble())),
				      		                 		Czesc_Zamienna = db.Czesc_Zamiennas.NormalDistribution(),
				      		                 		Data1 = data,
				      		                 		Liczba_Reklamacji = 10.Random(4),
				      		                 	};
				      		reklamacja.Koszt_Obslugi_Reklamacji = reklamacja.Liczba_Reklamacji*1000.Random(50)/2.Random(1);
				      		reklamacja.Przedstawicielstwo = db.Przedstawicielstwos.NormalDistribution();
				      			                 	
							db.Reklamacjas.InsertOnSubmit(reklamacja);			      							      		
				      	});
			db.SubmitChanges();

			// sprzedaz dla naszej firmy
			dateRange.MonthRange()
				.Each(day =>
				      	{
				      		var data = db.FindDate(day);
				      		db.Kategoria_Czescis.Each(kc =>
                          	{
                          		var sprzedaz = new Sprzedaz
                          		               	{
                          		               		Data = data,
                          		               		Firma = db.Firmas.First(),
                          		               		Kategoria_Czesci = kc,
													Wielkosc_sprzedazy = db.Zamowienies.Where(z => z.Data_Zamowienia.Value.CompareTo(day) >= 0 && z.Data_Zamowienia.Value.CompareTo(day.AddDays(DateTime.DaysInMonth(day.Year,day.Month)))<=0 && z.Czesc_Zamienna.Kategoria_Czesci.Equals(kc)).Sum(z => z.Wartosc_Zamowienia)
								};
								db.Sprzedazs.InsertOnSubmit(sprzedaz);
                          	});				      													      		
				      	});
			db.SubmitChanges();
			// sprzedaz dla pozostalych
			dateRange.MonthRange()
				.Each(day =>
				{
					var data = db.FindDate(day);
					db.Kategoria_Czescis.Each(kc =>
					{
						var wszystkie = db.Firmas.ToList();
						var pozostale = new List<Firma>();
						for (int i = 1; i < wszystkie.Count; ++i)
							pozostale.Add(wszystkie[i]);
						pozostale.Each(f =>
		               	{
							var sprzedaz = new Sprzedaz
							{
								Data = data,
								Firma = f,
								Kategoria_Czesci = kc,
								Wielkosc_sprzedazy = NumberOfRowsInFactTable*200.Random(50)
							};
							db.Sprzedazs.InsertOnSubmit(sprzedaz);
		               	});
												
					});					
				});
			db.SubmitChanges();
#endif

			Console.ReadKey();
		}

		private static DateTime[] GetDates(int count)
		{
			var dates = new DateTime[count];			
			var datesDistribution = new NormalDistribution(0,16);						
			var totalDayCount = (dateRange.End - dateRange.Start).TotalDays;
			for (int i = 0; i < count; ++i)
			{
				double random;
				do
				{
					random =  - Math.Abs(datesDistribution.NextDouble());
				} while (random < -10);
				
				var positive = random + 10;
				var dayNumber = positive/10*totalDayCount;
				dates[i] = dateRange.Start.Add(TimeSpan.FromDays(dayNumber)).Date;
			}
			return dates;
		}
	}
}
