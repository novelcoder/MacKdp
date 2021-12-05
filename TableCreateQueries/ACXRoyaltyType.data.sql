insert or replace into ACXRoyaltyType (ACXRoyaltyTypeID, ACXRoyaltyTypeDesc)
    select 1,'ALC'
    where not exists (select 1 from ACXRoyaltyType where ACXRoyaltyTypeID = 1);
insert or replace into ACXRoyaltyType (ACXRoyaltyTypeID, ACXRoyaltyTypeDesc)
    select 2,'AL'
    where not exists (select 1 from ACXRoyaltyType where ACXRoyaltyTypeID = 2);
insert or replace into ACXRoyaltyType (ACXRoyaltyTypeID, ACXRoyaltyTypeDesc)
    select 3,'ALOP'
    where not exists (select 1 from ACXRoyaltyType where ACXRoyaltyTypeID = 3);

