CREATE TABLE Address (
    address_id SERIAL PRIMARY KEY,
    address VARCHAR(255) NOT NULL,
    state VARCHAR(100) NOT NULL,
    country VARCHAR(100) NOT NULL,
    zip_code VARCHAR(20) NOT NULL,
    user_id INT NOT NULL,
    FOREIGN KEY (user_id) REFERENCES User(user_id)
);
