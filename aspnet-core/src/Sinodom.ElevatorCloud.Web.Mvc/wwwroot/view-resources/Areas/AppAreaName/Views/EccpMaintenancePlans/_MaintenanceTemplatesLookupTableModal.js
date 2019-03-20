(function ($) {
    app.modals.MaintenanceTemplatesLookupTableModal = function () {

        var _modalManager;

        var _eccpMaintenancePlansService = abp.services.app.eccpMaintenancePlans;
        var _$maintenanceTemplatesTable = $('#MaintenanceTemplatesTable');
        var _maintenanceType;

        this.init = function (modalManager, maintenanceType) {
            _modalManager = modalManager;
            _maintenanceType = maintenanceType;

        };


        var dataTable = _$maintenanceTemplatesTable.DataTable({
            paging: true,
            serverSide: true,
            processing: true,
            listAction: {
                ajaxFunction: _eccpMaintenancePlansService.getAllMaintenanceTemplatesForLookupTable,
                inputFilter: function () {
                    return {
                        filter: $('#MaintenanceTemplatesTableFilter').val(),
                        maintenanceTypeId: _maintenanceType.maintenanceTypeId
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
                },
                {
                    autoWidth: false,
                    orderable: false,
                    targets: 2,
                    data: "isDefault",
                    render: function (isDefault) {
                        return isDefault ? app.localize('Yes') : app.localize('No');
                    }
                }
            ]
        });


        $('#MaintenanceTemplatesTable tbody').on('click', '[id*=selectbtn]', function () {
            var data = dataTable.row($(this).parents('tr')).data();
            _modalManager.setResult(data);
            _modalManager.close();
        });

        function getMaintenanceTemplates() {
            dataTable.ajax.reload();
        }

        $('#GetMaintenanceTemplatesButton').click(function (e) {
            e.preventDefault();
            getMaintenanceTemplates();
        });

        $('#SelectButton').click(function (e) {
            e.preventDefault();
        });

        $(document).keypress(function (e) {
            if (e.which === 13) {
                getMaintenanceTemplates();
            }
        });

    };
})(jQuery);

