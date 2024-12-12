using Microsoft.JSInterop;

namespace JSInterop;

/// <summary>
/// Base contract for all JS modules.
/// </summary>
public interface IBaseJSModule
{
    /// <summary>
    /// Gets the module file name.
    /// </summary>
    string ModuleFileName { get; }

    /// <summary>
    /// Gets the awaitable <see cref="IJSObjectReference"/> task.
    /// </summary>
    Task<IJSObjectReference> Module { get; }
}