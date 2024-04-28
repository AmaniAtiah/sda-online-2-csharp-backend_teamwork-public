CREATE TABLE Order_Item(
order_item_id SERIAL PRIMARY KEY,
price DECIMAL(10,2) NOT NULL,
quantity INT NOT NULL,
order_id SERIAL,
product_id SERIAL,
FOREIGN KEY (product_id) REFERENCES Proudects(product_id),
FOREIGN KEY (order_id) REFERENCES Orders (order_id)
);

INSERT INTO Order_Item(price, quantity, order_id, product_id) 
VALUES 
('1,303 SAR', 1, 1, 1), 
('118 SAR', 2, 2, 2),
('157 SAR', 1, 3, 3), 
('589 SAR', 1, 4,4),
('516 SAR', 3, 5, 5), 
('540 SAR', 2, 6, 6),
('106 SAR', 2, 7, 7);