
//ModuleMgr --权限模块 js
function ModuleMgr() { }
//获取当前选中节点的 id，ntype
ModuleMgr.selectedNode = function () { var node = $('#modulemgr-easyui-tree').tree('getSelected'); return node; }
ModuleMgr.panelOpen = function () { $("#modulemgrpanel").panel("open"); }
ModuleMgr.panelClose = function () { $("#modulemgrpanel").panel("close"); }
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

}
//根据id获取当前权限代码的内容
ModuleMgr.TreeNodeClick = function (ntype, id) {
    if (ntype == "moduletype") {
        ModuleMgr.BtnEnable();
        $.getJSON("/ModuleMgr/ModuleTypeInfo/" + id, null, function (data, textStatus, jqXHR) {
            $('#txtModuleName').textbox("setText", data.Name);
            $('#txtModuleOrderId').textbox("setText", data.OrderId);
            $('#txtModuleRemark').textbox("setText", data.Remark);
            $('#trtxtModuleTag').hide();
            $('#trtxtModuleDisable').hide();
            $('#trtxtModuleSecurity').hide();
            $('#trtxtModuleAddress').hide();
        });
        ModuleMgr.panelOpen();
    }
    else if (ntype == "module") {
        $('#mbtnAddModuleType').linkbutton('disable');
        $('#mbtnAddModule').linkbutton('disable');
        $('#mbtnEdit').linkbutton('enable');
        $('#mbtnDel').linkbutton('enable');
        $.getJSON("/ModuleMgr/ModuleInfo/" + id, null, function (data, textStatus, jqXHR) {
            $('#txtModuleName').textbox("setText", data.Name);
            $('#txtModuleOrderId').textbox("setText", data.OrderId);
            $('#txtModuleRemark').textbox("setText", data.Remark);
            $('#txtModuleTag').textbox("setText", data.Tag);
            $('#txtModuleDisable').html(data.Disabled==1?"是":"否");
            $('#txtModuleAddress').textbox("setText", data.ModuleUrl);
            //清空divRights
            $("#divRights").html('');
            //拼接rights
            $.each(data.ModuleRights, function (i, val)
            {
                var slist = val.split('|');
                if (slist.length == 1)
                    $("#divRights").append("<div>"+"&nbsp;"+slist[0]+"</div>")
                else
                    $("#divRights").append("<div>" + slist[1]+slist[0] + "</div>")
            })
            $('#trtxtModuleTag').show();
            $('#trtxtModuleDisable').show();
            $('#trtxtModuleSecurity').show();
            $('#trtxtModuleAddress').show();
        });
        ModuleMgr.panelOpen();
    }
    else {
        $('#mbtnAddModuleType').linkbutton('enable');
        $('#mbtnAddModule').linkbutton('disable');
        $('#mbtnEdit').linkbutton('disable');
        $('#mbtnDel').linkbutton('disable');
        ModuleMgr.panelClose();
    }

}
//disable 所有的button
ModuleMgr.BtnDisable = function () {
    $('#mbtnAddModuleType').linkbutton('disable');
    $('#mbtnAddModule').linkbutton('disable');
    $('#mbtnEdit').linkbutton('disable');
    $('#mbtnDel').linkbutton('disable');;
}
//enable 所有的button
ModuleMgr.BtnEnable = function () {
    $('#mbtnAddModuleType').linkbutton('enable');
    $('#mbtnAddModule').linkbutton('enable');
    $('#mbtnEdit').linkbutton('enable');
    $('#mbtnDel').linkbutton('enable');
}

$(function () {

    //初始化 window
    $('#modulewin').window({
        collapsible: false,
        minimizable: false,
        maximizable: false,
        top: 230,
        width:430,
        closed: true,
        modal: true
    });
    //绑定按钮事件 add edit del move
    $("#mbtnAddModuleType").bind("click", function () {
        var sNode = ModuleMgr.selectedNode();
        var typestr = sNode.ntype;
        $("#modulewin").panel("setTitle", "新增模块分类");
        if (typestr == "root") {
            var content = "<iframe  id=\"ModuleMgr_iframe\" name=\"ModuleMgr_iframe\" height=\"190px\" width=\"100%\" frameborder=\"0\"  src=\"/SystemSecurity/ModuleMgr/CreateModuleType\"></iframe>";
            $("#modulewin").html(content);
        }
        else if (typestr == "moduletype") {
            var content = "<iframe  id=\"ModuleMgr_iframe\" name=\"ModuleMgr_iframe\" height=\"190px\" width=\"100%\" frameborder=\"0\"  src=\"/SystemSecurity/ModuleMgr/CreateModuleType/" + sNode.id + "\"></iframe>";
            $("#modulewin").html(content);
        }
        $("#modulewin").window("open");
    })

    $("#mbtnAddModule").bind("click", function () {
        var sNode = ModuleMgr.selectedNode();
        var typestr = sNode.ntype;
        $("#modulewin").panel("setTitle", "新增模块");
        var content = "<iframe  id=\"ModuleMgr_iframe\" name=\"ModuleMgr_iframe\" height=\"500px\" width=\"100%\" frameborder=\"0\" src=\"/SystemSecurity/ModuleMgr/CreateModule/" + sNode.id + "\"></iframe>";
        $("#modulewin").html(content);
        $("#modulewin").window("open");
    })

    $("#mbtnEdit").bind("click", function () {
        var sNode = ModuleMgr.selectedNode();
        var typestr = sNode.ntype;
        if (typestr == "moduletype") {
            $("#modulewin").panel("setTitle", "编辑模块分类");
            var content = "<iframe  id=\"ModuleMgr_iframe\" name=\"ModuleMgr_iframe\" height=\"190px\" width=\"100%\" frameborder=\"0\"  src=\"/SystemSecurity/ModuleMgr/EditModuleType/" + sNode.id + "\"></iframe>";
            $("#modulewin").html(content);
        }
        else if (typestr == "module") {
            $("#modulewin").panel("setTitle", "编辑模块");
            var content = "<iframe  id=\"ModuleMgr_iframe\" name=\"ModuleMgr_iframe\" height=\"500px\" width=\"100%\" frameborder=\"0\"  src=\"/SystemSecurity/ModuleMgr/EditModule/" + sNode.id + "\"></iframe>";
            $("#modulewin").html(content);
        }
        $("#modulewin").window("open");
    })
    $("#mbtnDel").bind("click", function () {
        var sNode = ModuleMgr.selectedNode();
        var typestr = sNode.ntype;
        if (typestr == "moduletype") {
            var nodes = $('#modulemgr-easyui-tree').tree('getChildren', ModuleMgr.selectedNode().target);
            if (nodes.length > 0)
                alert("提示：当前模块分类包含子模块分类或模块，所以不能被删除。");
            else {
                if (confirm("您确实要删除当前模块分类吗？")) {
                    $.post("/ModuleMgr/DelModuleType/" + sNode.id, "", function (succeed, textStatus, jqXHR) {
                        if (succeed == "1") {
                            ModuleMgr.LoadTree();
                        }
                        if (succeed == "-2") {
                            alert("提示：当前模块分类包含子模块分类或模块，所以不能被删除。");
                        }
                        else if (succeed == "-1") {
                            //alert(Message.serverError);
                        }
                    })
                }
            }
        }
        else if (typestr == "module") {
            if (confirm("您确实要删除当前模块吗？")) {
                $.post("/ModuleMgr/DelModule/" + sNode.id, "", function (succeed, textStatus, jqXHR) {
                    if (succeed == "1") {
                        ModuleMgr.LoadTree();
                    }
                    if (succeed == "-2") {
                        alert("提示：当前模块包含权限，所以不能被删除。");
                    }
                    else if (succeed == "-1") {
                        //alert(Message.serverError);
                    }
                })
            }
        }
    })

    //加载tree
    ModuleMgr.LoadTree();

})