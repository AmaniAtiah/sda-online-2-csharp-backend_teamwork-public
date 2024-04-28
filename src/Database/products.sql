
CREATE TABLE Products(
product_id SERIAL PRIMARY KEY,
name VARCHAR(150) NOT NULL,
description TEXT,
price DECIMAL(10,2) NOT NULL,
color VARCHAR(50),
size VARCHAR(50),
brand VARCHAR(150),
quantity INT NOT NULL,
category_id INT,
FOREIGN KEY (category_id) REFERENCES Categories (category_id) 
);

INSERT INTO Products(name, descriotion, price, color, size, brand, quantity, category_id) 
VALUES 
('KAREN MILLEN DRESS', 'Main: 62% Polyester, 34% Viscose/Rayon, 4% Elastane/Spandex. Lining: 100% Polyester. Dry Clean Only. Model wears UK 8/US 4. Model height: 5"9. Length measurement: 113cm.', '1,303 SAR', 'Black', 'KAREN MILLEN', 100, 1),
('BOOHOO BASIC OVERSIZED T-SHIRT', '100% COTTON. MACHINE WASHABLE. MODEL WEARS SIZE M.', '59 SAR', 'White', 'BOOHOO', 80, 2),
('TOUCHÉ A LINE CREPE SKIRT', '%95 Polyester. MACHINE WASHABLE. MODEL WEARS SIZE S', '157 SAR', 'Black', 'TOUCHÉ', 65, 3),
('KAREN MILLEN JACKET', 'Shell: 100% Polyester. Lining: 100% Polyester. Wash inside out. Gentle machine wash at 30 with similar colours. Model Height 5''10 - Model wears a UK 8/US Size 6.', '589 SAR', 'White', 'KAREN MILLEN', 100, 4),
('QUIZ MINI TOTE BAG', '- Satin effect - Tote style - Diamante trim detail - Magnetic button closure - Rhinestone - Metals - Approx length:23cm - Approx depth:6.5cm - Approx height:24cm', '172 SAR', 'Blue', 'QUIZ', 73, 5),
('TAMARIS HEELED SANDALS', 'Fabric: Uppers: 100% other materials ,Lining: 100% textile, Cushioning: 100% other materials, Sole: 100% other materials. Heel height: 7cm/3".', '270 SAR', 'Grey', 'TAMARIS', 23, 6),
('BOOHOO NECKLACE AND EARRING SET ', 'Material: 40% ZINC, 30% IRON, 20% GLASS + 10% CCB', '53 SAR', 'SILVER', 'BOOHOO', 5, 7);