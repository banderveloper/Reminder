namespace Reminder.Application;

public class Result<TData>
{
    public ErrorCode? ErrorCode { get; set; }
    public TData Data { get; set; }
    public bool Succeed => ErrorCode is null;

    public static Result<TData> Success(TData data) => new() { Data = data };
    public static Result<TData> Error(ErrorCode errorCode) => new() { ErrorCode = errorCode };
}

public record struct None;