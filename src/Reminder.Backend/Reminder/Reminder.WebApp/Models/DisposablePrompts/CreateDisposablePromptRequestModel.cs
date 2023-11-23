namespace Reminder.WebApp.Models.DisposablePrompts;

public class CreateDisposablePromptRequestModel
{
    public string Title { get; set; }
    public string? Description { get; set; }
    public DateTime ShowsAt { get; set; }
}