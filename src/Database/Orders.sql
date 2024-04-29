-- Amani
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

-- insert into data to the orders table
INSERT INTO Orders (user_id, address_id, total_price)
 VALUES 
 (2, 1, 300),
 (3, 2, 400),
 (4, 3, 500),
 (2, 1, 600),
 (3, 2, 1000),
 (4, 3, 1200);


-- display all data from users table
 SELECT * FROM Orders;


--  select data from users, order  and address tables 
--  Joining users and orders tables on user_id
--  Joining orders and addresses tables on address_id
 SELECT u.user_id, u.first_name, o.user_id, o.total_price, o.address_id, a.address_id, a.country, a.city
 FROM Users u 
 INNER JOIN Orders o ON u.user_id = o.user_id
 INNER JOIN Addresses a ON o.address_id = a.address_id;


-- update total 
UPDATE  Orders SET total_price = 1000 WHERE order_id = 1;

