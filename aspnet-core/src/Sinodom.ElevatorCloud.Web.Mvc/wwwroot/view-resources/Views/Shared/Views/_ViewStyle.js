(function ($) {
    app.modals.ViewStyleModal = function () {
        var id = $('#EccpElevatorName,#UserName');
        for (var j = 0; j < id.length; j++) {
            var txt = $('#' + id[j].getAttribute('id')).text();
            var txtArr = txt.split(",");
            //console.log(txtArr);
            $('#' + id[j].getAttribute('id')).empty();
            var html = '';
            for (var i = 0; i < txtArr.length; i++) {
                if (txtArr[i].length < 6) {
                    html = '<p style="width:15%;display:inline-block;text-align:left;float:left;">' + txtArr[i] + '</p>';
                    $('#' + id[j].getAttribute('id')).prepend(html);
                } else if (txtArr[i].length < 21) {
                    html = '<p style="width:50%;display:inline-block;text-align:left;float:left;">' + txtArr[i] + '</p>';
                    $('#' + id[j].getAttribute('id')).prepend(html);
                } else {
                    html = '<p style="width:100%;display:inline-block;text-align:left;float:left;"><span>' +
                        txtArr[i] +
                        '</p>';
                    $('#' + id[j].getAttribute('id')).append(html);
                }
            }
        }
    }
})(jQuery);