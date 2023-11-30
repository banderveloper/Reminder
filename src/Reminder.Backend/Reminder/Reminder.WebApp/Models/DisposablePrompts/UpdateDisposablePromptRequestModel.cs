namespace Reminder.WebApp.Models.DisposablePrompts;

public class UpdateDisposablePromptRequestModel
{
    public long Id { get; set; }
    public string Title { get; set; }
    public string? Description { get; set; }
    public DateTime ShowsAt { get; set; }
}