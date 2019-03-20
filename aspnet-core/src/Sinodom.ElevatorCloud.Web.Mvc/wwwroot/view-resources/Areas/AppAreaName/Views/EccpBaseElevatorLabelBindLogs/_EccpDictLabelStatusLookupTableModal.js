(function ($) {
    app.modals.EccpDictLabelStatusLookupTableModal = function () {

        var _modalManager;

        var _eccpBaseElevatorLabelBindLogsService = abp.services.app.eccpBaseElevatorLabelBindLogs;
        var _$eccpDictLabelStatusTable = $('#EccpDictLabelStatusTable');

        this.init = function (modalManager) {
            _modalManager = modalManager;
        };


        var dataTable = _$eccpDictLabelStatusTable.DataTable({
            paging: true,
            serverSide: true,
            processing: true,
            listAction: {
                ajaxFunction: _eccpBaseElevatorLabelBindLogsService.getAllEccpDictLabelStatusForLookupTable,
                inputFilter: function () {
                    return {
                        filter: $('#EccpDictLabelStatusTableFilter').val()
                    };
                }
            },
            columnDefs: [
                {
                    targets: 0,
                    data: null,
                    orderable: false,
                    autoWidth: false,
                    defaultContent: "<div class=\"text-center\"><input id='selectbtn' class='btn btn-success' type='button' width='25px' value='" + app.localize('Select') + "' /></div>"
                },
                {
                    autoWidth: false,
                    orderable: false,
                    targets: 1,
                    data: "displayName"
                }
            ]
        });

        $('#EccpDictLabelStatusTable tbody').on('click', '[id*=selectbtn]', function () {
            var data = dataTable.row($(this).parents('tr')).data();
            _modalManager.setResult(data);
            _modalManager.close();
        });

        function getEccpDictLabelStatus() {
            dataTable.ajax.reload();
        }

        $('#GetEccpDictLabelStatusButton').click(function (e) {
            e.preventDefault();
            getEccpDictLabelStatus();
        });

        $('#SelectButton').click(function (e) {
            e.preventDefault();
        });

        $(document).keypress(function (e) {
            if (e.which === 13) {
                getEccpDictLabelStatus();
            }
        });

    };
})(jQuery);

