CREATE TABLE [dbo].[ItemCatalog] (
    [ItemCatalogId] UNIQUEIDENTIFIER CONSTRAINT [DEFAULT_ItemCatalog_ItemId] DEFAULT (newid()) NOT NULL,
    [ItemName]          VARCHAR (MAX)    NOT NULL,
    [Price]         INT              NOT NULL,
    [Level]         INT              NOT NULL,
    [Rarity]        VARCHAR (50)     NOT NULL,
    [Source]        VARCHAR (MAX)    NOT NULL,
    CONSTRAINT [PK_ItemCatalog] PRIMARY KEY CLUSTERED ([ItemCatalogId] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Price in copper', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ItemCatalog', @level2type = N'COLUMN', @level2name = N'Price';

