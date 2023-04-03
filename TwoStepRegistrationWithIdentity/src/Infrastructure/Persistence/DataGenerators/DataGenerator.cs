using Bogus;
using Domain.Entities;
using Infrastructure.Identity;

namespace Infrastructure.Persistence.DataGenerators
{
    internal class DataGenerator
    {
        public const int CountriesCount = 5;
        public const int ProvincesPerCountry = 10;
        public const int UsersCount = 15;


        public static readonly List<Country> Countries = new();

        public static readonly List<Province> Provinces = new();

        public static readonly List<ApplicationUser> Users = new();
        

        public static void InitBogusData()
        {
            var countryGenerator = GetCountryGenerator();
            var countries = countryGenerator.Generate(CountriesCount);
            countries.ForEach(c => c.Provinces.AddRange(GetBogusProvinceData(c.Id)));
            Countries.AddRange(countries);

            var random = new Randomizer();
            var userGenerator = GetUserGenerator(random.Number(0, Provinces.Count - 1));
            var generatedUsers = userGenerator.Generate(UsersCount);
            Users.AddRange(generatedUsers);
        }

        private static Faker<Country> GetCountryGenerator()
        {
            return new Faker<Country>()
                .RuleFor(v => v.Name, f => f.Address.Country())
                .RuleFor(v => v.Code, f => f.Address.CountryCode());
        }

        private  static Faker<Province> GetProvinceGenerator(int countryId)
        {
            return new Faker<Province>()
                .RuleFor(v => v.Name, f => f.Address.City())
                .RuleFor(v => v.CountryId, _ => countryId);
        }

        private static List<Province> GetBogusProvinceData(int countryId)
        {
            var provinceGenerator = GetProvinceGenerator(countryId);
            var generatedProvinces = provinceGenerator.Generate(ProvincesPerCountry);
            Provinces.AddRange(generatedProvinces);
            return generatedProvinces;
        }

        private static Faker<ApplicationUser> GetUserGenerator(int provinceId)
        {
            return new Faker<ApplicationUser>()
                .RuleFor(u => u.Id, _ => Guid.NewGuid().ToString())
                .RuleFor(u => u.Email, f => f.Internet.Email())
                .RuleFor(u => u.ProvinceId, _ => provinceId)
                .RuleFor(u => u.ServiceAgreementAccepted, f => f.Random.Bool());
        }
    }
}
