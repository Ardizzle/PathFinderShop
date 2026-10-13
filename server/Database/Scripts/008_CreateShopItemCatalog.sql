SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ShopItemCatalog](
	[ShopItemId] [uniqueidentifier] NOT NULL,
	[ShopId] [uniqueidentifier] NOT NULL,
	[ItemCatalogId] [uniqueidentifier] NOT NULL,
	[PriceOverride] [int] NULL,
	[ItemDescriptionOverride] [varchar](max) NULL,
	[GameMasterNotes] [varchar](max) NOT NULL,
	[Quantity] [int] NULL,
	[Restock] [bit] NOT NULL,
	[RestockToQuantity] [int] NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
ALTER TABLE [dbo].[ShopItemCatalog] ADD  CONSTRAINT [DEFAULT_ShopItemCatalog_ShopItemId]  DEFAULT (newid()) FOR [ShopItemId]
GO
ALTER TABLE [dbo].[ShopItemCatalog] ADD  CONSTRAINT [DEFAULT_ShopItemCatalog_GameMasterNotes]  DEFAULT ('') FOR [GameMasterNotes]
GO
