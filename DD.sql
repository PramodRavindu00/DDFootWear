create database DD
use DD

create table UserTable (
username varchar(50) primary key,
password varchar(50),
usertype varchar(50)
)

insert into UserTable values('admin','1234','admin') 

create table shop (
username varchar(50) primary key,
password varchar(50),
usertype varchar(50),
shopname varchar(100),
location varchar(100)
)

create TABLE Product (
    ProductID INT IDENTITY(1,1) PRIMARY KEY,
    productName VARCHAR(100),
    price DECIMAL(10,2)
);

CREATE TABLE Stocks (
    stockID INT IDENTITY(1,1) PRIMARY KEY,
    username varchar(50) FOREIGN KEY REFERENCES shop(username),
    ProductID INT FOREIGN KEY REFERENCES Product(ProductID),
    quantity INT
);

create TABLE Orders (
    orderID INT IDENTITY(1,1) PRIMARY KEY,
    username varchar(50) FOREIGN KEY REFERENCES shop(username),
	ProductID INT, 
    quantity INT,
	status varchar(50),
    FOREIGN KEY (ProductID) REFERENCES Product(ProductID),
    orderDate DATETIME DEFAULT GETDATE(), 
);

