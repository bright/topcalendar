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
			var db = new HdDataContext {Log = new Log4NetWriter(typeof (Program))};
            
			db.DeleteAllAndSubmit(db.Czesc_Zamiennas);
			db.DeleteAllAndSubmit(db.Kategoria_Czescis);
			db.DeleteAllAndSubmit(db.Firmas);
			db.DeleteAllAndSubmit(db.Magazyns);
			db.DeleteAllAndSubmit(db.Lokalizacjas);
			db.DeleteAllAndSubmit(db.Dostawcas);
			db.DeleteAllAndSubmit(db.Models);

			var krajs = Mapper.Map<IEnumerable<string>, IEnumerable<Lokalizacja>>(RootEntityNames.Lokalizacje);
			db.Lokalizacjas.InsertAllOnSubmit(krajs);
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


			Console.ReadKey();
		}
	}
}
