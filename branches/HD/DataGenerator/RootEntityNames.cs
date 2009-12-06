using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using DataGenerator.Extensions;
using DataGenerator.Scheme;

namespace DataGenerator
{
	public static class RootEntityNames
	{
		
		public static IEnumerable<string> Magazyny
		{
			get
			{
				return new[]
				       	{
				       		"Szczecin CarParts",
				       		"Cardiff CarParts",
				       		"Lizbona CarParts",
				       		"Amsterdam CarParts"
				       	}; 
			}
		}

		public static IEnumerable<string> Lokalizacje
		{
			get
			{
				return new[]
				       	{
				       		"Polska, Szczecin",
				       		"Holandia, Amsterdam",
				       		"Hiszpania, Lizbona",
				       		"Wielka Brytania, Cardiff",
                            "Niemcy, Berlin",
                            "Francja, Lion",                            
                            "W³ochy, Turyn",
                            "Ukraina, Lwow",
                            "Litwa, Minsk",
                            "Czechy, Praga",                                                        
                            "Szwajcaria, Zurych"                            
				       	};
			}
		}

		public static IEnumerable<string> Dostawcy {
			get 
			{
				return new[] 
					{
						"Renault Inc.", 
						"BMW Fota Ltd.",
                        "Power of Engine",
                        "Star Ltd.",
                        "Auto Parts",
						"AutohausAZ"
					};
			}
		}

		public static IEnumerable<string> Modele
		{
			get
			{
				return new[]
				       	{
				       		"Opel Astra 1, 1400 Benzyna, 1992",
							"Opel Astra 1, 1400 Benzyna, 1994",
							"Opel Astra 1, 1600 Diesel, 1994",
							"Opel Astra 2, 1600 Diesel, 1998",
							"Opel Astra 3, 1800 Diesel, 2002",
                            "VW Golf 2, 1600 Benzyna, 1992",
							"VW Golf 3, 1700 Diesel, 1996"
						};
			}
		}

		public static IEnumerable<string> Firmy
		{
			get
			{
				return new[]
				       	{
				       		"Car Parts Ltd.",
	                        "WagenTeile",
                            "Part4U",
                            "AutoWiheister"
						};
			}
		}

		public static IEnumerable<string> Kategorie
		{
			get
			{
				return new[]
				       	{
                            "Olej silnikowy",
				       		"Uk³ad wtryskowy",
                            "Pompa wtryskowa",
                            "Korbowód",
                            "Uk³ad hamulcowy",
                            "Silnik",
                            "Uk³ad napêdowy",
                            "Uk³ad wydechowy",
                            "Uk³ad ch³odzenia",
							"Œwiat³a"
						};
			}
		}

		public static IEnumerable<string> Oleje
		{
			get {
					return ReadLinesToStringList("../../oleje.txt");
				}
		}

		public static IEnumerable<string> Klocki
		{
			get
			{
				return ReadLinesToStringList("../../klocki.txt", 4);
			}
		}

		public static IEnumerable<string> Filtry
		{
			get
			{
				return ReadLinesToStringList("../../filtry.txt", 4);
			}
		}

		private static IEnumerable<string> ReadLinesToStringList(string s, int onlyLineNumbersDivableBy)
		{
			var allList = ReadLinesToStringList(s);
			int index = 0;
			var result = new List<string>();
			allList.Each(item =>
			             	{
			             		var me = item.Trim();
			             		if(index% onlyLineNumbersDivableBy == 0)
									result.Add(item.Substring(0,item.Length < 50 ? item.Length: 50));
			             		index++;
			             	});
			return result;
		}


		private static IEnumerable<string> ReadLinesToStringList(string s)
		{
			Check.Require(File.Exists(s), "{0} no such file".AsFormat(s));
			var result = new List<string>();
			using(var fs = new FileStream(s,FileMode.Open))
			using(var sr = new StreamReader(fs))
			{
				while(!sr.EndOfStream)
					result.Add(sr.ReadLine());
			}
			return result;
		}
	}


}