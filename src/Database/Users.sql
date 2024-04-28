-- Users Table
CREATE TABLE Users (
    user_id SERIAL PRIMARY KEY,
    username VARCHAR(50) UNIQUE NOT NULL,
    first_name VARCHAR(50),
    last_name VARCHAR(50),
    email VARCHAR(50) UNIQUE NOT NULL,
    password VARCHAR(50) NOT NULL,
    phone_number VARCHAR(50) UNIQUE NOT NULL,
    is_admin BOOLEAN DEFAULT FALSE,
    created_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    updated_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP

);


INSERT INTO Users (username, firstname, lastname, email, password, phone_number, is_admin)
 VALUES 
 ('hawra_alramadan', 'Hawra', 'Alramadan', 'hawra@gmail.com', '1234qwer', '966588563487', TRUE),
 ('Amani_Atiah', 'Amani', 'Atiah', 'amani@gmail.com', '1234qwer', '966549563487', FALSE),
 ('Atheer_alsaedi', 'Atheer', 'Alsaedi', 'atheer@gmail.com', '1234qwer', '966549553487', FALSE),
 ('Fatimah_alramadan', 'Fatimah', 'Alramadan', 'fatimah@gmail.com', '1234qwer', '966533563487', FALSE);


 