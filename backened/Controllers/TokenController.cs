//using Microsoft.AspNetCore.Mvc;
//using SecureAuthApi.Models;
//using SecureAuthApi.Repositories;
//using Swashbuckle.AspNetCore.Annotations;
//using System;

//namespace SecureAuthApi.Controllers
//{
//    [Route("api/[controller]")]
//    [ApiController]
//    public class TokenController : ControllerBase
//    {
//        private readonly TokenRepository _tokenRepository;

//        public TokenController(TokenRepository tokenRepository)
//        {
//            _tokenRepository = tokenRepository;
//        }

//        /// <summary>
//        /// Create a new token for a user.
//        /// </summary>
//        /// <param name="userId">The ID of the user for whom the token is generated.</param>
//        /// <param name="expirationHours">Number of hours the token is valid.</param>
//        /// <returns>The generated token.</returns>
//        [HttpPost("create")]
//        [SwaggerOperation(Tags = new[] { "Token" })]
//        public IActionResult CreateToken(int userId, int expirationHours = 2)
//        {
//            try
//            {
//                var token = TokenModel.CreateToken(userId, expirationHours);
//                _tokenRepository.AddToken(token);
//                return Ok(new { Message = "Token created successfully.", Token = token });
//            }
//            catch (Exception ex)
//            {
//                return StatusCode(500, new { Message = "An error occurred while creating the token.", Error = ex.Message });
//            }
//        }

//        /// <summary>
//        /// Validate a token to check its validity.
//        /// </summary>
//        /// <param name="token">The token to validate.</param>
//        /// <returns>Validation result.</returns>
//        [HttpPost("validate")]
//        [SwaggerOperation(Tags = new[] { "Token" })]
//        public IActionResult ValidateToken(string token)
//        {
//            try
//            {
//                var tokenEntry = _tokenRepository.GetToken(token);
//                if (tokenEntry == null)
//                    return NotFound(new { Message = "Token not found." });

//                if (!tokenEntry.IsValid())
//                    return Unauthorized(new { Message = "Token is expired." });

//                return Ok(new { Message = "Token is valid.", Token = tokenEntry });
//            }
//            catch (Exception ex)
//            {
//                return StatusCode(500, new { Message = "An error occurred while validating the token.", Error = ex.Message });
//            }
//        }

//        /// <summary>
//        /// Delete a specific token.
//        /// </summary>
//        /// <param name="token">The token to delete.</param>
//        /// <returns>Deletion result.</returns>
//        [HttpDelete("delete")]
//        [SwaggerOperation(Tags = new[] { "Token" })]
//        public IActionResult DeleteToken(string token)
//        {
//            try
//            {
//                var tokenEntry = _tokenRepository.GetToken(token);
//                if (tokenEntry == null)
//                    return NotFound(new { Message = "Token not found." });

//                _tokenRepository.DeleteToken(token);
//                return Ok(new { Message = "Token deleted successfully." });
//            }
//            catch (Exception ex)
//            {
//                return StatusCode(500, new { Message = "An error occurred while deleting the token.", Error = ex.Message });
//            }
//        }

//        /// <summary>
//        /// Delete all tokens for a specific user.
//        /// </summary>
//        /// <param name="userId">The ID of the user whose tokens will be deleted.</param>
//        /// <returns>Deletion result.</returns>
//        [HttpDelete("delete-by-user/{userId}")]
//        [SwaggerOperation(Tags = new[] { "Token" })]
//        public IActionResult DeleteTokensByUser(int userId)
//        {
//            try
//            {
//                _tokenRepository.DeleteTokensByUser(userId);
//                return Ok(new { Message = "All tokens for the user were deleted successfully." });
//            }
//            catch (Exception ex)
//            {
//                return StatusCode(500, new { Message = "An error occurred while deleting the tokens.", Error = ex.Message });
//            }
//        }
//    }
//}
