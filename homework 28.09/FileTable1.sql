CREATE TABLE Librarian (
    id SERIAL PRIMARY KEY,
    login VARCHAR(50) NOT NULL UNIQUE,
    password VARCHAR(100) NOT NULL,
    email VARCHAR(100) NOT NULL UNIQUE
);

CREATE TABLE DocumentType (
    id SERIAL PRIMARY KEY,
    name VARCHAR(50) NOT NULL
);

CREATE TABLE Reader (
    id SERIAL PRIMARY KEY,
    login VARCHAR(50) NOT NULL UNIQUE,
    password VARCHAR(100) NOT NULL,
    email VARCHAR(100) NOT NULL UNIQUE,
    first_name VARCHAR(50) NOT NULL,
    last_name VARCHAR(50) NOT NULL,
    document_type_id INT REFERENCES DocumentType(id),
    document_number VARCHAR(50) NOT NULL
);

CREATE TABLE CodeType (
    id SERIAL PRIMARY KEY,
    name VARCHAR(50) NOT NULL
);

CREATE TABLE Book (
    id SERIAL PRIMARY KEY,
    title VARCHAR(255) NOT NULL,
    code VARCHAR(50),
    code_type_id INT REFERENCES CodeType(id),
    publication_year INT,
    country VARCHAR(50),
    city VARCHAR(50)
);

CREATE TABLE Author (
    id SERIAL PRIMARY KEY,
    first_name VARCHAR(50) NOT NULL,
    last_name VARCHAR(50) NOT NULL,
    middle_name VARCHAR(50),
    birth_date DATE
);

CREATE TABLE BookAuthor (
    book_id INT REFERENCES Book(id) ON DELETE CASCADE,
    author_id INT REFERENCES Author(id) ON DELETE CASCADE,
    PRIMARY KEY (book_id, author_id)
);

INSERT INTO DocumentType (name) VALUES 
('Паспорт'),
('Водійські права'),
('ID-картка');

INSERT INTO CodeType (name) VALUES 
('ISBN'),
('ББК');

INSERT INTO Librarian (login, password, email) VALUES 
('librarian1', 'password123', 'librarian1@library.com'),
('librarian2', 'password456', 'librarian2@library.com')

INSERT INTO Reader (login, password, email, first_name, last_name, document_type_id, document_number) VALUES 
('reader1', 'readerpassword123', 'reader1@library.com', 'Іван', 'Петров', 1, 'AB123456'),
('reader2', 'readerpassword456', 'reader2@library.com', 'Ольга', 'Сидоренко', 2, 'XY987654');

INSERT INTO Author (first_name, last_name, middle_name, birth_date) VALUES 
('Тарас', 'Шевченко', 'Григорович', '1814-03-09'),
('Ліна', 'Костенко', 'Віталіївна', '1930-03-19');

INSERT INTO Book (title, code, code_type_id, publication_year, country, city) VALUES 
('Кобзар', '978-617-07-0003-3', 1, 1840, 'Україна', 'Санкт-Петербург'),
('Маруся Чурай', '978-966-03-0007-1', 1, 1979, 'Україна', 'Київ');

INSERT INTO BookAuthor (book_id, author_id) VALUES 
(1, 1), -- Тарас Шевченко - Кобзар
(2, 2); -- Ліна Костенко - Маруся Чурай
