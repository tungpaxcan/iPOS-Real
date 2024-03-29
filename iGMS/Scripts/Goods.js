﻿var pagenum = $("#pagenum option:selected").val();
var page = 1;
var seach = "";
Goods(pagenum, page, seach);

//phan trang
$('#page').on('click', 'li', function (e) {
    e.preventDefault();
    page = $(this).attr('id');
    Goods(pagenum, page, seach);
});


function Goods(pagenum, page, seach, ) {
    $.ajax({
        url: '/goods/List',
        type: 'get',
        data: { pagenum, page, seach },
        success: function (data) {
            var Stt = 1;
            $('#tbd').empty();
            $('#kt_datatable_info').empty();
            if (data.code == 200) {
                $.each(data.c, function (k, v) {
                    let table = '<tr id="' + v.id + '" role="row" class="odd">';
                    table += '<td>' + (Stt++) + '</td>'
                    table += '<td>' + v.idgood + '</td>'
                    table += '<td>' + v.name + '</td>'
                    table += '<td>' + v.price + '</td>'
                    table += '<td class="action" nowrap="nowrap">';
                    table += '<div class="dropdown dropdown-inline">';
                    table += '<a href="javascript:;" class="btn btn-sm btn-clean btn-icon" data-toggle="dropdown">';
                    table += '<i class="la la-cog"></i></a>';
                    table += '<div class="dropdown-menu dropdown-menu-sm dropdown-menu-right">';
                    table += '<ul class="nav nav-hoverable flex-column">';
                    table += ' <li class="nav-item">';
                    table += '<a class="nav-link" href="/Goods/Details/' + v.id + '">';
                    table += '<i class="nav-icon icon-xl la la-building"></i>';
                    table += '<span class="nav-text">Detail</span></a></li>';
                    table += ' <li class="nav-item">';
                    table += '<a class="nav-link" href="/Goods/Edits/' + v.id + '">';
                    table += '<i class="nav-icon la la-edit"></i>';
                    table += '<span class="nav-text">Edit </span></a></li>';
                    table += '<li class="nav-item"><a class="nav-link" onclick="printDiv(\'barcodehh' + v.id + '\')">';
                    table += '<i class="nav-icon la la-print"></i><span class="nav-text">Print</span></a></li>';
                    table += '<li class="nav-item"><a class="nav-link" name="delete">';
                    table += '<i class="nav-icon la la-trash"></i><span class="nav-text">Delete</span></a></li>';
                    table += '</ul></div></div>';
                    table += '</td>';
                    table += '</tr>';
        
                    $('#tbd').append(table);
                });

                //--------------------------------
                let kt_datatable_info = 'Showing 1 to ' + pagenum + ' of ' + data.count + ' entries'
                $('#kt_datatable_info').append(kt_datatable_info);
                //-----------------------------page---------------------------
                $('#page').empty();
                if (parseInt(page) >= 2) {
                    let pagemin = '<li id="' + 1 + '"><a class="a_1 a_2" >' + 1 + '...</a></li>';
                    $('#page').append(pagemin);
                    let pre = ' <li id="' + (parseInt(page) - 1) + '" class="paginate_button page-item previous disabled" >';
                    pre += '<a  aria-controls="kt_datatable" data-dt-idx="0" tabindex="0" class="page-link">';
                    pre += '<i class="ki ki-arrow-back"></i></a></li>';
                    $('#page').append(pre);
                }
                for (let i = parseInt(page); i <= (parseInt(page) + 4); i++) {
                    if (i == data.pages + 1) {
                        return;
                    }
                    let li = '<li id="' + i + '" class="paginate_button page-item ">';
                    li += '<a aria-controls="kt_datatable" data-dt-idx="1" tabindex="0" class="page-link">' + i + '</a></li>';
                    $('#page').append(li);
                }
                let next = '<li  id="' + (parseInt(page) + 1) + '" class="paginate_button page-item next" id="kt_datatable_next">';
                next += '<a href="#" aria-controls="kt_datatable" data-dt-idx="6" tabindex="0" class="page-link"><i class="ki ki-arrow-next"></i></a></li>';
                $('#page').append(next);

                let pagemax = '<li id="' + data.pages + '"><a class="a_1 a_2" >...' + data.pages + '</a></li>';
                $('#page').append(pagemax);

            } else (
                alert(data.msg)
            )
        }
    })
}

$('#pagenum').on('change', function () {
    var pagenum = $("#pagenum option:selected").val();
    var page = 1;
    var seach = "";
    Goods(pagenum, page, seach)
})


//------------------------tim kiem------------------

$('#seach').on('keyup', function (e) {
    page = 1;
    seach = $('#seach').val();
    Goods(pagenum, page, seach);
});



//----------------Add::Goods---------------------
function Add() {
    var id = $('#id').val().trim();
    var idgood = $('#idgood').val().trim();
    var sku = $('#sku').val().trim();
    var style = $("#style option:selected").val();
    var warehouse = $("#warehouse option:selected").val();
    var qty = $('#qty').val().trim();
    var color = $("#color option:selected").val();
    var size = $("#size option:selected").val();
    var name = $("#name").val().trim();
    var gender = $("#gender option:selected").val();
    var categoods = $("#categoods option:selected").val();
    var groupgoods = $("#groupgoods option:selected").val();
    var price = $("#price").val().trim();
    var season = $("#season option:selected").val();
    var coo = $("#coo option:selected").val();
    var material = $("#material").val().trim();
    var company = $("#company").val().trim()
    var pricenew = $("#pricenew").val().trim();
    if ($('#supplier').val().trim().length <= 0) {
        alert("Chọn Nhà Cung Cấp !!!")
        return;
    }
    var tags = JSON.parse($('#supplier').val());
    var TagArray = [];
    //Convert to array
    for (let i = 0; i < tags.length; i++) {
        TagArray.push(tags[i].value.substring(0, tags[i].value.indexOf(' :')))
    }
    var tags1 = JSON.parse($('#epc').val());
    var TagArray1 = [];
    //Convert to array
    for (let i = 0; i < tags1.length; i++) {
        TagArray1.push(tags1[i].value)
    }
    if (TagArray1.length < qty) {
        alert("Nhập Đủ Mã EPC !!!!")
        return;
    }
    if (id.length <= 0) {
        alert("Nhập barcode !!!")
        return;
    } if (style == -1) {
        alert("Chọn Phong Cách !!!")
        return;
    } if (color == -1) {
        alert("Chọn Màu Sắc !!!")
        return;
    } if (size == -1) {
        alert("Chọn Kích Thước !!!")
        return;
    }if (name.length <= 0) {
        alert("Nhập Tên Hàng Hóa")
        return;
    } if (gender == -1) {
        alert("Chọn Giới Tính !!!")
        return;
    }if (categoods == -1) {
        alert("Chọn Loại hàng !!!")
        return;
    } if (groupgoods == -1) {
        alert("Chọn Nhóm hàng!!!")
        return;
    } if (price.length <= 0) {
        alert("Chọn Giá!!!")
        return;
    } if (season == -1) {
        alert("Chọn Mùa!!!")
        return;
    } if (coo == -1) {
        alert("Chọn Nơi Sản Xuất!!!")
        return;
    } if (material <= 0) {
        alert("Nhập Thành Phần !!!")
        return;
    } if (company <= 0) {
        alert("Nhập Công Ty !!!")
        return;
    } if (pricenew <= 0) {
        alert("Nhập Giá Mới !!!")
        return;
    } if (warehouse == -1) {
        alert("Chọn Kho !!!")
        return;
    } if (qty <= 0) {
        alert("Nhập Số lượng Tồn !!!")
        return;
    }
    $.ajax({
        url: '/goods/Add',
        type: 'post',
        data: {
            id, idgood, sku, style, warehouse, qty, color, size, name, gender, categoods
            , groupgoods, price, season, coo, material, company, pricenew

        },
        success: function (data) {
            if (data.code == 200) {
                for (let i = 0; i < TagArray.length; i++) {
                    var supplier = TagArray[i]
                    $.ajax({
                        url: '/suppliergoods/Add',
                        type: 'post',
                        data: {
                            id, supplier
                        },
                        success: function (data) {
                            if (data.code == 200) {
                                if (i == TagArray.length - 1) {
                                    for (let i = 0; i < TagArray1.length; i++) {
                                        var epc = TagArray1[i]
                                        $.ajax({
                                            url: '/goods/EPC',
                                            type: 'post',
                                            data: {
                                                id, epc
                                            },
                                            success: function (data) {
                                                if (data.code == 200) {
                                                    if (i == TagArray1.length - 1) {
                                                        Swal.fire({
                                                            title: "Tạo Hàng Hóa Thành Công",
                                                            icon: "success",
                                                            buttonsStyling: false,
                                                            confirmButtonText: "Confirm me!",
                                                            customClass: {
                                                                confirmButton: "btn btn-primary"
                                                            }
                                                        });
                                                        window.location.href = "/Goods/Index";
                                                    }
                                                } else if (data.code == 300) {
                                                    alert(data.msg)
                                                }
                                                else {
                                                    alert("Tạo  Thất Bại")
                                                }
                                            },
                                        })
                                    }
                                }

                            } else if (data.code == 300) {
                                alert(data.msg)
                            }
                            else {
                                alert("Tạo  Thất Bại")
                            }
                        },
                    })
                }

            } else if (data.code == 300) {
                alert(data.msg)
            }
            else {
                alert("Tạo Hàng Hóa Thất Bại")
            }
        },
        complete: function () {
            $('.Loading').css("display", "none");//Request is complete so hide spinner
        }
    })
}

//----------------Edit::Goods---------------------
function Edit() {
    var id = $('#id').val().trim();
    var idgood = $('#idgood').val().trim();
    var sku = $('#sku').val().trim();
    var style = $("#style option:selected").val();
    var warehouse = $("#warehouse option:selected").val();
    var qty = $('#qty').val().trim();
    var color = $("#color option:selected").val();
    var size = $("#size option:selected").val();
    var name = $("#name").val().trim();
    var gender = $("#gender option:selected").val();
    var categoods = $("#categoods option:selected").val();
    var groupgoods = $("#groupgoods option:selected").val();
    var price = $("#price").val().trim();
    var season = $("#season option:selected").val();
    var coo = $("#coo option:selected").val();
    var material = $("#material").val().trim();
    var company = $("#company").val().trim()
    var pricenew = $("#pricenew").val().trim();
    if ($('#supplier').val().trim().length <= 0) {
        alert("Chọn Nhà Cung Cấp !!!")
        return;
    }
    var tags = JSON.parse($('#supplier').val());
    var TagArray = [];
    //Convert to array
    for (let i = 0; i < tags.length; i++) {
        TagArray.push(tags[i].value.substring(0, tags[i].value.indexOf(' :')))
    }
    if (style == -1) {
        alert("Chọn Phong Cách !!!")
        return;
    } if (color == -1) {
        alert("Chọn Màu Sắc !!!")
        return;
    } if (size == -1) {
        alert("Chọn Kích Thước !!!")
        return;
    } if (name.length <= 0) {
        alert("Nhập Tên Hàng Hóa")
        return;
    } if (gender == -1) {
        alert("Chọn Giới Tính !!!")
        return;
    } if (categoods == -1) {
        alert("Chọn Loại hàng !!!")
        return;
    } if (groupgoods == -1) {
        alert("Chọn Nhóm hàng!!!")
        return;
    } if (price.length <= 0) {
        alert("Chọn Giá!!!")
        return;
    } if (season == -1) {
        alert("Chọn Mùa!!!")
        return;
    } if (coo == -1) {
        alert("Chọn Nơi Sản Xuất!!!")
        return;
    } if (material <= 0) {
        alert("Nhập Thành Phần !!!")
        return;
    } if (company <= 0) {
        alert("Nhập Công Ty !!!")
        return;
    } if (pricenew <= 0) {
        alert("Nhập Giá Mới !!!")
        return;
    } if (warehouse == -1) {
        alert("Chọn Kho !!!")
        return;
    } if (qty.length <= 0) {
        alert("Nhập Số Lượng Tồn !!!")
        return;
    }

    $.ajax({
        url: '/goods/Edit',
        type: 'post',
        data: {
            id, idgood, sku, style, warehouse, qty, color, size, name, gender, categoods
            , groupgoods, price, season, coo, material, company, pricenew
        },
        success: function (data) {
            if (data.code == 200) {
                    $.ajax({
                        url: '/suppliergoods/Delete',
                        type: 'post',
                        data: {
                            id
                        },
                        success: function (data) {
                            if (data.code == 200) {
                                    for (let j = 0; j < TagArray.length; j++) {
                                        var supplier = TagArray[j]
                                        $.ajax({
                                            url: '/suppliergoods/Add',
                                            type: 'post',
                                            data: {
                                                id, supplier
                                            },
                                            success: function (data) {
                                                if (data.code == 200) {
                                                    if (j == TagArray.length - 1) {
                                                        Swal.fire({
                                                            title: "Sửa Hàng Hóa Thành Công",
                                                            icon: "success",
                                                            buttonsStyling: false,
                                                            confirmButtonText: "Confirm me!",
                                                            customClass: {
                                                                confirmButton: "btn btn-primary"
                                                            }
                                                        });
                                                        window.location.href = "/Goods/Index";
                                                    }
                                                } else if (data.code == 300) {
                                                    alert(data.msg)
                                                }
                                                else {
                                                    alert("Tạo  Thất Bại")
                                                }
                                            },
                                        })
                                    }
                            } else if (data.code == 300) {
                                alert(data.msg)
                            }
                            else {
                                alert("Sửa  Thất Bại")
                            }
                        },
                    })
            } else if (data.code == 300) {
                alert(data.msg)
            }
            else {
                alert("Sửa Hàng Hóa Thất Bại")
            }
        },
        complete: function () {
            $('.Loading').css("display", "none");//Request is complete so hide spinner
        }
    })
}


//----------------Delete::Goods---------------------
$(document).on('click', "a[name='delete']", function () {
    var id = $(this).closest('tr').attr('id');
    if (confirm("Bạn Muốn Xóa Dữ Liệu Này ???")) {
        $.ajax({
            url: '/suppliergoods/Delete',
            type: 'post',
            data: {
                id
            },
            success: function (data) {
                if (data.code == 200) {
                    $.ajax({
                        url: '/goods/Delete',
                        type: 'post',
                        data: {
                            id
                        },
                        success: function (data) {
                            if (data.code == 200) {
                                Swal.fire({
                                    title: "Xóa Hàng Hóa Thành Công",
                                    icon: "success",
                                    buttonsStyling: false,
                                    confirmButtonText: "Confirm me!",
                                    customClass: {
                                        confirmButton: "btn btn-primary"
                                    }
                                });
                                window.location.href = "/Goods/Index";
                            }
                            else {
                                alert(data.msg)
                            }
                        }
                    })
                } else if (data.code == 300) {
                    alert(data.msg)
                }
            },
        })
     
    }
})


function chooseForSKU() {
    $('#sku').val(valueSKU())
}

function valueSKU() {
    let style = $('#style').val() == "-1" ? "" : $('#style').val()
    let color = $('#color').val() == "-1" ? "" : "-"+$('#color').val()
    let size = $('#size').val() == "-1" ? "" : "-"+$('#size').val()
    return style + color + size
}

//-------------barcode-------------
$('#id').keyup(function () {
    var id = $('#id').val().trim();
     if (id.length==8) {
        JsBarcode("#barcode", id, {
            format: "EAN8",
        });
    }else if (id.length == 12) {
        JsBarcode("#barcode", id, {
            format: "UPC",
        });
    } else if ( id.length==13) {
        JsBarcode("#barcode", id, {
            format: "EAN13",
        });
    }
    $('#idgood').val(id)
})


$('#qty').keyup(function () {
    $('#slepc').empty();
    var qty = $('#qty').val().trim();
    $('#slepc').append("Cho " + qty + " Sản Phẩm")
    new Tagify(input1, { maxTags: Number(qty) })
})
//----------------------chon file-------------------------
$('input[name="profile_avatar"]').change(function () {
    if (window.FormData !== undefined) {
        let fileUpload = $('input[name="profile_avatar"]').get(0);
        let files = fileUpload.files;
        let formdata = new FormData();
        formdata.append('file', files[0]);
        $.ajax({
            type: 'post',
            url: '/Goods/UploadImage',
            contentType: false,
            processData: false,
            data: formdata,
            success: function (urlImage) {
                $('input[name="name_route_image"]').val(urlImage);
            }
        })
    }

});

function addOrderUnit() {
    let orderUnit = $('#orderUnit')[0].rows.length
    let tr = `<tr>  
                <td>${orderUnit+1}</td>
                <td> <select class="form-control select2"  name="unit" id="kt_select22_2">
                        </select></td>
                <td><input class="form-control" type="text"/></td>
                <td></td>
            </tr>`
    Unit()
    $(document).ready(function () {
        KTSelect2.init();
    });
    $('#orderUnit').append(tr)
     
}

function keyUpMoney(id) {
    var price = $(id).val();
    $(id).val(Money(price))
}