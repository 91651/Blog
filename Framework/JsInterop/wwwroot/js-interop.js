export function getElementInfo(element, elementId) {
    if (!element || (element && elementId && element.id !== elementId)) {
        element = document.getElementById(elementId);
    }

    if (element) {
        const position = element.getBoundingClientRect();

        return {
            boundingClientRect: {
                x: position.x,
                y: position.y,
                top: position.top,
                bottom: position.bottom,
                left: position.left,
                right: position.right,
                width: position.width,
                height: position.height
            },
            offsetTop: element.offsetTop,
            offsetLeft: element.offsetLeft,
            offsetWidth: element.offsetWidth,
            offsetHeight: element.offsetHeight,
            scrollTop: element.scrollTop,
            scrollLeft: element.scrollLeft,
            scrollWidth: element.scrollWidth,
            scrollHeight: element.scrollHeight,
            clientTop: element.clientTop,
            clientLeft: element.clientLeft,
            clientWidth: element.clientWidth,
            clientHeight: element.clientHeight
        };
    }

    return {};
}