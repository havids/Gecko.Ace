
//syscodemgr --权限模块 js
function Syscodemgr() { }
//获取当前选中节点的 id，ntype
Syscodemgr.selectedNode = function () { var node = $('#syscodemgr-easyui-tree').tree('getSelected'); return node; }
Syscodemgr.panelOpen = function () { $("#syscodepanel").panel("open"); }
Syscodemgr.panelClose = function () {$("#syscodepanel").panel("close");}
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
    
}
//根据id获取当前权限代码的内容
Syscodemgr.TreeNodeClick = function (ntype, id) {
    if (ntype == "sysCodeType") {
        Syscodemgr.BtnEnable();
        $.getJSON("/SysCodeMgr/GetSysCodeTypeInfoJson/" + id, null, function (data, textStatus, jqXHR) {
            $('#txtSysCodeTypeTag').textbox("setText", data.Tag);
            $('#txtSysCodeTypeName').textbox("setText", data.Name);
            $('#txtSysCodeTypeRemark').textbox("setText", data.Remark);
            $('#txtSysCodeTypeOrderId').textbox("setText", data.OrderId);
        });
        Syscodemgr.panelOpen();
    }
    else if (ntype == "sysCode") {
        $('#btnAdd').linkbutton('disable');
        $('#btnEdit').linkbutton('enable');
        $('#btnDel').linkbutton('enable');
        $.getJSON("/SysCodeMgr/GetSysCodeInfoJson/" + id, null, function (data, textStatus, jqXHR) {
            $('#txtSysCodeTypeTag').textbox("setText", data.Tag);
            $('#txtSysCodeTypeName').textbox("setText", data.Name);
            $('#txtSysCodeTypeRemark').textbox("setText", data.Remark);
            $('#txtSysCodeTypeOrderId').textbox("setText", data.OrderId);
        });
        Syscodemgr.panelOpen();
    }
    else {
        $('#btnAdd').linkbutton('enable');
        $('#btnEdit').linkbutton('disable');
        $('#btnDel').linkbutton('disable');
        Syscodemgr.panelClose();
    }

}
//disable 所有的button
Syscodemgr.BtnDisable = function () {
    $('#btnAdd').linkbutton('disable');
    $('#btnEdit').linkbutton('disable');
    $('#btnDel').linkbutton('disable');
}
//enable 所有的button
Syscodemgr.BtnEnable = function () {
    $('#btnAdd').linkbutton('enable');
    $('#btnEdit').linkbutton('enable');
    $('#btnDel').linkbutton('enable');
}

$(function () {

    //初始化 window
    $('#syscodewin').window({
        collapsible: false,
        minimizable: false,
        maximizable: false,
        top: 230,
        closed: true,
        modal: true,
        width: 330
    });
    //绑定按钮事件 add edit del move
    $("#btnAdd").bind("click", function () {
        var sNode = Syscodemgr.selectedNode();
        var typestr = sNode.ntype;
        if (typestr == "root") {
            $("#syscodewin").panel("setTitle", "新增系统代码分类");
            var content = "<iframe  id=\"syscodemgr_iframe\" name=\"syscodemgr_iframe\" height=\"230px\" width=\"100%\" frameborder=\"0\"  src=\"/SystemSecurity/SysCodeMgr/SysCodeTypeCreate\"></iframe>";
            $("#syscodewin").html(content);
        }
        else if (typestr == "sysCodeType") {
            $("#syscodewin").panel("setTitle", "新增系统代码");
            var content = "<iframe  id=\"syscodemgr_iframe\" name=\"syscodemgr_iframe\" height=\"230px\" width=\"100%\" frameborder=\"0\"  src=\"/SystemSecurity/SysCodeMgr/SysCodeCreate/" + sNode.id + "\"></iframe>";
            $("#syscodewin").html(content);
        }
        $("#syscodewin").window("open");
    })
    $("#btnEdit").bind("click", function () {
        debugger;
        var sNode = Syscodemgr.selectedNode();
        var typestr = sNode.ntype;
        if (typestr == "sysCodeType") {
            $("#syscodewin").panel("setTitle", "编辑系统代码分类");
            var content = "<iframe  id=\"syscodemgr_iframe\" name=\"syscodemgr_iframe\" height=\"230px\" width=\"100%\" frameborder=\"0\"  src=\"/SystemSecurity/SysCodeMgr/SysCodeTypeEdit/" + sNode.id + "\"></iframe>";
            $("#syscodewin").html(content);
        }
        else if (typestr == "sysCode") {
            $("#syscodewin").panel("setTitle", "编辑系统代码");
            var content = "<iframe  id=\"syscodemgr_iframe\" name=\"syscodemgr_iframe\" height=\"230px\" width=\"100%\" frameborder=\"0\"  src=\"/SystemSecurity/SysCodeMgr/SysCodeEdit/" + sNode.tag + "\"></iframe>";
            $("#syscodewin").html(content);
        }
        $("#syscodewin").window("open");
    })
    $("#btnDel").bind("click", function () {
        var sNode = Syscodemgr.selectedNode();
        var typestr = sNode.ntype;
        if (typestr == "sysCodeType") {
            var nodes = $('#syscodemgr-easyui-tree').tree('getChildren', Syscodemgr.selectedNode().target);
            if (nodes.length > 0)
                alert("提示：当前系统代码分类包含系统代码，所以不能被删除。");
            else {
                if (confirm("您确实要删除当前系统代码分类吗？")) {
                    $.post("/SysCodeMgr/SysCodeTypeDel/" + sNode.id, "", function (succeed, textStatus, jqXHR) {
                        if (succeed == "1") {
                            Syscodemgr.LoadTree();
                        }
                        if (succeed == "-2") {
                            alert("提示：当前系统代码分类包含系统代码，所以不能被删除。");
                        }
                        else if (succeed == "-1") {
                            //alert(Message.serverError);
                        }
                    })
                }
            }
        }
        else if (typestr == "sysCode") {
            if (confirm("您确实要删除当前系统代码吗？")) {
                $.post("/SysCodeMgr/SysCodeDel/" + sNode.id, "", function (succeed, textStatus, jqXHR) {
                    if (succeed == "1") {
                        Syscodemgr.LoadTree();
                    }
                    if (succeed == "-2") {
                        alert("提示：当前系统代码分类包含系统代码，所以不能被删除。");
                    }
                    else if (succeed == "-1") {
                        //alert(Message.serverError);
                    }
                })
            }
        }
    })
   
    //加载tree
    Syscodemgr.LoadTree();

})