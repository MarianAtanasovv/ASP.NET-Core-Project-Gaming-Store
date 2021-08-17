namespace GameStore.Services.Emails
{
    public interface IEmailSenderService
    {
        public void SendKeyAsync(string userId);
    }
}
