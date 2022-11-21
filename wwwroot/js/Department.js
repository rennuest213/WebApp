$(document).ready(function () {
    $('#table_department').DataTable({
        ajax: {
            url: 'https://localhost:7140/api/Department',
            dataSrc: 'data',
            "header:": {
                'Content-Type': 'application/x-www-form-urlencoded',
            },
            "type": "GET"
        },
        columns: [
            { data: 'id', },
            { data: 'name', },
            { data: 'divisionId',},
            {
                data: null,
                "render": function (data, type, row, meta) {
                    return `<button type="button" class="btn btn-primary" onclick="detailDepartment(${data.id})" data-bs-toggle="modal" data-bs-target="#detailModalDepartment">DETAILS</button>
                            <button type="button" class="btn btn-primary" onclick="editDepartment(${data.id})" data-bs-toggle="modal" data-bs-target="#editDepartment">EDIT</button>
                            <button type="button" class="btn btn-danger" onclick="deleteDepartment(${data.id})">DELETE</button>`;
                }
            }
        ],
        dom: 'Bfrtip',
        buttons: ['colvis',
            'excelHtml5',
            'pdfHtml5'
        ],
    })
})


function detailDepartment(id) {
    $.ajax({
        url: `https://localhost:7140/api/Department/${id}`,
        type: "GET"
    }).done((res) => {
        let temp = "";
        temp += `
             <input type="hidden" class="form-control" id="hideId" readonly placeholder"" value="0"/>
             <h5>ID: </h5><input type="text" class="form-control" id=departId" placeholder="${res.data.id}" value="${res.data.id}" readonly/>
             <h5>Nama: </h5><input type="text" class="form-control" id=departName placeholder="${res.data.name}" value="${res.data.name}"/>
             <h5>Division ID: </h5><input type="text" class="form-control" id=departName placeholder="${res.data.divisionId}" value="${res.data.divisionId}"/>`;
        $("#detailData").html(temp);
        console.log(res.data.id);
    }).fail((err) => {
        console.log(err);
    });
}

function editDepartment(id) {
    $.ajax({
        url: `https://localhost:7140/api/Department/${id}`,
        type: "GET"
    }).done((res) => {
        let temp = "";
        temp += `
            <input type="hidden" class="form-control" id="hideId" readonly placeholder="" value="0">
            <p>Id: </p><input type="text" class="form-control" id="depId" placeholder="${res.data.id}" value="${res.data.id}">
            <p>Name: </p><input type="text" class="form-control" id="depName" placeholder="${res.data.name}" value="${res.data.name}">
            <p>Division ID: </p><input type="text" class="form-control" id="depDivId" placeholder="${res.data.divisionId}" value="${res.data.divisionId}">
            <button type= "button" class= "btn-primary" id= "editButton" onclick="saveDepartment(${res.data.id})">Save Changes</button>
            `;
        $("#editData").html(temp);
    }).fail((err) => {
        console.log(err);
    });
}

function saveDepartment(id) {
    var Id = id;
    var Name = $('#depName').val();
    var divisionId = $('#depDivId').val();

    var res = { Id, Name, divisionId };
    $.ajax({
        url: `https://localhost:7140/api/Department`,
        type: "PUT",
        contentType: "application/json",
        dataType: "json",
        data: JSON.stringify(res),
        success: function () {
            Swal.fire(
                'Good job!',
                'Data Berhasil Diedit!',
                'success'
            ); setTimeout(function () {
                location.reload();
            }, 3000);
        },
        error: function () {

        }
    });
}

function deleteDepartment(id) {
    var hapus = confirm("Yakin ingin menghapus?");

    if (hapus) {
        $.ajax({
            url: `https://localhost:7140/api/Department?id=${id}`,
            type: 'DELETE',
            success: function (data) {
                Swal.fire(
                    'Good job!',
                    'Data berhasil dihapus!',
                    'success'
                ); setTimeout(function () {
                    location.reload();
                }, 3000);
            }
        });
    }
}

function addDepartment() {
    let data;
    let id = 0;
    let name = $('#addNameDepartment').val();
    let divisionId = $('#addDivisionId').val();

    data = {
        "id": id,
        "name": name,
        "divisionId": divisionId,
    };

    $.ajax({
        url: 'https://localhost:7140/api/Department',
        type: 'POST',
        data: JSON.stringify(data),
        dataType: 'json',
        headers: {
            'Content-Type': 'application/json'
        },
        success: function () {
            Swal.fire(
                'Good job!',
                'Data Berhasil Ditambahkan!',
                'success'
            ); setTimeout(function () {
                location.reload();
            }, 3000);
        }
    });
}