using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace GameStore.Areas.Administration.Controllers
{

    [Authorize(Roles = "Administrator")]
    [Area("Administration")]
    public abstract class AdministrationController : Controller
    {

    }
}
