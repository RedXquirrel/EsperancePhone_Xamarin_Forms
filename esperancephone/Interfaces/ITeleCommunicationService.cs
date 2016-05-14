namespace esperancephone.Interfaces
{
    public interface ITeleCommunicationService
    {
        ICommunicationModel CurrentSession { get; set; }
        IDialService DialService { get; }
        void SetDialService(IDialService service);
    }
}