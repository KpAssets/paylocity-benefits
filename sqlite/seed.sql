PRAGMA foreign_keys = ON;

CREATE TABLE IF NOT EXISTS employees (
    id INTEGER PRIMARY KEY AUTOINCREMENT,
    first_name VARCHAR(100),
    last_name VARCHAR(100),
    date_of_birth DATETIME NOT NULL DEFAULT '0000-00-00:00:00:00',
    salary DECIMAL(10, 2) DEFAULT 0.00
);

CREATE TABLE IF NOT EXISTS dependents (
    id INTEGER PRIMARY KEY AUTOINCREMENT,
    first_name VARCHAR(100),
    last_name VARCHAR(100),
    date_of_birth DATETIME NOT NULL DEFAULT '0000-00-00:00:00:00',
    relationship VARCHAR(100) CHECK(relationship IN ('none', 'spouse', 'domesticpartner', 'child')),
    employees_id INTEGER NOT NULL,
    FOREIGN KEY(employees_id) REFERENCES employees(id)
);

INSERT INTO employees (
    first_name,
    last_name,
    date_of_birth,
    salary)
VALUES
('LeBron','James','1984-12-30',75420.99),
('Ja','Morant','1999-08-10',92365.22),
('Michael','Jordan','1963-02-17',143211.12);

INSERT INTO dependents (
    first_name,
    last_name,
    date_of_birth,
    relationship,
    employees_id
)
VALUES
('Spouse','Morant','1998-03-03','spouse',2),
('Child1','Morant','2020-06-23','child',2),
('Child2','Morant','2021-05-18','child',2),
('DP','Jordan','1974-01-02','domesticpartner',3);