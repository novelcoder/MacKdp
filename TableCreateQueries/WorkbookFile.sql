drop table if exists WorkbookFile;

CREATE TABLE WorkbookFile
(
    WorkbookFileID INTEGER PRIMARY KEY,
    FileName       NVARCHAR (2048) NULL,
    FileDate       DATETIME       NOT NULL
);

