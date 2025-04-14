//using System;
//using System.Collections.Generic;
//using System.Data;
//using Dapper;
//using SecureAuthApi.Models;

//namespace SecureAuthApi.Repositories
//{
//    public class TokenRepository
//    {
//        private readonly IDbConnection _dbConnection;

//        public TokenRepository(IDbConnection dbConnection)
//        {
//            _dbConnection = dbConnection;
//        }

//        public void AddToken(TokenModel tokenModel)
//        {
//            const string query = "INSERT INTO Token (UserID, Token, IssuedAt, ExpiresAt) VALUES (@UserID, @Token, @IssuedAt, @ExpiresAt)";
//            if (_dbConnection.State != ConnectionState.Open)
//                _dbConnection.Open();

//            _dbConnection.Execute(query, tokenModel);
//        }

//        public TokenModel GetToken(string token)
//        {
//            const string query = "SELECT * FROM Token WHERE Token = @Token";
//            if (_dbConnection.State != ConnectionState.Open)
//                _dbConnection.Open();

//            return _dbConnection.QuerySingleOrDefault<TokenModel>(query, new { Token = token });
//        }

//        public void DeleteToken(string token)
//        {
//            const string query = "DELETE FROM Token WHERE Token = @Token";
//            if (_dbConnection.State != ConnectionState.Open)
//                _dbConnection.Open();

//            _dbConnection.Execute(query, new { Token = token });
//        }

//        public void DeleteTokensByUser(int userId)
//        {
//            const string query = "DELETE FROM Token WHERE UserID = @UserID";
//            if (_dbConnection.State != ConnectionState.Open)
//                _dbConnection.Open();

//            _dbConnection.Execute(query, new { UserID = userId });
//        }

//        public IEnumerable<TokenModel> GetAllTokens()
//        {
//            const string query = "SELECT * FROM Token";
//            if (_dbConnection.State != ConnectionState.Open)
//                _dbConnection.Open();

//            return _dbConnection.Query<TokenModel>(query);
//        }
//    }
//}
