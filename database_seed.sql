CREATE DATABASE atm;

CREATE TABLE atm.status (
	id INT PRIMARY KEY NOT NULL AUTO_INCREMENT,
    name VARCHAR(255) NOT NULL UNIQUE
);

INSERT INTO atm.status (name) VALUES ('active'), ('disabled');

CREATE TABLE atm.role (
	id INT PRIMARY KEY NOT NULL AUTO_INCREMENT,
    name VARCHAR(255) NOT NULL UNIQUE
);

INSERT INTO atm.role (name) VALUES ('admin'), ('customer');

CREATE TABLE atm.user (
    id INT PRIMARY KEY NOT NULL AUTO_INCREMENT,
    login VARCHAR(255) NOT NULL UNIQUE,
    pin INT NOT NULL,
    name VARCHAR(255) NOT NULL,
    roleId int, 
    FOREIGN KEY (roleId) REFERENCES role(id)
);

insert into atm.user (login, pin, name, statusId, roleId)
VALUES 
('devadmin', 12345, 'John Doe', (select id from atm.status where name = 'active'), (select id from atm.role where name = 'admin')),
('devcust', 12345, 'Jane Doe', (select id from atm.status where name = 'active'), (select id from atm.role where name = 'customer'))
;

CREATE TABLE atm.account (
    id INT PRIMARY KEY NOT NULL AUTO_INCREMENT,
    userId INT NOT NULL,
    FOREIGN KEY (userId) REFERENCES user(id),
    statusId int NOT NULL,
    FOREIGN KEY (statusId) REFERENCES status(id),
    balance int NOT NULL
);

insert into atm.account (userId, statusId, balance)
VALUES 
(2, (select id from atm.status where name = 'active'), 5000)
;
