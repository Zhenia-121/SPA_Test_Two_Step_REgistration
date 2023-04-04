using Application.Users.Commands.Register;
using Microsoft.AspNetCore.Mvc;

namespace TwoStepRegistrationWithIdentity.Controllers
{
    public class UsersController : ApiControllerBase
    {
        [HttpPost]
        [Route("Register")]
        public async Task<ActionResult> RegisterUser([FromBody] RegisterUserCommand command, CancellationToken cancellationToken)
        {
            await Mediator.Send(command, cancellationToken);

            return Ok();
        }
    }
}
