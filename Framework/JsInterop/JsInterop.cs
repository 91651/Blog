using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace JsInterop
{
    // https://github.com/Megabit/Blazorise/

    public class JsInterop : IAsyncDisposable
    {
        private readonly Lazy<Task<IJSObjectReference>> moduleTask;

        public JsInterop(IJSRuntime jsRuntime)
        {
            moduleTask = new(() => jsRuntime.InvokeAsync<IJSObjectReference>(
                "import", "./_content/JsInterop/js-interop.js.js").AsTask());
        }

        public virtual ValueTask<DomElement> GetElementInfo(ElementReference elementRef, string elementId)
            => InvokeSafeAsync<DomElement>("getElementInfo", elementRef, elementId);

        public async ValueTask DisposeAsync()
        {
            if (moduleTask.IsValueCreated)
            {
                var module = await moduleTask.Value;
                await module.DisposeAsync();
            }
        }
    }
}
