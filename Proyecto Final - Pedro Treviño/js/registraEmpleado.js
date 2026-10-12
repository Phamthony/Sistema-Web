$(document).ready(function () {
    $('#selCategoria').change(function () {
        var idcategoria = $(this).val();
        var ayuda = "";

        if (idcategoria === "1") {
            ayuda = "Ingresa el salario fijo mensual (Tiempo Completo)";
        } else if (idcategoria === "2") {
            ayuda = "Ingresa el sueldo por hora (Tiempo Parcial)";
        } else if (idcategoria === "3") {
            ayuda = "Ingresa el sueldo por contrato (Contratista)";
        } else {
            ayuda = "";
        }

        $('#txtSueldo').val("");
        $('#txtSueldo').attr("placeholder", ayuda);
    });
});

$(document).ready(function () {
    SComboRoles();
    SComboCategorias();

    var tabla = $('#example').DataTable();

    // Filtrado por categoría
    $('#filtroCategoria').on('change', function () {
        var categoriaSeleccionada = $(this).val();

        // Filtra solo por la columna de categoría (índice 7 en tu tabla)
        tabla.column(7).search(categoriaSeleccionada).draw();
    });
});

function ModalRegistrar() {
    $("#RegistrarseModal").modal('show');
}

function SComboRoles() {
    var combo = '';

    $.ajax({
        type: "POST",
        url: "../Home/scomboRoles",
        data: "{}",
        dataType: "json",
        contentType: "application/json; charset=utf-8",
        success: function (result) {
            var str = result.data;

            for (var x = 0; x < str.length; x++) {

                combo += '<option value="' + str[x].id_rol + '" disable selected>' + str[x].descripcion + '</option>';
            }

            $('#selRol').html(combo);
            
        },
        error: function (xhr, status, error) {
            alert("Error al cargar los roles");
        }
    });
}

function SComboCategorias() {
    var combo = '<option value="">Seleccione una categoría</option>';
    var filtro = '<option value="">Todas</option>';

    $.ajax({
        type: "POST",
        url: "../Home/scomboCategorias",
        data: "{}",
        dataType: "json",
        contentType: "application/json; charset=utf-8",
        success: function (result) {
            var str = result.data;

            for (var x = 0; x < str.length; x++) {
                combo += '<option value="' + str[x].id_categoria + '">' + str[x].descripcion + '</option>';
                filtro += '<option value="' + str[x].descripcion + '">' + str[x].descripcion + '</option>';
            }

            $('#selCategoria').html(combo);         // Modal
            $('#filtroCategoria').html(filtro);     // Filtro
        },
        error: function () {
            alert("Error al cargar las categorías");
        }
    });
}


function registrarEmpleado() {
    var nombre = $('#txtNombre').val();
    var apellido = $('#txtApellido').val();
    var idrol = $('#selRol').val();
    var correo = $('#txtCorreo').val();
    var usuario = $('#txtUsuario').val();
    var contrasena = $('#txtContrasena').val();
    var idcategoria = $('#selCategoria').val();
    var sueldo = $('#txtSueldo').val();

    $.ajax({
        type: "POST",
        url: "../Home/AgregarEmpleadoJs",
        data: JSON.stringify({
            nombre: nombre,
            apellido: apellido,
            idrol: idrol,
            correo: correo,
            usuario: usuario,
            contrasena: contrasena,
            idcategoria: idcategoria,
            sueldo: sueldo
        }),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (result) {
            if (result == "1") {
                $("#msgCorrecto").show();
                $("#registrarForm")[0].reset();
            } else if (result == "2") {
                $("#msgError").html('Error en el insert');
                $("#DivMsgError").show();
                $("#msgCorrecto").hide();
            } else if (result == "3") {
                $("#msgError").html('Faltan Datos');
                $("#DivMsgError").show();
                $("#msgCorrecto").hide();
            }
        },
        error: function (xhr, status, error) {
            $('#msgError').html("Error al comunicarse con el controlador");
            $("#DivMsgError").show();
        }
    });

}

function descargarExcel() {
    //Creamos un Elemento Temporal en forma de enlace
    var tmpElemento = document.createElement('a');
    // obtenemos la información desde el div que lo contiene en el html
    // Obtenemos la información de la tabla
    var data_type = 'data:application/vnd.ms-excel';
    var tabla_div = document.getElementById('example');
    var tabla_html = tabla_div.outerHTML.replace(/ /g, '%20');
    // var tabla_html = tabla_div.outerHTML.replace(/<br>/g, '<br>;');

    tmpElemento.href = data_type + ', ' + tabla_html;
    //Asignamos el nombre a nuestro EXCEL
    tmpElemento.download = 'datosTabla.xls';
    // Simulamos el click al elemento creado para descargarlo
    tmpElemento.click();
}
function descargarPDF() {
    //Creamos un Elemento Temporal en forma de enlace
    var tmpElemento = document.createElement('a');
    // obtenemos la información desde el div que lo contiene en el html
    // Obtenemos la información de la tabla
    var data_type = 'data:application/vnd.ms-excel';
    var tabla_div = document.getElementById('resultadoNomina');
    var tabla_html = tabla_div.outerHTML.replace(/ /g, '%20');
    // var tabla_html = tabla_div.outerHTML.replace(/<br>/g, '<br>;');

    tmpElemento.href = data_type + ', ' + tabla_html;
    //Asignamos el nombre a nuestro EXCEL
    tmpElemento.download = 'datosTabla.pdf';
    // Simulamos el click al elemento creado para descargarlo
    tmpElemento.click();
}