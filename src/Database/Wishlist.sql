CREATE TABLE Wishlist (
    wishlist_id SERIAL PRIMARY KEY,
    product_id INT NOT NULL,
    user_id INT NOT NULL,
    FOREIGN KEY (user_id) REFERENCES "User" (user_id)
);
