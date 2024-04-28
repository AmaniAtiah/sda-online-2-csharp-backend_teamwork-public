CREATE TABLE Categories (
    category_id SERIAL PRIMARY KEY,
    category_name VARCHAR(100) UNIQUE NOT NULL,
    description TEXT
);

INSERT INTO Categories (category_name, description) VALUES ('Dresses', 'Various types of dresses.');
INNER INTO Categories (category_name, description) VALUES ('Tops', 'Shirts, Bodysuits, T-Shirts, Blouses, Sweaters');
INSERT INTO Categories (category_name, description) VALUES ('Bottoms', 'Pants, Jeans, Teousers, Skirts, Shorts');
INSERT INTO Categories (category_name, description) VALUES ('Jackets', 'Various types of jackets.');
INSERT INTO Categories (category_name, description) VALUES ('Bags', 'Crossbody, Shoulder Bag, Handbags, Backpacks, Tote Bags');
INSERT INTO Categories (category_name, description) VALUES ('Shoes', 'Flat Shoes, High-Heels, Sandals, Boots, Sneakers');
INSERT INTO Categories (category_name, description) VALUES ('Accessories', 'Scarves, hats, gloves, Belts, Jewellery');
