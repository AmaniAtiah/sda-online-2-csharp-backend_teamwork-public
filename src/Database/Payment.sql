CREATE TABLE Payment (
    payment_id SERIAL PRIMARY KEY,
    order_id INT UNIQUE,
    amount NUMERIC(10, 2) NOT NULL,
    payment_date TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    payment_method VARCHAR(50),
    payment_status BOOLEAN,
    --FOREIGN KEY (order_id) REFERENCES orders(order_id)
);