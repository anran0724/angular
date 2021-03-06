(function ($) {
    app.modals.EccpBaseElevatorLookupTableModal = function () {

        var _modalManager;

        var _eccpMaintenanceContractsService = abp.services.app.eccpMaintenanceContracts;
        var _$eCCPBaseElevatorTable = $('#ECCPBaseElevatorTable');
        var _maintenanceContract;

        this.init = function (modalManager, maintenanceContract) {
            _modalManager = modalManager;
            _maintenanceContract = maintenanceContract;
            bindData();
        };

        var bindData = function () {
            if (_maintenanceContract.eccpBaseElevatorsIds != "") {
                a = _maintenanceContract.eccpBaseElevatorsIds.split(',');
                var eccpBaseElevatorsNames = _maintenanceContract.eccpBaseElevatorsNames.split(',');
                $.each(eccpBaseElevatorsNames, function (index, value) {
                    _selecthtml += "<div class='btn-group m-btn-group' role='group' aria-label='...' id='" + a[index] + "'><button type='button'  name='displayName' class='btn btn-brand'>" + value + "</button><button type='button' class='btn btn-success' id='removebtn' aria-label='Close'><span aria-hidden='true'>X</span></button></div>";
                });

                $('#checked_all').html(_selecthtml);
            }
        }

        var dataTable = _$eCCPBaseElevatorTable.DataTable({
            paging: true,
            serverSide: true,
            processing: true,
            listAction: {
                ajaxFunction: _eccpMaintenanceContractsService.getAllECCPBaseElevatorForLookupTable,
                inputFilter: function () {
                    return {
                        filter: $('#ECCPBaseElevatorTableFilter').val(),
                        maintenanceContractId: _maintenanceContract.maintenanceContractId
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
                },
                {
                    targets: 2,
                    data: null,
                    orderable: false,
                    autoWidth: false,
                    defaultContent: "<label class='m-checkbox m-checkbox--single m-checkbox--solid m-checkbox--brand'><input type='checkbox' name='elevator' value='' class='m-checkable' id='selectcheck'><span></span></label>"
                }
            ],
            createdRow: function (row, data, index) {
                if ($.inArray(data.id, a) >= 0) {
                    $(row).find('.m-checkable').prop("checked", true);
                }
                if (a.length <= 0) {
                    $('#all_checked').prop("checked", false);
                }
                $(row).data('id', data.id);
                $(row).find('.m-checkable').first().val(data.id);
            }
        });

        var _selecthtml = "";
        var a = [];

        $("#all_checked").click(function () {
            $('[name=elevator]:checkbox').prop('checked', this.checked);

            $('input[name="elevator"]').each(function () {
                var data = dataTable.row($(this).parents('tr')).data();

                if ($(this).is(':checked')) {
                    if ($.inArray(data.id, a) < 0) {
                        a.push(data.id);
                        _selecthtml += "<div class='btn-group m-btn-group' role='group' aria-label='...' id='" + data.id + "'><button type='button'  name='displayName' class='btn btn-brand'>" + data.displayName + "</button><button type='button' class='btn btn-success' id='removebtn' aria-label='Close'><span aria-hidden='true'>X</span></button></div>";
                    }
                } else {
                    $('#' + data.id).remove();
                    _selecthtml = $('#checked_all').html();
                    for (var i = 0; i < a.length; i++) {
                        if (a[i] == data.id) {
                            a.splice($.inArray(data.id, a), 1);
                        }
                    }
                }
            });
            $('#checked_all').html(_selecthtml);
        });

        $('#ECCPBaseElevatorTable tbody').on('click', '[id*=selectcheck]', function () {
            var data = dataTable.row($(this).parents('tr')).data();
            if ($(this).is(':checked')) {
                a.push($(this).val());
                _selecthtml += "<div class='btn-group m-btn-group' role='group' aria-label='...' id='" + data.id + "'><button type='button' name='displayName' class='btn btn-brand'>" + data.displayName + "</button><button type='button' class='btn btn-success' id='removebtn' aria-label='Close'><span aria-hidden='true'>X</span></button></div>";
                $('#checked_all').html(_selecthtml);
            } else {
                $('#' + data.id).remove();
                _selecthtml = $('#checked_all').html();
                for (var i = 0; i < a.length; i++) {
                    if (a[i] == data.id) {
                        a.splice($.inArray(data.id, a), 1);
                    }
                }
            }
        });

        $('#checked_all').on('click', '[id*=removebtn]', function () {
            $(this).parent().remove();
            _selecthtml = $('#checked_all').html();

            for (var i = 0; i < a.length; i++) {
                if (a[i] == $(this).parent().attr('id')) {
                    a.splice($.inArray(a[i], a), 1);
                }
            }
            getECCPBasePropertyCompany();
        });

        $('#_confirm').click(function (e) {
            if (a.length <= 0) {
                return;
            }

            var displayName = "";
            $("#checked_all button[name=displayName]").each(function () {
                if (displayName != "") {
                    displayName += ",";
                }
                displayName += $(this).text();

            });
            var data = { id: a.join(','), displayName: displayName };
            _modalManager.setResult(data);
            _modalManager.close();
        });

        $('#ECCPBaseElevatorTable tbody').on('click', '[id*=selectbtn]', function () {

            var data = dataTable.row($(this).parents('tr')).data();
            _modalManager.setResult(data);
            _modalManager.close();
        });

        function getECCPBasePropertyCompany() {
            dataTable.ajax.reload();
        }

        $('#GetECCPBaseElevatorButton').click(function (e) {
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