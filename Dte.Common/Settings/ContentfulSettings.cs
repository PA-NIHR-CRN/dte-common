namespace Dte.Common
{
    public class ContentfulSettings
    {
        public static readonly string SectionName = "ContentfulSettings";
        public string DeliveryApiKey { get; set; }
        public string PreviewApiKey { get; set; }
        public string SpaceId { get; set; }
        public bool UsePreviewApi { get; set; }
        public string BaseUrl { get; set; }
        public EmailTemplates EmailTemplates { get; set; }
    }
    public class EmailTemplates
    {
        public string ForgotPassword { get; set; }
        public string ResendCode { get; set; }
        public string SignUp { get; set; }
        public string UpdateUserAttribute { get; set; }
        public string NhsAccountExists { get; set; }
        public string NhsPasswordReset { get; set; }
        public string EmailAccountExists { get; set; }
        public string NhsSignUp { get; set; }
        public string MfaEmailConfirmation { get; set; }
        public string MfaMobileNumberVerification { get; set; }
        public string DeleteAccount { get; set; }
        public string NewAccount { get; set; }
    }
}