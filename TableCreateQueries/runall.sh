#!/bin/bash
echo ACXProduct
sqlite3 KdpSheet.db < /users/JamesGreenwood/Projects/MacKdp/TableCreateQueries/ACXProduct.sql
echo ACXRoyalty
sqlite3 KdpSheet.db < /users/JamesGreenwood/Projects/MacKdp/TableCreateQueries/ACXRoyalty.sql
echo ACXRoyaltyType
sqlite3 KdpSheet.db < /users/JamesGreenwood/Projects/MacKdp/TableCreateQueries/ACXRoyaltyType.sql
echo ASINPageCount
sqlite3 KdpSheet.db < /users/JamesGreenwood/Projects/MacKdp/TableCreateQueries/ASINPageCount.sql
echo BookEntry
sqlite3 KdpSheet.db < /users/JamesGreenwood/Projects/MacKdp/TableCreateQueries/BookEntry.sql
echo ExchangeRate
sqlite3 KdpSheet.db < /users/JamesGreenwood/Projects/MacKdp/TableCreateQueries/ExchangeRate.sql
echo RoyaltyType
sqlite3 KdpSheet.db < /users/JamesGreenwood/Projects/MacKdp/TableCreateQueries/RoyaltyType.sql
echo WorkbookFile
sqlite3 KdpSheet.db < /users/JamesGreenwood/Projects/MacKdp/TableCreateQueries/WorkbookFile.sql
echo *
echo ** ADD DATA **
echo *
echo ACXProduct Data
sqlite3 KdpSheet.db < /users/JamesGreenwood/Projects/MacKdp/TableCreateQueries/ACXProduct.data.sql
echo ACXRoyaltyType
sqlite3 KdpSheet.db < /users/JamesGreenwood/Projects/MacKdp/TableCreateQueries/ACXRoyaltyType.data.sql
echo ASINPageCount Data
sqlite3 KdpSheet.db < /users/JamesGreenwood/Projects/MacKdp/TableCreateQueries/ASINPageCount.data.sql
echo ExchangeRate Data
sqlite3 KdpSheet.db < /users/JamesGreenwood/Projects/MacKdp/TableCreateQueries/ExchangeRate.data.sql
echo RoyaltyType Data
sqlite3 KdpSheet.db < /users/JamesGreenwood/Projects/MacKdp/TableCreateQueries/RoyaltyType.data.sql