(function ($) {
    app.modals.EccpBaseElevatorModelLookupTableModal = function () {

        var _modalManager;

        var _eccpBaseElevatorsService = abp.services.app.eccpBaseElevators;
        var _$eccpBaseElevatorModelTable = $('#EccpBaseElevatorModelTable');

        var _args;
        this.init = function (modalManager, args) {
            _modalManager = modalManager;
            _args = args;
        };


        var dataTable = _$eccpBaseElevatorModelTable.DataTable({
            paging: true,
            serverSide: true,
            processing: true,
            listAction: {
                ajaxFunction: _eccpBaseElevatorsService.getAllEccpBaseElevatorModelForLookupTable,
                inputFilter: function () {
                    return {
                        filter: $('#EccpBaseElevatorModelTableFilter').val(),
                        parentId: _args.ParentId
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

        $('#EccpBaseElevatorModelTable tbody').on('click', '[id*=selectbtn]', function () {
            var data = dataTable.row($(this).parents('tr')).data();
            _modalManager.setResult(data);
            _modalManager.close();
        });

        function getEccpBaseElevatorModel() {
            dataTable.ajax.reload();
        }

        $('#GetEccpBaseElevatorModelButton').click(function (e) {
            e.preventDefault();
            getEccpBaseElevatorModel();
        });

        $('#SelectButton').click(function (e) {
            e.preventDefault();
        });

        $(document).keypress(function (e) {
            if (e.which === 13) {
                getEccpBaseElevatorModel();
            }
        });

    };
})(jQuery);

