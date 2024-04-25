CREATE TABLE Order_Item(
order_item_id SERIAL PRIMARY KEY,
price DECIMAL(10,2) NOT NULL,
quantity INT NOT NULL,
order_id SERIAL,
product_id SERIAL,
FOREIGN KEY (product_id) REFERENCES Proudect(product_id) ,
FOREIGN KEY (order_id) REFERENCES Order (order_id)
);
