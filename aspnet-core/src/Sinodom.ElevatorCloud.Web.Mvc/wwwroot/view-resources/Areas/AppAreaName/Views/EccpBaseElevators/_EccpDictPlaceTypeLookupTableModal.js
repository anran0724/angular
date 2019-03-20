(function ($) {
    app.modals.EccpDictPlaceTypeLookupTableModal = function () {

        var _modalManager;

        var _eccpBaseElevatorsService = abp.services.app.eccpBaseElevators;
        var _$eccpDictPlaceTypeTable = $('#EccpDictPlaceTypeTable');

        this.init = function (modalManager) {
            _modalManager = modalManager;
        };


        var dataTable = _$eccpDictPlaceTypeTable.DataTable({
            paging: true,
            serverSide: true,
            processing: true,
            listAction: {
                ajaxFunction: _eccpBaseElevatorsService.getAllEccpDictPlaceTypeForLookupTable,
                inputFilter: function () {
                    return {
                        filter: $('#EccpDictPlaceTypeTableFilter').val()
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

        $('#EccpDictPlaceTypeTable tbody').on('click', '[id*=selectbtn]', function () {
            var data = dataTable.row($(this).parents('tr')).data();
            _modalManager.setResult(data);
            _modalManager.close();
        });

        function getEccpDictPlaceType() {
            dataTable.ajax.reload();
        }

        $('#GetEccpDictPlaceTypeButton').click(function (e) {
            e.preventDefault();
            getEccpDictPlaceType();
        });

        $('#SelectButton').click(function (e) {
            e.preventDefault();
        });

        $(document).keypress(function (e) {
            if (e.which === 13) {
                getEccpDictPlaceType();
            }
        });

    };
})(jQuery);

