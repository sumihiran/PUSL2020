/*
 Navicat Premium Data Transfer

 Source Server         : SQL Server Docker
 Source Server Type    : SQL Server
 Source Server Version : 15004178
 Source Host           : localhost:1433
 Source Catalog        : PUSL2020
 Source Schema         : dbo

 Target Server Type    : SQL Server
 Target Server Version : 15004178
 File Encoding         : 65001

 Date: 28/04/2022 09:33:37
*/


-- ----------------------------
-- Table structure for __EFMigrationsHistory
-- ----------------------------
IF EXISTS (SELECT * FROM sys.all_objects WHERE object_id = OBJECT_ID(N'[dbo].[__EFMigrationsHistory]') AND type IN ('U'))
	DROP TABLE [dbo].[__EFMigrationsHistory]
GO

CREATE TABLE [dbo].[__EFMigrationsHistory] (
  [MigrationId] nvarchar(150) COLLATE SQL_Latin1_General_CP1_CI_AS  NOT NULL,
  [ProductVersion] nvarchar(32) COLLATE SQL_Latin1_General_CP1_CI_AS  NOT NULL
)
GO

ALTER TABLE [dbo].[__EFMigrationsHistory] SET (LOCK_ESCALATION = TABLE)
GO


-- ----------------------------
-- Records of __EFMigrationsHistory
-- ----------------------------
BEGIN TRANSACTION
GO

INSERT INTO [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20220428033424_InitialCreate', N'6.0.2')
GO

INSERT INTO [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20220428035055_AddModelTimestamps', N'6.0.2')
GO

COMMIT
GO


-- ----------------------------
-- Table structure for Accidents
-- ----------------------------
IF EXISTS (SELECT * FROM sys.all_objects WHERE object_id = OBJECT_ID(N'[dbo].[Accidents]') AND type IN ('U'))
	DROP TABLE [dbo].[Accidents]
GO

CREATE TABLE [dbo].[Accidents] (
  [Id] uniqueidentifier  NOT NULL,
  [ReporterId] uniqueidentifier  NOT NULL,
  [Driver_Name] nvarchar(max) COLLATE SQL_Latin1_General_CP1_CI_AS  NOT NULL,
  [Driver_Dln] nvarchar(max) COLLATE SQL_Latin1_General_CP1_CI_AS  NOT NULL,
  [DateTime] datetime2(7)  NOT NULL,
  [Location_Road] nvarchar(max) COLLATE SQL_Latin1_General_CP1_CI_AS  NOT NULL,
  [Location_District] int  NOT NULL,
  [Location_Latitude] decimal(10,8)  NULL,
  [Location_Longitude] decimal(11,8)  NULL,
  [Cause] int  NOT NULL,
  [Reason] nvarchar(max) COLLATE SQL_Latin1_General_CP1_CI_AS  NOT NULL,
  [Description] nvarchar(max) COLLATE SQL_Latin1_General_CP1_CI_AS  NOT NULL,
  [Reported] datetime2(7)  NOT NULL,
  [Updated] datetime2(7)  NOT NULL,
  [Archived] datetime2(7)  NULL,
  [RdaApproval_IsApproved] bit  NULL,
  [RdaApproval_Reason] nvarchar(max) COLLATE SQL_Latin1_General_CP1_CI_AS  NULL,
  [RdaApproval_EmployeeId] uniqueidentifier  NULL,
  [PoliceApproval_IsApproved] bit  NULL,
  [PoliceApproval_Reason] nvarchar(max) COLLATE SQL_Latin1_General_CP1_CI_AS  NULL,
  [PoliceApproval_EmployeeId] uniqueidentifier  NULL,
  [InsuranceApproval_IsApproved] bit  NULL,
  [InsuranceApproval_Reason] nvarchar(max) COLLATE SQL_Latin1_General_CP1_CI_AS  NULL,
  [InsuranceApproval_EmployeeId] uniqueidentifier  NULL
)
GO

ALTER TABLE [dbo].[Accidents] SET (LOCK_ESCALATION = TABLE)
GO


-- ----------------------------
-- Records of Accidents
-- ----------------------------
BEGIN TRANSACTION
GO

COMMIT
GO


-- ----------------------------
-- Table structure for Employees
-- ----------------------------
IF EXISTS (SELECT * FROM sys.all_objects WHERE object_id = OBJECT_ID(N'[dbo].[Employees]') AND type IN ('U'))
	DROP TABLE [dbo].[Employees]
GO

CREATE TABLE [dbo].[Employees] (
  [Id] uniqueidentifier  NOT NULL,
  [UserName] nvarchar(max) COLLATE SQL_Latin1_General_CP1_CI_AS  NOT NULL,
  [DisplayName] nvarchar(max) COLLATE SQL_Latin1_General_CP1_CI_AS  NOT NULL,
  [PasswordHash] nvarchar(max) COLLATE SQL_Latin1_General_CP1_CI_AS  NOT NULL,
  [RdaOfficeId] uniqueidentifier  NULL,
  [Created] datetime2(7) DEFAULT '0001-01-01T00:00:00.0000000' NOT NULL
)
GO

ALTER TABLE [dbo].[Employees] SET (LOCK_ESCALATION = TABLE)
GO


-- ----------------------------
-- Records of Employees
-- ----------------------------
BEGIN TRANSACTION
GO

COMMIT
GO


-- ----------------------------
-- Table structure for EmployeeUsers
-- ----------------------------
IF EXISTS (SELECT * FROM sys.all_objects WHERE object_id = OBJECT_ID(N'[dbo].[EmployeeUsers]') AND type IN ('U'))
	DROP TABLE [dbo].[EmployeeUsers]
GO

CREATE TABLE [dbo].[EmployeeUsers] (
  [Id] uniqueidentifier  NOT NULL,
  [NormalizedUserName] nvarchar(450) COLLATE SQL_Latin1_General_CP1_CI_AS  NULL,
  [SecurityStamp] nvarchar(max) COLLATE SQL_Latin1_General_CP1_CI_AS  NULL,
  [ConcurrencyStamp] nvarchar(max) COLLATE SQL_Latin1_General_CP1_CI_AS  NULL,
  [TwoFactorEnabled] bit  NOT NULL,
  [LockoutEnd] datetimeoffset(7)  NULL,
  [LockoutEnabled] bit  NOT NULL,
  [AccessFailedCount] int  NOT NULL
)
GO

ALTER TABLE [dbo].[EmployeeUsers] SET (LOCK_ESCALATION = TABLE)
GO


-- ----------------------------
-- Records of EmployeeUsers
-- ----------------------------
BEGIN TRANSACTION
GO

COMMIT
GO


-- ----------------------------
-- Table structure for Images
-- ----------------------------
IF EXISTS (SELECT * FROM sys.all_objects WHERE object_id = OBJECT_ID(N'[dbo].[Images]') AND type IN ('U'))
	DROP TABLE [dbo].[Images]
GO

CREATE TABLE [dbo].[Images] (
  [Id] uniqueidentifier  NOT NULL,
  [Path] nvarchar(max) COLLATE SQL_Latin1_General_CP1_CI_AS  NOT NULL,
  [Name] nvarchar(max) COLLATE SQL_Latin1_General_CP1_CI_AS  NOT NULL,
  [AccidentId] uniqueidentifier DEFAULT '00000000-0000-0000-0000-000000000000' NOT NULL,
  [Uploaded] datetime2(7) DEFAULT '0001-01-01T00:00:00.0000000' NOT NULL
)
GO

ALTER TABLE [dbo].[Images] SET (LOCK_ESCALATION = TABLE)
GO


-- ----------------------------
-- Records of Images
-- ----------------------------
BEGIN TRANSACTION
GO

COMMIT
GO


-- ----------------------------
-- Table structure for Insurance_Employees
-- ----------------------------
IF EXISTS (SELECT * FROM sys.all_objects WHERE object_id = OBJECT_ID(N'[dbo].[Insurance_Employees]') AND type IN ('U'))
	DROP TABLE [dbo].[Insurance_Employees]
GO

CREATE TABLE [dbo].[Insurance_Employees] (
  [Id] uniqueidentifier  NOT NULL,
  [HeadOfficeId] uniqueidentifier  NOT NULL
)
GO

ALTER TABLE [dbo].[Insurance_Employees] SET (LOCK_ESCALATION = TABLE)
GO


-- ----------------------------
-- Records of Insurance_Employees
-- ----------------------------
BEGIN TRANSACTION
GO

COMMIT
GO


-- ----------------------------
-- Table structure for Insurances
-- ----------------------------
IF EXISTS (SELECT * FROM sys.all_objects WHERE object_id = OBJECT_ID(N'[dbo].[Insurances]') AND type IN ('U'))
	DROP TABLE [dbo].[Insurances]
GO

CREATE TABLE [dbo].[Insurances] (
  [Id] uniqueidentifier  NOT NULL,
  [Name] nvarchar(max) COLLATE SQL_Latin1_General_CP1_CI_AS  NOT NULL,
  [Address_Line1] nvarchar(max) COLLATE SQL_Latin1_General_CP1_CI_AS  NULL,
  [Address_Line2] nvarchar(max) COLLATE SQL_Latin1_General_CP1_CI_AS  NULL,
  [Address_Street] nvarchar(max) COLLATE SQL_Latin1_General_CP1_CI_AS  NULL,
  [Address_City] nvarchar(max) COLLATE SQL_Latin1_General_CP1_CI_AS  NULL,
  [Address_District] nvarchar(max) COLLATE SQL_Latin1_General_CP1_CI_AS  NULL,
  [Address_ZipCode] int  NULL,
  [PhoneNumber] nvarchar(max) COLLATE SQL_Latin1_General_CP1_CI_AS  NULL
)
GO

ALTER TABLE [dbo].[Insurances] SET (LOCK_ESCALATION = TABLE)
GO


-- ----------------------------
-- Records of Insurances
-- ----------------------------
BEGIN TRANSACTION
GO

COMMIT
GO


-- ----------------------------
-- Table structure for Police_Officers
-- ----------------------------
IF EXISTS (SELECT * FROM sys.all_objects WHERE object_id = OBJECT_ID(N'[dbo].[Police_Officers]') AND type IN ('U'))
	DROP TABLE [dbo].[Police_Officers]
GO

CREATE TABLE [dbo].[Police_Officers] (
  [Id] uniqueidentifier  NOT NULL,
  [StationId] uniqueidentifier  NOT NULL
)
GO

ALTER TABLE [dbo].[Police_Officers] SET (LOCK_ESCALATION = TABLE)
GO


-- ----------------------------
-- Records of Police_Officers
-- ----------------------------
BEGIN TRANSACTION
GO

COMMIT
GO


-- ----------------------------
-- Table structure for PoliceStations
-- ----------------------------
IF EXISTS (SELECT * FROM sys.all_objects WHERE object_id = OBJECT_ID(N'[dbo].[PoliceStations]') AND type IN ('U'))
	DROP TABLE [dbo].[PoliceStations]
GO

CREATE TABLE [dbo].[PoliceStations] (
  [Id] uniqueidentifier  NOT NULL,
  [Area] nvarchar(max) COLLATE SQL_Latin1_General_CP1_CI_AS  NOT NULL,
  [Address_Line1] nvarchar(max) COLLATE SQL_Latin1_General_CP1_CI_AS  NULL,
  [Address_Line2] nvarchar(max) COLLATE SQL_Latin1_General_CP1_CI_AS  NULL,
  [Address_Street] nvarchar(max) COLLATE SQL_Latin1_General_CP1_CI_AS  NULL,
  [Address_City] nvarchar(max) COLLATE SQL_Latin1_General_CP1_CI_AS  NULL,
  [Address_District] nvarchar(max) COLLATE SQL_Latin1_General_CP1_CI_AS  NULL,
  [Address_ZipCode] int  NULL,
  [PhoneNumber] nvarchar(max) COLLATE SQL_Latin1_General_CP1_CI_AS  NULL
)
GO

ALTER TABLE [dbo].[PoliceStations] SET (LOCK_ESCALATION = TABLE)
GO


-- ----------------------------
-- Records of PoliceStations
-- ----------------------------
BEGIN TRANSACTION
GO

COMMIT
GO


-- ----------------------------
-- Table structure for Rda_Employees
-- ----------------------------
IF EXISTS (SELECT * FROM sys.all_objects WHERE object_id = OBJECT_ID(N'[dbo].[Rda_Employees]') AND type IN ('U'))
	DROP TABLE [dbo].[Rda_Employees]
GO

CREATE TABLE [dbo].[Rda_Employees] (
  [Id] uniqueidentifier  NOT NULL,
  [OfficeId] uniqueidentifier  NOT NULL
)
GO

ALTER TABLE [dbo].[Rda_Employees] SET (LOCK_ESCALATION = TABLE)
GO


-- ----------------------------
-- Records of Rda_Employees
-- ----------------------------
BEGIN TRANSACTION
GO

COMMIT
GO


-- ----------------------------
-- Table structure for RdaOffices
-- ----------------------------
IF EXISTS (SELECT * FROM sys.all_objects WHERE object_id = OBJECT_ID(N'[dbo].[RdaOffices]') AND type IN ('U'))
	DROP TABLE [dbo].[RdaOffices]
GO

CREATE TABLE [dbo].[RdaOffices] (
  [Id] uniqueidentifier  NOT NULL,
  [District] int  NOT NULL,
  [Address_Line1] nvarchar(max) COLLATE SQL_Latin1_General_CP1_CI_AS  NULL,
  [Address_Line2] nvarchar(max) COLLATE SQL_Latin1_General_CP1_CI_AS  NULL,
  [Address_Street] nvarchar(max) COLLATE SQL_Latin1_General_CP1_CI_AS  NULL,
  [Address_City] nvarchar(max) COLLATE SQL_Latin1_General_CP1_CI_AS  NULL,
  [Address_District] nvarchar(max) COLLATE SQL_Latin1_General_CP1_CI_AS  NULL,
  [Address_ZipCode] int  NULL,
  [PhoneNumber] nvarchar(max) COLLATE SQL_Latin1_General_CP1_CI_AS  NULL
)
GO

ALTER TABLE [dbo].[RdaOffices] SET (LOCK_ESCALATION = TABLE)
GO


-- ----------------------------
-- Records of RdaOffices
-- ----------------------------
BEGIN TRANSACTION
GO

COMMIT
GO


-- ----------------------------
-- Table structure for Reporters
-- ----------------------------
IF EXISTS (SELECT * FROM sys.all_objects WHERE object_id = OBJECT_ID(N'[dbo].[Reporters]') AND type IN ('U'))
	DROP TABLE [dbo].[Reporters]
GO

CREATE TABLE [dbo].[Reporters] (
  [Id] uniqueidentifier  NOT NULL,
  [ReporterType] nvarchar(max) COLLATE SQL_Latin1_General_CP1_CI_AS  NOT NULL,
  [Email] nvarchar(max) COLLATE SQL_Latin1_General_CP1_CI_AS  NOT NULL,
  [PhoneNumber] nvarchar(max) COLLATE SQL_Latin1_General_CP1_CI_AS  NULL,
  [Address_Line1] nvarchar(max) COLLATE SQL_Latin1_General_CP1_CI_AS  NOT NULL,
  [Address_Line2] nvarchar(max) COLLATE SQL_Latin1_General_CP1_CI_AS  NULL,
  [Address_Street] nvarchar(max) COLLATE SQL_Latin1_General_CP1_CI_AS  NULL,
  [Address_City] nvarchar(max) COLLATE SQL_Latin1_General_CP1_CI_AS  NULL,
  [Address_District] nvarchar(max) COLLATE SQL_Latin1_General_CP1_CI_AS  NOT NULL,
  [Address_ZipCode] int  NULL,
  [PasswordHash] nvarchar(max) COLLATE SQL_Latin1_General_CP1_CI_AS  NOT NULL,
  [Crn] nvarchar(450) COLLATE SQL_Latin1_General_CP1_CI_AS  NULL,
  [LegalName] nvarchar(max) COLLATE SQL_Latin1_General_CP1_CI_AS  NULL,
  [Nic] nvarchar(450) COLLATE SQL_Latin1_General_CP1_CI_AS  NULL,
  [FirstName] nvarchar(max) COLLATE SQL_Latin1_General_CP1_CI_AS  NULL,
  [MiddleName] nvarchar(max) COLLATE SQL_Latin1_General_CP1_CI_AS  NULL,
  [LastName] nvarchar(max) COLLATE SQL_Latin1_General_CP1_CI_AS  NULL,
  [DriverLicenseId] nvarchar(450) COLLATE SQL_Latin1_General_CP1_CI_AS  NULL,
  [Created] datetime2(7) DEFAULT '0001-01-01T00:00:00.0000000' NOT NULL,
  [Updated] datetime2(7) DEFAULT '0001-01-01T00:00:00.0000000' NOT NULL
)
GO

ALTER TABLE [dbo].[Reporters] SET (LOCK_ESCALATION = TABLE)
GO


-- ----------------------------
-- Records of Reporters
-- ----------------------------
BEGIN TRANSACTION
GO

COMMIT
GO


-- ----------------------------
-- Table structure for ReporterUser
-- ----------------------------
IF EXISTS (SELECT * FROM sys.all_objects WHERE object_id = OBJECT_ID(N'[dbo].[ReporterUser]') AND type IN ('U'))
	DROP TABLE [dbo].[ReporterUser]
GO

CREATE TABLE [dbo].[ReporterUser] (
  [Id] uniqueidentifier  NOT NULL,
  [NormalizedEmail] nvarchar(256) COLLATE SQL_Latin1_General_CP1_CI_AS  NULL,
  [EmailConfirmed] bit  NOT NULL,
  [SecurityStamp] nvarchar(max) COLLATE SQL_Latin1_General_CP1_CI_AS  NULL,
  [ConcurrencyStamp] nvarchar(max) COLLATE SQL_Latin1_General_CP1_CI_AS  NULL,
  [PhoneNumberConfirmed] bit  NOT NULL,
  [TwoFactorEnabled] bit  NOT NULL,
  [LockoutEnd] datetimeoffset(7)  NULL,
  [LockoutEnabled] bit  NOT NULL,
  [AccessFailedCount] int  NOT NULL
)
GO

ALTER TABLE [dbo].[ReporterUser] SET (LOCK_ESCALATION = TABLE)
GO


-- ----------------------------
-- Records of ReporterUser
-- ----------------------------
BEGIN TRANSACTION
GO

COMMIT
GO


-- ----------------------------
-- Table structure for Vehicle_Snapshots
-- ----------------------------
IF EXISTS (SELECT * FROM sys.all_objects WHERE object_id = OBJECT_ID(N'[dbo].[Vehicle_Snapshots]') AND type IN ('U'))
	DROP TABLE [dbo].[Vehicle_Snapshots]
GO

CREATE TABLE [dbo].[Vehicle_Snapshots] (
  [AccidentId] uniqueidentifier  NOT NULL,
  [Vrn] nvarchar(max) COLLATE SQL_Latin1_General_CP1_CI_AS  NOT NULL,
  [Make] nvarchar(max) COLLATE SQL_Latin1_General_CP1_CI_AS  NOT NULL,
  [Model] nvarchar(max) COLLATE SQL_Latin1_General_CP1_CI_AS  NOT NULL,
  [Class] nvarchar(max) COLLATE SQL_Latin1_General_CP1_CI_AS  NOT NULL,
  [FuelType] int  NOT NULL,
  [EngineNo] nvarchar(max) COLLATE SQL_Latin1_General_CP1_CI_AS  NOT NULL,
  [RegisteredAt] datetime2(7)  NOT NULL,
  [Owner_Name] nvarchar(max) COLLATE SQL_Latin1_General_CP1_CI_AS  NOT NULL,
  [Owner_Phone] nvarchar(max) COLLATE SQL_Latin1_General_CP1_CI_AS  NOT NULL,
  [Insurance_PolicyId] nvarchar(max) COLLATE SQL_Latin1_General_CP1_CI_AS  NOT NULL,
  [Insurance_StartAt] datetime2(7)  NOT NULL,
  [Insurance_ExpiryAt] datetime2(7)  NOT NULL,
  [Insurance_IssuerId] uniqueidentifier  NOT NULL
)
GO

ALTER TABLE [dbo].[Vehicle_Snapshots] SET (LOCK_ESCALATION = TABLE)
GO


-- ----------------------------
-- Records of Vehicle_Snapshots
-- ----------------------------
BEGIN TRANSACTION
GO

COMMIT
GO


-- ----------------------------
-- Table structure for Vehicles
-- ----------------------------
IF EXISTS (SELECT * FROM sys.all_objects WHERE object_id = OBJECT_ID(N'[dbo].[Vehicles]') AND type IN ('U'))
	DROP TABLE [dbo].[Vehicles]
GO

CREATE TABLE [dbo].[Vehicles] (
  [Id] uniqueidentifier  NOT NULL,
  [ReporterId] uniqueidentifier  NOT NULL,
  [Vrn] nvarchar(450) COLLATE SQL_Latin1_General_CP1_CI_AS  NOT NULL,
  [Make] nvarchar(max) COLLATE SQL_Latin1_General_CP1_CI_AS  NOT NULL,
  [Model] nvarchar(max) COLLATE SQL_Latin1_General_CP1_CI_AS  NOT NULL,
  [Class] nvarchar(max) COLLATE SQL_Latin1_General_CP1_CI_AS  NOT NULL,
  [FuelType] int  NOT NULL,
  [EngineNo] nvarchar(max) COLLATE SQL_Latin1_General_CP1_CI_AS  NOT NULL,
  [RegisteredAt] datetime2(7)  NOT NULL,
  [Owner_Name] nvarchar(max) COLLATE SQL_Latin1_General_CP1_CI_AS  NOT NULL,
  [Owner_Address_Line1] nvarchar(max) COLLATE SQL_Latin1_General_CP1_CI_AS  NOT NULL,
  [Owner_Address_Line2] nvarchar(max) COLLATE SQL_Latin1_General_CP1_CI_AS  NULL,
  [Owner_Address_Street] nvarchar(max) COLLATE SQL_Latin1_General_CP1_CI_AS  NULL,
  [Owner_Address_City] nvarchar(max) COLLATE SQL_Latin1_General_CP1_CI_AS  NULL,
  [Owner_Address_District] nvarchar(max) COLLATE SQL_Latin1_General_CP1_CI_AS  NOT NULL,
  [Owner_Address_ZipCode] int  NULL,
  [Owner_Phone] nvarchar(max) COLLATE SQL_Latin1_General_CP1_CI_AS  NOT NULL,
  [Insurance_PolicyId] nvarchar(max) COLLATE SQL_Latin1_General_CP1_CI_AS  NOT NULL,
  [Insurance_StartAt] datetime2(7)  NOT NULL,
  [Insurance_ExpiryAt] datetime2(7)  NOT NULL,
  [Insurance_IssuerId] uniqueidentifier  NOT NULL,
  [Created] datetime2(7) DEFAULT '0001-01-01T00:00:00.0000000' NOT NULL,
  [Updated] datetime2(7) DEFAULT '0001-01-01T00:00:00.0000000' NOT NULL
)
GO

ALTER TABLE [dbo].[Vehicles] SET (LOCK_ESCALATION = TABLE)
GO


-- ----------------------------
-- Records of Vehicles
-- ----------------------------
BEGIN TRANSACTION
GO

COMMIT
GO


-- ----------------------------
-- Table structure for WebMasters
-- ----------------------------
IF EXISTS (SELECT * FROM sys.all_objects WHERE object_id = OBJECT_ID(N'[dbo].[WebMasters]') AND type IN ('U'))
	DROP TABLE [dbo].[WebMasters]
GO

CREATE TABLE [dbo].[WebMasters] (
  [Id] int  IDENTITY(1,1) NOT NULL,
  [UserName] nvarchar(max) COLLATE SQL_Latin1_General_CP1_CI_AS  NULL,
  [NormalizedUserName] nvarchar(450) COLLATE SQL_Latin1_General_CP1_CI_AS  NULL,
  [PasswordHash] nvarchar(max) COLLATE SQL_Latin1_General_CP1_CI_AS  NULL,
  [SecurityStamp] nvarchar(max) COLLATE SQL_Latin1_General_CP1_CI_AS  NULL,
  [ConcurrencyStamp] nvarchar(max) COLLATE SQL_Latin1_General_CP1_CI_AS  NULL
)
GO

ALTER TABLE [dbo].[WebMasters] SET (LOCK_ESCALATION = TABLE)
GO


-- ----------------------------
-- Records of WebMasters
-- ----------------------------
BEGIN TRANSACTION
GO

SET IDENTITY_INSERT [dbo].[WebMasters] ON
GO

SET IDENTITY_INSERT [dbo].[WebMasters] OFF
GO

COMMIT
GO


-- ----------------------------
-- Primary Key structure for table __EFMigrationsHistory
-- ----------------------------
ALTER TABLE [dbo].[__EFMigrationsHistory] ADD CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY CLUSTERED ([MigrationId])
WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)  
ON [PRIMARY]
GO


-- ----------------------------
-- Indexes structure for table Accidents
-- ----------------------------
CREATE NONCLUSTERED INDEX [IX_Accidents_InsuranceApproval_EmployeeId]
ON [dbo].[Accidents] (
  [InsuranceApproval_EmployeeId] ASC
)
GO

CREATE NONCLUSTERED INDEX [IX_Accidents_PoliceApproval_EmployeeId]
ON [dbo].[Accidents] (
  [PoliceApproval_EmployeeId] ASC
)
GO

CREATE NONCLUSTERED INDEX [IX_Accidents_RdaApproval_EmployeeId]
ON [dbo].[Accidents] (
  [RdaApproval_EmployeeId] ASC
)
GO

CREATE NONCLUSTERED INDEX [IX_Accidents_ReporterId]
ON [dbo].[Accidents] (
  [ReporterId] ASC
)
GO


-- ----------------------------
-- Primary Key structure for table Accidents
-- ----------------------------
ALTER TABLE [dbo].[Accidents] ADD CONSTRAINT [PK_Accidents] PRIMARY KEY CLUSTERED ([Id])
WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)  
ON [PRIMARY]
GO


-- ----------------------------
-- Indexes structure for table Employees
-- ----------------------------
CREATE NONCLUSTERED INDEX [IX_Employees_RdaOfficeId]
ON [dbo].[Employees] (
  [RdaOfficeId] ASC
)
GO


-- ----------------------------
-- Primary Key structure for table Employees
-- ----------------------------
ALTER TABLE [dbo].[Employees] ADD CONSTRAINT [PK_Employees] PRIMARY KEY CLUSTERED ([Id])
WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)  
ON [PRIMARY]
GO


-- ----------------------------
-- Indexes structure for table EmployeeUsers
-- ----------------------------
CREATE UNIQUE NONCLUSTERED INDEX [Employee_UsernameIndex]
ON [dbo].[EmployeeUsers] (
  [NormalizedUserName] ASC
)
WHERE ([NormalizedUserName] IS NOT NULL)
GO


-- ----------------------------
-- Primary Key structure for table EmployeeUsers
-- ----------------------------
ALTER TABLE [dbo].[EmployeeUsers] ADD CONSTRAINT [PK_EmployeeUsers] PRIMARY KEY CLUSTERED ([Id])
WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)  
ON [PRIMARY]
GO


-- ----------------------------
-- Indexes structure for table Images
-- ----------------------------
CREATE NONCLUSTERED INDEX [IX_Images_AccidentId]
ON [dbo].[Images] (
  [AccidentId] ASC
)
GO


-- ----------------------------
-- Primary Key structure for table Images
-- ----------------------------
ALTER TABLE [dbo].[Images] ADD CONSTRAINT [PK_Images] PRIMARY KEY CLUSTERED ([Id])
WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)  
ON [PRIMARY]
GO


-- ----------------------------
-- Indexes structure for table Insurance_Employees
-- ----------------------------
CREATE NONCLUSTERED INDEX [IX_Insurance_Employees_HeadOfficeId]
ON [dbo].[Insurance_Employees] (
  [HeadOfficeId] ASC
)
GO


-- ----------------------------
-- Primary Key structure for table Insurance_Employees
-- ----------------------------
ALTER TABLE [dbo].[Insurance_Employees] ADD CONSTRAINT [PK_Insurance_Employees] PRIMARY KEY CLUSTERED ([Id])
WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)  
ON [PRIMARY]
GO


-- ----------------------------
-- Primary Key structure for table Insurances
-- ----------------------------
ALTER TABLE [dbo].[Insurances] ADD CONSTRAINT [PK_Insurances] PRIMARY KEY CLUSTERED ([Id])
WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)  
ON [PRIMARY]
GO


-- ----------------------------
-- Indexes structure for table Police_Officers
-- ----------------------------
CREATE NONCLUSTERED INDEX [IX_Police_Officers_StationId]
ON [dbo].[Police_Officers] (
  [StationId] ASC
)
GO


-- ----------------------------
-- Primary Key structure for table Police_Officers
-- ----------------------------
ALTER TABLE [dbo].[Police_Officers] ADD CONSTRAINT [PK_Police_Officers] PRIMARY KEY CLUSTERED ([Id])
WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)  
ON [PRIMARY]
GO


-- ----------------------------
-- Primary Key structure for table PoliceStations
-- ----------------------------
ALTER TABLE [dbo].[PoliceStations] ADD CONSTRAINT [PK_PoliceStations] PRIMARY KEY CLUSTERED ([Id])
WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)  
ON [PRIMARY]
GO


-- ----------------------------
-- Indexes structure for table Rda_Employees
-- ----------------------------
CREATE NONCLUSTERED INDEX [IX_Rda_Employees_OfficeId]
ON [dbo].[Rda_Employees] (
  [OfficeId] ASC
)
GO


-- ----------------------------
-- Primary Key structure for table Rda_Employees
-- ----------------------------
ALTER TABLE [dbo].[Rda_Employees] ADD CONSTRAINT [PK_Rda_Employees] PRIMARY KEY CLUSTERED ([Id])
WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)  
ON [PRIMARY]
GO


-- ----------------------------
-- Primary Key structure for table RdaOffices
-- ----------------------------
ALTER TABLE [dbo].[RdaOffices] ADD CONSTRAINT [PK_RdaOffices] PRIMARY KEY CLUSTERED ([Id])
WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)  
ON [PRIMARY]
GO


-- ----------------------------
-- Indexes structure for table Reporters
-- ----------------------------
CREATE UNIQUE NONCLUSTERED INDEX [IX_Reporters_Crn]
ON [dbo].[Reporters] (
  [Crn] ASC
)
WHERE ([Crn] IS NOT NULL)
GO

CREATE UNIQUE NONCLUSTERED INDEX [IX_Reporters_DriverLicenseId]
ON [dbo].[Reporters] (
  [DriverLicenseId] ASC
)
WHERE ([DriverLicenseId] IS NOT NULL)
GO

CREATE UNIQUE NONCLUSTERED INDEX [IX_Reporters_Nic]
ON [dbo].[Reporters] (
  [Nic] ASC
)
WHERE ([Nic] IS NOT NULL)
GO


-- ----------------------------
-- Primary Key structure for table Reporters
-- ----------------------------
ALTER TABLE [dbo].[Reporters] ADD CONSTRAINT [PK_Reporters] PRIMARY KEY CLUSTERED ([Id])
WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)  
ON [PRIMARY]
GO


-- ----------------------------
-- Indexes structure for table ReporterUser
-- ----------------------------
CREATE UNIQUE NONCLUSTERED INDEX [Reporter_EmailIndex]
ON [dbo].[ReporterUser] (
  [NormalizedEmail] ASC
)
WHERE ([NormalizedEmail] IS NOT NULL)
GO


-- ----------------------------
-- Primary Key structure for table ReporterUser
-- ----------------------------
ALTER TABLE [dbo].[ReporterUser] ADD CONSTRAINT [PK_ReporterUser] PRIMARY KEY CLUSTERED ([Id])
WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)  
ON [PRIMARY]
GO


-- ----------------------------
-- Indexes structure for table Vehicle_Snapshots
-- ----------------------------
CREATE NONCLUSTERED INDEX [IX_Vehicle_Snapshots_Insurance_IssuerId]
ON [dbo].[Vehicle_Snapshots] (
  [Insurance_IssuerId] ASC
)
GO


-- ----------------------------
-- Primary Key structure for table Vehicle_Snapshots
-- ----------------------------
ALTER TABLE [dbo].[Vehicle_Snapshots] ADD CONSTRAINT [PK_Vehicle_Snapshots] PRIMARY KEY CLUSTERED ([AccidentId])
WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)  
ON [PRIMARY]
GO


-- ----------------------------
-- Indexes structure for table Vehicles
-- ----------------------------
CREATE NONCLUSTERED INDEX [IX_Vehicles_Insurance_IssuerId]
ON [dbo].[Vehicles] (
  [Insurance_IssuerId] ASC
)
GO


-- ----------------------------
-- Uniques structure for table Vehicles
-- ----------------------------
ALTER TABLE [dbo].[Vehicles] ADD CONSTRAINT [AK_Vehicles_ReporterId_Vrn] UNIQUE NONCLUSTERED ([ReporterId] ASC, [Vrn] ASC)
WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)  
ON [PRIMARY]
GO


-- ----------------------------
-- Primary Key structure for table Vehicles
-- ----------------------------
ALTER TABLE [dbo].[Vehicles] ADD CONSTRAINT [PK_Vehicles] PRIMARY KEY CLUSTERED ([Id])
WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)  
ON [PRIMARY]
GO


-- ----------------------------
-- Auto increment value for WebMasters
-- ----------------------------
DBCC CHECKIDENT ('[dbo].[WebMasters]', RESEED, 1)
GO


-- ----------------------------
-- Indexes structure for table WebMasters
-- ----------------------------
CREATE UNIQUE NONCLUSTERED INDEX [WebMaster_UsernameIndex]
ON [dbo].[WebMasters] (
  [NormalizedUserName] ASC
)
WHERE ([NormalizedUserName] IS NOT NULL)
GO


-- ----------------------------
-- Primary Key structure for table WebMasters
-- ----------------------------
ALTER TABLE [dbo].[WebMasters] ADD CONSTRAINT [PK_WebMasters] PRIMARY KEY CLUSTERED ([Id])
WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)  
ON [PRIMARY]
GO


-- ----------------------------
-- Foreign Keys structure for table Accidents
-- ----------------------------
ALTER TABLE [dbo].[Accidents] ADD CONSTRAINT [FK_Accidents_Insurance_Employees_InsuranceApproval_EmployeeId] FOREIGN KEY ([InsuranceApproval_EmployeeId]) REFERENCES [dbo].[Insurance_Employees] ([Id]) ON DELETE NO ACTION ON UPDATE NO ACTION
GO

ALTER TABLE [dbo].[Accidents] ADD CONSTRAINT [FK_Accidents_Police_Officers_PoliceApproval_EmployeeId] FOREIGN KEY ([PoliceApproval_EmployeeId]) REFERENCES [dbo].[Police_Officers] ([Id]) ON DELETE NO ACTION ON UPDATE NO ACTION
GO

ALTER TABLE [dbo].[Accidents] ADD CONSTRAINT [FK_Accidents_Rda_Employees_RdaApproval_EmployeeId] FOREIGN KEY ([RdaApproval_EmployeeId]) REFERENCES [dbo].[Rda_Employees] ([Id]) ON DELETE NO ACTION ON UPDATE NO ACTION
GO

ALTER TABLE [dbo].[Accidents] ADD CONSTRAINT [FK_Accidents_Reporters_ReporterId] FOREIGN KEY ([ReporterId]) REFERENCES [dbo].[Reporters] ([Id]) ON DELETE CASCADE ON UPDATE NO ACTION
GO


-- ----------------------------
-- Foreign Keys structure for table Employees
-- ----------------------------
ALTER TABLE [dbo].[Employees] ADD CONSTRAINT [FK_Employees_EmployeeUsers_Id] FOREIGN KEY ([Id]) REFERENCES [dbo].[EmployeeUsers] ([Id]) ON DELETE CASCADE ON UPDATE NO ACTION
GO

ALTER TABLE [dbo].[Employees] ADD CONSTRAINT [FK_Employees_RdaOffices_RdaOfficeId] FOREIGN KEY ([RdaOfficeId]) REFERENCES [dbo].[RdaOffices] ([Id]) ON DELETE NO ACTION ON UPDATE NO ACTION
GO


-- ----------------------------
-- Foreign Keys structure for table Images
-- ----------------------------
ALTER TABLE [dbo].[Images] ADD CONSTRAINT [FK_Images_Accidents_AccidentId] FOREIGN KEY ([AccidentId]) REFERENCES [dbo].[Accidents] ([Id]) ON DELETE CASCADE ON UPDATE NO ACTION
GO


-- ----------------------------
-- Foreign Keys structure for table Insurance_Employees
-- ----------------------------
ALTER TABLE [dbo].[Insurance_Employees] ADD CONSTRAINT [FK_Insurance_Employees_Employees_Id] FOREIGN KEY ([Id]) REFERENCES [dbo].[Employees] ([Id]) ON DELETE NO ACTION ON UPDATE NO ACTION
GO

ALTER TABLE [dbo].[Insurance_Employees] ADD CONSTRAINT [FK_Insurance_Employees_Insurances_HeadOfficeId] FOREIGN KEY ([HeadOfficeId]) REFERENCES [dbo].[Insurances] ([Id]) ON DELETE CASCADE ON UPDATE NO ACTION
GO


-- ----------------------------
-- Foreign Keys structure for table Police_Officers
-- ----------------------------
ALTER TABLE [dbo].[Police_Officers] ADD CONSTRAINT [FK_Police_Officers_Employees_Id] FOREIGN KEY ([Id]) REFERENCES [dbo].[Employees] ([Id]) ON DELETE NO ACTION ON UPDATE NO ACTION
GO

ALTER TABLE [dbo].[Police_Officers] ADD CONSTRAINT [FK_Police_Officers_PoliceStations_StationId] FOREIGN KEY ([StationId]) REFERENCES [dbo].[PoliceStations] ([Id]) ON DELETE CASCADE ON UPDATE NO ACTION
GO


-- ----------------------------
-- Foreign Keys structure for table Rda_Employees
-- ----------------------------
ALTER TABLE [dbo].[Rda_Employees] ADD CONSTRAINT [FK_Rda_Employees_Employees_Id] FOREIGN KEY ([Id]) REFERENCES [dbo].[Employees] ([Id]) ON DELETE NO ACTION ON UPDATE NO ACTION
GO

ALTER TABLE [dbo].[Rda_Employees] ADD CONSTRAINT [FK_Rda_Employees_RdaOffices_OfficeId] FOREIGN KEY ([OfficeId]) REFERENCES [dbo].[RdaOffices] ([Id]) ON DELETE CASCADE ON UPDATE NO ACTION
GO


-- ----------------------------
-- Foreign Keys structure for table Reporters
-- ----------------------------
ALTER TABLE [dbo].[Reporters] ADD CONSTRAINT [FK_Reporters_ReporterUser_Id] FOREIGN KEY ([Id]) REFERENCES [dbo].[ReporterUser] ([Id]) ON DELETE CASCADE ON UPDATE NO ACTION
GO


-- ----------------------------
-- Foreign Keys structure for table Vehicle_Snapshots
-- ----------------------------
ALTER TABLE [dbo].[Vehicle_Snapshots] ADD CONSTRAINT [FK_Vehicle_Snapshots_Accidents_AccidentId] FOREIGN KEY ([AccidentId]) REFERENCES [dbo].[Accidents] ([Id]) ON DELETE CASCADE ON UPDATE NO ACTION
GO

ALTER TABLE [dbo].[Vehicle_Snapshots] ADD CONSTRAINT [FK_Vehicle_Snapshots_Insurances_Insurance_IssuerId] FOREIGN KEY ([Insurance_IssuerId]) REFERENCES [dbo].[Insurances] ([Id]) ON DELETE CASCADE ON UPDATE NO ACTION
GO


-- ----------------------------
-- Foreign Keys structure for table Vehicles
-- ----------------------------
ALTER TABLE [dbo].[Vehicles] ADD CONSTRAINT [FK_Vehicles_Insurances_Insurance_IssuerId] FOREIGN KEY ([Insurance_IssuerId]) REFERENCES [dbo].[Insurances] ([Id]) ON DELETE CASCADE ON UPDATE NO ACTION
GO

ALTER TABLE [dbo].[Vehicles] ADD CONSTRAINT [FK_Vehicles_Reporters_ReporterId] FOREIGN KEY ([ReporterId]) REFERENCES [dbo].[Reporters] ([Id]) ON DELETE CASCADE ON UPDATE NO ACTION
GO

