--Create table TouristicObjective(

--TouristicObjectiveId int Primary Key Identity(1,1) Not Null ,
--TouristicObjectiveCode nvarchar(256) Not Null Unique,
--TouristicObjectiveName nvarchar(512) Not Null,
--TouristicObjectiveDetailDescription nvarchar(1056) Not Null,
--HasEntry bit Not Null,
--AttractionCategoryId int not null foreign key references AttractionCategory(AttractionCategoryId),
--OpenSeasonId int not null foreign key references DictionaryOpenSeason(OpenSeasonId),
--CityId int  not null foreign key  references DictionaryCity(CityId),
--Longitute float not null,
--Latitude float not null
--)

--Create table Ticket(

--TicketId int Primary Key Identity(1,1) Not Null,
--Price decimal Not Null,
--DictionaryTicketId int not null foreign key references DictionaryTicket(DictionaryTicketId),
--DictionaryCurrencyId int not null foreign key references DictionaryCurrency(DictionaryCurrencyId),
--ExchangeRateId int not null foreign key references ExchangeRate(ExchangeRateId),
--TouristicObjectiveId int not null foreign key references TouristicObjective(TouristicObjectiveId)
--)

--Create table Picture(

--ImageId int Primary Key identity(1,1) Not Null,
--Picture varbinary(max),
--TouristicObjectiveId int not null foreign key references TouristicObjective(TouristicObjectiveId)
--)

Create table Feedback(

FeedbackId int Primary Key Identity(1,1) Not Null,
CommentTitle nvarchar(256),
Comment nvarchar(1056) Not Null,
Rating int Not Null,
FeedbackName nvarchar(256) Not Null,
TouristicObjectiveId int not null foreign key references TouristicObjective(TouristicObjectiveId),
UserId nvarchar(450) not null foreign key references AspNetUsers(Id),
UserName nvarchar(1056)
)

--Create table ExchangeRate(
--ExchangeRateId int primary key identity not null,
--ExchangeRate float not null,
--CurrentDate DateTime not null
--)

--Create table DictionaryTicket(
--DictionaryTicketId int primary key identity(1,1) Not Null,
--TicketCategory nvarchar(256) Not Null
--)

--Create table DictionaryOpenSeason(
--OpenSeasonId int Not Null Primary Key identity(1,1),
--OpenSeasonType nvarchar(256) Not Null
--)

--Create table DictionaryCurrency(
--DictionaryCurrencyId int primary key identity(1,1) not null,
--CurrencyCode nvarchar(256) not null
--)


--Create table DictionaryCountry(
--CountryId int Primary Key identity(1,1) Not Null,
--CountryName nvarchar(100) Not NUll
--)

--Create table DictionaryCounty(
--CountyId int Primary Key identity(1,1) Not Null,
--CountyName nvarchar(100) Not NUll,
--CountryId int not null foreign key references DictionaryCountry(CountryId)
--)

--Create table DictionaryCity(
--CityId int Primary Key identity(1,1) Not Null,
--CityName nvarchar(100) Not NUll,
--CountyId int not null foreign key references DictionaryCounty(CountyId)
--)


--Create table AttractionCategory(
--AttractionCategoryId int Primary Key identity(1,1) Not Null,
--AttractionCategoryName nvarchar(512) Not Null
--)

Create table DictionaryContinent(
ContinentId int primary key identity(1,1) not null,
ContinentName nvarchar(450) not null
)












