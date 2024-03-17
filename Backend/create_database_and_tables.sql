CREATE DATABASE Invoices_DB;
GO

USE Invoices_DB;
GO

CREATE TABLE Invoices (
    InvoiceId INT,
    LocationId INT,
    InvoiceNumber NVARCHAR(100),
    InvoiceDate DATE,
    CustomerName NVARCHAR(100),
    PRIMARY KEY (InvoiceId, LocationId)
);
GO

CREATE TABLE InvoiceDetails (
    DetailId INT PRIMARY KEY,
    LocationId INT,
    InvoiceId INT,
    ProductName NVARCHAR(100),
    Quantity DECIMAL(18, 2),
    UnitPrice DECIMAL(18, 2),
    Value DECIMAL(18, 2)
);
GO

ALTER TABLE InvoiceDetails
ADD CONSTRAINT FK_Invoices FOREIGN KEY (InvoiceId, LocationId)
REFERENCES Invoices(InvoiceId, LocationId);
GO