var dataTable;
$(document).ready(function () {
   
    GetAllItems();
    SaveItem();

    $('#btnadd').click(function () {
        clearData();
        $("#btnsave").text("Save");
        $("#txttitle").text("Add Item");
        $("#loginModal").modal('show');
    });

    $("#btncancel").click(function () {
        clearData();
        $("#loginModal").modal('hide');
    });

});

function GetAllItems() {
    dataTable = $("#itemTable").DataTable({
        "ajax": {
            "url": "../Item/GetData",
            "type": "GET",
            "datatype": "json"
        },
        "columns": [
            {
                "targets":[0],
                "data": "Name"
            },
            //{ "data": "Description" },
            {
                "targets": [1],
                "data": "Price"
            },
            //{ "data": "ID" },
            {
                //"targets": [2],
                "data": "ID", "render": function (data) {

                    return '<a class="btn btn-default btn-sm btnEdit"  onclick = GetById("' + data + '")  ><i class="fa fa-pencil"></i> Edit</a> ' +
                        '<a class="btn btn-danger btn-sm"  style = "margin-left:5px" onclick = DeleteById("' + data + '")> <i class="fa fa-trash"></i> Delete</a> ';
            },
                "orderable": false,
                "searchable": true,
                "width": "150px"
            }

        ],
        "language": {

            "emptyTable": "No data found, Please click on <b>Add New</b> Button"
        }
    });
}

function SaveItem() {
    $("#btnsave").click(function () {
        //alert("SaveItem " + $("input[id=hdnId]").val());
        if (Validateform()) {

            $.ajax({
                type: "POST",
                url: "../Item/AddEditItem",
                dataType: "json",
                contentType: "application/json",
                data: JSON.stringify({
                    Name: $("#txtName").val(),
                    Desc: $("#txtDesc").val(),
                    Price: $("#txtprice").val(),
                    Id: $("input[id=hdnId]").val()
                }),
                error: function (response) {
                    alert("error" + response.responseText);
                },
                success: function (response) {
                    
                    dataTable.ajax.reload();
                    $("#loginModal").modal('hide');

                }
            });

        }

    });
}

function Validateform() {
    var isValid = true;
    if ($.trim($("#txtName").val()) == "") {
        $('#Nameerror').removeClass("error").addClass("error_show");
        isValid = false;
    } else {
        $('#Nameerror').removeClass("error_show").addClass("error");
    }

    if ($.trim($("#txtDesc").val()) == "") {
        $('#Descerror').removeClass("error").addClass("error_show");
        isValid = false;
    } else {
        $('#Descerror').removeClass("error_show").addClass("error");
    }
    if ($.trim($("#txtprice").val()) == "") {
        $('#Priceerror').removeClass("error").addClass("error_show");
        $('#PriceNoerror').removeClass("error_show").addClass("error");
        isValid = false;
    } else if (isNaN($.trim($("#txtprice").val()))) {
        $('#Priceerror').removeClass("error_show").addClass("error");
        $('#PriceNoerror').removeClass("error").addClass("error_show");
        isValid = false;
    } else {
        $('#Priceerror').removeClass("error_show").addClass("error");
        $('#PriceNoerror').removeClass("error_show").addClass("error");
    }

    return isValid
    
}

function GetById(data) {
    $("#btnsave").text("Update");
    $("#txttitle").text("Edit Item");

    $.ajax({
        type: "GET",
        url: "../Item/GetById",
        dataType: "json",
        contentType: "application/json",
        data: { Id: data },
        error: function (response) {
            alert("error" + response.responseText);
        },
        success: function (response) {
            clearData();
            $("#txtName").val(response.data.Name);
            $("#txtDesc").val(response.data.Description);
            $("#txtprice").val(response.data.Price);
            $("input[id=hdnId]").val(response.data.ID);
            $("#loginModal").modal('show');

        }
    });
    
}
function clearData() {
    $("#txtName").val("");
    $("#txtDesc").val("");
    $("#txtprice").val("");
    $("input[id=hdnId]").val("");
}


function DeleteById(data) {
        if (confirm('Are You Sure to Delete this Record ?')) {
            $.ajax({
                type: "POST",
                url: "../Item/ItemDeleteById",
                data: { Id: data },
                success: function (data) {
                    if (data.success) {
                        //alert("Record Deleted Succesfully");
                        dataTable.ajax.reload();
                       
                    }
                }

            });
        }
}