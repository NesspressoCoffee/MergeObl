$('#CantAsientos').change(function () {
    document.getElementById("tablaDisponibilidad").innerHTML = "";
    document.getElementById("tablaCategoria").innerHTML = "";
    //location.reload();
});

function saveDatos() {


    var DtoDatosAvion = {};
    DtoDatosAvion.idAvion = document.getElementById("idAvion").value;
    DtoDatosAvion.fechaIngreso = document.getElementById("FchIngreso").value;
    DtoDatosAvion.horasTotales = document.getElementById("HrsTotales").value;
    DtoDatosAvion.modelo = document.getElementById("Modelo").value;
    DtoDatosAvion.cantAsientos = document.getElementById("CantAsientos").value;

    $.ajax({
        url: "https://localhost:44378/Avion/saveDatosInSession",
        type: 'POST',
        dataType: 'json',
        data: DtoDatosAvion,
        done: function () {

            console.log("hola");
        },
    }

    );
}
