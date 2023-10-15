namespace Architecture.Application.Domain.Models.Auth
{
    internal class TokenFlowModel
    {
        public string token_type { get; set; }
        public string access_token { get; set; }
    }
}