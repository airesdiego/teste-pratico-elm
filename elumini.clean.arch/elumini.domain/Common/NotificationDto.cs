namespace elumini.domain.Common;

public class NotificationDto
{
    public NotificationDto(string code, string message)
    {
        Code = code;
        Message = message;
    }

    public string Code { get; set; }
    public string Message { get; set; }
}
