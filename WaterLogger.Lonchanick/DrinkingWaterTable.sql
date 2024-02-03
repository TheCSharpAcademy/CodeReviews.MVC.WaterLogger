CREATE TABLE [dbo].[DrinkingWater] (
    [ID]       INT           IDENTITY (1, 1) NOT NULL,
    [Quantity] INT           NOT NULL,
    [Date]     DATETIME2 (7) NOT NULL,
    CONSTRAINT [PK_DrinkingWater] PRIMARY KEY CLUSTERED ([ID] ASC)
);