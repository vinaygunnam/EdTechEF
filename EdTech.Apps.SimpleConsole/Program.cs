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
                    var mapperConfig = new MapperConfiguration(
                        cfg =>
                        {
                            var configuration
                                = CreateMappingBetweenUnitOfWorkEntityTypes<
                                    EdTech.Data.AlphaSource.Category,
                                    EdTech.Data.ZetaDestination.Category
                                  >(unitOfWorkForSource, unitOfWorkForDestination);
                            var mapper = configuration.CreateMapper();

                            var category = unitOfWorkForSource.CategoryRepository.SelectById(1);
                            var products = category.Products.ToList();

                            var destCategory = mapper.Map<
                                    EdTech.Data.AlphaSource.Category,
                                    EdTech.Data.ZetaDestination.Category
                                  >(category);
                            Console.WriteLine("Put a breakpoint before me and inspect categpry and destCategory. Only non-navigation properties must be filled in destCategory");
                        }
                    );
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
