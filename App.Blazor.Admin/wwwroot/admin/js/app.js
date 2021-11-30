// 动态加载JS
window.appLoadJS = function (url) {
    script = document.createElement("script");
    script.src = url;
    document.body.appendChild(script);
}