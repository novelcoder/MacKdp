drop table if exists ACXRoyalty;

CREATE TABLE ACXRoyalty
(
    ACXProductID INT NOT NULL, 
    ACXRoyaltyTypeID INT NOT NULL, 
    Quantity real NOT NULL, 
    NetSales real NOT NULL, 
    RoyaltyEarned real NOT NULL,
	ACXMarket NCHAR(2) NOT NULL, 
	EntryTime INT NOT NULL,
    foreign key (ACXProductID) references ACXProducts
)
