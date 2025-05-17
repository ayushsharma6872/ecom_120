var dataTable;
$(document).ready(function () {
    loadDataTable();
})
function loadDataTable() {
    dataTable = $('#tblData1').DataTable({
        "ajax": {
            "url": "/Admin/CoverType/GetAll"
        },
        "pageLength": 2,
        "lengthMenu": [2, 4, 6, 8],

        "columns": [
            { "data": "name", "width": "70%" },
            {
                "data": "id",
                "render": function (data) {
                    return `
                    <div class="text-center">
                    
                        <a href="/Admin/CoverType/upsert/${data}" class="btn btn-info">
                        <i class ="fas fa-edit"></i>
                        </a>`;

                },
            },

            {
                "data": "id",
                "render": function (data) {
                    return `
                    <div class="text-center">
                    
                        <a class="btn btn-danger" onclick=Delete('/Admin/CoverType/Delete/${data}') >
                        <i class="fas fa-trash-alt"></i>
                        </a >`;

                },
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

