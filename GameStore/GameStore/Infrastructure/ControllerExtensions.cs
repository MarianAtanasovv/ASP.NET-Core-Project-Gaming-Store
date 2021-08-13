using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameStore.Infrastructure
{
    
    public static class ControllerExtensions
        {
            public static string GetControllerName(this Type controllerType)
                => controllerType.Name.Replace(nameof(Controller), string.Empty);
        }
    
}