(function ($) {
    app.modals.EccpMaintenanceTemplateNodeLookupTableModal = function () {

        var _modalManager;

        var _eccpMaintenanceWorksService = abp.services.app.eccpMaintenanceWorks;
        var _$eccpMaintenanceTemplateNodeTable = $('#EccpMaintenanceTemplateNodeTable');

        this.init = function (modalManager) {
            _modalManager = modalManager;
        };


        var dataTable = _$eccpMaintenanceTemplateNodeTable.DataTable({
            paging: true,
            serverSide: true,
            processing: true,
            listAction: {
                ajaxFunction: _eccpMaintenanceWorksService.getAllEccpMaintenanceTemplateNodeForLookupTable,
                inputFilter: function () {
                    return {
                        filter: $('#EccpMaintenanceTemplateNodeTableFilter').val()
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

        $('#EccpMaintenanceTemplateNodeTable tbody').on('click', '[id*=selectbtn]', function () {
            var data = dataTable.row($(this).parents('tr')).data();
            _modalManager.setResult(data);
            _modalManager.close();
        });

        function getEccpMaintenanceTemplateNode() {
            dataTable.ajax.reload();
        }

        $('#GetEccpMaintenanceTemplateNodeButton').click(function (e) {
            e.preventDefault();
            getEccpMaintenanceTemplateNode();
        });

        $('#SelectButton').click(function (e) {
            e.preventDefault();
        });

        $(document).keypress(function (e) {
            if (e.which === 13) {
                getEccpMaintenanceTemplateNode();
            }
        });

    };
})(jQuery);

