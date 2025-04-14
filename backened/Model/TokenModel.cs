//using System;

//namespace SecureAuthApi.Models
//{
//    public class TokenModel
//    {
//        public int TokenID { get; set; }
//        public int UserID { get; set; }
//        public string Token { get; set; }
//        public DateTime IssuedAt { get; set; }
//        public DateTime ExpiresAt { get; set; }

//        /// <summary>
//        /// Validates if the token is still valid (not expired).
//        /// </summary>
//        /// <returns>True if the token is valid, false otherwise.</returns>
//        public bool IsValid()
//        {
//            return DateTime.UtcNow <= ExpiresAt;
//        }

//        /// <summary>
//        /// Generates a new token object.
//        /// </summary>
//        /// <param name="userId">The user ID associated with the token.</param>
//        /// <param name="expirationHours">Number of hours the token will remain valid.</param>
//        /// <returns>A new TokenModel instance.</returns>
//        public static TokenModel CreateToken(int userId, int expirationHours)
//        {
//            return new TokenModel
//            {
//                UserID = userId,
//                Token = Guid.NewGuid().ToString(), // Generate a unique token
//                IssuedAt = DateTime.UtcNow,
//                ExpiresAt = DateTime.UtcNow.AddHours(expirationHours)
//            };
//        }
//    }
//}
