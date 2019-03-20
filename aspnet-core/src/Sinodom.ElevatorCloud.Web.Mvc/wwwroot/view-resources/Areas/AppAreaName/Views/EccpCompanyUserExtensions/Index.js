(function () {
    $(function () {

        var _$usersTable = $('#UsersTable');
        var _companyUserService = abp.services.app.eccpCompanyUserExtensionsAppServicer;

        var _permissions = {
            edit: abp.auth.hasPermission('Pages.Administration.EccpCompanyUserExtensions.EditState'),
            impersonation: abp.auth.hasPermission('Pages.Administration.Users.Impersonation')
        };

        //var _permissions = {
        //    create: abp.auth.hasPermission('Pages.Administration.Users.Create'),
        //    edit: abp.auth.hasPermission('Pages.Administration.Users.Edit'),
        //    changePermissions: abp.auth.hasPermission('Pages.Administration.Users.ChangePermissions'),
        //    impersonation: abp.auth.hasPermission('Pages.Administration.Users.Impersonation'),
        //    'delete': abp.auth.hasPermission('Pages.Administration.Users.Delete')
        //};

        var _createOrEditModal = new app.ModalManager({
            viewUrl: abp.appPath + 'AppAreaName/EccpCompanyUserExtensions/AuditModal',
            scriptUrl: abp.appPath + 'view-resources/Areas/AppAreaName/Views/EccpCompanyUserExtensions/AuditModel.js',
            modalClass: 'CreateOrEditUserModal'
        });

        var dataTable = _$usersTable.DataTable({
            paging: true,
            serverSide: true,
            processing: true,
            listAction: {
                ajaxFunction: _companyUserService.getUsers,
                inputFilter: function () {
                    return {
                        filter: $('#UsersTableFilter').val(),
                        checkState: $("#CheckState").val()
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
                    defaultContent: '',
                    rowAction: {
                        text: '<i class="fa fa-cog"></i> ' + app.localize('Actions') + ' <span class="caret"></span>',
                        items: [{
                            text: app.localize('LoginAsThisUser'),
                            visible: function (data) {
                                return false;
                            },
                            action: function (data) {                            
                            }
                        },{
                                text: app.localize('Audit'),
                                visible: function (data) {
                                    if (data.record.checkState != 1) {
                                        return _permissions.edit;
                                    }
                                    else {
                                        return true;
                                    }
                                },
                                action: function (data) {                                    
                                    _createOrEditModal.open({ id: data.record.id });
                                }
                            }]
                    }
                },
                {
                    targets: 2,
                    data: "userName",
                    orderable: false                    
                },
                {
                    targets: 3,
                    data: "name",
                    orderable: false    
                },
                {
                    targets: 4,
                    data: "surname",
                    orderable: false    
                },
                {
                    targets: 5,
                    data: "emailAddress",
                    orderable: false                 
                },
                {
                    targets: 6,
                    data: "mobile",
                    orderable: false    
                },
                {
                    targets: 7,
                    data: "companyName",         
                    orderable: false    
                },
                {
                    targets: 8,
                    data: "companyType",
                    orderable: false ,
                    render: function (companyType) {
                        if (companyType==1) {
                            return '<span class="label label-default">使用单位</span>';
                        } else {
                            return '<span class="label label-default">维保单位</span>';
                        }
                    }
                },
                {
                    targets: 9,
                    data: "checkState",
                    render: function (checkState) {
                        if (checkState == 0) {
                            return '<span class="label label-default">未审核</span>';
                        }
                        else if (checkState == 2)
                        {
                            return '<span class="label label-default">未通过</span>';
                        }
                        else {
                            return '<span class="label label-default">已审核</span>';
                        }
                    }
                },
                {
                    targets: 10,
                    data: "creationTime",
                    render: function (creationTime) {
                        return moment(creationTime).format('YYYY-MM-DD');
                    }
                }
            ]
        });          

        function getUsers() {
            dataTable.ajax.reload();
        }
        $('#GetUsersButton').click(function (e) {
            e.preventDefault();
            getUsers();
        });

        abp.event.on('app.createOrEditUserModalSaved', function () {
            getUsers();
        });
    });
})();