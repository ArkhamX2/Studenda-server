﻿using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Studenda.Core.Model.Account;
using Studenda.Core.Server.Extension;
using System.IdentityModel.Tokens.Jwt;

namespace Studenda.Core.Server.utils.Token
{
    public class TokenService:ITokenService
    {
        private readonly IConfiguration _configuration;

        public TokenService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string CreateToken(Person user, List<IdentityRole<long>> roles)
        {
            var token = user
                .CreateClaims(roles)
                .CreateJwtToken(_configuration);
            var tokenHandler = new JwtSecurityTokenHandler();

            return tokenHandler.WriteToken(token);
        }
    }
}