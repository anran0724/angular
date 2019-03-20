(function ($) {
    app.modals.EccpDictMaintenanceTypeLookupTableModal = function () {

        var _modalManager;

        var _eccpMaintenanceTemplatesService = abp.services.app.eccpMaintenanceTemplates;
        var _$eccpDictMaintenanceTypeTable = $('#EccpDictMaintenanceTypeTable');

        this.init = function (modalManager) {
            _modalManager = modalManager;
        };


        var dataTable = _$eccpDictMaintenanceTypeTable.DataTable({
            paging: true,
            serverSide: true,
            processing: true,
            listAction: {
                ajaxFunction: _eccpMaintenanceTemplatesService.getAllEccpDictMaintenanceTypeForLookupTable,
                inputFilter: function () {
                    return {
                        filter: $('#EccpDictMaintenanceTypeTableFilter').val()
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

        $('#EccpDictMaintenanceTypeTable tbody').on('click', '[id*=selectbtn]', function () {
            var data = dataTable.row($(this).parents('tr')).data();
            _modalManager.setResult(data);
            _modalManager.close();
        });

        function getEccpDictMaintenanceType() {
            dataTable.ajax.reload();
        }

        $('#GetEccpDictMaintenanceTypeButton').click(function (e) {
            e.preventDefault();
            getEccpDictMaintenanceType();
        });

        $('#SelectButton').click(function (e) {
            e.preventDefault();
        });

        $(document).keypress(function (e) {
            if (e.which === 13) {
                getEccpDictMaintenanceType();
            }
        });

    };
})(jQuery);

