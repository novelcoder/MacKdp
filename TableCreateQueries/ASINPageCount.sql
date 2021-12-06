drop table if exists ASINPageCount;

CREATE TABLE ASINPageCount (
    ASINPageCountID INTEGER PRIMARY KEY,
    ASIN            NCHAR (10) NULL,
    KUPageCount     INT            NOT NULL,
    KU2PageCount    INT            NOT NULL,
    AmazonPageCount INT            NOT NULL
);

