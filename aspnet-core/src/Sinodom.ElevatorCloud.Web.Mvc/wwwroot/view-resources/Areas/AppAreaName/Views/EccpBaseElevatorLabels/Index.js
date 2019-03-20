(function () {
    $(function () {

        var _$eccpBaseElevatorLabelsTable = $('#EccpBaseElevatorLabelsTable');
        var _eccpBaseElevatorLabelsService = abp.services.app.eccpBaseElevatorLabels;

        $('.date-picker').datetimepicker({
            locale: abp.localization.currentLanguage.name,
            format: 'L'
        });

        var _permissions = {
            create: abp.auth.hasPermission('Pages.EccpElevator.EccpBaseElevatorLabels.Create'),
            edit: abp.auth.hasPermission('Pages.EccpElevator.EccpBaseElevatorLabels.Edit'),
            'delete': abp.auth.hasPermission('Pages.EccpElevator.EccpBaseElevatorLabels.Delete')
        };

        var _createOrEditModal = new app.ModalManager({
            viewUrl: abp.appPath + 'AppAreaName/EccpBaseElevatorLabels/CreateOrEditModal',
            scriptUrl: abp.appPath + 'view-resources/Areas/AppAreaName/Views/EccpBaseElevatorLabels/_CreateOrEditModal.js',
            modalClass: 'CreateOrEditEccpBaseElevatorLabelModal'
        });

        var _viewEccpBaseElevatorLabelModal = new app.ModalManager({
            viewUrl: abp.appPath + 'AppAreaName/EccpBaseElevatorLabels/VieweccpBaseElevatorLabelModal',
            modalClass: 'ViewEccpBaseElevatorLabelModal'
        });

        var dataTable = _$eccpBaseElevatorLabelsTable.DataTable({
            paging: true,
            serverSide: true,
            processing: true,
            listAction: {
                ajaxFunction: _eccpBaseElevatorLabelsService.getAll,
                inputFilter: function () {
                    return {
                        filter: $('#EccpBaseElevatorLabelsTableFilter').val(),
                        minBindingTimeFilter: $('#MinBindingTimeFilterId').val(),
                        maxBindingTimeFilter: $('#MaxBindingTimeFilterId').val(),
                        eccpBaseElevatorNameFilter: $('#EccpBaseElevatorNameFilterId').val(),
                        eccpDictLabelStatusNameFilter: $('#EccpDictLabelStatusNameFilterId').val(),
                        userNameFilter: $('#UserNameFilterId').val()
                    };
                }
            },
            columnDefs: [
                {
                    width: 120,
                    targets: 0,
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
                                    _viewEccpBaseElevatorLabelModal.open({ data: data.record });
                                }
                            },
                            {
                                text: app.localize('DiscontinueUse'),
                                visible: function (data) {
                                    if (data.record.eccpDictLabelStatusName == app.localize('Failure')) {
                                        return false;
                                    }
                                    return _permissions.discontinueUse;
                                },
                                action: function (data) {
                                    discontinueUseEccpBaseElevatorLabel(data.record.eccpBaseElevatorLabel);
                                }
                            }]
                    }
                },
                {
                    targets: 1,
                    data: "eccpBaseElevatorLabel.labelName"
                },
                {
                    targets: 2,
                    data: "eccpBaseElevatorLabel.uniqueId"
                },
                {
                    targets: 3,
                    data: "eccpBaseElevatorLabel.localInformation"
                },
                {
                    targets: 4,
                    data: "eccpBaseElevatorLabel.bindingTime",
                    render: function (bindingTime) {
                        if (bindingTime) {
                            return moment(bindingTime).format('L');
                        }
                        return "";
                    }

                },
                {
                    targets: 5,
                    data: "eccpBaseElevatorLabel.binaryObjectsId",
                    render: function (binaryObjectsId) {
                        var $container = $("<span/>");
                        if (binaryObjectsId) {
                            var profilePictureUrl = "/Profile/GetProfilePictureById?id=" + binaryObjectsId;
                            var $link = $("<a/>").attr("href", profilePictureUrl).attr("target", "_blank");
                            var $img = $("<img/>")
                                .addClass("img-circle")
                                .attr("src", profilePictureUrl);

                            $link.append($img);
                            $container.append($link);
                        }
                        return $container[0].outerHTML;
                    }

                },
                {
                    targets: 6,
                    data: "eccpBaseElevatorName"
                },
                {
                    targets: 7,
                    data: "eccpDictLabelStatusName"
                },
                {
                    targets: 8,
                    data: "userName"
                }
            ]
        });


        function getEccpBaseElevatorLabels() {
            dataTable.ajax.reload();
        }

        function discontinueUseEccpBaseElevatorLabel(eccpBaseElevatorLabel) {
            abp.message.confirm(
                '',
                function (isConfirmed) {
                    if (isConfirmed) {
                        _eccpBaseElevatorLabelsService.discontinueUse({
                            id: eccpBaseElevatorLabel.id
                        }).done(function () {
                            getEccpBaseElevatorLabels(true);
                            abp.notify.success(app.localize('SuccessfullyDiscontinueUse'));
                        });
                    }
                }
            );
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

        $('#CreateNewEccpBaseElevatorLabelButton').click(function () {
            _createOrEditModal.open();
        });



        abp.event.on('app.createOrEditEccpBaseElevatorLabelModalSaved', function () {
            getEccpBaseElevatorLabels();
        });

        $('#GetEccpBaseElevatorLabelsButton').click(function (e) {
            e.preventDefault();
            getEccpBaseElevatorLabels();
        });

        $(document).keypress(function (e) {
            if (e.which === 13) {
                getEccpBaseElevatorLabels();
            }
        });

    });
})();