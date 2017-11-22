
//syscodemgr --权限模块 js
function Syscodemgr() { }
//获取当前选中节点的 id，ntype
Syscodemgr.selectedNode = function () { var node = $('#syscodemgr-easyui-tree').tree('getSelected'); return node; }
Syscodemgr.panelOpen = function () { $(".widget-main").show(); }
Syscodemgr.panelClose = function () { $(".widget-main").hide(); }
//绑定tree 列表
Syscodemgr.LoadTree = function () {
    $("#syscodemgr-easyui-tree").tree({
        url: '/SysCodeMgr/SysCodeTree',
        method: 'get',
        onClick: function (node) {
            if (node.ntype) {
                Syscodemgr.TreeNodeClick(node.ntype, node.id);
            }
        }
    });
    Syscodemgr.panelClose();
    Syscodemgr.BtnDisable();
}
//根据id获取当前权限代码的内容
Syscodemgr.TreeNodeClick = function (ntype, id) {
    if (ntype == "sysCodeType") {
        Syscodemgr.BtnEnable();
        $.getJSON("/SysCodeMgr/GetSysCodeTypeInfoJson/" + id, null, function (data, textStatus, jqXHR) {
            $('#txtSysCodeTypeTag').html(data.Tag);
            $('#txtSysCodeTypeName').html(data.Name);
            $('#txtSysCodeTypeRemark').html(data.Remark);
            $('#txtSysCodeTypeOrderId').html(data.OrderId);
        });
        Syscodemgr.panelOpen();
    }
    else if (ntype == "sysCode") {
        $('#btnAdd').attr("disabled", true);
        $('#btnEdit').removeAttr("disabled");
        $('#btnDel').removeAttr("disabled");
        $.getJSON("/SysCodeMgr/GetSysCodeInfoJson/" + id, null, function (data, textStatus, jqXHR) {
            $('#txtSysCodeTypeTag').html(data.Tag);
            $('#txtSysCodeTypeName').html(data.Name);
            $('#txtSysCodeTypeRemark').html(data.Remark);
            $('#txtSysCodeTypeOrderId').html(data.OrderId);
        });
        Syscodemgr.panelOpen();
    }
    else {
        $('#btnAdd').removeAttr("disabled");
        $('#btnEdit').attr("disabled", true);
        $('#btnDel').attr("disabled", true);
        Syscodemgr.panelClose();
    }

}
//disable 所有的button
Syscodemgr.BtnDisable = function () {
    $('#btnAdd').attr("disabled", true);
    $('#btnEdit').attr("disabled", true);
    $('#btnDel').attr("disabled", true);
}
//enable 所有的button
Syscodemgr.BtnEnable = function () {
    $('#btnAdd').removeAttr("disabled");
    $('#btnEdit').removeAttr("disabled");
    $('#btnDel').removeAttr("disabled");
}

$(function () {

    layer.config({
        extend: 'gecko/style.css', //加载您的扩展样式
        skin: 'geckoskin',
        maxmin: false
    });

    //绑定按钮事件 add edit del move
    $("#btnAdd").bind("click", function () {
        var sNode = Syscodemgr.selectedNode();
        var typestr = sNode.ntype;
        var url = "/SystemSecurity/SysCodeMgr/SysCodeTypeCreate";
        var title = "新增系统代码分类";
        if (typestr == "sysCodeType") {
            title = "新增系统代码";
            url = "/SystemSecurity/SysCodeMgr/SysCodeCreate/" + sNode.id;
        }
        
        layer.open({
            title: title,
            type: 2,
            maxmin: false,
            area: ['530px', '430px'],
            fixed: false, //不固定
            maxmin: false,
            content: url
        });

    })
    $("#btnEdit").bind("click", function () {
       
        var sNode = Syscodemgr.selectedNode();
        var typestr = sNode.ntype;
        var url = "/SystemSecurity/SysCodeMgr/SysCodeTypeEdit/"+sNode.id;
        var title = "编辑系统代码分类";

        if (typestr == "sysCode") {
            url = "/SystemSecurity/SysCodeMgr/SysCodeEdit/" + sNode.tag;
            title = "编辑系统代码";
        }
        
        layer.open({
            title: title,
            type: 2,
            maxmin: false,
            area: ['530px', '430px'],
            fixed: false, //不固定
            maxmin: false,
            content: url
        });

    })

    $("#btnDel").bind("click", function () {
        var sNode = Syscodemgr.selectedNode();
        var typestr = sNode.ntype;
        if (typestr == "sysCodeType") {
            var nodes = $('#syscodemgr-easyui-tree').tree('getChildren', Syscodemgr.selectedNode().target);
            if (nodes.length > 0)
                layer.msg("提示：当前系统代码分类包含系统代码，所以不能被删除。");
            else {

                var confirmIndex = layer.confirm('您确实要删除当前系统代码分类吗？', {
                    btn: ['确定', '取消'] //按钮
                }, function () {
                    $.post("/SysCodeMgr/SysCodeTypeDel/" + sNode.id, "", function (succeed, textStatus, jqXHR) {
                        if (succeed == "1") {
                            Syscodemgr.LoadTree();
                            layer.msg("操作成功");
                        }
                        if (succeed == "-2") {
                            layer.msg("提示：当前系统代码分类包含系统代码，所以不能被删除。");
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
        else if (typestr == "sysCode") {

            var confirmIndex = layer.confirm('您确实要删除当前系统代码吗？', {
                btn: ['确定', '取消'] //按钮
            }, function () {
                $.post("/SysCodeMgr/SysCodeDel/" + sNode.id, "", function (succeed, textStatus, jqXHR) {
                    if (succeed == "1") {
                        Syscodemgr.LoadTree();
                        layer.msg("操作成功");
                    }
                    if (succeed == "-2") {
                        layer.msg("提示：当前系统代码分类包含系统代码，所以不能被删除。");
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
    Syscodemgr.LoadTree();
    Syscodemgr.panelClose();

})