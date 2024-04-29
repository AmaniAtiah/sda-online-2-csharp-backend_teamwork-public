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

-- Addresses Table
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

-- Categories Table
CREATE TABLE Categories (
    category_id SERIAL PRIMARY KEY,
    category_name VARCHAR(100) UNIQUE NOT NULL,
    description TEXT
);

-- Products Table
CREATE TABLE Products(
product_id SERIAL PRIMARY KEY,
name VARCHAR(150) NOT NULL,
description TEXT,
price DECIMAL(10,2) NOT NULL,
color VARCHAR(50),
size VARCHAR(50),
brand VARCHAR(150),
quantity INT NOT NULL,
category_id INT,
FOREIGN KEY (category_id) REFERENCES Categories (category_id) 
);

-- Orders Table
CREATE TABLE Orders (
    order_id SERIAL PRIMARY KEY,
    user_id INT NOT NULL,
    address_id INT NOT NULL,
    total_price INT NOT NULL,
    created_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    updated_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    FOREIGN KEY (user_id) REFERENCES Users(user_id),
    FOREIGN KEY (address_id) REFERENCES Addresses(address_id)
);

-- Order Item Table
CREATE TABLE Order_Item(
order_item_id SERIAL PRIMARY KEY,
price DECIMAL(10,2) NOT NULL,
quantity INT NOT NULL,
order_id INT,
product_id INT,
FOREIGN KEY (product_id) REFERENCES Products(product_id),
FOREIGN KEY (order_id) REFERENCES Orders (order_id)
);

-- Wishlist Table
CREATE TABLE Wishlist (
    wishlist_id SERIAL PRIMARY KEY,
    product_id INT NOT NULL,
    user_id INT NOT NULL,
    FOREIGN KEY (user_id) REFERENCES Users (user_id),
    FOREIGN KEY (product_id) REFERENCES Products (product_id)
);

-- Cart Table
CREATE TABLE Cart (
    cart_id SERIAL PRIMARY KEY,
    user_id INT NOT NULL,
    product_id INT NOT NULL,
    quantity INT NOT NULL,
    FOREIGN KEY (user_id) REFERENCES Users (user_id),
    FOREIGN KEY (product_id) REFERENCES Products (product_id)
);

-- Payment Table
CREATE TABLE Payment (
    payment_id SERIAL PRIMARY KEY,
    order_id INT UNIQUE,
    amount NUMERIC(10, 2) NOT NULL,
    payment_date TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    payment_method VARCHAR(50),
    payment_status BOOLEAN,
    FOREIGN KEY (order_id) REFERENCES Orders(order_id)
);

 -- Insert data into Users table
INSERT INTO Users (username, first_name, last_name, email, password, phone_number, is_admin)
 VALUES 
    ('hawra_alramadan', 'Hawra', 'Alramadan', 'hawra@gmail.com', '1234qwer', '966588563487', TRUE),
    ('Amani_Atiah', 'Amani', 'Atiah', 'amani@gmail.com', '1234qwer', '966549563487', FALSE),
    ('Atheer_alsaedi', 'Atheer', 'Alsaedi', 'atheer@gmail.com', '1234qwer', '966549553487', FALSE),
    ('Fatimah_alramadan', 'Fatimah', 'Alramadan', 'fatimah@gmail.com', '1234qwer', '966533563487', FALSE),
    ('Reem_Ahmed', 'Reem', 'Ahmed', 'reem@gmail.com', '1234qwer', '966567563487', FALSE),
    ('Rawabi_Khaled', 'Rawabi', 'Khaled', 'rawabi@gmail.com', '1234qwer', '966579553487', FALSE),
    ('Lama_Waleed', 'Lama', 'Waleed', 'lama@gmail.com', '1234qwer', '96653563487', FALSE);

 -- Insert data into Addresses table
INSERT INTO Addresses (address, city, state, country, zip_code, user_id)
VALUES
    ('123 Street', 'Cairo', 'Cairo Governorate', 'Egypt', '12345', 1),
    ('456 Street', 'Dubai', 'Dubai', 'UAE', '67890', 2),
    ('789 Street', 'Riyadh', 'Riyadh Province', 'Saudi Arabia', '98765', 3),
    ('101 Street', 'Amman', 'Amman Governorate', 'Jordan', '10101', 6),
    ('222 Street', 'Beirut', 'Beirut Governorate', 'Lebanon', '54321', 1),
    ('333 Street', 'Casablanca', 'Grand Casablanca', 'Morocco', '98765', 5),
    ('444 Street', 'Tunis', 'Tunis', 'Tunisia', '13579', 3);

 -- Insert data into Categories table
INSERT INTO Categories (category_name, description) 
 VALUES 
    ('Dresses', 'Various types of dresses.'),
    ('Tops', 'Shirts, Bodysuits, T-Shirts, Blouses, Sweaters'),
    ('Bottoms', 'Pants, Jeans, Teousers, Skirts, Shorts'),
    ('Jackets', 'Various types of jackets.'),
    ('Bags', 'Crossbody, Shoulder Bag, Handbags, Backpacks, Tote Bags'),
    ('Shoes', 'Flat Shoes, High-Heels, Sandals, Boots, Sneakers'),
    ('Accessories', 'Scarves, hats, gloves, Belts, Jewellery');

 -- Insert data into Products table
INSERT INTO Products (name, description, price, color, size, brand, quantity, category_id) 
VALUES 
    ('KAREN MILLEN DRESS', 'Main: 62% Polyester, 34% Viscose/Rayon, 4% Elastane/Spandex. Lining: 100% Polyester. Dry Clean Only. Model wears UK 8/US 4. Model height: 5"9. Length measurement: 113cm.', 1303, 'Black', 'KAREN MILLEN', 100, 1),
    ('BOOHOO BASIC OVERSIZED T-SHIRT', '100% COTTON. MACHINE WASHABLE. MODEL WEARS SIZE M.', 59, 'White', 'BOOHOO', 80, 2),
    ('TOUCHÉ A LINE CREPE SKIRT', '%95 Polyester. MACHINE WASHABLE. MODEL WEARS SIZE S', 157, 'Black', 'TOUCHÉ', 65, 3),
    ('KAREN MILLEN JACKET', 'Shell: 100% Polyester. Lining: 100% Polyester. Wash inside out. Gentle machine wash at 30 with similar colors. Model Height 5''10 - Model wears a UK 8/US Size 6.', 589, 'White', 'KAREN MILLEN', 100, 4),
    ('QUIZ MINI TOTE BAG', '- Satin effect - Tote style - Diamante trim detail - Magnetic button closure - Rhinestone - Metals - Approx length:23cm - Approx depth:6.5cm - Approx height:24cm', 172, 'Blue', 'QUIZ', 73, 5),
    ('TAMARIS HEELED SANDALS', 'Fabric: Uppers: 100% other materials ,Lining: 100% textile, Cushioning: 100% other materials, Sole: 100% other materials. Heel height: 7cm/3".', 270, 'Grey', 'TAMARIS', 23, 6),
    ('BOOHOO NECKLACE AND EARRING SET ', 'Material: 40% ZINC, 30% IRON, 20% GLASS + 10% CCB', 53, 'SILVER', 'BOOHOO', 5, 7);

 -- Insert data into Orders table
INSERT INTO Orders (user_id, address_id, total_price)
 VALUES 
    (2, 1, 300),
    (3, 2, 400),
    (4, 3, 500),
    (2, 1, 600),
    (3, 2, 1000),
    (4, 3, 1200);

 -- Insert data into Order_Item table
INSERT INTO Order_Item (price, quantity, order_id, product_id) 
VALUES 
    (1303.00, 1, 1, 1), 
    (118.00, 2, 2, 2),
    (157.00, 1, 3, 3), 
    (589.00, 1, 4, 4),
    (516.00, 3, 5, 5), 
    (540.00, 2, 6, 6),
    (106.00, 2, 7, 7);

 -- Insert data into Wishlist table
INSERT INTO Wishlist (product_id, user_id)
VALUES
    (1, 1),
    (2, 2),
    (3, 3),
    (4, 4),
    (5, 1),
    (6, 2),
    (7, 3);

 -- Insert data into Cart table
INSERT INTO Cart (user_id, product_id, quantity)
VALUES
    (1, 1, 2),
    (2, 2, 3),
    (3, 3, 1),
    (4, 4, 2),
    (5, 5, 1),
    (7, 6, 2),
    (3, 7, 1);

 -- Insert data into Payment table
INSERT INTO Payment (order_id, amount, payment_method, payment_status) 
VALUES 
    (1, 150.00, 'Credit Card', true),
    (2, 575.50, 'PayPal', true),
    (3, 842.00, 'Apple Pay', true),
    (4, 75.00, 'Cash', false),
    (5, 1220.00, 'Credit Card', true),
    (6, 344.50, 'Apple Pay', true),
    (7, 103.00, 'Cash', false);
