using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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

		public static IEnumerable<string> Przedstawicielstwa
		{
			get
			{
				return new[] 
				{ 
					"Szczecin - £u¿ycka ",
                    "LongLake",
                    "LowLake",
                    "Carens",
                    "AutoCar",
                    "MoonLightCar",
                    "Boost4U",
                    "EnginePower",
                    "Wagenner",
                    "OlaOla",
					"WarLiebs",
					"Autosam",
					"LiebenCar",
					"Zuruck",
					"Molas",
                    "Barban",
					"Sweego",
					"Lottal",
					"CarStore",
					"AutoShultz",
					"Mobiller",
					"WheelSlip",
					"UnderHummer",
					"SeniorasCaras",
					"Caringo",
					"AutoJaan",
					"RealParts",
					"LowLevelMortals",
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
							"Polska, Warszawa",
							"Polska, Wroc³aw",
				       		"Holandia, Amsterdam",
				       		"Hiszpania, Lizbona",
							"Hiszpania, Barcelona",
				       		"Wielka Brytania, Cardiff",
							"Wielka Brytania, Manchester",
							"Wielka Brytania, Londyn",
                            "Niemcy, Berlin",
							"Niemcy, Monachium",
                            "Francja, Lion",                            
							"Francja, Pary¿",
							"Francja, Marsylia",
                            "W³ochy, Turyn",
							"W³ochy, Rzym",
                            "Ukraina, Lwow",
							"Ukraina, Kijow",
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
				return ReadLinesToStringList("../../auta.txt").Where(s=> s.Length > 0);				
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
                            "Korbowód",
                            "Uk³ad hamulcowy",
                            "Silnik",
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
				return ReadLinesToStringList("../../filtry.txt", 3);
			}
		}

		public static IEnumerable<string> Lozyska
		{
			get
			{
				return ReadLinesToStringList("../../lozyska.txt", 3);
			}
		}

		public static IEnumerable<string> Rozne
		{
			get
			{
				return ReadLinesToStringList("../../rozne.txt", 3);
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