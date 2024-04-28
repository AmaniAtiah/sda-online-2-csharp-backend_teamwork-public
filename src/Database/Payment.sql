CREATE TABLE Payment (
    payment_id SERIAL PRIMARY KEY,
    order_id INT UNIQUE,
    amount NUMERIC(10, 2) NOT NULL,
    payment_date TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    payment_method VARCHAR(50),
    payment_status BOOLEAN,
    FOREIGN KEY (order_id) REFERENCES Orders(order_id)
);

INSERT INTO Payment (order_id, amount, payment_method, payment_status) 
VALUES (1, 150.00, 'Credit Card', true),
       (2, 575.50, 'PayPal', true),
       (3, 842.00, 'Apple Pay', true),
       (4, 75.00, 'Cash', false),
       (5, 1220.00, 'Credit Card', true),
       (6, 344.50, 'Apple Pay', true),
       (7, 103.00, 'Cash', false);