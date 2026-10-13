SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ItemCatalog](
	[ItemID] [uniqueidentifier] NOT NULL,
	[Name] [varchar](max) NOT NULL,
	[Price] [int] NOT NULL,
	[Level] [int] NOT NULL,
	[Rarity] [varchar](50) NOT NULL,
	[Source] [varchar](max) NOT NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
ALTER TABLE [dbo].[ItemCatalog] ADD  CONSTRAINT [PK_ItemCatalog] PRIMARY KEY CLUSTERED 
(
	[ItemID] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
ALTER TABLE [dbo].[ItemCatalog] ADD  CONSTRAINT [DEFAULT_ItemCatalog_ItemId]  DEFAULT (newid()) FOR [ItemID]
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Price in copper' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ItemCatalog', @level2type=N'COLUMN',@level2name=N'Price'
GO
