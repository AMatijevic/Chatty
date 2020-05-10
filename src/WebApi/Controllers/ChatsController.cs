using Chatty.Application.Features.Chat;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using static Chatty.Application.Features.Chat.GetAllChats;

namespace Chatty.WebApi.Controllers
{
    public class ChatsController : ApiController
    {
        [HttpGet]
        [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Get))]
        public async Task<ActionResult<IEnumerable<ChatDto>>> Get()
        {
            return new ActionResult<IEnumerable<ChatDto>>(await Mediator.Send(new GetAllChatsQuery()).ConfigureAwait(false));
        }
    }
}
