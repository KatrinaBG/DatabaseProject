USE [hurtownia]
GO
/****** Object:  Table [dbo].[kategoria]    Script Date: 08/09/2011 08:43:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[kategoria](
	[id_kategoria] [int] IDENTITY(1,1) NOT NULL,
	[nazwa] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_kategoria] PRIMARY KEY CLUSTERED 
(
	[id_kategoria] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[osoba_kontaktowa]    Script Date: 08/09/2011 08:43:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[osoba_kontaktowa](
	[id_osoba_kontaktowa] [int] IDENTITY(1,1) NOT NULL,
	[imie] [nvarchar](30) NOT NULL,
	[nazwisko] [nvarchar](30) NOT NULL,
	[tel] [nvarchar](15) NOT NULL,
	[mail] [nvarchar](max) NOT NULL,
 CONSTRAINT [PK_osoba_kontaktowa] PRIMARY KEY CLUSTERED 
(
	[id_osoba_kontaktowa] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[promocja]    Script Date: 08/09/2011 08:43:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[promocja](
	[id_promocji] [int] IDENTITY(1,1) NOT NULL,
	[obnizka] [decimal](19, 4) NOT NULL,
	[data_rozpoczecia] [datetime] NOT NULL,
	[data_zakonczenia] [datetime] NOT NULL,
 CONSTRAINT [PK_promocja] PRIMARY KEY CLUSTERED 
(
	[id_promocji] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[zamowienia]    Script Date: 08/09/2011 08:43:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[zamowienia](
	[id_zamowienia] [int] IDENTITY(1,1) NOT NULL,
	[data_wystawienia] [datetime] NOT NULL,
	[data_realizacji] [datetime] NOT NULL,
	[wartosc_brutto] [decimal](19, 4) NOT NULL,
 CONSTRAINT [PK_zamowienia] PRIMARY KEY CLUSTERED 
(
	[id_zamowienia] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[wojewodztwo]    Script Date: 08/09/2011 08:43:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[wojewodztwo](
	[id_wojewodztwo] [int] IDENTITY(1,1) NOT NULL,
	[nazwa] [nchar](30) NOT NULL,
 CONSTRAINT [PK_wojewodztwo] PRIMARY KEY CLUSTERED 
(
	[id_wojewodztwo] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  StoredProcedure [dbo].[usp_zamowieniaUpdate]    Script Date: 08/09/2011 08:43:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[usp_zamowieniaUpdate] 
    @id_zamowienia int,
    @data_wystawienia datetime,
    @data_realizacji datetime,
    @wartosc_brutto decimal(19, 4)
AS 
	SET NOCOUNT ON 
	SET XACT_ABORT ON  
	
	BEGIN TRAN

	UPDATE [dbo].[zamowienia]
	SET    [data_wystawienia] = @data_wystawienia, [data_realizacji] = @data_realizacji, [wartosc_brutto] = @wartosc_brutto
	WHERE  [id_zamowienia] = @id_zamowienia
	
	-- Begin Return Select <- do not remove
	SELECT [id_zamowienia], [data_wystawienia], [data_realizacji], [wartosc_brutto]
	FROM   [dbo].[zamowienia]
	WHERE  [id_zamowienia] = @id_zamowienia	
	-- End Return Select <- do not remove

	COMMIT TRAN
GO
/****** Object:  StoredProcedure [dbo].[usp_zamowieniaSelect]    Script Date: 08/09/2011 08:43:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[usp_zamowieniaSelect] 
    @id_zamowienia INT
AS 
	SET NOCOUNT ON 
	SET XACT_ABORT ON  

	BEGIN TRAN

	SELECT [id_zamowienia], [data_wystawienia], [data_realizacji], [wartosc_brutto] 
	FROM   [dbo].[zamowienia] 
	WHERE  ([id_zamowienia] = @id_zamowienia OR @id_zamowienia IS NULL) 

	COMMIT
GO
/****** Object:  StoredProcedure [dbo].[usp_zamowieniaInsert]    Script Date: 08/09/2011 08:43:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[usp_zamowieniaInsert] 
    @data_wystawienia datetime,
    @data_realizacji datetime,
    @wartosc_brutto decimal(19, 4)
AS 
	SET NOCOUNT ON 
	SET XACT_ABORT ON  
	
	BEGIN TRAN
	
	INSERT INTO [dbo].[zamowienia] ([data_wystawienia], [data_realizacji], [wartosc_brutto])
	SELECT @data_wystawienia, @data_realizacji, @wartosc_brutto
	
	-- Begin Return Select <- do not remove
	SELECT [id_zamowienia], [data_wystawienia], [data_realizacji], [wartosc_brutto]
	FROM   [dbo].[zamowienia]
	WHERE  [id_zamowienia] = SCOPE_IDENTITY()
	-- End Return Select <- do not remove
               
	COMMIT
GO
/****** Object:  StoredProcedure [dbo].[usp_zamowieniaDelete]    Script Date: 08/09/2011 08:43:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[usp_zamowieniaDelete] 
    @id_zamowienia int
AS 
	SET NOCOUNT ON 
	SET XACT_ABORT ON  
	
	BEGIN TRAN

	DELETE
	FROM   [dbo].[zamowienia]
	WHERE  [id_zamowienia] = @id_zamowienia

	COMMIT
GO
/****** Object:  StoredProcedure [dbo].[usp_wojewodztwoUpdate]    Script Date: 08/09/2011 08:43:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[usp_wojewodztwoUpdate] 
    @id_wojewodztwo int,
    @nazwa nchar(30)
AS 
	SET NOCOUNT ON 
	SET XACT_ABORT ON  
	
	BEGIN TRAN

	UPDATE [dbo].[wojewodztwo]
	SET    [nazwa] = @nazwa
	WHERE  [id_wojewodztwo] = @id_wojewodztwo
	
	-- Begin Return Select <- do not remove
	SELECT [id_wojewodztwo], [nazwa]
	FROM   [dbo].[wojewodztwo]
	WHERE  [id_wojewodztwo] = @id_wojewodztwo	
	-- End Return Select <- do not remove

	COMMIT TRAN
GO
/****** Object:  StoredProcedure [dbo].[usp_wojewodztwoSelect]    Script Date: 08/09/2011 08:43:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[usp_wojewodztwoSelect] 
    @id_wojewodztwo INT
AS 
	SET NOCOUNT ON 
	SET XACT_ABORT ON  

	BEGIN TRAN

	SELECT [id_wojewodztwo], [nazwa] 
	FROM   [dbo].[wojewodztwo] 
	WHERE  ([id_wojewodztwo] = @id_wojewodztwo OR @id_wojewodztwo IS NULL) 

	COMMIT
GO
/****** Object:  StoredProcedure [dbo].[usp_wojewodztwoInsert]    Script Date: 08/09/2011 08:43:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[usp_wojewodztwoInsert] 
    @nazwa nchar(30)
AS 
	SET NOCOUNT ON 
	SET XACT_ABORT ON  
	
	BEGIN TRAN
	
	INSERT INTO [dbo].[wojewodztwo] ([nazwa])
	SELECT @nazwa
	
	-- Begin Return Select <- do not remove
	SELECT [id_wojewodztwo], [nazwa]
	FROM   [dbo].[wojewodztwo]
	WHERE  [id_wojewodztwo] = SCOPE_IDENTITY()
	-- End Return Select <- do not remove
               
	COMMIT
GO
/****** Object:  StoredProcedure [dbo].[usp_wojewodztwoDelete]    Script Date: 08/09/2011 08:43:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[usp_wojewodztwoDelete] 
    @id_wojewodztwo int
AS 
	SET NOCOUNT ON 
	SET XACT_ABORT ON  
	
	BEGIN TRAN

	DELETE
	FROM   [dbo].[wojewodztwo]
	WHERE  [id_wojewodztwo] = @id_wojewodztwo

	COMMIT
GO
/****** Object:  StoredProcedure [dbo].[usp_osoba_kontaktowaUpdate]    Script Date: 08/09/2011 08:43:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[usp_osoba_kontaktowaUpdate] 
    @id_osoba_kontaktowa int,
    @imie nvarchar(30),
    @nazwisko nvarchar(30),
    @tel nvarchar(15),
    @mail nvarchar(MAX)
AS 
	SET NOCOUNT ON 
	SET XACT_ABORT ON  
	
	BEGIN TRAN

	UPDATE [dbo].[osoba_kontaktowa]
	SET    [imie] = @imie, [nazwisko] = @nazwisko, [tel] = @tel, [mail] = @mail
	WHERE  [id_osoba_kontaktowa] = @id_osoba_kontaktowa
	
	-- Begin Return Select <- do not remove
	SELECT [id_osoba_kontaktowa], [imie], [nazwisko], [tel], [mail]
	FROM   [dbo].[osoba_kontaktowa]
	WHERE  [id_osoba_kontaktowa] = @id_osoba_kontaktowa	
	-- End Return Select <- do not remove

	COMMIT TRAN
GO
/****** Object:  StoredProcedure [dbo].[usp_osoba_kontaktowaSelect]    Script Date: 08/09/2011 08:43:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[usp_osoba_kontaktowaSelect] 
    @id_osoba_kontaktowa INT
AS 
	SET NOCOUNT ON 
	SET XACT_ABORT ON  

	BEGIN TRAN

	SELECT [id_osoba_kontaktowa], [imie], [nazwisko], [tel], [mail] 
	FROM   [dbo].[osoba_kontaktowa] 
	WHERE  ([id_osoba_kontaktowa] = @id_osoba_kontaktowa OR @id_osoba_kontaktowa IS NULL) 

	COMMIT
GO
/****** Object:  StoredProcedure [dbo].[usp_osoba_kontaktowaInsert]    Script Date: 08/09/2011 08:43:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[usp_osoba_kontaktowaInsert] 
    @imie nvarchar(30),
    @nazwisko nvarchar(30),
    @tel nvarchar(15),
    @mail nvarchar(MAX)
AS 
	SET NOCOUNT ON 
	SET XACT_ABORT ON  
	
	BEGIN TRAN
	
	INSERT INTO [dbo].[osoba_kontaktowa] ([imie], [nazwisko], [tel], [mail])
	SELECT @imie, @nazwisko, @tel, @mail
	
	-- Begin Return Select <- do not remove
	SELECT [id_osoba_kontaktowa], [imie], [nazwisko], [tel], [mail]
	FROM   [dbo].[osoba_kontaktowa]
	WHERE  [id_osoba_kontaktowa] = SCOPE_IDENTITY()
	-- End Return Select <- do not remove
               
	COMMIT
GO
/****** Object:  StoredProcedure [dbo].[usp_osoba_kontaktowaDelete]    Script Date: 08/09/2011 08:43:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[usp_osoba_kontaktowaDelete] 
    @id_osoba_kontaktowa int
AS 
	SET NOCOUNT ON 
	SET XACT_ABORT ON  
	
	BEGIN TRAN

	DELETE
	FROM   [dbo].[osoba_kontaktowa]
	WHERE  [id_osoba_kontaktowa] = @id_osoba_kontaktowa

	COMMIT
GO
/****** Object:  StoredProcedure [dbo].[usp_promocjaUpdate]    Script Date: 08/09/2011 08:43:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[usp_promocjaUpdate] 
    @id_promocji int,
    @obnizka decimal(19, 4),
    @data_rozpoczecia datetime,
    @data_zakonczenia datetime
AS 
	SET NOCOUNT ON 
	SET XACT_ABORT ON  
	
	BEGIN TRAN

	UPDATE [dbo].[promocja]
	SET    [obnizka] = @obnizka, [data_rozpoczecia] = @data_rozpoczecia, [data_zakonczenia] = @data_zakonczenia
	WHERE  [id_promocji] = @id_promocji
	
	-- Begin Return Select <- do not remove
	SELECT [id_promocji], [obnizka], [data_rozpoczecia], [data_zakonczenia]
	FROM   [dbo].[promocja]
	WHERE  [id_promocji] = @id_promocji	
	-- End Return Select <- do not remove

	COMMIT TRAN
GO
/****** Object:  StoredProcedure [dbo].[usp_promocjaSelect]    Script Date: 08/09/2011 08:43:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[usp_promocjaSelect] 
    @id_promocji INT
AS 
	SET NOCOUNT ON 
	SET XACT_ABORT ON  

	BEGIN TRAN

	SELECT [id_promocji], [obnizka], [data_rozpoczecia], [data_zakonczenia] 
	FROM   [dbo].[promocja] 
	WHERE  ([id_promocji] = @id_promocji OR @id_promocji IS NULL) 

	COMMIT
GO
/****** Object:  StoredProcedure [dbo].[usp_promocjaInsert]    Script Date: 08/09/2011 08:43:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[usp_promocjaInsert] 
    @obnizka decimal(19, 4),
    @data_rozpoczecia datetime,
    @data_zakonczenia datetime
AS 
	SET NOCOUNT ON 
	SET XACT_ABORT ON  
	
	BEGIN TRAN
	
	INSERT INTO [dbo].[promocja] ([obnizka], [data_rozpoczecia], [data_zakonczenia])
	SELECT @obnizka, @data_rozpoczecia, @data_zakonczenia
	
	-- Begin Return Select <- do not remove
	SELECT [id_promocji], [obnizka], [data_rozpoczecia], [data_zakonczenia]
	FROM   [dbo].[promocja]
	WHERE  [id_promocji] = SCOPE_IDENTITY()
	-- End Return Select <- do not remove
               
	COMMIT
GO
/****** Object:  StoredProcedure [dbo].[usp_promocjaDelete]    Script Date: 08/09/2011 08:43:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[usp_promocjaDelete] 
    @id_promocji int
AS 
	SET NOCOUNT ON 
	SET XACT_ABORT ON  
	
	BEGIN TRAN

	DELETE
	FROM   [dbo].[promocja]
	WHERE  [id_promocji] = @id_promocji

	COMMIT
GO
/****** Object:  Table [dbo].[adres]    Script Date: 08/09/2011 08:43:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[adres](
	[id_adres] [int] IDENTITY(1,1) NOT NULL,
	[panstwo] [nchar](20) NOT NULL,
	[miasto] [nchar](20) NOT NULL,
	[ulica] [nchar](30) NOT NULL,
	[kod_pocztowy] [nchar](6) NOT NULL,
	[nr_domu] [nchar](10) NOT NULL,
	[nr_mieszkania] [nchar](10) NOT NULL,
	[id_wojewodztwo] [int] NOT NULL,
 CONSTRAINT [PK_adres] PRIMARY KEY CLUSTERED 
(
	[id_adres] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  StoredProcedure [dbo].[usp_kategoriaUpdate]    Script Date: 08/09/2011 08:43:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[usp_kategoriaUpdate] 
    @id_kategoria int,
    @nazwa nvarchar(50)
AS 
	SET NOCOUNT ON 
	SET XACT_ABORT ON  
	
	BEGIN TRAN

	UPDATE [dbo].[kategoria]
	SET    [nazwa] = @nazwa
	WHERE  [id_kategoria] = @id_kategoria
	
	-- Begin Return Select <- do not remove
	SELECT [id_kategoria], [nazwa]
	FROM   [dbo].[kategoria]
	WHERE  [id_kategoria] = @id_kategoria	
	-- End Return Select <- do not remove

	COMMIT TRAN
GO
/****** Object:  StoredProcedure [dbo].[usp_kategoriaSelect]    Script Date: 08/09/2011 08:43:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[usp_kategoriaSelect] 
    @id_kategoria INT
AS 
	SET NOCOUNT ON 
	SET XACT_ABORT ON  

	BEGIN TRAN

	SELECT [id_kategoria], [nazwa] 
	FROM   [dbo].[kategoria] 
	WHERE  ([id_kategoria] = @id_kategoria OR @id_kategoria IS NULL) 

	COMMIT
GO
/****** Object:  StoredProcedure [dbo].[usp_kategoriaInsert]    Script Date: 08/09/2011 08:43:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[usp_kategoriaInsert] 
    @nazwa nvarchar(50)
AS 
	SET NOCOUNT ON 
	SET XACT_ABORT ON  
	
	BEGIN TRAN
	
	INSERT INTO [dbo].[kategoria] ([nazwa])
	SELECT @nazwa
	
	-- Begin Return Select <- do not remove
	SELECT [id_kategoria], [nazwa]
	FROM   [dbo].[kategoria]
	WHERE  [id_kategoria] = SCOPE_IDENTITY()
	-- End Return Select <- do not remove
               
	COMMIT
GO
/****** Object:  StoredProcedure [dbo].[usp_kategoriaDelete]    Script Date: 08/09/2011 08:43:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[usp_kategoriaDelete] 
    @id_kategoria int
AS 
	SET NOCOUNT ON 
	SET XACT_ABORT ON  
	
	BEGIN TRAN

	DELETE
	FROM   [dbo].[kategoria]
	WHERE  [id_kategoria] = @id_kategoria

	COMMIT
GO
/****** Object:  StoredProcedure [dbo].[usp_adresUpdate]    Script Date: 08/09/2011 08:43:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[usp_adresUpdate] 
    @id_adres int,
    @panstwo nchar(20),
    @miasto nchar(20),
    @ulica nchar(30),
    @kod_pocztowy nchar(6),
    @nr_domu nchar(10),
    @nr_mieszkania nchar(10),
    @id_wojewodztwo int
AS 
	SET NOCOUNT ON 
	SET XACT_ABORT ON  
	
	BEGIN TRAN

	UPDATE [dbo].[adres]
	SET    [panstwo] = @panstwo, [miasto] = @miasto, [ulica] = @ulica, [kod_pocztowy] = @kod_pocztowy, [nr_domu] = @nr_domu, [nr_mieszkania] = @nr_mieszkania, [id_wojewodztwo] = @id_wojewodztwo
	WHERE  [id_adres] = @id_adres
	
	-- Begin Return Select <- do not remove
	SELECT [id_adres], [panstwo], [miasto], [ulica], [kod_pocztowy], [nr_domu], [nr_mieszkania], [id_wojewodztwo]
	FROM   [dbo].[adres]
	WHERE  [id_adres] = @id_adres	
	-- End Return Select <- do not remove

	COMMIT TRAN
GO
/****** Object:  StoredProcedure [dbo].[usp_adresSelect]    Script Date: 08/09/2011 08:43:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[usp_adresSelect] 
    @id_adres INT
AS 
	SET NOCOUNT ON 
	SET XACT_ABORT ON  

	BEGIN TRAN

	SELECT [id_adres], [panstwo], [miasto], [ulica], [kod_pocztowy], [nr_domu], [nr_mieszkania], [id_wojewodztwo] 
	FROM   [dbo].[adres] 
	WHERE  ([id_adres] = @id_adres OR @id_adres IS NULL) 

	COMMIT
GO
/****** Object:  StoredProcedure [dbo].[usp_adresInsert]    Script Date: 08/09/2011 08:43:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[usp_adresInsert] 
    @panstwo nchar(20),
    @miasto nchar(20),
    @ulica nchar(30),
    @kod_pocztowy nchar(6),
    @nr_domu nchar(10),
    @nr_mieszkania nchar(10),
    @id_wojewodztwo int
AS 
	SET NOCOUNT ON 
	SET XACT_ABORT ON  
	
	BEGIN TRAN
	
	INSERT INTO [dbo].[adres] ([panstwo], [miasto], [ulica], [kod_pocztowy], [nr_domu], [nr_mieszkania], [id_wojewodztwo])
	SELECT @panstwo, @miasto, @ulica, @kod_pocztowy, @nr_domu, @nr_mieszkania, @id_wojewodztwo
	
	-- Begin Return Select <- do not remove
	SELECT [id_adres], [panstwo], [miasto], [ulica], [kod_pocztowy], [nr_domu], [nr_mieszkania], [id_wojewodztwo]
	FROM   [dbo].[adres]
	WHERE  [id_adres] = SCOPE_IDENTITY()
	-- End Return Select <- do not remove
               
	COMMIT
GO
/****** Object:  StoredProcedure [dbo].[usp_adresDelete]    Script Date: 08/09/2011 08:43:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[usp_adresDelete] 
    @id_adres int
AS 
	SET NOCOUNT ON 
	SET XACT_ABORT ON  
	
	BEGIN TRAN

	DELETE
	FROM   [dbo].[adres]
	WHERE  [id_adres] = @id_adres

	COMMIT
GO
/****** Object:  Table [dbo].[pracownik]    Script Date: 08/09/2011 08:43:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[pracownik](
	[id_pracownika] [nvarchar](7) NOT NULL,
	[imie] [nchar](30) NOT NULL,
	[nazwisko] [nchar](30) NOT NULL,
	[pesel] [char](11) NOT NULL,
	[id_adres] [int] NOT NULL,
	[wyplata] [money] NOT NULL,
	[pracuje] [binary](1) NOT NULL,
 CONSTRAINT [PK_pracownik] PRIMARY KEY CLUSTERED 
(
	[id_pracownika] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  StoredProcedure [dbo].[usp_pracownikUpdate]    Script Date: 08/09/2011 08:43:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[usp_pracownikUpdate] 
    @id_pracownika nvarchar(7),
    @imie nchar(30),
    @nazwisko nchar(30),
    @pesel char(11),
    @id_adres int,
    @wyplata money,
    @pracuje binary(1)
AS 
	SET NOCOUNT ON 
	SET XACT_ABORT ON  
	
	BEGIN TRAN

	UPDATE [dbo].[pracownik]
	SET    [id_pracownika] = @id_pracownika, [imie] = @imie, [nazwisko] = @nazwisko, [pesel] = @pesel, [id_adres] = @id_adres, [wyplata] = @wyplata, [pracuje] = @pracuje
	WHERE  [id_pracownika] = @id_pracownika
	
	-- Begin Return Select <- do not remove
	SELECT [id_pracownika], [imie], [nazwisko], [pesel], [id_adres], [wyplata], [pracuje]
	FROM   [dbo].[pracownik]
	WHERE  [id_pracownika] = @id_pracownika	
	-- End Return Select <- do not remove

	COMMIT TRAN
GO
/****** Object:  StoredProcedure [dbo].[usp_pracownikSelect]    Script Date: 08/09/2011 08:43:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[usp_pracownikSelect] 
    @id_pracownika NVARCHAR(7)
AS 
	SET NOCOUNT ON 
	SET XACT_ABORT ON  

	BEGIN TRAN

	SELECT [id_pracownika], [imie], [nazwisko], [pesel], [id_adres], [wyplata], [pracuje] 
	FROM   [dbo].[pracownik] 
	WHERE  ([id_pracownika] = @id_pracownika OR @id_pracownika IS NULL) 

	COMMIT
GO
/****** Object:  StoredProcedure [dbo].[usp_pracownikInsert]    Script Date: 08/09/2011 08:43:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[usp_pracownikInsert] 
    @id_pracownika nvarchar(7),
    @imie nchar(30),
    @nazwisko nchar(30),
    @pesel char(11),
    @id_adres int,
    @wyplata money,
    @pracuje binary(1)
AS 
	SET NOCOUNT ON 
	SET XACT_ABORT ON  
	
	BEGIN TRAN
	
	INSERT INTO [dbo].[pracownik] ([id_pracownika], [imie], [nazwisko], [pesel], [id_adres], [wyplata], [pracuje])
	SELECT @id_pracownika, @imie, @nazwisko, @pesel, @id_adres, @wyplata, @pracuje
	
	-- Begin Return Select <- do not remove
	SELECT [id_pracownika], [imie], [nazwisko], [pesel], [id_adres], [wyplata], [pracuje]
	FROM   [dbo].[pracownik]
	WHERE  [id_pracownika] = @id_pracownika
	-- End Return Select <- do not remove
               
	COMMIT
GO
/****** Object:  StoredProcedure [dbo].[usp_pracownikDelete]    Script Date: 08/09/2011 08:43:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[usp_pracownikDelete] 
    @id_pracownika nvarchar(7)
AS 
	SET NOCOUNT ON 
	SET XACT_ABORT ON  
	
	BEGIN TRAN

	DELETE
	FROM   [dbo].[pracownik]
	WHERE  [id_pracownika] = @id_pracownika

	COMMIT
GO
/****** Object:  Table [dbo].[kontrahenci]    Script Date: 08/09/2011 08:43:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[kontrahenci](
	[id_kontrahenci] [int] NOT NULL,
	[Nazwa] [nvarchar](30) NOT NULL,
	[id_adres_wysylki] [int] NOT NULL,
	[id_adres_fv] [int] NOT NULL,
	[nip] [nvarchar](10) NOT NULL,
	[regon] [int] NOT NULL,
	[id_osoba_kontaktowa] [int] NOT NULL,
	[id_pracownika] [nvarchar](7) NOT NULL,
	[umowa] [bit] NOT NULL,
	[data_podpisania_umowy] [datetime] NOT NULL,
	[kredyt_kupiecki] [binary](1) NOT NULL,
	[wartosc_kredytu] [money] NULL,
 CONSTRAINT [PK_kontrahenci] PRIMARY KEY CLUSTERED 
(
	[id_kontrahenci] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[producent]    Script Date: 08/09/2011 08:43:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[producent](
	[id_producent] [int] IDENTITY(1,1) NOT NULL,
	[id_kontrahenci] [int] NOT NULL,
	[id_pracownika] [nvarchar](7) NOT NULL,
	[logo] [bit] NOT NULL,
	[strona_www] [nvarchar](max) NOT NULL,
 CONSTRAINT [PK_producent] PRIMARY KEY CLUSTERED 
(
	[id_producent] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[sprzedaz]    Script Date: 08/09/2011 08:43:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[sprzedaz](
	[id_sprzedaz] [int] IDENTITY(1,1) NOT NULL,
	[id_zamowienia] [int] NOT NULL,
	[id_pracownika] [nvarchar](7) NOT NULL,
	[id_kontrahenci] [int] NOT NULL,
 CONSTRAINT [PK_sprzedaz_] PRIMARY KEY CLUSTERED 
(
	[id_sprzedaz] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[MZZ]    Script Date: 08/09/2011 08:43:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[MZZ](
	[MZZ] [int] NOT NULL,
	[data_zamowienia] [date] NOT NULL,
	[id_pracownika] [nvarchar](7) NOT NULL,
	[id_kontrahenci] [int] NOT NULL,
 CONSTRAINT [PK_MZZ] PRIMARY KEY CLUSTERED 
(
	[MZZ] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  StoredProcedure [dbo].[usp_kontrahenciUpdate]    Script Date: 08/09/2011 08:43:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[usp_kontrahenciUpdate] 
    @id_kontrahenci int,
    @Nazwa nvarchar(30),
    @id_adres_wysylki int,
    @id_adres_fv int,
    @nip nvarchar(10),
    @regon int,
    @id_osoba_kontaktowa int,
    @id_pracownika nvarchar(7),
    @umowa bit,
    @data_podpisania_umowy datetime,
    @kredyt_kupiecki binary(1),
    @wartosc_kredytu money
AS 
	SET NOCOUNT ON 
	SET XACT_ABORT ON  
	
	BEGIN TRAN

	UPDATE [dbo].[kontrahenci]
	SET    [id_kontrahenci] = @id_kontrahenci, [Nazwa] = @Nazwa, [id_adres_wysylki] = @id_adres_wysylki, [id_adres_fv] = @id_adres_fv, [nip] = @nip, [regon] = @regon, [id_osoba_kontaktowa] = @id_osoba_kontaktowa, [id_pracownika] = @id_pracownika, [umowa] = @umowa, [data_podpisania_umowy] = @data_podpisania_umowy, [kredyt_kupiecki] = @kredyt_kupiecki, [wartosc_kredytu] = @wartosc_kredytu
	WHERE  [id_kontrahenci] = @id_kontrahenci
	
	-- Begin Return Select <- do not remove
	SELECT [id_kontrahenci], [Nazwa], [id_adres_wysylki], [id_adres_fv], [nip], [regon], [id_osoba_kontaktowa], [id_pracownika], [umowa], [data_podpisania_umowy], [kredyt_kupiecki], [wartosc_kredytu]
	FROM   [dbo].[kontrahenci]
	WHERE  [id_kontrahenci] = @id_kontrahenci	
	-- End Return Select <- do not remove

	COMMIT TRAN
GO
/****** Object:  StoredProcedure [dbo].[usp_kontrahenciSelect]    Script Date: 08/09/2011 08:43:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[usp_kontrahenciSelect] 
    @id_kontrahenci INT
AS 
	SET NOCOUNT ON 
	SET XACT_ABORT ON  

	BEGIN TRAN

	SELECT [id_kontrahenci], [Nazwa], [id_adres_wysylki], [id_adres_fv], [nip], [regon], [id_osoba_kontaktowa], [id_pracownika], [umowa], [data_podpisania_umowy], [kredyt_kupiecki], [wartosc_kredytu] 
	FROM   [dbo].[kontrahenci] 
	WHERE  ([id_kontrahenci] = @id_kontrahenci OR @id_kontrahenci IS NULL) 

	COMMIT
GO
/****** Object:  StoredProcedure [dbo].[usp_kontrahenciInsert]    Script Date: 08/09/2011 08:43:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[usp_kontrahenciInsert] 
    @id_kontrahenci int,
    @Nazwa nvarchar(30),
    @id_adres_wysylki int,
    @id_adres_fv int,
    @nip nvarchar(10),
    @regon int,
    @id_osoba_kontaktowa int,
    @id_pracownika nvarchar(7),
    @umowa bit,
    @data_podpisania_umowy datetime,
    @kredyt_kupiecki binary(1),
    @wartosc_kredytu money
AS 
	SET NOCOUNT ON 
	SET XACT_ABORT ON  
	
	BEGIN TRAN
	
	INSERT INTO [dbo].[kontrahenci] ([id_kontrahenci], [Nazwa], [id_adres_wysylki], [id_adres_fv], [nip], [regon], [id_osoba_kontaktowa], [id_pracownika], [umowa], [data_podpisania_umowy], [kredyt_kupiecki], [wartosc_kredytu])
	SELECT @id_kontrahenci, @Nazwa, @id_adres_wysylki, @id_adres_fv, @nip, @regon, @id_osoba_kontaktowa, @id_pracownika, @umowa, @data_podpisania_umowy, @kredyt_kupiecki, @wartosc_kredytu
	
	-- Begin Return Select <- do not remove
	SELECT [id_kontrahenci], [Nazwa], [id_adres_wysylki], [id_adres_fv], [nip], [regon], [id_osoba_kontaktowa], [id_pracownika], [umowa], [data_podpisania_umowy], [kredyt_kupiecki], [wartosc_kredytu]
	FROM   [dbo].[kontrahenci]
	WHERE  [id_kontrahenci] = @id_kontrahenci
	-- End Return Select <- do not remove
               
	COMMIT
GO
/****** Object:  StoredProcedure [dbo].[usp_kontrahenciDelete]    Script Date: 08/09/2011 08:43:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[usp_kontrahenciDelete] 
    @id_kontrahenci int
AS 
	SET NOCOUNT ON 
	SET XACT_ABORT ON  
	
	BEGIN TRAN

	DELETE
	FROM   [dbo].[kontrahenci]
	WHERE  [id_kontrahenci] = @id_kontrahenci

	COMMIT
GO
/****** Object:  StoredProcedure [dbo].[usp_producentUpdate]    Script Date: 08/09/2011 08:43:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[usp_producentUpdate] 
    @id_producent int,
    @id_kontrahenci int,
    @id_pracownika nvarchar(7),
    @logo bit,
    @strona_www nvarchar(MAX)
AS 
	SET NOCOUNT ON 
	SET XACT_ABORT ON  
	
	BEGIN TRAN

	UPDATE [dbo].[producent]
	SET    [id_kontrahenci] = @id_kontrahenci, [id_pracownika] = @id_pracownika, [logo] = @logo, [strona_www] = @strona_www
	WHERE  [id_producent] = @id_producent
	
	-- Begin Return Select <- do not remove
	SELECT [id_producent], [id_kontrahenci], [id_pracownika], [logo], [strona_www]
	FROM   [dbo].[producent]
	WHERE  [id_producent] = @id_producent	
	-- End Return Select <- do not remove

	COMMIT TRAN
GO
/****** Object:  StoredProcedure [dbo].[usp_producentSelect]    Script Date: 08/09/2011 08:43:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[usp_producentSelect] 
    @id_producent INT
AS 
	SET NOCOUNT ON 
	SET XACT_ABORT ON  

	BEGIN TRAN

	SELECT [id_producent], [id_kontrahenci], [id_pracownika], [logo], [strona_www] 
	FROM   [dbo].[producent] 
	WHERE  ([id_producent] = @id_producent OR @id_producent IS NULL) 

	COMMIT
GO
/****** Object:  StoredProcedure [dbo].[usp_producentInsert]    Script Date: 08/09/2011 08:43:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[usp_producentInsert] 
    @id_kontrahenci int,
    @id_pracownika nvarchar(7),
    @logo bit,
    @strona_www nvarchar(MAX)
AS 
	SET NOCOUNT ON 
	SET XACT_ABORT ON  
	
	BEGIN TRAN
	
	INSERT INTO [dbo].[producent] ([id_kontrahenci], [id_pracownika], [logo], [strona_www])
	SELECT @id_kontrahenci, @id_pracownika, @logo, @strona_www
	
	-- Begin Return Select <- do not remove
	SELECT [id_producent], [id_kontrahenci], [id_pracownika], [logo], [strona_www]
	FROM   [dbo].[producent]
	WHERE  [id_producent] = SCOPE_IDENTITY()
	-- End Return Select <- do not remove
               
	COMMIT
GO
/****** Object:  StoredProcedure [dbo].[usp_producentDelete]    Script Date: 08/09/2011 08:43:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[usp_producentDelete] 
    @id_producent int
AS 
	SET NOCOUNT ON 
	SET XACT_ABORT ON  
	
	BEGIN TRAN

	DELETE
	FROM   [dbo].[producent]
	WHERE  [id_producent] = @id_producent

	COMMIT
GO
/****** Object:  StoredProcedure [dbo].[usp_sprzedazUpdate]    Script Date: 08/09/2011 08:43:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[usp_sprzedazUpdate] 
    @id_sprzedaz int,
    @id_zamowienia int,
    @id_pracownika nvarchar(7),
    @id_kontrahenci int
AS 
	SET NOCOUNT ON 
	SET XACT_ABORT ON  
	
	BEGIN TRAN

	UPDATE [dbo].[sprzedaz]
	SET    [id_zamowienia] = @id_zamowienia, [id_pracownika] = @id_pracownika, [id_kontrahenci] = @id_kontrahenci
	WHERE  [id_sprzedaz] = @id_sprzedaz
	
	-- Begin Return Select <- do not remove
	SELECT [id_sprzedaz], [id_zamowienia], [id_pracownika], [id_kontrahenci]
	FROM   [dbo].[sprzedaz]
	WHERE  [id_sprzedaz] = @id_sprzedaz	
	-- End Return Select <- do not remove

	COMMIT TRAN
GO
/****** Object:  StoredProcedure [dbo].[usp_sprzedazSelect]    Script Date: 08/09/2011 08:43:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[usp_sprzedazSelect] 
    @id_sprzedaz INT
AS 
	SET NOCOUNT ON 
	SET XACT_ABORT ON  

	BEGIN TRAN

	SELECT [id_sprzedaz], [id_zamowienia], [id_pracownika], [id_kontrahenci] 
	FROM   [dbo].[sprzedaz] 
	WHERE  ([id_sprzedaz] = @id_sprzedaz OR @id_sprzedaz IS NULL) 

	COMMIT
GO
/****** Object:  StoredProcedure [dbo].[usp_sprzedazInsert]    Script Date: 08/09/2011 08:43:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[usp_sprzedazInsert] 
    @id_zamowienia int,
    @id_pracownika nvarchar(7),
    @id_kontrahenci int
AS 
	SET NOCOUNT ON 
	SET XACT_ABORT ON  
	
	BEGIN TRAN
	
	INSERT INTO [dbo].[sprzedaz] ([id_zamowienia], [id_pracownika], [id_kontrahenci])
	SELECT @id_zamowienia, @id_pracownika, @id_kontrahenci
	
	-- Begin Return Select <- do not remove
	SELECT [id_sprzedaz], [id_zamowienia], [id_pracownika], [id_kontrahenci]
	FROM   [dbo].[sprzedaz]
	WHERE  [id_sprzedaz] = SCOPE_IDENTITY()
	-- End Return Select <- do not remove
               
	COMMIT
GO
/****** Object:  StoredProcedure [dbo].[usp_sprzedazDelete]    Script Date: 08/09/2011 08:43:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[usp_sprzedazDelete] 
    @id_sprzedaz int
AS 
	SET NOCOUNT ON 
	SET XACT_ABORT ON  
	
	BEGIN TRAN

	DELETE
	FROM   [dbo].[sprzedaz]
	WHERE  [id_sprzedaz] = @id_sprzedaz

	COMMIT
GO
/****** Object:  StoredProcedure [dbo].[usp_MZZUpdate]    Script Date: 08/09/2011 08:43:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[usp_MZZUpdate] 
    @MZZ int,
    @data_zamowienia date,
    @id_pracownika nvarchar(7),
    @id_kontrahenci int
AS 
	SET NOCOUNT ON 
	SET XACT_ABORT ON  
	
	BEGIN TRAN

	UPDATE [dbo].[MZZ]
	SET    [MZZ] = @MZZ, [data_zamowienia] = @data_zamowienia, [id_pracownika] = @id_pracownika, [id_kontrahenci] = @id_kontrahenci
	WHERE  [MZZ] = @MZZ
	
	-- Begin Return Select <- do not remove
	SELECT [MZZ], [data_zamowienia], [id_pracownika], [id_kontrahenci]
	FROM   [dbo].[MZZ]
	WHERE  [MZZ] = @MZZ	
	-- End Return Select <- do not remove

	COMMIT TRAN
GO
/****** Object:  StoredProcedure [dbo].[usp_MZZSelect]    Script Date: 08/09/2011 08:43:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[usp_MZZSelect] 
    @MZZ INT
AS 
	SET NOCOUNT ON 
	SET XACT_ABORT ON  

	BEGIN TRAN

	SELECT [MZZ], [data_zamowienia], [id_pracownika], [id_kontrahenci] 
	FROM   [dbo].[MZZ] 
	WHERE  ([MZZ] = @MZZ OR @MZZ IS NULL) 

	COMMIT
GO
/****** Object:  StoredProcedure [dbo].[usp_MZZInsert]    Script Date: 08/09/2011 08:43:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[usp_MZZInsert] 
    @MZZ int,
    @data_zamowienia date,
    @id_pracownika nvarchar(7),
    @id_kontrahenci int
AS 
	SET NOCOUNT ON 
	SET XACT_ABORT ON  
	
	BEGIN TRAN
	
	INSERT INTO [dbo].[MZZ] ([MZZ], [data_zamowienia], [id_pracownika], [id_kontrahenci])
	SELECT @MZZ, @data_zamowienia, @id_pracownika, @id_kontrahenci
	
	-- Begin Return Select <- do not remove
	SELECT [MZZ], [data_zamowienia], [id_pracownika], [id_kontrahenci]
	FROM   [dbo].[MZZ]
	WHERE  [MZZ] = @MZZ
	-- End Return Select <- do not remove
               
	COMMIT
GO
/****** Object:  StoredProcedure [dbo].[usp_MZZDelete]    Script Date: 08/09/2011 08:43:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[usp_MZZDelete] 
    @MZZ int
AS 
	SET NOCOUNT ON 
	SET XACT_ABORT ON  
	
	BEGIN TRAN

	DELETE
	FROM   [dbo].[MZZ]
	WHERE  [MZZ] = @MZZ

	COMMIT
GO
/****** Object:  Table [dbo].[produkty]    Script Date: 08/09/2011 08:43:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[produkty](
	[id_produkty] [int] IDENTITY(1,1) NOT NULL,
	[id_producent] [int] NOT NULL,
	[model] [nchar](30) NOT NULL,
	[opis] [varchar](max) NULL,
	[cena_katalogowa] [decimal](19, 4) NOT NULL,
	[stan_magazynowy] [int] NOT NULL,
	[id_kategoria] [int] NOT NULL,
	[PN] [nvarchar](20) NOT NULL,
	[aktywne] [binary](1) NOT NULL,
	[id_promocja] [int] NOT NULL,
 CONSTRAINT [PK_produkty] PRIMARY KEY CLUSTERED 
(
	[id_produkty] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[FV_zakupu]    Script Date: 08/09/2011 08:43:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[FV_zakupu](
	[NR_fv] [nvarchar](15) NOT NULL,
	[data_wystawienia_fv] [date] NOT NULL,
	[data_przyjecia] [date] NOT NULL,
	[id_kontrahenci] [int] NOT NULL,
	[termin_zaplaty] [int] NOT NULL,
	[MZZ] [int] NOT NULL,
	[zaplacono] [binary](1) NOT NULL,
 CONSTRAINT [PK_FV_zakupu] PRIMARY KEY CLUSTERED 
(
	[NR_fv] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  StoredProcedure [dbo].[usp_FV_zakupuUpdate]    Script Date: 08/09/2011 08:43:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[usp_FV_zakupuUpdate] 
    @NR_fv nvarchar(15),
    @data_wystawienia_fv date,
    @data_przyjecia date,
    @id_kontrahenci int,
    @termin_zaplaty int,
    @MZZ int,
    @zaplacono binary(1)
AS 
	SET NOCOUNT ON 
	SET XACT_ABORT ON  
	
	BEGIN TRAN

	UPDATE [dbo].[FV_zakupu]
	SET    [NR_fv] = @NR_fv, [data_wystawienia_fv] = @data_wystawienia_fv, [data_przyjecia] = @data_przyjecia, [id_kontrahenci] = @id_kontrahenci, [termin_zaplaty] = @termin_zaplaty, [MZZ] = @MZZ, [zaplacono] = @zaplacono
	WHERE  [NR_fv] = @NR_fv
	
	-- Begin Return Select <- do not remove
	SELECT [NR_fv], [data_wystawienia_fv], [data_przyjecia], [id_kontrahenci], [termin_zaplaty], [MZZ], [zaplacono]
	FROM   [dbo].[FV_zakupu]
	WHERE  [NR_fv] = @NR_fv	
	-- End Return Select <- do not remove

	COMMIT TRAN
GO
/****** Object:  StoredProcedure [dbo].[usp_FV_zakupuSelect]    Script Date: 08/09/2011 08:43:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[usp_FV_zakupuSelect] 
    @NR_fv NVARCHAR(15)
AS 
	SET NOCOUNT ON 
	SET XACT_ABORT ON  

	BEGIN TRAN

	SELECT [NR_fv], [data_wystawienia_fv], [data_przyjecia], [id_kontrahenci], [termin_zaplaty], [MZZ], [zaplacono] 
	FROM   [dbo].[FV_zakupu] 
	WHERE  ([NR_fv] = @NR_fv OR @NR_fv IS NULL) 

	COMMIT
GO
/****** Object:  StoredProcedure [dbo].[usp_FV_zakupuInsert]    Script Date: 08/09/2011 08:43:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[usp_FV_zakupuInsert] 
    @NR_fv nvarchar(15),
    @data_wystawienia_fv date,
    @data_przyjecia date,
    @id_kontrahenci int,
    @termin_zaplaty int,
    @MZZ int,
    @zaplacono binary(1)
AS 
	SET NOCOUNT ON 
	SET XACT_ABORT ON  
	
	BEGIN TRAN
	
	INSERT INTO [dbo].[FV_zakupu] ([NR_fv], [data_wystawienia_fv], [data_przyjecia], [id_kontrahenci], [termin_zaplaty], [MZZ], [zaplacono])
	SELECT @NR_fv, @data_wystawienia_fv, @data_przyjecia, @id_kontrahenci, @termin_zaplaty, @MZZ, @zaplacono
	
	-- Begin Return Select <- do not remove
	SELECT [NR_fv], [data_wystawienia_fv], [data_przyjecia], [id_kontrahenci], [termin_zaplaty], [MZZ], [zaplacono]
	FROM   [dbo].[FV_zakupu]
	WHERE  [NR_fv] = @NR_fv
	-- End Return Select <- do not remove
               
	COMMIT
GO
/****** Object:  StoredProcedure [dbo].[usp_FV_zakupuDelete]    Script Date: 08/09/2011 08:43:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[usp_FV_zakupuDelete] 
    @NR_fv nvarchar(15)
AS 
	SET NOCOUNT ON 
	SET XACT_ABORT ON  
	
	BEGIN TRAN

	DELETE
	FROM   [dbo].[FV_zakupu]
	WHERE  [NR_fv] = @NR_fv

	COMMIT
GO
/****** Object:  Table [dbo].[produkty_zamowienia]    Script Date: 08/09/2011 08:43:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[produkty_zamowienia](
	[id_produkty_zam] [int] IDENTITY(1,1) NOT NULL,
	[id_zamowienia] [int] NOT NULL,
	[id_produkty] [int] NOT NULL,
	[cena] [money] NOT NULL,
	[ilosc] [int] NOT NULL,
	[suma] [money] NOT NULL,
 CONSTRAINT [PK_produkty_zamowienia] PRIMARY KEY CLUSTERED 
(
	[id_produkty_zam] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[pozycje_zamowienia]    Script Date: 08/09/2011 08:43:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[pozycje_zamowienia](
	[id_pozycje_zamowienia] [int] NOT NULL,
	[id_produkty] [int] NOT NULL,
	[cena] [money] NULL,
	[ilosc_zamowiona] [int] NOT NULL,
	[ilosc_przyjeta] [int] NULL,
	[MZZ] [int] NOT NULL,
	[wartosc] [money] NULL,
 CONSTRAINT [PK_pozycje_zamowienia] PRIMARY KEY CLUSTERED 
(
	[id_pozycje_zamowienia] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  StoredProcedure [dbo].[usp_produktyUpdate]    Script Date: 08/09/2011 08:43:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[usp_produktyUpdate] 
    @id_produkty int,
    @id_producent int,
    @model nchar(30),
    @opis varchar(MAX),
    @cena_katalogowa decimal(19, 4),
    @stan_magazynowy int,
    @id_kategoria int,
    @PN nvarchar(20),
    @aktywne binary(1),
    @id_promocja int
AS 
	SET NOCOUNT ON 
	SET XACT_ABORT ON  
	
	BEGIN TRAN

	UPDATE [dbo].[produkty]
	SET    [id_producent] = @id_producent, [model] = @model, [opis] = @opis, [cena_katalogowa] = @cena_katalogowa, [stan_magazynowy] = @stan_magazynowy, [id_kategoria] = @id_kategoria, [PN] = @PN, [aktywne] = @aktywne, [id_promocja] = @id_promocja
	WHERE  [id_produkty] = @id_produkty
	
	-- Begin Return Select <- do not remove
	SELECT [id_produkty], [id_producent], [model], [opis], [cena_katalogowa], [stan_magazynowy], [id_kategoria], [PN], [aktywne], [id_promocja]
	FROM   [dbo].[produkty]
	WHERE  [id_produkty] = @id_produkty	
	-- End Return Select <- do not remove

	COMMIT TRAN
GO
/****** Object:  StoredProcedure [dbo].[usp_produktySelect]    Script Date: 08/09/2011 08:43:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[usp_produktySelect] 
    @id_produkty INT
AS 
	SET NOCOUNT ON 
	SET XACT_ABORT ON  

	BEGIN TRAN

	SELECT [id_produkty], [id_producent], [model], [opis], [cena_katalogowa], [stan_magazynowy], [id_kategoria], [PN], [aktywne], [id_promocja] 
	FROM   [dbo].[produkty] 
	WHERE  ([id_produkty] = @id_produkty OR @id_produkty IS NULL) 

	COMMIT
GO
/****** Object:  StoredProcedure [dbo].[usp_produktyInsert]    Script Date: 08/09/2011 08:43:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[usp_produktyInsert] 
    @id_producent int,
    @model nchar(30),
    @opis varchar(MAX),
    @cena_katalogowa decimal(19, 4),
    @stan_magazynowy int,
    @id_kategoria int,
    @PN nvarchar(20),
    @aktywne binary(1),
    @id_promocja int
AS 
	SET NOCOUNT ON 
	SET XACT_ABORT ON  
	
	BEGIN TRAN
	
	INSERT INTO [dbo].[produkty] ([id_producent], [model], [opis], [cena_katalogowa], [stan_magazynowy], [id_kategoria], [PN], [aktywne], [id_promocja])
	SELECT @id_producent, @model, @opis, @cena_katalogowa, @stan_magazynowy, @id_kategoria, @PN, @aktywne, @id_promocja
	
	-- Begin Return Select <- do not remove
	SELECT [id_produkty], [id_producent], [model], [opis], [cena_katalogowa], [stan_magazynowy], [id_kategoria], [PN], [aktywne], [id_promocja]
	FROM   [dbo].[produkty]
	WHERE  [id_produkty] = SCOPE_IDENTITY()
	-- End Return Select <- do not remove
               
	COMMIT
GO
/****** Object:  StoredProcedure [dbo].[usp_produktyDelete]    Script Date: 08/09/2011 08:43:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[usp_produktyDelete] 
    @id_produkty int
AS 
	SET NOCOUNT ON 
	SET XACT_ABORT ON  
	
	BEGIN TRAN

	DELETE
	FROM   [dbo].[produkty]
	WHERE  [id_produkty] = @id_produkty

	COMMIT
GO
/****** Object:  StoredProcedure [dbo].[usp_produkty_zamowieniaUpdate]    Script Date: 08/09/2011 08:43:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[usp_produkty_zamowieniaUpdate] 
    @id_produkty_zam int,
    @id_zamowienia int,
    @id_produkty int,
    @cena money,
    @ilosc int,
    @suma money
AS 
	SET NOCOUNT ON 
	SET XACT_ABORT ON  
	
	BEGIN TRAN

	UPDATE [dbo].[produkty_zamowienia]
	SET    [id_zamowienia] = @id_zamowienia, [id_produkty] = @id_produkty, [cena] = @cena, [ilosc] = @ilosc, [suma] = @suma
	WHERE  [id_produkty_zam] = @id_produkty_zam
	
	-- Begin Return Select <- do not remove
	SELECT [id_produkty_zam], [id_zamowienia], [id_produkty], [cena], [ilosc], [suma]
	FROM   [dbo].[produkty_zamowienia]
	WHERE  [id_produkty_zam] = @id_produkty_zam	
	-- End Return Select <- do not remove

	COMMIT TRAN
GO
/****** Object:  StoredProcedure [dbo].[usp_produkty_zamowieniaSelect]    Script Date: 08/09/2011 08:43:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[usp_produkty_zamowieniaSelect] 
    @id_produkty_zam INT
AS 
	SET NOCOUNT ON 
	SET XACT_ABORT ON  

	BEGIN TRAN

	SELECT [id_produkty_zam], [id_zamowienia], [id_produkty], [cena], [ilosc], [suma] 
	FROM   [dbo].[produkty_zamowienia] 
	WHERE  ([id_produkty_zam] = @id_produkty_zam OR @id_produkty_zam IS NULL) 

	COMMIT
GO
/****** Object:  StoredProcedure [dbo].[usp_produkty_zamowieniaInsert]    Script Date: 08/09/2011 08:43:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[usp_produkty_zamowieniaInsert] 
    @id_zamowienia int,
    @id_produkty int,
    @cena money,
    @ilosc int,
    @suma money
AS 
	SET NOCOUNT ON 
	SET XACT_ABORT ON  
	
	BEGIN TRAN
	
	INSERT INTO [dbo].[produkty_zamowienia] ([id_zamowienia], [id_produkty], [cena], [ilosc], [suma])
	SELECT @id_zamowienia, @id_produkty, @cena, @ilosc, @suma
	
	-- Begin Return Select <- do not remove
	SELECT [id_produkty_zam], [id_zamowienia], [id_produkty], [cena], [ilosc], [suma]
	FROM   [dbo].[produkty_zamowienia]
	WHERE  [id_produkty_zam] = SCOPE_IDENTITY()
	-- End Return Select <- do not remove
               
	COMMIT
GO
/****** Object:  StoredProcedure [dbo].[usp_produkty_zamowieniaDelete]    Script Date: 08/09/2011 08:43:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[usp_produkty_zamowieniaDelete] 
    @id_produkty_zam int
AS 
	SET NOCOUNT ON 
	SET XACT_ABORT ON  
	
	BEGIN TRAN

	DELETE
	FROM   [dbo].[produkty_zamowienia]
	WHERE  [id_produkty_zam] = @id_produkty_zam

	COMMIT
GO
/****** Object:  StoredProcedure [dbo].[usp_pozycje_zamowieniaUpdate]    Script Date: 08/09/2011 08:43:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[usp_pozycje_zamowieniaUpdate] 
    @id_pozycje_zamowienia int,
    @id_produkty int,
    @cena money,
    @ilosc_zamowiona int,
    @ilosc_przyjeta int,
    @MZZ int,
    @wartosc money
AS 
	SET NOCOUNT ON 
	SET XACT_ABORT ON  
	
	BEGIN TRAN

	UPDATE [dbo].[pozycje_zamowienia]
	SET    [id_pozycje_zamowienia] = @id_pozycje_zamowienia, [id_produkty] = @id_produkty, [cena] = @cena, [ilosc_zamowiona] = @ilosc_zamowiona, [ilosc_przyjeta] = @ilosc_przyjeta, [MZZ] = @MZZ, [wartosc] = @wartosc
	WHERE  [id_pozycje_zamowienia] = @id_pozycje_zamowienia
	
	-- Begin Return Select <- do not remove
	SELECT [id_pozycje_zamowienia], [id_produkty], [cena], [ilosc_zamowiona], [ilosc_przyjeta], [MZZ], [wartosc]
	FROM   [dbo].[pozycje_zamowienia]
	WHERE  [id_pozycje_zamowienia] = @id_pozycje_zamowienia	
	-- End Return Select <- do not remove

	COMMIT TRAN
GO
/****** Object:  StoredProcedure [dbo].[usp_pozycje_zamowieniaSelect]    Script Date: 08/09/2011 08:43:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[usp_pozycje_zamowieniaSelect] 
    @id_pozycje_zamowienia INT
AS 
	SET NOCOUNT ON 
	SET XACT_ABORT ON  

	BEGIN TRAN

	SELECT [id_pozycje_zamowienia], [id_produkty], [cena], [ilosc_zamowiona], [ilosc_przyjeta], [MZZ], [wartosc] 
	FROM   [dbo].[pozycje_zamowienia] 
	WHERE  ([id_pozycje_zamowienia] = @id_pozycje_zamowienia OR @id_pozycje_zamowienia IS NULL) 

	COMMIT
GO
/****** Object:  StoredProcedure [dbo].[usp_pozycje_zamowieniaInsert]    Script Date: 08/09/2011 08:43:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[usp_pozycje_zamowieniaInsert] 
    @id_pozycje_zamowienia int,
    @id_produkty int,
    @cena money,
    @ilosc_zamowiona int,
    @ilosc_przyjeta int,
    @MZZ int,
    @wartosc money
AS 
	SET NOCOUNT ON 
	SET XACT_ABORT ON  
	
	BEGIN TRAN
	
	INSERT INTO [dbo].[pozycje_zamowienia] ([id_pozycje_zamowienia], [id_produkty], [cena], [ilosc_zamowiona], [ilosc_przyjeta], [MZZ], [wartosc])
	SELECT @id_pozycje_zamowienia, @id_produkty, @cena, @ilosc_zamowiona, @ilosc_przyjeta, @MZZ, @wartosc
	
	-- Begin Return Select <- do not remove
	SELECT [id_pozycje_zamowienia], [id_produkty], [cena], [ilosc_zamowiona], [ilosc_przyjeta], [MZZ], [wartosc]
	FROM   [dbo].[pozycje_zamowienia]
	WHERE  [id_pozycje_zamowienia] = @id_pozycje_zamowienia
	-- End Return Select <- do not remove
               
	COMMIT
GO
/****** Object:  StoredProcedure [dbo].[usp_pozycje_zamowieniaDelete]    Script Date: 08/09/2011 08:43:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[usp_pozycje_zamowieniaDelete] 
    @id_pozycje_zamowienia int
AS 
	SET NOCOUNT ON 
	SET XACT_ABORT ON  
	
	BEGIN TRAN

	DELETE
	FROM   [dbo].[pozycje_zamowienia]
	WHERE  [id_pozycje_zamowienia] = @id_pozycje_zamowienia

	COMMIT
GO
/****** Object:  ForeignKey [FK_adres_wojewodztwo]    Script Date: 08/09/2011 08:43:58 ******/
ALTER TABLE [dbo].[adres]  WITH CHECK ADD  CONSTRAINT [FK_adres_wojewodztwo] FOREIGN KEY([id_wojewodztwo])
REFERENCES [dbo].[wojewodztwo] ([id_wojewodztwo])
GO
ALTER TABLE [dbo].[adres] CHECK CONSTRAINT [FK_adres_wojewodztwo]
GO
/****** Object:  ForeignKey [FK_FV_zakupu_kontrahenci]    Script Date: 08/09/2011 08:43:58 ******/
ALTER TABLE [dbo].[FV_zakupu]  WITH CHECK ADD  CONSTRAINT [FK_FV_zakupu_kontrahenci] FOREIGN KEY([id_kontrahenci])
REFERENCES [dbo].[kontrahenci] ([id_kontrahenci])
GO
ALTER TABLE [dbo].[FV_zakupu] CHECK CONSTRAINT [FK_FV_zakupu_kontrahenci]
GO
/****** Object:  ForeignKey [FK_FV_zakupu_MZZ]    Script Date: 08/09/2011 08:43:58 ******/
ALTER TABLE [dbo].[FV_zakupu]  WITH CHECK ADD  CONSTRAINT [FK_FV_zakupu_MZZ] FOREIGN KEY([MZZ])
REFERENCES [dbo].[MZZ] ([MZZ])
GO
ALTER TABLE [dbo].[FV_zakupu] CHECK CONSTRAINT [FK_FV_zakupu_MZZ]
GO
/****** Object:  ForeignKey [FK_kontrahenci_adres]    Script Date: 08/09/2011 08:43:58 ******/
ALTER TABLE [dbo].[kontrahenci]  WITH CHECK ADD  CONSTRAINT [FK_kontrahenci_adres] FOREIGN KEY([id_adres_wysylki])
REFERENCES [dbo].[adres] ([id_adres])
GO
ALTER TABLE [dbo].[kontrahenci] CHECK CONSTRAINT [FK_kontrahenci_adres]
GO
/****** Object:  ForeignKey [FK_kontrahenci_adres1]    Script Date: 08/09/2011 08:43:58 ******/
ALTER TABLE [dbo].[kontrahenci]  WITH CHECK ADD  CONSTRAINT [FK_kontrahenci_adres1] FOREIGN KEY([id_adres_fv])
REFERENCES [dbo].[adres] ([id_adres])
GO
ALTER TABLE [dbo].[kontrahenci] CHECK CONSTRAINT [FK_kontrahenci_adres1]
GO
/****** Object:  ForeignKey [FK_kontrahenci_osoba_kontaktowa]    Script Date: 08/09/2011 08:43:58 ******/
ALTER TABLE [dbo].[kontrahenci]  WITH CHECK ADD  CONSTRAINT [FK_kontrahenci_osoba_kontaktowa] FOREIGN KEY([id_osoba_kontaktowa])
REFERENCES [dbo].[osoba_kontaktowa] ([id_osoba_kontaktowa])
GO
ALTER TABLE [dbo].[kontrahenci] CHECK CONSTRAINT [FK_kontrahenci_osoba_kontaktowa]
GO
/****** Object:  ForeignKey [FK_kontrahenci_pracownik]    Script Date: 08/09/2011 08:43:58 ******/
ALTER TABLE [dbo].[kontrahenci]  WITH CHECK ADD  CONSTRAINT [FK_kontrahenci_pracownik] FOREIGN KEY([id_pracownika])
REFERENCES [dbo].[pracownik] ([id_pracownika])
GO
ALTER TABLE [dbo].[kontrahenci] CHECK CONSTRAINT [FK_kontrahenci_pracownik]
GO
/****** Object:  ForeignKey [FK_MZZ_kontrahenci]    Script Date: 08/09/2011 08:43:58 ******/
ALTER TABLE [dbo].[MZZ]  WITH CHECK ADD  CONSTRAINT [FK_MZZ_kontrahenci] FOREIGN KEY([id_kontrahenci])
REFERENCES [dbo].[kontrahenci] ([id_kontrahenci])
GO
ALTER TABLE [dbo].[MZZ] CHECK CONSTRAINT [FK_MZZ_kontrahenci]
GO
/****** Object:  ForeignKey [FK_MZZ_pracownik]    Script Date: 08/09/2011 08:43:58 ******/
ALTER TABLE [dbo].[MZZ]  WITH CHECK ADD  CONSTRAINT [FK_MZZ_pracownik] FOREIGN KEY([id_pracownika])
REFERENCES [dbo].[pracownik] ([id_pracownika])
GO
ALTER TABLE [dbo].[MZZ] CHECK CONSTRAINT [FK_MZZ_pracownik]
GO
/****** Object:  ForeignKey [FK_pozycje_zamowienia_MZZ]    Script Date: 08/09/2011 08:43:58 ******/
ALTER TABLE [dbo].[pozycje_zamowienia]  WITH CHECK ADD  CONSTRAINT [FK_pozycje_zamowienia_MZZ] FOREIGN KEY([MZZ])
REFERENCES [dbo].[MZZ] ([MZZ])
GO
ALTER TABLE [dbo].[pozycje_zamowienia] CHECK CONSTRAINT [FK_pozycje_zamowienia_MZZ]
GO
/****** Object:  ForeignKey [FK_pozycje_zamowienia_produkty]    Script Date: 08/09/2011 08:43:58 ******/
ALTER TABLE [dbo].[pozycje_zamowienia]  WITH CHECK ADD  CONSTRAINT [FK_pozycje_zamowienia_produkty] FOREIGN KEY([id_produkty])
REFERENCES [dbo].[produkty] ([id_produkty])
GO
ALTER TABLE [dbo].[pozycje_zamowienia] CHECK CONSTRAINT [FK_pozycje_zamowienia_produkty]
GO
/****** Object:  ForeignKey [FK_pracownik_adres]    Script Date: 08/09/2011 08:43:58 ******/
ALTER TABLE [dbo].[pracownik]  WITH CHECK ADD  CONSTRAINT [FK_pracownik_adres] FOREIGN KEY([id_adres])
REFERENCES [dbo].[adres] ([id_adres])
GO
ALTER TABLE [dbo].[pracownik] CHECK CONSTRAINT [FK_pracownik_adres]
GO
/****** Object:  ForeignKey [FK_producent_kontrahenci]    Script Date: 08/09/2011 08:43:58 ******/
ALTER TABLE [dbo].[producent]  WITH CHECK ADD  CONSTRAINT [FK_producent_kontrahenci] FOREIGN KEY([id_kontrahenci])
REFERENCES [dbo].[kontrahenci] ([id_kontrahenci])
GO
ALTER TABLE [dbo].[producent] CHECK CONSTRAINT [FK_producent_kontrahenci]
GO
/****** Object:  ForeignKey [FK_producent_pracownik]    Script Date: 08/09/2011 08:43:58 ******/
ALTER TABLE [dbo].[producent]  WITH CHECK ADD  CONSTRAINT [FK_producent_pracownik] FOREIGN KEY([id_pracownika])
REFERENCES [dbo].[pracownik] ([id_pracownika])
GO
ALTER TABLE [dbo].[producent] CHECK CONSTRAINT [FK_producent_pracownik]
GO
/****** Object:  ForeignKey [FK_produkty_kategoria]    Script Date: 08/09/2011 08:43:58 ******/
ALTER TABLE [dbo].[produkty]  WITH CHECK ADD  CONSTRAINT [FK_produkty_kategoria] FOREIGN KEY([id_kategoria])
REFERENCES [dbo].[kategoria] ([id_kategoria])
GO
ALTER TABLE [dbo].[produkty] CHECK CONSTRAINT [FK_produkty_kategoria]
GO
/****** Object:  ForeignKey [FK_produkty_producent]    Script Date: 08/09/2011 08:43:58 ******/
ALTER TABLE [dbo].[produkty]  WITH CHECK ADD  CONSTRAINT [FK_produkty_producent] FOREIGN KEY([id_producent])
REFERENCES [dbo].[producent] ([id_producent])
GO
ALTER TABLE [dbo].[produkty] CHECK CONSTRAINT [FK_produkty_producent]
GO
/****** Object:  ForeignKey [FK_produkty_promocja]    Script Date: 08/09/2011 08:43:58 ******/
ALTER TABLE [dbo].[produkty]  WITH CHECK ADD  CONSTRAINT [FK_produkty_promocja] FOREIGN KEY([id_promocja])
REFERENCES [dbo].[promocja] ([id_promocji])
GO
ALTER TABLE [dbo].[produkty] CHECK CONSTRAINT [FK_produkty_promocja]
GO
/****** Object:  ForeignKey [FK_produkty_zamowienia_produkty]    Script Date: 08/09/2011 08:43:58 ******/
ALTER TABLE [dbo].[produkty_zamowienia]  WITH CHECK ADD  CONSTRAINT [FK_produkty_zamowienia_produkty] FOREIGN KEY([id_produkty])
REFERENCES [dbo].[produkty] ([id_produkty])
GO
ALTER TABLE [dbo].[produkty_zamowienia] CHECK CONSTRAINT [FK_produkty_zamowienia_produkty]
GO
/****** Object:  ForeignKey [FK_produkty_zamowienia_zamowienia]    Script Date: 08/09/2011 08:43:58 ******/
ALTER TABLE [dbo].[produkty_zamowienia]  WITH CHECK ADD  CONSTRAINT [FK_produkty_zamowienia_zamowienia] FOREIGN KEY([id_zamowienia])
REFERENCES [dbo].[zamowienia] ([id_zamowienia])
GO
ALTER TABLE [dbo].[produkty_zamowienia] CHECK CONSTRAINT [FK_produkty_zamowienia_zamowienia]
GO
/****** Object:  ForeignKey [FK_sprzedaz_kontrahenci]    Script Date: 08/09/2011 08:43:58 ******/
ALTER TABLE [dbo].[sprzedaz]  WITH CHECK ADD  CONSTRAINT [FK_sprzedaz_kontrahenci] FOREIGN KEY([id_kontrahenci])
REFERENCES [dbo].[kontrahenci] ([id_kontrahenci])
GO
ALTER TABLE [dbo].[sprzedaz] CHECK CONSTRAINT [FK_sprzedaz_kontrahenci]
GO
/****** Object:  ForeignKey [FK_sprzedaz_pracownik]    Script Date: 08/09/2011 08:43:58 ******/
ALTER TABLE [dbo].[sprzedaz]  WITH CHECK ADD  CONSTRAINT [FK_sprzedaz_pracownik] FOREIGN KEY([id_pracownika])
REFERENCES [dbo].[pracownik] ([id_pracownika])
GO
ALTER TABLE [dbo].[sprzedaz] CHECK CONSTRAINT [FK_sprzedaz_pracownik]
GO
/****** Object:  ForeignKey [FK_sprzedaz_zamowienia]    Script Date: 08/09/2011 08:43:58 ******/
ALTER TABLE [dbo].[sprzedaz]  WITH CHECK ADD  CONSTRAINT [FK_sprzedaz_zamowienia] FOREIGN KEY([id_zamowienia])
REFERENCES [dbo].[zamowienia] ([id_zamowienia])
GO
ALTER TABLE [dbo].[sprzedaz] CHECK CONSTRAINT [FK_sprzedaz_zamowienia]
GO
