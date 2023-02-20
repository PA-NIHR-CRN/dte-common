using System;

namespace Dte.Common.Authentication
{
    public class ClientsSettings
    {
        public static string SectionName => "Clients";
        public ClientSettings StudyService { get; set; }
        public ClientSettings PostCoderService { get; set; }
        public ClientSettings OrdnanceSurveyService { get; set; }
        public ClientSettings StudyManagementService { get; set; }
        public ClientSettings ParticipantService { get; set; }
        public ClientSettings ReferenceDataService { get; set; }
        public ClientSettings LocationService { get; set; }
        public ClientSettings CpmsService { get; set; }
        public ClientSettings IdgService { get; set; }
    }

    public class ClientSettings
    {
        public string BaseUrl { get; set; }
        public TimeSpan DefaultTimeout { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string ApiKey { get; set; }
        public string TokenEndpointUrl { get; set; }
        public string ClientId { get; set; }
        public string ClientSecret { get; set; }
        public string Scope { get; set; }
        public string GrantType { get; set; }
        public string RefreshToken { get; set; }
        public string AccessToken { get; set; }
        public string TokenType { get; set; }
        public string RefreshTokenEndpointUrl { get; set; }
        public string RefreshTokenClientId { get; set; }
        public string RefreshTokenClientSecret { get; set; }
        public string RefreshTokenGrantType { get; set; }
        public string RefreshTokenScope { get; set; }
        public string RefreshTokenTokenType { get; set; }
    }
}
