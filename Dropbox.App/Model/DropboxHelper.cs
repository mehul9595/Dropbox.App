using System;
using System.Configuration;
using System.IO;
using System.Net;
using DropNet;
using DropNet.Models;

namespace Dropbox.App.Model
{
    public class DropboxHelper
    {
        #region PRIVATE

        private DropNetClient _client;
        private UserLogin _usLogin;
        private readonly string _appKey;
        private readonly string _appSecret;

        public DropNetClient Client
        {
            get { return _client; }
        }

        #endregion PRIVATE

        public DropboxHelper()
        {
            _appKey = ConfigurationManager.AppSettings.Get("appkey");
            _appSecret = ConfigurationManager.AppSettings.Get("appsecret");

            InitClient();
        }

        public void InitClient()
        {
            var accessToken = RegistryHelper.Read("AccessToken");
            var secret = RegistryHelper.Read("Secret");
//            RegistryHelper.Write("AccessToken", "dglqz3cbm4nqv9h8");
//            RegistryHelper.Write("Secret", "nvl4wf0135njsro");
            if (!string.IsNullOrEmpty(accessToken))
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
            else
            {
                return string.Empty;
            }
        }

        public bool GetAccessToken()
        {
            bool isSuccess = false;

            _usLogin = _client.GetAccessToken();
            if (_usLogin!=null && !string.IsNullOrEmpty(_usLogin.Token) && !string.IsNullOrEmpty(_usLogin.Secret))
            {
              isSuccess =  RegistryHelper.Write("AccessToken", _usLogin.Token) && RegistryHelper.Write("Secret", _usLogin.Secret);
            }
            
            return isSuccess;
        }

        public void TestCall()
        {
            var files = _client.Search("a");
        }

    }
}