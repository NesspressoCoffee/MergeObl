create database alasdb;
use alasdb;

create table Avion(
idAvion int Primary key not null Identity(1,1),
fechaIngreso datetime not null,
horasTotales float not null,
modelo varchar(20) not null,
cantAsientos int not null,
disponible bit not null
);

create table Aeropuerto(
abreviatura varchar(5) Primary key not null,
ciudad varchar(20) not null,
pais varchar(20) not null,
continente varchar(20) not null,
disponible bit not null
);

create table HistoricoTasa(
idTasa int Primary Key not null Identity(1,1),
fecha date not null,
tasaRegional float not null,
tasaIntercontinental float not null,
codigoAirport varchar(5) not null,
constraint Fk_HTasa_Aeropuerto foreign key (codigoAirport) references Aeropuerto(abreviatura)
);

create table Asiento(
numeroAsiento int not null,
categoria varchar(20) not null check(categoria IN('Economy', 'Premium Economy', 'Business', 'First Class')),
disponible bit not null,
avionId int not null,
constraint Fk_Asiento_Avion foreign key (avionId) references Avion(idAvion),
constraint Pk_Asiento Primary Key (numeroAsiento, avionId)
);

create table Vuelo(
idVuelo int Primary key not null Identity(1,1),
numeroVuelo varchar(10) not null,
fechaSalida datetime not null,
fechaLlegada datetime not null,
horasVuelo float not null,
estado varchar(10) NOT NULL check (estado IN('Programado', 'Abordando', 'En Vuelo', 'Finalizado', 'Cancelado')),
origen varchar(5) not null,
destino varchar(5) not null,
avionId int not null,
tipo varchar(16) not null check (tipo IN('Nacional', 'Regional', 'Intercontinental')),
visa varchar(2) not null,
constraint Fk_Origen foreign key (origen) references Aeropuerto(abreviatura),
constraint Fk_Destino foreign key (destino) references Aeropuerto(abreviatura),
constraint Fk_Vuelo_Avion foreign key (avionId) references Avion(idAvion)
);

create table Empleado(
documentoEmpleado varchar(15) Primary key not null,
nombreEmpleado varchar(20) not null,
apellidoEmpleado varchar(20) not null,
contrasenia varchar(20) not null,
disponible bit not null,
);

create table AccionesEmpleados(
id int Primary key not null Identity(1,1),
tipoModificacion varchar(12) not null check (tipoModificacion IN('Alta', 'Baja', 'Modificacion')),
fecha datetime not null,       
avionId int,
vueloId int,
abreviaturaAeropuerto  varchar(5),
docEmpleado varchar(15) not null,
constraint Fk_Acciones_Avion foreign key (avionId) references Avion(idAvion),
constraint Fk_Acciones_Vuelo foreign key (vueloId) references Vuelo(idVuelo),
constraint Fk_Acciones_Aeropuerto foreign key (abreviaturaAeropuerto) references Aeropuerto(abreviatura),
constraint Fk_Acciones_Empleado foreign key (docEmpleado) references Empleado(documentoEmpleado),
);
 
create table Compra(
idCompra int not null Primary Key Identity(1,1),
fechaCompra DateTime not null,
nombreTitular varchar(20) not null,
apellidoTitular varchar(20) not null,
documentoTitular varchar(15) not null,
tipoTarjeta varchar(10) not null,
companiaTarjeta varchar(10) not null,
numeroTarjeta int not null,
vencimientoTarjeta datetime,
email varchar(50) not null,
);

create table Pasaje(
idPasaje int Primary key not null Identity(1,1),
nombrePasajero varchar(20) not null,
apellidoPasajero varchar(20) not null,
documentoPasajero varchar(15) not null,
vueloId int not null,
compraId int not null,
asientoNumero int not null,
avionId int not null,
constraint Fk_Pasaje_Asiento foreign key (asientoNumero, avionId) references Asiento(numeroAsiento, avionId),
constraint Fk_Pasaje_Vuelo foreign key (vueloId) references Vuelo(idVuelo),
constraint Fk_Pasaje_Compra foreign key (compraId) references Compra(idCompra)
);

create table PrecioCategoria(
precioEconomy float not null,
precioPremium float not null,
precioBusiness float not null,
precioFirstClass float not null,
avionId int not null,
vueloId int not null,
foreign key (avionId) references Avion(idAvion),
foreign key (vueloId) references Vuelo(idVuelo),
constraint pkPreciosCat Primary Key (avionId, vueloId)
);


/*create table CompraPasaje(
IDPasaje int not null,
IDCompra int not null,
foreign key (IDPasaje) references Pasaje(idPasaje),
foreign key (IDCompra) references Compra(idCompra),
constraint pkCompraPasaje Primary Key (IDPasaje, IDCompra)
);


create table VideoAvion(
urlVideo varchar(200),
video blob,
modeloAvion varchar(20) not null,
foreign key (modeloAvion) references Avion(modelo)
);*/