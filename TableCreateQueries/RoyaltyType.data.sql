insert or replace into RoyaltyType
    select 'AUD'
    where not exists ( select 1 from RoyaltyType where RoyaltyTypeID = 'AUD');
insert or replace into RoyaltyType
    select 'BRL'
    where not exists ( select 1 from RoyaltyType where RoyaltyTypeID = 'BRL');
insert or replace into RoyaltyType
    select 'CAD'
    where not exists ( select 1 from RoyaltyType where RoyaltyTypeID = 'CAD');
insert or replace into RoyaltyType
    select 'EUR'
    where not exists ( select 1 from RoyaltyType where RoyaltyTypeID = 'EUR');
insert or replace into RoyaltyType
    select 'GBP'
    where not exists ( select 1 from RoyaltyType where RoyaltyTypeID = 'GBP');
insert or replace into RoyaltyType
    select 'INR'
    where not exists ( select 1 from RoyaltyType where RoyaltyTypeID = 'INR');
insert or replace into RoyaltyType
    select 'JPY'
    where not exists ( select 1 from RoyaltyType where RoyaltyTypeID = 'JPY');
insert or replace into RoyaltyType
    select 'MXN'
    where not exists ( select 1 from RoyaltyType where RoyaltyTypeID = 'MXN');
insert or replace into RoyaltyType
    select 'USD'
    where not exists ( select 1 from RoyaltyType where RoyaltyTypeID = 'USD');
