CREATE TABLE Product(
product_id SERIAL PRIMARY KEY,
name VARCHAR(150) NOT NULL,
descriotion TEXT,
price DECIMAL(10,2) NOT NULL,
color VARCHAR(50),
size VARCHAR(50),
brand VARCHAR(150),
quantity INT NOT NULL,
category_id INT,
FOREIGN KEY (category_id) REFERENCES Categories (category_id) 
);
