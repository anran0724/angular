(function () {
    var _createOrEditModal = new app.ModalManager({
        viewUrl: abp.appPath + 'AppAreaName/EccpMaintenanceTemplateNodes/CreateOrEditModal',
        scriptUrl: abp.appPath + 'view-resources/Areas/AppAreaName/Views/EccpMaintenanceTemplateNodes/_CreateOrEditModal.js',
        modalClass: 'CreateOrEditEccpMaintenanceTemplateNodeModal'
    });
    var _eccpMaintenanceTemplateNodesService = abp.services.app.eccpMaintenanceTemplateNodes;
    app.modals.EccpMaintenanceTemplateNodesTableModal = function () {
        this.init = function () {
            var options = {
                tagID: "#tbody",
                ajax: {
                    url: '/AppAreaName/EccpMaintenanceTemplates/GetMaintenanceTemplateNodes', data: { id: $("#MaintenanceTemplateId").val() }, type: 'get'
                }
            }
            var treeTable = new TreeTable(options);
            treeTable.Load();
        }
       
        $("#CreateNewEccpMaintenanceTemplateNodeButton").click(function () {
            _createOrEditModal.open({ maintenanceTemplateId: $("#MaintenanceTemplateId").val(), parentNodeId: 0 });
        });
        abp.event.on('app.createOrEditEccpMaintenanceTemplateNodeModalSaved', function () {
            var options = {
                tagID: "#tbody",
                ajax: {
                    url: '/AppAreaName/EccpMaintenanceTemplates/GetMaintenanceTemplateNodes', data: { id: $("#MaintenanceTemplateId").val() }, type: 'get'
                }
            }
            var treeTable = new TreeTable(options);
            treeTable.Load();
        });
    };
    var TreeTable = (function (options) {
        var self = this;
        this._option = {};
        this._option = $.extend(this._option, options);
        this.Load = function () {
            $.ajax({
                url: options.ajax.url,
                data: options.ajax.data,
                type: options.ajax.type,
                dataType: "json",
                success: function (returnData) {
                    if (returnData.result) {
                        $(self._option.tagID).empty();
                        self.RecursiveNode(returnData.result,0);
                        $("span[name='parentNode']").click(function () {
                            self.ControlChild(this);
                        });
                        $("button[name='btnAddNode']").click(function () {
                            var parentNodeId = $(this).attr("data-pid");                          
                            _createOrEditModal.open({ maintenanceTemplateId: $("#MaintenanceTemplateId").val(), parentNodeId: parentNodeId });
                        });
                        $("button[name='btnUpdateNode']").click(function () {
                            var nodeid = $(this).attr("data-nodeid");
                            var parentNodeId = $(this).attr("data-pid");
                            _createOrEditModal.open({ maintenanceTemplateId: $("#MaintenanceTemplateId").val(), parentNodeId: parentNodeId, id: nodeid});
                        });
                        $("button[name='btnDelNode']").click(function () {
                            var nodeid = $(this).attr("data-nodeid");
                            var trID = $(this).attr("data-tt-id");
                            var pid = $(this).attr("data-pid");
                            console.log(trID);
                            var childCount = 0;
                            var tbodyTr = $(self._option.tagID).find("tr");
                            $.each(tbodyTr, function (i, _i) {
                                var ttid = $(_i).attr("data-tt-id");
                                if (ttid.indexOf(trID) == 0 && ttid != trID) {
                                    childCount += 1;
                                }
                            });
                            if (childCount == 0) {
                                abp.message.confirm("确定要删除吗？", function (isConfirmed) {
                                    if (isConfirmed) {
                                        _eccpMaintenanceTemplateNodesService.delete({
                                            id: nodeid
                                        }, pid, $("#MaintenanceTemplateId").val()).done(function () {
                                            self.Load();
                                            abp.notify.success(app.localize('SuccessfullyDeleted'));
                                        });
                                    }
                                })
                            }
                            else {
                                abp.notify.info(app.localize('DelNode'));
                            }
                        });                        
                    }
                }
            })
        }
        this.ControlChild = function (node) {           
            var x = $(node).text();
            var tr = $(node).parent().parent().first("tr");
            var trID = $(tr).attr("data-tt-id");            
            if (x == "+") {
                $(node).text("-");
                var tbodyTr = $(self._option.tagID).find("tr");
                $.each(tbodyTr, function (i, _i) {
                    var ttid = $(_i).attr("data-tt-id");
                    if (ttid.indexOf(trID) == 0 && ttid != trID) {
                        var pid = $(_i).attr("pid");
                        var ptype = $("#" + pid + "").find("span[name='parentNode']").text();
                        if (ptype == "+") {
                            $(_i).hide();
                        } else {
                            $(_i).show();
                        }
                    }
                })               
            }
            else {               
                var tbodyTr = $(self._option.tagID).find("tr");
                $.each(tbodyTr, function (i, _i) {
                    if ($(_i).attr("data-tt-id").indexOf(trID) == 0 && $(_i).attr("data-tt-id") != trID) {
                        $(_i).hide();
                    }
                })
                $(node).text("+");
            }
        }
        this.nodeLevel = 0;
        this.nodeForNumber = 0;
        this.RecursiveNode = function (nodes, pttid) {
            var tr = "";
            var ttid = 0;
            $.each(nodes, function (i, node) {
                var display = "style=display:none";
                if (node.parentNodeId == 0) {
                    display = "";
                    self.nodeLevel += 1;
                    self.nodeForNumber = 0;
                    ttid = self.nodeLevel;
                }
                else {
                    ttid = pttid + "-" + (i + 1); 
                }
                var czDiv = "<button class='btn'  name='btnAddNode'    data-pid='" + node.id + "'>添加子节点</button><button class='btn'   name='btnUpdateNode' data-nodeid='" + node.id + "'  data-pid='" + node.parentNodeId + "'>修改</button><button class='btn'  name='btnDelNode'   data-nodeid='" + node.id + "'  data-tt-id='" + ttid + "'  data-pid='" + node.parentNodeId + "'>删除</button>";
                var spanPx = self.nodeForNumber * 20;
                if (node.childNode.length != 0) {

                    tr = "<tr id='" + node.id + "' pid='" + node.parentNodeId + "'   " + display + "  data-tt-id='" + ttid + "'><td width='260px'>" + czDiv + "</td><td><span name='parentNode' style='margin-left: " + spanPx + "px'>+</span>" + node.templateNodeName + "</td><td>" + node.nodeIndex + "</td><td>" + node.actionCode + "</td><td>" + node.dictNodeName + "</td></tr>";
                    $(self._option.tagID).append(tr);
                    self.nodeForNumber += 1;
                    self.RecursiveNode(node.childNode, ttid);
                }
                else {
                    tr = "<tr id='" + node.id + "' pid='" + node.parentNodeId + "'  data-tt-id='" + ttid + "'  " + display + "><td width='260px'>" + czDiv + "</td><td><span style='margin-left: " + spanPx + "px'></span>" + node.templateNodeName + "</td><td>" + node.nodeIndex + "</td><td>" + node.actionCode + "</td><td>" + node.dictNodeName + "</td></tr>";
                    $(self._option.tagID).append(tr);
                }
            });
            self.nodeForNumber -= 1;
        }
    });
})()
