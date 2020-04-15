--------------------Init data ---------------------

IF OBJECT_ID('dbo.Offers') IS NOT NULL
BEGIN
    DROP TABLE dbo.Offers
END

IF OBJECT_ID('dbo.Customers') IS NOT NULL
BEGIN
    DROP TABLE dbo.Customers
END

create table Customers (
   Id int primary key,
   Name varchar(100)
);
 
insert into Customers values(1, 'Kevin Costner');
insert into Customers values(2, 'Akshay Kumar');
insert into Customers values(3, 'Sean Connery');
insert into Customers values(4, 'Sanjay Dutt');
insert into Customers values(5, 'Sharukh Khan');
insert into Customers values(6, 'Lilo Liloviskow');
insert into Customers values(7, 'Mailo Mailovich');
insert into Customers values(8, 'Szarko Szarkovich');
insert into Customers values(9, 'Izka Mariskovich');

create table Offers (
   Id int primary key,
   [Name] varchar(100) not null,
   CustomerId int null,
   ParentId int null,
   CONSTRAINT FK_Tables_CustomerID FOREIGN KEY (CustomerId)
      REFERENCES Customers (Id),
    CONSTRAINT FK_ParentId FOREIGN KEY (ParentId)
         REFERENCES Offers (Id)
);


insert into Offers values(1, 'Offer1', null, null);
insert into Offers values(2, 'Offer2', 1, null);
insert into Offers values(3, 'Offer3', 2, null);
insert into Offers values(4, 'Offer4', 5, null);
insert into Offers values(5, 'Offer4', 6, null);
insert into Offers values(6, 'Offer4', 7, null);
insert into Offers values(7, 'Offer4', 8, null);
insert into Offers values(8, 'Offer1.1', null, 1);
insert into Offers values(9, 'Offer2.1', null, 2);
insert into Offers values(10, 'Offer3.1', null, 3);

--------------------End init data---------------------

select 'Customers data'
select * from Customers

SELECT 'Offers data'
select * from Offers

select 'Inner join'
select C.Id as CustomerId, C.Name, o.Id as OfferId, o.Name
From Customers C
join Offers o
on (C.Id=o.CustomerID)

select 'Left Join'
Select C.ID as customerId,C.Name, o.Id, o.Name 
From Customers C
Left join Offers o
on (C.Id=o.CustomerID)

select 'Right Join'
Select o.Id as OfferId, o.Name as Offer, C.ID as customerId,C.Name as Customer
From Customers C
Right join Offers o
on (C.ID=o.CustomerID)

select 'Full Join'
Select C.ID customerId,C.Name as Customer, o.Id, o.Name as Offer
From Customers C
Full join Offers o
on (C.ID=o.CustomerID)

select 'Cross Join'
Select C.ID as customerId, C.Name as Customer, o.Id as offerId, o.Name as Offer
From Customers C
cross join Offers o

select 'Self Join'
Select o1.Id as OfferId, o1.Name as OfferName, o2.ID as ParentOfferId, o2.Name as ParentOffer
from Offers o1
join Offers o2
on (o1.ParentId=o2.Id)

select 'Grouping'
select o.Name as Offer, count(c.Id) as OfferCount from Customers c 
join Offers o on c.Id = o.CustomerId
GROUP by o.Name 