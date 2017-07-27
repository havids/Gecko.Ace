
//syscodemgr --权限模块 js
function Staffmgr() { }
//获取当前选中节点的 id，ntype
Staffmgr.selectedNode = function () { var node = $('#staff-easyui-tree').tree('getSelected'); return node; }
Staffmgr.staffpanelOpen = function () { $("#staffpanel").panel("open"); }
Staffmgr.staffpanelClose = function () { $("#staffpanel").panel("close"); }
Staffmgr.departmentpanelOpen = function () { $("#staffdepartmentpanel").panel("open"); }
Staffmgr.departmentpanelClose = function () { $("#staffdepartmentpanel").panel("close"); }
//绑定tree 列表
Staffmgr.LoadTree = function () {
    $("#staff-easyui-tree").tree({
        url: '/StaffMgr/StaffInfoTree',
        method: 'get',
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
            //如果要移动的节点为 部门  或者 目标节点是 用户 或者目标节点是 root
            if (sourceType == "department" || targetType == "staff" || targetType == "root")
                return false;

        },
        onDrop: function (target, source, point) {

            var moveUrl = '/StaffMgr/StaffMove';
            var newParentId = $(this).tree("getNode", target).id;
            var sourceType = source.ntype;

            $.ajax({
                url: moveUrl,
                async: false,
                method: 'post',
                data: { id: source.id, newParentPKId: newParentId }
            }).done(function (data) {
                if (data == "1")
                    Staffmgr.LoadTree();
                else if (data == "-1") {
                    //返回失败 暂时不处理 (重新加载当前页面)
                }
            }).fail(function () { /*提交失败 暂时不处理*/ });
            //.always(function(){});

        },
        onClick: function (node) {
            if (node.ntype) {
                Staffmgr.TreeNodeClick(node.ntype, node.id);
            }
        }
    });
}
//根据id获取当前权限代码的内容
Staffmgr.TreeNodeClick = function (ntype, id) {

    if (ntype == "root") {
        Staffmgr.BtnDisable();
        Staffmgr.departmentpanelClose();
        Staffmgr.staffpanelClose();
    }
    else if (ntype == "department") {
        Staffmgr.BtnDisable();
        $('#s_btnAdd').linkbutton('enable');
        $.getJSON("/DepartMentMgr/GetDepartMentInfoJson/" + id, null, function (data, textStatus, jqXHR) {
            $('#staff_dtxtName').textbox("setText", data.Name);
            $('#staff_dtxtOrderId').textbox("setText", data.OrderId);
            $('#staff_dtxtPhone').textbox("setText", data.Phone);
            $('#staff_dtxtExtNumber').textbox("setText", data.ExtNumber);
            $('#staff_dtxtFax').textbox("setText", data.Fax);
            $('#staff_dtxtaRemark').textbox("setText", data.Remark);
        });
        Staffmgr.departmentpanelOpen();
        Staffmgr.staffpanelClose();
    }
    else if(ntype=="staff") {
        Staffmgr.BtnEnable();
        $('#s_btnAdd').linkbutton('disable');
        $.getJSON("/StaffMgr/GetStaffInfo/" + id, null, function (data, textStatus, jqXHR) {
            $('#txtStaffLoginId').textbox("setText", data.LoginId);
            $('#txtStaffCode').textbox("setText", data.Code);
            $('#txtStaffName').textbox("setText", data.Name);
            $('#txtStaffSex').textbox("setText", data.Sex);
            $('#txtStaffMarried').textbox("setText", data.Married);
            $('#txtStaffIdCard').textbox("setText", data.IdCard);
            $('#txtStaffCountry').textbox("setText", data.CountryTag);
            $('#txtStaffNation').textbox("setText", data.NationTag);
            $('#txtStaffPosition').textbox("setText", data.PositionTag);
            $('#txtStaffTitle').textbox("setText", data.TitleTag);
            $('#txtStaffPolitical').textbox("setText", data.PoliticalAppearanceTag);
            $('#txtStaffDegree').textbox("setText", data.DegreeTag);
            $('#txtStaffBirthday').textbox("setText", data.Birthday);
            $('#txtStaffEntersDay').textbox("setText", data.EntersDay);
            $('#txtStaffLeavesDay').textbox("setText", data.LeavesDay);
            $('#txtStaffOfficePhone').textbox("setText", data.OfficePhone);
            $('#txtStaffExtNumber').textbox("setText", data.ExtNumber);
            $('#txtStaffFamilyPhone').textbox("setText", data.FamilyPhone);
            $('#txtStaffCellPhone').textbox("setText", data.CellPhone);
            $('#txtStaffEmail').textbox("setText", data.Email);
            $('#txtaStaffAddress').textbox("setText", data.Addres);
            $('#txtStaffZipCode').textbox("setText", data.ZipCode);
            $('#txtaStaffRemark').textbox("setText", data.Remark);
            if (data.Disabled==1)
                $("#txtStaffDisabled").html("是");
            else
                $("#txtStaffDisabled").html("否");
            $('#txtStaffOrderId').textbox("setText", data.OrderId);
        });
        Staffmgr.departmentpanelClose();
        Staffmgr.staffpanelOpen();
    }

}
//disable 所有的button
Staffmgr.BtnDisable = function () {

    $('#s_btnAdd').linkbutton('disable');
    $('#s_btnEdit').linkbutton('disable');
    $('#s_btnEditPwd').linkbutton('disable');
    $('#s_btnRole').linkbutton('disable');
    $('#s_btnSecurity').linkbutton('disable');
    $('#s_btnDel').linkbutton('disable');

}
//enable 所有的button
Staffmgr.BtnEnable = function () {

    $('#s_btnAdd').linkbutton('enable');
    $('#s_btnEdit').linkbutton('enable');
    $('#s_btnEditPwd').linkbutton('enable');
    $('#s_btnRole').linkbutton('enable');
    $('#s_btnSecurity').linkbutton('enable');
    $('#s_btnDel').linkbutton('enable');

}

$(function () {

    //初始化 window
    $('#staffwin').window({
        collapsible: false,
        minimizable: false,
        maximizable: false,
        top: 230,
        constrain: true,
        width: 599,
        closed: true,
        modal: true
    });

    $('#staffwin1').window({
        collapsible: false,
        minimizable: false,
        maximizable: false,
        top: 230,
        modal: true,
        closed: true
    });

    //绑定按钮事件 add edit del move
    $("#s_btnAdd").bind("click", function () {
        var sNode = Staffmgr.selectedNode();
        var typestr = sNode.ntype;
        $("#staffwin").panel("setTitle", "新增职员");
        var content = "<iframe  id=\"staffmgr_iframe\" name=\"staffmgr_iframe\" height=\"390px\" width=\"100%\" frameborder=\"0\"  src=\"/SystemSecurity/StaffMgr/StaffCreate/" + sNode.id + "\"></iframe>";
        $("#staffwin").html(content);
        $("#staffwin").window("open");
    })
    $("#s_btnEdit").bind("click", function () {
        var sNode = Staffmgr.selectedNode();
        var typestr = sNode.ntype;
        $("#staffwin").panel("setTitle", "编辑职员");
        var content = "<iframe  id=\"staffmgr_iframe\" name=\"staffmgr_iframe\" height=\"390px\" width=\"100%\" frameborder=\"0\"  src=\"/SystemSecurity/StaffMgr/StaffEdit/" + sNode.id + "\"></iframe>";
        $("#staffwin").html(content);
        $("#staffwin").window("open");
    })

    $("#s_btnSecurity").bind("click", function () {
        var sNode = Staffmgr.selectedNode();
        $("#staffwin1").panel("setTitle", "职员授权");
        var content = "<iframe  id=\"staffmgr_iframe\" name=\"staffmgr_iframe\" height=\"390px\" width=\"100%\" frameborder=\"0\"  src=\"/SystemSecurity/StaffMgr/Permissions/" + sNode.id + "\"></iframe>";
        $("#staffwin1").html(content);
        $("#staffwin1").window("open");
    })

    $("#s_btnRole").bind("click", function () {
        var sNode = Staffmgr.selectedNode();
        $("#staffwin1").panel("setTitle", "角色管理");
        var content = "<iframe  id=\"staffmgr_iframe\" name=\"staffmgr_iframe\" height=\"390px\" width=\"100%\" frameborder=\"0\"  src=\"/SystemSecurity/StaffMgr/Roles/" + sNode.id + "\"></iframe>";
        $("#staffwin1").html(content);
        $("#staffwin1").window("open");
    })

    $("#s_btnEditPwd").bind("click", function () {
        var sNode = Staffmgr.selectedNode();
        $("#staffwin1").panel("setTitle", "修改密码");
        var content = "<iframe  id=\"staffmgr_iframe\" name=\"staffmgr_iframe\" height=\"146px\" width=\"100%\" frameborder=\"0\"  src=\"/SystemSecurity/StaffMgr/StaffPassword/" + sNode.id + "\"></iframe>";
        $("#staffwin1").html(content);
        $("#staffwin1").window("open");
    })

    $("#s_btnDel").bind("click", function () {
        var sNode = Staffmgr.selectedNode();
        if (confirm("您确实要删除当前职员吗？")) {
            $.post("/SystemSecurity/StaffMgr/StaffDel/" + sNode.id, "", function (succeed, textStatus, jqXHR) {
                if (succeed == "1") {
                    Staffmgr.LoadTree();
                    Staffmgr.departmentpanelClose();
                    Staffmgr.staffpanelClose();
                    Staffmgr.BtnDisable();
                }
                else if (succeed == "-1") {
                    //alert(Message.serverError);
                }
            })
        }
    })

    //加载tree
    Staffmgr.LoadTree();

})