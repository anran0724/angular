(function ($) {
    app.modals.EccpBaseElevatorLookupTableModal = function () {

        var _modalManager;

        var _elevatorClaimLogsService = abp.services.app.elevatorClaimLogs;
        var _$eccpBaseElevatorTable = $('#EccpBaseElevatorTable');

        this.init = function (modalManager) {
            _modalManager = modalManager;
        };


        var dataTable = _$eccpBaseElevatorTable.DataTable({
            paging: true,
            serverSide: true,
            processing: true,
            listAction: {
                ajaxFunction: _elevatorClaimLogsService.getAllEccpBaseElevatorForLookupTable,
                inputFilter: function () {
                    return {
                        filter: $('#EccpBaseElevatorTableFilter').val()
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

        $('#EccpBaseElevatorTable tbody').on('click', '[id*=selectbtn]', function () {
            var data = dataTable.row($(this).parents('tr')).data();
            _modalManager.setResult(data);
            _modalManager.close();
        });

        function getEccpBaseElevator() {
            dataTable.ajax.reload();
        }

        $('#GetEccpBaseElevatorButton').click(function (e) {
            e.preventDefault();
            getEccpBaseElevator();
        });

        $('#SelectButton').click(function (e) {
            e.preventDefault();
        });

        $(document).keypress(function (e) {
            if (e.which === 13) {
                getEccpBaseElevator();
            }
        });

    };
})(jQuery);

