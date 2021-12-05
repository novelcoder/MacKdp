drop table if exists BookEntry;

CREATE TABLE BookEntry (
    BookEntryID    INT     PRIMARY KEY,
    ASIN           NCHAR (10)  NULL,
    Title          NVARCHAR (1024)  NULL,
    SoldBooks      INT             NOT NULL,
    KOLLBooks      INT             NOT NULL,
    Royalty        REAL NOT NULL,
    KOLLShare      REAL NOT NULL,
    FreeBooks      INT             NOT NULL,
    KENP           INT             NOT NULL,
    WorkbookFileID INT             NOT NULL,
    RoyaltyTypeID  NVARCHAR (128)  NULL,
    FOREIGN KEY (RoyaltyTypeID) REFERENCES RoyaltyType (RoyaltyTypeID),
    FOREIGN KEY (WorkbookFileID) REFERENCES WorkbookFile (WorkbookFileID)
);