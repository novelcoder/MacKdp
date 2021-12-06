drop table if exists ExchangeRate;

CREATE TABLE ExchangeRate (
    ExchangeRateID INTEGER         PRIMARY KEY,
    RoyaltyTypeID  NVARCHAR (128)  NULL,
    StartDate      DateTime        NOT NULL,
    EndDate        DateTime        NOT NULL,
    ToUSD          REAL NOT NULL,
    FOREIGN KEY (RoyaltyTypeID) REFERENCES RoyaltyType (RoyaltyTypeID)
);