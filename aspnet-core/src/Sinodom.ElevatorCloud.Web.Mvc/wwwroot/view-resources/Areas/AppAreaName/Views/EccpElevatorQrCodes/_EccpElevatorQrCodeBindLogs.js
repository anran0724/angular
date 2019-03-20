(function ($) {
    app.modals.EccpElevatorQrCodeBindLogs = function () {

        var _modalManager;

        var _eccpElevatorQrCodesService = abp.services.app.eccpElevatorQrCodes;
        var _$eccpBaseElevatorTable = $('#EccpElevatorQrCodeBindLogsTable');
   

        var _args;

        this.init = function (modalManager, args) {
            _modalManager = modalManager;
            _args = args;

            console.log(args);
        };

        var dataTable = _$eccpBaseElevatorTable.DataTable({
            paging: true,
            serverSide: true,
            processing: true,
            listAction: {
                ajaxFunction: _eccpElevatorQrCodesService.getAllbind,
                inputFilter: function () {
                    return {
                        newId: _args.newid
                    };
                }
            },
            columnDefs: [
           
            {
                targets: 0,
                    data: "newCertificateNum"
            },
            {
                targets: 1,
                data: "oleCertificateNum"
            },
            {
                targets: 2,
                data: "newaename"
            },
            {
                targets: 3,
                data: "oleaename"

                },
            {
                targets: 4,
                data: "remark"

                }
        ]
    });
    

        $('#EccpElevatorQrCodeBindLogsTable tbody').on('click', '[id*=selectbtn]', function () {
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





