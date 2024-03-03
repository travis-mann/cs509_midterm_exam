CREATE DATABASE atm;

CREATE TABLE status (
	id INT PRIMARY KEY NOT NULL AUTO_INCREMENT,
    name VARCHAR(255) NOT NULL UNIQUE
);

INSERT INTO status (name) VALUES ('active'), ('disabled');

CREATE TABLE role (
	id INT PRIMARY KEY NOT NULL AUTO_INCREMENT,
    name VARCHAR(255) NOT NULL UNIQUE
);

INSERT INTO role (name) VALUES ('admin'), ('customer');

CREATE TABLE user (
    id INT PRIMARY KEY NOT NULL AUTO_INCREMENT,
    login VARCHAR(255) NOT NULL UNIQUE,
    pin INT NOT NULL,
    name VARCHAR(255) NOT NULL,
    role_id int, 
    FOREIGN KEY (role_id) REFERENCES role(id)
);

insert into user (login, pin, name, role_id)
VALUES 
('devadmin', 12345, 'John Doe', (select id from role where name = 'admin')),
('devcust', 12345, 'Jane Doe', (select id from role where name = 'customer'))
;

CREATE TABLE account (
    id INT PRIMARY KEY NOT NULL AUTO_INCREMENT,
    user_id INT NOT NULL,
    FOREIGN KEY (user_id) REFERENCES user(id),
    status_id int NOT NULL,
    FOREIGN KEY (status_id) REFERENCES status(id),
    balance int NOT NULL
);

insert into account (user_id, status_id, balance)
VALUES 
(2, (select id from status where name = 'active'), 5000)
;
