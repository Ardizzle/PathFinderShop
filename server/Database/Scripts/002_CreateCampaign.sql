CREATE TABLE [dbo].[Campaign] (
    [CampaignId]          UNIQUEIDENTIFIER CONSTRAINT [DEFAULT_Campaign_CampaignId] DEFAULT (newid()) NOT NULL,
    [AppUserId]           UNIQUEIDENTIFIER NOT NULL,
    [CampaignName]        VARCHAR (200)    CONSTRAINT [DEFAULT_Campaign_CampaignName] DEFAULT '' NOT NULL,
    [CampaignDescription] VARCHAR (MAX)    CONSTRAINT [DEFAULT_Campaign_CampaignDescription] DEFAULT '' NOT NULL,
    CONSTRAINT [PK_Campaign] PRIMARY KEY CLUSTERED ([CampaignId] ASC),
    CONSTRAINT [FK_Campaign_AppUser] FOREIGN KEY ([AppUserId]) REFERENCES [dbo].[AppUser] ([AppUserID])
);

CREATE INDEX IX_Campaign_AppUserId ON Campaign (AppUserId);