// 动态加载JS
window.appLoadJS = function (url, objRef, callback) {
    script = document.createElement("script");
    script.src = url;
    script.addEventListener('load', e => {
        objRef.invokeMethodAsync(callback);
        console.log("aaaaaaaabbbbbb------------------1");
    });
    document.body.appendChild(script);
}

window.appLoadJSWithWaiting = function (url) {
    return new Promise((resolve, reject) => {
        script = document.createElement("script");
        script.src = url;
        script.addEventListener('load', e => {
            resolve();
            console.log("aaaaaaaabbbbbb------------------2");
        });
        document.body.appendChild(script);
    });
}