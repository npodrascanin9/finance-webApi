CREATE TABLE [dbo].[Products](
	[Code] [nvarchar](10) NOT NULL,
	[RULE] [int] NOT NULL,
	[CreatedAt] [datetime] NOT NULL,
	[UpdatedAt] [datetime] NOT NULL,
	[AssignedAt] [date] NOT NULL,
	[ExpiresAt] [date] NOT NULL,
	[IsSupported]  AS (case when [ExpiresAt]<=getdate() then CONVERT([bit],(1)) else CONVERT([bit],(0)) end),
PRIMARY KEY CLUSTERED 
(
	[Code] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[Products] ADD  DEFAULT (getdate()) FOR [CreatedAt]
GO
