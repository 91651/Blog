namespace Blazor.HybridRender
{
    public enum HybridRenderMode
    {
        ServerSide,
        WebAssembly,
        HybridManual,
        HybridOnNavigation,
        HybridOnReady
    }

    public class HybridRenderOptions
    {
        public HybridRenderMode HybridRenderMode { get; set; }
    }
}