#define INSERT_DATA 
//#define LOG_SQL
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AutoMapper;
using DataGenerator.Extensions;
using DataGenerator.Scheme;
using Configuration=DataGenerator.MappingConfiguration.Configuration;

namespace DataGenerator
{
	class Program
	{
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
			przedstawiclestwa.Each(p => p.Lokalizacja = db.Lokalizacjas.Random());
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
			firmy.Each(f => f.Lokalizacja = db.Lokalizacjas.Random());
			db.Firmas.InsertAllOnSubmit(firmy);
			db.SubmitChanges();

			var kategorie = Mapper.Map<IEnumerable<string>, IEnumerable<Kategoria_Czesci>>(RootEntityNames.Kategorie);
			db.Kategoria_Czescis.InsertAllOnSubmit(kategorie);
			db.SubmitChanges();

			var czesci = new List<Czesc_Zamienna>();

			var oleje = Mapper.Map<IEnumerable<string>, IEnumerable<Czesc_Zamienna>>(RootEntityNames.Oleje);
			oleje.Each(olej => olej.Kategoria_Czesci = db.Kategoria_Czescis.Where(kc => kc.Nazwa.StartsWith("Olej")).FirstOrDefault());
			oleje.Each(czesci.Add);
						
			var klocki = Mapper.Map<IEnumerable<string>, IEnumerable<Czesc_Zamienna>>(RootEntityNames.Klocki);
			klocki.Each(kl => kl.Kategoria_Czesci = db.Kategoria_Czescis.Where(kc => kc.Nazwa.StartsWith("Układ hamulcowy")).FirstOrDefault());
			klocki.Each(czesci.Add);

			var filtry = Mapper.Map<IEnumerable<string>, IEnumerable<Czesc_Zamienna>>(RootEntityNames.Filtry);
			filtry.Each(
				fl => fl.Kategoria_Czesci = db.Kategoria_Czescis.Where(kc => kc.Nazwa.StartsWith("Silnik")).FirstOrDefault());
			filtry.Each(czesci.Add);

			czesci.Each(cz => cz.Model = db.Models.Random());
			db.Czesc_Zamiennas.InsertAllOnSubmit(czesci);
			db.SubmitChanges();
			
			// zamowienia
			var dateRange = new DateRange(new DateTime(2007, 1, 1), new DateTime(2009, 11, 30));

			dateRange.MonthRange()
				.Each(month =>
                    	{
							for (int i = 0; i < 100.Random(10); ++i)
							{
								var data = db.FindDate(month.RandomDayOfMonth());
								var zamowienie = new Zamowienie
								                 	{
								                 		Czas_Dostarczenia_Do_Magazynu = 10.Random(2),
								                 		Czas_Realizacji = 10.Random(1),
								                 		Czesc_Zamienna = db.Czesc_Zamiennas.Random(),
								                 		Data = data,
								                 		Dostawca = db.Dostawcas.Random(),
								                 		Koszt_Obslugi = 100000.Random(10000)/100M,
								                 		Magazyn = db.Magazyns.Random(),
								                 		Przedstawicielstwo = db.Przedstawicielstwos.Random(),
								                 		Wielkosc = 100.Random(10),
								                 		Zysk = 100000.Random(10000)/100M,
								                 		Wartosc_Zamowienia = 200000.Random(10000)/100M
								                 	};
								db.Zamowienies.InsertOnSubmit(zamowienie);
								db.SubmitChanges();
							}
                    	});

			// reklamacje

			dateRange.MonthRange()
				.Each(month =>
				      	{
				      		for(int i=0;i<50.Random(5);++i)
				      		{
				      			var data = db.FindDate(month.RandomDayOfMonth());
				      			var reklamacja = new Reklamacja
				      			                 	{
				      			                 		Czas_Obslugi_Reklamacji = 20.Random(1),
														Czesc_Zamienna = db.Czesc_Zamiennas.Random(),
														Data1 = data,
														Liczba_Reklamacji = 10.Random(4),
                                                        Koszt_Obslugi_Reklamacji= 10000.Random(1000)/100M,
                                                        Przedstawicielstwo = db.Przedstawicielstwos.Random()
				      			                 	};
								db.Reklamacjas.InsertOnSubmit(reklamacja);
				      			db.SubmitChanges();
				      		}
				      	});


			// sprzedaz
			dateRange.MonthRange()
				.Each(month =>
				      	{
				      		var data = db.FindDate(month.Date);
				      		var sprzedaz = new Sprzedaz
				      		               	{
				      		               		Data = data,
				      		               		Firma = db.Firmas.Random(),
				      		               		Kategoria_Czesci = db.Kategoria_Czescis.Random(),
				      		               		Wielkosc_sprzedazy = (500000).Random(400000)
				      		               	};
							db.Sprzedazs.InsertOnSubmit(sprzedaz);
				      		db.SubmitChanges();
				      	});
#endif

			Console.ReadKey();
		}
	}
}
