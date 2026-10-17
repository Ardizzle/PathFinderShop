CREATE TABLE [dbo].[Shop] (
    [ShopId]                  UNIQUEIDENTIFIER CONSTRAINT [DEFAULT_Shop_ShopId] DEFAULT (newid()) NOT NULL,
    [AppUserId]               UNIQUEIDENTIFIER NOT NULL,
    [IsActive]                BIT              NOT NULL,
    [ShopName]                VARCHAR (200)    CONSTRAINT [DEFAULT_Shop_ShopName] DEFAULT ('') NOT NULL,
    [ShopKeeperName]          VARCHAR (200)    CONSTRAINT [DEFAULT_Shop_ShopKeeperName] DEFAULT ('') NOT NULL,
    [ShopKeeperDescription]   VARCHAR (MAX)    CONSTRAINT [DEFAULT_Shop_ShopKeeperDescription] DEFAULT ('') NOT NULL,
    [ShopDescription]         VARCHAR (MAX)    CONSTRAINT [DEFAULT_Shop_ShopDescription] DEFAULT ('') NOT NULL,
    [GameMasterNotes]         VARCHAR (MAX)    CONSTRAINT [DEFAULT_Shop_GameMasterNotes] DEFAULT ('') NOT NULL,
    [ItemCostPercentIncrease] DECIMAL (18, 4)  CONSTRAINT [DEFAULT_Shop_ItemCostPercentIncrease] DEFAULT ((0)) NOT NULL,
    [CampaignId]              UNIQUEIDENTIFIER NULL,
    CONSTRAINT [PK_Shop] PRIMARY KEY CLUSTERED ([ShopId] ASC),
    CONSTRAINT [FK_Shop_AppUser] FOREIGN KEY ([AppUserId]) REFERENCES [dbo].[AppUser] ([AppUserID]),
    CONSTRAINT [FK_Shop_Campaign] FOREIGN KEY ([CampaignId]) REFERENCES [dbo].[Campaign] ([CampaignId])
);

CREATE INDEX IX_Shop_AppUserId ON Shop (AppUserId);
CREATE INDEX IX_Shop_CampaignId ON Shop (CampaignId);