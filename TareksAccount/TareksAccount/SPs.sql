
--//////////////////////////////////////
-- LOGIN
--//////////////////////////////////////

CREATE PROC LoginDataByUsername @Username VARCHAR(15) AS
BEGIN
	SELECT * FROM Users WHERE Username=@UserName 
END
GO



--//////////////////////////////////////
-- ACCOUNTS
--//////////////////////////////////////

CREATE PROC AllAccountTypes AS
BEGIN
	SELECT * FROM AccountTypes
END
GO


CREATE PROC AddLedgerGroup 
@Code VARCHAR(50),
@Name VARCHAR(150),
@Description VARCHAR(200),
@CompanyId INT, 
@TypeId INT, 
@AcceptsSubAccounts BIT, 
@PL_Account BIT,
@IsActive BIT
AS
BEGIN
	INSERT INTO AccountGroups (TypeId, Code, CompanyId, Name, Description, AcceptsSubGroups, PL_Account, IsActive)
		VALUES (@TypeId, @Code, @CompanyId, @Name, @Description, @AcceptsSubAccounts,@PL_Account,  @IsActive);
				
			INSERT INTO AccountSubGroups (GroupId, Code, Name, Description, IsActive)
				VALUES (@@IDENTITY, 'NO001', 'NONE', 'Do not use any subgroup', 0)
		
END
GO



CREATE PROC AllAccountGroups @CompanyId INT AS
BEGIN
	SELECT  
	AccountGroups.Id,
	AccountGroups.Code ,
	AccountGroups.Name ,
	AccountTypes.Name AS AccountType, 
	AccountGroups.AcceptsSubGroups AS SubAccounts,
	AccountGroups.IsActive
	FROM AccountGroups INNER JOIN AccountTypes 
		ON AccountGroups.TypeId=AccountTypes.Id

	WHERE AccountGroups.CompanyId=@CompanyId
	ORDER BY AccountGroups.Name 
END
GO



CREATE PROC AddLedgerSubGroup 
@Code VARCHAR(50),
@Name VARCHAR(150),
@GroupId INT,
@Description VARCHAR(200),
@IsActive BIT
AS
BEGIN
	INSERT INTO AccountSubGroups (Code, Name, Description, GroupId, IsActive)
		VALUES (@Code, @Name, @Description, @GroupId,@IsActive);
END
GO


CREATE PROC SearchSubGroupsByGroupId @GroupId INT AS
BEGIN
	IF(@GroupId =0)
	BEGIN
		SELECT 
			AccountSubGroups.Id,
			AccountSubGroups.Code,
			AccountSubGroups.Name ,
			AccountGroups.Name 'Group',
			AccountSubGroups.IsActive
		FROM AccountSubGroups INNER JOIN AccountGroups ON AccountSubGroups.GroupId=AccountGroups.Id
		
	END
	ELSE
	BEGIN
		SELECT
			AccountSubGroups.Id,
			AccountSubGroups.Code,
			AccountSubGroups.Name ,
			AccountGroups.Name 'Group',
			AccountSubGroups.IsActive
		FROM AccountSubGroups INNER JOIN AccountGroups ON AccountSubGroups.GroupId=AccountGroups.Id
		
		WHERE GroupId=@GroupId 
	END
END
GO



CREATE PROC AddAccount 
@SubGroupId INT,
@Name VARCHAR(50) ,
@Description VARCHAR(200)
AS
BEGIN
	INSERT INTO Accounts(SubGroupId, Name, Description) 
		VALUES (@SubGroupId, @Name, @Description)
END
GO


CREATE PROC AllAccounts @CompanyId INT AS
BEGIN
	SELECT 
		Accounts.Id,		
		AccountGroups.Name 'Group',
		AccountSubGroups.Name 'Sub Group',
		Accounts.Name 'Account'
	FROM Accounts INNER JOIN AccountSubGroups ON Accounts.SubGroupId=AccountSubGroups.Id 
		INNER JOIN AccountGroups ON AccountSubGroups.GroupId=AccountGroups.Id
	WHERE AccountGroups.CompanyId=@CompanyId
END
GO


CREATE PROC AllOperationalExpensesByCompanyId @CompanyId INT AS
BEGIN
	SELECT 
	Accounts.Id,
	Accounts.Name

	FROM Accounts INNER JOIN AccountSubGroups ON AccountSubGroups.Id=Accounts.SubGroupId
		INNER JOIN AccountGroups ON AccountGroups.Id=AccountSubGroups.GroupId

	WHERE CompanyId=@CompanyId AND AccountGroups.Name LIKE '%OPERATION%'
END
GO


--//////////////////////////////////////
-- SETTINGS
--//////////////////////////////////////


CREATE PROC GetCompanyDetails @CompanyId INT AS
BEGIN
	SELECT * FROM Companies
	WHERE Companies.Id=@CompanyId
END
GO

CREATE PROC AddCompany 
@Name VARCHAR(100),
@Logo VARBINARY(MAX),
@Phone VARCHAR(50), 
@Email VARCHAR(50),
@Website VARCHAR(50),
@LegalName VARCHAR(50),
@LegalAddress VARCHAR(100),
@Code VARCHAR(50),
@RegistrationNo VARCHAR(50),
@VAT_Number VARCHAR(50)
AS
BEGIN
	INSERT INTO Companies (Name, Logo, Phone, Email, Website, LegalName, LegalAddress, Code, RegistrationNo, VAT_Number)
	VALUES (@Name, @Logo, @Phone, @Email, @Website, @LegalName, @LegalAddress, @Code, @RegistrationNo, @VAT_Number)
END
GO


CREATE  PROC UpdateCompanyInfo
@CompanyId INT, 
@Name VARCHAR(100),
@Logo VARBINARY(MAX),
@Phone VARCHAR(50), 
@Email VARCHAR(50),
@Website VARCHAR(50),
@LegalName VARCHAR(50),
@LegalAddress VARCHAR(100),
@Code VARCHAR(50),
@RegistrationNo VARCHAR(50),
@VAT_Number VARCHAR(50)
AS 
BEGIN
	UPDATE Companies
	SET Name=@Name,
		Logo=@Logo,
		Phone=@Phone,
		Email=@Email,
		Website=@Website, 
		LegalName=@LegalName,
		LegalAddress=@LegalAddress,
		Code=@Code, 
		RegistrationNo=@RegistrationNo,
		VAT_Number=@VAT_Number
	WHERE Id=@CompanyId
END
GO

CREATE PROC AllCompanies AS
BEGIN
	SELECT * 
	FROM Companies
END
GO

CREATE PROC AddCurrency 
@Code NVARCHAR(3), 
@Name VARCHAR(20), 

@ExchangeRate DECIMAL, 
@DateFrom DATE,
@DateTo DATE

AS
BEGIN
	DECLARE @IsLocal BIT=0
	IF(@ExchangeRate=1)
	BEGIN
		SET @IsLocal=1
	END

	INSERT INTO Currencies(Code, Name, IsLocal) VALUES (@Code, @Name, @IsLocal);	
	DECLARE @ForeignCurrencyId INT
	SET @ForeignCurrencyId=@@IDENTITY
	INSERT INTO ExchangeRateHistory(ForeignCurrencyId, ExchangeRate, DateFrom, DateTo) 
		VALUES (@ForeignCurrencyId, @ExchangeRate, @DateFrom, @DateTo);
END
GO


CREATE PROC AddCurrencyExchangeRate
@CurrencyId INT,

@ExchangeRate DECIMAL, 
@DateFrom DATE,
@DateTo DATE
AS
BEGIN
	INSERT INTO ExchangeRateHistory (ForeignCurrencyId, ExchangeRate, DateFrom, DateTo)
		VALUES (@CurrencyId, @ExchangeRate, @DateFrom, @DateTo)			
END
GO


CREATE PROC ModifyCurrencyExchangeRate
@ExchangeRateId INT,
@ExchangeRate DECIMAL, 
@DateFrom DATE,
@DateTo DATE
AS
BEGIN
	UPDATE ExchangeRateHistory 
	SET ExchangeRate=@ExchangeRate,
	DateFrom=@DateFrom,
	DateTo=@DateTo
	WHERE ExchangeRateHistory.Id=@ExchangeRateId
END
GO

CREATE PROC AllCurrencies AS
BEGIN
	SELECT Currencies.Id,
	Currencies.Code

	FROM Currencies 
	
END
GO

CREATE  PROC AllCurrenciesCurrentExchangeRate AS
BEGIN
	SELECT * FROM (
		SELECT		
			Currencies.Id 'CurrencyId',
			Currencies.Code,
			Currencies.Name,		
			ExchangeRateHistory.Id 'ExchangeRateId',
			ExchangeRateHistory.ExchangeRate,
			ExchangeRateHistory.DateFrom,
			ExchangeRateHistory.DateTo,
			ROW_NUMBER() OVER(PARTITION BY Currencies.Id ORDER BY DateTo DESC) rn

		FROM ExchangeRateHistory INNER JOIN Currencies 
			ON ExchangeRateHistory.ForeignCurrencyId=Currencies.Id 

			WHERE ExchangeRateHistory.DateFrom <= GETDATE() 
		AND ExchangeRateHistory.DateTo>= GETDATE() 
		) a

	WHERE rn=1	
	
END
GO


CREATE PROC AllCurrenciesExchangeRateHistory AS
BEGIN
	SELECT 
		Currencies.Id,
		Currencies.Code,
		Currencies.Name,		
		ExchangeRateHistory.ExchangeRate,
		ExchangeRateHistory.DateFrom,
		ExchangeRateHistory.DateTo

	FROM ExchangeRateHistory INNER JOIN Currencies 
		ON ExchangeRateHistory.ForeignCurrencyId=Currencies.Id 

	ORDER BY DateTo DESC
END
GO


CREATE PROC LocalCurrency AS
BEGIN
	SELECT * FROM Currencies WHERE IsLocal=1
END
GO



CREATE PROC GetCurrentCurrencyExchangeRateByCurrencyId @CurrencyId INT AS
BEGIN
	SELECT * FROM ExchangeRateHistory 
	WHERE ExchangeRateHistory.DateFrom <= GETDATE() 
		AND ExchangeRateHistory.DateTo>= GETDATE() 
		AND ExchangeRateHistory.ForeignCurrencyId=@CurrencyId
END
GO



--//////////////////////////////////////
-- CLIENTS
--//////////////////////////////////////

CREATE PROC AddClientGroup @Name VARCHAR(100), @Description VARCHAR(300), @CompanyId INT AS
BEGIN
	INSERT INTO ClientGroups (Name, Description, CompanyId) VALUES (@Name, @Description, @CompanyId);
END
GO



CREATE PROC AllClientGroups @CompanyId INT AS
BEGIN
	SELECT * FROM ClientGroups 
	WHERE ClientGroups.CompanyId=@CompanyId
END
GO

--//////////////////////////////////////
-- SALES REP ADD SP
--//////////////////////////////////////

--CREATE PROC AddSalesRep @Name VARCHAR(50) AS
--BEGIN
--	INSERT INTO SalesReps (Name) VALUES (@Name);
--END
--GO

--INSERT INTO SalesReps (Name) VALUES ('SalesRep1');
--INSERT INTO SalesReps (Name) VALUES ('SalesRep2');
--INSERT INTO SalesReps (Name) VALUES ('SalesRep3');
--GO



CREATE PROC AddClient 
@ClientGroupId INT, 
@CurrencyId INT, 
@SalesRep VARCHAR(100), 
@TermsOfPayment VARCHAR(100),
@Name VARCHAR(100), 
@Country VARCHAR(50),
@City VARCHAR(50),
@Address VARCHAR(100),
@CompanyName VARCHAR(100),
@Phone VARCHAR(20),
@AlternativePhone VARCHAR(20),
@Email VARCHAR(50),
@CC VARCHAR(100),
@CreditLimit FLOAT,
@CreditDays INT,
@SourceOfClient VARCHAR(150),
@Notes VARCHAR(300),
@Status BIT,
@ClientSince DATE
AS
BEGIN
	INSERT INTO Clients(

		ClientGroupId , 
		CurrencyId , 
		SalesRep , 
		TermsOfPayment ,
		Name, 
		Country ,
		City ,
		Address ,
		CompanyName ,
		Phone ,
		AlternativePhone ,
		Email,
		CC ,
		CreditLimit ,
		CreditDays ,
		SourceOfClient ,
		Notes ,
		Status,
		ClientSince)
	VALUES (

		@ClientGroupId , 
		@CurrencyId , 
		@SalesRep , 
		@TermsOfPayment ,
		@Name , 
		@Country ,
		@City ,
		@Address ,
		@CompanyName ,
		@Phone ,
		@AlternativePhone ,
		@Email ,
		@CC ,
		@CreditLimit ,
		@CreditDays ,
		@SourceOfClient ,
		@Notes ,
		@Status ,
		@ClientSince)

END
GO


CREATE PROC AllClients @CompanyId INT AS
BEGIN
	SELECT * FROM Clients INNER JOIN ClientGroups ON ClientGroups.Id=Clients.ClientGroupId
	WHERE ClientGroups.CompanyId=@CompanyId
END
GO


CREATE PROC SearchClientByClientId @ClientId INT AS
BEGIN
	SELECT * FROM Clients WHERE Clients.Id=@ClientId
END
GO

CREATE PROC AddSalesInvoice 
@StatusId INT, 
@ProjectId INT, 
@CurrencyId INT,
@CustRef VARCHAR(200),
@InvoiceNo VARCHAR(10),
@InvoiceDate DATE,
@ShipType VARCHAR(100),
@BL_Reference VARCHAR(50),
@Remarks VARCHAR(300),
@DueDate DATE,
@IsRecurring BIT,
@IsConfirmed BIT
AS
BEGIN
	INSERT INTO SalesInvoices(
		StatusId , 
		ProjectId , 
		CurrencyId ,
		CustRef ,
		InvoiceNo ,
		InvoiceDate ,
		ShipType ,
		BL_Reference ,
		Remarks ,
		IsConfirmed )
	VALUES (
		@StatusId , 
		@ProjectId , 
		@CurrencyId ,
		@CustRef ,
		@InvoiceNo ,
		@InvoiceDate ,
		@ShipType ,
		@BL_Reference ,
		@Remarks ,
		@IsConfirmed )
END
GO


CREATE PROC AddSaleInvoiceLine @InvoiceId INT, @ExpenseId INT,  @FC_Amount FLOAT, @ClientCharge FLOAT AS
BEGIN
	INSERT INTO SaleInvoiceLines(InvoiceId, ExpenseId, FC_Amount, ClientCharge) VALUES (@InvoiceId, @ExpenseId, @FC_Amount, @ClientCharge)
END
GO



CREATE PROC AllSalesInvoices @CompanyId INT AS
BEGIN
	SELECT * FROM SalesInvoices INNER JOIN Projects ON Projects.Id=SalesInvoices.ProjectId
		INNER JOIN Clients ON Clients.Id=Projects.ClientId
		INNER JOIN ClientGroups ON ClientGroups.Id=Clients.ClientGroupId
	WHERE ClientGroups.CompanyId=@CompanyId
END
GO


CREATE PROC SearchSalesInvoiceById @SalesInvoiceId INT AS
BEGIN
	SELECT * FROM SalesInvoices INNER JOIN SaleInvoiceLines ON SaleInvoiceLines.InvoiceId=SalesInvoices.Id
	WHERE SalesInvoices.Id=@SalesInvoiceId
END
GO


CREATE PROC SearchInvoiceForPrintingByInvoiceId @InvoiceId INT
AS
BEGIN
	SELECT 
	
	Clients.Name,
	Clients.Address,
	Clients.Phone,
	Clients.AlternativePhone,
	Clients.TermsOfPayment,

	SalesInvoices.InvoiceNo,
	
	ProductsAndServices.Name, 
	ProductsAndServices.Description,
	
	SaleInvoiceLines.ClientCharge,

	Companies.Logo,
	Companies.Name

	FROM Companies INNER JOIN ProductsAndServices ON Companies.Id=ProductsAndServices.CompanyId 
	 INNER JOIN SaleInvoiceLines ON SaleInvoiceLines.ExpenseId=ProductsAndServices.Id
		INNER JOIN SalesInvoices ON SalesInvoices.Id=SaleInvoiceLines.InvoiceId
		INNER JOIN Projects ON Projects.Id = SalesInvoices.ProjectId
		INNER JOIN Clients ON Projects.ClientId=Clients.Id
		
	WHERE SalesInvoices.Id=@InvoiceId;

	
END
GO


CREATE PROC AllQuotations @CompanyId INT AS
BEGIN
	SELECT Quotations.*,
	Clients.Name,
	Projects.Name
	
	FROM Quotations INNER JOIN Projects ON Projects.Id=Quotations.ProjectId 
		INNER JOIN Clients ON Clients.Id=Projects.ClientId 
		INNER JOIN ClientGroups ON Clients.ClientGroupId = ClientGroups.Id
	WHERE ClientGroups.CompanyId=@CompanyId
END
GO

CREATE PROC AddQuotation 
@StatusId INT,
@ProjectId INT,
@CurrencyId INT,
@CustRef VARCHAR(200),
@QuotationNo1 VARCHAR(10),
@QuotationNo2 INT,
@QuotationDate DATE,
@ShipType VARCHAR(100),
@BL_Reference VARCHAR(50),
@Remarks VARCHAR(300),
@Attention VARCHAR(200),
@IsTaxable BIT,
@ValidUntil DATE
AS
BEGIN
	INSERT INTO Quotations (
	StatusId, 
	ProjectId, 
	CurrencyId, 
	CustRef, 
	QuotationNo1,
	QuotationNo2,
	QuotationDate,
	ShipType,
	BL_Reference, 
	Remarks, 
	Attention, 
	IsTaxable, 
	ValidUntil)

	VALUES (
	@StatusId,
	@ProjectId, 
	@CurrencyId, 
	@CustRef, 
	@QuotationNo1, 
	@QuotationNo2,
	@QuotationDate,
	@ShipType,
	@BL_Reference,
	@Remarks, 
	@Attention,
	@IsTaxable, 
	@ValidUntil);
END
GO

CREATE PROC LastQuotationId
AS 
BEGIN
	SELECT TOP 1 Quotations.Id FROM Quotations 
	ORDER BY Quotations.Id DESC
END
GO

CREATE PROC NextQuotationNo @QuotationNo1 VARCHAR(10) AS
BEGIN
	SELECT Quotations.QuotationNo2 
	FROM Quotations
	WHERE Quotations.QuotationNo1 LIKE @QuotationNo1
END
GO

CREATE PROC AddQuotationLine 

@QuotationId INT ,
@IsExpense BIT,
@IsProductService BIT,
@ItemId INT,
@Incharge INT ,
@Date DATE,
@CurrencyId INT ,
@FC_Amount MONEY,
@LC_Amount MONEY,
@ClientCharge MONEY
AS
BEGIN
	INSERT INTO QuotationLines (
		
		QuotationId ,

		IsExpense ,
		IsProductService ,
		ItemId ,
		Incharge ,
		Date ,
		CurrencyId ,
		FC_Amount ,
		LC_Amount ,
		ClientCharge )

		VALUES (
		@QuotationId  ,
		@IsExpense ,
		@IsProductService ,
		@ItemId ,
		@Incharge  ,
		@Date ,
		@CurrencyId  ,
		@FC_Amount ,
		@LC_Amount ,
		@ClientCharge )
END
GO


CREATE PROC AllQuotationStatuses AS
BEGIN
	SELECT * FROM QuotationStatus
END
GO

CREATE PROC PrintQuotationHeader @QuotationId INT AS
BEGIN
	SELECT 
		Quotations.QuotationNo1,
		Quotations.QuotationNo2,
		Companies.Logo,
		Companies.LegalName,
		Companies.LegalAddress,
		Companies.Phone,
		Currencies.Code,
		Quotations.QuotationDate,
		Quotations.ValidUntil,
		Clients.Name

	FROM Quotations INNER JOIN Currencies ON Quotations.CurrencyId=Currencies.Id
	INNER JOIN Projects ON Projects.Id=Quotations.ProjectId
	INNER JOIN Clients ON Projects.ClientId=Clients.Id
	INNER JOIN ClientGroups ON ClientGroups.Id=Clients.ClientGroupId
	INNER JOIN Companies ON Companies.Id=ClientGroups.CompanyId

	WHERE Quotations.Id=@QuotationId
END
GO



CREATE PROC AddProject @ClientId INT, @Name VARCHAR(100), @Description VARCHAR(600)
AS
BEGIN
	INSERT INTO Projects(ClientId, Name, Description) VALUES (@ClientId, @Name, @Description)
END
GO


CREATE PROC AllProjects @CompanyId INT AS
BEGIN
	SELECT * FROM Projects INNER JOIN Clients ON Clients.Id=Projects.ClientId
	INNER JOIN ClientGroups ON ClientGroups.Id=Clients.ClientGroupId
	WHERE ClientGroups.CompanyId=@CompanyId
END
GO


CREATE PROC SearchProjectsByClientId @ClientId INT AS
BEGIN
	SELECT * FROM Projects WHERE ClientId=@ClientId
END
GO



--//////////////////////////////////////
-- GET ALL PRODUCTS AND SERVICES SP
--//////////////////////////////////////

CREATE PROC AllProductsAndServices @CompanyId INT AS
BEGIN
	SELECT * FROM ProductsAndServices 
	WHERE CompanyId= @CompanyId
END
GO

--//////////////////////////////////////
-- ADD PRODUCT OR SERVICE SP
--//////////////////////////////////////

CREATE PROC AddProductService 
@Name VARCHAR(100), 
@Description VARCHAR(300), 
@ExpenseCost MONEY, 
@SuggestedPrice MONEY, 
@CompanyId INT AS
BEGIN
	INSERT INTO ProductsAndServices (Name, Description, ExpenseCost, SuggestedPrice, CompanyId) VALUES (@Name, @Description, @ExpenseCost, @SuggestedPrice, @CompanyId);
END
GO

--//////////////////////////////////////
-- SEARCH SERVICE BY Id SP
--//////////////////////////////////////

CREATE PROC SearchProductServiceById @Id INT AS
BEGIN
	SELECT * FROM ProductsAndServices WHERE Id=@Id
END
GO




--//////////////////////////////////////
--//////////////////////////////////////
--//// SAMPLE DATA ////////////////
--//////////////////////////////////////
--//////////////////////////////////////



--INSERT INTO Companies (

