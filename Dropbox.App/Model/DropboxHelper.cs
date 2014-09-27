using System.Configuration;
using DropNet;
using DropNet.Models;

namespace Dropbox.App.Model
{
    public class DropboxHelper
    {
        #region PRIVATE

        private readonly string _appKey;
        private readonly string _appSecret;
        private DropNetClient _client;
        private UserLogin _usLogin;

        public DropNetClient Client
        {
            get { return _client; }
        }

        #endregion PRIVATE

        #region CONSTRUCTOR

        public DropboxHelper()
        {
            _appKey = ConfigurationManager.AppSettings.Get("appkey");
            _appSecret = ConfigurationManager.AppSettings.Get("appsecret");

            InitClient();
        }

        #endregion CONSTRUCTOR

        #region METHODS

        public void InitClient()
        {
            string accessToken = RegistryHelper.Read("AccessToken");
            string secret = RegistryHelper.Read("Secret");

            if (!string.IsNullOrEmpty(accessToken) && !string.IsNullOrEmpty(secret))
            {
                _client = new DropNetClient(_appKey, _appSecret, accessToken, secret);
            }
            else
            {
                _client = new DropNetClient(_appKey, _appSecret);
            }
        }

        public string GetAuthorizationUrl()
        {
            if (_client.UserLogin == null)
            {
                string url = _client.GetTokenAndBuildUrl();
                return url;
            }
            return string.Empty;
        }

        public bool GetAccessToken()
        {
            bool isSuccess = false;

            _usLogin = _client.GetAccessToken();
            if (_usLogin != null && !string.IsNullOrEmpty(_usLogin.Token) && !string.IsNullOrEmpty(_usLogin.Secret))
            {
                isSuccess = RegistryHelper.Write("AccessToken", _usLogin.Token) &&
                            RegistryHelper.Write("Secret", _usLogin.Secret);
            }

            return isSuccess;
        }

        #endregion METHODS
    }
}