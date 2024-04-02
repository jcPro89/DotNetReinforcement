create database Tarek_Test;

-- CREATE USERS TABLE

CREATE TABLE Users (
Id INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
FullName VARCHAR(70) NOT NULL,
Username VARCHAR(15) NOT NULL,
Password VARCHAR(20) NOT NULL,
IsActive BIT DEFAULT(1)
)
GO

INSERT INTO Users (FullName, Username, Password, IsActive) VALUES ('Tarek', 'Tarek', '1234',1)

-- COMPANY INFO
CREATE TABLE Companies (
Id INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
Name VARCHAR(100) NOT NULL,
Logo VARBINARY(MAX),
Phone VARCHAR(50),
Email VARCHAR(50),
Website VARCHAR(50),
LegalName VARCHAR(50),
LegalAddress VARCHAR(100),
Code VARCHAR(50),
RegistrationNo VARCHAR(50),
VAT_Number VARCHAR(50)
)
GO

INSERT INTO Companies (Name) VALUES ('Test Company');

-- ACCOUNT TYPE TABLE
CREATE TABLE AccountTypes (
Id INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
Name VARCHAR(50) NOT NULL
)
GO

INSERT INTO AccountTypes (Name) VALUES ('Asset');
INSERT INTO AccountTypes (Name) VALUES ('Liability');
INSERT INTO AccountTypes (Name) VALUES ('Income');
INSERT INTO AccountTypes (Name) VALUES ('Expense');

GO


-- ACCOUNT GROUP TABLE
CREATE TABLE AccountGroups (
Id INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
TypeId INT FOREIGN KEY REFERENCES AccountTypes (Id),
CompanyId INT FOREIGN KEY REFERENCES Companies(Id),
Code VARCHAR(50) NOT NULL,
Name VARCHAR(150) NOT NULL,
Description VARCHAR(200) NOT NULL,
AcceptsSubGroups BIT DEFAULT(1) NOT NULL,
PL_Account BIT NOT NULL,
IsActive BIT DEFAULT(1) NOT NULL
)
GO



-- ACCOUNT SUB-GROUP TABLE
CREATE TABLE AccountSubGroups (
Id INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
Code VARCHAR(50) NOT NULL,
Name VARCHAR(150) NOT NULL,
GroupId INT FOREIGN KEY REFERENCES AccountGroups(Id),
Description VARCHAR(200) NOT NULL,
IsActive BIT DEFAULT(1) NOT NULL
)
GO



--//////////////////////////////////////
-- ACCOUNTS TABLE
--//////////////////////////////////////

CREATE TABLE Accounts (
Id INT IDENTITY(1,1) PRIMARY KEY,
SubGroupId INT FOREIGN KEY REFERENCES AccountSubGroups(Id),
Name VARCHAR(50) NOT NULL,
Description VARCHAR(200) NOT NULL,
)
GO

--//////////////////////////////////////
-- CURRENCIES TABLE
--//////////////////////////////////////
CREATE TABLE Currencies (
Id INT IDENTITY(1,1) PRIMARY KEY,
Code NVARCHAR(3) NOT NULL,
Name VARCHAR(50),
IsLocal BIT DEFAULT(0)
)
GO

INSERT INTO Currencies (Code, IsLocal) VALUES ('NGC', 1);
GO

--//////////////////////////////////////
-- CURRENCY XR HISTORY TABLE
--//////////////////////////////////////

CREATE TABLE ExchangeRateHistory (
Id INT IDENTITY(1,1) PRIMARY KEY,
ForeignCurrencyId INT FOREIGN KEY REFERENCES Currencies(Id),
ExchangeRate DECIMAL NOT NULL,
DateFrom DATE NOT NULL,
DateTo DATE NOT NULL
)
GO


CREATE TABLE ClientGroups (
Id INT IDENTITY(1,1) PRIMARY KEY,
CompanyId INT FOREIGN KEY REFERENCES Companies(Id),
Name VARCHAR(100) NOT NULL,
Description VARCHAR(300) NOT NULL
)
GO

--//////////////////////////////////////
-- SALES REP TABLE
--//////////////////////////////////////

--CREATE TABLE SalesReps (
--Id INT IDENTITY(1,1) PRIMARY KEY,
--Name VARCHAR(50) NOT NULL
--)
--GO


--//////////////////////////////////////
-- TERMS OF PAYMENT TABLE
--//////////////////////////////////////

--CREATE TABLE TermsOfPayment (
--Id INT IDENTITY(1,1) PRIMARY KEY,
--Name VARCHAR(50) NOT NULL,
--Description VARCHAR(200),
--) 
--GO



--//////////////////////////////////////
-- CLIENTS 
--//////////////////////////////////////

CREATE TABLE Clients (
Id INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
ClientGroupId INT FOREIGN KEY REFERENCES ClientGroups(Id),
CurrencyId INT FOREIGN KEY REFERENCES Currencies(Id),
--SalesRepId INT FOREIGN KEY REFERENCES SalesReps(Id),
SalesRep VARCHAR(100),
--TermsOfPaymentId INT FOREIGN KEY REFERENCES TermsOfPayment(Id), 
TermsOfPayment VARCHAR(100),
Name VARCHAR(100) NOT NULL,
Country VARCHAR(50),
City VARCHAR(50) ,
Address VARCHAR(100),
CompanyName VARCHAR(100) ,
Phone VARCHAR(20),
AlternativePhone VARCHAR(20),
Email VARCHAR(50),
CC VARCHAR(100),
CreditLimit FLOAT,
CreditDays INT ,
SourceOfClient VARCHAR(150),
ClientSince DATE,
Notes VARCHAR(300),
Status BIT
)
GO

CREATE TABLE Projects (
Id INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
ClientId INT FOREIGN KEY REFERENCES Clients(Id),
Name VARCHAR(100) NOT NULL,
Description VARCHAR(600)
)
GO


CREATE TABLE InvoiceStatus (
Id INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
Status VARCHAR(20) NOT NULL
)

INSERT INTO InvoiceStatus (Status) VALUES ('Open');
INSERT INTO InvoiceStatus (Status) VALUES ('Pending');
INSERT INTO InvoiceStatus (Status) VALUES ('Cancelled');



CREATE TABLE ProductsAndServices (
Id INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
CompanyId INT FOREIGN KEY REFERENCES Companies(Id),
Name VARCHAR(100) NOT NULL,
Description VARCHAR(300),
ExpenseCost MONEY,
SuggestedPrice MONEY NOT NULL -- Suggested price to client
)
GO



CREATE TABLE SalesInvoices (
Id INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
StatusId INT FOREIGN KEY REFERENCES InvoiceStatus(Id),
ProjectId INT FOREIGN KEY REFERENCES Projects (Id),
CurrencyId INT FOREIGN KEY REFERENCES Currencies (Id),
CustRef VARCHAR(200),
InvoiceNo VARCHAR(10) NOT NULL,
InvoiceDate DATE NOT NULL,
ShipType VARCHAR(100),
BL_Reference VARCHAR(50),
Remarks VARCHAR(300),
Notes VARCHAR(200),
IsConfirmed BIT
)
GO




CREATE TABLE SaleInvoiceLines (
Id INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
InvoiceId INT FOREIGN KEY REFERENCES SalesInvoices(Id),
ExpenseId INT FOREIGN KEY REFERENCES ProductsAndServices(Id), 
CurrencyId INT FOREIGN KEY REFERENCES Currencies(Id),

FC_Amount MONEY,
ClientCharge MONEY
)
GO


CREATE TABLE QuotationStatus (
Id INT PRIMARY KEY IDENTITY(1,1),
Status VARCHAR(20) 
)
GO

INSERT INTO QuotationStatus (Status) VALUES ('Draft');
INSERT INTO QuotationStatus (Status) VALUES ('Sent');
INSERT INTO QuotationStatus (Status) VALUES ('Rejected');
INSERT INTO QuotationStatus (Status) VALUES ('Approved');
GO


CREATE TABLE Quotations (
Id INT IDENTITY(1,1) PRIMARY KEY,
StatusId INT FOREIGN KEY REFERENCES QuotationStatus (Id),
ProjectId INT FOREIGN KEY REFERENCES Projects (Id),
CurrencyId INT FOREIGN KEY REFERENCES Currencies (Id),
CustRef VARCHAR(200),
QuotationNo1 VARCHAR(10) NOT NULL,
QuotationNo2 INT NOT NULL,
QuotationDate DATE NOT NULL,
ShipType VARCHAR(100),
BL_Reference VARCHAR(50),
Remarks VARCHAR(300),
Attention VARCHAR(200),
IsTaxable BIT,
ValidUntil DATE
)
GO


CREATE TABLE QuotationLines (
Id INT IDENTITY(1,1) PRIMARY KEY,
QuotationId INT FOREIGN KEY REFERENCES Quotations(Id),
IsExpense BIT,
IsProductService BIT,
ItemId INT,
Incharge INT FOREIGN KEY REFERENCES Users(Id),
Date DATE,
CurrencyId INT FOREIGN KEY REFERENCES Currencies(Id),
FC_Amount MONEY,
LC_Amount MONEY,
ClientCharge MONEY
)



--//////////////////////////////////////
-- EVENTS TABLE
--//////////////////////////////////////

CREATE TABLE Events (
Id INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
UserId INT FOREIGN KEY REFERENCES Users (Id),
CompanyId INT FOREIGN KEY REFERENCES Companies (Id),
DateAndTime DATETIME,
Description VARCHAR(300) NOT NULL
)
GO


