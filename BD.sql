CREATE TABLE Restaurante 
   (RestauranteID int PRIMARY KEY NOT NULL,  
    Nombre varchar(100) NOT NULL,  
    Direccion varchar(200) NOT NULL,  
    Telefono varchar(15) NOT NULL)  
GO  

CREATE TABLE Plato 
   (PlatoID int PRIMARY KEY NOT NULL, 
    RestauranteID int,
    TipoPlato varchar(100) NOT NULL,  
    Plato varchar(150) NOT NULL,  
    Descripcion varchar(15) NOT NULL,
	Calificacion decimal(10, 2),
	ratings int,
	FOREIGN KEY (RestauranteID) REFERENCES Restaurante(RestauranteID) )  
GO 