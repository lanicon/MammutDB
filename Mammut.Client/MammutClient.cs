using Mammut.Client.API;
using Mammut.Common.Payload.Model;
using System;
using System.Net.Http;

namespace Mammut.Client
{
    public class MammutClient : IDisposable
    {
        public HttpClient Client { get; private set; }
        public LoginToken Token { get; set; }
        public SecurityClient Security { get; private set; }
        public SchemaClient Schema { get; private set; }
        public TransactionClient Transaction { get; private set; }
        public DocumentClient Document { get; private set; }
        public QueryClient Query { get; private set; }

        protected void Initialize(string baseAddress, TimeSpan commandTimeout, string username = "", string password = "")
        {
            Client = new HttpClient();
            Client.BaseAddress = new Uri(baseAddress);
            Client.Timeout = commandTimeout;

            Token = null;

            Security = new SecurityClient(this);
            Schema = new SchemaClient(this);
            Transaction = new TransactionClient(this);
            Document = new DocumentClient(this);
            Query = new QueryClient(this);

            if (string.IsNullOrWhiteSpace(username) == false)
            {
                Security.Login(username, password);
            }
        }

        #region ~CTor

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                // get rid of managed resources:
                if (this.Token.IsValid)
                {
                    this.Logout();
                }
            }
            // get rid of unmanaged resources:
        }

        public MammutClient(string baseAddress, TimeSpan commandTimeout)
        {
            Initialize(baseAddress, commandTimeout);
        }

        public MammutClient(string baseAddress)
        {
            Initialize(baseAddress, new TimeSpan(0, 0, 1, 0, 0));
        }

        public MammutClient(string baseAddress, string username, string password)
        {
            Initialize(baseAddress, new TimeSpan(0, 0, 1, 0, 0), username, password);

        }

        public MammutClient(string baseAddress, TimeSpan commandTimeout, string username, string password)
        {
            Initialize(baseAddress, commandTimeout, username, password);
        }

        #endregion

        protected LoginToken Login(string username, string password)
        {
            return Security.Login(username, password);
        }

        protected void Logout()
        {
            Security.Logout();
        }
    }
}
