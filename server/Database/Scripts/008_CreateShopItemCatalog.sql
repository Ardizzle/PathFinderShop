CREATE TABLE [dbo].[ShopItemCatalog] (
    [ShopItemId]              UNIQUEIDENTIFIER CONSTRAINT [DEFAULT_ShopItemCatalog_ShopItemId] DEFAULT (newid()) NOT NULL,
    [ShopId]                  UNIQUEIDENTIFIER NOT NULL,
    [ItemCatalogId]           UNIQUEIDENTIFIER NOT NULL,
    [PriceOverride]           INT              NULL,
    [ItemDescriptionOverride] VARCHAR (MAX)    NULL,
    [GameMasterNotes]         VARCHAR (MAX)    CONSTRAINT [DEFAULT_ShopItemCatalog_GameMasterNotes] DEFAULT ('') NOT NULL,
    [Quantity]                INT              NULL,
    [Restock]                 BIT              NOT NULL,
    [RestockToQuantity]       INT              NULL,
    CONSTRAINT [FK_ShopItemCatalog_ItemCatalog] FOREIGN KEY ([ItemCatalogId]) REFERENCES [dbo].[ItemCatalog] ([ItemCatalogId]),
    CONSTRAINT [FK_ShopItemCatalog_Shop] FOREIGN KEY ([ShopId]) REFERENCES [dbo].[Shop] ([ShopId])
);

CREATE INDEX IX_ShopItemCatalog_ShopId ON ShopItemCatalog (ShopId);