namespace esperancephone.Models
{
    public class SendModel
    {
        public CommunicationType SendType { get; set; }
        public string DisplayName { get; set; }
        public string PhoneNumber { get; set; }
        public object Persona { get; set; }
    }
}