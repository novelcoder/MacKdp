insert or replace into ExchangeRate (RoyaltyTypeID, StartDate, EndDate, ToUSD)
    select 'AUD',strftime('%s', '2012-01-01 00:00:00.000'),strftime('%s','2100-12-31 00:00:00.000'),0.76
    where not exists (select 1 from ExchangeRate where RoyaltyTypeID = 'AUD'
                                                   and StartDate = strftime('%s', '2012-01-01 00:00:00.000'));
insert or replace into ExchangeRate (RoyaltyTypeID, StartDate, EndDate, ToUSD)
    select 'BRL',strftime('%s', '2012-01-01 00:00:00.000'),strftime('%s','2015-12-31 00:00:00.000'),0.30
    where not exists (select 1 from ExchangeRate where RoyaltyTypeID = 'BRL'
                                                   and StartDate = strftime('%s', '2012-01-01 00:00:00.000'));
insert or replace into ExchangeRate (RoyaltyTypeID, StartDate, EndDate, ToUSD)
    select 'BRL',strftime('%s', '2016-01-01 00:00:00.000'),strftime('%s','2100-12-31 00:00:00.000'),0.29
    where not exists (select 1 from ExchangeRate where RoyaltyTypeID = 'BRL'
                                                   and StartDate = strftime('%s', '2012-01-01 00:00:00.000'));
insert or replace into ExchangeRate (RoyaltyTypeID, StartDate, EndDate, ToUSD)
    select 'CAD',strftime('%s', '2012-01-01 00:00:00.000'),strftime('%s','2015-12-31 00:00:00.000'),0.78
    where not exists (select 1 from ExchangeRate where RoyaltyTypeID = 'BRL'
                                                   and StartDate = strftime('%s', '2012-01-01 00:00:00.000'));
insert or replace into ExchangeRate (RoyaltyTypeID, StartDate, EndDate, ToUSD)
    select 'CAD',strftime('%s', '2016-01-31 00:00:00.000'),strftime('%s','2100-12-31 00:00:00.000'),0.79
    where not exists (select 1 from ExchangeRate where RoyaltyTypeID = 'CAD'
                                                   and StartDate = strftime('%s', '2012-01-01 00:00:00.000'));
insert or replace into ExchangeRate (RoyaltyTypeID, StartDate, EndDate, ToUSD)
    select 'EUR',strftime('%s', '2012-01-01 00:00:00.000'),strftime('%s','2015-12-31 00:00:00.000'),1.06
    where not exists (select 1 from ExchangeRate where RoyaltyTypeID = 'EUR'
                                                   and StartDate = strftime('%s', '2012-01-01 00:00:00.000'));
insert or replace into ExchangeRate (RoyaltyTypeID, StartDate, EndDate, ToUSD)
    select 'EUR',strftime('%s', '2016-01-01 00:00:00.000'),strftime('%s','2100-12-31 00:00:00.000'),1.14
    where not exists (select 1 from ExchangeRate where RoyaltyTypeID = 'EUR'
                                                   and StartDate = strftime('%s', '2016-01-01 00:00:00.000'));
insert or replace into ExchangeRate (RoyaltyTypeID, StartDate, EndDate, ToUSD)
    select 'GBP',strftime('%s', '2012-01-01 00:00:00.000'),strftime('%s','2100-12-31 00:00:00.000'),1.47
    where not exists (select 1 from ExchangeRate where RoyaltyTypeID = 'GBP'
                                                   and StartDate = strftime('%s', '2012-01-01 00:00:00.000'));
insert or replace into ExchangeRate (RoyaltyTypeID, StartDate, EndDate, ToUSD)
    select 'INR',strftime('%s', '2012-01-01 00:00:00.000'),strftime('%s','2015-12-31 00:00:00.000'),0.01
    where not exists (select 1 from ExchangeRate where RoyaltyTypeID = 'INR'
                                                   and StartDate = strftime('%s', '2012-01-01 00:00:00.000'));
insert or replace into ExchangeRate (RoyaltyTypeID, StartDate, EndDate, ToUSD)
    select 'INR',strftime('%s', '2016-01-01 00:00:00.000'),strftime('%s','2100-12-31 00:00:00.000'),0.01
    where not exists (select 1 from ExchangeRate where RoyaltyTypeID = 'INR'
                                                   and StartDate = strftime('%s', '2016-01-01 00:00:00.000'));
insert or replace into ExchangeRate (RoyaltyTypeID, StartDate, EndDate, ToUSD)
    select 'JPY',strftime('%s', '2012-01-01 00:00:00.000'),strftime('%s','2015-12-31 00:00:00.000'),0.00
    where not exists (select 1 from ExchangeRate where RoyaltyTypeID = 'JPY'
                                                   and StartDate = strftime('%s', '2012-01-01 00:00:00.000'));
insert or replace into ExchangeRate (RoyaltyTypeID, StartDate, EndDate, ToUSD)
    select 'JPY',strftime('%s', '2016-01-01 00:00:00.000'),strftime('%s','2100-12-31 00:00:00.000'),0.00
    where not exists (select 1 from ExchangeRate where RoyaltyTypeID = 'JPY'
                                                   and StartDate = strftime('%s', '2016-01-01 00:00:00.000'));
insert or replace into ExchangeRate (RoyaltyTypeID, StartDate, EndDate, ToUSD)
    select 'MXN',strftime('%s', '2012-01-01 00:00:00.000'),strftime('%s','2015-12-31 00:00:00.000'),0.06
    where not exists (select 1 from ExchangeRate where RoyaltyTypeID = 'MXN'
                                                   and StartDate = strftime('%s', '2012-01-01 00:00:00.000'));
insert or replace into ExchangeRate (RoyaltyTypeID, StartDate, EndDate, ToUSD)
    select 'MXN',strftime('%s', '2016-01-01 00:00:00.000'),strftime('%s','2100-12-31 00:00:00.000'),0.05
    where not exists (select 1 from ExchangeRate where RoyaltyTypeID = 'MXN'
                                                   and StartDate = strftime('%s', '2016-01-01 00:00:00.000'));
insert or replace into ExchangeRate (RoyaltyTypeID, StartDate, EndDate, ToUSD)
    select 'USD',strftime('%s', '2012-01-01 00:00:00.000'),strftime('%s','2100-12-31 00:00:00.000'),1.00
    where not exists (select 1 from ExchangeRate where RoyaltyTypeID = 'USD'
                                                   and StartDate = strftime('%s', '2012-01-01 00:00:00.000'));