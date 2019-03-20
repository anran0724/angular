(function ($) {
    app.modals.CreateOrEditEccpMaintenanceTemplateNodeModal = function () {

        var _eccpMaintenanceTemplateNodesService = abp.services.app.eccpMaintenanceTemplateNodes;

        var _modalManager;
        var _$eccpMaintenanceTemplateNodeInformationForm = null;

        function _findAssignedItemIds() {
            var assignedItemIds = [];

            _modalManager.getModal()
                .find('.user-role-checkbox-list input[type=checkbox]')
                .each(function () {
                    if ($(this).is(':checked')) {
                        assignedItemIds.push($(this).attr('name'));
                    }
                });
            return assignedItemIds;
        }

        var _eccpDictNodeTypeLookupTableModal = new app.ModalManager({
            viewUrl: abp.appPath + 'AppAreaName/EccpMaintenanceTemplateNodes/EccpDictNodeTypeLookupTableModal',
            scriptUrl: abp.appPath + 'view-resources/Areas/AppAreaName/Views/EccpMaintenanceTemplateNodes/_EccpDictNodeTypeLookupTableModal.js',
            modalClass: 'EccpDictNodeTypeLookupTableModal'
        });

        var _eccpMaintenanceNextNodeLookupTableModal = new app.ModalManager({
            viewUrl: abp.appPath + 'AppAreaName/EccpMaintenanceTemplateNodes/EccpMaintenanceTemplateLookupTableModal',
            scriptUrl: abp.appPath + 'view-resources/Areas/AppAreaName/Views/EccpMaintenanceTemplateNodes/_EccpMaintenanceNextNodeLookupTableModal.js',
            modalClass: 'EccpMaintenanceNextNodeLookupTableModal'
        });

        this.init = function (modalManager) {
            _modalManager = modalManager;

            var modal = _modalManager.getModal();
            modal.find('.date-picker').datetimepicker({
                locale: abp.localization.currentLanguage.name,
                format: 'L'
            });
            _$eccpMaintenanceTemplateNodeInformationForm = _modalManager.getModal().find('form[name=EccpMaintenanceTemplateNodeInformationsForm]');
            _$eccpMaintenanceTemplateNodeInformationForm.validate();

            _modalManager.getModal()
                .find('.user-role-checkbox-list input[type=checkbox]')
                .change(function () {
                    $('#assigned-item-count').text(_findAssignedItemIds().length);
                });

        };
                  

        $('#OpenEccpDictNodeTypeLookupTableButton').click(function () {
            var eccpMaintenanceTemplateNode = _$eccpMaintenanceTemplateNodeInformationForm.serializeFormToObject();
            _eccpDictNodeTypeLookupTableModal.open({ id: eccpMaintenanceTemplateNode.dictNodeTypeId, displayName: eccpMaintenanceTemplateNode.eccpDictNodeTypeName }, function (data) {
                _$eccpMaintenanceTemplateNodeInformationForm.find('input[name=eccpDictNodeTypeName]').val(data.displayName);
                _$eccpMaintenanceTemplateNodeInformationForm.find('input[name=dictNodeTypeId]').val(data.id);
            });
        });
        $('#ClearEccpDictNodeTypeNameButton').click(function () {
            _$eccpMaintenanceTemplateNodeInformationForm.find('input[name=eccpDictNodeTypeName]').val('');
            _$eccpMaintenanceTemplateNodeInformationForm.find('input[name=dictNodeTypeId]').val('');
        });
        //NextNode
        $('#OpenNextNodeLookupTableButton').click(function () {
            var eccpMaintenanceTemplateNode = _$eccpMaintenanceTemplateNodeInformationForm.serializeFormToObject();
            _eccpMaintenanceNextNodeLookupTableModal.open({ id: eccpMaintenanceTemplateNode.nextNodeId, displayName: eccpMaintenanceTemplateNode.nextNodeName, maintenanceTemplateId: eccpMaintenanceTemplateNode.maintenanceTemplateId}, function (data) {
                _$eccpMaintenanceTemplateNodeInformationForm.find('input[name=nextNodeName]').val(data.displayName);
                _$eccpMaintenanceTemplateNodeInformationForm.find('input[name=nextNodeId]').val(data.id);
            });
        });
        $('#ClearNextNodeNameButton').click(function () {
            _$eccpMaintenanceTemplateNodeInformationForm.find('input[name=nextNodeName]').val('');
            _$eccpMaintenanceTemplateNodeInformationForm.find('input[name=nextNodeId]').val('');
        });
        //SpareNode
        $('#OpenSpareNodeLookupTableButton').click(function () {
            var eccpMaintenanceTemplateNode = _$eccpMaintenanceTemplateNodeInformationForm.serializeFormToObject();
            _eccpMaintenanceNextNodeLookupTableModal.open({ id: eccpMaintenanceTemplateNode.spareNodeId, displayName: eccpMaintenanceTemplateNode.spareNodeName, maintenanceTemplateId: eccpMaintenanceTemplateNode.maintenanceTemplateId }, function (data) {
                _$eccpMaintenanceTemplateNodeInformationForm.find('input[name=spareNodeName]').val(data.displayName);
                _$eccpMaintenanceTemplateNodeInformationForm.find('input[name=spareNodeId]').val(data.id);
            });
        });
        $('#ClearSpareNodeNameButton').click(function () {
            _$eccpMaintenanceTemplateNodeInformationForm.find('input[name=spareNodeName]').val('');
            _$eccpMaintenanceTemplateNodeInformationForm.find('input[name=spareNodeId]').val('');
        });
        this.save = function () {
            if (!_$eccpMaintenanceTemplateNodeInformationForm.valid()) {
                return;
            }
            var assignedItemIds = _findAssignedItemIds();


            var eccpMaintenanceTemplateNode = _$eccpMaintenanceTemplateNodeInformationForm.serializeFormToObject();

            eccpMaintenanceTemplateNode.AssignedItemIds = assignedItemIds;

            _modalManager.setBusy(true);
            _eccpMaintenanceTemplateNodesService.createOrEdit(
                eccpMaintenanceTemplateNode
            ).done(function () {
                abp.notify.info(app.localize('SavedSuccessfully'));
                _modalManager.close();
                abp.event.trigger('app.createOrEditEccpMaintenanceTemplateNodeModalSaved');
            }).always(function () {
                _modalManager.setBusy(false);
            });
        };
    };
})(jQuery);