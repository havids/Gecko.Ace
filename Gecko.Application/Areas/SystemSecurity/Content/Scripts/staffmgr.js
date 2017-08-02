
//syscodemgr --权限模块 js
function Staffmgr() { }
//获取当前选中节点的 id，ntype
Staffmgr.selectedNode = function () { var node = $('#staff-easyui-tree').tree('getSelected'); return node; }
Staffmgr.staffpanelOpen = function () { $(".widget-title").html("职员详情"); $("#staffpanel").show(); }
Staffmgr.staffpanelClose = function () { $("#staffpanel").hide(); }
Staffmgr.departmentpanelOpen = function () { $(".widget-title").html("部门详情"); $("#staffdepartmentpanel").show(); }
Staffmgr.departmentpanelClose = function () { $("#staffdepartmentpanel").hide(); }
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

    Staffmgr.staffpanelClose();
    Staffmgr.departmentpanelClose();
    Staffmgr.BtnDisable();

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
        $('#s_btnAdd').removeAttr("disabled");
        $.getJSON("/DepartMentMgr/GetDepartMentInfoJson/" + id, null, function (data, textStatus, jqXHR) {
            $('#staff_dtxtName').html(data.Name);
            $('#staff_dtxtOrderId').html(data.OrderId);
            $('#staff_dtxtPhone').html(data.Phone);
            $('#staff_dtxtExtNumber').html(data.ExtNumber);
            $('#staff_dtxtFax').html(data.Fax);
            $('#staff_dtxtaRemark').html(data.Remark);
        });
        Staffmgr.departmentpanelOpen();
        Staffmgr.staffpanelClose();
    }
    else if(ntype=="staff") {
        Staffmgr.BtnEnable();
        $('#s_btnAdd').attr("disabled", true);
        $.getJSON("/StaffMgr/GetStaffInfo/" + id, null, function (data, textStatus, jqXHR) {
            $('#txtStaffLoginId').html(data.LoginId);
            $('#txtStaffCode').html(data.Code);
            $('#txtStaffName').html(data.Name);
            $('#txtStaffSex').html(data.Sex);
            $('#txtStaffMarried').html(data.Married);
            $('#txtStaffIdCard').html(data.IdCard);
            $('#txtStaffCountry').html(data.CountryTag);
            $('#txtStaffNation').html(data.NationTag);
            $('#txtStaffPosition').html(data.PositionTag);
            $('#txtStaffTitle').html(data.TitleTag);
            $('#txtStaffPolitical').html(data.PoliticalAppearanceTag);
            $('#txtStaffDegree').html(data.DegreeTag);
            $('#txtStaffBirthday').html(data.Birthday);
            $('#txtStaffEntersDay').html(data.EntersDay);
            $('#txtStaffLeavesDay').html(data.LeavesDay);
            $('#txtStaffOfficePhone').html(data.OfficePhone);
            $('#txtStaffExtNumber').html(data.ExtNumber);
            $('#txtStaffFamilyPhone').html(data.FamilyPhone);
            $('#txtStaffCellPhone').html(data.CellPhone);
            $('#txtStaffEmail').html(data.Email);
            $('#txtaStaffAddress').html(data.Addres);
            $('#txtStaffZipCode').html(data.ZipCode);
            $('#txtaStaffRemark').html(data.Remark);
            if (data.Disabled==1)
                $("#txtStaffDisabled").html("是");
            else
                $("#txtStaffDisabled").html("否");
            $('#txtStaffOrderId').html(data.OrderId);
        });
        Staffmgr.departmentpanelClose();
        Staffmgr.staffpanelOpen();
    }

}
//disable 所有的button
Staffmgr.BtnDisable = function () {

    $('#s_btnAdd').attr("disabled", true);
    $('#s_btnEdit').attr("disabled", true);
    $('#s_btnEditPwd').attr("disabled", true);
    $('#s_btnRole').attr("disabled", true);
    $('#s_btnSecurity').attr("disabled", true);
    $('#s_btnDel').attr("disabled", true);

}
//enable 所有的button
Staffmgr.BtnEnable = function () {

    $('#s_btnAdd').removeAttr("disabled");
    $('#s_btnEdit').removeAttr("disabled");
    $('#s_btnEditPwd').removeAttr("disabled");
    $('#s_btnRole').removeAttr("disabled");
    $('#s_btnSecurity').removeAttr("disabled");
    $('#s_btnDel').removeAttr("disabled");

}

$(function () {

    layer.config({
        extend: 'gecko/style.css', //加载您的扩展样式
        skin: 'geckoskin',
        maxmin: false
    });

    //绑定按钮事件 add edit del move
    $("#s_btnAdd").bind("click", function () {
        var sNode = Staffmgr.selectedNode();

        layer.open({
            title: "新增职员",
            type: 2,
            offset: '28px',
            maxmin: false,
            area: ['730px', '698px'],
            fixed: false, //不固定
            maxmin: false,
            content: "/SystemSecurity/StaffMgr/StaffCreate/" + sNode.id
        });

    })

    $("#s_btnEdit").bind("click", function () {
        var sNode = Staffmgr.selectedNode();
        var typestr = sNode.ntype;

        layer.open({
            title: "编辑职员",
            type: 2,
            offset: '28px',
            maxmin: false,
            area: ['730px', '698px'],
            fixed: false, //不固定
            maxmin: false,
            content: "/SystemSecurity/StaffMgr/StaffEdit/" + sNode.id
        });

    })

    $("#s_btnSecurity").bind("click", function () {
        var sNode = Staffmgr.selectedNode();

        layer.open({
            title: "职员授权",
            type: 2,
            maxmin: false,
            area: ['530px', '430px'],
            fixed: false, //不固定
            maxmin: false,
            content: "/SystemSecurity/StaffMgr/Permissions/" + sNode.id
        });
    })

    $("#s_btnRole").bind("click", function () {
        var sNode = Staffmgr.selectedNode();

        layer.open({
            title: "角色管理",
            type: 2,
            maxmin: false,
            area: ['530px', '430px'],
            fixed: false, //不固定
            maxmin: false,
            content: "/SystemSecurity/StaffMgr/Roles/" + sNode.id
        });

    })

    $("#s_btnEditPwd").bind("click", function () {
        var sNode = Staffmgr.selectedNode();

        layer.open({
            title: "修改密码",
            type: 2,
            maxmin: false,
            area: ['530px', '430px'],
            fixed: false, //不固定
            maxmin: false,
            content: "/SystemSecurity/StaffMgr/StaffPassword/" + sNode.id
        });

    })

    $("#s_btnDel").bind("click", function () {

        var sNode = Staffmgr.selectedNode();
        var confirmIndex = layer.confirm('您确实要删除当前职员吗？', {
            btn: ['确定', '取消'] //按钮
        }, function () {
            $.post("/SystemSecurity/StaffMgr/StaffDel/" + sNode.id, "", function (succeed, textStatus, jqXHR) {
                if (succeed == "1") {
                    SStaffmgr.LoadTree();
                    Staffmgr.departmentpanelClose();
                    Staffmgr.staffpanelClose();
                    Staffmgr.BtnDisable();
                }
                else if (succeed == "-1") {
                    //alert(Message.serverError);
                }
            })
            layer.close(confirmIndex);
        }, function () {
            layer.close(confirmIndex);
        });

    })

    //加载tree
    Staffmgr.LoadTree();

})