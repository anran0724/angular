(function ($) {
    app.modals.ECCPBaseRegisterCompanyLookupTableModal = function () {

        var _modalManager;

        var _eccpBaseElevatorsService = abp.services.app.eccpBaseElevators;
        var _$eCCPBaseRegisterCompanyTable = $('#ECCPBaseRegisterCompanyTable');

        this.init = function (modalManager) {
            _modalManager = modalManager;
        };


        var dataTable = _$eCCPBaseRegisterCompanyTable.DataTable({
            paging: true,
            serverSide: true,
            processing: true,
            listAction: {
                ajaxFunction: _eccpBaseElevatorsService.getAllECCPBaseRegisterCompanyForLookupTable,
                inputFilter: function () {
                    return {
                        filter: $('#ECCPBaseRegisterCompanyTableFilter').val()
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

        $('#ECCPBaseRegisterCompanyTable tbody').on('click', '[id*=selectbtn]', function () {
            var data = dataTable.row($(this).parents('tr')).data();
            _modalManager.setResult(data);
            _modalManager.close();
        });

        function getECCPBaseRegisterCompany() {
            dataTable.ajax.reload();
        }

        $('#GetECCPBaseRegisterCompanyButton').click(function (e) {
            e.preventDefault();
            getECCPBaseRegisterCompany();
        });

        $('#SelectButton').click(function (e) {
            e.preventDefault();
        });

        $(document).keypress(function (e) {
            if (e.which === 13) {
                getECCPBaseRegisterCompany();
            }
        });

    };
})(jQuery);

