using esperancephone.Interfaces;

namespace esperancephone.Services
{
    public class TelecommunicationService : ITeleCommunicationService
    {
        public ICommunicationModel CurrentSession { get; set; }
        public IDialService DialService { get; private set; }
        public void SetDialService(IDialService service)
        {
            DialService = service;
        }
    }
}