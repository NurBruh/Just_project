namespace Just_project.Models
{
    public interface IMessage
    {
        public bool sendMessage(string to, string messageBody, string subject);
    }

}
