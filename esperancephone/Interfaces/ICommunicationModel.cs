using esperancephone.Models;

namespace esperancephone.Interfaces
{
    public interface ICommunicationModel
    {
        CommunicationType CommunicationType { get; set; }
        string DisplayName { get; set; }
        string PhoneNumber { get; set; }
        object Persona { get; set; }
    }
}