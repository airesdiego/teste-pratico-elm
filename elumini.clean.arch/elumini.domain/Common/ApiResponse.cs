using FluentValidation.Results;
using System.Text.Json.Serialization;

namespace elumini.domain.Common;

public class ApiResponse<T> where T : class
{
    public ApiResponse() { }

    public ApiResponse(ValidationResult validationResult)
    {
        var _notificacoes = new List<NotificationDto>();

        foreach (var erro in validationResult.Errors)
            _notificacoes.Add(new NotificationDto(erro.ErrorCode, erro.ErrorMessage));

        Success = false;
        Notifications = _notificacoes.ToList();
    }

    public ApiResponse(bool success, T data, IList<NotificationDto> notifications, int statusCode)
    {
        Success = success;
        Data = data;
        Notifications = notifications;
        StatusCode = statusCode;
    }

    public ApiResponse(bool success, T data, NotificationDto notifications, int statusCode)
    {
        Success = success;
        Data = data;
        Notifications = new List<NotificationDto> { notifications };
        StatusCode = statusCode;
    }

    public ApiResponse(bool success, T data, int statusCode)
    {
        Success = success;
        Data = data;
        StatusCode = statusCode;
    }

    public ApiResponse(bool success, int statusCode)
    {
        Success = success;
        StatusCode = statusCode;
    }

    public ApiResponse(bool success, int statusCode, NotificationDto notificationData = null)
    {
        Success = success;
        StatusCode = statusCode;
        Notifications = new List<NotificationDto> { notificationData };
    }

    public ApiResponse(bool success, int statusCode, IList<NotificationDto> notificationData = null)
    {
        Success = success;
        StatusCode = statusCode;
        Notifications = notificationData;
    }

    public bool Success { get; set; }
    public T Data { get; set; }
    public IList<NotificationDto> Notifications { get; set; }

    [JsonIgnore]        
    public int StatusCode { get; set; }
}
