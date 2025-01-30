﻿using learning_jwt_auth_dotnet.Entities;
using learning_jwt_auth_dotnet.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace learning_jwt_auth_dotnet.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class AuthController : ControllerBase
  {
    public static User user = new();

    [HttpPost("register")]
    public ActionResult<User> Register(UserDto request)
    {
      var hashedPassword = new PasswordHasher<User>()
        .HashPassword(user, request.Password);

      user.Username = request.Username;
      user.PasswordHash = hashedPassword;

      return Ok(user);
    }

    [HttpPost("login")]
    public ActionResult<string> Login(UserDto request)
    {
      if (user.Username != request.Username)
        return BadRequest("User not found");

      if (new PasswordHasher<User>().VerifyHashedPassword(user, user.PasswordHash, request.Password) == PasswordVerificationResult.Failed)
        return BadRequest("Wrong password");

      string token = "Success";

      return Ok(token);
    }
  }
}
