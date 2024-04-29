-- Amani 
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

-- insert into data to the users table
INSERT INTO Users (username, first_name, last_name, email, password, phone_number, is_admin)
 VALUES 
 ('hawra_alramadan', 'Hawra', 'Alramadan', 'hawra@gmail.com', '1234qwer', '966588563487', TRUE),
 ('Amani_Atiah', 'Amani', 'Atiah', 'amani@gmail.com', '1234qwer', '966549563487', FALSE),
 ('Atheer_alsaedi', 'Atheer', 'Alsaedi', 'atheer@gmail.com', '1234qwer', '966549553487', FALSE),
 ('Fatimah_alramadan', 'Fatimah', 'Alramadan', 'fatimah@gmail.com', '1234qwer', '966533563487', FALSE),
 ('Reem_Ahmed', 'Reem', 'Ahmed', 'reem@gmail.com', '1234qwer', '966567563487', FALSE),
 ('Rawabi_Khaled', 'Rawabi', 'Khaled', 'rawabi@gmail.com', '1234qwer', '966579553487', FALSE),
 ('Lama_Waleed', 'Lama', 'Waleed', 'lama@gmail.com', '1234qwer', '96653563487', FALSE);


-- display all data from users table
SELECT * FROM Users;

-- Display all data from the users table provided that the user is an administrator
SELECT * FROM Users WHERE is_admin = TRUE; 

-- Update email address
UPDATE Users SET email = 'amaniatiah@gmail.com' WHERE user_id = 2; 

