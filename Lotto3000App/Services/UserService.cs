﻿using DataAccess;
using DataModels;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Models;
using Services.Helpers;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace Services
{
    public class UserService : IUserService
    {
        private readonly IRepository<User> _userRepository;
        private readonly IOptions<AppSettings> _options;
        public UserService(IRepository<User> userRepository,
            IOptions<AppSettings> options)
        {
            _userRepository = userRepository;
            _options = options;
        }

        public UserModel Authenticate(string username, string password)
        {
            var md5 = new MD5CryptoServiceProvider();
            var md5data = md5.ComputeHash(
                Encoding.ASCII.GetBytes(password));
            var hashedPassword = Encoding.ASCII.GetString(md5data);

            var user = _userRepository.GetAll()
                .SingleOrDefault(x => x.Username == username &&
                x.Password == hashedPassword);

            if (user == null) return null;

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_options.Value.Secret);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new System.Security.Claims.ClaimsIdentity(
                    new[]
                    {
                        new Claim(ClaimTypes.Name,
                        $"{user.Firstname} {user.Lastname}"),
                        new Claim(ClaimTypes.NameIdentifier,
                        user.Id.ToString(),
                        user.Role.ToString())
                    }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(key),
                    SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);

            // mapping
            UserModel userModel = new UserModel()
            {
                Id = user.Id,
                Firstname = user.Firstname,
                Lastname = user.Lastname,
                Username = user.Username,
                Token = tokenHandler.WriteToken(token)
            };

            return userModel;
        }

        public void RegisterUser(RegisterModel model)
        {
            var md5 = new MD5CryptoServiceProvider();
            var md5data = md5.ComputeHash(
                Encoding.ASCII.GetBytes(model.Password));
            var hashedPassword = Encoding.ASCII.GetString(md5data);

            // mapping
            User user = new User()
            {
                Firstname = model.Firstname,
                Lastname = model.Lastname,
                Username = model.Username,
                Password = hashedPassword
            };

            _userRepository.Add(user);
        }
    }
}
