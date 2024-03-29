// Helpers/Settings.cs

using System.Diagnostics;
using esperancephone.Models;
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

        private const string CurrentPageCacheModelKey = "currentpagecachemodel_key";
        private static readonly string CurrentPageCacheModelDefault = string.Empty;

        private const string UserModeKey = "usermode_key";
        private static readonly UserMode UserModeDefault = UserMode.Basic;

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
                Debug.WriteLine($"INFORMATION: UserId = {AppSettings.GetValueOrDefault<string>(UserIdKey, UserIdDefault)}");
                return AppSettings.GetValueOrDefault<string>(UserIdKey, UserIdDefault);
            }
            set
            {
                AppSettings.AddOrUpdateValue<string>(UserIdKey, value);
            }
        }

      public static UserMode UserMode
      {
          get
          {
                Debug.WriteLine($"INFORMATION: UserMode = {AppSettings.GetValueOrDefault<UserMode>(UserModeKey, UserModeDefault)}");
                return AppSettings.GetValueOrDefault<UserMode>(UserModeKey, UserModeDefault);
          }
          set
          {
              AppSettings.AddOrUpdateValue<UserMode>(UserModeKey, value);
          }
        }

        public static string MobileServiceAuthenticationToken
        {
            get
            {
                Debug.WriteLine($"INFORMATION: MobileServiceAuthenticationToken = {AppSettings.GetValueOrDefault<string>(MobileServiceAuthenticationTokenKey, MobileServiceAuthenticationTokenDefault)}");
                return AppSettings.GetValueOrDefault<string>(MobileServiceAuthenticationTokenKey, MobileServiceAuthenticationTokenDefault);
            }
            set
            {
                AppSettings.AddOrUpdateValue<string>(MobileServiceAuthenticationTokenKey, value);
            }
        }

        /// <summary>
        /// Json value of CurrentPageCacheModel class instance.
        /// </summary>
        public static string CurrentPageCacheModel
        {
            get
            {
                Debug.WriteLine($"INFORMATION: CurrentPageCacheModel = {AppSettings.GetValueOrDefault<string>(CurrentPageCacheModelKey, CurrentPageCacheModelDefault)}");
                return AppSettings.GetValueOrDefault<string>(CurrentPageCacheModelKey, CurrentPageCacheModelDefault);
            }
            set
            {
                AppSettings.AddOrUpdateValue<string>(CurrentPageCacheModelKey, value);
            }
        }


    }
}