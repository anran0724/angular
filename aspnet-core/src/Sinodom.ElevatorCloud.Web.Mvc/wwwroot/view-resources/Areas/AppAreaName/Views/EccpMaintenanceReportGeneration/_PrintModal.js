(function ($) {
    app.modals.PrintModal = function () {

        $('#_print').click(function () {
            var obj = document.getElementById('EccpDictMaintenanceItemInformationsTab');
            var new_iframe = document.createElement('IFRAME');
            var doc = null;
            new_iframe.setAttribute('style', 'width:0px;height:0px;position:absolute;left:-2000px;top:-2000px;');
            new_iframe.setAttribute('align', 'center');
            document.body.appendChild(new_iframe);
            doc = new_iframe.contentWindow.document;
            doc.write('<div style="width:100%;height:auto;min-width:900px;margin:0px auto;"align="center">' + obj.innerHTML + '</div>');
            doc.close();
            new_iframe.contentWindow.focus();
            new_iframe.contentWindow.print();
        });
    };
})(jQuery);