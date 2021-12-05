drop table if exists WorkbookFile;

CREATE TABLE WorkbookFile
(
    WorkbookFileID INT PRIMARY KEY,
    FileName       NVARCHAR (2048) NULL,
    FileDate       DATETIME       NOT NULL,
    EmailAddress   NVARCHAR (512) NULL
);

