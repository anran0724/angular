(function ($) {
    app.modals.ECCPBaseAreaLookupTableModal = function () {

        var _modalManager;

        var _eCCPBaseRegisterCompaniesService = abp.services.app.eCCPBaseRegisterCompanies;
        var _$eCCPBaseAreaTable = $('#ECCPBaseAreaTable');

        this.init = function (modalManager) {
            _modalManager = modalManager;
        };

        var _args;

        this.init = function (modalManager, args) {
            _modalManager = modalManager;
            _args = args;
        };

        var dataTable = _$eCCPBaseAreaTable.DataTable({
            paging: true,
            serverSide: true,
            processing: true,
            listAction: {
                ajaxFunction: _eCCPBaseRegisterCompaniesService.getAllECCPBaseAreaForLookupTable,
                inputFilter: function () {
                    return {
                        filter: $('#ECCPBaseAreaTableFilter').val(),
                        parentId: _args.parentId
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

        $('#ECCPBaseAreaTable tbody').on('click', '[id*=selectbtn]', function () {
            var data = dataTable.row($(this).parents('tr')).data();
            _modalManager.setResult(data);
            _modalManager.close();
        });

        function getECCPBaseArea() {
            dataTable.ajax.reload();
        }

        $('#GetECCPBaseAreaButton').click(function (e) {
            e.preventDefault();
            getECCPBaseArea();
        });

        $('#SelectButton').click(function (e) {
            e.preventDefault();
        });

        $(document).keypress(function (e) {
            if (e.which === 13) {
                getECCPBaseArea();
            }
        });

    };
})(jQuery);

