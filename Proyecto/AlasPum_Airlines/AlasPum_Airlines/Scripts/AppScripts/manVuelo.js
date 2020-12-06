function saveDia(dia) {


    console.info("olis");
    $.ajax({
        url: "https://localhost:44378/Vuelo/saveDiaInSession?dataDia=" + dia,
        type: 'POST',
        dataType: 'json',
        done: function () {

            console.log("hola");
        },
    });
}

//$('#fechaComienzo').change(function () {
//    document.getElementById("tablaFechasExclusivas").innerHTML = "";
//});

//$('#fechaLimite').change(function () {
//    document.getElementById("tablaFechasExclusivas").innerHTML = "";
//});

function saveDatos() {


    var DtoDatosVuelo = {};
    DtoDatosVuelo.numeroVuelo = document.getElementById("numeroVuelo").value;
    DtoDatosVuelo.horasVuelo = document.getElementById("horasVuelo").value;
    DtoDatosVuelo.origen = document.getElementById("origen").value;
    DtoDatosVuelo.destino = document.getElementById("destino").value;
    DtoDatosVuelo.avionId = document.getElementById("avionId").value;
    DtoDatosVuelo.visa = document.getElementById("visa").value;
    DtoDatosVuelo.horaSalida = document.getElementById("horaSalida").value;
    DtoDatosVuelo.precioEconomy = document.getElementById("precioEconomy").value;
    DtoDatosVuelo.precioBusiness = document.getElementById("precioBusiness").value;
    DtoDatosVuelo.precioFirstClass = document.getElementById("precioFirstClass").value;
    DtoDatosVuelo.precioPremium = document.getElementById("precioPremium").value;





    if (document.getElementById("fechaLimite") != null) {
        DtoDatosVuelo.fechaLimite = document.getElementById("fechaLimite").value;
        DtoDatosVuelo.fechaComienzo = document.getElementById("fechaComienzo").value;
    } if (document.getElementById("estado") != null) {
        DtoDatosVuelo.idVuelo = document.getElementById("idVuelo").value;
        DtoDatosVuelo.estado = document.getElementById("estado").value;
        DtoDatosVuelo.fechaSalida = document.getElementById("fechaSalida").value;
    }

    $.ajax({
        url: "https://localhost:44378/Vuelo/saveDatosInSession",
        type: 'POST',
        dataType: 'json',
        data: DtoDatosVuelo,
        done: function () {

            console.log("hola");
        },
    });
}
