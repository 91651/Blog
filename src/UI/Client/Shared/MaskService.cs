namespace Blog.Client.Shared;

public class MaskService
{
    public Action Show { get; set; } = default!;
    public Action Hide { get; set; } = default!;
    public Action? OnShow { get; set; }
    public Action? OnHide { get; set; }
}