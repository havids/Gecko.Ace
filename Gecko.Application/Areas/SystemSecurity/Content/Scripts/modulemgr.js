
//ModuleMgr --权限模块 js
function ModuleMgr() { }
//获取当前选中节点的 id，ntype
ModuleMgr.selectedNode = function () { var node = $('#modulemgr-easyui-tree').tree('getSelected'); return node; }
ModuleMgr.panelOpen = function () { $(".widget-main").show(); }
ModuleMgr.panelClose = function () { $(".widget-main").hide(); }
//绑定tree 列表
ModuleMgr.LoadTree = function () {
    $("#modulemgr-easyui-tree").tree({
        url: '/ModuleMgr/ModuleTypeInfoTree',
        method: 'post',
        dnd: false,
        //onBeforeDrop: function (target, source, point) {
        //    //如果为 top bottom 不能drop
        //    if (point != "append")
        //        return false;

        //    var targetNode = $(this).tree("getNode", target);
        //    var pNode = $(this).tree("getParent", source.target);
        //    if (pNode.id == targetNode.id) {
        //        alert("您选择了无效的目标父节点。");
        //        return false;
        //    }

        //},
        //onDrop: function (target, source, point) {
        //    var moveUrl = '';
        //    var nowParentId = $(this).tree("getNode", target).id;
        //    if (nowParentId == "0")
        //        moveUrl = '/DepartMentMgr/DepartMentMove/' + source.id;
        //    else
        //        moveUrl = '/DepartMentMgr/DepartMentMove/' + source.id + "/" + nowParentId;

        //    $.ajax({
        //        url: moveUrl,
        //        async: false
        //    }).done(function (data) { Departmentmgr.LoadTree(); });

        //},
        onClick: function (node) {
            if (node.ntype) {
                ModuleMgr.TreeNodeClick(node.ntype, node.id);
            }
        }
    });

    ModuleMgr.panelClose();
    ModuleMgr.BtnDisable();

}
//根据id获取当前权限代码的内容
ModuleMgr.TreeNodeClick = function (ntype, id) {
    if (ntype == "moduletype") {
        ModuleMgr.BtnEnable();
        $.getJSON("/ModuleMgr/ModuleTypeInfo/" + id, null, function (data, textStatus, jqXHR) {
            $('#txtModuleName').html(data.Name);
            $('#txtModuleOrderId').html(data.OrderId);
            $('#txtModuleRemark').html(data.Remark);
            $('#txtModuleTag').html("");
            $('#txtModuleDisable').html("");
            $('#trtxtModuleSecurity').html("");
            $('#txtModuleAddress').html("");
            $("#divRights").html("");
        });
        ModuleMgr.panelOpen();
    }
    else if (ntype == "module") {
        $('#mbtnAddModuleType').attr("disabled", true); 
        $('#mbtnAddModule').attr("disabled", true);
        $('#mbtnEdit').removeAttr("disabled");
        $('#mbtnDel').removeAttr("disabled");
        $.getJSON("/ModuleMgr/ModuleInfo/" + id, null, function (data, textStatus, jqXHR) {
            $('#txtModuleName').html(data.Name);
            $('#txtModuleOrderId').html(data.OrderId);
            $('#txtModuleRemark').html(data.Remark);
            $('#txtModuleTag').html(data.Tag);
            $('#txtModuleDisable').html(data.Disabled?"是":"否");
            $('#txtModuleAddress').html(data.ModuleUrl);
            //清空divRights
            $("#divRights").html('');
            //拼接rights
            $.each(data.ModuleRights, function (i, val)
            {
                var slist = val.split('|');
                if (slist.length == 1)
                    $("#divRights").append("<div><i class=\"glyphicon glyphicon-remove\"></i>" + slist[0] + "</div>")
                else
                    $("#divRights").append("<div><i class=\"glyphicon glyphicon-ok red\"></i>" + slist[0] + "</div>")
            })
        });
        ModuleMgr.panelOpen();
    }
    else {
        $('#mbtnAddModuleType').removeAttr("disabled");
        $('#mbtnAddModule').attr("disabled", true);
        $('#mbtnEdit').attr("disabled", true);
        $('#mbtnDel').attr("disabled", true);
        ModuleMgr.panelClose();
    }

}
//disable 所有的button
ModuleMgr.BtnDisable = function () {
    $('#mbtnAddModuleType').attr("disabled", true);
    $('#mbtnAddModule').attr("disabled", true);
    $('#mbtnEdit').attr("disabled", true);
    $('#mbtnDel').attr("disabled", true);
}
//enable 所有的button
ModuleMgr.BtnEnable = function () {
    $('#mbtnAddModuleType').removeAttr("disabled");
    $('#mbtnAddModule').removeAttr("disabled");
    $('#mbtnEdit').removeAttr("disabled");
    $('#mbtnDel').removeAttr("disabled");
}

$(function () {

    layer.config({
        extend: 'gecko/style.css', //加载您的扩展样式
        skin: 'geckoskin',
        maxmin: false
    });
    //绑定按钮事件 add edit del move
    $("#mbtnAddModuleType").bind("click", function () {
        var sNode = ModuleMgr.selectedNode();
        var typestr = sNode.ntype;
        var url = '/SystemSecurity/ModuleMgr/CreateModuleType';
        //if (typestr == "moduletype") {
        //    var content = "<iframe  id=\"ModuleMgr_iframe\" name=\"ModuleMgr_iframe\" height=\"190px\" width=\"100%\" frameborder=\"0\"  src=\"/SystemSecurity/ModuleMgr/CreateModuleType/" + sNode.id + "\"></iframe>";
        //    $("#modulewin").html(content);
        //}

        layer.open({
            title: '新增模块分类',
            type: 2,
            maxmin: false,
            area: ['530px', '430px'],
            fixed: false, //不固定
            maxmin: false,
            content: url
        });
    })

    $("#mbtnAddModule").bind("click", function () {
        var sNode = ModuleMgr.selectedNode();
        var typestr = sNode.ntype;

        layer.open({
            title: '新增模块',
            type: 2,
            maxmin: false,
            area: ['530px', '430px'],
            fixed: false, //不固定
            maxmin: false,
            content: "/SystemSecurity/ModuleMgr/CreateModule/" + sNode.id
        });

    })

    $("#mbtnEdit").bind("click", function () {
        var sNode = ModuleMgr.selectedNode();
        var typestr = sNode.ntype;
        if (typestr == "moduletype") {

            layer.open({
                title: '编辑模块分类',
                type: 2,
                maxmin: false,
                area: ['530px', '430px'],
                fixed: false, //不固定
                maxmin: false,
                content: "/SystemSecurity/ModuleMgr/EditModuleType/" + sNode.id
            });

        }
        else if (typestr == "module") {

            layer.open({
                title: '编辑模块',
                type: 2,
                maxmin: false,
                area: ['530px', '430px'],
                fixed: false, //不固定
                maxmin: false,
                content: "/SystemSecurity/ModuleMgr/EditModule/" + sNode.id
            });

        }

    })
    $("#mbtnDel").bind("click", function () {
        var sNode = ModuleMgr.selectedNode();
        var typestr = sNode.ntype;
        if (typestr == "moduletype") {
            var nodes = $('#modulemgr-easyui-tree').tree('getChildren', ModuleMgr.selectedNode().target);
            if (nodes.length > 0)
                layer.msg("提示：当前模块分类包含子模块分类或模块，所以不能被删除。");
            else {

                var confirmIndex = layer.confirm('您确实要删除当前模块分类吗？', {
                    btn: ['确定', '取消'] //按钮
                }, function () {
                    $.post("/ModuleMgr/DelModuleType/" + sNode.id, "", function (succeed, textStatus, jqXHR) {
                        if (succeed == "1") {
                            ModuleMgr.LoadTree();
                            layer.msg("操作成功");
                        }
                        if (succeed == "-2") {
                            layer.msg("提示：当前模块分类包含子模块分类或模块，所以不能被删除。");
                        }
                        else if (succeed == "-1") {
                            layer.msg("操作失败");
                        }
                    })
                    layer.close(confirmIndex);
                }, function () {
                    layer.close(confirmIndex);
                });

            }
        }
        else if (typestr == "module") {

            var confirmIndex = layer.confirm('您确实要删除当前模块吗？', {
                btn: ['确定', '取消'] //按钮
            }, function () {
                $.post("/ModuleMgr/DelModule/" + sNode.id, "", function (succeed, textStatus, jqXHR) {
                    if (succeed == "1") {
                        ModuleMgr.LoadTree();
                        layer.msg("操作成功");
                    }
                    if (succeed == "-2") {
                        layer.msg("提示：当前模块包含权限，所以不能被删除。");
                    }
                    else if (succeed == "-1") {
                        layer.msg("操作失败");
                    }
                })
                layer.close(confirmIndex);
            }, function () {
                layer.close(confirmIndex);
            });

        }
    })

    //加载tree
    ModuleMgr.LoadTree();

})