insert or replace into ExchangeRate (RoyaltyTypeID, StartDate, EndDate, ToUSD)
    select 'AUD','2012-01-01','2100-12-31',0.76
    where not exists (select 1 from ExchangeRate where RoyaltyTypeID = 'AUD'
                                                   and StartDate = '2012-01-01');
insert or replace into ExchangeRate (RoyaltyTypeID, StartDate, EndDate, ToUSD)
    select 'BRL','2012-01-01','2015-12-31',0.30
    where not exists (select 1 from ExchangeRate where RoyaltyTypeID = 'BRL'
                                                   and StartDate = '2012-01-01');
insert or replace into ExchangeRate (RoyaltyTypeID, StartDate, EndDate, ToUSD)
    select 'BRL','2016-01-01','2100-12-31',0.29
    where not exists (select 1 from ExchangeRate where RoyaltyTypeID = 'BRL'
                                                   and StartDate = '2012-01-01');
insert or replace into ExchangeRate (RoyaltyTypeID, StartDate, EndDate, ToUSD)
    select 'CAD','2012-01-01','2015-12-31',0.78
    where not exists (select 1 from ExchangeRate where RoyaltyTypeID = 'BRL'
                                                   and StartDate = '2012-01-01');
insert or replace into ExchangeRate (RoyaltyTypeID, StartDate, EndDate, ToUSD)
    select 'CAD','2016-01-31','2100-12-31',0.79
    where not exists (select 1 from ExchangeRate where RoyaltyTypeID = 'CAD'
                                                   and StartDate = '2012-01-01');
insert or replace into ExchangeRate (RoyaltyTypeID, StartDate, EndDate, ToUSD)
    select 'EUR','2012-01-01','2015-12-31',1.06
    where not exists (select 1 from ExchangeRate where RoyaltyTypeID = 'EUR'
                                                   and StartDate = '2012-01-01');
insert or replace into ExchangeRate (RoyaltyTypeID, StartDate, EndDate, ToUSD)
    select 'EUR','2016-01-01','2100-12-31',1.14
    where not exists (select 1 from ExchangeRate where RoyaltyTypeID = 'EUR'
                                                   and StartDate = '2016-01-01');
insert or replace into ExchangeRate (RoyaltyTypeID, StartDate, EndDate, ToUSD)
    select 'GBP','2012-01-01','2100-12-31',1.47
    where not exists (select 1 from ExchangeRate where RoyaltyTypeID = 'GBP'
                                                   and StartDate = '2012-01-01');
insert or replace into ExchangeRate (RoyaltyTypeID, StartDate, EndDate, ToUSD)
    select 'INR','2012-01-01','2015-12-31',0.01
    where not exists (select 1 from ExchangeRate where RoyaltyTypeID = 'INR'
                                                   and StartDate = '2012-01-01');
insert or replace into ExchangeRate (RoyaltyTypeID, StartDate, EndDate, ToUSD)
    select 'INR','2016-01-01','2100-12-31',0.01
    where not exists (select 1 from ExchangeRate where RoyaltyTypeID = 'INR'
                                                   and StartDate = '2016-01-01');
insert or replace into ExchangeRate (RoyaltyTypeID, StartDate, EndDate, ToUSD)
    select 'JPY','2012-01-01','2015-12-31',0.00
    where not exists (select 1 from ExchangeRate where RoyaltyTypeID = 'JPY'
                                                   and StartDate = '2012-01-01');
insert or replace into ExchangeRate (RoyaltyTypeID, StartDate, EndDate, ToUSD)
    select 'JPY','2016-01-01','2100-12-31',0.00
    where not exists (select 1 from ExchangeRate where RoyaltyTypeID = 'JPY'
                                                   and StartDate = '2016-01-01');
insert or replace into ExchangeRate (RoyaltyTypeID, StartDate, EndDate, ToUSD)
    select 'MXN','2012-01-01','2015-12-31',0.06
    where not exists (select 1 from ExchangeRate where RoyaltyTypeID = 'MXN'
                                                   and StartDate = '2012-01-01');
insert or replace into ExchangeRate (RoyaltyTypeID, StartDate, EndDate, ToUSD)
    select 'MXN','2016-01-01','2100-12-31',0.05
    where not exists (select 1 from ExchangeRate where RoyaltyTypeID = 'MXN'
                                                   and StartDate = '2016-01-01');
insert or replace into ExchangeRate (RoyaltyTypeID, StartDate, EndDate, ToUSD)
    select 'USD','2012-01-01','2100-12-31',1.00
    where not exists (select 1 from ExchangeRate where RoyaltyTypeID = 'USD'
                                                   and StartDate = '2012-01-01');