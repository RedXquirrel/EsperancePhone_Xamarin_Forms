// Helpers/Settings.cs
using Plugin.Settings;
using Plugin.Settings.Abstractions;

namespace esperancephone.Helpers
{
  /// <summary>
  /// This is the Settings static class that can be used in your Core solution or in any
  /// of your client applications. All settings are laid out the same exact way with getters
  /// and setters. 
  /// </summary>
  public static class Settings
  {
        private static ISettings AppSettings
        {
          get
          {
            return CrossSettings.Current;
          }
        }

        #region Setting Constants
        private const string ApplicationNameKey = "applicationname_key";
        private static readonly string ApplicationNameDefault = string.Empty;

        private const string UserIdKey = "sip_key";
        private static readonly string UserIdDefault = string.Empty;

        private const string MobileServiceAuthenticationTokenKey = "mobileserviceauthenticationtoken_key";
        private static readonly string MobileServiceAuthenticationTokenDefault = string.Empty;
        #endregion

        public static string ApplicationName
        {
            get
            {
                return AppSettings.GetValueOrDefault<string>(ApplicationNameKey, ApplicationNameDefault);
            }
            set
            {
                AppSettings.AddOrUpdateValue<string>(ApplicationNameKey, value);
            }
        }

        public static string UserId
        {
            get
            {
                return AppSettings.GetValueOrDefault<string>(UserIdKey, UserIdDefault);
            }
            set
            {
                AppSettings.AddOrUpdateValue<string>(UserIdKey, value);
            }
        }

        public static string MobileServiceAuthenticationToken
        {
            get
            {
                return AppSettings.GetValueOrDefault<string>(MobileServiceAuthenticationTokenKey, MobileServiceAuthenticationTokenDefault);
            }
            set
            {
                AppSettings.AddOrUpdateValue<string>(MobileServiceAuthenticationTokenKey, value);
            }
        }
    }
}