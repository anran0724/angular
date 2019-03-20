(function ($) {
    app.modals.ECCPBaseCommunityLookupTableModal = function () {

        var _modalManager;

        var _eccpBaseElevatorsService = abp.services.app.eccpBaseElevators;
        var _$eCCPBaseCommunityTable = $('#ECCPBaseCommunityTable');
        var _args;
        this.init = function (modalManager,args) {
            _modalManager = modalManager;
            _args = args;
        };


        var dataTable = _$eCCPBaseCommunityTable.DataTable({
            paging: true,
            serverSide: true,
            processing: true,
            listAction: {
                ajaxFunction: _eccpBaseElevatorsService.getAllECCPBaseCommunityForLookupTable,
                inputFilter: function () {
                    return {
                        filter: $('#ECCPBaseCommunityTableFilter').val(),
                        parentId: _args.ParentId
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

        $('#ECCPBaseCommunityTable tbody').on('click', '[id*=selectbtn]', function () {
            var data = dataTable.row($(this).parents('tr')).data();
            _modalManager.setResult(data);
            _modalManager.close();
        });

        function getECCPBaseCommunity() {
            dataTable.ajax.reload();
        }

        $('#GetECCPBaseCommunityButton').click(function (e) {
            e.preventDefault();
            getECCPBaseCommunity();
        });

        $('#SelectButton').click(function (e) {
            e.preventDefault();
        });

        $(document).keypress(function (e) {
            if (e.which === 13) {
                getECCPBaseCommunity();
            }
        });

    };
})(jQuery);

