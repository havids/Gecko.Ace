
//syscodemgr --权限模块 js
function Departmentmgr() { }
//获取当前选中节点的 id，ntype
Departmentmgr.selectedNode = function () { var node = $('#department-easyui-tree').tree('getSelected'); return node; }
Departmentmgr.panelOpen = function () { $("#departmentpanel").panel("open"); }
Departmentmgr.panelClose = function () { $("#departmentpanel").panel("close"); }
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

}
//根据id获取当前权限代码的内容
Departmentmgr.TreeNodeClick = function (ntype, id) {

    if (ntype == "root") {
        $('#d_btnAdd').linkbutton('enable');
        $('#d_btnEdit').linkbutton('disable');
        $('#d_btnDel').linkbutton('disable');
        Departmentmgr.panelClose();
    }
    else if (ntype == "department") {
        Departmentmgr.BtnEnable();
        $.getJSON("/SystemSecurity/DepartMentMgr/GetDepartMentInfoJson/" + id, null, function (data, textStatus, jqXHR) {
            $('#d_txtName').textbox("setText", data.Name);
            $('#d_txtOrderId').textbox("setText", data.OrderId);
            $('#d_txtPhone').textbox("setText", data.Phone);
            $('#d_txtExtNumber').textbox("setText", data.ExtNumber);
            $('#d_txtFax').textbox("setText", data.Fax);
            $('#d_txtaRemark').textbox("setText", data.Remark);
        });
        Departmentmgr.panelOpen();
    }

}
//disable 所有的button
Departmentmgr.BtnDisable = function () {

    $('#d_btnAdd').linkbutton('disable');
    $('#d_btnEdit').linkbutton('disable');
    $('#d_btnDel').linkbutton('disable');
    
}
//enable 所有的button
Departmentmgr.BtnEnable = function () {

    $('#d_btnAdd').linkbutton('enable');
    $('#d_btnEdit').linkbutton('enable');
    $('#d_btnDel').linkbutton('enable');

}

$(function () {

    //初始化 window
    $('#departmentwin').window({
        collapsible: false,
        minimizable: false,
        maximizable: false,
        top: 230,
        constrain: true,
        width: 390,
        closed: true,
        modal: true
    });
    //绑定按钮事件 add edit del move
    $("#d_btnAdd").bind("click", function () {
        var sNode = Departmentmgr.selectedNode();
        var typestr = sNode.ntype;
        if (typestr == "root") {
            $("#departmentwin").panel("setTitle", "新增部门");
            var content = "<iframe  id=\"departmentmgr_iframe\" name=\"departmentmgr_iframe\" height=\"280px\" width=\"100%\" frameborder=\"0\"  src=\"/SystemSecurity/DepartMentMgr/DepartMentCreate\"></iframe>";
            $("#departmentwin").html(content);
        }
        else if (typestr == "department") {
            $("#departmentwin").panel("setTitle", "新增部门");
            var content = "<iframe  id=\"departmentmgr_iframe\" name=\"departmentmgr_iframe\" height=\"280px\" width=\"100%\" frameborder=\"0\"  src=\"/SystemSecurity/DepartMentMgr/DepartMentCreate/" + sNode.id + "\"></iframe>";
            $("#departmentwin").html(content);
        }
        $("#departmentwin").window("open");
    })
    $("#d_btnEdit").bind("click", function () {
        var sNode = Departmentmgr.selectedNode();
        var typestr = sNode.ntype;
        $("#departmentwin").panel("setTitle", "编辑部门");
        var content = "<iframe  id=\"departmentmgr_iframe\" name=\"departmentmgr_iframe\" height=\"280px\" width=\"100%\" frameborder=\"0\"  src=\"/SystemSecurity/DepartMentMgr/DepartMentEdit/" + sNode.id + "\"></iframe>";
        $("#departmentwin").html(content);
        $("#departmentwin").window("open");
    })
    $("#d_btnDel").bind("click", function () {
        var sNode = Departmentmgr.selectedNode();
        var nodes = $('#department-easyui-tree').tree('getChildren', Departmentmgr.selectedNode().target);
        if (nodes.length > 0)
            alert("提示：当前部门下含有子部门，所以不能被删除。");
        else {
            if (confirm("您确实要删除当前部门吗？")) {
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
            }
        }
    })

    //加载tree
    Departmentmgr.LoadTree();

})