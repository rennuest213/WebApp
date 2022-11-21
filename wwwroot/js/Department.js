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
                            <button type="button" class="btn btn-primary">EDIT</button>
                            <button type="button" class="btn btn-danger">DELETE</button>`;
                }
            }
        ]
    })
})


/*function detailsDepartment(id) {
    $.ajax({
        url: `https://localhost:7140/api/Department/${id}`,
        type: "GET"
    }).done((res) => {
        let temp = "";
        temp += `
        <input type="hidden" class="form-control" id="hideId" value="0" readonly/>
        <h5>id: </h5><input type="text" class="form-control" id="depId" placeholder="${res.data.id}" value="${res.data.id}" readonly/>
        <h5>Name: </h5><input type="text" class="form-control" id="depName" placeholder="${res.data.name}" value="${res.data.name}" readonly/>
        <h5>Divisi: </h5><input type="text" class="form-control" id="depDivision" placeholder="${res.data.divisionId}" value="${res.data.divisionId}" readonly/>
        `;
        $("#detailDepartment").html(temp);
        console.log(res.data.id);
    }).fail((err) => {
        console.log(err);
    })
}*/

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