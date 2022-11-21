$(document).ready(function () {
    $('#table_division').DataTable({
        ajax: {
            url: 'https://localhost:7140/api/Division',
            dataSrc: 'data',
            "header:": {
                'Content-Type': 'application/x-www-form-urlencoded',                
            },    
            "type": "GET"
        },
        columns: [
            { data: 'id', },
            { data: 'name', },
            {
                data: null,
                "render": function (data, type, row, meta) {
                    return `<button type="button" class="btn btn-primary" onclick="detailsDivision(${data.id})" data-bs-toggle="modal" data-bs-target="#detailModalDivision">DETAILS</button>
                            <button type="button" class="btn btn-primary" onclick="editDivision(${data.id})" data-bs-toggle="modal" data-bs-target="#editDivision">EDIT</button>
                            <button type="button" class="btn btn-danger" onclick="deleteDivision(${data.id})">DELETE</button>`;
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

function detailsDivision(id){
    $.ajax({
        url: `https://localhost:7140/api/Division/${id}`,
        type: "GET"
    }).done((res) => {
        let temp = "";
        temp += `
        <input type="hidden" class="form-control" id="hideId" value="0" readonly/>
        <h5>id: </h5><input type="text" class="form-control" id="divId" placeholder="${res.data.id}" value="${res.data.id}" readonly/>
        <h5>Nama: </h5><input type="text" class="form-control" id="divName" placeholder="${res.data.name}" value="${res.data.name}" readonly/>
        `;
        $("#detailData").html(temp);
        console.log(res.data.id);
    }).fail((err) => {
        console.log(err);
    })
}

function deleteDivision(id) {
    var hapus = confirm("Yakin ingin menghapus?");

    if (hapus) {
        $.ajax({
            url: `https://localhost:7140/api/Division?id=${id}`,
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

function addDivision() {
    let data;
    let id = 0;
    let name = $('#addNameDivision').val()

    data = {
        "id": id,
        "name": name,
    };

    $.ajax({
        url: 'https://localhost:7140/api/Division',
        type: 'POST',
        data: JSON.stringify(data),
        dataType: 'json',
        headers: {
            'Content-Type': 'application/json'
        },
        success: function() {
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

function editDivision(id) {
    $.ajax({
        url: `https://localhost:7140/api/Division/${id}`,
        type: "GET"
    }).done((res) => {
        let temp = "";
        temp += `
            <input type="hidden" class="form-control" id="hideId" readonly placeholder="" value="0">
            <p>Id: </p><input type="text" class="form-control" id="divId" placeholder="${res.data.id}" value="${res.data.id}">
            <p>Name: </p><input type="text" class="form-control" id="divName" placeholder="${res.data.name}" value="${res.data.name}">
            <button type= "button" class= "btn-primary" id= "editButton" onclick="saveDivision(${res.data.id})">Save Changes</button>
            `;
        $("#editData").html(temp);
    }).fail((err) => {
        console.log(err);
    });
}

function saveDivision(id) {
    var Id = id;
    var Name = $('#divName').val();

    var res = { Id, Name};
    $.ajax({
        url: `https://localhost:7140/api/Division`,
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