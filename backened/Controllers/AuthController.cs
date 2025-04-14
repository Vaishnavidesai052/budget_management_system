using System;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using SecureAuthApi.Helpers;
using SecureAuthApi.Models;
using System.Security.Cryptography;
using BudgetManagementSystemNew.Repositories;
using Swashbuckle.AspNetCore.Annotations;
using Swashbuckle.Swagger.Annotations;
using BudgetManagementSystemNew.DTOs;



namespace SecureAuthApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly UserRepository _userRepository;
        private readonly JwtHelper _jwtHelper;
        private readonly RoleRepository _roleRepository;
        private readonly DepartmentRepository _departmentRepository;

        public AuthController(UserRepository userRepository, JwtHelper jwtHelper, RoleRepository roleRepository, DepartmentRepository departmentRepository)
        {
            _userRepository = userRepository;
            _jwtHelper = jwtHelper;
            _roleRepository = roleRepository;
            _departmentRepository = departmentRepository;
        }

        #region User CRUD Operations

        [HttpPost("register")]
        //[SwaggerOperation(Tags = new[] { "User" })]
        public IActionResult Register([FromBody] RegisterModel model)
        {
            //if (!model.Email.EndsWith("@mahametro.org"))
            // return BadRequest(new { Message = "Only @mahametro.org email addresses are allowed." });

            if (_userRepository.UserExists(model.Email))
                return Conflict(new { Message = "User already exists." });

            if (model.Password != model.ConfirmPassword)
                return BadRequest(new { Message = "Passwords do not match." });

            try
            {
                _userRepository.AddUser(model);
                return Ok(new { Message = "User registered successfully." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "An error occurred during registration.", Error = ex.Message });
            }
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginModel model)
        {
            var storedPasswordHash = _userRepository.GetPasswordHash(model.Email);
            if (storedPasswordHash == null || !VerifyPassword(model.Password, storedPasswordHash))
                return Unauthorized(new { Message = "Invalid login attempt." });

            var userDetails = _userRepository.GetUserByEmail(model.Email);

            if (userDetails == null)
            {
                return Unauthorized(new { Message = "Invalid login attempt." });
            }

            // Generate JWT token
            var token = _jwtHelper.GenerateToken(model.Email);

            var response = new LoginResponseDTO
            {
                Token = token,
                Username = userDetails.Username,
                UserDetails = userDetails
            };

            return Ok(response);
        }



        [HttpGet("all")]
        //[SwaggerOperation(Tags = new[] { "User" })]
        public IActionResult GetAllUsers()
        {
            try
            {
                var users = _userRepository.GetAllUsers();
                return Ok(users);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "An error occurred while fetching users.", Error = ex.Message });
            }
        }

        [HttpGet("{id}")]
        //[SwaggerOperation(Tags = new[] { "User" })]
        public IActionResult GetUserById(int id)
        {
            try
            {
                var user = _userRepository.GetUserById(id);
                if (user == null)
                {
                    return NotFound(new { Message = "User not found." });
                }
                return Ok(user);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "An error occurred while fetching the user.", Error = ex.Message });
            }
        }

        [HttpPut("update/{id}")]
        //[SwaggerOperation(Tags = new[] { "User" })]
        public IActionResult Update(int id, [FromBody] UserUpdateModel model)
        {
            try
            {
                _userRepository.UpdateUser(id, model);
                return Ok(new { Message = "User updated successfully." });
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new { Message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "An error occurred while updating the user.", Error = ex.Message });
            }
        }

        [HttpDelete("delete/{id}")]
        // [SwaggerOperation(Tags = new[] { "User" })]
        public IActionResult DeleteUser(int id)
        {
            if (!_userRepository.UserExistsById(id))
                return NotFound(new { Message = "User not found." });

            try
            {
                _userRepository.DeleteUser(id);
                return Ok(new { Message = "User deleted successfully." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "An error occurred while deleting the user.", Error = ex.Message });
            }
        }

        #endregion


        private bool VerifyPassword(string password, string storedHash)
        {
            var hash = HashPassword(password);
            return hash == storedHash;
        }

        private string HashPassword(string password)
        {
            using var sha256 = SHA256.Create();
            var bytes = Encoding.UTF8.GetBytes(password);
            var hash = sha256.ComputeHash(bytes);
            return Convert.ToBase64String(hash);
        }

    }
}

//using System;
//using System.Text;
//using Microsoft.AspNetCore.Mvc;
//using SecureAuthApi.Helpers;
//using SecureAuthApi.Models;
//using System.Security.Cryptography;
//using BudgetManagementSystemNew.Repositories;
//using Microsoft.AspNetCore.Http;
//using Swashbuckle.AspNetCore.Annotations;

//namespace SecureAuthApi.Controllers
//{
//    [Route("api/[controller]")]
//    [ApiController]
//    public class AuthController : ControllerBase
//    {
//        private readonly UserRepository _userRepository;
//        private readonly JwtHelper _jwtHelper;
//        private readonly RoleRepository _roleRepository;
//        private readonly DepartmentRepository _departmentRepository;

//        public AuthController(UserRepository userRepository, JwtHelper jwtHelper, RoleRepository roleRepository, DepartmentRepository departmentRepository)
//        {
//            _userRepository = userRepository;
//            _jwtHelper = jwtHelper;
//            _roleRepository = roleRepository;
//            _departmentRepository = departmentRepository;
//        }

//        #region User Authentication & Session Management

//        [HttpPost("register")]
//        public IActionResult Register([FromBody] RegisterModel model)
//        {
//            if (_userRepository.UserExists(model.Email))
//                return Conflict(new { Message = "User already exists." });

//            if (model.Password != model.ConfirmPassword)
//                return BadRequest(new { Message = "Passwords do not match." });

//            try
//            {
//                _userRepository.AddUser(model);
//                return Ok(new { Message = "User registered successfully." });
//            }
//            catch (Exception ex)
//            {
//                return StatusCode(500, new { Message = "An error occurred during registration.", Error = ex.Message });
//            }
//        }
//        [HttpPost("login")]
//        public IActionResult Login([FromBody] LoginModel model)
//        {
//            var storedPasswordHash = _userRepository.GetPasswordHash(model.Email);
//            if (storedPasswordHash == null || !VerifyPassword(model.Password, storedPasswordHash))
//                return Unauthorized(new { Message = "Invalid login attempt." });

//            var userDetails = _userRepository.GetUserByEmail(model.Email);
//            if (userDetails == null)
//                return NotFound(new { Message = "User not found." });

//            var token = _jwtHelper.GenerateToken(model.Email);

//            // Store user session (Ensure RoleId is handled properly)
//            HttpContext.Session.SetInt32("UserId", userDetails.Id);
//            HttpContext.Session.SetInt32("RoleId", userDetails.RoleId ?? 0); // Use 0 if null
//            HttpContext.Session.SetString("Email", userDetails.Email);

//            return Ok(new
//            {
//                Token = token,
//                UserDetails = new
//                {
//                    userDetails.Id,
//                    userDetails.Email,
//                    RoleId = userDetails.RoleId // This will now be included in JSON
//                }
//            });
//        }


//        [HttpGet("check-auth")]
//        public IActionResult CheckAuth()
//        {
//            var userId = HttpContext.Session.GetInt32("UserId");
//            var roleId = HttpContext.Session.GetInt32("RoleId");
//            var email = HttpContext.Session.GetString("Email");

//            if (userId == null)
//                return Unauthorized(new { Message = "User is not authenticated." });

//            return Ok(new { UserId = userId, RoleId = roleId, Email = email });
//        }

//        [HttpPost("logout")]
//        public IActionResult Logout()
//        {
//            HttpContext.Session.Clear();
//            return Ok(new { Message = "Logged out successfully." });
//        }

//        #endregion

//        #region User Management

//        [HttpGet("all")]
//        public IActionResult GetAllUsers()
//        {
//            try
//            {
//                var users = _userRepository.GetAllUsers();
//                return Ok(users);
//            }
//            catch (Exception ex)
//            {
//                return StatusCode(500, new { Message = "An error occurred while fetching users.", Error = ex.Message });
//            }
//        }

//        [HttpGet("{id}")]
//        public IActionResult GetUserById(int id)
//        {
//            try
//            {
//                var user = _userRepository.GetUserById(id);
//                if (user == null)
//                    return NotFound(new { Message = "User not found." });

//                return Ok(user);
//            }
//            catch (Exception ex)
//            {
//                return StatusCode(500, new { Message = "An error occurred while fetching the user.", Error = ex.Message });
//            }
//        }

//        [HttpPut("update/{id}")]
//        public IActionResult Update(int id, [FromBody] UserUpdateModel model)
//        {
//            try
//            {
//                _userRepository.UpdateUser(id, model);
//                return Ok(new { Message = "User updated successfully." });
//            }
//            catch (InvalidOperationException ex)
//            {
//                return BadRequest(new { Message = ex.Message });
//            }
//            catch (Exception ex)
//            {
//                return StatusCode(500, new { Message = "An error occurred while updating the user.", Error = ex.Message });
//            }
//        }

//        [HttpDelete("delete/{id}")]
//        public IActionResult DeleteUser(int id)
//        {
//            if (!_userRepository.UserExistsById(id))
//                return NotFound(new { Message = "User not found." });

//            try
//            {
//                _userRepository.DeleteUser(id);
//                return Ok(new { Message = "User deleted successfully." });
//            }
//            catch (Exception ex)
//            {
//                return StatusCode(500, new { Message = "An error occurred while deleting the user.", Error = ex.Message });
//            }
//        }

//        #endregion

//        #region Helper Methods

//        private bool VerifyPassword(string password, string storedHash)
//        {
//            var hash = HashPassword(password);
//            return hash == storedHash;
//        }

//        private string HashPassword(string password)
//        {
//            using var sha256 = SHA256.Create();
//            var bytes = Encoding.UTF8.GetBytes(password);
//            var hash = sha256.ComputeHash(bytes);
//            return Convert.ToBase64String(hash);
//        }

//        #endregion
//    }
//}
