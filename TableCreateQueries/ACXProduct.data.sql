
insert or replace into ACXProduct (ACXProductKey, Author, Title, ASIN)
    select 'BK_ACX0_053147', 'Jamie McFarlane', 'Rookie Privateer: Privateer Tales, Book 1', 'B00JT2LEOG'
    where not exists (select * from ACXProduct where ACXProductKey = 'BK_ACX0_053147');
insert or replace into ACXProduct (ACXProductKey, Author, Title, ASIN)
    select 'BK_ACX0_064216', 'Jamie McFarlane', 'Fool Me Once: Privateer Tales, Book 2', 'B00KPBLL6G'
    where not exists (select * from ACXProduct where ACXProductKey = 'BK_ACX0_064216');
insert or replace into ACXProduct (ACXProductKey, Author, Title, ASIN)
    select 'BK_ACX0_066206', 'Jamie McFarlane', 'Parley: Privateer Tales, Book 3', 'B00N041JBM'
    where not exists (select * from ACXProduct where ACXProductKey = 'BK_ACX0_066206');
insert or replace into ACXProduct (ACXProductKey, Author, Title, ASIN)
    select 'BK_ACX0_zzzzzz', 'Jamie McFarlane', 'Big Pete: Privateer Tales, Book 4', 'B00ONFN29Y'
    where not exists (select * from ACXProduct where ACXProductKey = 'BK_ACX0_zzzzzz');
