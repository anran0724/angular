(function ($) {
    app.modals.ECCPBaseAnnualInspectionUnitLookupTableModal = function () {

        var _modalManager;

        var _eccpBaseElevatorsService = abp.services.app.eccpBaseElevators;
        var _$eCCPBaseAnnualInspectionUnitTable = $('#ECCPBaseAnnualInspectionUnitTable');

        this.init = function (modalManager) {
            _modalManager = modalManager;
        };


        var dataTable = _$eCCPBaseAnnualInspectionUnitTable.DataTable({
            paging: true,
            serverSide: true,
            processing: true,
            listAction: {
                ajaxFunction: _eccpBaseElevatorsService.getAllECCPBaseAnnualInspectionUnitForLookupTable,
                inputFilter: function () {
                    return {
                        filter: $('#ECCPBaseAnnualInspectionUnitTableFilter').val()
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

        $('#ECCPBaseAnnualInspectionUnitTable tbody').on('click', '[id*=selectbtn]', function () {
            var data = dataTable.row($(this).parents('tr')).data();
            _modalManager.setResult(data);
            _modalManager.close();
        });

        function getECCPBaseAnnualInspectionUnit() {
            dataTable.ajax.reload();
        }

        $('#GetECCPBaseAnnualInspectionUnitButton').click(function (e) {
            e.preventDefault();
            getECCPBaseAnnualInspectionUnit();
        });

        $('#SelectButton').click(function (e) {
            e.preventDefault();
        });

        $(document).keypress(function (e) {
            if (e.which === 13) {
                getECCPBaseAnnualInspectionUnit();
            }
        });

    };
})(jQuery);

