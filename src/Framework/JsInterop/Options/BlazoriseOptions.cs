namespace JSInterop.Options;

/// <summary>
/// Defines the global options.
/// </summary>
public class JSInteropOptions
{
    #region Members

    private readonly IServiceProvider serviceProvider;

    #endregion

    #region Constructors
    public JSInteropOptions(IServiceProvider serviceProvider, Action<JSInteropOptions> configureOptions)
    {
        this.serviceProvider = serviceProvider;
        configureOptions?.Invoke(this);
    }

    #endregion

    #region Properties

    /// <summary>
    /// Gets the service provider.
    /// </summary>
    public IServiceProvider Services => serviceProvider;

    /// <summary>
    /// Whether to safely invoke internal javascript. Will ignore any exceptions that might be thrown as part of the javascript invoke process.
    /// </summary>
    public bool SafeJsInvoke { get; set; } = true;

    #endregion
}