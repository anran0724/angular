(function () {
    $(function () {

        var _$eccpBaseElevatorsTable = $('#EccpBaseElevatorsTable');
        var _eccpBaseElevatorsService = abp.services.app.eccpBaseElevators;
        var a = [];

        $('.date-picker').datetimepicker({
            locale: abp.localization.currentLanguage.name,
            format: 'YYYY-MM-DD'
        });

        var _permissions = {
            create: abp.auth.hasPermission('Pages.EccpElevator.EccpBaseElevators.Create'),
            edit: abp.auth.hasPermission('Pages.EccpElevator.EccpBaseElevators.Edit'),
            'delete': abp.auth.hasPermission('Pages.EccpElevator.EccpBaseElevators.Delete')
        };
        var _claimModal = new app.ModalManager({
            viewUrl: abp.appPath + 'AppAreaName/EccpBaseElevatorsClaim/ClaimModal',
            scriptUrl: abp.appPath + 'view-resources/Areas/AppAreaName/Views/EccpBaseElevatorsClaim/_ClaimModal.js',
            modalClass: 'ClaimModal'
        });

        var _viewEccpBaseElevatorModal = new app.ModalManager({
            viewUrl: abp.appPath + 'AppAreaName/EccpBaseElevators/VieweccpBaseElevatorModal',
            modalClass: 'ViewEccpBaseElevatorModal'
        });

        var dataTable = _$eccpBaseElevatorsTable.DataTable({
            paging: true,
            serverSide: true,
            processing: true,
            listAction: {
                ajaxFunction: _eccpBaseElevatorsService.getClaimAll,
                inputFilter: function () {
                    return {
                        filter: $('#EccpBaseElevatorsTableFilter').val(),
                        certificateNumFilter: $('#CertificateNumFilterId').val(),
                        machineNumFilter: $('#MachineNumFilterId').val(),
                        installationAddressFilter: $('#InstallationAddressFilterId').val()
                    };
                }
            },
            columnDefs: [
                {
                    className: 'control responsive',
                    orderable: false,
                    render: function () {
                        return '';
                    },
                    targets: 0
                },
                {
                    targets: 1,
                    data: null,
                    orderable: false,
                    autoWidth: false,
                    defaultContent: "<label class='m-checkbox m-checkbox--single m-checkbox--solid m-checkbox--brand'><input type='checkbox' name='elevator' value='' class='m-checkable' id='selectcheck'><span></span></label>"
                },
                {
                    width: 120,
                    targets: 2,
                    data: null,
                    orderable: false,
                    autoWidth: false,
                    defaultContent: '',
                    rowAction: {
                        cssClass: 'btn btn-brand dropdown-toggle',
                        text: '<i class="fa fa-cog"></i> ' + app.localize('Actions') + ' <span class="caret"></span>',
                        items: [
                            {
                                text: app.localize('View'),
                                action: function (data) {
                                    _viewEccpBaseElevatorModal.open({ data: data.record });
                                }
                            },
                            {
                                text: app.localize('Claim'),
                                visible: function () {
                                    return _permissions.edit;
                                },
                                action: function (data) {
                                    var ids = [];
                                    ids.push(data.record.eccpBaseElevator.id);
                                    _claimModal.open({ id: ids });
                                }
                            }
                        ]
                    }
                },
                {
                    targets: 3,
                    data: "eccpBaseElevator.name"
                },
                {
                    targets: 4,
                    data: "eccpBaseElevator.certificateNum"
                },
                {
                    targets: 5,
                    data: "eccpBaseElevator.machineNum"
                },
                {
                    targets: 6,
                    data: "eccpBaseElevator.installationAddress"
                },
                {
                    targets: 7,
                    data: "eccpBasePropertyCompanyName"
                },
                {
                    targets: 8,
                    data: "eccpBaseMaintenanceCompanyName"
                },
                {
                    targets: 9,
                    data: "provinceName"
                },
                {
                    targets: 10,
                    data: "cityName"
                },
                {
                    targets: 11,
                    data: "districtName"
                },
                {
                    targets: 12,
                    data: "streetName"
                },
                {
                    targets: 13,
                    data: "eccpBaseCommunityName"
                },
                {
                    targets: 14,
                    data: "eccpDictElevatorStatusName"
                },
                {
                    targets: 15,
                    data: "eccpDictElevatorTypeName"
                },
                {
                    targets: 16,
                    data: "eccpBaseElevatorBrandName"
                },
                {
                    targets: 17,
                    data: "eccpBaseElevatorModelName"
                }
            ]
        });



        $("#all_checked").click(function () {

            $('[name=elevator]:checkbox').prop('checked', this.checked);

            $('input[name="elevator"]').each(function () {
                var data = dataTable.row($(this).parents('tr')).data().eccpBaseElevator;
                if ($(this).is(':checked')) {
                    if ($.inArray(data.id, a) < 0) {
                        a.push(data.id);
                    }
                } else {
                    $('#' + data.id).remove();
                    for (var i = 0; i < a.length; i++) {
                        if (a[i] == data.id) {
                            a.splice($.inArray(data.id, a), 1);
                        }
                    }
                }
            });
        });

        $('#EccpBaseElevatorsTable tbody').on('click', '[id*=selectcheck]', function () {

            var data = dataTable.row($(this).parents('tr')).data().eccpBaseElevator;
            if ($(this).is(':checked')) {
                a.push(data.id);
            } else {
                $('#' + data.id).remove();
                for (var i = 0; i < a.length; i++) {
                    if (a[i] == data.id) {
                        a.splice($.inArray(data.id, a), 1);
                    }
                }
            }
        });


        $('#BatchClaimButton').click(function () {

            _claimModal.open({ id: a }, function (data) {
                if (data == false) {
                    $('[id=all_checked]:checkbox').prop('checked', false);
                }
            });
        });

        function getEccpBaseElevators() {
            dataTable.ajax.reload();
        }

        $('#ShowAdvancedFiltersSpan').click(function () {
            $('#ShowAdvancedFiltersSpan').hide();
            $('#HideAdvancedFiltersSpan').show();
            $('#AdvacedAuditFiltersArea').slideDown();
        });

        $('#HideAdvancedFiltersSpan').click(function () {
            $('#HideAdvancedFiltersSpan').hide();
            $('#ShowAdvancedFiltersSpan').show();
            $('#AdvacedAuditFiltersArea').slideUp();
        });


        abp.event.on('app.createOrEditEccpBaseElevatorModalSaved', function () {
            getEccpBaseElevators();
        });

        $('#GetEccpBaseElevatorsButton').click(function (e) {
            e.preventDefault();
            getEccpBaseElevators();
        });

        $(document).keypress(function (e) {
            if (e.which === 13) {
                getEccpBaseElevators();
            }
        });

    });
})();