namespace Dte.Common.Exceptions.Common
{
    public class ErrorCode
    {
        // Http codes
        public const string InternalServerError = "InternalServerError";
        public const string BadRequest = "Bad_Request";
        public const string NotFound = "Not_Found";
        public const string Unauthorized = "Unauthorized";
        public const string Forbidden = "Forbidden";
        public const string Conflict = "Conflict";
        public const string UnprocessableEntity = "Unprocessable_Entity";
        public const string TooManyRequests = "Too_Many_Requests";
        public const string ServiceUnavailable = "Service_Unavailable";
        public const string GatewayTimeout = "Gateway_Timeout";
        public const string RequestTimeout = "Request_Timeout";
        public const string MethodNotAllowed = "Method_Not_Allowed";
        public const string UnsupportedMediaType = "Unsupported_Media_Type";
        
        // Authentication codes
        public const string AuthenticationError = "Authentication_Error";
        public const string AuthenticationNotAuthorized = "Authentication_Not_Authorized";
        public const string AuthenticationUserNotConfirmed = "Authentication_User_Not_Confirmed";
        
        // User & Password error codes
        public const string PasswordValidationError = "Password_Validation_Error";
        public const string SignUpError = "SignUp_Error";
        public const string SignUpErrorUsernameExists = "SignUp_Error_Username_Exists";
        public const string SignUpErrorInvalidParameter = "SignUp_Error_Invalid_Parameter";
        public const string ConfirmSignUpError = "Confirm_SignUp_Error";
        public const string ConfirmSignUpErrorUserNotFound = "Confirm_SignUp_Error_User_Not_Found";
        public const string ConfirmSignUpErrorUserAlreadyConfirmed = "Confirm_SignUp_Error_User_Already_Confirmed";
        public const string ConfirmSignUpErrorExpiredCode = "Confirm_SignUp_Error_Expired_Code";
        public const string AdminCreateUserError = "Create_User_Error";
        public const string AdminCreateUserErrorUserAlreadyExists = "Create_User_Error_User_Already_Exists";
        public const string AdminSetUserPasswordError = "Set_User_Password_Error";
        public const string AdminCreateUserErrorUsernameExists = "Admin_Create_User_Error_Username_Exists";
        public const string AdminSetUserPasswordErrorInvalidParameter = "Admin_Set_User_Password_Error_Invalid_Parameter";
        public const string ChangePasswordError = "Change_Password_Error";
        public const string ChangePasswordErrorLimitExceeded = "Change_Password_Error_Limit_Exceeded";
        public const string ChangePasswordErrorUnauthorised = "Change_Password_Error_Unauthorised";
        public const string ChangeEmailError = "Change_Email_Error";
        public const string ChangeEmailErrorInvalidParameter = "Change_Password_Error_Invalid_Parameter";
        public const string ChangeEmailErrorUnauthorised = "Change_Email_Error_Unauthorised";
        public const string UserIsUnderage = "User_Is_Underage";
        public const string UnableToMatchAccounts = "Unable_To_Match_Accounts";
        
        // MFA error codes
        public const string MfaSetupChallenge = "Mfa_Setup_Challenge";
        public const string MfaSmsChallenge = "Sms_Mfa_Challenge";
        public const string MfaSoftwareTokenChallenge = "Software_Token_Mfa_Challenge";
        public const string MfaCodeMismatch = "Mfa_Code_Mismatch";
        public const string MfaSessionExpired = "Mfa_Session_Expired";
        public const string MfaNoChallenge = "No_Mfa_Challenge";
        public const string MfaUserNotFound = "Mfa_User_Not_Found";
        public const string MfaCodeExpired = "Mfa_Code_Expired";
        public const string MfaReissueSession = "Mfa_Reissue_Session";


        // Verification error codes
        public const string ResendVerificationEmailError = "Resend_Verification_Email_Error";
        public const string ResendVerificationEmailErrorUserAlreadyConfirmed = "Resend_Verification_Email_Error_User_Already_Confirmed";
        
        // Forgot password error codes
        public const string ForgotPasswordError = "Forgot_Password_Error";
        public const string ForgotPasswordErrorUserIsNotConfirmed = "Forgot_Password_Error_User_Is_Not_Confirmed";
        public const string ConfirmForgotPasswordError = "Confirm_Forgot_Password_Error";
        
        // Demographics error codes
        public const string DemographicsNotFoundForParticipantError = "Demographics_Not_Found_For_Participant_Error";

        // Participant registration codes
        public const string DeleteParticipantAccountError = "Unable_To_Delete_Cognito_Account_Error";
        public const string ParticipantRegistrationAlreadyExistsForStudyError = "Participant_Registration_Already_Exists_For_Study_Error";
        
        // Study registration codes
        public const string StudyRegistrationAlreadyExistsError = "Study_Registration_Already_Exists_Error";
        public const string StudyRegistrationNotFoundError = "Study_Registration_Not_Found_Error";
        
        // Location codes
        public const string UnableToGetAddressesFromLocationServiceError = "Unable_To_Get_Addresses_From_LocationService_Error";
        public const string UnknownErrorGettingAddressesFromLocationServiceError = "Unknown_Error_Getting_Addresses_From_LocationService_Error";
    }
}