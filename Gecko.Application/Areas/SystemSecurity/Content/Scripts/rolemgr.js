
//RoleMgr --权限模块 js
function RoleMgr() { }
//获取当前选中节点的 id，ntype
RoleMgr.selectedNode = function () { var node = $('#rolemgr-easyui-tree').tree('getSelected'); return node; }
RoleMgr.panelOpen = function () { $(".widget-main").show(); }
RoleMgr.panelClose = function () { $(".widget-main").hide(); }
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
        $('#rbtnSecurity').attr("disabled", true);
        $.getJSON("/RoleMgr/RoleTypeInfo/" + id, null, function (data, textStatus, jqXHR) {
            $('#txtRoleName').html(data.Name);
            $('#txtRoleOrderId').html(data.OrderId);
            $('#txtRoleRemark').html(data.Remark);
        });
        RoleMgr.panelOpen();
    }
    else if (ntype == "role") {
        RoleMgr.BtnDisable();
        $('#rbtnSecurity').removeAttr("disabled");
        $('#rbtnEdit').removeAttr("disabled");
        $('#rbtnDel').removeAttr("disabled");
        $.getJSON("/RoleMgr/RoleInfo/" + id, null, function (data, textStatus, jqXHR) {
            $('#txtRoleName').html(data.Name);
            $('#txtRoleOrderId').html(data.OrderId);
            $('#txtRoleRemark').html(data.Remark);
        });
        RoleMgr.panelOpen();
    }
    else {
        RoleMgr.BtnDisable();
        $('#rbtnAddType').removeAttr("disabled");
       
        RoleMgr.panelClose();
    }

}
//disable 所有的button
RoleMgr.BtnDisable = function () {
    $('#rbtnAddType').attr("disabled", true);
    $('#rbtnAdd').attr("disabled", true);
    $('#rbtnSecurity').attr("disabled", true);
    $('#rbtnEdit').attr("disabled", true);
    $('#rbtnDel').attr("disabled", true);
}
//enable 所有的button
RoleMgr.BtnEnable = function () {
    $('#rbtnAddType').removeAttr("disabled");
    $('#rbtnAdd').removeAttr("disabled");
    $('#rbtnSecurity').removeAttr("disabled");
    $('#rbtnEdit').removeAttr("disabled");
    $('#rbtnDel').removeAttr("disabled");
}

$(function () {

    layer.config({
        extend: 'gecko/style.css', //加载您的扩展样式
        skin: 'geckoskin',
        maxmin: false
    });

    //绑定按钮事件 add edit del move
    $("#rbtnAddType").bind("click", function () {
        var sNode = RoleMgr.selectedNode();
        var typestr = sNode.ntype;
        
        var url = '/SystemSecurity/RoleMgr/CreateRoleType';
        if (typestr == "roletype") {
            url = '/SystemSecurity/RoleMgr/CreateRoleType/' + sNode.id;
        }

        layer.open({
            title: '新增角色分类',
            type: 2,
            maxmin: false,
            area: ['530px', '430px'],
            fixed: false, //不固定
            maxmin: false,
            content: '/SystemSecurity/RoleMgr/CreateRoleType/' + sNode.id
        });

    })

    $("#rbtnAdd").bind("click", function () {
        var sNode = RoleMgr.selectedNode();
        layer.open({
            title: '新增角色',
            type: 2,
            maxmin: false,
            area: ['530px', '430px'],
            fixed: false, //不固定
            maxmin: false,
            content: '/SystemSecurity/RoleMgr/CreateRole/' + sNode.id
        });

    })

    $("#rbtnSecurity").bind("click", function () {
        var sNode = RoleMgr.selectedNode();
        layer.open({
            title: '新增角色',
            type: 2,
            maxmin: false,
            area: ['530px', '430px'],
            fixed: false, //不固定
            maxmin: false,
            content: '/SystemSecurity/RoleMgr/Permissions/' + sNode.id
        });

    })

    $("#rbtnEdit").bind("click", function () {
        var sNode = RoleMgr.selectedNode();
        var typestr = sNode.ntype;
        if (typestr == "roletype") {
            layer.open({
                title: '编辑角色分类',
                type: 2,
                maxmin: false,
                area: ['530px', '430px'],
                fixed: false, //不固定
                maxmin: false,
                content: '/SystemSecurity/RoleMgr/EditRoleType/' + sNode.id
            });
        }
        else if (typestr == "role") {

            layer.open({
                title: '编辑角色',
                type: 2,
                maxmin: false,
                area: ['530px', '430px'],
                fixed: false, //不固定
                maxmin: false,
                content: '/SystemSecurity/RoleMgr/EditRole/' + sNode.id
            });

        }
    })
    $("#rbtnDel").bind("click", function () {
        var sNode = RoleMgr.selectedNode();
        var typestr = sNode.ntype;
        if (typestr == "roletype") {
            var nodes = $('#rolemgr-easyui-tree').tree('getChildren', RoleMgr.selectedNode().target);
            if (nodes.length > 0)
                layer.msg("提示：当前角色分类包含子角色分类或角色，所以不能被删除。");
            else {

                var confirmIndex = layer.confirm('您确实要删除当前角色分类吗？', {
                    btn: ['确定', '取消'] //按钮
                }, function () {
                    $.post("/RoleMgr/DelRoleType/" + sNode.id, "", function (succeed, textStatus, jqXHR) {
                        if (succeed == "1") {
                            RoleMgr.LoadTree();
                            layer.msg("操作成功");
                        }
                        if (succeed == "-2") {
                            layer.msg("提示：当前角色分类包含子角色，所以不能被删除。");
                        }
                        else if (succeed == "-1") {
                            //alert(Message.serverError);
                        }
                    })
                    layer.close(confirmIndex);
                }, function () {
                    layer.close(confirmIndex);
                });

            }
        }
        else if (typestr == "role") {

            var confirmIndex = layer.confirm('您确实要删除当前角色吗？', {
                btn: ['确定', '取消'] //按钮
            }, function () {
                $.post("/RoleMgr/DelRole/" + sNode.id, "", function (succeed, textStatus, jqXHR) {
                    if (succeed == "1") {
                        RoleMgr.LoadTree();
                        layer.msg("操作成功");
                    }
                    if (succeed == "-2") {
                        layer.msg("提示：当前角色包含子角色，所以不能被删除。");
                    }
                    else if (succeed == "-1") {
                        //alert(Message.serverError);
                    }
                })
                layer.close(confirmIndex);
            }, function () {
                layer.close(confirmIndex);
            });

        }
    })

    //加载tree
    RoleMgr.LoadTree();

})