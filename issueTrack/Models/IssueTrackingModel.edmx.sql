
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 05/19/2017 08:09:57
-- Generated from EDMX file: C:\Users\hjd\Source\Repos\issueTrack\issueTrack\Models\IssueTrackingModel.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [testdb];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[FK_TBCreators_TBRepositories]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[TBCreators] DROP CONSTRAINT [FK_TBCreators_TBRepositories];
GO
IF OBJECT_ID(N'[dbo].[FK_TBIssues_TBCreators]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[TBIssues] DROP CONSTRAINT [FK_TBIssues_TBCreators];
GO
IF OBJECT_ID(N'[dbo].[FK_TBRepositories_TBUsers]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[TBRepositories] DROP CONSTRAINT [FK_TBRepositories_TBUsers];
GO

-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[TBCreators]', 'U') IS NOT NULL
    DROP TABLE [dbo].[TBCreators];
GO
IF OBJECT_ID(N'[dbo].[TBIssues]', 'U') IS NOT NULL
    DROP TABLE [dbo].[TBIssues];
GO
IF OBJECT_ID(N'[dbo].[TBReplies]', 'U') IS NOT NULL
    DROP TABLE [dbo].[TBReplies];
GO
IF OBJECT_ID(N'[dbo].[TBRepositories]', 'U') IS NOT NULL
    DROP TABLE [dbo].[TBRepositories];
GO
IF OBJECT_ID(N'[dbo].[TBUsers]', 'U') IS NOT NULL
    DROP TABLE [dbo].[TBUsers];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'TBUsers'
CREATE TABLE [dbo].[TBUsers] (
    [FDEmailOrPhone] varchar(64)  NOT NULL,
    [FDNickname] nvarchar(32)  NOT NULL,
    [FDPassword] varbinary(32)  NULL,
    [FDCreationTimestamp] datetime  NOT NULL,
    [FDUpdateTimestamp] datetime  NULL
);
GO

-- Creating table 'TBCreators'
CREATE TABLE [dbo].[TBCreators] (
    [FDRepositoryID] int  NOT NULL,
    [FDCreator] nvarchar(32)  NOT NULL,
    [FDToken] nvarchar(32)  NOT NULL,
    [FDLoginMethod] int  NOT NULL,
    [FDCreationTImestamp] datetime  NOT NULL
);
GO

-- Creating table 'TBRepositories'
CREATE TABLE [dbo].[TBRepositories] (
    [FDRepositoryID] int IDENTITY(1,1) NOT NULL,
    [FDOwner] varchar(64)  NOT NULL,
    [FDRepositoryName] nvarchar(64)  NOT NULL,
    [FDDescription] nvarchar(2048)  NULL,
    [FDCreationTimestamp] datetime  NOT NULL
);
GO

-- Creating table 'TBIssues'
CREATE TABLE [dbo].[TBIssues] (
    [FDIssueID] int IDENTITY(1,1) NOT NULL,
    [FDCreator] nvarchar(32)  NOT NULL,
    [FDRepositoryID] int  NOT NULL,
    [FDIssueTitle] nvarchar(256)  NOT NULL,
    [FDIssueContent] nvarchar(2048)  NULL,
    [FDState] int  NOT NULL,
    [FDCreationTimestamp] datetime  NOT NULL
);
GO

-- Creating table 'TBReplies'
CREATE TABLE [dbo].[TBReplies] (
    [FDReplyID] int IDENTITY(1,1) NOT NULL,
    [FDCreator] nvarchar(32)  NOT NULL,
    [FDIssueIDOrReplyID] int  NOT NULL,
    [FDReferenceType] int  NOT NULL,
    [FDReplyContent] nvarchar(1024)  NOT NULL,
    [FDCreationTimestamp] datetime  NOT NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [FDEmailOrPhone] in table 'TBUsers'
ALTER TABLE [dbo].[TBUsers]
ADD CONSTRAINT [PK_TBUsers]
    PRIMARY KEY CLUSTERED ([FDEmailOrPhone] ASC);
GO

-- Creating primary key on [FDRepositoryID], [FDCreator] in table 'TBCreators'
ALTER TABLE [dbo].[TBCreators]
ADD CONSTRAINT [PK_TBCreators]
    PRIMARY KEY CLUSTERED ([FDRepositoryID], [FDCreator] ASC);
GO

-- Creating primary key on [FDRepositoryID] in table 'TBRepositories'
ALTER TABLE [dbo].[TBRepositories]
ADD CONSTRAINT [PK_TBRepositories]
    PRIMARY KEY CLUSTERED ([FDRepositoryID] ASC);
GO

-- Creating primary key on [FDIssueID] in table 'TBIssues'
ALTER TABLE [dbo].[TBIssues]
ADD CONSTRAINT [PK_TBIssues]
    PRIMARY KEY CLUSTERED ([FDIssueID] ASC);
GO

-- Creating primary key on [FDReplyID] in table 'TBReplies'
ALTER TABLE [dbo].[TBReplies]
ADD CONSTRAINT [PK_TBReplies]
    PRIMARY KEY CLUSTERED ([FDReplyID] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [FDRepositoryID] in table 'TBCreators'
ALTER TABLE [dbo].[TBCreators]
ADD CONSTRAINT [FK_TBCreators_TBRepositories]
    FOREIGN KEY ([FDRepositoryID])
    REFERENCES [dbo].[TBRepositories]
        ([FDRepositoryID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating foreign key on [FDOwner] in table 'TBRepositories'
ALTER TABLE [dbo].[TBRepositories]
ADD CONSTRAINT [FK_TBRepositories_TBUsers]
    FOREIGN KEY ([FDOwner])
    REFERENCES [dbo].[TBUsers]
        ([FDEmailOrPhone])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_TBRepositories_TBUsers'
CREATE INDEX [IX_FK_TBRepositories_TBUsers]
ON [dbo].[TBRepositories]
    ([FDOwner]);
GO

-- Creating foreign key on [FDRepositoryID], [FDCreator] in table 'TBIssues'
ALTER TABLE [dbo].[TBIssues]
ADD CONSTRAINT [FK_TBIssues_TBCreators]
    FOREIGN KEY ([FDRepositoryID], [FDCreator])
    REFERENCES [dbo].[TBCreators]
        ([FDRepositoryID], [FDCreator])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_TBIssues_TBCreators'
CREATE INDEX [IX_FK_TBIssues_TBCreators]
ON [dbo].[TBIssues]
    ([FDRepositoryID], [FDCreator]);
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------