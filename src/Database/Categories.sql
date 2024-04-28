CREATE TABLE Categories (
    category_id SERIAL PRIMARY KEY,
    category_name VARCHAR(100) UNIQUE NOT NULL,
    description TEXT
);

INSERT INTO Categories (category_name, description) 
 VALUES ('Dresses', 'Various types of dresses.'),
        ('Tops', 'Shirts, Bodysuits, T-Shirts, Blouses, Sweaters'),
        ('Bottoms', 'Pants, Jeans, Teousers, Skirts, Shorts'),
        ('Jackets', 'Various types of jackets.'),
        ('Bags', 'Crossbody, Shoulder Bag, Handbags, Backpacks, Tote Bags'),
        ('Shoes', 'Flat Shoes, High-Heels, Sandals, Boots, Sneakers'),
        ('Accessories', 'Scarves, hats, gloves, Belts, Jewellery');
