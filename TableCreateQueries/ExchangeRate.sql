drop table if exists ExchangeRate;

CREATE TABLE ExchangeRate (
    ExchangeRateID INT             PRIMARY KEY,
    RoyaltyTypeID  NVARCHAR (128)  NULL,
    StartDate      INT        NOT NULL,
    EndDate        INT        NOT NULL,
    ToUSD          REAL NOT NULL,
    FOREIGN KEY (RoyaltyTypeID) REFERENCES RoyaltyType (RoyaltyTypeID)
);