(function ($) {
    app.modals.ECCPBaseProductionCompanyLookupTableModal = function () {

        var _modalManager;

        var _eccpBaseElevatorsService = abp.services.app.eccpBaseElevators;
        var _$eCCPBaseProductionCompanyTable = $('#ECCPBaseProductionCompanyTable');

        this.init = function (modalManager) {
            _modalManager = modalManager;
        };


        var dataTable = _$eCCPBaseProductionCompanyTable.DataTable({
            paging: true,
            serverSide: true,
            processing: true,
            listAction: {
                ajaxFunction: _eccpBaseElevatorsService.getAllECCPBaseProductionCompanyForLookupTable,
                inputFilter: function () {
                    return {
                        filter: $('#ECCPBaseProductionCompanyTableFilter').val()
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

        $('#ECCPBaseProductionCompanyTable tbody').on('click', '[id*=selectbtn]', function () {
            var data = dataTable.row($(this).parents('tr')).data();
            _modalManager.setResult(data);
            _modalManager.close();
        });

        function getECCPBaseProductionCompany() {
            dataTable.ajax.reload();
        }

        $('#GetECCPBaseProductionCompanyButton').click(function (e) {
            e.preventDefault();
            getECCPBaseProductionCompany();
        });

        $('#SelectButton').click(function (e) {
            e.preventDefault();
        });

        $(document).keypress(function (e) {
            if (e.which === 13) {
                getECCPBaseProductionCompany();
            }
        });

    };
})(jQuery);

