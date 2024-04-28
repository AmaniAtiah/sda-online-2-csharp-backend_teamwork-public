CREATE TABLE Addresses (
    address_id SERIAL PRIMARY KEY,
    address VARCHAR(255) NOT NULL,
    city VARCHAR(100) NOT NULL,
    state VARCHAR(100),
    country VARCHAR(100) NOT NULL,
    zip_code VARCHAR(20) NOT NULL,
    user_id INT NOT NULL,
    FOREIGN KEY (user_id) REFERENCES Users(user_id)
);

-- Insert data into Addresses table

INSERT INTO Addresses (address, city, state, country, zip_code, user_id)
VALUES
    ('123 Street', 'Cairo', NULL, 'Egypt', '12345', 1),
    ('456 Street', 'Dubai', NULL, 'UAE', '67890', 2),
    ('789 Street', 'Riyadh', NULL, 'Saudi Arabia', '98765', 3),
    ('101 Street', 'Amman', NULL, 'Jordan', '10101', 6),
    ('222 Street', 'Beirut', NULL, 'Lebanon', '54321', 1),
    ('333 Street', 'Casablanca', NULL, 'Morocco', '98765', 5),
    ('444 Street', 'Tunis', NULL, 'Tunisia', '13579', 3);
