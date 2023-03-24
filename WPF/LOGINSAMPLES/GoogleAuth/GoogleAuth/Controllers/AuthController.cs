using GoogleAuth.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace WebApplication1.Controllers {
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase {
        // GET: api/<GAuthController>
        [Route("google")]
        [HttpGet]
        public string GetGoogle([FromQuery]string code) {
            Task.Run(() => { Globals.MakeCall("google", code); });
            return "Please close this window and return back to your application..";
        }

        //[Route("github")]
        //[HttpGet]
        //public string GetGithub([FromQuery] string code) {
        //    Globals.MakeCall("google", code);
        //    return "Please close this window and return back to your application..";
        //}
    }
}
