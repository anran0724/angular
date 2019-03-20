(function ($) {
    app.modals.EccpDictNodeTypeLookupTableModal = function () {

        var _modalManager;

        var _eccpMaintenanceTemplateNodesService = abp.services.app.eccpMaintenanceTemplateNodes;
        var _$eccpDictNodeTypeTable = $('#EccpDictNodeTypeTable');

        this.init = function (modalManager) {
            _modalManager = modalManager;
        };


        var dataTable = _$eccpDictNodeTypeTable.DataTable({
            paging: true,
            serverSide: true,
            processing: true,
            listAction: {
                ajaxFunction: _eccpMaintenanceTemplateNodesService.getAllEccpDictNodeTypeForLookupTable,
                inputFilter: function () {
                    return {
                        filter: $('#EccpDictNodeTypeTableFilter').val()
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

        $('#EccpDictNodeTypeTable tbody').on('click', '[id*=selectbtn]', function () {
            var data = dataTable.row($(this).parents('tr')).data();
            _modalManager.setResult(data);
            _modalManager.close();
        });

        function getEccpDictNodeType() {
            dataTable.ajax.reload();
        }

        $('#GetEccpDictNodeTypeButton').click(function (e) {
            e.preventDefault();
            getEccpDictNodeType();
        });

        $('#SelectButton').click(function (e) {
            e.preventDefault();
        });

        $(document).keypress(function (e) {
            if (e.which === 13) {
                getEccpDictNodeType();
            }
        });

    };
})(jQuery);

