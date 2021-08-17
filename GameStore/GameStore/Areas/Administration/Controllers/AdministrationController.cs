using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GameStore.Areas.Administration.Controllers
{

    [Authorize(Roles = "Administrator")]
    [Area("Administration")]
    public abstract class AdministrationController : Controller
    {

    }
}
