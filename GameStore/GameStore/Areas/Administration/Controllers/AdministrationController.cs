using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GameStore.Areas.Administration.Controllers
{
    using static AdminConstants;

    [Authorize(Roles = AdministratorRoleName)]
    [Area(AreaName)]
    public abstract class AdministrationController : Controller
    {

    }
}
