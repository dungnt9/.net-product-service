USE ProductServiceDb;

-- Categories
INSERT INTO Categories (Name, Description, IsActive, CreatedAt) VALUES
    ('Flagship Phones', 'Điện thoại cao cấp với hiệu năng mạnh mẽ nhất', 1, NOW()),
    ('Mid-Range Phones', 'Điện thoại tầm trung với giá cả hợp lý', 1, NOW()),
    ('Budget Phones', 'Điện thoại giá rẻ phù hợp cho mọi người', 1, NOW()),
    ('Accessories', 'Phụ kiện điện thoại: ốp lưng, sạc, tai nghe', 1, NOW()),
    ('Tablets', 'Máy tính bảng các hãng', 1, NOW());

-- Products (with CategoryId)
INSERT INTO Products (Name, Brand, Price, Description, Stock, IsActive, CategoryId, CreatedAt) VALUES
    ('iPhone 15 Pro Max', 'Apple', 1399.99, 'Latest iPhone with titanium design and A17 Pro chip', 25, 1, 1, NOW()),
    ('iPhone 15 Pro', 'Apple', 1199.99, 'Pro iPhone with A17 Pro chip and ProMotion display', 30, 1, 1, NOW()),
    ('iPhone 15', 'Apple', 899.99, 'Standard iPhone 15 with USB-C and Dynamic Island', 40, 1, 1, NOW()),
    ('Galaxy S24 Ultra', 'Samsung', 1299.99, 'Premium Samsung flagship with S Pen and AI features', 18, 1, 1, NOW()),
    ('Galaxy S24', 'Samsung', 799.99, 'Compact flagship with excellent AI performance', 35, 1, 1, NOW()),
    ('Pixel 8 Pro', 'Google', 999.99, 'Google flagship with pure Android and best camera AI', 15, 1, 1, NOW()),
    ('Pixel 8', 'Google', 699.99, 'Compact Pixel with Tensor G3 chip and 7 years updates', 28, 1, 2, NOW()),
    ('OnePlus 12', 'OnePlus', 799.99, 'Flagship killer with 100W fast charging', 20, 1, 1, NOW()),
    ('Xiaomi 14 Pro', 'Xiaomi', 649.99, 'High-end Xiaomi with Leica cameras co-engineered', 30, 1, 2, NOW()),
    ('Nothing Phone 2', 'Nothing', 699.99, 'Unique transparent Glyph design', 18, 1, 2, NOW()),
    ('Samsung Galaxy A54', 'Samsung', 449.99, 'Best mid-range Samsung with great camera', 50, 1, 2, NOW()),
    ('Xiaomi Redmi Note 13 Pro', 'Xiaomi', 349.99, 'Value king with 200MP camera', 60, 1, 2, NOW()),
    ('Realme 12 Pro+', 'Realme', 399.99, 'Stylish mid-range with periscope zoom', 35, 1, 2, NOW()),
    ('Samsung Galaxy A14', 'Samsung', 199.99, 'Reliable budget phone with long support', 80, 1, 3, NOW()),
    ('Xiaomi Redmi 13C', 'Xiaomi', 149.99, 'Best value budget phone', 100, 1, 3, NOW()),
    ('Apple AirPods Pro 2', 'Apple', 249.99, 'Premium wireless earbuds with ANC', 45, 1, 4, NOW()),
    ('Samsung Galaxy Buds 2 Pro', 'Samsung', 199.99, 'Best Galaxy earbuds with 360 Audio', 40, 1, 4, NOW()),
    ('Anker 65W GaN Charger', 'Anker', 59.99, 'Compact powerful charger for all devices', 100, 1, 4, NOW()),
    ('iPad Pro 12.9 M2', 'Apple', 1099.99, 'Most powerful iPad with M2 chip', 15, 1, 5, NOW()),
    ('Samsung Galaxy Tab S9 Ultra', 'Samsung', 1199.99, 'Premium Android tablet with S Pen', 12, 1, 5, NOW());