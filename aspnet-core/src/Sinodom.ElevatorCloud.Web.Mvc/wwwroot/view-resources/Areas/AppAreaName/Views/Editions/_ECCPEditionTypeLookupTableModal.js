(function ($) {
    app.modals.ECCPEditionTypeLookupTableModal = function () {

        var _modalManager;

        var _editionsService = abp.services.app.edition;
        var _$eccpEditionsTypeTable = $('#ECCPEditionTypeTable');

        this.init = function (modalManager) {
            _modalManager = modalManager;
        };

        console.log(222);

        var dataTable = _$eccpEditionsTypeTable.DataTable({
            paging: true,
            serverSide: true,
            processing: true,
            listAction: {
                ajaxFunction: _editionsService.getAllECCPEditionsTypeForLookupTable,
                inputFilter: function () {
                    return {
                        filter: $('#ECCPEditionTypeTableFilter').val()
                    };
                }
            },
            columnDefs: [
                {
                    targets: 0,
                    data: null,
                    orderable: false,
                    autoWidth: false,
                    defaultContent: "<div class=\"text-center\"><input id='selectbtn' class='btn btn-success' type='button' width='25px' value='Select' /></div>"
                },
                {
                    autoWidth: false,
                    orderable: false,
                    targets: 1,
                    data: "displayName"
                }
            ]
        });

        $('#ECCPEditionTypeTable tbody').on('click', '[id*=selectbtn]', function () {
            var data = dataTable.row($(this).parents('tr')).data();
            _modalManager.setResult(data);
            _modalManager.close();
        });

        function getECCPEditionType() {
            dataTable.ajax.reload();
        }

        $('#GetECCPEditionTypeButton').click(function (e) {
            e.preventDefault();
            getECCPEditionType();
        });

        $('#SelectButton').click(function (e) {
            e.preventDefault();
        });

        $(document).keypress(function (e) {
            if (e.which === 13) {
                getECCPEditionType();
            }
        });

    };
})(jQuery);

