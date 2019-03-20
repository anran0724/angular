(function ($) {
    app.modals.ECCPDictElevatorStatusLookupTableModal = function () {

        var _modalManager;

        var _eccpBaseElevatorsService = abp.services.app.eccpBaseElevators;
        var _$eCCPDictElevatorStatusTable = $('#ECCPDictElevatorStatusTable');

        this.init = function (modalManager) {
            _modalManager = modalManager;
        };


        var dataTable = _$eCCPDictElevatorStatusTable.DataTable({
            paging: true,
            serverSide: true,
            processing: true,
            listAction: {
                ajaxFunction: _eccpBaseElevatorsService.getAllECCPDictElevatorStatusForLookupTable,
                inputFilter: function () {
                    return {
                        filter: $('#ECCPDictElevatorStatusTableFilter').val()
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

        $('#ECCPDictElevatorStatusTable tbody').on('click', '[id*=selectbtn]', function () {
            var data = dataTable.row($(this).parents('tr')).data();
            _modalManager.setResult(data);
            _modalManager.close();
        });

        function getECCPDictElevatorStatus() {
            dataTable.ajax.reload();
        }

        $('#GetECCPDictElevatorStatusButton').click(function (e) {
            e.preventDefault();
            getECCPDictElevatorStatus();
        });

        $('#SelectButton').click(function (e) {
            e.preventDefault();
        });

        $(document).keypress(function (e) {
            if (e.which === 13) {
                getECCPDictElevatorStatus();
            }
        });

    };
})(jQuery);

