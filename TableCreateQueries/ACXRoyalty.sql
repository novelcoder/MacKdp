drop table if exists ACXRoyalty;

CREATE TABLE ACXRoyalty
(
    ACXRoyaltyID INTEGER PRIMARY KEY,
    ACXProductID INT NOT NULL, 
    ACXRoyaltyTypeID INT NOT NULL, 
    Quantity real NOT NULL, 
    NetSales real NOT NULL, 
    RoyaltyEarned real NOT NULL,
	ACXMarket NCHAR(2) NOT NULL, 
	EntryDate DateTime NOT NULL,
    foreign key (ACXProductID) references ACXProduct(ACXProductID)
)
