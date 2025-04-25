window.BlazorApp = {
    loadJS : function (url, objRef, callback) {
        script = document.createElement("script");
        script.src = url;
        script.addEventListener('load', e => {
            objRef.invokeMethodAsync(callback);
        });
        document.body.appendChild(script);
    },
    loadJSWithWaiting : function (url) {
        return new Promise((resolve, reject) => {
            script = document.createElement("script");
            script.src = url;
            script.addEventListener('load', e => {
                resolve();
            });
            document.body.appendChild(script);
        });
    }
}
