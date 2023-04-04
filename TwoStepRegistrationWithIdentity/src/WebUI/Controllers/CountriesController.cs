using Application.Countries.Queries;
using Application.Province.Queries;
using Application.Provinces.Queries;
using Microsoft.AspNetCore.Mvc;

namespace TwoStepRegistrationWithIdentity.Controllers
{
    public class CountriesController : ApiControllerBase
    {
        [HttpGet]
        public async Task<ActionResult<List<CountryListItemDto>>> GetAllCountries(CancellationToken cancellationToken)
        {
            return await Mediator.Send(new GetAllCountriesQuery(), cancellationToken);
        }

        [HttpGet]
        [Route("{id:int}/Provinces")]
        public async Task<ActionResult<List<ProvinceListItemDto>>> GetProvinceCountries([FromRoute] int id, CancellationToken cancellationToken)
        {
            return await Mediator.Send(new GetProvincesByCountryQuery { CountryId = id }, cancellationToken);
        }
    }
}
