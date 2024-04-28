-- order table


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


INSERT INTO orders (user_id, address_id, total_price)
 VALUES 
 (2, 1, 300),
 (3, 2, 400),
 (4, 3, 500),
 (2, 1, 600),
 (3, 2, 1000),
 (4, 3, 1200);


