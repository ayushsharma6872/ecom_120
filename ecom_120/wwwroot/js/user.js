var dataTable;

$(document).ready(function () {
    loadDataTable();
});

function loadDataTable() {
    dataTable = $('#tblDataA').DataTable({
        "ajax": {
            "url": "/Admin/User/GetAll"
        },
        "columns": [
            { "data": "name", "width": "15%" },
            { "data": "email", "width": "15%" },
            { "data": "phoneNumber", "width": "15%" },
            { "data": "company.name", "width": "15%" },
            { "data": "role", "width": "15%" },
            {
                "data": "id", // just pass id, lockoutEnd separately
                "render": function (data, type, row) {
                    let today = new Date().getTime();
                    let lockout = new Date(row.lockoutEnd).getTime();

                    if (lockout > today) {
                        // User is locked
                        return `
                            <div class="text-center">
                                <a class="btn btn-danger" onclick="LockUnLock('${data}')">
                                    Unlock
                                </a>
                            </div>
                        `;
                    } else {
                        // User is unlocked
                        return `
                            <div class="text-center">
                                <a class="btn btn-success" onclick="LockUnLock('${data}')">
                                    Lock
                                </a>
                            </div>
                        `;
                    }
                },
                "width": "25%"
            }
        ]
    });
}

function LockUnLock(id) {
    $.ajax({
        url: "/Admin/User/LockUnlock",
        type: "POST",
        data: JSON.stringify(id),
        contentType: "application/json",
        success: function (data) {
            if (data.success) {
                toastr.success(data.message);
                dataTable.ajax.reload();
            } else {
                toastr.error(data.message);
            }
        },
        error: function () {
            toastr.error("An unexpected error occurred.");
        }
    });
}
