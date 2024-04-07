$(document).ready(function () {
    showEmp();
    
});

$("#btn").click(function () {
    $("#mymodal").modal('show');
});

$("#closebtn").click(function () {
    $("#mymodal").modal('hide');
});

function clear()
{
    $("#Name").val('');
    $("#Dept").val('');
    $("#Salary").val('');
}

$("#savebtn").click(function () {
    
    //var obj = {
    //    name: $("#Name").val(),
    //    dept: $("#Dept").val(),
    //    salary: $("#Salary").val()
    //};

    var obj = $("#myform").serialize();
    $.ajax({
        url: '/Ajax/AddEmp',
        type: 'Post',
        dataType: 'json',
        contentType: 'Application/x-www-form-urlencoded;charset=utf-8;',
        data: obj,
        success: function () {
            alert('Emp Added Successfull');
            clear();
            $("#mymodal").modal('hide');
            showEmp();
        },
        error: function () {
            alert('something went wrong');
        }


    });


});

function showEmp()
{
    $.ajax({
        url: '/Ajax/ShowEmp',
        dataType: 'json',
        type: 'Get',
        contentType: 'Application/json;charset=utf-8;',
        success: function (result,status,xhr) {
            var obj = '';
            $.each(result, function (index,item) {
                obj += "<tr>";
                obj += "<td>" + item.id + "</td>";
                obj += "<td>" + item.name + "</td>";
                obj += "<td>" + item.dept + "</td>";
                obj += "<td>" + item.salary + "</td>";
                obj += "</tr>";
            });
            $("#tdata").html(obj);
        },

        error: function () {

        }

    });
}