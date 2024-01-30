
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, and Azure
-- --------------------------------------------------
-- Date Created: 08/24/2011 18:48:54
-- Generated from EDMX file: C:\Users\katarzyna_lapek\Documents\Visual Studio 2010\Projects\KBSystemSale\KBSystemSale\Model.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [hurtownia];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[FK_adres_wojewodztwo]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[adres] DROP CONSTRAINT [FK_adres_wojewodztwo];
GO
IF OBJECT_ID(N'[dbo].[FK_kontrahenci_adres]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[kontrahenci] DROP CONSTRAINT [FK_kontrahenci_adres];
GO
IF OBJECT_ID(N'[dbo].[FK_kontrahenci_adres1]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[kontrahenci] DROP CONSTRAINT [FK_kontrahenci_adres1];
GO
IF OBJECT_ID(N'[dbo].[FK_pracownik_adres]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[pracownik] DROP CONSTRAINT [FK_pracownik_adres];
GO
IF OBJECT_ID(N'[dbo].[FK_FV_zakupu_kontrahenci]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[FV_zakupu] DROP CONSTRAINT [FK_FV_zakupu_kontrahenci];
GO
IF OBJECT_ID(N'[dbo].[FK_FV_zakupu_MZZ]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[FV_zakupu] DROP CONSTRAINT [FK_FV_zakupu_MZZ];
GO
IF OBJECT_ID(N'[dbo].[FK_produkty_kategoria]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[produkty] DROP CONSTRAINT [FK_produkty_kategoria];
GO
IF OBJECT_ID(N'[dbo].[FK_kontrahenci_osoba_kontaktowa]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[kontrahenci] DROP CONSTRAINT [FK_kontrahenci_osoba_kontaktowa];
GO
IF OBJECT_ID(N'[dbo].[FK_kontrahenci_pracownik]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[kontrahenci] DROP CONSTRAINT [FK_kontrahenci_pracownik];
GO
IF OBJECT_ID(N'[dbo].[FK_MZZ_kontrahenci]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[MZZ] DROP CONSTRAINT [FK_MZZ_kontrahenci];
GO
IF OBJECT_ID(N'[dbo].[FK_producent_kontrahenci]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[producent] DROP CONSTRAINT [FK_producent_kontrahenci];
GO
IF OBJECT_ID(N'[dbo].[FK_sprzedaz_kontrahenci]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[sprzedaz] DROP CONSTRAINT [FK_sprzedaz_kontrahenci];
GO
IF OBJECT_ID(N'[dbo].[FK_sprzedaz_kontrahenci1]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[sprzedaz] DROP CONSTRAINT [FK_sprzedaz_kontrahenci1];
GO
IF OBJECT_ID(N'[dbo].[FK_pozycje_zamowienia_MZZ]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[pozycje_zamowienia] DROP CONSTRAINT [FK_pozycje_zamowienia_MZZ];
GO
IF OBJECT_ID(N'[dbo].[FK_pozycje_zamowienia_produkty]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[pozycje_zamowienia] DROP CONSTRAINT [FK_pozycje_zamowienia_produkty];
GO
IF OBJECT_ID(N'[dbo].[FK_producent_pracownik]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[producent] DROP CONSTRAINT [FK_producent_pracownik];
GO
IF OBJECT_ID(N'[dbo].[FK_sprzedaz_pracownik]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[sprzedaz] DROP CONSTRAINT [FK_sprzedaz_pracownik];
GO
IF OBJECT_ID(N'[dbo].[FK_produkty_producent]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[produkty] DROP CONSTRAINT [FK_produkty_producent];
GO
IF OBJECT_ID(N'[dbo].[FK_produkty_promocja]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[produkty] DROP CONSTRAINT [FK_produkty_promocja];
GO
IF OBJECT_ID(N'[dbo].[FK_produkty_zamowienia_produkty]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[produkty_zamowienia] DROP CONSTRAINT [FK_produkty_zamowienia_produkty];
GO
IF OBJECT_ID(N'[dbo].[FK_produkty_zamowienia_zamowienia]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[produkty_zamowienia] DROP CONSTRAINT [FK_produkty_zamowienia_zamowienia];
GO
IF OBJECT_ID(N'[dbo].[FK_sprzedaz_zamowienia]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[sprzedaz] DROP CONSTRAINT [FK_sprzedaz_zamowienia];
GO

-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[adres]', 'U') IS NOT NULL
    DROP TABLE [dbo].[adres];
GO
IF OBJECT_ID(N'[dbo].[FV_zakupu]', 'U') IS NOT NULL
    DROP TABLE [dbo].[FV_zakupu];
GO
IF OBJECT_ID(N'[dbo].[kategoria]', 'U') IS NOT NULL
    DROP TABLE [dbo].[kategoria];
GO
IF OBJECT_ID(N'[dbo].[kontrahenci]', 'U') IS NOT NULL
    DROP TABLE [dbo].[kontrahenci];
GO
IF OBJECT_ID(N'[dbo].[MZZ]', 'U') IS NOT NULL
    DROP TABLE [dbo].[MZZ];
GO
IF OBJECT_ID(N'[dbo].[osoba_kontaktowa]', 'U') IS NOT NULL
    DROP TABLE [dbo].[osoba_kontaktowa];
GO
IF OBJECT_ID(N'[dbo].[pozycje_zamowienia]', 'U') IS NOT NULL
    DROP TABLE [dbo].[pozycje_zamowienia];
GO
IF OBJECT_ID(N'[dbo].[pracownik]', 'U') IS NOT NULL
    DROP TABLE [dbo].[pracownik];
GO
IF OBJECT_ID(N'[dbo].[producent]', 'U') IS NOT NULL
    DROP TABLE [dbo].[producent];
GO
IF OBJECT_ID(N'[dbo].[produkty]', 'U') IS NOT NULL
    DROP TABLE [dbo].[produkty];
GO
IF OBJECT_ID(N'[dbo].[produkty_zamowienia]', 'U') IS NOT NULL
    DROP TABLE [dbo].[produkty_zamowienia];
GO
IF OBJECT_ID(N'[dbo].[promocja]', 'U') IS NOT NULL
    DROP TABLE [dbo].[promocja];
GO
IF OBJECT_ID(N'[dbo].[sprzedaz]', 'U') IS NOT NULL
    DROP TABLE [dbo].[sprzedaz];
GO
IF OBJECT_ID(N'[dbo].[wojewodztwo]', 'U') IS NOT NULL
    DROP TABLE [dbo].[wojewodztwo];
GO
IF OBJECT_ID(N'[dbo].[zamowienia]', 'U') IS NOT NULL
    DROP TABLE [dbo].[zamowienia];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'adres'
CREATE TABLE [dbo].[adres] (
    [id_adres] int IDENTITY(1,1) NOT NULL,
    [panstwo] nchar(20)  NOT NULL,
    [miasto] nchar(20)  NOT NULL,
    [ulica] nchar(30)  NOT NULL,
    [kod_pocztowy] nchar(6)  NOT NULL,
    [nr_domu] nchar(10)  NOT NULL,
    [nr_mieszkania] nchar(10)  NOT NULL,
    [id_wojewodztwo] int  NOT NULL
);
GO

-- Creating table 'FV_zakupu'
CREATE TABLE [dbo].[FV_zakupu] (
    [NR_fv] nvarchar(15)  NOT NULL,
    [data_wystawienia_fv] datetime  NOT NULL,
    [data_przyjecia] datetime  NOT NULL,
    [id_kontrahenci] int  NOT NULL,
    [termin_zaplaty] int  NOT NULL,
    [MZZ] int  NOT NULL,
    [zaplacono] binary(1)  NOT NULL
);
GO

-- Creating table 'kategoria'
CREATE TABLE [dbo].[kategoria] (
    [id_kategoria] int IDENTITY(1,1) NOT NULL,
    [nazwa] nvarchar(50)  NOT NULL
);
GO

-- Creating table 'kontrahenci'
CREATE TABLE [dbo].[kontrahenci] (
    [id_kontrahenci] int IDENTITY(1,1) NOT NULL,
    [Nazwa] nvarchar(30)  NOT NULL,
    [id_adres_wysylki] int  NOT NULL,
    [id_adres_fv] int  NOT NULL,
    [nip] nvarchar(10)  NOT NULL,
    [regon] int  NOT NULL,
    [id_osoba_kontaktowa] int  NOT NULL,
    [id_pracownika] nvarchar(7)  NOT NULL,
    [umowa] bit  NOT NULL,
    [data_podpisania_umowy] datetime  NOT NULL,
    [kredyt_kupiecki] decimal(1,0) NOT NULL,
    [wartosc_kredytu] decimal(19,4)  NULL
);
GO

-- Creating table 'MZZ'
CREATE TABLE [dbo].[MZZ] (
    [MZZ1] int  NOT NULL,
    [data_zamowienia] datetime  NOT NULL,
    [id_kontrahenci] int  NOT NULL
);
GO

-- Creating table 'osoba_kontaktowa'
CREATE TABLE [dbo].[osoba_kontaktowa] (
    [id_osoba_kontaktowa] int IDENTITY(1,1) NOT NULL,
    [imie] nvarchar(30)  NOT NULL,
    [nazwisko] nvarchar(30)  NOT NULL,
    [tel] nvarchar(15)  NOT NULL,
    [mail] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'pozycje_zamowienia'
CREATE TABLE [dbo].[pozycje_zamowienia] (
    [id_pozycje_zamowienia] int  NOT NULL,
    [id_produkty] int  NOT NULL,
    [cena] decimal(19,4)  NULL,
    [ilosc_zamowiona] int  NOT NULL,
    [ilosc_przyjeta] int  NULL,
    [MZZ] int  NOT NULL,
    [wartosc] decimal(19,4)  NULL
);
GO

-- Creating table 'pracownik'
CREATE TABLE [dbo].[pracownik] (
    [id_pracownika] nvarchar(7)  NOT NULL,
    [imie] nchar(30)  NOT NULL,
    [nazwisko] nchar(30)  NOT NULL,
    [pesel] char(11)  NOT NULL,
    [id_adres] int  NOT NULL,
    [wyplata] decimal(19,4)  NOT NULL,
    [pracuje] bit  NOT NULL
);
GO

-- Creating table 'producent'
CREATE TABLE [dbo].[producent] (
    [id_producent] int IDENTITY(1,1) NOT NULL,
    [id_kontrahenci] int  NOT NULL,
    [id_pracownika] nvarchar(7)  NOT NULL,
    [logo] bit  NOT NULL,
    [strona_www] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'produkty'
CREATE TABLE [dbo].[produkty] (
    [id_produkty] int IDENTITY(1,1) NOT NULL,
    [id_producent] int  NOT NULL,
    [model] nchar(30)  NOT NULL,
    [opis] varchar(max)  NULL,
    [cena_katalogowa] decimal(19,4)  NOT NULL,
    [stan_magazynowy] int  NOT NULL,
    [id_kategoria] int  NOT NULL,
    [PN] nvarchar(20)  NOT NULL,
    [aktywne] bit  NULL,
    [id_promocja] int  NOT NULL
);
GO

-- Creating table 'produkty_zamowienia'
CREATE TABLE [dbo].[produkty_zamowienia] (
    [id_produkty_zam] int IDENTITY(1,1) NOT NULL,
    [id_zamowienia] int  NOT NULL,
    [id_produkty] int  NOT NULL,
    [cena] decimal(19,4)  NOT NULL,
    [ilosc] int  NOT NULL,
    [suma] decimal(19,4)  NOT NULL
);
GO

-- Creating table 'promocja'
CREATE TABLE [dbo].[promocja] (
    [id_promocji] int IDENTITY(1,1) NOT NULL,
    [odnizka] decimal(19,4)  NOT NULL,
    [data_rozpoczecia] datetime  NOT NULL,
    [data_zakonczenia] datetime  NOT NULL
);
GO

-- Creating table 'sprzedaz'
CREATE TABLE [dbo].[sprzedaz] (
    [id_sprzedaz] int IDENTITY(1,1) NOT NULL,
    [id_zamowienia] int  NOT NULL,
    [id_pracownika] nvarchar(7)  NOT NULL,
    [id_kontrahenci] int  NOT NULL,
    [id_kontrahent_adres] int  NOT NULL
);
GO

-- Creating table 'wojewodztwo'
CREATE TABLE [dbo].[wojewodztwo] (
    [id_wojewodztwo] int IDENTITY(1,1) NOT NULL,
    [nazwa] nchar(30)  NOT NULL
);
GO

-- Creating table 'zamowienia'
CREATE TABLE [dbo].[zamowienia] (
    [id_zamowienia] int IDENTITY(1,1) NOT NULL,
    [data_wystawienia] datetime  NOT NULL,
    [data_realizacji] datetime  NOT NULL,
    [wartosc_brutto] decimal(19,4)  NOT NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [id_adres] in table 'adres'
ALTER TABLE [dbo].[adres]
ADD CONSTRAINT [PK_adres]
    PRIMARY KEY CLUSTERED ([id_adres] ASC);
GO

-- Creating primary key on [NR_fv] in table 'FV_zakupu'
ALTER TABLE [dbo].[FV_zakupu]
ADD CONSTRAINT [PK_FV_zakupu]
    PRIMARY KEY CLUSTERED ([NR_fv] ASC);
GO

-- Creating primary key on [id_kategoria] in table 'kategoria'
ALTER TABLE [dbo].[kategoria]
ADD CONSTRAINT [PK_kategoria]
    PRIMARY KEY CLUSTERED ([id_kategoria] ASC);
GO

-- Creating primary key on [id_kontrahenci] in table 'kontrahenci'
ALTER TABLE [dbo].[kontrahenci]
ADD CONSTRAINT [PK_kontrahenci]
    PRIMARY KEY CLUSTERED ([id_kontrahenci] ASC);
GO

-- Creating primary key on [MZZ1] in table 'MZZ'
ALTER TABLE [dbo].[MZZ]
ADD CONSTRAINT [PK_MZZ]
    PRIMARY KEY CLUSTERED ([MZZ1] ASC);
GO

-- Creating primary key on [id_osoba_kontaktowa] in table 'osoba_kontaktowa'
ALTER TABLE [dbo].[osoba_kontaktowa]
ADD CONSTRAINT [PK_osoba_kontaktowa]
    PRIMARY KEY CLUSTERED ([id_osoba_kontaktowa] ASC);
GO

-- Creating primary key on [id_pozycje_zamowienia] in table 'pozycje_zamowienia'
ALTER TABLE [dbo].[pozycje_zamowienia]
ADD CONSTRAINT [PK_pozycje_zamowienia]
    PRIMARY KEY CLUSTERED ([id_pozycje_zamowienia] ASC);
GO

-- Creating primary key on [id_pracownika] in table 'pracownik'
ALTER TABLE [dbo].[pracownik]
ADD CONSTRAINT [PK_pracownik]
    PRIMARY KEY CLUSTERED ([id_pracownika] ASC);
GO

-- Creating primary key on [id_producent] in table 'producent'
ALTER TABLE [dbo].[producent]
ADD CONSTRAINT [PK_producent]
    PRIMARY KEY CLUSTERED ([id_producent] ASC);
GO

-- Creating primary key on [id_produkty] in table 'produkty'
ALTER TABLE [dbo].[produkty]
ADD CONSTRAINT [PK_produkty]
    PRIMARY KEY CLUSTERED ([id_produkty] ASC);
GO

-- Creating primary key on [id_produkty_zam] in table 'produkty_zamowienia'
ALTER TABLE [dbo].[produkty_zamowienia]
ADD CONSTRAINT [PK_produkty_zamowienia]
    PRIMARY KEY CLUSTERED ([id_produkty_zam] ASC);
GO

-- Creating primary key on [id_promocji] in table 'promocja'
ALTER TABLE [dbo].[promocja]
ADD CONSTRAINT [PK_promocja]
    PRIMARY KEY CLUSTERED ([id_promocji] ASC);
GO

-- Creating primary key on [id_sprzedaz] in table 'sprzedaz'
ALTER TABLE [dbo].[sprzedaz]
ADD CONSTRAINT [PK_sprzedaz]
    PRIMARY KEY CLUSTERED ([id_sprzedaz] ASC);
GO

-- Creating primary key on [id_wojewodztwo] in table 'wojewodztwo'
ALTER TABLE [dbo].[wojewodztwo]
ADD CONSTRAINT [PK_wojewodztwo]
    PRIMARY KEY CLUSTERED ([id_wojewodztwo] ASC);
GO

-- Creating primary key on [id_zamowienia] in table 'zamowienia'
ALTER TABLE [dbo].[zamowienia]
ADD CONSTRAINT [PK_zamowienia]
    PRIMARY KEY CLUSTERED ([id_zamowienia] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [id_wojewodztwo] in table 'adres'
ALTER TABLE [dbo].[adres]
ADD CONSTRAINT [FK_adres_wojewodztwo]
    FOREIGN KEY ([id_wojewodztwo])
    REFERENCES [dbo].[wojewodztwo]
        ([id_wojewodztwo])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_adres_wojewodztwo'
CREATE INDEX [IX_FK_adres_wojewodztwo]
ON [dbo].[adres]
    ([id_wojewodztwo]);
GO

-- Creating foreign key on [id_adres_wysylki] in table 'kontrahenci'
ALTER TABLE [dbo].[kontrahenci]
ADD CONSTRAINT [FK_kontrahenci_adres]
    FOREIGN KEY ([id_adres_wysylki])
    REFERENCES [dbo].[adres]
        ([id_adres])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_kontrahenci_adres'
CREATE INDEX [IX_FK_kontrahenci_adres]
ON [dbo].[kontrahenci]
    ([id_adres_wysylki]);
GO

-- Creating foreign key on [id_adres_fv] in table 'kontrahenci'
ALTER TABLE [dbo].[kontrahenci]
ADD CONSTRAINT [FK_kontrahenci_adres1]
    FOREIGN KEY ([id_adres_fv])
    REFERENCES [dbo].[adres]
        ([id_adres])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_kontrahenci_adres1'
CREATE INDEX [IX_FK_kontrahenci_adres1]
ON [dbo].[kontrahenci]
    ([id_adres_fv]);
GO

-- Creating foreign key on [id_adres] in table 'pracownik'
ALTER TABLE [dbo].[pracownik]
ADD CONSTRAINT [FK_pracownik_adres]
    FOREIGN KEY ([id_adres])
    REFERENCES [dbo].[adres]
        ([id_adres])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_pracownik_adres'
CREATE INDEX [IX_FK_pracownik_adres]
ON [dbo].[pracownik]
    ([id_adres]);
GO

-- Creating foreign key on [id_kontrahenci] in table 'FV_zakupu'
ALTER TABLE [dbo].[FV_zakupu]
ADD CONSTRAINT [FK_FV_zakupu_kontrahenci]
    FOREIGN KEY ([id_kontrahenci])
    REFERENCES [dbo].[kontrahenci]
        ([id_kontrahenci])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_FV_zakupu_kontrahenci'
CREATE INDEX [IX_FK_FV_zakupu_kontrahenci]
ON [dbo].[FV_zakupu]
    ([id_kontrahenci]);
GO

-- Creating foreign key on [MZZ] in table 'FV_zakupu'
ALTER TABLE [dbo].[FV_zakupu]
ADD CONSTRAINT [FK_FV_zakupu_MZZ]
    FOREIGN KEY ([MZZ])
    REFERENCES [dbo].[MZZ]
        ([MZZ1])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_FV_zakupu_MZZ'
CREATE INDEX [IX_FK_FV_zakupu_MZZ]
ON [dbo].[FV_zakupu]
    ([MZZ]);
GO

-- Creating foreign key on [id_kategoria] in table 'produkty'
ALTER TABLE [dbo].[produkty]
ADD CONSTRAINT [FK_produkty_kategoria]
    FOREIGN KEY ([id_kategoria])
    REFERENCES [dbo].[kategoria]
        ([id_kategoria])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_produkty_kategoria'
CREATE INDEX [IX_FK_produkty_kategoria]
ON [dbo].[produkty]
    ([id_kategoria]);
GO

-- Creating foreign key on [id_osoba_kontaktowa] in table 'kontrahenci'
ALTER TABLE [dbo].[kontrahenci]
ADD CONSTRAINT [FK_kontrahenci_osoba_kontaktowa]
    FOREIGN KEY ([id_osoba_kontaktowa])
    REFERENCES [dbo].[osoba_kontaktowa]
        ([id_osoba_kontaktowa])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_kontrahenci_osoba_kontaktowa'
CREATE INDEX [IX_FK_kontrahenci_osoba_kontaktowa]
ON [dbo].[kontrahenci]
    ([id_osoba_kontaktowa]);
GO

-- Creating foreign key on [id_pracownika] in table 'kontrahenci'
ALTER TABLE [dbo].[kontrahenci]
ADD CONSTRAINT [FK_kontrahenci_pracownik]
    FOREIGN KEY ([id_pracownika])
    REFERENCES [dbo].[pracownik]
        ([id_pracownika])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_kontrahenci_pracownik'
CREATE INDEX [IX_FK_kontrahenci_pracownik]
ON [dbo].[kontrahenci]
    ([id_pracownika]);
GO

-- Creating foreign key on [id_kontrahenci] in table 'MZZ'
ALTER TABLE [dbo].[MZZ]
ADD CONSTRAINT [FK_MZZ_kontrahenci]
    FOREIGN KEY ([id_kontrahenci])
    REFERENCES [dbo].[kontrahenci]
        ([id_kontrahenci])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_MZZ_kontrahenci'
CREATE INDEX [IX_FK_MZZ_kontrahenci]
ON [dbo].[MZZ]
    ([id_kontrahenci]);
GO

-- Creating foreign key on [id_kontrahenci] in table 'producent'
ALTER TABLE [dbo].[producent]
ADD CONSTRAINT [FK_producent_kontrahenci]
    FOREIGN KEY ([id_kontrahenci])
    REFERENCES [dbo].[kontrahenci]
        ([id_kontrahenci])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_producent_kontrahenci'
CREATE INDEX [IX_FK_producent_kontrahenci]
ON [dbo].[producent]
    ([id_kontrahenci]);
GO

-- Creating foreign key on [id_kontrahenci] in table 'sprzedaz'
ALTER TABLE [dbo].[sprzedaz]
ADD CONSTRAINT [FK_sprzedaz_kontrahenci]
    FOREIGN KEY ([id_kontrahenci])
    REFERENCES [dbo].[kontrahenci]
        ([id_kontrahenci])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_sprzedaz_kontrahenci'
CREATE INDEX [IX_FK_sprzedaz_kontrahenci]
ON [dbo].[sprzedaz]
    ([id_kontrahenci]);
GO

-- Creating foreign key on [id_kontrahent_adres] in table 'sprzedaz'
ALTER TABLE [dbo].[sprzedaz]
ADD CONSTRAINT [FK_sprzedaz_kontrahenci1]
    FOREIGN KEY ([id_kontrahent_adres])
    REFERENCES [dbo].[kontrahenci]
        ([id_kontrahenci])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_sprzedaz_kontrahenci1'
CREATE INDEX [IX_FK_sprzedaz_kontrahenci1]
ON [dbo].[sprzedaz]
    ([id_kontrahent_adres]);
GO

-- Creating foreign key on [MZZ] in table 'pozycje_zamowienia'
ALTER TABLE [dbo].[pozycje_zamowienia]
ADD CONSTRAINT [FK_pozycje_zamowienia_MZZ]
    FOREIGN KEY ([MZZ])
    REFERENCES [dbo].[MZZ]
        ([MZZ1])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_pozycje_zamowienia_MZZ'
CREATE INDEX [IX_FK_pozycje_zamowienia_MZZ]
ON [dbo].[pozycje_zamowienia]
    ([MZZ]);
GO

-- Creating foreign key on [id_produkty] in table 'pozycje_zamowienia'
ALTER TABLE [dbo].[pozycje_zamowienia]
ADD CONSTRAINT [FK_pozycje_zamowienia_produkty]
    FOREIGN KEY ([id_produkty])
    REFERENCES [dbo].[produkty]
        ([id_produkty])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_pozycje_zamowienia_produkty'
CREATE INDEX [IX_FK_pozycje_zamowienia_produkty]
ON [dbo].[pozycje_zamowienia]
    ([id_produkty]);
GO

-- Creating foreign key on [id_pracownika] in table 'producent'
ALTER TABLE [dbo].[producent]
ADD CONSTRAINT [FK_producent_pracownik]
    FOREIGN KEY ([id_pracownika])
    REFERENCES [dbo].[pracownik]
        ([id_pracownika])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_producent_pracownik'
CREATE INDEX [IX_FK_producent_pracownik]
ON [dbo].[producent]
    ([id_pracownika]);
GO

-- Creating foreign key on [id_pracownika] in table 'sprzedaz'
ALTER TABLE [dbo].[sprzedaz]
ADD CONSTRAINT [FK_sprzedaz_pracownik]
    FOREIGN KEY ([id_pracownika])
    REFERENCES [dbo].[pracownik]
        ([id_pracownika])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_sprzedaz_pracownik'
CREATE INDEX [IX_FK_sprzedaz_pracownik]
ON [dbo].[sprzedaz]
    ([id_pracownika]);
GO

-- Creating foreign key on [id_producent] in table 'produkty'
ALTER TABLE [dbo].[produkty]
ADD CONSTRAINT [FK_produkty_producent]
    FOREIGN KEY ([id_producent])
    REFERENCES [dbo].[producent]
        ([id_producent])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_produkty_producent'
CREATE INDEX [IX_FK_produkty_producent]
ON [dbo].[produkty]
    ([id_producent]);
GO

-- Creating foreign key on [id_promocja] in table 'produkty'
ALTER TABLE [dbo].[produkty]
ADD CONSTRAINT [FK_produkty_promocja]
    FOREIGN KEY ([id_promocja])
    REFERENCES [dbo].[promocja]
        ([id_promocji])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_produkty_promocja'
CREATE INDEX [IX_FK_produkty_promocja]
ON [dbo].[produkty]
    ([id_promocja]);
GO

-- Creating foreign key on [id_produkty] in table 'produkty_zamowienia'
ALTER TABLE [dbo].[produkty_zamowienia]
ADD CONSTRAINT [FK_produkty_zamowienia_produkty]
    FOREIGN KEY ([id_produkty])
    REFERENCES [dbo].[produkty]
        ([id_produkty])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_produkty_zamowienia_produkty'
CREATE INDEX [IX_FK_produkty_zamowienia_produkty]
ON [dbo].[produkty_zamowienia]
    ([id_produkty]);
GO

-- Creating foreign key on [id_zamowienia] in table 'produkty_zamowienia'
ALTER TABLE [dbo].[produkty_zamowienia]
ADD CONSTRAINT [FK_produkty_zamowienia_zamowienia]
    FOREIGN KEY ([id_zamowienia])
    REFERENCES [dbo].[zamowienia]
        ([id_zamowienia])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_produkty_zamowienia_zamowienia'
CREATE INDEX [IX_FK_produkty_zamowienia_zamowienia]
ON [dbo].[produkty_zamowienia]
    ([id_zamowienia]);
GO

-- Creating foreign key on [id_zamowienia] in table 'sprzedaz'
ALTER TABLE [dbo].[sprzedaz]
ADD CONSTRAINT [FK_sprzedaz_zamowienia]
    FOREIGN KEY ([id_zamowienia])
    REFERENCES [dbo].[zamowienia]
        ([id_zamowienia])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_sprzedaz_zamowienia'
CREATE INDEX [IX_FK_sprzedaz_zamowienia]
ON [dbo].[sprzedaz]
    ([id_zamowienia]);
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------