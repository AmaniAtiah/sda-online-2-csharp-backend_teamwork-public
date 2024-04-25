-- Users Table
CREATE TABLE Users (
    user_id SERIAL PRIMARY KEY,
    username VARCHAR(50) UNIQUE NOT NULL,
    firstname VARCHAR(50),
    lastname VARCHAR(50),
    email VARCHAR(50) UNIQUE NOT NULL,
    password VARCHAR(50) NOT NULL,
    phone_number VARCHAR(50) UNIQUE NOT NULL,
    is_admin BOOLEAN DEFAULT FALSE,
    created_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    updated_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP

);


