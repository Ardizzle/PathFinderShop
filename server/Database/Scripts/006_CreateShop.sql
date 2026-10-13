SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Shop](
	[ShopId] [uniqueidentifier] NOT NULL,
	[UserId] [uniqueidentifier] NOT NULL,
	[IsActive] [bit] NOT NULL,
	[ShopName] [varchar](200) NOT NULL,
	[ShopKeeperName] [varchar](200) NOT NULL,
	[ShopKeeperDescription] [varchar](max) NOT NULL,
	[ShopDescription] [varchar](max) NOT NULL,
	[GameMasterNotes] [varchar](max) NOT NULL,
	[ItemCostPercentIncrease] [decimal](18, 4) NOT NULL,
	[CampaignId] [uniqueidentifier] NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
ALTER TABLE [dbo].[Shop] ADD  CONSTRAINT [PK_Shop] PRIMARY KEY CLUSTERED 
(
	[ShopId] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Shop] ADD  CONSTRAINT [DEFAULT_Shop_ShopId]  DEFAULT (newid()) FOR [ShopId]
GO
ALTER TABLE [dbo].[Shop] ADD  CONSTRAINT [DEFAULT_Shop_ShopName]  DEFAULT ('') FOR [ShopName]
GO
ALTER TABLE [dbo].[Shop] ADD  CONSTRAINT [DEFAULT_Shop_ShopKeeperName]  DEFAULT ('') FOR [ShopKeeperName]
GO
ALTER TABLE [dbo].[Shop] ADD  CONSTRAINT [DEFAULT_Shop_ShopKeeperDescription]  DEFAULT ('') FOR [ShopKeeperDescription]
GO
ALTER TABLE [dbo].[Shop] ADD  CONSTRAINT [DEFAULT_Shop_ShopDescription]  DEFAULT ('') FOR [ShopDescription]
GO
ALTER TABLE [dbo].[Shop] ADD  CONSTRAINT [DEFAULT_Shop_GameMasterNotes]  DEFAULT ('') FOR [GameMasterNotes]
GO
ALTER TABLE [dbo].[Shop] ADD  CONSTRAINT [DEFAULT_Shop_ItemCostPercentIncrease]  DEFAULT ((0)) FOR [ItemCostPercentIncrease]
GO
