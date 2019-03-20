(function ($) {
    app.modals.EccpDictElevatorTypeLookupTableModal = function () {

        var _modalManager;

        var _eccpBaseElevatorsService = abp.services.app.eccpBaseElevators;
        var _$eccpDictElevatorTypeTable = $('#EccpDictElevatorTypeTable');

        this.init = function (modalManager) {
            _modalManager = modalManager;
        };


        var dataTable = _$eccpDictElevatorTypeTable.DataTable({
            paging: true,
            serverSide: true,
            processing: true,
            listAction: {
                ajaxFunction: _eccpBaseElevatorsService.getAllEccpDictElevatorTypeForLookupTable,
                inputFilter: function () {
                    return {
                        filter: $('#EccpDictElevatorTypeTableFilter').val()
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

        $('#EccpDictElevatorTypeTable tbody').on('click', '[id*=selectbtn]', function () {
            var data = dataTable.row($(this).parents('tr')).data();
            _modalManager.setResult(data);

            console.log(111);

            _modalManager.close();
        });

        function getEccpDictElevatorType() {
            dataTable.ajax.reload();
        }

        $('#GetEccpDictElevatorTypeButton').click(function (e) {
            e.preventDefault();
            getEccpDictElevatorType();
        });

        $('#SelectButton').click(function (e) {
            e.preventDefault();
        });

        $(document).keypress(function (e) {
            if (e.which === 13) {
                getEccpDictElevatorType();
            }
        });

    };
})(jQuery);

