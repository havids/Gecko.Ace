
//syscodemgr --权限模块 js
function Departmentmgr() { }
//获取当前选中节点的 id，ntype
Departmentmgr.selectedNode = function () { var node = $('#department-easyui-tree').tree('getSelected'); return node; }
Departmentmgr.panelOpen = function () { $(".widget-main").show(); }
Departmentmgr.panelClose = function () { $(".widget-main").hide(); }
//绑定tree 列表
Departmentmgr.LoadTree = function () {
    $("#department-easyui-tree").tree({
        url: '/SystemSecurity/DepartMentMgr/DepartMentTree',
        method: 'get',
        dnd: true,
        onBeforeDrop: function (target, source, point) {
            //如果为 top bottom 不能drop
            if (point != "append")
                return false;

            var targetNode = $(this).tree("getNode", target);
            var pNode = $(this).tree("getParent", source.target);
            if (pNode.id == targetNode.id)
            {
                alert("您选择了无效的目标父节点。");
                return false;
            }

        },
        onDrop: function (target, source, point) {
            var moveUrl = '/DepartMentMgr/DepartMentMove';
            var nowParentId = $(this).tree("getNode", target).id;
            $.ajax({
                url: moveUrl,
                method:"post",
                async: false,
                data: { Id: source.id, newParentPKId: nowParentId }
            }).done(function (data) { Departmentmgr.LoadTree(); });

        },
        onClick: function (node) {
            if (node.ntype) {
                Departmentmgr.TreeNodeClick(node.ntype, node.id);
            }
        }
    });
    //初始化 禁用按钮 隐藏详情
    Departmentmgr.BtnDisable();
    Departmentmgr.panelClose();

}
//根据id获取当前权限代码的内容
Departmentmgr.TreeNodeClick = function (ntype, id) {

    if (ntype == "root") {
        $('#d_btnAdd').removeAttr("disabled");
        $('#d_btnEdit').attr("disabled", true);
        $('#d_btnDel').attr("disabled", true);
        Departmentmgr.panelClose();
    }
    else if (ntype == "department") {
        Departmentmgr.BtnEnable();
        $.getJSON("/SystemSecurity/DepartMentMgr/GetDepartMentInfoJson/" + id, null, function (data, textStatus, jqXHR) {
            $('#d_txtName').html(data.Name);
            $('#d_txtOrderId').html(data.OrderId);
            $('#d_txtPhone').html(data.Phone);
            $('#d_txtExtNumber').html(data.ExtNumber);
            $('#d_txtFax').html(data.Fax);
            $('#d_txtaRemark').html(data.Remark);
        });
        Departmentmgr.panelOpen();
    }

}
//disable 所有的button
Departmentmgr.BtnDisable = function () {

    $('#d_btnAdd').attr("disabled", true);
    $('#d_btnEdit').attr("disabled", true);
    $('#d_btnDel').attr("disabled", true);
    
}
//enable 所有的button
Departmentmgr.BtnEnable = function () {

    $('#d_btnAdd').removeAttr("disabled");
    $('#d_btnEdit').removeAttr("disabled");
    $('#d_btnDel').removeAttr("disabled");

}

$(function () {

    layer.config({
        extend: 'gecko/style.css', //加载您的扩展样式
        skin: 'geckoskin',
        maxmin:false
    });

    //绑定按钮事件 add edit del move
    $("#d_btnAdd").bind("click", function () {
        var sNode = Departmentmgr.selectedNode();
        var typestr = sNode.ntype;
        if (typestr == "root") {

            layer.open({
                title:'新增部门',
                type: 2,
                maxmin:false,
                area: ['530px', '430px'],
                fixed: false, //不固定
                maxmin: false,
                content: '/SystemSecurity/DepartMentMgr/DepartMentCreate'
            });
        }
        else if (typestr == "department") {
            layer.open({
                title: '新增部门',
                type: 2,
                maxmin: false,
                area: ['530px', '430px'],
                fixed: false, //不固定
                maxmin: false,
                content: '/SystemSecurity/DepartMentMgr/DepartMentCreate/'+sNode.id
            });
        }
        $("#departmentwin").window("open");
    })
    $("#d_btnEdit").bind("click", function () {
        var sNode = Departmentmgr.selectedNode();
        var typestr = sNode.ntype;

        layer.open({
            title: '编辑部门',
            type: 2,
            maxmin: false,
            area: ['530px', '430px'],
            fixed: false, //不固定
            maxmin: false,
            content: "/SystemSecurity/DepartMentMgr/DepartMentEdit/" + sNode.id
        });
    })
    $("#d_btnDel").bind("click", function () {
        var sNode = Departmentmgr.selectedNode();
        var nodes = $('#department-easyui-tree').tree('getChildren', Departmentmgr.selectedNode().target);
        if (nodes.length > 0)
            layer.msg("提示：当前部门下含有子部门，所以不能被删除。");    
        else {

            var confirmIndex = layer.confirm('您确实要删除当前部门吗？', {
                btn: ['确定', '取消'] //按钮
            }, function () {
                $.post("/DepartMentMgr/DepartMentDel/" + sNode.id, "", function (succeed, textStatus, jqXHR) {
                    if (succeed == "1") {
                        Departmentmgr.LoadTree();
                        Departmentmgr.panelClose();
                    }
                    if (succeed == "-2") {
                        alert("提示：当前系统代码分类包含系统代码，所以不能被删除。");
                    }
                    else if (succeed == "-1") {
                        //alert(Message.serverError);
                    }
                })
            }, function () {
                layer.close(confirmIndex);
            });

        }
    })

    //加载tree
    Departmentmgr.LoadTree();

})