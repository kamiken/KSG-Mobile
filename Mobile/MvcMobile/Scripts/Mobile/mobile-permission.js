urlCreatePermission = '/Permission/Admin/SavePermission';
urlDeletePermission = '/Permission/Admin/DeletePermission';
urlListPermission = '/Permission/Admin/GetPermissionList';

$(document).ready(function () {
    elementFormPermissionList = "form_permission_list_partial";
    BuildPermissionList(elementFormPermissionList);
    $("#" + elementFormPermissionList).jtable("load");

    BuildPermissionFormPopup();

    $("#btnCreatePermission").click(function () {
        ClearPermissionForm();
        OpenPermissionForm();
        
    });
});

function OpenPermissionForm() {    
    $("#permission_form").dialog("open");
}

function ClosePermissionForm() {
    $("#permission_form").dialog("close");
}

function ClearPermissionForm() {
    var db = new DataBindingScript.DataBinding();
    //db.clearDataForm("permission_form_partial");    
}

function BuildPermissionFormPopup() {
    $("#permission_form").dialog({
        autoOpen: false,
        resizable: true,
        height: 200,
        width: 450,
        modal: true,
        buttons: {
            OK: function (event) {
                SavePermission(function () {
                    $("#" + elementFormPermissionList).jtable("load");
                });
            },
            Cancel: function () {
                $(this).dialog("close");
            }
        }
    });
}

function SavePermission( fn) {    
    var db = new DataBindingScript.DataBinding();
    var tbPermissionObj = DataBindingScript.DataBinding.serializeData("permission_form_partial");

    $.ajax({
        url: urlCreatePermission,
        type: "POST",
        data: JSON.stringify({ 'objPermission': tbPermissionObj }),
        datatype: "json",
        contentType: "application/json charset=utf-8",
        success: function (result) {
            if (result.success == 1) {
                if (fn) {
                    fn();
                }
            }
            else {
                alert("Thêm permission không thành công");
                if (result.errormessages != null && result.errormessages.length > 0) {
                    SetDataToErrorFormWithTmpl(result.errormessages, $("#permission_form_partial").find("ul.error"), true);
                }

            }
        },
        error: function () {
            alert("error");
        }
    });
}

function BuildPermissionList(element) {
    $('#' + element).jtable({
        paging: true,
        pageSize: 10,
        sorting: true,
        defaultSorting: 'PermissionID ASC',
        actions: {
            listAction: urlListPermission
        },
        fields: {
            PermissionID: {
                title: 'Permission ID'
            },
            PermissionName: {
                title: 'Permission Name'
            },
            Edit: {
                title: "",
                display: function (data) {
                    var editBtn = $("<a class='command-button command-button-edit'><a/>");
                    editBtn.click(function () {
                        OpenPermissionForm();
                        var db = new DataBindingScript.DataBinding();
                        db.bindData(data.record, "permission_form_partial");
                    });
                    return editBtn;
                }
            },
            Delete: {
                title: "",
                display: function (data) {
                    var deleteBtn = $("<a class='command-button command-button-delete'><a/>");
                    deleteBtn.click(function (event) {
                        if (confirm("Bạn có chắc muốn xoá mục này không?")) {
//                            var urlDeleteAction = '@Url.Action("DeletePermission","Admin" , new {area = "Permission"})';
                            $.ajax({
                                url: urlDeletePermission,
                                type: "POST",
                                data: JSON.stringify({ 'permissionId': data.record.PermissionID }),
                                datatype: "json",
                                contentType: "application/json charset=utf-8",
                                success: function (result) {
                                    if (result.success == 1) {
                                        $("#" + elementFormPermissionList).jtable("reload");
                                    }
                                    else {
                                        alert("Xoá permission không thành công");
                                        if (result.errormessages != null && result.errormessages.length > 0) {
                                            SetDataToErrorFormWithTmpl(result.errormessages, $("#message_board").find("ul.error"), true);
                                        }
                                    }
                                },
                                error: function () {
                                    alert("error");
                                }
                            });
                        }
                    })
                    return deleteBtn;
                }
            }
        }
    });
}