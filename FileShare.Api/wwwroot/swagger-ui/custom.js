window.addEventListener("load", () => {
    let c = function () {
        return {
            fn: {
                opsFilter: (a, b) => a.filter((_, a) => -1 !== a.toLowerCase().indexOf(b.toLowerCase()))
            }
        }
    }, b = window.SwaggerUIBundle;
    function a(a) {
        return a.presets.push(c),
            b(...arguments)
    }
    a.__proto__ = b,
        window.SwaggerUIBundle = a
});