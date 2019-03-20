(function ($) {
    app.modals.EccpBaseElevatorBrandLookupTableModal = function () {

        var _modalManager;

        var _eccpBaseElevatorModelsService = abp.services.app.eccpBaseElevatorModels;
        var _$eccpBaseElevatorBrandTable = $('#EccpBaseElevatorBrandTable');

        this.init = function (modalManager) {
            _modalManager = modalManager;
        };


        var dataTable = _$eccpBaseElevatorBrandTable.DataTable({
            paging: true,
            serverSide: true,
            processing: true,
            listAction: {
                ajaxFunction: _eccpBaseElevatorModelsService.getAllEccpBaseElevatorBrandForLookupTable,
                inputFilter: function () {
                    return {
                        filter: $('#EccpBaseElevatorBrandTableFilter').val()
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

        $('#EccpBaseElevatorBrandTable tbody').on('click', '[id*=selectbtn]', function () {
            var data = dataTable.row($(this).parents('tr')).data();
            _modalManager.setResult(data);
            _modalManager.close();
        });

        function getEccpBaseElevatorBrand() {
            dataTable.ajax.reload();
        }

        $('#GetEccpBaseElevatorBrandButton').click(function (e) {
            e.preventDefault();
            getEccpBaseElevatorBrand();
        });

        $('#SelectButton').click(function (e) {
            e.preventDefault();
        });

        $(document).keypress(function (e) {
            if (e.which === 13) {
                getEccpBaseElevatorBrand();
            }
        });

    };
})(jQuery);

