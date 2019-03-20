(function ($) {
    app.modals.ECCPDictTempWorkOrderTypeLookupTableModal = function () {

        var _modalManager;

        var _eccpMaintenanceTempWorkOrdersService = abp.services.app.eccpMaintenanceTempWorkOrders;
        var _$eccpDictTempWorkOrderTypeTable = $('#ECCPDictTempWorkOrderTypeTable');
        this.init = function (modalManager) {
            _modalManager = modalManager;
        };


      
        var dataTable = _$eccpDictTempWorkOrderTypeTable.DataTable({
            paging: true,
            serverSide: true,
            processing: true,
            listAction: {
                ajaxFunction: _eccpMaintenanceTempWorkOrdersService.getAllECCPDictTempWorkOrderTypeForLookupTable,
                inputFilter: function () {
                    return {
                        filter: $('#ECCPDictTempWorkOrderTypeTableFilter').val()
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

        $('#ECCPDictTempWorkOrderTypeTable tbody').on('click', '[id*=selectbtn]', function () {
            var data = dataTable.row($(this).parents('tr')).data();
            _modalManager.setResult(data);
            _modalManager.close();
        });

        function getECCPBaseMaintenanceCompany() {
            dataTable.ajax.reload();
        }

        $('#GetECCPDictTempWorkOrderTypeButton').click(function (e) {
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

