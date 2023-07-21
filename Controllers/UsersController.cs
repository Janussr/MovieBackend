using CodeFirstProject.DbConnector;
using CodeFirstProject.DTOs;
using CodeFirstProject.Models;
using CodeFirstProject.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CodeFirstProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {

        private readonly UserService _userService;

        public UsersController(UserService userService)
        {
            _userService = userService;
        }


        [HttpGet]
        [Route("GetUsers")]
        public IActionResult Get()
        {
            var users = _userService.GetAllUsers();
            return Ok(users);
        }


        [HttpGet]
        [Route("getuserbyid")]
        public IActionResult Get(int id)
        {
            try
            {
                var user = _userService.GetUserById(id);
                if (user == null)
                {
                    return NotFound($"User not found with id {id}");
                }
                return Ok(user);
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        [Route("AddUser")]
        public IActionResult Post(UserDTO model)
        {
            try
            {
                var user = _userService.AddUser(model);
                return Ok("User created");
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        public IActionResult Put(int _userId, UserDTO model)
        {
            if (model == null || model.UserId == 0)
            {
                if (model == null)
                {
                    return BadRequest("Model data is invalid");
                }
                else if (model.UserId == 0)
                {
                    return BadRequest($"User Id {model.UserId} is invalid");
                }
            }

            var updatedProduct = _userService.UpdateUser(_userId, model);
            if (updatedProduct == null)
            {
                return NotFound($"User not found with id {_userId}");
            }

            return Ok(updatedProduct);
        }


        [HttpDelete]
        public IActionResult Delete(int id)
        {
            try
            {
                _userService.DeleteUser(id);

                return Ok("User deleted.");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


    }
}
