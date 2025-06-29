USE ProductServiceDb;

INSERT INTO Products (Name, Brand, Price, Description, Stock, IsActive, CreatedAt) VALUES
                                                                                       ('iPhone 15 Pro Max', 'Apple', 1399.99, 'Latest iPhone with titanium design', 25, 1, NOW()),
                                                                                       ('iPhone 15 Pro', 'Apple', 1199.99, 'Pro iPhone with A17 Pro chip', 30, 1, NOW()),
                                                                                       ('iPhone 15', 'Apple', 899.99, 'Standard iPhone 15 with USB-C', 40, 1, NOW()),
                                                                                       ('Galaxy S24 Ultra', 'Samsung', 1299.99, 'Premium Samsung flagship with S Pen', 18, 1, NOW()),
                                                                                       ('Galaxy S24', 'Samsung', 799.99, 'Compact flagship with excellent performance', 35, 1, NOW()),
                                                                                       ('Pixel 8 Pro', 'Google', 999.99, 'Google flagship with pure Android', 15, 1, NOW()),
                                                                                       ('Pixel 8', 'Google', 699.99, 'Compact Pixel with Tensor G3 chip', 28, 1, NOW()),
                                                                                       ('OnePlus 12', 'OnePlus', 799.99, 'Flagship killer with fast charging', 20, 1, NOW()),
                                                                                       ('Xiaomi 14 Pro', 'Xiaomi', 649.99, 'High-end Xiaomi with Leica cameras', 30, 1, NOW()),
                                                                                       ('Nothing Phone 2', 'Nothing', 699.99, 'Unique transparent design', 18, 1, NOW());