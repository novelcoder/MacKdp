drop table if exists ACXProduct;

CREATE TABLE ACXProduct
(
    ACXProductID INTEGER PRIMARY KEY,
    ACXProductKey NVARCHAR(50) NOT NULL unique, 
    Author NVARCHAR(64) NOT NULL, 
    Title NVARCHAR(128) NOT NULL, 
    ASIN NCHAR(10) NOT NULL
)