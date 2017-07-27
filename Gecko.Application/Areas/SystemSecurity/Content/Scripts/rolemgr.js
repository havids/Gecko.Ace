
//RoleMgr --权限模块 js
function RoleMgr() { }
//获取当前选中节点的 id，ntype
RoleMgr.selectedNode = function () { var node = $('#rolemgr-easyui-tree').tree('getSelected'); return node; }
RoleMgr.panelOpen = function () { $("#rolemgrpanel").panel("open"); }
RoleMgr.panelClose = function () { $("#rolemgrpanel").panel("close"); }
//绑定tree 列表
RoleMgr.LoadTree = function () {
    $("#rolemgr-easyui-tree").tree({
        url: '/RoleMgr/RoleTypeInfoTree',
        method: 'post',
        lines:true,
        dnd: true,
        onBeforeDrop: function (target, source, point) {
            //如果为 top bottom 不能drop
            if (point != "append")
                return false;
            //如果移动节点 与 父节点相同
            var targetNode = $(this).tree("getNode", target);
            var pNode = $(this).tree("getParent", source.target);
            if (pNode.id == targetNode.id) {
                alert("您选择了无效的目标父节点。");
                return false;
            }
            //获取节点的type
            var targetType = targetNode.ntype;
            var sourceType = source.ntype;
            //如果节点type都为 role 返回false
            if (targetType == "role" && sourceType == "role")
                return false;
            //如果roletype 移动到 role 返回 false
            if (targetType == "role" && sourceType == "roletype")
                return false;
            //如果role 移动到 root 返回 false
            if (targetType == "root" && sourceType == "role")
                return false;

        },
        onDrop: function (target, source, point) {

            var moveUrl = '';
            var newParentId = $(this).tree("getNode", target).id;
            var sourceType = source.ntype;
            if (sourceType == 'role')
                moveUrl = '/RoleMgr/MoveRole';
            else if(sourceType == "roletype")
                moveUrl = '/RoleMgr/MoveRoleType';

            $.ajax({
                url: moveUrl,
                async:false,
                method: 'post',
                data: { id: source.id, newParentPKId: newParentId }
            }).done(function (data) {
                if(data=="1")
                    RoleMgr.LoadTree();
                else if(data=="-1")
                {
                    //返回失败 暂时不处理 (重新加载当前页面)
                }
            }).fail(function () { /*提交失败 暂时不处理*/ });
            //.always(function(){});

        },
        onClick: function (node) {
            if (node.ntype) {
                RoleMgr.TreeNodeClick(node.ntype, node.id);
            }
        }
    });

}
//根据id获取当前权限代码的内容
RoleMgr.TreeNodeClick = function (ntype, id) {
    if (ntype == "roletype") {
        RoleMgr.BtnEnable();
        $('#rbtnSecurity').linkbutton('disable');
        $.getJSON("/RoleMgr/RoleTypeInfo/" + id, null, function (data, textStatus, jqXHR) {
            $('#txtRoleName').textbox("setText", data.Name);
            $('#txtRoleOrderId').textbox("setText", data.OrderId);
            $('#txtRoleRemark').textbox("setText", data.Remark);
        });
        RoleMgr.panelOpen();
    }
    else if (ntype == "role") {
        RoleMgr.BtnDisable();
        $('#rbtnSecurity').linkbutton('enable');
        $('#rbtnEdit').linkbutton('enable');
        $('#rbtnDel').linkbutton('enable');
        $.getJSON("/RoleMgr/RoleInfo/" + id, null, function (data, textStatus, jqXHR) {
            $('#txtRoleName').textbox("setText", data.Name);
            $('#txtRoleOrderId').textbox("setText", data.OrderId);
            $('#txtRoleRemark').textbox("setText", data.Remark);
        });
        RoleMgr.panelOpen();
    }
    else {
        RoleMgr.BtnDisable();
        $('#rbtnAddType').linkbutton('enable');
       
        RoleMgr.panelClose();
    }

}
//disable 所有的button
RoleMgr.BtnDisable = function () {
    $('#rbtnAddType').linkbutton('disable');
    $('#rbtnAdd').linkbutton('disable');
    $('#rbtnSecurity').linkbutton('disable');
    $('#rbtnEdit').linkbutton('disable');
    $('#rbtnDel').linkbutton('disable');
}
//enable 所有的button
RoleMgr.BtnEnable = function () {
    $('#rbtnAddType').linkbutton('enable');
    $('#rbtnAdd').linkbutton('enable');
    $('#rbtnSecurity').linkbutton('enable');
    $('#rbtnEdit').linkbutton('enable');
    $('#rbtnDel').linkbutton('enable');
}

$(function () {

    //初始化 window
    $('#rolewin').window({
        collapsible: false,
        minimizable: false,
        maximizable: false,
        top:230,
        closed: true,
        width: 328,
        modal: true
    });
    //绑定按钮事件 add edit del move
    $("#rbtnAddType").bind("click", function () {
        var sNode = RoleMgr.selectedNode();
        var typestr = sNode.ntype;
        $("#rolewin").panel("setTitle", "新增角色分类");
        if (typestr == "root") {
            var content = "<iframe  id=\"RoleMgr_iframe\" name=\"RoleMgr_iframe\" height=\"300px\" width=\"100%\" frameborder=\"0\"  src=\"/SystemSecurity/RoleMgr/CreateRoleType\"></iframe>";
            $("#rolewin").html(content);
        }
        else if (typestr == "roletype") {
            var content = "<iframe  id=\"RoleMgr_iframe\" name=\"RoleMgr_iframe\" height=\"300px\" width=\"100%\" frameborder=\"0\"  src=\"/SystemSecurity/RoleMgr/CreateRoleType/" + sNode.id + "\"></iframe>";
            $("#rolewin").html(content);
        }
        $("#rolewin").window("open");
    })

    $("#rbtnAdd").bind("click", function () {
        var sNode = RoleMgr.selectedNode();
        var typestr = sNode.ntype;
        $("#rolewin").panel("setTitle", "新增角色");
        var content = "<iframe  id=\"RoleMgr_iframe\" name=\"RoleMgr_iframe\" height=\"300px\" width=\"100%\" frameborder=\"0\"  src=\"/SystemSecurity/RoleMgr/CreateRole/" + sNode.id + "\"></iframe>";
        $("#rolewin").html(content);
        $("#rolewin").window("open");
    })

    $("#rbtnSecurity").bind("click", function () {
        var sNode = RoleMgr.selectedNode();
        var typestr = sNode.ntype;
        $("#rolewin").panel("setTitle", "角色授权");
        var content = "<iframe  id=\"RoleMgr_iframe\" name=\"RoleMgr_iframe\" height=\"430px\" width=\"100%\" frameborder=\"0\"  src=\"/SystemSecurity/RoleMgr/Permissions/" + sNode.id + "\"></iframe>";
        $("#rolewin").html(content);
        $("#rolewin").window("open");
    })

    $("#rbtnEdit").bind("click", function () {
        var sNode = RoleMgr.selectedNode();
        var typestr = sNode.ntype;
        if (typestr == "roletype") {
            $("#rolewin").panel("setTitle", "编辑角色分类");
            var content = "<iframe  id=\"RoleMgr_iframe\" name=\"RoleMgr_iframe\" height=\"300px\" width=\"100%\" frameborder=\"0\"  src=\"/SystemSecurity/RoleMgr/EditRoleType/" + sNode.id + "\"></iframe>";
            $("#rolewin").html(content);
        }
        else if (typestr == "role") {
            $("#rolewin").panel("setTitle", "编辑角色");
            var content = "<iframe  id=\"RoleMgr_iframe\" name=\"RoleMgr_iframe\" height=\"300px\" width=\"100%\" frameborder=\"0\"  src=\"/SystemSecurity/RoleMgr/EditRole/" + sNode.id + "\"></iframe>";
            $("#rolewin").html(content);
        }
        $("#rolewin").window("open");
    })
    $("#rbtnDel").bind("click", function () {
        var sNode = RoleMgr.selectedNode();
        var typestr = sNode.ntype;
        if (typestr == "roletype") {
            var nodes = $('#rolemgr-easyui-tree').tree('getChildren', RoleMgr.selectedNode().target);
            if (nodes.length > 0)
                alert("提示：当前角色分类包含子角色分类或角色，所以不能被删除。");
            else {
                if (confirm("您确实要删除当前角色分类吗？")) {
                    $.post("/RoleMgr/DelRoleType/" + sNode.id, "", function (succeed, textStatus, jqXHR) {
                        if (succeed == "1") {
                            RoleMgr.LoadTree();
                        }
                        if (succeed == "-2") {
                            alert("提示：当前角色分类包含子角色，所以不能被删除。");
                        }
                        else if (succeed == "-1") {
                            //alert(Message.serverError);
                        }
                    })
                }
            }
        }
        else if (typestr == "role") {
            if (confirm("您确实要删除当前角色吗？")) {
                $.post("/RoleMgr/DelRole/" + sNode.id, "", function (succeed, textStatus, jqXHR) {
                    if (succeed == "1") {
                        RoleMgr.LoadTree();
                    }
                    if (succeed == "-2") {
                        alert("提示：当前角色包含子角色，所以不能被删除。");
                    }
                    else if (succeed == "-1") {
                        //alert(Message.serverError);
                    }
                })
            }
        }
    })

    //加载tree
    RoleMgr.LoadTree();

})