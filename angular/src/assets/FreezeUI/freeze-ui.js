// repository: https://github.com/alexradulescu/FreezeUI
// Converted to ES5 manually.
'use strict';

(function () {

    /**
     * Setup the freeze element to be appended
     */
    var freezeHtml = document.createElement('div');
    freezeHtml.classList.add('freeze-ui');

    /** 
    * Freezes the UI
    * options = { 
    *   selector: '.class-name' -> Choose an element where to limit the freeze or leave empty to freeze the whole body. Make sure the element has position relative or absolute,
    *   text: 'Magic is happening' -> Choose any text to show or use the default "Loading". Be careful for long text as it will break the design.
    * }
    */
    window.FreezeUI = function () {
        var options = arguments.length > 0 && arguments[0] !== undefined ? arguments[0] : {};

        var parent = document.querySelector(options.selector) || document.body;
        freezeHtml.setAttribute('data-text', options.text || 'Loading');
        if (document.querySelector(options.selector)) {
            freezeHtml.style.position = 'absolute';
        }
        parent.appendChild(freezeHtml);
    };

    /**
     * Unfreezes the UI.
     * No options here.
     */
    window.UnFreezeUI = function () {
        var element = document.querySelector('.freeze-ui');
        if (element) {
            element.classList.add('is-unfreezing');
            setTimeout(function () {
                if (element) {
                    element.classList.remove('is-unfreezing');
                    if (element.parentElement) {
                        element.parentElement.removeChild(element);
                    }
                }
            }, 250);
        }
    };
})();
