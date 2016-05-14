using esperancephone.Interfaces;

namespace esperancephone.Models
{
    public class CommunicationModel : ICommunicationModel
    {
        public CommunicationType CommunicationType { get; set; }
        public string DisplayName { get; set; }
        public string PhoneNumber { get; set; }
        public object Persona { get; set; }
    }
}