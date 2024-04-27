CREATE DATABASE atm;

CREATE TABLE atm.account (
	id INT PRIMARY KEY NOT NULL AUTO_INCREMENT,
    login VARCHAR(255) NOT NULL UNIQUE,
    pin INT NOT NULL,
    name VARCHAR(255) NOT NULL,
    balance int NOT NULL,
    role VARCHAR(255) NOT NULL,
    status VARCHAR(255) NOT NULL
);

insert into atm.account (login, pin, name, balance, role, status)
VALUES 
('devadmin', 12345, 'John Doe', 0, 'admin', 'active'),
('devcust', 12345, 'Jane Doe', 1000, 'customer', 'active')
;
