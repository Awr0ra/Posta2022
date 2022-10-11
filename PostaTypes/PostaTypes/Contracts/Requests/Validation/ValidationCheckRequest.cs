
namespace PostaTypes.Contracts.Requests.Validation
{
    public record ValidationCheckRequest(
        string Name,
        string Email,
        DateTime DateSend,
        DateTime DateWork,
        List<string> Reciepients);
}
