
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 07/09/2015 14:37:26
-- Generated from EDMX file: C:\KAY\MADAGASCAR\Developement\MADAGASCAR\Omni.Agence.DAL\Model.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [Madagascar];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[FK_Augementation_ContratLocations]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Augementations] DROP CONSTRAINT [FK_Augementation_ContratLocations];
GO
IF OBJECT_ID(N'[dbo].[FK_CONTRATL_ASSOCIATI_LOCAL]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[ContratLocations] DROP CONSTRAINT [FK_CONTRATL_ASSOCIATI_LOCAL];
GO
IF OBJECT_ID(N'[dbo].[FK_CONTRATL_ASSOCIATI_PERSONNE]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[ContratLocations] DROP CONSTRAINT [FK_CONTRATL_ASSOCIATI_PERSONNE];
GO
IF OBJECT_ID(N'[dbo].[FK_DEPENSE_ASSOCIATI_IMMEUBLE]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Depenses] DROP CONSTRAINT [FK_DEPENSE_ASSOCIATI_IMMEUBLE];
GO
IF OBJECT_ID(N'[dbo].[FK_DEPENSE_ASSOCIATI_LOCAL]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Depenses] DROP CONSTRAINT [FK_DEPENSE_ASSOCIATI_LOCAL];
GO
IF OBJECT_ID(N'[dbo].[FK_DEPENSE_ASSOCIATI_TYPEDESI]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Depenses] DROP CONSTRAINT [FK_DEPENSE_ASSOCIATI_TYPEDESI];
GO
IF OBJECT_ID(N'[dbo].[FK_DEPENSE_GENERALIZ_OPERATIO]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Depenses] DROP CONSTRAINT [FK_DEPENSE_GENERALIZ_OPERATIO];
GO
IF OBJECT_ID(N'[dbo].[FK_EtatPaiement_Operation]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[EtatPaiements] DROP CONSTRAINT [FK_EtatPaiement_Operation];
GO
IF OBJECT_ID(N'[dbo].[FK_IMMEUBLE_ASSOCIATI_CONCIERG]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Immeubles] DROP CONSTRAINT [FK_IMMEUBLE_ASSOCIATI_CONCIERG];
GO
IF OBJECT_ID(N'[dbo].[FK_Immeuble_Personne]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Immeubles] DROP CONSTRAINT [FK_Immeuble_Personne];
GO
IF OBJECT_ID(N'[dbo].[FK_Immeubles_Adresses]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Immeubles] DROP CONSTRAINT [FK_Immeubles_Adresses];
GO
IF OBJECT_ID(N'[dbo].[FK_LOCAL_ASSOCIATI_ADRESSE]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Locals] DROP CONSTRAINT [FK_LOCAL_ASSOCIATI_ADRESSE];
GO
IF OBJECT_ID(N'[dbo].[FK_LOCAL_ASSOCIATI_IMMEUBLE]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Locals] DROP CONSTRAINT [FK_LOCAL_ASSOCIATI_IMMEUBLE];
GO
IF OBJECT_ID(N'[dbo].[FK_LOCAL_ASSOCIATI_PERSONNE]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Locals] DROP CONSTRAINT [FK_LOCAL_ASSOCIATI_PERSONNE];
GO
IF OBJECT_ID(N'[dbo].[FK_LOCAL_ASSOCIATI_SYNDIC]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Locals] DROP CONSTRAINT [FK_LOCAL_ASSOCIATI_SYNDIC];
GO
IF OBJECT_ID(N'[dbo].[FK_LOCAL_ASSOCIATI_TYPELOCA]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Locals] DROP CONSTRAINT [FK_LOCAL_ASSOCIATI_TYPELOCA];
GO
IF OBJECT_ID(N'[dbo].[FK_Menu_Parent]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[MenuItems] DROP CONSTRAINT [FK_Menu_Parent];
GO
IF OBJECT_ID(N'[dbo].[FK_OPERATIO_ASSOCIATI_PERSONNE]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Operations] DROP CONSTRAINT [FK_OPERATIO_ASSOCIATI_PERSONNE];
GO
IF OBJECT_ID(N'[dbo].[FK_OPERATIO_ASSOCIATI_TYPEOP]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Operations] DROP CONSTRAINT [FK_OPERATIO_ASSOCIATI_TYPEOP];
GO
IF OBJECT_ID(N'[dbo].[FK_PAIEMENT_ASSOCIATI_CONTRATL]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[PaiementLoyers] DROP CONSTRAINT [FK_PAIEMENT_ASSOCIATI_CONTRATL];
GO
IF OBJECT_ID(N'[dbo].[FK_PAIEMENT_GENERALIZ_OPERATIO]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[PaiementLoyers] DROP CONSTRAINT [FK_PAIEMENT_GENERALIZ_OPERATIO];
GO
IF OBJECT_ID(N'[dbo].[FK_PERSONNE_ASSOCIATI_ADRESSE]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Personnes] DROP CONSTRAINT [FK_PERSONNE_ASSOCIATI_ADRESSE];
GO
IF OBJECT_ID(N'[dbo].[FK_PERSONNE_ASSOCIATI_TYPEPERS]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Personnes] DROP CONSTRAINT [FK_PERSONNE_ASSOCIATI_TYPEPERS];
GO
IF OBJECT_ID(N'[dbo].[FK_SYNDIC_ASSOCIATI_ADRESSE]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Syndics] DROP CONSTRAINT [FK_SYNDIC_ASSOCIATI_ADRESSE];
GO

-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[Adresses]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Adresses];
GO
IF OBJECT_ID(N'[dbo].[Augementations]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Augementations];
GO
IF OBJECT_ID(N'[dbo].[Concierges]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Concierges];
GO
IF OBJECT_ID(N'[dbo].[ContratLocations]', 'U') IS NOT NULL
    DROP TABLE [dbo].[ContratLocations];
GO
IF OBJECT_ID(N'[dbo].[Depenses]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Depenses];
GO
IF OBJECT_ID(N'[dbo].[EtatPaiements]', 'U') IS NOT NULL
    DROP TABLE [dbo].[EtatPaiements];
GO
IF OBJECT_ID(N'[dbo].[Immeubles]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Immeubles];
GO
IF OBJECT_ID(N'[dbo].[Locals]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Locals];
GO
IF OBJECT_ID(N'[dbo].[MenuItems]', 'U') IS NOT NULL
    DROP TABLE [dbo].[MenuItems];
GO
IF OBJECT_ID(N'[dbo].[Operations]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Operations];
GO
IF OBJECT_ID(N'[dbo].[PaiementLoyers]', 'U') IS NOT NULL
    DROP TABLE [dbo].[PaiementLoyers];
GO
IF OBJECT_ID(N'[dbo].[Personnes]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Personnes];
GO
IF OBJECT_ID(N'[dbo].[Syndics]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Syndics];
GO
IF OBJECT_ID(N'[dbo].[TypeDesignationDepenses]', 'U') IS NOT NULL
    DROP TABLE [dbo].[TypeDesignationDepenses];
GO
IF OBJECT_ID(N'[dbo].[TypeLocals]', 'U') IS NOT NULL
    DROP TABLE [dbo].[TypeLocals];
GO
IF OBJECT_ID(N'[dbo].[TypeOps]', 'U') IS NOT NULL
    DROP TABLE [dbo].[TypeOps];
GO
IF OBJECT_ID(N'[dbo].[TypePersonnes]', 'U') IS NOT NULL
    DROP TABLE [dbo].[TypePersonnes];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'Adresses'
CREATE TABLE [dbo].[Adresses] (
    [NumAdresse] int IDENTITY(1,1) NOT NULL,
    [Pays] varchar(254)  NULL,
    [Ville] varchar(254)  NULL,
    [quartier] varchar(254)  NULL,
    [descAdresse] varchar(254)  NULL,
    [CPadress] int  NULL
);
GO

-- Creating table 'Concierges'
CREATE TABLE [dbo].[Concierges] (
    [NumConcierge] int IDENTITY(1,1) NOT NULL,
    [NomConci] varchar(254)  NULL,
    [PrenomConci] varchar(254)  NULL,
    [CinConci] varchar(254)  NULL,
    [SalaireConci] float  NULL,
    [TelConci] varchar(254)  NULL
);
GO

-- Creating table 'ContratLocations'
CREATE TABLE [dbo].[ContratLocations] (
    [CodePers] int  NOT NULL,
    [CodeLocal] int  NOT NULL,
    [CodeContrat] int IDENTITY(1,1) NOT NULL,
    [DateJuissance] datetime  NULL,
    [Usage] int  NULL,
    [LoyerDebase] float  NULL,
    [isClosed] int  NULL,
    [dateClose] datetime  NULL,
    [Charge] int  NULL,
    [TaxeDedilite] int  NULL,
    [Garage] int  NULL,
    [LoyerNet] float  NULL,
    [Caution] int  NULL,
    [DateFinContrat] datetime  NULL,
    [FrequencePaiement] int  NULL,
    [adults] int  NULL,
    [enfants] int  NULL,
    [animaux] bit  NULL,
    [commentaire] varchar(100)  NULL,
    [dateModification] datetime  NULL
);
GO

-- Creating table 'Depenses'
CREATE TABLE [dbo].[Depenses] (
    [codeOp] int  NOT NULL,
    [CodeImmeuble] int  NULL,
    [CodeLocal] int  NULL,
    [NumTypeDesignationDespense] int  NOT NULL,
    [Description] varchar(254)  NULL,
    [DateDepense] datetime  NULL,
    [DateFact] datetime  NULL
);
GO

-- Creating table 'EtatPaiements'
CREATE TABLE [dbo].[EtatPaiements] (
    [codeEP] int IDENTITY(1,1) NOT NULL,
    [cheminEP] varchar(254)  NULL,
    [codeOp] int  NULL
);
GO

-- Creating table 'Immeubles'
CREATE TABLE [dbo].[Immeubles] (
    [CodeImmeuble] int IDENTITY(1,1) NOT NULL,
    [NumAdresse] int  NOT NULL,
    [NumConcierge] int  NULL,
    [NomImmeuble] varchar(254)  NULL,
    [ServiceImmeuble] int  NULL,
    [CodePers] int  NULL,
    [NbrEtage] int  NULL,
    [Lon] varchar(50)  NULL,
    [Lat] varchar(50)  NULL,
    [Parking] bit  NOT NULL,
    [Jardin] bit  NOT NULL,
    [Interphone] bit  NOT NULL
);
GO

-- Creating table 'Locals'
CREATE TABLE [dbo].[Locals] (
    [CodeLocal] int IDENTITY(1,1) NOT NULL,
    [NumTypeLocal] int  NOT NULL,
    [NumAdresse] int  NOT NULL,
    [CodeImmeuble] int  NULL,
    [CodePers] int  NULL,
    [CodeSynd] int  NULL,
    [SuperficieLocal] float  NULL,
    [NbrPieceLocal] int  NULL,
    [NbrChambreLocal] int  NULL,
    [TitreLocal] varchar(254)  NULL,
    [DescrLocal] varchar(254)  NULL,
    [MeubleLocal] int  NULL,
    [PrixRefLocationLocal] float  NULL,
    [ServiceLocal] int  NULL,
    [Staut] int  NULL,
    [rezCh] bit  NULL,
    [Interphone] bit  NOT NULL,
    [Parking] bit  NOT NULL,
    [Garage] bit  NOT NULL,
    [Jardin] bit  NOT NULL,
    [DateDDispo] datetime  NULL,
    [DateFDispo] datetime  NULL,
    [NumLocal] int  NULL
);
GO

-- Creating table 'MenuItems'
CREATE TABLE [dbo].[MenuItems] (
    [id] int IDENTITY(1,1) NOT NULL,
    [Titre] nvarchar(100)  NOT NULL,
    [HtmlText] nvarchar(max)  NULL,
    [RowUrl] nvarchar(500)  NULL,
    [CssClass] nvarchar(100)  NULL,
    [Roles] nvarchar(100)  NULL,
    [Parent_Id] int  NULL,
    [Order] int  NULL,
    [Controller] nvarchar(50)  NULL,
    [Action] nvarchar(50)  NULL
);
GO

-- Creating table 'Operations'
CREATE TABLE [dbo].[Operations] (
    [codeOp] int IDENTITY(1,1) NOT NULL,
    [CodePers] int  NULL,
    [codeType] int  NOT NULL,
    [montant] float  NULL,
    [dateOp] datetime  NULL,
    [Commentaire] varchar(50)  NULL
);
GO

-- Creating table 'PaiementLoyers'
CREATE TABLE [dbo].[PaiementLoyers] (
    [codeOp] int  NOT NULL,
    [CodePers] int  NULL,
    [CodeLocal] int  NULL,
    [CodeContrat] int  NULL,
    [MoyenPaiement] varchar(254)  NULL,
    [Reference] varchar(254)  NULL,
    [DteDebut] datetime  NULL,
    [DteFin] datetime  NULL,
    [NbrQuittanceImprime] int  NULL,
    [DateFact] datetime  NULL,
    [DateJuissance] datetime  NULL,
    [LoyerDebase] float  NULL,
    [Charge] int  NOT NULL,
    [TaxeDedilite] int  NULL,
    [Garage] int  NULL,
    [LoyerNet] float  NULL,
    [DateFinContrat] datetime  NULL,
    [FrequencePaiement] int  NULL
);
GO

-- Creating table 'Personnes'
CREATE TABLE [dbo].[Personnes] (
    [CodePers] int IDENTITY(1,1) NOT NULL,
    [CodeTypePersonne] int  NOT NULL,
    [NumAdresse] int  NOT NULL,
    [nom] varchar(254)  NULL,
    [prenom] varchar(254)  NULL,
    [nationalite] varchar(254)  NULL,
    [sexe] int  NULL,
    [situationMatrimonial] varchar(254)  NULL,
    [numPassport] varchar(254)  NULL,
    [cin] varchar(254)  NULL,
    [raisonSocial] varchar(254)  NULL,
    [employeur] varchar(254)  NULL,
    [identifiantFiscale] varchar(254)  NULL,
    [nPatente] varchar(254)  NULL,
    [rc] varchar(254)  NULL,
    [activite] varchar(254)  NULL,
    [fonction] varchar(254)  NULL,
    [carteSejour] varchar(254)  NULL,
    [TelFixPers] varchar(254)  NULL,
    [TelMobilPers] varchar(254)  NULL,
    [TelPers3] varchar(254)  NULL,
    [EmailPers] varchar(254)  NULL,
    [FaxPers] varchar(254)  NULL,
    [Solde] float  NULL
);
GO

-- Creating table 'Syndics'
CREATE TABLE [dbo].[Syndics] (
    [CodeSynd] int IDENTITY(1,1) NOT NULL,
    [NumAdresse] int  NOT NULL,
    [NomSynd] varchar(254)  NULL,
    [PrenomSynd] varchar(254)  NULL,
    [TelSynd] varchar(254)  NULL,
    [CinSynd] varchar(50)  NULL,
    [Salaire] int  NULL
);
GO

-- Creating table 'TypeDesignationDepenses'
CREATE TABLE [dbo].[TypeDesignationDepenses] (
    [NumTypeDesignationDespense] int  NOT NULL,
    [Description] varchar(254)  NULL
);
GO

-- Creating table 'TypeLocals'
CREATE TABLE [dbo].[TypeLocals] (
    [NumTypeLocal] int IDENTITY(1,1) NOT NULL,
    [Description] varchar(254)  NULL
);
GO

-- Creating table 'TypeOps'
CREATE TABLE [dbo].[TypeOps] (
    [codeType] int IDENTITY(1,1) NOT NULL,
    [libelle] varchar(254)  NULL
);
GO

-- Creating table 'TypePersonnes'
CREATE TABLE [dbo].[TypePersonnes] (
    [CodeTypePersonne] int IDENTITY(1,1) NOT NULL,
    [Libelle] varchar(254)  NULL
);
GO

-- Creating table 'Augementations'
CREATE TABLE [dbo].[Augementations] (
    [idAug] int IDENTITY(1,1) NOT NULL,
    [dateAug] datetime  NULL,
    [pcAug] int  NULL,
    [CodeContrat] int  NULL,
    [CodePers] int  NULL,
    [CodeLocal] int  NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [NumAdresse] in table 'Adresses'
ALTER TABLE [dbo].[Adresses]
ADD CONSTRAINT [PK_Adresses]
    PRIMARY KEY CLUSTERED ([NumAdresse] ASC);
GO

-- Creating primary key on [NumConcierge] in table 'Concierges'
ALTER TABLE [dbo].[Concierges]
ADD CONSTRAINT [PK_Concierges]
    PRIMARY KEY CLUSTERED ([NumConcierge] ASC);
GO

-- Creating primary key on [CodePers], [CodeLocal], [CodeContrat] in table 'ContratLocations'
ALTER TABLE [dbo].[ContratLocations]
ADD CONSTRAINT [PK_ContratLocations]
    PRIMARY KEY CLUSTERED ([CodePers], [CodeLocal], [CodeContrat] ASC);
GO

-- Creating primary key on [codeOp] in table 'Depenses'
ALTER TABLE [dbo].[Depenses]
ADD CONSTRAINT [PK_Depenses]
    PRIMARY KEY CLUSTERED ([codeOp] ASC);
GO

-- Creating primary key on [codeEP] in table 'EtatPaiements'
ALTER TABLE [dbo].[EtatPaiements]
ADD CONSTRAINT [PK_EtatPaiements]
    PRIMARY KEY CLUSTERED ([codeEP] ASC);
GO

-- Creating primary key on [CodeImmeuble] in table 'Immeubles'
ALTER TABLE [dbo].[Immeubles]
ADD CONSTRAINT [PK_Immeubles]
    PRIMARY KEY CLUSTERED ([CodeImmeuble] ASC);
GO

-- Creating primary key on [CodeLocal] in table 'Locals'
ALTER TABLE [dbo].[Locals]
ADD CONSTRAINT [PK_Locals]
    PRIMARY KEY CLUSTERED ([CodeLocal] ASC);
GO

-- Creating primary key on [id] in table 'MenuItems'
ALTER TABLE [dbo].[MenuItems]
ADD CONSTRAINT [PK_MenuItems]
    PRIMARY KEY CLUSTERED ([id] ASC);
GO

-- Creating primary key on [codeOp] in table 'Operations'
ALTER TABLE [dbo].[Operations]
ADD CONSTRAINT [PK_Operations]
    PRIMARY KEY CLUSTERED ([codeOp] ASC);
GO

-- Creating primary key on [codeOp] in table 'PaiementLoyers'
ALTER TABLE [dbo].[PaiementLoyers]
ADD CONSTRAINT [PK_PaiementLoyers]
    PRIMARY KEY CLUSTERED ([codeOp] ASC);
GO

-- Creating primary key on [CodePers] in table 'Personnes'
ALTER TABLE [dbo].[Personnes]
ADD CONSTRAINT [PK_Personnes]
    PRIMARY KEY CLUSTERED ([CodePers] ASC);
GO

-- Creating primary key on [CodeSynd] in table 'Syndics'
ALTER TABLE [dbo].[Syndics]
ADD CONSTRAINT [PK_Syndics]
    PRIMARY KEY CLUSTERED ([CodeSynd] ASC);
GO

-- Creating primary key on [NumTypeDesignationDespense] in table 'TypeDesignationDepenses'
ALTER TABLE [dbo].[TypeDesignationDepenses]
ADD CONSTRAINT [PK_TypeDesignationDepenses]
    PRIMARY KEY CLUSTERED ([NumTypeDesignationDespense] ASC);
GO

-- Creating primary key on [NumTypeLocal] in table 'TypeLocals'
ALTER TABLE [dbo].[TypeLocals]
ADD CONSTRAINT [PK_TypeLocals]
    PRIMARY KEY CLUSTERED ([NumTypeLocal] ASC);
GO

-- Creating primary key on [codeType] in table 'TypeOps'
ALTER TABLE [dbo].[TypeOps]
ADD CONSTRAINT [PK_TypeOps]
    PRIMARY KEY CLUSTERED ([codeType] ASC);
GO

-- Creating primary key on [CodeTypePersonne] in table 'TypePersonnes'
ALTER TABLE [dbo].[TypePersonnes]
ADD CONSTRAINT [PK_TypePersonnes]
    PRIMARY KEY CLUSTERED ([CodeTypePersonne] ASC);
GO

-- Creating primary key on [idAug] in table 'Augementations'
ALTER TABLE [dbo].[Augementations]
ADD CONSTRAINT [PK_Augementations]
    PRIMARY KEY CLUSTERED ([idAug] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [NumAdresse] in table 'Immeubles'
ALTER TABLE [dbo].[Immeubles]
ADD CONSTRAINT [FK_Immeubles_Adresses]
    FOREIGN KEY ([NumAdresse])
    REFERENCES [dbo].[Adresses]
        ([NumAdresse])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_Immeubles_Adresses'
CREATE INDEX [IX_FK_Immeubles_Adresses]
ON [dbo].[Immeubles]
    ([NumAdresse]);
GO

-- Creating foreign key on [NumAdresse] in table 'Locals'
ALTER TABLE [dbo].[Locals]
ADD CONSTRAINT [FK_LOCAL_ASSOCIATI_ADRESSE]
    FOREIGN KEY ([NumAdresse])
    REFERENCES [dbo].[Adresses]
        ([NumAdresse])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_LOCAL_ASSOCIATI_ADRESSE'
CREATE INDEX [IX_FK_LOCAL_ASSOCIATI_ADRESSE]
ON [dbo].[Locals]
    ([NumAdresse]);
GO

-- Creating foreign key on [NumAdresse] in table 'Personnes'
ALTER TABLE [dbo].[Personnes]
ADD CONSTRAINT [FK_PERSONNE_ASSOCIATI_ADRESSE]
    FOREIGN KEY ([NumAdresse])
    REFERENCES [dbo].[Adresses]
        ([NumAdresse])
    ON DELETE CASCADE ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_PERSONNE_ASSOCIATI_ADRESSE'
CREATE INDEX [IX_FK_PERSONNE_ASSOCIATI_ADRESSE]
ON [dbo].[Personnes]
    ([NumAdresse]);
GO

-- Creating foreign key on [NumAdresse] in table 'Syndics'
ALTER TABLE [dbo].[Syndics]
ADD CONSTRAINT [FK_SYNDIC_ASSOCIATI_ADRESSE]
    FOREIGN KEY ([NumAdresse])
    REFERENCES [dbo].[Adresses]
        ([NumAdresse])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_SYNDIC_ASSOCIATI_ADRESSE'
CREATE INDEX [IX_FK_SYNDIC_ASSOCIATI_ADRESSE]
ON [dbo].[Syndics]
    ([NumAdresse]);
GO

-- Creating foreign key on [NumConcierge] in table 'Immeubles'
ALTER TABLE [dbo].[Immeubles]
ADD CONSTRAINT [FK_IMMEUBLE_ASSOCIATI_CONCIERG]
    FOREIGN KEY ([NumConcierge])
    REFERENCES [dbo].[Concierges]
        ([NumConcierge])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_IMMEUBLE_ASSOCIATI_CONCIERG'
CREATE INDEX [IX_FK_IMMEUBLE_ASSOCIATI_CONCIERG]
ON [dbo].[Immeubles]
    ([NumConcierge]);
GO

-- Creating foreign key on [CodeLocal] in table 'ContratLocations'
ALTER TABLE [dbo].[ContratLocations]
ADD CONSTRAINT [FK_CONTRATL_ASSOCIATI_LOCAL]
    FOREIGN KEY ([CodeLocal])
    REFERENCES [dbo].[Locals]
        ([CodeLocal])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_CONTRATL_ASSOCIATI_LOCAL'
CREATE INDEX [IX_FK_CONTRATL_ASSOCIATI_LOCAL]
ON [dbo].[ContratLocations]
    ([CodeLocal]);
GO

-- Creating foreign key on [CodePers] in table 'ContratLocations'
ALTER TABLE [dbo].[ContratLocations]
ADD CONSTRAINT [FK_CONTRATL_ASSOCIATI_PERSONNE]
    FOREIGN KEY ([CodePers])
    REFERENCES [dbo].[Personnes]
        ([CodePers])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating foreign key on [CodePers], [CodeLocal], [CodeContrat] in table 'PaiementLoyers'
ALTER TABLE [dbo].[PaiementLoyers]
ADD CONSTRAINT [FK_PAIEMENT_ASSOCIATI_CONTRATL]
    FOREIGN KEY ([CodePers], [CodeLocal], [CodeContrat])
    REFERENCES [dbo].[ContratLocations]
        ([CodePers], [CodeLocal], [CodeContrat])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_PAIEMENT_ASSOCIATI_CONTRATL'
CREATE INDEX [IX_FK_PAIEMENT_ASSOCIATI_CONTRATL]
ON [dbo].[PaiementLoyers]
    ([CodePers], [CodeLocal], [CodeContrat]);
GO

-- Creating foreign key on [CodeImmeuble] in table 'Depenses'
ALTER TABLE [dbo].[Depenses]
ADD CONSTRAINT [FK_DEPENSE_ASSOCIATI_IMMEUBLE]
    FOREIGN KEY ([CodeImmeuble])
    REFERENCES [dbo].[Immeubles]
        ([CodeImmeuble])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_DEPENSE_ASSOCIATI_IMMEUBLE'
CREATE INDEX [IX_FK_DEPENSE_ASSOCIATI_IMMEUBLE]
ON [dbo].[Depenses]
    ([CodeImmeuble]);
GO

-- Creating foreign key on [CodeLocal] in table 'Depenses'
ALTER TABLE [dbo].[Depenses]
ADD CONSTRAINT [FK_DEPENSE_ASSOCIATI_LOCAL]
    FOREIGN KEY ([CodeLocal])
    REFERENCES [dbo].[Locals]
        ([CodeLocal])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_DEPENSE_ASSOCIATI_LOCAL'
CREATE INDEX [IX_FK_DEPENSE_ASSOCIATI_LOCAL]
ON [dbo].[Depenses]
    ([CodeLocal]);
GO

-- Creating foreign key on [NumTypeDesignationDespense] in table 'Depenses'
ALTER TABLE [dbo].[Depenses]
ADD CONSTRAINT [FK_DEPENSE_ASSOCIATI_TYPEDESI]
    FOREIGN KEY ([NumTypeDesignationDespense])
    REFERENCES [dbo].[TypeDesignationDepenses]
        ([NumTypeDesignationDespense])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_DEPENSE_ASSOCIATI_TYPEDESI'
CREATE INDEX [IX_FK_DEPENSE_ASSOCIATI_TYPEDESI]
ON [dbo].[Depenses]
    ([NumTypeDesignationDespense]);
GO

-- Creating foreign key on [codeOp] in table 'Depenses'
ALTER TABLE [dbo].[Depenses]
ADD CONSTRAINT [FK_DEPENSE_GENERALIZ_OPERATIO]
    FOREIGN KEY ([codeOp])
    REFERENCES [dbo].[Operations]
        ([codeOp])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating foreign key on [codeOp] in table 'EtatPaiements'
ALTER TABLE [dbo].[EtatPaiements]
ADD CONSTRAINT [FK_EtatPaiement_Operation]
    FOREIGN KEY ([codeOp])
    REFERENCES [dbo].[Operations]
        ([codeOp])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_EtatPaiement_Operation'
CREATE INDEX [IX_FK_EtatPaiement_Operation]
ON [dbo].[EtatPaiements]
    ([codeOp]);
GO

-- Creating foreign key on [CodePers] in table 'Immeubles'
ALTER TABLE [dbo].[Immeubles]
ADD CONSTRAINT [FK_Immeuble_Personne]
    FOREIGN KEY ([CodePers])
    REFERENCES [dbo].[Personnes]
        ([CodePers])
    ON DELETE CASCADE ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_Immeuble_Personne'
CREATE INDEX [IX_FK_Immeuble_Personne]
ON [dbo].[Immeubles]
    ([CodePers]);
GO

-- Creating foreign key on [CodeImmeuble] in table 'Locals'
ALTER TABLE [dbo].[Locals]
ADD CONSTRAINT [FK_LOCAL_ASSOCIATI_IMMEUBLE]
    FOREIGN KEY ([CodeImmeuble])
    REFERENCES [dbo].[Immeubles]
        ([CodeImmeuble])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_LOCAL_ASSOCIATI_IMMEUBLE'
CREATE INDEX [IX_FK_LOCAL_ASSOCIATI_IMMEUBLE]
ON [dbo].[Locals]
    ([CodeImmeuble]);
GO

-- Creating foreign key on [CodePers] in table 'Locals'
ALTER TABLE [dbo].[Locals]
ADD CONSTRAINT [FK_LOCAL_ASSOCIATI_PERSONNE]
    FOREIGN KEY ([CodePers])
    REFERENCES [dbo].[Personnes]
        ([CodePers])
    ON DELETE CASCADE ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_LOCAL_ASSOCIATI_PERSONNE'
CREATE INDEX [IX_FK_LOCAL_ASSOCIATI_PERSONNE]
ON [dbo].[Locals]
    ([CodePers]);
GO

-- Creating foreign key on [CodeSynd] in table 'Locals'
ALTER TABLE [dbo].[Locals]
ADD CONSTRAINT [FK_LOCAL_ASSOCIATI_SYNDIC]
    FOREIGN KEY ([CodeSynd])
    REFERENCES [dbo].[Syndics]
        ([CodeSynd])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_LOCAL_ASSOCIATI_SYNDIC'
CREATE INDEX [IX_FK_LOCAL_ASSOCIATI_SYNDIC]
ON [dbo].[Locals]
    ([CodeSynd]);
GO

-- Creating foreign key on [NumTypeLocal] in table 'Locals'
ALTER TABLE [dbo].[Locals]
ADD CONSTRAINT [FK_LOCAL_ASSOCIATI_TYPELOCA]
    FOREIGN KEY ([NumTypeLocal])
    REFERENCES [dbo].[TypeLocals]
        ([NumTypeLocal])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_LOCAL_ASSOCIATI_TYPELOCA'
CREATE INDEX [IX_FK_LOCAL_ASSOCIATI_TYPELOCA]
ON [dbo].[Locals]
    ([NumTypeLocal]);
GO

-- Creating foreign key on [CodePers] in table 'Operations'
ALTER TABLE [dbo].[Operations]
ADD CONSTRAINT [FK_OPERATIO_ASSOCIATI_PERSONNE]
    FOREIGN KEY ([CodePers])
    REFERENCES [dbo].[Personnes]
        ([CodePers])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_OPERATIO_ASSOCIATI_PERSONNE'
CREATE INDEX [IX_FK_OPERATIO_ASSOCIATI_PERSONNE]
ON [dbo].[Operations]
    ([CodePers]);
GO

-- Creating foreign key on [codeType] in table 'Operations'
ALTER TABLE [dbo].[Operations]
ADD CONSTRAINT [FK_OPERATIO_ASSOCIATI_TYPEOP]
    FOREIGN KEY ([codeType])
    REFERENCES [dbo].[TypeOps]
        ([codeType])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_OPERATIO_ASSOCIATI_TYPEOP'
CREATE INDEX [IX_FK_OPERATIO_ASSOCIATI_TYPEOP]
ON [dbo].[Operations]
    ([codeType]);
GO

-- Creating foreign key on [codeOp] in table 'PaiementLoyers'
ALTER TABLE [dbo].[PaiementLoyers]
ADD CONSTRAINT [FK_PAIEMENT_GENERALIZ_OPERATIO]
    FOREIGN KEY ([codeOp])
    REFERENCES [dbo].[Operations]
        ([codeOp])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating foreign key on [CodeTypePersonne] in table 'Personnes'
ALTER TABLE [dbo].[Personnes]
ADD CONSTRAINT [FK_PERSONNE_ASSOCIATI_TYPEPERS]
    FOREIGN KEY ([CodeTypePersonne])
    REFERENCES [dbo].[TypePersonnes]
        ([CodeTypePersonne])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_PERSONNE_ASSOCIATI_TYPEPERS'
CREATE INDEX [IX_FK_PERSONNE_ASSOCIATI_TYPEPERS]
ON [dbo].[Personnes]
    ([CodeTypePersonne]);
GO

-- Creating foreign key on [CodePers], [CodeLocal], [CodeContrat] in table 'Augementations'
ALTER TABLE [dbo].[Augementations]
ADD CONSTRAINT [FK_Augementation_ContratLocations]
    FOREIGN KEY ([CodePers], [CodeLocal], [CodeContrat])
    REFERENCES [dbo].[ContratLocations]
        ([CodePers], [CodeLocal], [CodeContrat])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_Augementation_ContratLocations'
CREATE INDEX [IX_FK_Augementation_ContratLocations]
ON [dbo].[Augementations]
    ([CodePers], [CodeLocal], [CodeContrat]);
GO

-- Creating foreign key on [Parent_Id] in table 'MenuItems'
ALTER TABLE [dbo].[MenuItems]
ADD CONSTRAINT [FK_Menu_Parent]
    FOREIGN KEY ([Parent_Id])
    REFERENCES [dbo].[MenuItems]
        ([id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_Menu_Parent'
CREATE INDEX [IX_FK_Menu_Parent]
ON [dbo].[MenuItems]
    ([Parent_Id]);
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------