using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using AutoMapper;
using DataGenerator.Extensions;
using DataGenerator.Scheme;

namespace DataGenerator.MappingConfiguration
{
	public class Configuration
	{
		public void CreateMappings(Assembly assembly)
		{
			var configurations = FetchConfigurations(assembly);
			configurations.Each(c=> c.CreateMapping());
			Mapper.AssertConfigurationIsValid();
		}

		private IEnumerable<IMappingConfiguration> FetchConfigurations(Assembly assembly)
		{
			var types = assembly.GetTypes().Where(
					t => t.IsClass && t.Namespace.Equals(typeof(Configuration).Namespace)
				);
			var result = new List<IMappingConfiguration>();
			types.Each(t =>
			           	{
			           		var instace = Activator.CreateInstance(t);
			           		var mappingConfiguration = instace as IMappingConfiguration;
							if (mappingConfiguration != null)
							{
								"Adding mapping {0}".AsFormat(t.FullName).LogDebug();
								result.Add(mappingConfiguration);
							}
			           	});
			return result;
		}
	}

	public interface IMappingConfiguration
	{
		void CreateMapping();
	}
	
	public class StringToPrzedstawicielstwo : IMappingConfiguration
	{
		public void CreateMapping()
		{
			Mapper.CreateMap<string,Przedstawicielstwo>()
				.ConvertUsing(src=> new Przedstawicielstwo{Nazwa=src});
		}
	}


    //public class DateTimeToDbDateMapping : IMappingConfiguration
    //{
    //    public void CreateMapping()
    //    {
    //        Mapper.CreateMap<DateTime,Data>()
    //            .ConvertUsing(src=> new Data
    //                                    {
    //                                        PK_Date = src,
    //                                        Day_Of_Month = src.Day,
    //                                        Day_Of_Week = (int) src.DayOfWeek,
    //                                        Day_Of_Year = src.DayOfYear,
    //                                        Year = src.Year,
    //                                        Month_Of_Year = src.Month,
    //                                        Month_Of_Quarter = src.Month%4,
    //                                        Quarter_Of_Year = src.Month/4 + 1,
    //                                        Week_Of_Year = src.DayOfYear/7 + 1
    //                                    });
    //    }
    //}

	public class StringToMagazynMapping : IMappingConfiguration
	{
		public void CreateMapping()
		{            
			Mapper.CreateMap<string, Magazyn>()
				.ConvertUsing(src=> new Magazyn{Nazwa=src});
			"Mapping Created: {0} => {1}".AsFormat(typeof (string).Name, typeof (Magazyn).Name).LogDebug();
		}
	}

	public class StringToLokalizacjaMapping : IMappingConfiguration
	{
		public void CreateMapping()
		{
			Mapper.CreateMap<string, Lokalizacja>()
				.ConvertUsing(src => {
					var parts = src.Split(',');
					return new Lokalizacja() 
					{
						Kraj = parts[0],
						Miasto = parts[1]
					}; 
				});
		}
	}

	public class StringToDostawcaMapping : IMappingConfiguration
	{
		public void CreateMapping()
		{
			Mapper.CreateMap<string, Dostawca>()
				.ConvertUsing(src=> new Dostawca{Nazwa=src});
				
		}
	}

	public class StringToModelMapping : IMappingConfiguration
	{
		public void CreateMapping()
		{
			Mapper.CreateMap<string,Model>()
				.ConvertUsing(src=>
				              	{
				              		var parts = src.Split(',');									
				              		return new Model
				              		            	{
				              		            		Nazwa_Modelu =  parts[0].Trim(),                                                        
														Typ_Silnika = parts[1].Trim(),
														Rok = Convert.ToInt32(parts[2])
				              		            	};
				              	});
		}
	}

	public class StringToFirmaMapping : IMappingConfiguration
	{
		public void CreateMapping()
		{
			Mapper.CreateMap<string, Firma>()
				.ConvertUsing(src=> new Firma{Nazwa=src});
		}
	}

	public class StringToKategoriaMapping : IMappingConfiguration
	{
		public void CreateMapping()
		{
			Mapper.CreateMap<string, Kategoria_Czesci>()
				.ConvertUsing(src=> new Kategoria_Czesci{Nazwa=src});				
		}
	}

	public class StringToCzescZamiennaMapping : IMappingConfiguration
	{
		public void CreateMapping()
		{
			Mapper.CreateMap<string,Czesc_Zamienna>()
				.ConvertUsing(src=> new Czesc_Zamienna {Nazwa = src});
		}
	}
}