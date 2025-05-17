var dataTable;
$(document).ready(function () {
    loadDataTable();
})
function loadDataTable() {
    dataTable = $('#tbldata6').DataTable({
        "ajax": {
            "url": "/Admin/Company/GetAll"
        },
        "columns": [
            { "data": "name", "width": "10%" },
            { "data": "streetAddress", "width": "15%" },
            { "data": "city", "width": "10%" },
            { "data": "state", "width": "10%" },
            { "data": "postalCode", "width": "10%" },
            { "data": "phoneNumber", "width": "15%" },
            {
                "data": "isAuhtorizedCompany",
                "render": function (data) {
                    return data ? "Yes" : "No";
                },
                "width": "10%"
            },


            {
                "data": "id",
                "render": function (data) {
                    return `
                    <div class="text-center">
                        <a href="/Admin/Company/Upsert/${data}" class="btn btn-info text-white">
                            <i class="fas fa-edit"></i>
                        </a>
                        <a class="btn btn-danger text-white" onclick=Delete('/Admin/Company/Delete/${data}')>
                            <i class="fas fa-trash-alt"></i>
                        </a>
                    </div>
                `;
                },
                "width": "20%"
            }
        ]
    })
}

function Delete(url) {
    swal({
        title: "Want to Delete?",
        text: "Delete Information",
        icon: "warning",
        buttons: ['cancel', 'delete'],
        dangerMode: true
    }).then(function (willDelete) {
        if (willDelete) {
            $.ajax({
                url: url,
                type: "DELETE",
                success: function (data) {
                    if (data.success) {
                        toastr.success(data.message);
                        dataTable.ajax.reload();
                    } else {
                        toastr.error(data.message);
                    }
                }
            });
        }
    });
}

