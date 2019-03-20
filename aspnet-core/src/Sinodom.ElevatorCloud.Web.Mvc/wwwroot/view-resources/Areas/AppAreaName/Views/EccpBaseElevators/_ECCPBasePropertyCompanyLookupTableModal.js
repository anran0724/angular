(function ($) {
    app.modals.ECCPBasePropertyCompanyLookupTableModal = function () {
        
        var _modalManager;

        var _eccpBaseElevatorsService = abp.services.app.eccpBaseElevators;
        var _$eCCPBasePropertyCompanyTable = $('#ECCPBasePropertyCompanyTable');

        this.init = function (modalManager) {
            _modalManager = modalManager;
        };


        var dataTable = _$eCCPBasePropertyCompanyTable.DataTable({
            paging: true,
            serverSide: true,
            processing: true,
            listAction: {
                ajaxFunction: _eccpBaseElevatorsService.getAllECCPBasePropertyCompanyForLookupTable,
                inputFilter: function () {
                    return {
                        filter: $('#ECCPBasePropertyCompanyTableFilter').val()
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

        $('#ECCPBasePropertyCompanyTable tbody').on('click', '[id*=selectbtn]', function () {
            var data = dataTable.row($(this).parents('tr')).data();
            _modalManager.setResult(data);
            _modalManager.close();
        });

        function getECCPBasePropertyCompany() {
            dataTable.ajax.reload();
        }

        $('#GetECCPBasePropertyCompanyButton').click(function (e) {
            e.preventDefault();
            getECCPBasePropertyCompany();
        });

        $('#SelectButton').click(function (e) {
            e.preventDefault();
        });

        $(document).keypress(function (e) {
            if (e.which === 13) {
                getECCPBasePropertyCompany();
            }
        });

    };
})(jQuery);

