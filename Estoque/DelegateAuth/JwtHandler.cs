using System.Net.Http.Headers;

namespace Estoque.DelegateAuth
{
    public class JwtHandler : DelegatingHandler
    {
        private readonly IHttpContextAccessor _accessor;

        public JwtHandler(IHttpContextAccessor accessor)
        {
            _accessor = accessor;
        }

        protected override Task<HttpResponseMessage> SendAsync(
            HttpRequestMessage request,
            CancellationToken cancellationToken)
        {
            var token = _accessor.HttpContext?
                .Request.Cookies["jwt"];

            if (!string.IsNullOrEmpty(token))
            {
                request.Headers.Authorization =
                    new AuthenticationHeaderValue("Bearer", token);
            }

            return base.SendAsync(request, cancellationToken);
        }
    }
}
