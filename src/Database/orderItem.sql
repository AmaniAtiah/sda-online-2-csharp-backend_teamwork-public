CREATE TABLE Order_Item(
order_item_id SERIAL PRIMARY KEY,
price DECIMAL(10,2) NOT NULL,
quantity INT NOT NULL,
order_id INT,
product_id INT,
FOREIGN KEY (product_id) REFERENCES Products(product_id),
FOREIGN KEY (order_id) REFERENCES Orders (order_id)
);

INSERT INTO Order_Item (price, quantity, order_id, product_id) 
VALUES 
(1303.00, 1, 1, 1), 
(118.00, 2, 2, 2),
(157.00, 1, 3, 3), 
(589.00, 1, 4, 4),
(516.00, 3, 5, 5), 
(540.00, 2, 6, 6),
(106.00, 2, 7, 7);
