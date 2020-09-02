using AutoMapper;
using EdTech.Data.AlphaSource;
using EdTech.Data.ZetaDestination;
using EdTech.Data.Common.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EdTech.Apps.SimpleConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var unitOfWorkForSource = new UnitOfWorkForAlpha())
            {
                using (var unitOfWorkForDestination = new UnitOfWorkForZeta())
                {
                    var configuration
                                = CreateMappingBetweenUnitOfWorkEntityTypes<
                                    EdTech.Data.AlphaSource.Category,
                                    EdTech.Data.ZetaDestination.Category
                                  >(unitOfWorkForSource, unitOfWorkForDestination);
                    var mapper = configuration.CreateMapper();

                    var categories
                        = unitOfWorkForSource.CategoryRepository.SelectAll()
                            .Select(category => mapper.Map<
                                        EdTech.Data.AlphaSource.Category,
                                        EdTech.Data.ZetaDestination.Category
                                      >(category)).ToList();

                    unitOfWorkForDestination.CategoryRepository.Upsert(categories);
                    Console.WriteLine("Upsert complete.");
                }
            }
        }

        static MapperConfiguration CreateMappingBetweenUnitOfWorkEntityTypes<T, U>(
            UnitOfWorkForAlpha source,
            UnitOfWorkForZeta destination,
            bool ignoreNavigationProperties = true
        ) where T : class {
            var mapperConfig = new MapperConfiguration(
                        cfg =>
                        {
                            var configuration
                                = cfg.CreateMap<T, U>();
                            var propertiesToBeIgnored
                                = source.NavigationPropertiesFor<T>();
                            propertiesToBeIgnored
                                .Aggregate(configuration, (result, propertyInfo) => result.ForMember(propertyInfo.Name, m => m.Ignore()));
                        }
                    );
            return mapperConfig;
        }
    }
}
