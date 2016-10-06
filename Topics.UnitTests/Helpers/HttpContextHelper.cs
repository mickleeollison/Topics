using System;
using System.Web;

namespace Topics.UnitTests.Helpers
{
    public class HttpContextHelper : IDisposable
    {
        private FakeHttpContext.FakeHttpContext _fake;
        private bool disposedValue = false;

        public HttpContextHelper()
        {
            if (HttpContext.Current == null)
            {
                _fake = new FakeHttpContext.FakeHttpContext();
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    _fake.Dispose();
                }
                disposedValue = true;
            }
        }
    }
}
