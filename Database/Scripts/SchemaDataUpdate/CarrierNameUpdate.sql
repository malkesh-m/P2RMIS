-- First Update Carrier in Dev to have matching data in Carrier table in both Prod and Dev
DELETE Carrier where Name = 'American'
DELETE Carrier where Name = 'Delta'
DELETE Carrier where Name = 'JetBlue'
DELETE Carrier where Name = 'Southwest'
DELETE Carrier where Name = 'Spirit'
DELETE Carrier where Name = 'Sun Country'
DELETE Carrier where Name = 'Tonga'
DELETE Carrier where Name = 'Turkish'
DELETE Carrier where Name = 'TWA'
DELETE Carrier where Name = 'United'
DELETE Carrier where Name = 'US Air'
DELETE Carrier where Name = 'Value Jet'
DELETE Carrier where Name = 'Virgin America'
DELETE Carrier where Name = 'Southern Airways Express'


SET IDENTITY_INSERT Carrier ON
INSERT INTO Carrier (CarrierId,Name,CreatedBy,CreatedDate,ModifiedBy,ModifiedDate,DeletedFlag,DeletedBy,DeletedDate)
VALUES(7,'American Airlines',10,dbo.GetP2rmisDateTime(),10,dbo.GetP2rmisDateTime(),0,null,null)
SET IDENTITY_INSERT Carrier ON
INSERT INTO Carrier (CarrierId,Name,CreatedBy,CreatedDate,ModifiedBy,ModifiedDate,DeletedFlag,DeletedBy,DeletedDate)
VALUES(15,'Delta Airlines',10,dbo.GetP2rmisDateTime(),10,dbo.GetP2rmisDateTime(),0,null,null)
SET IDENTITY_INSERT Carrier ON
INSERT INTO Carrier (CarrierId,Name,CreatedBy,CreatedDate,ModifiedBy,ModifiedDate,DeletedFlag,DeletedBy,DeletedDate)
VALUES(19,'Jetblue Airways',10,dbo.GetP2rmisDateTime(),10,dbo.GetP2rmisDateTime(),0,null,null)
SET IDENTITY_INSERT Carrier ON
INSERT INTO Carrier (CarrierId,Name,CreatedBy,CreatedDate,ModifiedBy,ModifiedDate,DeletedFlag,DeletedBy,DeletedDate)
VALUES(32,'Southern Airways Exp LLC',10,dbo.GetP2rmisDateTime(),10,dbo.GetP2rmisDateTime(),0,null,null)
SET IDENTITY_INSERT Carrier ON
INSERT INTO Carrier (CarrierId,Name,CreatedBy,CreatedDate,ModifiedBy,ModifiedDate,DeletedFlag,DeletedBy,DeletedDate)
VALUES(33,'Southwest Airlines',10,dbo.GetP2rmisDateTime(),10,dbo.GetP2rmisDateTime(),0,null,null)
SET IDENTITY_INSERT Carrier ON
INSERT INTO Carrier (CarrierId,Name,CreatedBy,CreatedDate,ModifiedBy,ModifiedDate,DeletedFlag,DeletedBy,DeletedDate)
VALUES(34,'Spirit',10,dbo.GetP2rmisDateTime(),10,dbo.GetP2rmisDateTime(),0,null,null)
SET IDENTITY_INSERT Carrier ON
INSERT INTO Carrier (CarrierId,Name,CreatedBy,CreatedDate,ModifiedBy,ModifiedDate,DeletedFlag,DeletedBy,DeletedDate)
VALUES(35,'Sun Country',10,dbo.GetP2rmisDateTime(),10,dbo.GetP2rmisDateTime(),0,null,null)
SET IDENTITY_INSERT Carrier ON
INSERT INTO Carrier (CarrierId,Name,CreatedBy,CreatedDate,ModifiedBy,ModifiedDate,DeletedFlag,DeletedBy,DeletedDate)
VALUES(36,'Tonga',10,dbo.GetP2rmisDateTime(),10,dbo.GetP2rmisDateTime(),0,null,null)
SET IDENTITY_INSERT Carrier ON
INSERT INTO Carrier (CarrierId,Name,CreatedBy,CreatedDate,ModifiedBy,ModifiedDate,DeletedFlag,DeletedBy,DeletedDate)
VALUES(37,'Turkish',10,dbo.GetP2rmisDateTime(),10,dbo.GetP2rmisDateTime(),0,null,null)
SET IDENTITY_INSERT Carrier ON
INSERT INTO Carrier (CarrierId,Name,CreatedBy,CreatedDate,ModifiedBy,ModifiedDate,DeletedFlag,DeletedBy,DeletedDate)
VALUES(38,'TWA',10,dbo.GetP2rmisDateTime(),10,dbo.GetP2rmisDateTime(),0,null,null)
SET IDENTITY_INSERT Carrier ON
INSERT INTO Carrier (CarrierId,Name,CreatedBy,CreatedDate,ModifiedBy,ModifiedDate,DeletedFlag,DeletedBy,DeletedDate)
VALUES(39,'United Airlines',10,dbo.GetP2rmisDateTime(),10,dbo.GetP2rmisDateTime(),0,null,null)
SET IDENTITY_INSERT Carrier ON
INSERT INTO Carrier (CarrierId,Name,CreatedBy,CreatedDate,ModifiedBy,ModifiedDate,DeletedFlag,DeletedBy,DeletedDate)
VALUES(40,'US Air',10,dbo.GetP2rmisDateTime(),10,dbo.GetP2rmisDateTime(),0,null,null)
SET IDENTITY_INSERT Carrier ON
INSERT INTO Carrier (CarrierId,Name,CreatedBy,CreatedDate,ModifiedBy,ModifiedDate,DeletedFlag,DeletedBy,DeletedDate)
VALUES(41,'Value Jet',10,dbo.GetP2rmisDateTime(),10,dbo.GetP2rmisDateTime(),0,null,null)
SET IDENTITY_INSERT Carrier ON
INSERT INTO Carrier (CarrierId,Name,CreatedBy,CreatedDate,ModifiedBy,ModifiedDate,DeletedFlag,DeletedBy,DeletedDate)
VALUES(42,'Virgin America',10,dbo.GetP2rmisDateTime(),10,dbo.GetP2rmisDateTime(),0,null,null)
SET IDENTITY_INSERT Carrier OFF

