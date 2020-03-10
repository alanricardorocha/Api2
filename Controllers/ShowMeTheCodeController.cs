using Microsoft.AspNetCore.Mvc;
using System;

namespace Api2.Controllers
{
    public class ShowMeTheCodeController : ControllerBase
    {
        [HttpGet("showmethecode")]
        public String ShowMeTheCode()
        {
            return "https://github.com/alanricardorocha";
        }

    }
}
