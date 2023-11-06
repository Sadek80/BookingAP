﻿using System.Net.Http.Json;
using BookingAP.Application.Abstractions.Authentication;
using BookingAP.Domain.Abstractions;
using BookingAP.Infrastructure.Authentication.Models;
using ErrorOr;
using Microsoft.Extensions.Options;
using static BookingAP.Domain.Users.DomainErrors;

namespace BookingAP.Infrastructure.Authentication;

internal sealed class JwtService : IJwtService
{
    private readonly HttpClient _httpClient;
    private readonly KeycloakOptions _keycloakOptions;

    public JwtService(HttpClient httpClient, IOptions<KeycloakOptions> keycloakOptions)
    {
        _httpClient = httpClient;
        _keycloakOptions = keycloakOptions.Value;
    }

    public async Task<ErrorOr<string>> GetAccessTokenAsync(
        string email,
        string password,
        CancellationToken cancellationToken = default)
    {
        try
        {
            var authRequestParameters = new KeyValuePair<string, string>[]
            {
                new("client_id", _keycloakOptions.AuthClientId),
                new("client_secret", _keycloakOptions.AuthClientSecret),
                new("scope", "openid email"),
                new("grant_type", "password"),
                new("username", email),
                new("password", password)
            };

            var authorizationRequestContent = new FormUrlEncodedContent(authRequestParameters);

            var response = await _httpClient.PostAsync("", authorizationRequestContent, cancellationToken);

            response.EnsureSuccessStatusCode();

            var authorizationToken = await response.Content.ReadFromJsonAsync<AuthorizationToken>();

            if (authorizationToken is null)
            {
                return DomainError.Failure(UserErrors.AuthenticationFailed);
            }

            return authorizationToken.AccessToken;
        }
        catch (HttpRequestException)
        {
            return DomainError.Failure(UserErrors.AuthenticationFailed);
        }
    }
}