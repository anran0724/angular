(function ($) {
    app.modals.LanFlowSchemeLookupTableModal = function () {

        var _modalManager;

        var _lanFlowStatusActionsService = abp.services.app.lanFlowStatusActions;
        var _$lanFlowSchemeTable = $('#LanFlowSchemeTable');

        this.init = function (modalManager) {
            _modalManager = modalManager;
        };


        var dataTable = _$lanFlowSchemeTable.DataTable({
            paging: true,
            serverSide: true,
            processing: true,
            listAction: {
                ajaxFunction: _lanFlowStatusActionsService.getAllLanFlowSchemeForLookupTable,
                inputFilter: function () {
                    return {
                        filter: $('#LanFlowSchemeTableFilter').val()
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

        $('#LanFlowSchemeTable tbody').on('click', '[id*=selectbtn]', function () {
            var data = dataTable.row($(this).parents('tr')).data();
            _modalManager.setResult(data);
            _modalManager.close();
        });

        function getLanFlowScheme() {
            dataTable.ajax.reload();
        }

        $('#GetLanFlowSchemeButton').click(function (e) {
            e.preventDefault();
            getLanFlowScheme();
        });

        $('#SelectButton').click(function (e) {
            e.preventDefault();
        });

        $(document).keypress(function (e) {
            if (e.which === 13) {
                getLanFlowScheme();
            }
        });

    };
})(jQuery);

