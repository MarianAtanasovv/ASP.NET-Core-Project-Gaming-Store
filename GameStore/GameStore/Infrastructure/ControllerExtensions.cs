using Microsoft.AspNetCore.Mvc;
using System;

namespace GameStore.Infrastructure
{
   
    public static class ControllerExtensions
        {
            public static string GetControllerName(this Type controllerType)
                => controllerType.Name.Replace(nameof(Controller), string.Empty);
        }
    
}