(function ($) {
    app.modals.ECCPBaseMaintenanceCompanyLookupTableModal = function () {

        var _modalManager;

        var _eccpMaintenanceCompanyChangeLogsService = abp.services.app.eccpMaintenanceCompanyChangeLogs;
        var _$eCCPBaseMaintenanceCompanyTable = $('#ECCPBaseMaintenanceCompanyTable');

        this.init = function (modalManager) {
            _modalManager = modalManager;
        };


        var dataTable = _$eCCPBaseMaintenanceCompanyTable.DataTable({
            paging: true,
            serverSide: true,
            processing: true,
            listAction: {
                ajaxFunction: _eccpMaintenanceCompanyChangeLogsService.getAllECCPBaseMaintenanceCompanyForLookupTable,
                inputFilter: function () {
                    return {
                        filter: $('#ECCPBaseMaintenanceCompanyTableFilter').val()
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

        $('#ECCPBaseMaintenanceCompanyTable tbody').on('click', '[id*=selectbtn]', function () {
            var data = dataTable.row($(this).parents('tr')).data();
            _modalManager.setResult(data);
            _modalManager.close();
        });

        function getECCPBaseMaintenanceCompany() {
            dataTable.ajax.reload();
        }

        $('#GetECCPBaseMaintenanceCompanyButton').click(function (e) {
            e.preventDefault();
            getECCPBaseMaintenanceCompany();
        });

        $('#SelectButton').click(function (e) {
            e.preventDefault();
        });

        $(document).keypress(function (e) {
            if (e.which === 13) {
                getECCPBaseMaintenanceCompany();
            }
        });

    };
})(jQuery);

